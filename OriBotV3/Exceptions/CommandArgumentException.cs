using OriBotV3.Interaction;
using OriBotV3.Utility.Arguments;
using OriBotV3.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace OriBotV3.Exceptions {
	public class CommandArgumentException : Exception {

		public CommandArgumentException(ArgumentMapProvider source, int argIndex, object value) : base(
			$"Invalid input for argument #{argIndex} `{source.GetArgName(argIndex)}` - Attempted to turn value `{value.ToString().EscapeAllDiscordMarkdown().LimitCharCount(32, true)}` into a(n) {source.GetArgTypeName(argIndex)}. You can use `>> typeinfo {source.GetArgTypeName(argIndex)}` for more information on this type."
		) { }

	}
}
