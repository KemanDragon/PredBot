using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoreConcurrentThings {

	/// <summary>
	/// Akin to <see cref="ConcurrentBag{T}"/>, but this also provides a means of removing items from the container as well.<para/>
	/// Unfortunately, due to the nature of multithreaded applications, it is not safe (nor implemented, by extension) to address things by numeric index.
	/// </summary>
	/// <remarks>
	/// This is just a fancy wrapper for a <see cref="ConcurrentDictionary{TKey, TValue}"/>
	/// </remarks>
	/// <typeparam name="T"></typeparam>
	public sealed class ConcurrentList<T> : IEnumerable<T> where T : notnull {

		private readonly ConcurrentDictionary<T, T> InnerDict; // lol

		/// <summary>
		/// Construct a new, empty <see cref="ConcurrentList{T}"/>
		/// </summary>
		public ConcurrentList() : this(0) { }

		/// <summary>
		/// Construct a new <see cref="ConcurrentList{T}"/> with the given number of elements pre-allocated.
		/// </summary>
		public ConcurrentList(int capacity) {
			InnerDict = new ConcurrentDictionary<T, T>(4, capacity);
		}

		/// <summary>
		/// Construct a new <see cref="ConcurrentList{T}"/> with the given elements inside.
		/// </summary>
		/// <param name="elements"></param>
		public ConcurrentList(IEnumerable<T> elements) {
			InnerDict = new ConcurrentDictionary<T, T>(4, elements.Count());
			foreach (T element in elements) {
				InnerDict.TryAdd(element, element);
			}
		}

		/// <summary>
		/// The amount of elements in this <see cref="ConcurrentList{T}"/>
		/// </summary>
		public int Count => InnerDict.Count;

		/// <summary>
		/// Adds the given item to the <see cref="ConcurrentList{T}"/>.
		/// </summary>
		/// <param name="item"></param>
		public void Add(T item) => InnerDict.TryAdd(item, item);

		/// <summary>
		/// Adds all of the items from the given <see cref="IEnumerable{T}"/> into this <see cref="ConcurrentList{T}"/>.
		/// </summary>
		/// <param name="items"></param>
		public void AddRange(IEnumerable<T> items) {
			foreach (T item in items) {
				Add(item);
			}
		}

		/// <summary>
		/// Returns whether or not the <see cref="ConcurrentList{T}"/> contains the given item.
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		public bool Contains(T item) => InnerDict.ContainsKey(item);

		/// <summary>
		/// Removes the given item from the <see cref="ConcurrentList{T}"/>
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		public bool Remove(T item) => InnerDict.TryRemove(item, out T _);

		/// <summary>
		/// Empties the contents of the <see cref="ConcurrentList{T}"/>.
		/// </summary>
		public void Clear() => InnerDict.Clear();

		/// <summary>
		/// Returns a snapshot of the <see cref="ConcurrentList{T}"/> as it exists now in the form of an array.
		/// </summary>
		/// <returns></returns>
		public T[] ToArray() {
			KeyValuePair<T, T>[] elements = InnerDict.ToArray();
			T[] output = new T[elements.Length];
			for (int index = 0; index < output.Length; index++) {
				output[index] = elements[index].Key;
			}
			return output;
		}

		/// <summary>
		/// Returns a snapshot of the <see cref="ConcurrentList{T}"/> as it exists now in the form of a <see cref="List{T}"/>.
		/// </summary>
		/// <returns></returns>
		public List<T> ToList() {
			KeyValuePair<T, T>[] elements = InnerDict.ToArray();
			List<T> output = new List<T>(elements.Length);
			for (int index = 0; index < output.Count; index++) {
				output[index] = elements[index].Key;
			}
			return output;
		}

		/// <summary>
		/// Returns a snapshot of the <see cref="ConcurrentList{T}"/> as it exists now in the form of a <see cref="List{K}"/>, 
		/// where every <typeparamref name="T"/> within the list is cast into <typeparamref name="K"/>.
		/// </summary>
		/// <returns></returns>
		public List<K> ToListOf<K>() where K : T {
			KeyValuePair<T, T>[] elements = InnerDict.ToArray();
			List<K> output = new List<K>(elements.Length);
			for (int index = 0; index < output.Count; index++) {
				output[index] = (K)elements[index].Key;
			}
			return output;
		}

		/// <summary>
		/// Attempts to find an element in this <see cref="ConcurrentList{T}"/>
		/// </summary>
		/// <param name="predicate"></param>
		/// <returns>The item that matched the predicate first, or <see langword="default"/></returns>
		public T Find(Predicate<T> predicate) {
			KeyValuePair<T, T>[] elements = InnerDict.ToArray();
			for (int index = 0; index < elements.Length; index++) {
				T item = elements[index].Key;
				if (predicate(item)) {
					return item;
				}
			}
			return default!;
		}

		/// <summary>
		/// Returns a snapshot of this <see cref="ConcurrentList{T}"/> as a <see cref="IReadOnlyList{T}"/>
		/// </summary>
		/// <returns></returns>
		public IReadOnlyList<T> AsReadOnly() => ToList().AsReadOnly();

		/// <summary>
		/// Creates a lazy copy of this <see cref="ConcurrentList{T}"/> where the returned list is not the same reference as this list, but every included element <em>does</em> share references.
		/// </summary>
		/// <returns></returns>
		public ConcurrentList<T> LazyCopy() {
			return new ConcurrentList<T>(ToArray());
		}

		/// <inheritdoc/>
		public IEnumerator<T> GetEnumerator() => InnerDict.Values.GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() => InnerDict.Values.GetEnumerator();
	}
}
