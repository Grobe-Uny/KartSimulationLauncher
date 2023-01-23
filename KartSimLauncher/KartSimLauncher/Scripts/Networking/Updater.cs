using System;
using System.Net;
using System.Net.Http;
using System.IO;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Markup;
using System.Windows.Shapes;

namespace KartSimLauncher.Scripts.Networking
{
    public static class Updater
    {
        public static string url = "https://drive.google.com/drive/folders/1j5oglBQH6nHkiDx6aEwhgKj1Xnc50IrG?usp=sharing";
        static HttpClient Client = new HttpClient();

        static bool isAdmin;

        static string SimulationFolder = @"C:\Program Files\Kart Simulator";
        public static string NewSimulationFolder = $"C:\\Users\\{Environment.UserName}\\Documents\\Kart Simulator";
        static string SimulationEXE = @"C:\Program Files\Kart Simulator\Kart Simulator.exe"; 
        public static string NewSimulationEXE = $"C:\\Users\\{Environment.UserName}\\Documents\\Kart Simulator\\Kart Simulator.exe";

        public static async Task CheckForFiles()
        {
            WindowsIdentity user = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(user);
            isAdmin = principal.IsInRole(WindowsBuiltInRole.Administrator);
            //Client.BaseAddress = new Uri(url);
      
            if (isAdmin)
            {
                Directory.CreateDirectory(SimulationFolder);                
            }
            else
            {
                Directory.CreateDirectory(NewSimulationFolder);
            }


            await nezDownloadnesto();
            var bytes = await nezDownloadnesto();
            File.WriteAllBytes(SimulationEXE, bytes);
            File.WriteAllBytes(NewSimulationEXE, bytes);
        }
        public static async Task<byte[]> nezDownloadnesto()
        {
            var response = await Client.GetAsync(url);
            var content = await response.Content.ReadAsByteArrayAsync();
            return content;
        }
    }
}
