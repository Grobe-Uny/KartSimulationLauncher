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
        public static string urlForInstallation = "https://drive.google.com/u/2/uc?id=1vdwI2d90xg9BXe7Grpsb6L7dY2a1NuPI&export=download&confirm=t&uuid=7f7463db-4d4b-465c-a137-797cd25532a8&at=ALgDtsxrQozXao9DI0GqHtHJ_ec4:1674681281487";
        static HttpClient Client = new HttpClient();
        public static OpenFileDialog folderBrowser = new OpenFileDialog();

        static bool isAdmin;

        public static string UserPath;
        public static string DownloadPath = $"C:\\Users\\{Environment.UserName}\\AppData\\Local\\Temp\\mysetup.exe";

        public static async Task CheckForFiles()
        {
            WindowsIdentity user = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(user);
            isAdmin = principal.IsInRole(WindowsBuiltInRole.Administrator);
    
            await nezDownloadnesto();
            var bytes = await nezDownloadnesto();
            File.WriteAllBytes(DownloadPath, bytes);
        }
        public static async Task<byte[]> nezDownloadnesto()
        {
            var response = await Client.GetAsync(urlForInstallation);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsByteArrayAsync();
            return content;
        }
    }
}
