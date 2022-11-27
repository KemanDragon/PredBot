using System;
using System.Collections.Generic;
using System.Text;

namespace SaltyPiss.Opus.Data {
	public enum OpusError {

		/// <summary>
		/// Everything looks good here.
		/// </summary>
		OPUS_OK = 0,

		/// <summary>
		/// An argument was invalid.
		/// </summary>
		OPUS_BAD_ARG = -1,

		/// <summary>
		/// The output buffer was too small.
		/// </summary>
		OPUS_BUFFER_TOO_SMALL = -2,

		/// <summary>
		/// An internal error has occurred.
		/// </summary>
		OPUS_INTERNAL_ERROR = -3,

		/// <summary>
		/// An invalid packet was sent for decoding.
		/// </summary>
		OPUS_INVALID_PACKET = -4,

		/// <summary>
		/// This feature is not implemented.
		/// </summary>
		OPUS_UNIMPLEMENTED = -5,

		/// <summary>
		/// An encoder or decoder structure is invalid or already freed.
		/// </summary>
		OPUS_INVALID_STATE = -6,

		/// <summary>
		/// Memory allocation has failed.
		/// </summary>
		OPUS_ALLOC_FAIL = -7,

	}
}
