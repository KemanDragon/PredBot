#nullable disable
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoreConcurrentThings {

	/// <summary>
	/// An implementation of a <see cref="List{T}"/> that has elements inserted at the start and pushed off of the end. This implementation is thread-safe.
	/// </summary>
	/// <remarks>
	/// Unlike other concurrent objects as seen in <see cref="System.Collections.Concurrent"/>, this uses a simple <see langword="lock"/> call. Unfortunately, this is the only easy + reliable way to allow indexing things by position in a list.
	/// </remarks>
	/// <typeparam name="T"></typeparam>
	public class ConcurrentLimitedSpaceArray<T> : IEnumerable<T> {
		private T[] ObjectsInternal { get; }

		/// <summary>
		/// A list of every object in this array.
		/// </summary>
		public IReadOnlyList<T> Objects {
			get {
				lock (ObjectsInternal) {
					return ObjectsInternal.ToList().AsReadOnly();
				}
			}
		}

		/// <summary>
		/// The maximum amount of objects this <see cref="ConcurrentLimitedSpaceArray{T}"/> can contain.
		/// </summary>
		public int Capacity { get; }

		/// <summary>
		/// The number of non-null objects this <see cref="ConcurrentLimitedSpaceArray{T}"/> contains.
		/// </summary>
		public int Count {
			get {
				lock (ObjectsInternal) {
					int count = 0;
					foreach (T obj in ObjectsInternal) {
						if (obj != null) {
							count++;
						} else {
							return count;
						}
					}
					return count;
				}
			}
		}

		/// <summary>
		/// The object type that this <see cref="ConcurrentLimitedSpaceArray{T}"/> stores.
		/// </summary>
		public Type ArrayType => typeof(T);

		/// <summary>
		/// If <see cref="ArrayType"/> extends <see cref="IDisposable"/>, and this is <see langword="true"/>, objects bumped off the end of the array will have their <see cref="IDisposable.Dispose()"/> method called.
		/// </summary>
		public bool DisposeOfBumpedObjects { get; }

		/// <summary>
		/// Construct a limited space array with the specified capacity.<para/>
		/// If <paramref name="disposeBumpedObjects"/> is <see langword="true"/>, and <typeparamref name="T"/> implements <see cref="IDisposable"/>, then objects that are bumped off the end of the array will have their <see cref="IDisposable.Dispose()"/> method called.
		/// </summary>
		/// <param name="capacity">The maximum amount of elements in this array. Adding new elements that result in the object count exceeding this will cause the oldest element to be discarded.</param>
		/// <param name="disposeBumpedObjects">If <typeparamref name="T"/> implements <see cref="IDisposable"/>, then objects that are bumped off the end of the array will have their <see cref="IDisposable.Dispose()"/> method called.</param>
		public ConcurrentLimitedSpaceArray(int capacity, bool disposeBumpedObjects = false) {
			ObjectsInternal = new T[capacity];
			Capacity = capacity;
			DisposeOfBumpedObjects = disposeBumpedObjects;
		}

		/// <summary>
		/// Adds an object to the start of this array. If adding this object causes the amount of objects in the array to exceed its limit, the oldest object (the object at the highest index) will be discarded.
		/// </summary>
		/// <param name="obj">The object to add.</param>
		public void Push(T obj) {
			lock (ObjectsInternal) {
				// Update: If the type extends IDisposable then we need to dispose of the last object if it's getting bumped off.
				if (DisposeOfBumpedObjects) {
					T lastObject = ObjectsInternal.Last();
					if (lastObject != null && lastObject is IDisposable disposableObject)
						disposableObject.Dispose();
				}

				for (int idx = ObjectsInternal.Length - 1; idx >= 0; idx--) {
					if (idx < ObjectsInternal.Length - 1) {
						ObjectsInternal[idx + 1] = ObjectsInternal[idx];
					}
					if (idx == 0) {
						ObjectsInternal[idx] = obj;
					}
				}
			}
		}

		/// <summary>
		/// Attempts to add the given object to the end of the <see cref="ConcurrentLimitedSpaceArray{T}"/>. If no slots are available, this throws <see cref="InvalidOperationException"/>.
		/// </summary>
		/// <param name="obj"></param>
		/// <exception cref="InvalidOperationException">If the <see cref="ConcurrentLimitedSpaceArray{T}"/> is full.</exception>
		public void AddToEnd(T obj) {
			lock (ObjectsInternal) {
				for (int idx = 0; idx < ObjectsInternal.Length; idx++) {
					if (ObjectsInternal[idx]?.Equals(default) ?? true) {
						ObjectsInternal[idx] = obj;
						break;
					}
				}
			}
		}

		/// <summary>
		/// Removes <paramref name="obj"/> from this array, and then pulls all objects that were ahead of it backwards by 1 index, setting the last index of the array to <see langword="default"/>
		/// </summary>
		/// <param name="obj"></param>
		/// <returns><see langword="true"/> if the object was found in this <see cref="ConcurrentLimitedSpaceArray{T}"/> and subsequently removed, and <see langword="false"/> if it was not.</returns>
		/// <exception cref="NullReferenceException">If the object does not exist in the array.</exception>
		public bool Pull(T obj) {
			lock (ObjectsInternal) {
				int i = -1;
				for (int idx = 0; idx < ObjectsInternal.Length; idx++) {
					if (ObjectsInternal[idx].Equals(obj)) {
						i = idx;
					}
				}
				if (i == -1)
					return false;

				if (DisposeOfBumpedObjects) {
					if (obj is IDisposable disposableObject)
						disposableObject.Dispose();
				}

				for (int idx = i; idx < ObjectsInternal.Length - 1; idx++) {
					ObjectsInternal[idx] = ObjectsInternal[idx + 1];
				}
				ObjectsInternal[^1] = default;
				return true;
			}
		}

		/// <summary>
		/// Returns true if this <see cref="ConcurrentLimitedSpaceArray{T}"/> contains the specified object, and false if it does not.
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public bool Contains(T obj) {
			lock (ObjectsInternal) {
				return ObjectsInternal.Contains(obj);
			}
		}

		/// <summary>
		/// Clears the contents of this <see cref="ConcurrentLimitedSpaceArray{T}"/>.
		/// </summary>
		/// <returns></returns>
		public void Clear() {
			lock (ObjectsInternal) {
				for (int index = 0; index < ObjectsInternal.Length; index++) {
					T obj = ObjectsInternal[index];
					if (DisposeOfBumpedObjects && obj is IDisposable disposableObject)
						disposableObject.Dispose();
					ObjectsInternal[index] = default;
				}
			}
		}

		/// <param name="predicate"></param>
		/// <returns>The first object that satisfies the predicate, or <see langword="default"/> if no item did such a thing.</returns>
		public T Find(Predicate<T> predicate) {
			lock (ObjectsInternal) {
				for (int index = 0; index < ObjectsInternal.Length; index++) {
					T obj = ObjectsInternal[index];
					if (predicate(obj)) {
						return obj;
					}
				}
				return default;
			}
		}

		/// <inheritdoc/>
		public IEnumerator<T> GetEnumerator() {
			lock (ObjectsInternal) {
				T[] result = new T[ObjectsInternal.Length];
				ObjectsInternal.CopyTo(result, 0);
				return (IEnumerator<T>)result.GetEnumerator();
			}
		}

		IEnumerator IEnumerable.GetEnumerator() {
			lock (ObjectsInternal) {
				T[] result = new T[ObjectsInternal.Length];
				ObjectsInternal.CopyTo(result, 0);
				return result.GetEnumerator();
			}
		}

		/// <summary>
		/// Index a component of this <see cref="ConcurrentLimitedSpaceArray{T}"/> by a numeric value.
		/// </summary>
		/// <remarks>
		/// Generally speaking, this should be handled with care as it may not return proper results.
		/// </remarks>
		/// <param name="index"></param>
		/// <returns></returns>
		public T this[int index] {
			get {
				lock (ObjectsInternal) {
					return ObjectsInternal[index];
				}
			}
			set {
				lock (ObjectsInternal) {
					ObjectsInternal[index] = value;
				}
			}
		}
	}
}
