using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WASender.enums;
using WASender.Models;

namespace WASender
{
    public class AutomationCommon
    {

       
        public static IWebElement WaitUntilElementVisible(IWebDriver driver, By by, int attempts = 10)
        {
            IWebElement element = null;
            int attempt = 0;
            while (element == null)
            {
                if (attempt >=  attempts)
                {
                    break;
                }
                element = check(driver, by);
                attempt++;
            }
            return element;
        }

        public static IWebElement WaitUntilElementDispose(IWebDriver driver, By by, int attempts = 10,bool doClick=false,string matchText="")
        {
            IWebElement element = check(driver, by);
            int attempt = 0;
            while (element != null)
            {
                if (attempt >= attempts)
                {
                    break;
                }
                element = check(driver, by, doClick,matchText);
                attempt++;
            }
            return element;
        }

        private static IWebElement check(IWebDriver driver, By by, bool doClick = false, string matchText = "")
        {
            Thread.Sleep(1000);
            if (IsElementPresent(by, driver))
            {
                IWebElement el = driver.FindElement(by);
                if (doClick == true )
                {
                    if (matchText != "")
                    {
                        if (matchText == el.Text)
                        {
                            el.Click();
                        }
                    }
                    else
                    {
                        el.Click();
                    }
                    
                }
                return el;
            }
            return null;
        }



        public static void GenerateReport(WASenderGroupTransModel wASenderSingleTransModel)
        {
            string strim = "<link rel=\"stylesheet\" href=\"https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css\" >";
            strim += "<body><div class=\"container\"><center><h1>";
            strim += wASenderSingleTransModel.CampaignName;
            strim += " </h1></center>";

            int MesageCounter = 0;
            if (wASenderSingleTransModel.messages != null)
            {
                foreach (var message in wASenderSingleTransModel.messages)
                {
                    if (message != null)
                    {
                        MesageCounter++;
                        strim += " <div class='panel panel-default'>";
                        strim += "  <div class='panel-heading'>";
                        strim += "      Message " + MesageCounter;
                        strim += "  </div>";
                        strim += "  <div class='panel-body'>";
                        strim += message.longMessage;
                        strim += "  </div>";
                        strim += "";
                        strim += "";
                        strim += "</div>";
                        strim += "<br/>";
                    }
                }
            }

            int SuccessCount = wASenderSingleTransModel.groupList.Where(x => x.sendStatusModel.sendStatusEnum == SendStatusEnum.Success).Count();
            int FailedCount = wASenderSingleTransModel.groupList.Where(x => x.sendStatusModel.sendStatusEnum != SendStatusEnum.Success).Count();

            strim += " <div class='panel panel-default'>";
            strim += "  <div class='panel-heading'>";
            strim += "      Summery ";
            strim += "  </div>";
            strim += "  <div class='panel-body'>";
            strim += " Total Success = " + SuccessCount;
            strim += " Total Fail = " + FailedCount;
            strim += "  </div>";
            strim += "";
            strim += "";
            strim += "</div>";
            strim += "<br/>";


            strim += "<table class=\"table table-bordered\"><thead><tr><th>Chat Name</th><th>Result</th></tr></thead><tbody>";


            foreach (GroupModel contact in wASenderSingleTransModel.groupList)
            {
                strim += "<tr>";
                strim += "<td>";
                strim += contact.Name;
                strim += "</td>";
                strim += "<td>";
                if (contact.sendStatusModel.sendStatusEnum == SendStatusEnum.Success)
                {
                    strim += "Success";
                }
                else
                {
                    strim += "Fail - " + contact.sendStatusModel.sendStatusEnum.ToString();
                }
                strim += "</td>";
                strim += "</tr>";
            }

            strim += "</tbody></table></div></body>";


            String FolderPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            String path = Path.Combine(FolderPath, wASenderSingleTransModel.CampaignName + "_Report_" + Guid.NewGuid().ToString() + ".html");
            
            File.Create(path).Close();

            File.AppendAllLines(path, new[] { strim });

            SaveFileDialog savesampleExceldialog = new SaveFileDialog();
            savesampleExceldialog.FileName = wASenderSingleTransModel.CampaignName + "_Group_Report.html";
            if (savesampleExceldialog.ShowDialog() == DialogResult.OK)
            {
                File.Copy(path, savesampleExceldialog.FileName, true);
                Utils.showAlert(Strings.Filedownloadedsuccessfully, Alerts.Alert.enmType.Success);
            }
        }

