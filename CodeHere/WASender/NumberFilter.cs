using FluentValidation.Results;
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
using WASender.Models;
using WASender.Validators;

namespace WASender
{
    public partial class NumberFilter : MaterialForm
    {

        MaterialSkin.MaterialSkinManager materialSkinManager;
        InitStatusEnum initStatusEnum;
        System.Windows.Forms.Timer timerInitChecker;
        IWebDriver driver;
        BackgroundWorker worker;
        WaSenderForm waSenderForm;
        CampaignStatusEnum campaignStatusEnum;
        System.Windows.Forms.Timer timerRunner;
        Logger logger;

        public NumberFilter(WaSenderForm _waSenderForm)
        {
            InitializeComponent();
            logger = new Logger("NumberFilter");
            waSenderForm = _waSenderForm;

        }


        private void ChangeInitStatus(InitStatusEnum _initStatus)
        {
            this.initStatusEnum = _initStatus;
            logger.WriteLog(_initStatus.ToString());
            AutomationCommon.ChangeInitStatus(_initStatus, lblInitStatus);
        }
        private void ChangeCampStatus(CampaignStatusEnum _campaignStatus)
        {
            logger.WriteLog(_campaignStatus.ToString());
            this.campaignStatusEnum = _campaignStatus;
            AutomationCommon.ChangeCampStatus(_campaignStatus, lblRunStatus);
        }

        public void init()
        {
            logger.WriteLog("init");
            ChangeInitStatus(InitStatusEnum.NotInitialised);
            ChangeCampStatus(CampaignStatusEnum.NotStarted);
            materialCheckbox1.Checked = true;
            materialCheckbox2.Checked = true;
        }

        private void InitLanguage()
        {
            this.Text = Strings.WhatsAppNumberFilter;
            materialButton1.Text = Strings.UploadSampleExcel;
            De.Text = Strings.DelaySettings;
            materialLabel3.Text = Strings.Wait;
            materialLabel9.Text = Strings.Wait;
            materialLabel4.Text = Strings.to;
            materialLabel8.Text = Strings.to;
            materialLabel5.Text = Strings.secondsafterevery;
            materialLabel6.Text = Strings.NumberCheck;
            materialLabel7.Text = Strings.secondsbeforeeveryNumberCheck;
            materialLabel2.Text = Strings.InitiateWhatsAppScaneQRCodefromyourrmobile;
            label5.Text = Strings.Status;
            label7.Text = Strings.Status;
            btnInitWA.Text = Strings.ClicktoInitiate;
            btnSTart.Text = Strings.StartChecking;

            gridTargetsGroup.Columns[0].HeaderText = Strings.Number;
            gridStatus.Columns[0].HeaderText = Strings.Number;
            gridStatus.Columns[1].HeaderText = Strings.Status;
        }

        private void NumberFilter_FormClosing(object sender, FormClosingEventArgs e)
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

        private void NumberFilter_Load(object sender, EventArgs e)
        {
            init();
            InitLanguage();
        }

        private void materialButton1_Click(object sender, EventArgs e)
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = Strings.SelectExcel;
            openFileDialog.DefaultExt = "xlsx";
            openFileDialog.Filter = "Excel Files|*.xlsx;";
            openFileDialog.Multiselect = false;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string file = openFileDialog.FileName;

                FileInfo fi = new FileInfo(file);
                if (fi.Extension != ".xlsx")
                {
                    Utils.showAlert(Strings.PleaseselectExcelfilesformatonly, Alerts.Alert.enmType.Error);
                    return;
                }

                ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

