using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace SaltyPiss.Sodium {
	public static class SodiumWrapper {

		#region DLL

		private const string SODIUM = @".\libs\sodium.dll";

		/// <summary>
		/// The amount of bytes in MAC.
		/// </summary>
		private static readonly int MACBYTES;

		[DllImport(SODIUM, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Auto, SetLastError = true)]
		private static extern int sodium_init();

		[DllImport(SODIUM, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Auto, SetLastError = true)]
		private static extern int crypto_secretbox_easy(byte[] resultBuffer, byte[] message, ulong messageLength, byte[] nonce, byte[] key);

		[DllImport(SODIUM, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Auto, SetLastError = true)]
		private static extern UIntPtr crypto_secretbox_xsalsa20poly1305_macbytes();

		#endregion

		static SodiumWrapper() {
			sodium_init();
			MACBYTES = (int)crypto_secretbox_xsalsa20poly1305_macbytes();
		}

		public static byte[] Encrypt(byte[] data, byte[] nonce, byte[] key) {
			byte[] result = new byte[data.Length + MACBYTES];
			crypto_secretbox_easy(result, data, (ulong)data.LongLength, nonce, key);
			return result;
		}

	}
}
