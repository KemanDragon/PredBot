using System;
using System.Collections.Generic;
using System.Text;

namespace SaltyPiss.Opus.Data {
	public class OpusException : Exception {

		/// <summary>
		/// The error code.
		/// </summary>
		public OpusError Error { get; }

		public OpusException(OpusError error) : base("An Opus error has occurred! " + error.ToString()) {
			Error = error;
		}

	}
}
