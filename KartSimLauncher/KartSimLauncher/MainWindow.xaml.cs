﻿using KartSimLauncher.Scripts.Networking;
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
        
        string file = @"C:\Program Files\Kart Simulator\Kart Simulator.exe";

        public static bool isAppRunning = false;
        public static Process process;

        public int DelayforWindowStateAnim = 100;
        public int DelayforUpdate = 100;

        public MainWindow()
        {
            InitializeComponent();
            Thread th = new Thread(() =>
            {
                while (true)
                {
                    Update();
                    Task.Delay(DelayforUpdate).Wait();
                }
            });

            th.Start();

        }

        private void Exit_Button_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);   
        }
        public void Update()
        {
            if(process == null)
            {
                return;
            }
            if (process.HasExited == true)
            {
                Dispatcher.Invoke(() => { 
                    WindowState = WindowState.Normal;
                    ShowInTaskbar = true;               
                });
            }
        }

        private async void Start_Button_Click(object sender, RoutedEventArgs e)
        {
            var fi = new FileInfo(file);
            if (fi.Exists)
            {
                process = Process.Start(file);
                process.EnableRaisingEvents = true;
                WindowState = WindowState.Minimized;
                Task.Delay(DelayforWindowStateAnim).Wait();
                ShowInTaskbar = false;
            }
            else
            {
                await Updater.CheckForFiles();
                Displaying_Debug_Data.Text = Updater.url;
                process = Process.Start(Updater.NewSimulationEXE);
            }
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
