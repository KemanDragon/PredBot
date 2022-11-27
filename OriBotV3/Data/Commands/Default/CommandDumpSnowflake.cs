using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EtiBotCore.Data.Structs;
using EtiBotCore.DiscordObjects.Factory;
using EtiBotCore.DiscordObjects.Guilds;
using EtiBotCore.DiscordObjects.Guilds.ChannelData;
using EtiBotCore.DiscordObjects.Universal.Data;
using OriBotV3.Exceptions;
using OriBotV3.Interaction;
using OriBotV3.Utility;
using OriBotV3.Utility.Arguments;
using OriBotV3.Utility.Formatting;
using OriBotV3.Utility.Responding;

namespace OriBotV3.Data.Commands.Default {
	public class CommandDumpSnowflake : Command {
		public CommandDumpSnowflake() : base(null) { }
		public override string Name { get; } = "about";
		public override string Description { get; } = "Dump the information on a Snowflake";
		public override ArgumentMapProvider Syntax { get; } = new ArgumentMapProvider<Snowflake>("snowflake").SetRequiredState(true);

		public override async Task ExecuteCommandAsync(Member executor, BotContext executionContext, Message originalMessage, string[] argArray, string rawArgs, bool isConsole) {
			if (argArray.Length > 1) {
				throw new CommandException(this, Personality.Get("cmd.err.tooManyArgs"));
			} else if (argArray.Length == 0) {
				throw new CommandException(this, Personality.Get("cmd.err.missingArgs", Syntax.GetArgName(0)));
			}

			EmbedBuilder embed = new EmbedBuilder {
				Title = "Snowflake Data",
				Description = "This is the data contained within the ID you gave me."
			};

			Snowflake id = Syntax.Parse<Snowflake>(argArray[0]).Arg1;
			embed.AddField("Creation Date", id.GetDisplayTimestampMS());
			embed.AddField("Age", (DateTimeOffset.UtcNow - id.ToDateTimeOffset()).GetTimeDifference());
			embed.AddField("Internal Worker ID", id.InternalWorkerID.ToString(), true);
			embed.AddField("Internal Process ID", id.InternalProcessID.ToString(), true);
			embed.AddField("Increment", id.Increment.ToString(), true);
			embed.SetFooter("Dates are in the order DD/MM/YYYY", new Uri(Images.INFORMATION));

			await ResponseUtil.RespondToAsync(originalMessage, CommandLogger, null, embed.Build(), AllowedMentions.Reply);
		}
	}
}
