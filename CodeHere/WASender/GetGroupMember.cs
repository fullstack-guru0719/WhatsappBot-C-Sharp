using MaterialSkin;
using MaterialSkin.Controls;
using OfficeOpenXml;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WASender.enums;

namespace WASender
{
    public partial class GetGroupMember : MaterialForm
    {
        MaterialSkin.MaterialSkinManager materialSkinManager;
        InitStatusEnum initStatusEnum;
        System.Windows.Forms.Timer timerInitChecker;
        IWebDriver driver;
        BackgroundWorker worker;
        CampaignStatusEnum campaignStatusEnum;
        WaSenderForm waSenderForm;
        Logger logger;

        public GetGroupMember(WaSenderForm _waSenderForm)
        {
            InitializeComponent();

            waSenderForm = _waSenderForm;
            materialSkinManager = MaterialSkin.MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkin.MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Green700, Primary.Green400, Primary.Blue900, Accent.Green700, TextShade.WHITE);

            logger = new Logger("GteGroupMembers");
        }

        private void ChangeInitStatus(InitStatusEnum _initStatus)
        {
            this.initStatusEnum = _initStatus;
            AutomationCommon.ChangeInitStatus(_initStatus, lblInitStatus);
        }

        private void init()
        {
            ChangeInitStatus(InitStatusEnum.NotInitialised);
        }



        private void checkQRScanDone()
        {
            timerInitChecker = new System.Windows.Forms.Timer();
            timerInitChecker.Interval = 1000;
            timerInitChecker.Tick += timerInitChecker_Tick;
            timerInitChecker.Start();
        }
        public void timerInitChecker_Tick(object sender, EventArgs e)
        {
            try
            {
                bool isElementDisplayed = AutomationCommon.IsElementPresent(By.ClassName("_1XkO3"), driver);
                if (isElementDisplayed == true)
                {
                    logger.WriteLog("_1XkO3 ElementDisplayed");
                    ChangeInitStatus(InitStatusEnum.Initialised);
                    timerInitChecker.Stop();
                    Activate();
                }
            }
            catch (Exception ex)
            {
                ChangeInitStatus(InitStatusEnum.Unable);
                logger.WriteLog(ex.Message);
                logger.WriteLog(ex.StackTrace);
                timerInitChecker.Stop();
            }
        }


        List<string> chatNames;

        private void btnInitWA_Click(object sender, EventArgs e)
        {
            ChangeInitStatus(InitStatusEnum.Initialising);
            logger.WriteLog("ChangeInitStatus");
            try
            {
                Config.KillChromeDriverProcess();
                //ChromeOptions options = new ChromeOptions();
                //options.AddExcludedArgument("enable-automation");
                //options.AddAdditionalCapability("useAutomationExtension", false);
                var chromeDriverService = ChromeDriverService.CreateDefaultService();
                chromeDriverService.HideCommandPromptWindow = true;


                driver = new ChromeDriver(chromeDriverService, Config.GetChromeOptions());
                //driver = new ChromeDriver();
                driver.Url = "https://web.whatsapp.com";

                checkQRScanDone();
            }
            catch (Exception ex)
            {
                logger.WriteLog(ex.Message);
                logger.WriteLog(ex.StackTrace);
                ChangeInitStatus(InitStatusEnum.Unable);
                string ss = "";
                if (ex.Message.Contains("session not created"))
                {
                    DialogResult dr = MessageBox.Show("Your Chrome Driver and Google Chrome Version Is not same, Click 'Yes botton' to view detail info ", "Error ", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Error);
                    if (dr == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start("https://medium.com/fusionqa/selenium-webdriver-error-sessionnotcreatederror-session-not-created-this-version-of-7b3a8acd7072");
                    }
                }
                else if (ex.Message.Contains("invalid argument: user data directory is already in use"))
                {
                    Config.KillChromeDriverProcess();
                    MaterialSnackBar SnackBarMessage = new MaterialSnackBar("Please Close All Previous Sessions and Browsers if open, Then try again", Strings.OK, true);
                    SnackBarMessage.Show(this);
                }
            }
        }

        private void GetGroupMember_Load(object sender, EventArgs e)
        {
            init();
            InitLanguage();
        }

        private void InitLanguage()
        {
            this.Text = Strings.GetGroupMember;
            materialLabel2.Text = Strings.InitiateWhatsAppScaneQRCodefromyourmobile;
            materialLabel1.Text = Strings.OpenanyGroupchatClickbuttonbellow;
            btnInitWA.Text = Strings.ClicktoInitiate;
            materialButton1.Text = Strings.StartGrabbing;
            label5.Text = Strings.Status;
        }

        private List<string> TryThirdMethod()
        {
            logger.WriteLog("Third Attempt Started ");
            List<string> _chatNames = new List<string>();

            try
            {
                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                string text =(string) js.ExecuteScript("return document.getElementsByClassName('_2YPr_')[0].getAttribute('title')");
                _chatNames = AutomationCommon.GetNumbers(text);
                logger.WriteLog("Got Text=" + text);
            }
            catch (Exception ex)
            {
                logger.WriteLog("Third Attempt Error-");
                logger.WriteLog(ex.Message);
                logger.WriteLog(ex.StackTrace);
            }
            logger.WriteLog("Third Attempt End ");
            return _chatNames;
        }

