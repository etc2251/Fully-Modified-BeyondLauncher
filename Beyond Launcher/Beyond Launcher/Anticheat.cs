
using Beyond_Launcher.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CUE4Parse;
using CUE4Parse.FileProvider;
using CUE4Parse.UE4.Pak;
using CUE4Parse.UE4.VirtualFileSystem;
using CUE4Parse.UE4.Assets;
using CUE4Parse.UE4.Assets.Readers;
using CUE4Parse.GameTypes.FN.Assets.Exports;
using CUE4Parse.UE4.Readers;
using System.ComponentModel.DataAnnotations;

namespace Beyond_Launcher
{
    internal class Anticheat
    {
        public static string getHwid()
        {
            var mbs = new ManagementObjectSearcher("Select ProcessorId From Win32_processor");
            ManagementObjectCollection mbsList = mbs.Get();

            string id = "";
            foreach (ManagementObject mo in mbsList)
            {
                id = mo["ProcessorId"].ToString();
                break;
            }

            return id;
        }

        public static bool hasHwid(string email)
        {
            bool result = false;
            using (WebClient webClient = new WebClient())
            {
                string a = webClient.DownloadString(new Uri("http://135.148.86.165:3551/backend/" + email + "/isBanned"));
                
                bool flag = a == "empty";
                bool notFound = a == "notfound";
                if (notFound)
                {
                    MessageBox.Show("Incorrect Email!");
                    result = false;
                }
                if (flag)
                {
                    Anticheat.sendHwid(email);
                }
                else
                {
                    bool flag2 = a == "true";
                    result = flag2;
                }
            }
            using (WebClient webClient2 = new WebClient())
            {
                string a2 = webClient2.DownloadString(new Uri("http://135.148.86.165:3551/backend/hwid/" + getHwid() + "/isBanned"));
                bool flag3 = a2 == "empty";
                bool notFound = a2 == "notfound";
                if (notFound)
                {
                    MessageBox.Show("Incorrect Email!");
                    result = false;
                   
                }
                if (flag3)
                {
                    Anticheat.sendHwid(email);
                }
                else
                {
                    bool flag4 = a2 == "true";
                    result = flag4;
                }
            }
            return result;
        }

