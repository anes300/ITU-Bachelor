﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeEngine.Services
{
    public class SensorManager
    {
        

        public string GetSensorData(string variable)
        {

            switch (variable)
            {
                case "temp":
                    return ExecuteCommand("cat /sys/class/thermal/thermal_zone0/temp");
                default:
                    break;
            }
            string sensordata = ExecuteCommand("cat /sys/class/thermal/thermal_zone0/temp");

            return sensordata;
        }

        private string ExecuteCommand(string command)
        {         
            string result = "";
            using (System.Diagnostics.Process proc = new System.Diagnostics.Process())
            {
                proc.StartInfo.FileName = "/bin/bash";
                proc.StartInfo.Arguments = "-c \" " + command + " \"";
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.StartInfo.RedirectStandardError = true;
                proc.Start();

                result += proc.StandardOutput.ReadToEnd();
                result += proc.StandardError.ReadToEnd();

                proc.WaitForExit();
            }
            return result;
        }
    }



}
