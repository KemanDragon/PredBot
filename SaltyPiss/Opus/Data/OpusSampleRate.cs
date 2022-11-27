using System;
using System.Collections.Generic;
using System.Text;

namespace SaltyPiss.Opus.Data {

	/// <summary>
	/// Represents a valid sample rate for use in Opus. This is specifically for the input signal.
	/// </summary>
	public enum OpusSampleRate {

		/// <summary>
		/// An invalid sample rate.
		/// </summary>
		Invalid = 0,

		/// <summary>
		/// The input audio being encoded is 8kHz
		/// </summary>
		Rate8000 = 8000,

		/// <summary>
		/// The input audio being encoded is 12kHz
		/// </summary>
		Rate12000 = 12000,

		/// <summary>
		/// The input audio being encoded is 16kHz
		/// </summary>
		Rate16000 = 16000,

		/// <summary>
		/// The input audio being encoded is 24kHz
		/// </summary>
		Rate24000 = 24000,

		/// <summary>
		/// The input audio being encoded is 48kHz
		/// </summary>
		Rate48000 = 48000

	}
}
