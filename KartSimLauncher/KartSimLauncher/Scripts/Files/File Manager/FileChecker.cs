using KartSimLauncher.Scripts.Networking;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KartSimLauncher.Scripts.Files.File_Manager
{
    internal class FileChecker
    {
        public static bool IsDownloaded = false;

        

        public static void CheckForDownloadedFiles()
        {
            System.IO.FileInfo fi = new FileInfo(@"C:\TempDownload\mysetup.exe");
            //long size = fi.Length;

            //if (Directory.Exists(Updater.DownloadFolder) && File.Exists(Updater.DownloadPath))
            if (Directory.Exists(Updater.DownloadFolder) && fi.Exists)
            {
                IsDownloaded = true;
            }
            else
            {
                IsDownloaded = false;
            }

            if (fi.Exists)
            {
                IsDownloaded = false;
            }
        }


    }
}
