using System;
using System.Collections.Generic;
using System.Text;

namespace SaltyPiss.Opus.Data {

	/// <summary>
	/// Represents a valid frame size for opus at 48kHz
	/// </summary>
	public enum OpusFrameSize {

		/// <summary>
		/// An invalid frame size.
		/// </summary>
		Invalid = 0,

		/// <summary>
		/// The frame is 120 bytes.
		/// </summary>
		Frame120 = 120,

		/// <summary>
		/// The frame is 240 bytes.
		/// </summary>
		Frame240 = 240,

		/// <summary>
		/// The frame is 480 bytes.
		/// </summary>
		Frame480 = 480,

		/// <summary>
		/// The frame is 960 bytes.
		/// </summary>
		Frame960 = 960,

		/// <summary>
		/// The frame is 1920 bytes.
		/// </summary>
		Frame1920 = 1920,

		/// <summary>
		/// The frame is 2880 bytes.
		/// </summary>
		Frame2880 = 2880

	}
}
