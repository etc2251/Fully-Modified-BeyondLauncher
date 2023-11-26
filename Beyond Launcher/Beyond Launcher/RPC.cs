using DiscordRPC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Beyond_Launcher
{
    internal class RPC
    {
        public static DiscordRpcClient Client = new DiscordRpcClient("");
        public static string name = "";
        public static void Init()
        {

            Client.OnReady += (sendre, e) =>
            {
               

            };


            /*
                        Client.SetPresence(new RichPresence()
                        {
                            Details = "Launching Beyond",
                            Timestamps = Timestamps.Now
                        }); */
        }
    }
}