                using (var package = new ExcelPackage(fi))
                {
                    try
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault();

                        var globalCounter = gridTargetsGroup.Rows.Count - 1;
                        for (int i = 1; i < worksheet.Dimension.Rows; i++)
                        {
                            if (Config.IsDemoMode == true && globalCounter > 5)
                            {
                                Utils.showAlert("You can import only 5 Groups in trial Version", Alerts.Alert.enmType.Error);
                                return;
                            }
                            gridTargetsGroup.Rows.Add();
                            gridTargetsGroup.Rows[globalCounter].Cells[0].Value = worksheet.Cells[i + 1, 1].Value.ToString();
                            globalCounter++;

                        }
                    }
                    catch (Exception ex)
                    {
                        logger.WriteLog(ex.Message);
                        logger.WriteLog(ex.StackTrace);
                        Utils.showAlert(ex.Message, Alerts.Alert.enmType.Error);
                    }
                }
            }
        }

        private void btnInitWA_Click(object sender, EventArgs e)
        {
            ChangeInitStatus(InitStatusEnum.Initialising);

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
                ChangeInitStatus(InitStatusEnum.Unable);
                logger.WriteLog(ex.Message);
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
                    logger.WriteLog("isElementDisplayed = true");
                    ChangeInitStatus(InitStatusEnum.Initialised);
                    timerInitChecker.Stop();
                    initBackgroundWorker();
                    Activate();
                }
            }
            catch (Exception ex)
            {
                logger.WriteLog(ex.Message);
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
            worker.ProgressChanged += new ProgressChangedEventHandler(worker_ProgressChanged);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);

            ChangeCampStatus(CampaignStatusEnum.NotStarted);
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            int counter = 0;
            int totalCounter = 0;

            logger.WriteLog("Started Checking");

            foreach (var item in wASenderGroupTransModel.contactList)
            {
                try
                {
                    By hrefHolder1 = By.ClassName("_2-IT7");
                    By hrefHolder2 = By.ClassName("_1y6Yk");
                    if (AutomationCommon.IsElementPresent(hrefHolder1, driver))
                    {
                        IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                        string NavigateJS = "document.getElementsByClassName('_2-IT7')[0].innerHTML=\"\"; ";
                        NavigateJS += "var para = document.createElement(\"div\"); ";
                        NavigateJS += "para.innerHTML = \"<a class='ownCLickable' href='http://wa.me/" + item.number + "'>*</a>\"; ";
                        NavigateJS += "document.getElementsByClassName(\"_2-IT7\")[0].appendChild(para); ";
                        NavigateJS += "var link = document.getElementsByClassName('ownCLickable'); ";
                        NavigateJS += "link[0].click(); ";
                        js.ExecuteScript(NavigateJS);
                    }
                    else if (AutomationCommon.IsElementPresent(hrefHolder2, driver))
                    {
                        IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                        string NavigateJS = "document.getElementsByClassName('_1y6Yk')[0].innerHTML=\"\"; ";
                        NavigateJS += "var para = document.createElement(\"div\"); ";
                        NavigateJS += "para.innerHTML = \"<a class='ownCLickable' href='http://wa.me/" + item.number + "'>*</a>\"; ";
                        NavigateJS += "document.getElementsByClassName(\"_1y6Yk\")[0].appendChild(para); ";
                        NavigateJS += "var link = document.getElementsByClassName('ownCLickable'); ";
                        NavigateJS += "link[0].click(); ";
                        js.ExecuteScript(NavigateJS);
                    }
                    else
                    {
                        IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                        js.ExecuteScript("window.location.href='https://web.whatsapp.com/send?phone=" + item.number + "&text&app_absent=0'");
                    }

                    Thread.Sleep(1000);

                    try
                    {
                        By loadingBy = By.XPath("//div[contains(@class, '_1INL_') and contains(@class, '_1iyey') and contains(@class, '_2FX6G') and contains(@class, '_1UG2S') ]");

                        bool IsLoading = AutomationCommon.IsElementPresent(loadingBy, driver);
                        if (IsLoading == true)
                        {
                            AutomationCommon.WaitUntilElementDispose(driver, loadingBy, 500);
                        }
                    }
                    catch (Exception ex)
                    {
                    }

                    try
                    {
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
                    catch (Exception ex)
                    {

                    }

                    try
                    {
                        bool IsSearching = AutomationCommon.IsElementPresent(By.ClassName("_3_EXz"), driver);

                        if (IsSearching == true)
                        {
                            AutomationCommon.WaitUntilElementDispose(driver, By.ClassName("_3_EXz"), 500);
                        }
                        AutomationCommon.checkConnection(driver);

                        if (true == true)
                        {
                           // AutomationCommon.WaitUntilElementDispose(driver, By.ClassName("_3_EXz"), 500);

                            By NumberInvalidBox = By.ClassName("_2Nr6U");
                            if (AutomationCommon.IsElementPresent(NumberInvalidBox, driver))
                            {
                                By OkButtonBy = By.ClassName("_20C5O");
                                if (AutomationCommon.IsElementPresent(OkButtonBy, driver))
                                {
                                    driver.FindElement(OkButtonBy).Click();

                                    item.sendStatusModel.isDone = true;
                                    item.sendStatusModel.sendStatusEnum = SendStatusEnum.NotAvailable;
                                    counter++;
                                    totalCounter++;

                                    var _count = wASenderGroupTransModel.contactList.Count();
                                    var percentage = totalCounter * 100 / _count;
                                    worker.ReportProgress(percentage);
                                    // continue;
                                }
                            }
                            else
                            {
                                AutomationCommon.checkConnection(driver);
                                By messageTextBoxBy = By.XPath("//*[@id='main']/footer/div[1]/div/span[2]/div/div[2]/div[1]/div/div[2]");
                                IWebElement messageTextBox = driver.FindElement(messageTextBoxBy);
                                if (!AutomationCommon.IsElementPresent(messageTextBoxBy, driver))
                                {
                                    item.sendStatusModel.isDone = true;
                                    item.sendStatusModel.sendStatusEnum = SendStatusEnum.NotAvailable;
                                    counter++;
                                    totalCounter++;

                                    var _count = wASenderGroupTransModel.contactList.Count();
                                    var percentage = totalCounter * 100 / _count;
                                    worker.ReportProgress(percentage);

                                }
                                else
                                {
                                    item.sendStatusModel.isDone = true;
                                    item.sendStatusModel.sendStatusEnum = SendStatusEnum.Available;
                                    counter++;
                                    totalCounter++;

                                    var _count = wASenderGroupTransModel.contactList.Count();
                                    var percentage = totalCounter * 100 / _count;
                                    worker.ReportProgress(percentage);

                                    try
                                    {
                                        By menuBy = By.XPath("//*[@id='main']/header/div[3]/div/div[2]/div/div");
                                        By meuBySecond = By.XPath("//*[@id='main']/header/div[3]/div/div[3]/div/div/span");


                                        if (AutomationCommon.IsElementPresent(menuBy, driver))
                                        {
                                            IWebElement menu = driver.FindElement(menuBy);
                                            menu.Click();
                                        }
                                        else if (AutomationCommon.IsElementPresent(meuBySecond, driver))
                                        {
                                            IWebElement menu = driver.FindElement(meuBySecond);
                                            menu.Click();
                                        }

                                        Thread.Sleep(500);

                                        IWebElement menuHolder = driver.FindElement(By.CssSelector("[data-testid='contact-menu-dropdown']"));

                                        var lis = menuHolder.FindElements(By.ClassName("_2qR8G"));
                                        if (lis.Count() == 6)
                                        {
                                            lis[2].Click();
                                        }
                                        else if (lis.Count() == 9)
                                        {
                                            lis[5].Click();
                                        }
                                        else if (lis.Count() == 8)
                                        {
                                            lis[4].Click();
                                        }
                                        else if (lis.Count() == 7)
                                        {
                                            lis[2].Click();
                                        }
                                        else
                                        {
                                            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                                            js.ExecuteScript("window.location.href=''");
                                        }

                                        By prifileBy = By.XPath("//*[@class='_1PzAL']");
                                        if (AutomationCommon.IsElementPresent(prifileBy, driver))
                                        {
                                            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                                            js.ExecuteScript("window.location.href=''");
                                        }

                                    }
                                    catch (Exception Ex)
                                    {
                                        string ss = "";
                                    }

                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {

                    }

                    Thread.Sleep(Utils.getRandom(wASenderGroupTransModel.settings.delayAfterEveryMessageFrom * 1000, wASenderGroupTransModel.settings.delayAfterEveryMessageTo * 1000));
                    counter++;

                    if (wASenderGroupTransModel.settings.delayAfterMessages == counter)
                    {
                        counter = 0;
                        Thread.Sleep(Utils.getRandom(wASenderGroupTransModel.settings.delayAfterMessagesFrom * 1000, wASenderGroupTransModel.settings.delayAfterMessagesFrom * 1000));
                    }


                }
                catch (Exception ex2)
                {
                    item.sendStatusModel.isDone = true;
                    item.sendStatusModel.sendStatusEnum = SendStatusEnum.NotAvailable;
                    counter++;
                    totalCounter++;

                    var _count = wASenderGroupTransModel.contactList.Count();
                    var percentage = totalCounter * 100 / _count;
                    worker.ReportProgress(percentage);
                }
            }

        }

        private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            lblPersentage.Text = e.ProgressPercentage + "% " + Strings.Completed;
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ChangeCampStatus(CampaignStatusEnum.Finish);
            // AutomationCommon.GenerateReport(this.wASenderGroupTransModel);

            String FolderPath = Config.GetTempFolderPath();
            String file = Path.Combine(FolderPath, "Number_Filter_" + Guid.NewGuid().ToString() + ".xlsx");
            string NewFileName = file.ToString();
            File.Copy("MemberListTemplate.xlsx", NewFileName);
            var newFile = new FileInfo(NewFileName);
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            using (ExcelPackage xlPackage = new ExcelPackage(newFile))
            {
                var ws = xlPackage.Workbook.Worksheets[0];

                for (int i = 0; i < wASenderGroupTransModel.contactList.Count(); i++)
                {
                    ws.Cells[i + 1, 1].Value = wASenderGroupTransModel.contactList[i].number;
                    ws.Cells[i + 1, 2].Value = wASenderGroupTransModel.contactList[i].sendStatusModel.sendStatusEnum;
                }
                xlPackage.Save();
            }


            savesampleExceldialog.FileName = "NumberFilter.xlsx";
            if (savesampleExceldialog.ShowDialog() == DialogResult.OK)
            {
                File.Copy(NewFileName, savesampleExceldialog.FileName.EndsWith(".xlsx") ? savesampleExceldialog.FileName : savesampleExceldialog.FileName + ".xlsx", true);
                Utils.showAlert(Strings.Filedownloadedsuccessfully, Alerts.Alert.enmType.Success);
            }
        }

        private void btnSTart_Click(object sender, EventArgs e)
        {
            ValidateControlsGroup();
        }

        WASenderSingleTransModel wASenderGroupTransModel;

        private bool CheckValidationMessage(IList<ValidationFailure> validationFailures, string AdditionalMessage = "")
        {
            string Messages = "";
            if (validationFailures != null && validationFailures.Count() > 0)
            {
                foreach (var item in validationFailures)
                {
                    Messages = Messages + item.ErrorMessage + "\n\n";
                }
            }
            if (Messages == "")
            {
                return true;
            }
            else
            {
                MessageBox.Show(AdditionalMessage + " " + Messages, Strings.Errors, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }


        private bool showValidationErrorIfAnyGroup()
        {
            bool validationFail = true;
            if (CheckValidationMessage(wASenderGroupTransModel.validationFailures))
            {
                if (CheckValidationMessage(wASenderGroupTransModel.settings.validationFailures))
                {
                    for (int i = 0; i < wASenderGroupTransModel.contactList.Count(); i++)
                    {
                        if (CheckValidationMessage(wASenderGroupTransModel.contactList[i].validationFailures, Strings.RowNo + "- " + Convert.ToString(i + 1)))
                        {
                            string ss = "";
                        }
                        else
                        {
                            i = wASenderGroupTransModel.contactList.Count;
                            validationFail = false;
                        }
                    }
                }
                else
                {
                    validationFail = false;
                }
            }
            else
            {
                validationFail = false;
            }
            return validationFail;
        }

        private void ValidateControlsGroup()
        {
            wASenderGroupTransModel = new WASenderSingleTransModel();
            wASenderGroupTransModel.contactList = new List<ContactModel>();
            ContactModel contact;
            if (initStatusEnum != InitStatusEnum.Initialised)
            {
                Utils.showAlert(Strings.PleaseFollowStepnoThree, Alerts.Alert.enmType.Error);
                return;
            }

            for (int i = 0; i < gridTargetsGroup.Rows.Count; i++)
            {
                if (!(gridTargetsGroup.Rows[i].Cells[0].Value == null))
                {
                    contact = new ContactModel
                    {
                        number = gridTargetsGroup.Rows[i].Cells[0].Value == null ? "" : gridTargetsGroup.Rows[i].Cells[0].Value.ToString(),
                        sendStatusModel = new SendStatusModel { isDone = false }
                    };

                    contact.validationFailures = new ContactModelValidator().Validate(contact).Errors;
                    wASenderGroupTransModel.contactList.Add(contact);
                }
            }

            wASenderGroupTransModel.messages = new List<MesageModel>();
            wASenderGroupTransModel.messages.Add(new MesageModel
            {
                longMessage = "dfg"
            });
            wASenderGroupTransModel.validationFailures = new WASenderSingleTransModelValidator().Validate(wASenderGroupTransModel).Errors;

            wASenderGroupTransModel.settings = new SingleSettingModel();
            wASenderGroupTransModel.settings.delayAfterMessages = Convert.ToInt32(txtdelayAfterMessages.Text);
            wASenderGroupTransModel.settings.delayAfterMessagesFrom = Convert.ToInt32(txtdelayAfterMessagesFrom.Text);
            wASenderGroupTransModel.settings.delayAfterMessagesTo = Convert.ToInt32(txtdelayAfterMessagesTo.Text);
            wASenderGroupTransModel.settings.delayAfterEveryMessageFrom = Convert.ToInt32(txtdelayAfterEveryMessageFrom.Text);
            wASenderGroupTransModel.settings.delayAfterEveryMessageTo = Convert.ToInt32(txtdelayAfterEveryMessageTo.Text);

            wASenderGroupTransModel.settings.validationFailures = new SingleSettingModelValidator().Validate(wASenderGroupTransModel.settings).Errors;

            if (showValidationErrorIfAnyGroup())
            {
                if (campaignStatusEnum != CampaignStatusEnum.Running)
                {
                    worker.RunWorkerAsync();
                    ChangeCampStatus(CampaignStatusEnum.Running);
                    initTimer();
                }
            }

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
            try
            {
                int i = 1;
                foreach (var item in wASenderGroupTransModel.contactList)
                {
                    if (item.sendStatusModel.isDone == true && item.logged == false)
                    {
                        var globalCounter = gridStatus.Rows.Count - 1;
                        gridStatus.Rows.Add();
                        gridStatus.Rows[globalCounter].Cells[0].Value = item.number;
                        gridStatus.Rows[globalCounter].Cells[1].Value = item.sendStatusModel.sendStatusEnum;

                        gridStatus.FirstDisplayedScrollingRowIndex = gridStatus.RowCount - 1;
                        item.logged = true;

                    }
                }

            }
            catch (Exception ex)
            {

            }
        }
    }
}
