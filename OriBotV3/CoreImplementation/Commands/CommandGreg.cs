using EtiBotCore.DiscordObjects.Guilds;
using EtiBotCore.DiscordObjects.Guilds.ChannelData;
using OriBotV3.Interaction;
using OriBotV3.Utility.Arguments;
using OriBotV3.Utility.Responding;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OriBotV3.CoreImplementation.Commands {
	public class CommandGreg : Command {
		public override string Name { get; } = "greg";
		public override string Description { get; } = "greg";
		public override ArgumentMapProvider Syntax { get; }

		public CommandGreg(BotContext ctx) : base(ctx) { }

		public override async Task ExecuteCommandAsync(Member executor, BotContext executionContext, Message originalMessage, string[] argArray, string rawArgs, bool isConsole) {
			await ResponseUtil.RespondToAsync(originalMessage, CommandLogger, "greg");
		}
	}
}
