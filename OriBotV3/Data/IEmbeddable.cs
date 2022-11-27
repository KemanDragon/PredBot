using System;
using System.Collections.Generic;
using System.Text;
using EtiBotCore.DiscordObjects.Universal;

namespace OriBotV3.Data {

	/// <summary>
	/// Represents something that can be turned into a Discord Embed
	/// </summary>
	public interface IEmbeddable {

		/// <summary>
		/// Translate this object into an embed.
		/// </summary>
		/// <returns></returns>
		public Embed ToEmbed();

	}
}
