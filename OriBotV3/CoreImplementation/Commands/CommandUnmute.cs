using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EtiBotCore.Data.Structs;
using EtiBotCore.DiscordObjects.Guilds;
using EtiBotCore.DiscordObjects.Guilds.ChannelData;
using EtiBotCore.Utility.Marshalling;
using OriBotV3.Data;
using OriBotV3.Data.Commands.ArgData;
using OriBotV3.Exceptions;
using OriBotV3.Interaction;
using OriBotV3.PermissionData;
using OriBotV3.Utility;
using OriBotV3.Utility.Arguments;

namespace OriBotV3.CoreImplementation.Commands {
	public class CommandUnmute : DeprecatedCommand {
		public override string Name { get; } = "unmute";
		public override PermissionLevel RequiredPermissionLevel { get; } = PermissionLevel.Operator;
		public override bool RequiresContext { get; } = true;
		public CommandUnmute(CommandMute.CommandMuteRemove cmd) : base(cmd) { }
	}
}