        private List<string> TrySecondMetod()
        {
            List<string> _chatNames = new List<string>();
            logger.WriteLog("Second Attempt Started ");
            try
            {
                By span = By.XPath("//*[@id='main']/header/div[2]/div[2]/span");
                if (AutomationCommon.IsElementPresent(span, driver))
                {
                    logger.WriteLog("span found");
                    string text = driver.FindElement(span).Text;
                    logger.WriteLog("Got Text=" + text);
                    _chatNames = AutomationCommon.GetNumbers(text);
                }
                else {
                    logger.WriteLog("span not found");
                }
            }
            catch (Exception ex)
            {
                logger.WriteLog("Second Attempt Error-");
                logger.WriteLog(ex.Message);
                logger.WriteLog(ex.StackTrace);
            }
            logger.WriteLog("Second Attempt End");
            return _chatNames;
        }

        private void materialButton1_Click(object sender, EventArgs e)
        {
            if (initStatusEnum != InitStatusEnum.Initialised)
            {
                logger.WriteLog("!InitStatusEnum.Initialised");
                Utils.showAlert(Strings.PleasefollowStepNo1FirstInitialiseWhatsapp, Alerts.Alert.enmType.Error);
                return;
            }
            if (campaignStatusEnum != CampaignStatusEnum.Running)
            {

                By numbersBy = By.ClassName("_2YPr_");

                chatNames = new List<string>();

                if (AutomationCommon.IsElementPresent(numbersBy, driver))
                {
                    logger.WriteLog("_2YPr_ is present");
                    IWebElement numbersDiv = driver.FindElement(numbersBy);
                    string list = numbersDiv.GetAttribute("title");

                    var splitter = list.Split(',');
                    logger.WriteLog("splitter.Count = " + splitter.Count());
                    if (splitter.Count() == 1)
                    {
                        logger.WriteLog("splitter.Count = 1");
                        Thread.Sleep(2000);
                        list = numbersDiv.GetAttribute("title");
                        splitter = list.Split(',');
                        logger.WriteLog("splitter.Count = " + splitter.Count());
                        if (splitter.Count() == 1)
                        {
                            Thread.Sleep(2000);
                            list = numbersDiv.GetAttribute("title");
                            splitter = list.Split(',');
                        }
                    }



                    int globalCounter = 0;
                    foreach (var item in splitter)
                    {
                        string newItem = item.Replace("+", "").Replace(" ", "");

                        try
                        {
                            if (Config.IsDemoMode == true)
                            {
                                if (globalCounter > 5)
                                {
                                    Utils.showAlert("You can Extract only 5 Group Members in trial version", Alerts.Alert.enmType.Error);
                                    break;
                                }
                            }
                            Int64 number = Convert.ToInt64(newItem);
                            chatNames.Add(newItem);
                            globalCounter++;
                        }
                        catch (Exception ex)
                        {

                        }

                    }


                    if (chatNames.Count() == 0)
                    {
                        logger.WriteLog("First Attempt - chatNames.Count = 0");
                        try
                        {
                            chatNames = TrySecondMetod();
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                    if (chatNames.Count() == 0)
                    {
                        logger.WriteLog("Second Attempt - chatNames.Count = 0");
                        chatNames = TryThirdMethod();
                    }
                    if (chatNames.Count() == 0)
                    {
                        logger.WriteLog("Third Attempt - chatNames.Count = 0");
                    }

                    string GroupName = "Group_";

                    By GroupNameBy = By.XPath("/html/body/div[1]/div[1]/div[1]/div[4]/div[1]/header/div[2]/div[1]/div/span");


                    if (AutomationCommon.IsElementPresent(GroupNameBy, driver))
                    {
                        GroupName = AutomationCommon.RemoveSpecialCharacters(driver.FindElement(GroupNameBy).Text);
                    }

                    String FolderPath = Config.GetTempFolderPath();
                    String file = Path.Combine(FolderPath, "" + GroupName + "_Members_" + Guid.NewGuid().ToString() + ".xlsx");
                    string NewFileName = file.ToString();

                    File.Copy("MemberListTemplate.xlsx", NewFileName);


                    var newFile = new FileInfo(NewFileName);
                    ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                    using (ExcelPackage xlPackage = new ExcelPackage(newFile))
                    {
                        var ws = xlPackage.Workbook.Worksheets[0];

                        for (int i = 0; i < chatNames.Count(); i++)
                        {
                            ws.Cells[i + 1, 1].Value = chatNames[i];
                        }
                        xlPackage.Save();
                    }


                    savesampleExceldialog.FileName = GroupName + "_Members.xlsx";
                    if (savesampleExceldialog.ShowDialog() == DialogResult.OK)
                    {
                        File.Copy(NewFileName, savesampleExceldialog.FileName.EndsWith(".xlsx") ? savesampleExceldialog.FileName : savesampleExceldialog.FileName + ".xlsx", true);
                        Utils.showAlert(Strings.Filedownloadedsuccessfully, Alerts.Alert.enmType.Success);
                    }
                }
                else
                {
                    Utils.showAlert(Strings.PleaseGotoanygroupchat, Alerts.Alert.enmType.Warning);
                }

            }
            else
            {
                Utils.showAlert(Strings.Processisalreadyrunning, Alerts.Alert.enmType.Info);
            }
        }

        private void GetGroupMember_FormClosed(object sender, FormClosedEventArgs e)
        {
            waSenderForm.formReturn(true);
            logger.Complete();
            try
            {
                driver.Quit();
            }
            catch (Exception ex)
            {

            }
            foreach (var process in Process.GetProcessesByName("chromedriver"))
            {
                process.Kill();
            }
        }
    }
}
