using MaterialSkin.Controls;
using OfficeOpenXml;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
    public partial class GMapExtractor : MaterialForm
    {
        InitStatusEnum initStatusEnum;
        System.Windows.Forms.Timer timerInitChecker;
        IWebDriver driver;
        BackgroundWorker worker;
        CampaignStatusEnum campaignStatusEnum;
        WaSenderForm waSenderForm;
        Logger logger;
        List<GMapModel> gMapModelList;

        public GMapExtractor(WaSenderForm _WASender)
        {
            this.waSenderForm = _WASender;
            InitializeComponent();
        }

        private void GMapExtractor_Load(object sender, EventArgs e)
        {
            initLanguage();
            logger = new Logger("ContactGrabber");
            init();
            gMapModelList = new List<GMapModel>();
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


        private void initLanguage()
        {
            this.Text = Strings.GoogleMapDataEExtractor;
            this.materialLabel2.Text = Strings.Clickbellowbuttontoopenbrowser;
            this.label5.Text = Strings.Status;
            this.materialLabel1.Text = Strings.Usethatwindowtosearchforbusinessesandwhensearchresultsareshown;
            this.materialButton1.Text = Strings.StartGrabbing;
            this.btnInitWA.Text = Strings.OpenBrowser;
            this.materialButton2.Text = Strings.Stop;
            materialButton3.Text = Strings.Export;
            materialButton4.Text = Strings.ImportInWaSender;
            label2.Text = Strings.Count;
            dataGridView1.Columns[0].HeaderText = Strings.Name;
            dataGridView1.Columns[1].HeaderText = Strings.MobileNumber;
            dataGridView1.Columns[2].HeaderText = Strings.ReviewCount;
            dataGridView1.Columns[3].HeaderText = Strings.ReviewCount;
            dataGridView1.Columns[4].HeaderText = Strings.Catagory;
            dataGridView1.Columns[5].HeaderText = Strings.Address;
            dataGridView1.Columns[6].HeaderText = Strings.Website;
            dataGridView1.Columns[7].HeaderText = Strings.PlusCode;
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
                driver.Url = "https://www.google.com/maps/";
                ChangeInitStatus(InitStatusEnum.Initialised);

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

        private bool IsResultShown()
        {

            try
            {
                By ResultsBy = By.XPath(XPathStore.GMap_Result);
                if (AutomationCommon.IsElementPresent(ResultsBy, driver))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        private void initBackgroundWorker()
        {
            worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;

            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);

            initTimer();
        }


        private string GetString(By by)
        {
            if (AutomationCommon.IsElementPresent(by, driver))
            {
                IWebElement el = driver.FindElement(by);
                return el.Text;
            }
            return "";
        }

        bool isStop = false;
        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {

            logger.WriteLog("Started Grabbing chat list");

            IJavaScriptExecutor jsFunction = (IJavaScriptExecutor)driver;
            jsFunction.ExecuteScript("function getElementByXpath(path) { return document.evaluate(path, document, null, XPathResult.FIRST_ORDERED_NODE_TYPE, null).singleNodeValue; }");


            while (!isStop)
            {

                var results = driver.FindElements(By.XPath(XPathStore.GMap_Result));
                GMapModel gMapModel;

                string scrollerJS = " function getElementByXpath(path) { return document.evaluate(path, document, null, XPathResult.FIRST_ORDERED_NODE_TYPE, null).singleNodeValue; }; \nvar ss=getElementByXpath('//*[@id=\"QA0Szd\"]/div/div/div[1]/div[2]/div/div[1]/div/div/div[2]/div[1]');";
                scrollerJS += "return ss.scrollTop = ss.scrollHeight;";
                Int64 divHeight = (Int64)jsFunction.ExecuteScript(scrollerJS);

                logger.WriteLog("divHeight = " + divHeight);

                while (results.Count() <= 19)
                {
                    divHeight = (Int64)jsFunction.ExecuteScript(scrollerJS);
                    Thread.Sleep(1000);
                    results = driver.FindElements(By.XPath(XPathStore.GMap_Result));
                }

                logger.WriteLog("results = " + results.Count());
                foreach (var item in results)
                {
                    if (!isStop)
                    {
                        gMapModel = new GMapModel();
                        try
                        {
                            logger.WriteLog("item Click");
                            item.Click();
                        }
                        catch (Exception ex)
                        {
                            logger.WriteLog("ex=" + ex.Message);
                            jsFunction.ExecuteScript(scrollerJS);

                            driver.Manage().Window.Maximize();
                            Thread.Sleep(500);
                            try
                            {
                                logger.WriteLog("item Click");
                                item.Click();
                            }
                            catch (Exception)
                            {

                            }
                        }

                        Thread.Sleep(1000);
                        logger.WriteLog("Checking for heading..");
                        AutomationCommon.WaitUntilElementVisible(driver, By.XPath(XPathStore.GMap_Heading), 40);

                        if (AutomationCommon.IsElementPresent(By.XPath(XPathStore.GMap_Heading), driver))
                        {
                            logger.WriteLog("Heading is present");
                        }
                        else
                        {
                            logger.WriteLog("Heading is not present");
                        }

                        By GMap_HeadingBy = By.XPath(XPathStore.GMap_Heading);
                        By GMap_MobileNumberBy = By.XPath(XPathStore.GMap_MobileNumber);

                        By GMap_AddressBy = By.XPath(XPathStore.GMap_Address);
                        By GMap_WebSiteBy = By.XPath(XPathStore.GMap_WebSite);
                        By GMap_PlusCodeBy = By.XPath(XPathStore.GMap_PlusCode);
                        By GMap_RatingBy = By.XPath(XPathStore.GMap_Rating);

                        By GMap_ReviewCountBy = By.XPath(XPathStore.GMap_ReviewCount);
                        By GMap_CatagoryBy = By.XPath(XPathStore.GMap_Catagory);


                        AutomationCommon.WaitUntilElementVisible(driver, GMap_HeadingBy, 5);


                        gMapModel.Name = GetString(GMap_HeadingBy);

                        string MobileNumber = GetString(GMap_MobileNumberBy);
                        if (MobileNumber.StartsWith("0"))
                        {
                            MobileNumber = MobileNumber.Substring(1);
                        }

                        MobileNumber = MobileNumber.Replace(@" ", "");
                        gMapModel.mobilenumber = MobileNumber;
                        gMapModel.address = GetString(GMap_AddressBy);
                        gMapModel.website = GetString(GMap_WebSiteBy);
                        gMapModel.PlusCode = GetString(GMap_PlusCodeBy);
                        gMapModel.rating = GetString(GMap_RatingBy);
                        gMapModel.reviewCount = GetString(GMap_ReviewCountBy);
                        gMapModel.category = GetString(GMap_CatagoryBy);

                        gMapModel.Logged = false;
                        gMapModelList.Add(gMapModel);
                        logger.WriteLog("addedd in gMapModelList");
                    }
                }
                logger.WriteLog("Loop completed");
                if (!isStop)
                {

                    try
                    {
                        logger.WriteLog("checking Next button ");
                        By nextButtonBy = By.XPath(XPathStore.GMap_NextButton);
                        if (AutomationCommon.IsElementPresent(nextButtonBy, driver))
                        {
                            logger.WriteLog("Next button clicked");
                            driver.FindElement(nextButtonBy).Click();
                        }
                        AutomationCommon.WaitUntilElementDispose(driver, By.ClassName("IeJeYc"));
                    }
                    catch (Exception ex)
                    {
                        logger.WriteLog("ex= " + ex.Message);
                    }
                }
            }

        }

        System.Windows.Forms.Timer timerRunner;

        private void initTimer()
        {
            timerRunner = new System.Windows.Forms.Timer();
            timerRunner.Interval = 1000;
            timerRunner.Tick += timerRunnerChecker_Tick;
            timerRunner.Start();
        }

        public void timerRunnerChecker_Tick(object sender, EventArgs e)
        {
            try
            {
                int i = 1;
                label1.Text = gMapModelList.Count().ToString();
                foreach (var item in gMapModelList)
                {
                    if (item.Logged == false)
                    {
                        var globalCounter = dataGridView1.Rows.Count - 1;
                        dataGridView1.Rows.Add();
                        dataGridView1.Rows[globalCounter].Cells[0].Value = item.Name;


                        dataGridView1.Rows[globalCounter].Cells[1].Value = item.mobilenumber;
                        dataGridView1.Rows[globalCounter].Cells[2].Value = item.reviewCount;
                        dataGridView1.Rows[globalCounter].Cells[3].Value = item.rating;
                        dataGridView1.Rows[globalCounter].Cells[4].Value = item.category;
                        dataGridView1.Rows[globalCounter].Cells[5].Value = item.address;
                        dataGridView1.Rows[globalCounter].Cells[6].Value = item.website;
                        dataGridView1.Rows[globalCounter].Cells[7].Value = item.PlusCode;
                        dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.RowCount - 1;
                        item.Logged = true;
                    }
                }

            }
            catch (Exception ex)
            {

            }
        }


        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        private void materialButton1_Click(object sender, EventArgs e)
        {
            if (initStatusEnum != InitStatusEnum.Initialised)
            {
                Utils.showAlert(Strings.PleasefollowStepNo1FirstInitialiseWhatsapp, Alerts.Alert.enmType.Error);
                return;
            }
            else if (!IsResultShown())
            {
                Utils.showAlert(Strings.PleaseSearchsomething, Alerts.Alert.enmType.Info);
            }
            else if (campaignStatusEnum != CampaignStatusEnum.Running)
            {
                isStop = false;
                initBackgroundWorker();
                worker.RunWorkerAsync();
                ChangeCampStatus(CampaignStatusEnum.Running);
            }
            else
            {
                Utils.showAlert(Strings.Processisalreadyrunning, Alerts.Alert.enmType.Info);
            }
        }

        private void materialButton2_Click(object sender, EventArgs e)
        {
            timerRunner.Stop();
            worker.CancelAsync();
            isStop = true;
            ChangeCampStatus(CampaignStatusEnum.Stopped);
        }

        private void GMapExtractor_FormClosing(object sender, FormClosingEventArgs e)
        {

            logger.Complete();
            try
            {
                driver.Quit();
            }
            catch (Exception ex)
            {
                
            }
            waSenderForm.formReturn(true);
        }

        private void materialButton4_Click(object sender, EventArgs e)
        {
            logger.Complete();
            try
            {
                driver.Quit();
            }
            catch (Exception ex)
            {

            }
            this.Close();
            this.waSenderForm.gmapDataReturn(this.gMapModelList);
        }

        private void materialButton3_Click(object sender, EventArgs e)
        {
            String FolderPath = Config.GetTempFolderPath();
            String file = Path.Combine(FolderPath, "GMapData" + Guid.NewGuid().ToString() + ".xlsx");
            string NewFileName = file.ToString();
            File.Copy("ChatListTemplate.xlsx", NewFileName);
            var newFile = new FileInfo(NewFileName);
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            using (ExcelPackage xlPackage = new ExcelPackage(newFile))
            {
                var ws = xlPackage.Workbook.Worksheets[0];

                ws.Cells[1, 1].Value = "Name";
                ws.Cells[1, 2].Value = "Mobile Number";
                ws.Cells[1, 3].Value = "Review Count";
                ws.Cells[1, 4].Value = "Rating";
                ws.Cells[1, 5].Value = "Category";
                ws.Cells[1, 6].Value = "Address";
                ws.Cells[1, 7].Value = "Website";
                ws.Cells[1, 8].Value = "PlusCode";


                for (int i = 0; i < gMapModelList.Count(); i++)
                {
                    ws.Cells[i + 2, 1].Value = gMapModelList[i].Name;
                    ws.Cells[i + 2, 2].Value = gMapModelList[i].mobilenumber;
                    ws.Cells[i + 2, 3].Value = gMapModelList[i].reviewCount;
                    ws.Cells[i + 2, 4].Value = gMapModelList[i].rating;
                    ws.Cells[i + 2, 5].Value = gMapModelList[i].category;
                    ws.Cells[i + 2, 6].Value = gMapModelList[i].address;
                    ws.Cells[i + 2, 7].Value = gMapModelList[i].website;
                    ws.Cells[i + 2, 8].Value = gMapModelList[i].PlusCode;
                }
                xlPackage.Save();
            }


            savesampleExceldialog.FileName = "GMapData.xlsx";
            if (savesampleExceldialog.ShowDialog() == DialogResult.OK)
            {
                File.Copy(NewFileName, savesampleExceldialog.FileName.EndsWith(".xlsx") ? savesampleExceldialog.FileName : savesampleExceldialog.FileName + ".xlsx", true);
                Utils.showAlert(Strings.Filedownloadedsuccessfully, Alerts.Alert.enmType.Success);
            }
        }
    }
}