        public static void GenerateReport(WASenderSingleTransModel wASenderSingleTransModel)
        {
            string strim = "<link rel=\"stylesheet\" href=\"https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css\" >";
            strim  +="<body><div class=\"container\"><center><h1>";
            strim += wASenderSingleTransModel.CampaignName;
            strim += " </h1></center>";

            int MesageCounter = 0;
            foreach (var message in wASenderSingleTransModel.messages)
            {
                if (message != null)
                {
                    MesageCounter++;
                    strim += " <div class='panel panel-default'>";
                    strim += "  <div class='panel-heading'>";
                    strim += "      Message " + MesageCounter;
                    strim += "  </div>";
                    strim += "  <div class='panel-body'>";
                    strim += message.longMessage;
                    strim += "  </div>";
                    strim += "";
                    strim += "";
                    strim += "</div>";
                    strim += "<br/>";
                }
            }

            int SuccessCount = wASenderSingleTransModel.contactList.Where(x => x.sendStatusModel.sendStatusEnum == SendStatusEnum.Success).Count();
            int FailedCount = wASenderSingleTransModel.contactList.Where(x => x.sendStatusModel.sendStatusEnum != SendStatusEnum.Success).Count();

            strim += " <div class='panel panel-default'>";
            strim += "  <div class='panel-heading'>";
            strim += "      Summery " ;
            strim += "  </div>";
            strim += "  <div class='panel-body'>";
            strim += " Total Success = " + SuccessCount;
            strim += " Total Fail = " + FailedCount;
            strim += "  </div>";
            strim += "";
            strim += "";
            strim += "</div>";
            strim += "<br/>";

           
            strim += "<table class=\"table table-bordered\"><thead><tr><th>Chat Name</th><th>Result</th></tr></thead><tbody>";

            
            foreach (ContactModel contact in wASenderSingleTransModel.contactList)
            {
                strim += "<tr>";
                strim += "<td>";
                strim += contact.number;
                strim += "</td>";
                strim += "<td>";
                if (contact.sendStatusModel.sendStatusEnum == SendStatusEnum.Success)
                {
                    strim += "Succes";
                }
                else
                {
                    strim += "Fail - " + contact.sendStatusModel.sendStatusEnum.ToString() ;
                }
                strim += "</td>";
                strim += "</tr>";
            }
            
            strim += "</tbody></table></div></body>";

            String FolderPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            String path = Path.Combine(FolderPath, wASenderSingleTransModel.CampaignName + "_Report_" + Guid.NewGuid().ToString() + ".html");
            
             File.Create(path).Close();
            
            File.AppendAllLines(path, new[] { strim });

            SaveFileDialog savesampleExceldialog = new SaveFileDialog();
            savesampleExceldialog.FileName = wASenderSingleTransModel.CampaignName + "_Report.html";
            if (savesampleExceldialog.ShowDialog() == DialogResult.OK)
            {
                File.Copy(path, savesampleExceldialog.FileName, true);
                Utils.showAlert(Strings.Filedownloadedsuccessfully, Alerts.Alert.enmType.Success);
            }
        }


        public static void isLoadingWeb(IWebDriver driver)
        {
            //_1INL_ _1iyey A_WMk _1UG2S
            By loadingBy = By.XPath("//div[contains(@class, '_1INL_') and contains(@class, '_1iyey') and contains(@class, '_1UG2S') ]");
            bool IsLoading = AutomationCommon.IsElementPresent(loadingBy, driver);
            if (IsLoading == true)
            {
                AutomationCommon.WaitUntilElementDispose(driver, loadingBy, 500);
            }

        }

        public static string RemoveSpecialCharacters(string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '.' || c == '_')
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }
        

