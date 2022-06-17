using MaterialSkin;
using MaterialSkin.Controls;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WASender.enums;
using System.Threading;
using System.Diagnostics;
using System.IO;
using OfficeOpenXml;

namespace WASender
{


    public partial class GrabChatList : MaterialForm
    {
        MaterialSkin.MaterialSkinManager materialSkinManager;
        InitStatusEnum initStatusEnum;
        System.Windows.Forms.Timer timerInitChecker;
        IWebDriver driver;
        BackgroundWorker worker;
        CampaignStatusEnum campaignStatusEnum;
        WaSenderForm waSenderForm;
        Logger logger;

        public GrabChatList(WaSenderForm _waSenderForm)
        {
            InitializeComponent();
            logger = new Logger("GrabChatList");
            waSenderForm = _waSenderForm;
            materialSkinManager = MaterialSkin.MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkin.MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Green700, Primary.Green400, Primary.Blue900, Accent.Green700, TextShade.WHITE);
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
            //worker.ProgressChanged += new ProgressChangedEventHandler(worker_ProgressChanged);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
        }

        List<string> chatNames;

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            bool isEnd = false;
            chatNames = new List<string>();
            logger.WriteLog("Started Grabbing chat list");
            int nonINsertedCount = 0;
            while (isEnd == false)
            {
                logger.WriteLog("Not End");
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
                    { }
                }
                if (IsInserted == false)
                {
                    nonINsertedCount++;
                }
                else
                {
                    nonINsertedCount = 0;
                }
                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                driver.Manage().Window.Maximize();
                var res =(bool) js.ExecuteScript("return document.getElementById('pane-side').offsetHeight + document.getElementById('pane-side').scrollTop >= document.getElementById('pane-side').scrollHeight");
                Thread.Sleep(600);
                isEnd = res;
                if (isEnd == false && nonINsertedCount > 30)
                {
                    isEnd = true;
                }
                
                js.ExecuteScript("document.getElementById('pane-side').scrollBy(0,100)");
                logger.WriteLog("Js Executed");
            }

            

        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            String FolderPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            String file = Path.Combine(FolderPath, "ChatList_" + Guid.NewGuid().ToString() + ".xlsx");
            
            string NewFileName = file.ToString();

            File.Copy("ChatListTemplate.xlsx", NewFileName);


            var newFile = new FileInfo(NewFileName);
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            using (ExcelPackage xlPackage = new ExcelPackage(newFile))
            {
                var ws = xlPackage.Workbook.Worksheets[0];

                for (int i = 0; i < chatNames.Count(); i++)
                {
                    ws.Cells[i + 1,1 ].Value = chatNames[i];
                }
                xlPackage.Save();
            }


            savesampleExceldialog.FileName = "ChatList.xlsx";
            if (savesampleExceldialog.ShowDialog() == DialogResult.OK)
            {
                File.Copy(NewFileName, savesampleExceldialog.FileName.EndsWith(".xlsx") ? savesampleExceldialog.FileName : savesampleExceldialog.FileName + ".xlsx");
                Utils.showAlert(Strings.Filedownloadedsuccessfully, Alerts.Alert.enmType.Success);
            }
            ChangeCampStatus(CampaignStatusEnum.Finish);
        }

        private void ChangeCampStatus(CampaignStatusEnum _campaignStatus)
        {
            AutomationCommon.ChangeCampStatus(_campaignStatus, lblRunStatus);
        }

        private void materialButton1_Click(object sender, EventArgs e)
        {
            if (initStatusEnum != InitStatusEnum.Initialised)
            {
                Utils.showAlert( Strings.PleasefollowStepNo1FirstInitialiseWhatsapp , Alerts.Alert.enmType.Error);
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

        private void btnInitWA_Click(object sender, EventArgs e)
        {
            logger.WriteLog("btnInitWA_Click");
            ChangeInitStatus(InitStatusEnum.Initialising);
            try
            {
                //ChromeOptions options = new ChromeOptions();
                //options.AddExcludedArgument("enable-automation");
                //options.AddAdditionalCapability("useAutomationExtension", false);
                Config.KillChromeDriverProcess();
                var chromeDriverService = ChromeDriverService.CreateDefaultService();
                chromeDriverService.HideCommandPromptWindow = true;


                driver = new ChromeDriver(chromeDriverService, Config.GetChromeOptions());
                //driver = new ChromeDriver();
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

        private void GrabGroups_Load(object sender, EventArgs e)
        {
            init();
            InitLanguage();
        }

        private void InitLanguage()
        {
            this.Text = Strings.GrabChatList;
            materialLabel2.Text = Strings.InitiateWhatsAppScaneQRCodefromyourmobile;
            btnInitWA.Text = Strings.ClicktoInitiate;
            label5.Text = Strings.Status;
            label7.Text = Strings.Status;
            materialButton1.Text = Strings.StartGrabbing;
            
        }

        private void GrabGroups_FormClosed(object sender, FormClosedEventArgs e)
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

        private void materialButton2_Click(object sender, EventArgs e)
        {

            List<string> chatNames = new List<string>();
            chatNames.Add("sjdgfsdfj");
            chatNames.Add("sjdgfsdfj");
            chatNames.Add("sjdgfsdfj");
            chatNames.Add("sjdgfsdfj");

            string NewFileName = "temp\\ChatList_" + Guid.NewGuid().ToString() + ".xlsx";

            File.Copy("ChatListTemplate.xlsx", NewFileName);


            var newFile = new FileInfo(NewFileName);
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            using (ExcelPackage xlPackage = new ExcelPackage(newFile))
            {
                var ws = xlPackage.Workbook.Worksheets[0];

                for (int i = 0; i < chatNames.Count(); i++)
                {
                    ws.Cells[1, i + 1].Value = chatNames[i];
                }
                xlPackage.Save();
            }


            savesampleExceldialog.FileName = "ChatList.xlsx";
            if (savesampleExceldialog.ShowDialog() == DialogResult.OK)
            {
                File.Copy(NewFileName, savesampleExceldialog.FileName);
                Utils.showAlert(Strings.Filedownloadedsuccessfully, Alerts.Alert.enmType.Success);
            }
        }



    }
}
