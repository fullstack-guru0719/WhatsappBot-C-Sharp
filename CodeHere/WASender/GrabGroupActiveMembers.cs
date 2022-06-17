using MaterialSkin;
using MaterialSkin.Controls;
using Newtonsoft.Json;
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
    public partial class GrabGroupActiveMembers : MaterialForm
    {
        MaterialSkin.MaterialSkinManager materialSkinManager;
        InitStatusEnum initStatusEnum;
        System.Windows.Forms.Timer timerInitChecker;
        IWebDriver driver;
        BackgroundWorker worker;
        CampaignStatusEnum campaignStatusEnum;
        WaSenderForm waSenderForm;
        Logger logger;
        System.Windows.Forms.Timer timerRunner;
        private static List<Models.ActiveMemberModel> activeMembersMain = new List<Models.ActiveMemberModel>();

        public GrabGroupActiveMembers(WaSenderForm _waSenderForm)
        {
            InitializeComponent();
            waSenderForm = _waSenderForm;
            materialSkinManager = MaterialSkin.MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkin.MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Green700, Primary.Green400, Primary.Blue900, Accent.Green700, TextShade.WHITE);

            logger = new Logger("GrabAcriveMembers");
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


        private void initTimer()
        {
            timerRunner = new System.Windows.Forms.Timer();
            timerRunner.Interval = 1000;
            timerRunner.Tick += timerRunnerChecker_Tick;
            timerRunner.Start();
        }

        public void timerRunnerChecker_Tick(object sender, EventArgs e)
        {
            if (isStoped == false)
            {
                lbltotalfoundCount.Text = (activeMembersMain.Count() + activeMembers.Count()).ToString();
            }
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




        private void btnInitWA_Click(object sender, EventArgs e)
        {
            ChangeInitStatus(InitStatusEnum.Initialising);
            logger.WriteLog("ChangeInitStatus");
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

        private void ChangeCampStatus(CampaignStatusEnum _campaignStatus)
        {
            AutomationCommon.ChangeCampStatus(_campaignStatus, lblRunStatus);
        }


        private void materialButton1_Click(object sender, EventArgs e)
        {
            //activeMembers = new List<Models.ActiveMemberModel>();
            isStoped = false;
            if (initStatusEnum != InitStatusEnum.Initialised)
            {
                logger.WriteLog("!InitStatusEnum.Initialised");
                Utils.showAlert(Strings.PleasefollowStepNo1FirstInitialiseWhatsapp, Alerts.Alert.enmType.Error);
                return;
            }
            if (campaignStatusEnum != CampaignStatusEnum.Running)
            {
                By groupHeadderBy = By.XPath("//*[@id='main']/header/div[2]/div[1]/div/span");

                if (AutomationCommon.IsElementPresent(groupHeadderBy, driver))
                {
                    logger.WriteLog("groupHeadderBy is present");
                    initBackgroundWorker();
                    worker.RunWorkerAsync();
                    initTimer();
                    ChangeCampStatus(CampaignStatusEnum.Running);
                    campaignStatusEnum = CampaignStatusEnum.Running;
                }
                else
                {
                    Utils.showAlert(Strings.PleaseGotoanygroupchat, Alerts.Alert.enmType.Warning);
                    logger.WriteLog("groupHeadderBy is not present");
                }
            }
        }

        private void GrabGroupActiveMembers_Load(object sender, EventArgs e)
        {
            init();
            InitLanguage();
            ChangeCampStatus(CampaignStatusEnum.NotStarted);
        }

        private void InitLanguage()
        {
            this.Text = Strings.GrabActiveGroupMembers;
            materialLabel2.Text = Strings.InitiateWhatsAppScaneQRCodefromyourmobile;
            materialLabel1.Text = Strings.OpenanyGroupchatClickbuttonbellow;
            btnInitWA.Text = Strings.ClicktoInitiate;
            materialButton1.Text = Strings.StartGrabbing;
            label5.Text = Strings.Status;
            materialButton2.Text = Strings.Stop;
            label1.Text = Strings.TotalFound;
            materialButton3.Text = Strings.Export;
        }




        private void GrabGroupActiveMembers_FormClosing(object sender, FormClosingEventArgs e)
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

            if (campaignStatusEnum == CampaignStatusEnum.Running)
            {
                isStoped = true;
                timerRunner.Stop();
                ChangeCampStatus(CampaignStatusEnum.Stopped);
                campaignStatusEnum = CampaignStatusEnum.Stopped;
                activeMembersMain.AddRange(activeMembers);
                activeMembers = new List<Models.ActiveMemberModel>();
            }

        }

        private void initBackgroundWorker()
        {
            worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);

        }


        private static bool isStoped = false;
        private static List<Models.ActiveMemberModel> activeMembers = new List<Models.ActiveMemberModel>();

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread.Sleep(1000);
            while (isStoped == false)
            {
                Thread.Sleep(1000);
                if (isStoped == false)
                {
                    try
                    {
                        string mainJsString = @"
                    var list=document.getElementsByClassName('_1BUvv');
                    var mainList=[];
                    for ( var i=0;i < list.length;i++)
                        {
                            mainList.push( list[i].innerText );
                        }

                    var finalObj=[];
                    for(var i=0;i<mainList.length;i++)
                    {
	                    finalObj.push({
		                    number:mainList[i],
		                    count:    mainList.filter(x=> x==mainList[i]).length
	                    });

                    }

                    var dups = [];

                    var arr = finalObj.filter(function(el) {
                      // If it is not a duplicate, return true
                      if (dups.indexOf(el.number) == -1) {
                        dups.push(el.number);
                        return true;
                      }

                      return false;
  
                    });

                    return JSON.stringify(arr )
                ";

                        IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                        string jsonstrin = (string)js.ExecuteScript(mainJsString);

                        activeMembers = JsonConvert.DeserializeObject<List<Models.ActiveMemberModel>>(jsonstrin);



                        logger.WriteLog("found : " + activeMembers.Count());

                        IJavaScriptExecutor js2 = (IJavaScriptExecutor)driver;
                        js2.ExecuteScript(" document.getElementsByClassName('_33LGR')[0].scrollTop=5");
                    }
                    catch (Exception ex)
                    {

                        logger.WriteLog(ex.Message);
                    }
                }
                // document.getElementsByClassName('_33LGR')[0].scrollTop=5

                



            }
        }



        private void materialButton3_Click(object sender, EventArgs e)
        {

            if (campaignStatusEnum == CampaignStatusEnum.Stopped)
            {
                if (activeMembersMain.Count() == 0)
                {
                    Utils.showAlert(Strings.Nothingtoexport, Alerts.Alert.enmType.Warning);
                    logger.WriteLog("Nothing to export");
                }
                else
                {
                    string GroupName = "Multiple_Group";

                    String FolderPath = Config.GetTempFolderPath();
                    String file = Path.Combine(FolderPath, "" + GroupName + "_Members_" + Guid.NewGuid().ToString() + ".xlsx");
                    string NewFileName = file.ToString();
                    File.Copy("MemberListTemplate.xlsx", NewFileName);
                    var newFile = new FileInfo(NewFileName);

                    ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                    using (ExcelPackage xlPackage = new ExcelPackage(newFile))
                    {
                        var ws = xlPackage.Workbook.Worksheets[0];
                        ws.Cells[1, 1].Value = "Contact Name";
                        ws.Cells[1, 2].Value = "Total Messages";

                        int i = 1;

                        var nonduplicates = activeMembersMain.GroupBy(x => x.number).Select(y => y.First());

                        foreach (var item in nonduplicates.OrderByDescending(x => x.count))
                        {
                            ws.Cells[i + 1, 1].Value = AutomationCommon.GetNumbers(item.number);
                            ws.Cells[i + 1, 2].Value = item.count;
                            i++;
                        }
                        xlPackage.Save();
                    }
                    savesampleExceldialog.FileName = GroupName + "_Active_Members.xlsx";
                    if (savesampleExceldialog.ShowDialog() == DialogResult.OK)
                    {
                        File.Copy(NewFileName, savesampleExceldialog.FileName.EndsWith(".xlsx") ? savesampleExceldialog.FileName : savesampleExceldialog.FileName + ".xlsx", true);
                        Utils.showAlert(Strings.Filedownloadedsuccessfully, Alerts.Alert.enmType.Success);
                    }
                }
            }

        }
    }
}
