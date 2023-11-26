using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;
using System.IO;
using FortClass;

namespace Beyond_Launcher
{
    internal class Mods
    {
        class Mod
        {
            public string name { get; set; }
            public string pak { get; set; }
            public string sig { get; set; }
        }

        
       
        public static void DownloadRequiredPaks()
        {
          
                /*        string loaded = wc.DownloadString("http://135.148.86.165:3551/mods");
                        List<Mod> mods = JsonConvert.DeserializeObject<List<Mod>>(loaded);
        */
                if (File.Exists(Properties.Settings1.Default.Path + "\\FortniteGame\\Content\\Paks\\pakchunkBeyond-WindowsClient.pak"))
                {
                    File.Delete(Properties.Settings1.Default.Path + "\\FortniteGame\\Content\\Paks\\pakchunkBeyond-WindowsClient.pak");
              
                }
                if (File.Exists(Properties.Settings1.Default.Path + "\\FortniteGame\\Content\\Paks\\pakchunkBeyond-WindowsClient.sig"))
                {
                    File.Delete(Properties.Settings1.Default.Path + "\\FortniteGame\\Content\\Paks\\pakchunkBeyond-WindowsClient.sig");
                }
      
                Fortnite.DownloadFile("http://135.148.86.165:3000/downloadbeyondpak", Properties.Settings1.Default.Path + "\\FortniteGame\\Content\\Paks\\pakchunkBeyond-WindowsClient.pak");
                Fortnite.DownloadFile("http://135.148.86.165:3000/downloadbeyondsig", Properties.Settings1.Default.Path + "\\FortniteGame\\Content\\Paks\\pakchunkBeyond-WindowsClient.sig");
              
            
               

                
        }

        
    }
}