        public static bool sendHwid(string email)
        {
            bool result = false;
            string hwid = getHwid();
            // Create a HttpClient to send the request
            using (WebClient webClient = new WebClient())
            {
            
                string response = webClient.DownloadString(new Uri($"http://135.148.86.165:3551/backend/{email}/pushHwid/{hwid}"));


                if (response == "done")
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            }

            return result;

        }
        public static bool Scan()
        {
            bool result = false;
            string path = Settings1.Default.Path;
            string paks = path + "\\FortniteGame\\Content\\Paks";
            var provider = new DefaultFileProvider(paks, SearchOption.TopDirectoryOnly, true, new CUE4Parse.UE4.Versions.VersionContainer(CUE4Parse.UE4.Versions.EGame.GAME_UE4_21));
            provider.Initialize();
            provider.SubmitKey(new CUE4Parse.UE4.Objects.Core.Misc.FGuid(), new CUE4Parse.Encryption.Aes.FAesKey("0x60D1D252C5996FAC112A74EC72F84A6BCD2C61F7050812F70D0928B41A3D682A"));
            var dups = provider.Files.GroupBy(x => x.Value.Path).Where(y => y.Count() > 1).Select(z => z.Key).ToList();
            var modded = provider.Files.GroupBy(x => x.Value.Path).Select(z => z.Key).ToList();
            var dupsName = provider.Files.GroupBy(x => x.Value.Name).Where(y => y.Count() > 1).Select(z => z.Key).ToList();
            var gober = provider.MountedVfs.ToList();

            foreach (var pak in gober)
            {
                if (!pak.IsEncrypted)
                {
                    var files = pak.Files.GroupBy(x => x.Value.Path).Select(z => z.Key).ToList();
                    foreach (var file in files)
                    {
                        var fileLowerd = file.ToLower();
                        var withoutExt = fileLowerd.Split('.')[0];
                        var uasset = withoutExt + ".uasset";
                        var ubulk = withoutExt + ".ubulk";
                        var uexp = withoutExt + ".uexp";
                        FArchive uassetReader = null;
                        FArchive ubulkReader = null;
                        FArchive uexpReader = null;

                        if (pak.Files.Any(x => x.Key == uasset))
                            uassetReader = new FStreamArchive(uasset, new MemoryStream(pak.Extract((VfsEntry)pak.Files.First(x => x.Key == uasset).Value)), new CUE4Parse.UE4.Versions.VersionContainer(CUE4Parse.UE4.Versions.EGame.GAME_UE4_21));
                        if (pak.Files.Any(x => x.Key == ubulk))
                            ubulkReader = new FStreamArchive(ubulk, new MemoryStream(pak.Extract((VfsEntry)pak.Files.First(x => x.Key == ubulk).Value)), new CUE4Parse.UE4.Versions.VersionContainer(CUE4Parse.UE4.Versions.EGame.GAME_UE4_21));
                        if (pak.Files.Any(x => x.Key == uexp))
                            uexpReader = new FStreamArchive(uexp, new MemoryStream(pak.Extract((VfsEntry)pak.Files.First(x => x.Key == uexp).Value)), new CUE4Parse.UE4.Versions.VersionContainer(CUE4Parse.UE4.Versions.EGame.GAME_UE4_21));

                        if (uassetReader == null || uexpReader == null)
                            continue;

                        var package = new Package(
                            uassetReader,
                            uexpReader,
                           (FArchive)null,
                            null,
                            provider);

                        var exportTypeData = package.ExportsLazy.Any(x => x.Value.ExportType.Contains("DataTable"));
                  
                        if (exportTypeData)
                        {

                   
                            return true;
                        }
                        
                    }
                 
                }
            }


            foreach ( var dup in dups)
            {
                var dupLowered = dup.ToLower();
                var pakswithDup = provider.MountedVfs.Where(x => x.Files.ContainsKey(dup.ToLower()));
                if (!pakswithDup.Any())
                    continue;
                
                foreach (var pak in pakswithDup)
                {
                    var withoutExt = dupLowered.Split('.')[0];
                    var uasset = withoutExt + ".uasset";
                    var ubulk = withoutExt + ".ubulk";
                    var uexp = withoutExt + ".uexp";
                    FArchive uassetReader = null;
                    FArchive ubulkReader = null;
                    FArchive uexpReader = null;

                    if (pak.Files.Any(x => x.Key == uasset))
                        uassetReader = new FStreamArchive(uasset, new MemoryStream(pak.Extract((VfsEntry)pak.Files.First(x => x.Key == uasset).Value)), new CUE4Parse.UE4.Versions.VersionContainer(CUE4Parse.UE4.Versions.EGame.GAME_UE4_21));
                    if (pak.Files.Any(x => x.Key == ubulk))
                        ubulkReader = new FStreamArchive(ubulk, new MemoryStream(pak.Extract((VfsEntry)pak.Files.First(x => x.Key == ubulk).Value)), new CUE4Parse.UE4.Versions.VersionContainer(CUE4Parse.UE4.Versions.EGame.GAME_UE4_21));
                    if (pak.Files.Any(x => x.Key == uexp))
                        uexpReader = new FStreamArchive(uexp, new MemoryStream(pak.Extract((VfsEntry)pak.Files.First(x => x.Key == uexp).Value)), new CUE4Parse.UE4.Versions.VersionContainer(CUE4Parse.UE4.Versions.EGame.GAME_UE4_21));

                    var package = new Package(
                        uassetReader,
                        uexpReader,
                       (FArchive)null,
                        null,
                        provider);
                    var exportType = package.ExportsLazy.Any(x => x.Value.ExportType.Contains("Blueprint") || x.Value.ExportType.Contains("DataTable"));

                    var exportTypeData = package.ExportsLazy.Any(x => x.Value.ExportType.Contains("DataTable"));
                    if ( exportType )
                        return true;

                    if (exportTypeData)
                        return true;
                }
            }
       
            return result;
        }

        static int FindPattern(byte[] data, byte[] pattern)
        {
            for (int i = 0; i < data.Length - pattern.Length + 1; i++)
            {
                bool match = true;

                // Check if the pattern matches at the current index
                for (int j = 0; j < pattern.Length; j++)
                {
                    if (data[i + j] != pattern[j])
                    {
                        match = false;
                        break;
                    }
                }

                if (match)
                {
                    return i; // Return the index where the pattern starts
                }
            }

            return -1; // Pattern not found
        }
    }
}
