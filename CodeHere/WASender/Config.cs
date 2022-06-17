using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WASender
{
    public class Config
    {
        public static bool IsDemoMode = false;

        public static readonly string WaSenderFolderName = "WaSender";
        public static readonly string KeyMarkersFile = "KeyMarkers.txt";
        public static readonly string GeneralSettingsFile = "GeneralSettings.txt";
        public static readonly string ChromeProfileFolder = "ChromeProfile";
        public static readonly string ActivationFile = "Activation.txt";
        public static readonly string ProcessLoggerFolderName = "ProcessLogger";
        public static readonly string ErrorLoggerFolderName = "ErrorLogger";
        public static readonly string TempFolderName = "temp";

        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public static bool IsProductActive()
        {
            String FolderPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);

            String WaSenderFolderpath = Path.Combine(FolderPath, Config.WaSenderFolderName);
            if (!Directory.Exists(WaSenderFolderpath))
            {
                Directory.CreateDirectory(WaSenderFolderpath);
            }
            String keyMarkersTxtFilepath = Path.Combine(WaSenderFolderpath, Config.ActivationFile);
            if (File.Exists(keyMarkersTxtFilepath))
            {
                string DecodedText = File.ReadAllText(keyMarkersTxtFilepath);
                string encodedJson = Config.Base64Decode(DecodedText);

                try
                {
                    var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<WASender.Models.ActivationModel>(encodedJson);

                    if (Security.FingerPrint.Value() == Base64Decode(obj.ActivationCode))
                    {
                        if (obj.EndDate < DateTime.Now)
                        {
                            return false;
                        }
                        return true;
                    }
                }
                catch (Exception ex)
                {

                }
            }
            return false;
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }


        public static string GetTempFolderPath()
        {
            String FolderPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);

            String WaSenderFolderpath = Path.Combine(FolderPath, Config.WaSenderFolderName);
            if (!Directory.Exists(WaSenderFolderpath))
            {
                Directory.CreateDirectory(WaSenderFolderpath);
            }
            String returnableFolder = Path.Combine(WaSenderFolderpath, Config.TempFolderName);

            if (!Directory.Exists(returnableFolder))
            {
                Directory.CreateDirectory(returnableFolder);
            }

            return returnableFolder;
        }

        public static void KillChromeDriverProcess()
        {
            foreach (var process in Process.GetProcessesByName("chromedriver"))
            {
                process.Kill();
            }
        }

        public static string GetProcessLoggerFolderPath()
        {
            String FolderPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);

            String WaSenderFolderpath = Path.Combine(FolderPath, Config.WaSenderFolderName);
            if (!Directory.Exists(WaSenderFolderpath))
            {
                Directory.CreateDirectory(WaSenderFolderpath);
            }
            String returnableFolder = Path.Combine(WaSenderFolderpath, Config.ProcessLoggerFolderName);

            if (!Directory.Exists(returnableFolder))
            {
                Directory.CreateDirectory(returnableFolder);
            }

            return returnableFolder;
        }


        public static void ActivateProduct(string ActivationKey)
        {
            String FolderPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);

            String WaSenderFolderpath = Path.Combine(FolderPath, Config.WaSenderFolderName);
            if (!Directory.Exists(WaSenderFolderpath))
            {
                Directory.CreateDirectory(WaSenderFolderpath);
            }
            String keyMarkersTxtFilepath = Path.Combine(WaSenderFolderpath, Config.ActivationFile);
            if (!File.Exists(keyMarkersTxtFilepath))
            {
                File.WriteAllText(keyMarkersTxtFilepath, ActivationKey);
            }
            else
            {
                File.Delete(keyMarkersTxtFilepath);
                File.WriteAllText(keyMarkersTxtFilepath, ActivationKey);
            }
        }

        public static string GetKeyMarkersFilePath()
        {
            String FolderPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);

            String WaSenderFolderpath = Path.Combine(FolderPath, Config.WaSenderFolderName);
            if (!Directory.Exists(WaSenderFolderpath))
            {
                Directory.CreateDirectory(WaSenderFolderpath);
            }
            String keyMarkersTxtFilepath = Path.Combine(WaSenderFolderpath, Config.KeyMarkersFile);
            return keyMarkersTxtFilepath;
        }

        public static string GetGeneralSettingsFilePath()
        {
            String FolderPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);

            String WaSenderFolderpath = Path.Combine(FolderPath, Config.WaSenderFolderName);
            if (!Directory.Exists(WaSenderFolderpath))
            {
                Directory.CreateDirectory(WaSenderFolderpath);
            }
            String keyMarkersTxtFilepath = Path.Combine(WaSenderFolderpath, Config.GeneralSettingsFile);
            return keyMarkersTxtFilepath;
        }

        public static string GetChromeProfileFolder()
        {
            String FolderPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);

            String WaSenderFolderpath = Path.Combine(FolderPath, Config.WaSenderFolderName);
            if (!Directory.Exists(WaSenderFolderpath))
            {
                Directory.CreateDirectory(WaSenderFolderpath);
            }
            String keyMarkersTxtFilepath = Path.Combine(WaSenderFolderpath, Config.ChromeProfileFolder);
            if (!Directory.Exists(keyMarkersTxtFilepath))
            {
                Directory.CreateDirectory(keyMarkersTxtFilepath);
            }
            return keyMarkersTxtFilepath;
        }

        public static ChromeOptions GetChromeOptions()
        {
            
            ChromeOptions options = new ChromeOptions();
            options.AddExcludedArgument("enable-automation");
            options.AddAdditionalCapability("useAutomationExtension", false);
            options.AddArgument("user-data-dir=" + Config.GetChromeProfileFolder());
            return options;
        }



    }
}
