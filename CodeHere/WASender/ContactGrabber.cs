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
    public partial class ContactGrabber : MaterialForm
    {
        MaterialSkin.MaterialSkinManager materialSkinManager;
        InitStatusEnum initStatusEnum;
        System.Windows.Forms.Timer timerInitChecker;
        IWebDriver driver;
        BackgroundWorker worker;
        CampaignStatusEnum campaignStatusEnum;
        WaSenderForm waSenderForm;
        Logger logger;


        public ContactGrabber(WaSenderForm _waSenderForm)
        {
            InitializeComponent();
            logger = new Logger("ContactGrabber");
            waSenderForm = _waSenderForm;
        }
        private void ChangeInitStatus(InitStatusEnum _initStatus)
        {
            logger.WriteLog("ChangeInitStatus = " + _initStatus.ToString());
            this.initStatusEnum = _initStatus;
            AutomationCommon.ChangeInitStatus(_initStatus, lblInitStatus);
        }


        private void init()
        {
            ChangeInitStatus(InitStatusEnum.NotInitialised);
            ChangeCampStatus(CampaignStatusEnum.NotStarted);
        }
        private void ChangeCampStatus(CampaignStatusEnum _campaignStatus)
        {
            AutomationCommon.ChangeCampStatus(_campaignStatus, lblRunStatus);
        }

        private void ContactGrabber_Load(object sender, EventArgs e)
        {
            init();
            InitLanguage();
        }

        private void checkQRScanDone()
        {
            logger.WriteLog("checkQRScanDone");
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
                    initBackgroundWorker();
                    Activate();
                }
            }
            catch (Exception ex)
            {
                logger.WriteLog(ex.Message);
                logger.WriteLog(ex.StackTrace);
                ChangeInitStatus(InitStatusEnum.Unable);
                timerInitChecker.Stop();
            }
        }

        private void initBackgroundWorker()
        {
            worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;

            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
        }

        List<string> chatNames;


        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            bool isEnd = false;
            chatNames = new List<string>();
            logger.WriteLog("Started Grabbing chat list");
            int nonINsertedCount = 0;
            try
            {
                By newChatButonBy = By.XPath("//*[@id=\"side\"]/header/div[2]/div/span/div[2]/div/span");
                if (AutomationCommon.IsElementPresent(newChatButonBy, driver))
                {
                    driver.FindElement(newChatButonBy).Click();
                    while (isEnd == false)
                    {

                        logger.WriteLog("Not End");
                        By HolderBy = By.ClassName("KPJpj");
                        if (AutomationCommon.IsElementPresent(HolderBy, driver))
                        {
                            IWebElement Holder = driver.FindElement(HolderBy);

                            var list = driver.FindElements(By.XPath("//span[contains(@class,'_3m_Xw')] | //span[contains(@class,'_3q9s6')] | //div[contains(@class,'zoWT4')] "));
                            logger.WriteLog("list count = " + list.Count());
                            bool IsInserted = false;
                            foreach (var chat in list)
                            {
                                try
                                {
                                    string ChatName = chat.FindElement(By.ClassName("ggj6brxn")).Text;
                                    if ((chatNames.Where(_ => _ == ChatName).Count()) == 0)
                                    {
                                        chatNames.Add(ChatName);
                                        IsInserted = true;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    try
                                    {
                                        string ss = chat.GetAttribute("innerHTML");
                                        string ChatName = chat.FindElement(By.ClassName("_3q9s6")).Text;
                                        if ((chatNames.Where(_ => _ == ChatName).Count()) == 0)
                                        {
                                            chatNames.Add(ChatName);
                                            IsInserted = true;
                                        }
                                    }
                                    catch (Exception)
                                    {

                                    }
                                }
                            }
                            if (IsInserted == false)
                            {
                                nonINsertedCount++;
                            }
                            else
                            {
                                nonINsertedCount = 0;
                            }
                        }


                        IJavaScriptExecutor js = (IJavaScriptExecutor)driver;

                        var res = (bool)js.ExecuteScript("return document.getElementsByClassName('KPJpj')[0].offsetHeight + document.getElementsByClassName('KPJpj')[0].scrollTop >= document.getElementsByClassName('KPJpj')[0].scrollHeight");
                        Thread.Sleep(600);
                        isEnd = res;
                        if (isEnd == false && nonINsertedCount > 30)
                        {
                            isEnd = true;
                        }
                        driver.Manage().Window.Maximize();
                        js.ExecuteScript("document.getElementsByClassName('KPJpj')[0].scrollBy(0,100)");
                        logger.WriteLog("Js Executed");
                    }
                }
                else
                {
                    logger.WriteLog("New CHat Element is not Ready");
                }
            }
            catch (Exception ex)
            {
                logger.WriteLog(ex.Message);
                logger.WriteLog(ex.StackTrace);
            }


        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            String FolderPath = Config.GetTempFolderPath();
            String file = Path.Combine(FolderPath, "ContactList_" + Guid.NewGuid().ToString() + ".xlsx");

            string NewFileName = file.ToString();

            File.Copy("ChatListTemplate.xlsx", NewFileName);


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


            savesampleExceldialog.FileName = "ContactLIstList.xlsx";
            if (savesampleExceldialog.ShowDialog() == DialogResult.OK)
            {
                File.Copy(NewFileName, savesampleExceldialog.FileName.EndsWith(".xlsx") ? savesampleExceldialog.FileName : savesampleExceldialog.FileName + ".xlsx");
                Utils.showAlert(Strings.Filedownloadedsuccessfully, Alerts.Alert.enmType.Success);
            }
            ChangeCampStatus(CampaignStatusEnum.Finish);
        }

        private void InitLanguage()
        {
            this.Text = Strings.ContactListGrabber;
            materialLabel2.Text = Strings.InitiateWhatsAppScaneQRCodefromyourmobile;
            btnInitWA.Text = Strings.ClicktoInitiate;
            label5.Text = Strings.Status;
            label7.Text = Strings.Status;
            materialButton1.Text = Strings.StartGrabbing;

        }

        private void ContactGrabber_FormClosed(object sender, FormClosedEventArgs e)
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

        private void btnInitWA_Click(object sender, EventArgs e)
        {
            logger.WriteLog("btnInitWA_Click");
            ChangeInitStatus(InitStatusEnum.Initialising);
            try
            {
                Config.KillChromeDriverProcess();
                var chromeDriverService = ChromeDriverService.CreateDefaultService();
                chromeDriverService.HideCommandPromptWindow = true;


                driver = new ChromeDriver(chromeDriverService, Config.GetChromeOptions());
                driver.Url = "https://web.whatsapp.com";

                checkQRScanDone();
            }
            catch (Exception ex)
            {
                ChangeInitStatus(InitStatusEnum.Unable);
                logger.WriteLog(ex.Message);
                logger.WriteLog(ex.StackTrace);
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

        private void materialButton1_Click(object sender, EventArgs e)
        {
            if (initStatusEnum != InitStatusEnum.Initialised)
            {
                Utils.showAlert(Strings.PleasefollowStepNo1FirstInitialiseWhatsapp, Alerts.Alert.enmType.Error);
                return;
            }
            if (campaignStatusEnum != CampaignStatusEnum.Running)
            {
                worker.RunWorkerAsync();
                ChangeCampStatus(CampaignStatusEnum.Running);
            }
            else
            {
                Utils.showAlert(Strings.Processisalreadyrunning, Alerts.Alert.enmType.Info);
            }
        }
    }
}
