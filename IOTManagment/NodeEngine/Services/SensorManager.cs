using Model.Nodes.Enum;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeEngine.Services
{
    public class SensorManager
    {
        
        // TODO: Add commands for more datatypes
        public string GetSensorData(DataType type)
        {

            switch (type)
            {
                case DataType.Temperature:
                    return ExecuteCommand("cat /sys/class/thermal/thermal_zone0/temp");
                default:
                    return null;
                    break;
            }
    
        }

        // IMPORTANT: Commands will only work when running on linux system
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
