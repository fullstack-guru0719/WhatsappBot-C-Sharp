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
using System.Threading.Tasks;
using System.Windows.Forms;
using WASender.enums;

namespace WASender
{
    public partial class GrabGroupLinks : MaterialForm
    {

        MaterialSkin.MaterialSkinManager materialSkinManager;
        InitStatusEnum initStatusEnum;
        System.Windows.Forms.Timer timerInitChecker;
        IWebDriver driver;
        BackgroundWorker worker;
        CampaignStatusEnum campaignStatusEnum;
        WaSenderForm waSenderForm;

        public GrabGroupLinks(WaSenderForm _waSenderForm)
        {
            InitializeComponent();

            waSenderForm = _waSenderForm;
            materialSkinManager = MaterialSkin.MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkin.MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Green700, Primary.Green400, Primary.Blue900, Accent.Green700, TextShade.WHITE);
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
                    ChangeInitStatus(InitStatusEnum.Initialised);
                    timerInitChecker.Stop();
                    Activate();
                }
            }
            catch (Exception ex)
            {
                ChangeInitStatus(InitStatusEnum.Unable);
                timerInitChecker.Stop();
            }
        }


        List<string> chatNames;

        private void btnInitWA_Click(object sender, EventArgs e)
        {
            ChangeInitStatus(InitStatusEnum.Initialising);
            try
            {
                ChromeOptions options = new ChromeOptions();
                options.AddExcludedArgument("enable-automation");
                options.AddAdditionalCapability("useAutomationExtension", false);
                var chromeDriverService = ChromeDriverService.CreateDefaultService();
                chromeDriverService.HideCommandPromptWindow = true;


                driver = new ChromeDriver(chromeDriverService, options);
                //driver = new ChromeDriver();
                driver.Url = "https://www.google.com/search?q=whatsapp+group+links&oq=whatsapp+group+links&aqs=chrome.0.69i59j0i433i512j0i512j0i457i512j0i402j69i60l3.2696j0j7&sourceid=chrome&ie=UTF-8";

               // checkQRScanDone();
                ChangeInitStatus(InitStatusEnum.Initialised);
            }
            catch (Exception ex)
            {
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
            }
        }

        private void GrabGroupLinks_Load(object sender, EventArgs e)
        {
            init();
            InitLanguage();
        }

        private void InitLanguage()
        {
            this.Text = Strings.GrabGroupLinks;
            this.materialLabel2.Text = Strings.Clickbellowbuttontoopenbrowser;
            this.label5.Text = Strings.Status;
            this.materialLabel1.Text = Strings.Navigatetoanywebsitewherelistedgrouplinkstheclickbellowbellowbuton;
            this.materialButton1.Text = Strings.StartGrabbing;
            this.btnInitWA.Text = Strings.OpenBrowser;
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
                chatNames = new List<string>();
                var links = driver.FindElements(By.XPath("//a[contains(@href,'https://chat.whatsapp.com/')]"));
                int globalCounter = 0;
                foreach (var item in links)
                {
                    try
                    {
                        if (Config.IsDemoMode == true)
                        {
                            if (globalCounter > 5)
                            {
                                Utils.showAlert("You can Extract only 5 Links in trial version", Alerts.Alert.enmType.Error);
                                break;
                            }
                        }
                        string Link = item.GetAttribute("href").ToString();
                        chatNames.Add(Link);
                        globalCounter++;
                    }
                    catch (Exception ex)
                    { 
                        
                    }
                }
                if (links.Count() == 0)
                {
                    Utils.showAlert(Strings.NoGroupLinkfoundincurrentPage, Alerts.Alert.enmType.Error);
                }
                else
                {
                    String FolderPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
                    String file = Path.Combine(FolderPath, "GroupLinks__" + Guid.NewGuid().ToString() + ".xlsx");
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


                    savesampleExceldialog.FileName = "GroupLinks.xlsx";
                    if (savesampleExceldialog.ShowDialog() == DialogResult.OK)
                    {
                        File.Copy(NewFileName, savesampleExceldialog.FileName, true);
                        Utils.showAlert(Strings.Filedownloadedsuccessfully, Alerts.Alert.enmType.Success);
                    }
                }

                

            }
        }

        private void GrabGroupLinks_FormClosed(object sender, FormClosedEventArgs e)
        {
            waSenderForm.formReturn(true);
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