        public static void checkConnection(IWebDriver driver)
        {

            By alertPhone = By.CssSelector("[data-testid='alert-phone']");
            if (IsElementPresent(alertPhone, driver))
            {
                AutomationCommon.WaitUntilElementDispose(driver, alertPhone, 5000, true);
            }

            By alertComputer = By.CssSelector("[data-testid='alert-computer']");
            if (IsElementPresent(alertComputer, driver))
            {
                AutomationCommon.WaitUntilElementDispose(driver, alertComputer, 5000, true);
            }

           

            string ss = "";

            By retryingBy = By.XPath("//div[contains(@class, 'tvf2evcx') and contains(@class, 'm0h2a7mj') and contains(@class, 'lb5m6g5c') and contains(@class, 'j7l1k36l') and contains(@class, 'ktfrpxia') and contains(@class, 'nu7pwgvd') and contains(@class, 'gjuq5ydh') ]");
            bool IsRetrying = AutomationCommon.IsElementPresent(retryingBy, driver);

            if (IsRetrying == true)
            {
                var retryEltxt = driver.FindElements(retryingBy);
                foreach (var retryEl in retryEltxt)
                {
                    if (retryEl.Text == "RETRY NOW")
                    {
                        AutomationCommon.WaitUntilElementDispose(driver, retryingBy, 500, true, "Retry Now");
                    }
                }
            }
        }


        public static bool IsElementPresent(By by, IWebElement element)
        {
            try
            {
                element.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }


        public static bool IsElementPresent(By by, IWebDriver driver)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public static void ChangeCampStatus(CampaignStatusEnum _campaignStatus,Label lblRunStatus)
        {
            if (_campaignStatus == CampaignStatusEnum.NotStarted)
            {
                lblRunStatus.ForeColor = Color.Orange;
                lblRunStatus.Text = "Not Started";
            }
            else if (_campaignStatus == CampaignStatusEnum.Running)
            {
                lblRunStatus.ForeColor = Color.Green;
                lblRunStatus.Text = "Running";
            }
            else if (_campaignStatus == CampaignStatusEnum.Paused)
            {
                lblRunStatus.ForeColor = Color.Blue;
                lblRunStatus.Text = "Paused";
            }
            else if (_campaignStatus == CampaignStatusEnum.Stopped)
            {
                lblRunStatus.ForeColor = Color.Red;
                lblRunStatus.Text = "Stopped";
            }
            else if (_campaignStatus == CampaignStatusEnum.Error)
            {
                lblRunStatus.ForeColor = Color.DarkRed;
                lblRunStatus.Text = "Error";
            }
            else if (_campaignStatus == CampaignStatusEnum.Finish)
            {
                lblRunStatus.ForeColor = Color.DarkGray;
                lblRunStatus.Text = "Completed";
            }
        }

        public static void ChangeInitStatus(InitStatusEnum _initStatus, Label lblInitStatus)
        {
            if (_initStatus == InitStatusEnum.NotInitialised)
            {
                lblInitStatus.ForeColor = Color.Orange;
                lblInitStatus.Text = "Not Initialised";
            }
            else if (_initStatus == InitStatusEnum.Initialising)
            {
                lblInitStatus.ForeColor = Color.Gray;
                lblInitStatus.Text = "Initialising";
            }
            else if (_initStatus == InitStatusEnum.Initialised)
            {
                lblInitStatus.ForeColor = Color.Green;
                lblInitStatus.Text = "Initialised";
            }
            else if (_initStatus == InitStatusEnum.Unable)
            {
                lblInitStatus.ForeColor = Color.DarkRed;
                lblInitStatus.Text = "Error";
            }

            
        }


        public static List<string> GetNumbers(string List)
        {
            List = List.Replace(" ", "");
            List = List.Replace("+", "");
            List = List.Replace("-", "");
            List = List.Replace("/", "");
            List = List.Replace("\\", "");
            List = List.Replace("(", "");
            List = List.Replace(")", "");
            List = List.Replace("，", ",");
            List<string> list = new List<string>();
            string[] array = List.Split(',');
            foreach (string text in array)
            {
               // if (text.StartsWith("+"))
               // {
                    list.Add(text);
                //}
            }
            return list;
        }

        public bool IsElementVisible(IWebElement element)
        {
            return element.Displayed && element.Enabled;
        }
    }

}
