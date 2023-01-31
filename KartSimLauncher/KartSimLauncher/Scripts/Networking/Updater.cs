using System.Net.Http;
using System.IO;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.Win32;
using System;

namespace KartSimLauncher.Scripts.Networking
{
    public static class Updater
    {
        /*public static string KartExeUrl = "https://774f-86-33-85-245.eu.ngrok.io/file?FileName=Kart+Simulator.exe";
        public static string CrashHandlerExeUrl = "https://774f-86-33-85-245.eu.ngrok.io/file?FileName=Kart+Simulator.exe";
        public static string PlayerDllUrl = "https://774f-86-33-85-245.eu.ngrok.io/file?FileName=Kart+Simulator.exe";
        public static string VersionTXTUrl = "https://774f-86-33-85-245.eu.ngrok.io/file?FileName=Kart+Simulator.exe";
        public static string WinPixRuntimeDllUrl = "https://774f-86-33-85-245.eu.ngrok.io/file?FileName=Kart+Simulator.exe";*/
        public static string SetupUrl = "https://799b-86-33-92-119.eu.ngrok.io/file?FileName=mysetup.exe";

        public static HttpClient Client = new HttpClient();
        public static OpenFileDialog folderBrowser = new OpenFileDialog();

        static bool isAdmin;

        public static string UserPath;
        public static string DownloadPath = @"C:\TempDownload\mysetup.exe";
        public static string DownloadFolder = @"C:\TempDownload";
        public static void CreateTempDir()
        {   
            //Directory.CreateDirectory(@"C:\TempDownload").Create(); lose 
            Directory.CreateDirectory(DownloadFolder);
        }
        

        public static async Task CheckForFiles()
        {
            WindowsIdentity user = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(user);
            isAdmin = principal.IsInRole(WindowsBuiltInRole.Administrator);
            
            CreateTempDir();
            //File.Create(DownloadPath).Close(); nepotrebno
            var bytes = await nezDownloadnesto();
            await File.WriteAllBytesAsync(DownloadPath, bytes); //ovo ce samo napravit file

        }
        public static async Task<byte[]> nezDownloadnesto()
        {
            var content = await Client.GetByteArrayAsync(SetupUrl);
            return content;
        }
    }
}
