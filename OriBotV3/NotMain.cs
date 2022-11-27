using System;
using System.Threading.Tasks;
using EtiBotCore.Client;
using EtiBotCore.DiscordObjects.Guilds;
using EtiBotCore.Payloads.Data;
using EtiLogger.Logging;
using EtiBotCore.Data.Structs;
using EtiBotCore.Utility.Extension;
using EtiBotCore.DiscordObjects.Universal.Data;
using System.IO;
using OriBotV3.Data;
using OriBotV3.Data.Commands.Default;
using OriBotV3.CoreImplementation;
using OriBotV3.Data.Commands;
using System.Windows.Forms;

namespace OriBotV3 {
	class NotMain {

		[STAThread]
		static void Main() {
			Application.SetHighDpiMode(HighDpiMode.SystemAware);
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new BotWindow());
		}
		
	}
}
