﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace KartSimLauncher.Scripts.Installer
{
    public static class Installer
    {
        public static async Task Install(string ExecutableFilePath)
        {
            PowerShell powerShell = null;

            try 
            { 
                using(powerShell = PowerShell.Create())
                {
                    powerShell.AddScript("$setup=Start-Process 'C:\\Users\\{ Environment.UserName}\\AppData\\Local\\Temp\\mysetup.exe' -ArgumentList ' / Silent ' -Wait -PassThru");
                    Collection<PSObject> PSOutput = powerShell.Invoke(); foreach (PSObject outputItem in PSOutput) 
                    {
                        if(outputItem != null)
                        {
                            Console.WriteLine(outputItem.ToString());
                        }
                    }
                }            
                if(powerShell.Streams.Error.Count > 0 ) 
                {
                    string temp = powerShell.Streams.Error.First().ToString();
                    Console.WriteLine("Error: {0}", temp);
                }
                else
                {
                    Console.WriteLine("Installation has completed succesfully");
                }
            }
            catch(Exception ex) 
            {
                Console.WriteLine("Error occured: {0}", ex.InnerException);
                //throw;
            }
            finally { if(powerShell != null) { powerShell.Dispose(); } }
        } 
              
    }
}
