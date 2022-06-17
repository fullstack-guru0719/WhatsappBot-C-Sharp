using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WASender
{
    public class Logger
    {
        public string ProcessName { get; set; }

        private List<string> loggHolder { get; set; }

        public Logger(string _processName)
        {
            ProcessName = _processName;
            loggHolder = new List<string>();
        }

        public void WriteLog(string msg)
        {
            loggHolder.Add(msg);
        }

        public void Complete()
        {
            try
            {
                string jsonString = JsonConvert.SerializeObject(loggHolder);
                string fileName = ProcessName + "_" + Guid.NewGuid() + ".json";
                string filePath = Config.GetProcessLoggerFolderPath() + "//" + fileName;
                File.WriteAllText(filePath, jsonString);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
