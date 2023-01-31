using KartSimLauncher.Scripts.Files.File_Manager;
using KartSimLauncher.Scripts.Installer;
using KartSimLauncher.Scripts.Networking;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KartSimLauncher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        string File = @"C:\Program Files\Kart Simulator\Kart Simulator.exe";
        string TempFileInstallation = $"C:\\Users\\{Environment.UserName}\\Desktop\\Zavrsni rad\\Install\\Output\\mysetup.exe";

        public static bool IsAppRunning = false;
        public static Process Process;

        public int DelayForWindowStateAnim = 100;
        public int DelayForUpdate = 100;

        public MainWindow()
        {
            InitializeComponent();
            Start();
            /*Thread th = new Thread(() =>
            {
                while (true)
                {
                    Update();
                    Task.Delay(DelayforUpdate).Wait();
                }
            });*/ // reka sn ti da je thread "lose"
            
            Task.Run(() => 
            {
                while(true)
                {
                    Update();
                    await Task.Delay(DelayForUpdate);
                }
            });
        }
        public void Start()
        {
            Updater.Client.Timeout = TimeSpan.FromHours(2);
            FileChecker.CheckForDownloadedFiles();
        }
        private void Exit_Button_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);   
        }
        public void Update()
        {
            if(Process == null)
            {
                return;
            }
            if (Process.HasExited == true)
            {
                Dispatcher.Invoke(() => { 
                    WindowState = WindowState.Normal;
                    ShowInTaskbar = true;               
                });
            }
        }

        private async void Start_Button_Click(object sender, RoutedEventArgs e)
        {
            var fi = new FileInfo(File);//kartsim.exe, fc=setup.exe
            if (!fi.Exists && !FileChecker.IsDownloaded)
            {
                Displaying_Debug_Data.Text = "Downloading from: " + Updater.SetupUrl;
                await Updater.CheckForFiles();
                Displaying_Debug_Data.Text = "Instaling file: " + Updater.DownloadPath;
                Installer.InstallExe(TempFileInstallation);
                Displaying_Debug_Data.IsEnabled = false;
            }
            else if (!fi.Exists && FileChecker.isDownloaded)
            {
                Displaying_Debug_Data.Text = "Installing file: " + Updater.DownloadPath;
                Installer.InstallExe(TempFileInstallation);
                Displaying_Debug_Data.IsEnabled = false;
            }
            /*else
            {
                process = Process.Start(file);
                process.EnableRaisingEvents = true;
                WindowState = WindowState.Minimized;
                Task.Delay(DelayforWindowStateAnim).Wait();
                ShowInTaskbar = false;
            }*/
            //ovo tria bit ode jer vako nako su ti skinuti svi files i installed nakn onog gori       
            process = Process.Start(File);
            process.EnableRaisingEvents = true;
            WindowState = WindowState.Minimized;
            await Task.Delay(DelayForWindowStateAnim); //awaitas jer imas gori async
            ShowInTaskbar = false;
        }

        private void Options_Button_Click(object sender, RoutedEventArgs e)
        {
            bool enabled = Options_Background.IsEnabled;
            if(!enabled) 
            {
                Options_Background.IsEnabled = true;
                Options_Background.Opacity = 1;            
            }else if (enabled)
            {
                Options_Background.IsEnabled = false;   
                Options_Background.Opacity = 0;
            }
        }
    }
   
}
