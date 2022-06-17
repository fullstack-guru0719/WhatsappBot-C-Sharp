using MaterialSkin;
using MaterialSkin.Controls;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WASender.enums;
using WASender.Models;

namespace WASender
{
    public partial class RunGroup : MaterialForm
    {
        MaterialSkin.MaterialSkinManager materialSkinManager;
        WASenderGroupTransModel wASenderGroupTransModel;
        WaSenderForm waSenderForm;
        InitStatusEnum initStatusEnum;
        CampaignStatusEnum campaignStatusEnum;
        IWebDriver driver;
        System.Windows.Forms.Timer timerInitChecker;
        System.Windows.Forms.Timer timerRunner;
        BackgroundWorker worker;
        Logger logger;

        public RunGroup(WASenderGroupTransModel _wASenderGroupTransModel, WaSenderForm _waSenderForm)
        {
            logger = new Logger("RunGroup");
            InitializeComponent();
            materialSkinManager = MaterialSkin.MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkin.MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Green700, Primary.Green400, Primary.Blue900, Accent.Green700, TextShade.WHITE);

            this.waSenderForm = _waSenderForm;
            this.wASenderGroupTransModel = _wASenderGroupTransModel;
            this.Text = _wASenderGroupTransModel.CampaignName;
        }

        private void RunGroup_FormClosed(object sender, FormClosedEventArgs e)
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

        private void init()
        {
            label4.ForeColor = Color.Red;
            ChangeInitStatus(InitStatusEnum.NotInitialised);
            ChangeCampStatus(CampaignStatusEnum.NotStarted);
        }

        private void ChangeCampStatus(CampaignStatusEnum _campaignStatus)
        {
            this.campaignStatusEnum = _campaignStatus;
            AutomationCommon.ChangeCampStatus(_campaignStatus, lblRunStatus);
        }

        private void ChangeInitStatus(InitStatusEnum _initStatus)
        {
            this.initStatusEnum = _initStatus;
            AutomationCommon.ChangeInitStatus(_initStatus, lblInitStatus);
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
                    initBackgroundWorker();
                    Activate();
                }
            }
            catch (Exception ex)
            {
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
            string BaseMessageId = null;

            foreach (var item in wASenderGroupTransModel.groupList)
            {
                try
                {

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

                    //  AutomationCommon.checkConnection(driver);

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

                    AutomationCommon.checkConnection(driver);

                    By searchTextBoxBy = By.XPath("//div[contains(@class, '_13NKt') and contains(@class, 'copyable-text') and contains(@class, 'selectable-text')  ]");
                    if (AutomationCommon.IsElementPresent(searchTextBoxBy, driver))
                    {
                        IWebElement searchTextBox = driver.FindElement(searchTextBoxBy);


                        AutomationCommon.checkConnection(driver);

                        By BackButtonBy = By.ClassName("_28-cz");
                        if (AutomationCommon.IsElementPresent(BackButtonBy, driver))
                        {
                            driver.FindElement(BackButtonBy).Click();
                        }
                        searchTextBox.SendKeys(item.Name);
                        searchTextBox.SendKeys(" ");

                        Thread.Sleep(1000); //  

                        AutomationCommon.checkConnection(driver);
                        By searchListBy = By.XPath("//div[contains(@class, '_3uIPm') and contains(@class, 'WYyr1') ]");

                        if (AutomationCommon.IsElementPresent(searchListBy, driver))
                        {
                            IWebElement searchList = driver.FindElement(searchListBy);
                            List<IWebElement> ss = searchList.FindElements(By.ClassName("_3m_Xw")).ToList();

                            if (ss.Count == 0)
                            {
                                ss = searchList.FindElements(By.XPath("//div[contains(@class,'lhggkp7q')]")).ToList();
                            }

                            ss[1].Click();
                            Thread.Sleep(1000);

                            if (AutomationCommon.IsElementPresent(BackButtonBy, driver))
                            {
                                driver.FindElement(BackButtonBy).Click();
                            }


                            searchTextBox.Clear();



                            By DisappearingmessagesBy = By.ClassName("_1N4rE");
                            if (AutomationCommon.IsElementPresent(DisappearingmessagesBy, driver))
                            {
                                By HeaderHolderBy = By.ClassName("_1FrBz");
                                IWebElement Disappearingmessages = driver.FindElement(DisappearingmessagesBy);
                                if (AutomationCommon.IsElementPresent(HeaderHolderBy, Disappearingmessages))
                                {
                                    IWebElement HeaderHolder = driver.FindElement(HeaderHolderBy);

                                    if (AutomationCommon.IsElementPresent(HeaderHolderBy, Disappearingmessages))
                                    {
                                        IWebElement holder = Disappearingmessages.FindElement(HeaderHolderBy);
                                        try
                                        {
                                            string HeaderText = holder.FindElement(By.TagName("h1")).Text;
                                            if (HeaderText == "Disappearing messages")
                                            {
                                                By okButtonBy = By.XPath("//div[contains(@class, '_20C5O') and contains(@class, '_2Zdgs') ]");
                                                if (AutomationCommon.IsElementPresent(okButtonBy, Disappearingmessages))
                                                {
                                                    IWebElement okButton = Disappearingmessages.FindElement(okButtonBy);
                                                    okButton.Click();
                                                }
                                            }
                                        }
                                        catch (Exception ex)
                                        {

                                        }
                                    }
                                }

                            }

                            AutomationCommon.checkConnection(driver);
                            By TextBoxBy = By.XPath("//*[@id='main']/footer/div[1]/div/span[2]/div/div[2]/div[1]/div/div[2]");
                            IWebElement el = driver.FindElement(By.XPath("//*[@id=\"main\"]/footer/div[1]/div/span[2]/div/div[2]/div[1]/div/div[1]"));

                            if (AutomationCommon.IsElementPresent(TextBoxBy, driver))
                            {
                                IWebElement messageTextBox = driver.FindElement(TextBoxBy);
                                int n = Utils.getRandom(0, wASenderGroupTransModel.messages.Where(x => x != null).Count());
                                MesageModel mesageModel = wASenderGroupTransModel.messages.Where(x => x != null).ToList()[n];


                                int fileCntr = 0;
                                IWebElement image = null;

                                if (mesageModel.files.Count > 0)
                                {
                                    driver.FindElement(By.XPath("//*[@id='main']/footer/div[1]/div/span[2]/div/div[1]/div[2]/div")).Click();
                                    image = driver.FindElement(By.XPath("//*[@id='main']/footer/div[1]/div/span[2]/div/div[1]/div[2]/div/span/div[1]/div/ul/li[1]/button/input"));
                                }

                                string filesString = "";
                                foreach (var file in mesageModel.files)
                                {
                                    if (filesString != "")
                                    {
                                        filesString += "\n" + file.filePath;
                                    }
                                    else
                                    {
                                        filesString += file.filePath;
                                    }
                                    fileCntr++;
                                }

                                if (filesString != "")
                                {
                                    image.SendKeys(filesString);

                                    Thread.Sleep(TimeSpan.FromSeconds(2));
                                    AutomationCommon.checkConnection(driver);
                                    driver.FindElement(By.XPath("//*[@id='app']/div[1]/div[1]/div[2]/div[2]/span/div[1]/span/div[1]/div/div[2]/div/div[2]/div[2]/div/div")).Click();
                                    Thread.Sleep(500);
                                }

                                if (wASenderGroupTransModel.MessageSendingType == 2)
                                {
                                    var messages = mesageModel.longMessage.Split('\n');

                                    foreach (var m in messages)
                                    {
                                        if (m != "")
                                        {
                                            string MsgLine = m;
                                            // Check KeyMarkers
                                            if (m.Contains("{{ KEY :"))
                                            {
                                                string str = Utils.ExtractBetweenTwoStrings(m, "{{ KEY :", "}}", false, false);
                                                var Keysplitter = str.Split('|');
                                                string randomKey = Keysplitter[Utils.getRandom(0, Keysplitter.Length - 1)];
                                                MsgLine = m.Replace("{{ KEY :" + str + "}}", randomKey);
                                            }
                                            // Check {{ RANDOM }}
                                            if (MsgLine.Contains("{{ RANDOM }}"))
                                            {
                                                string rand = Utils.getRandom(10000, 50000).ToString();
                                                MsgLine = MsgLine.Replace("{{ RANDOM }}", rand);
                                            }


                                            var splitter = MsgLine.Split(' ');
                                            foreach (var _char in splitter)
                                            {
                                                try
                                                {
                                                    messageTextBox.SendKeys(_char.ToString() + " ");
                                                }
                                                catch (Exception Ex)
                                                {
                                                    try
                                                    {
                                                        string new_char = _char.Replace("\r", "");
                                                        string script = "arguments[0].innerHTML += '" + new_char + "'";
                                                        ((IJavaScriptExecutor)driver).ExecuteScript(script, messageTextBox);
                                                        Thread.Sleep(500);
                                                        messageTextBox.SendKeys(OpenQA.Selenium.Keys.Control + OpenQA.Selenium.Keys.End);
                                                        Thread.Sleep(500);
                                                    }
                                                    catch (Exception exx)
                                                    {
                                                        try
                                                        {
                                                            Invoke((Action)(() => { Clipboard.SetText(_char.ToString() + " "); }));
                                                            el.SendKeys(OpenQA.Selenium.Keys.Control + "v");
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            string new_char = _char.Replace("\r", "");
                                                            string script = "arguments[0].innerHTML += '" + new_char + "'";
                                                            ((IJavaScriptExecutor)driver).ExecuteScript(script, messageTextBox);
                                                            Thread.Sleep(500);
                                                            el.SendKeys(OpenQA.Selenium.Keys.Control + OpenQA.Selenium.Keys.End);
                                                            Thread.Sleep(500);
                                                        }
                                                    }
                                                }
                                            }
                                            try
                                            {
                                                messageTextBox.SendKeys(OpenQA.Selenium.Keys.Shift + '\n');
                                            }
                                            catch (Exception ex)
                                            {
                                                 el.SendKeys(OpenQA.Selenium.Keys.Control + "\n");
                                            }
                                        }
                                    }
                                }
                                else if (wASenderGroupTransModel.MessageSendingType == 1)
                                {
                                    var messages = mesageModel.longMessage.Split('\n');
                                    string NewMessage = "";
                                    foreach (var m in messages)
                                    {
                                        if (m != "")
                                        {
                                            string MsgLine = m;

                                            // Check For KeyMarker
                                            if (m.Contains("{{ KEY :"))
                                            {
                                                string str = Utils.ExtractBetweenTwoStrings(m, "{{ KEY :", "}}", false, false);
                                                var Keysplitter = str.Split('|');
                                                string randomKey = Keysplitter[Utils.getRandom(0, Keysplitter.Length - 1)];
                                                MsgLine = m.Replace("{{ KEY :" + str + "}}", randomKey);
                                            }
                                            // Check {{ RANDOM }}
                                            if (MsgLine.Contains("{{ RANDOM }}"))
                                            {
                                                string rand = Utils.getRandom(10000, 50000).ToString();
                                                MsgLine = MsgLine.Replace("{{ RANDOM }}", rand);
                                            }

                                            MsgLine = MsgLine + "\n";
                                            NewMessage = NewMessage + MsgLine;
                                        }
                                    }


                                    try
                                    {
                                        Invoke((Action)(() => { Clipboard.SetText(NewMessage); }));
                                        messageTextBox.SendKeys(OpenQA.Selenium.Keys.Control + "v");
                                    }
                                    catch (Exception ex)
                                    {
                                        try
                                        {
                                            Invoke((Action)(() => { Clipboard.SetText(NewMessage); }));
                                            el.SendKeys(OpenQA.Selenium.Keys.Control + "v");
                                        }
                                        catch (Exception)
                                        {

                                        }
                                    }
                                }



                                AutomationCommon.checkConnection(driver);
                                try
                                {
                                    IWebElement sendButton = AutomationCommon.WaitUntilElementVisible(driver, By.ClassName("_4sWnG"), 0);
                                    sendButton.Click();
                                    Thread.Sleep(250);
                                }
                                catch (Exception ex)
                                {

                                }

                                //Thread.Sleep(TimeSpan.FromSeconds(2));
                                By sendButton2 = By.XPath("//*[@id=\"main\"]/footer/div[1]/div/span[2]/div/div[2]/div[2]/button/span");
                                if (AutomationCommon.IsElementPresent(sendButton2, driver))
                                {
                                    try
                                    {
                                        driver.FindElement(sendButton2).Click();
                                        Thread.Sleep(250);
                                    }
                                    catch (Exception ex)
                                    {

                                    }
                                }
                                if (AutomationCommon.IsElementPresent(By.ClassName("_4sWnG"), driver))
                                {
                                    try
                                    {
                                        IJavaScriptExecutor jssend = (IJavaScriptExecutor)driver;
                                        jssend.ExecuteScript("document.getElementsByClassName('_4sWnG')[0].click()");
                                        Thread.Sleep(500);
                                    }
                                    catch (Exception ex)
                                    {

                                    }
                                }

                                By buttonByCustomAttr = By.CssSelector("[data-testid='send']");
                                if (AutomationCommon.IsElementPresent(buttonByCustomAttr, driver))
                                {
                                    IJavaScriptExecutor jssend = (IJavaScriptExecutor)driver;
                                    jssend.ExecuteScript("document.querySelector('[data-testid=\"send\"]').click()");
                                    Thread.Sleep(500);
                                }

                                BaseMessageId = driver.FindElement(By.ClassName("message-out")).GetAttribute("data-id");

                                AutomationCommon.checkConnection(driver);
                                bool IsStillSending = AutomationCommon.IsElementPresent(By.XPath("//*[@data-testid=\"msg-time\"]"), driver);

                                if (IsStillSending == true)
                                {
                                    AutomationCommon.WaitUntilElementDispose(driver, By.XPath("//*[@data-testid=\"msg-time\"]"), 1000);
                                }

                                Thread.Sleep(1000);
                                Thread.Sleep(Utils.getRandom(wASenderGroupTransModel.settings.delayAfterEveryMessageFrom * 1000, wASenderGroupTransModel.settings.delayAfterEveryMessageTo * 1000));
                                counter++;

                                if (wASenderGroupTransModel.settings.delayAfterMessages == counter)
                                {
                                    counter = 0;
                                    Thread.Sleep(Utils.getRandom(wASenderGroupTransModel.settings.delayAfterMessagesFrom * 1000, wASenderGroupTransModel.settings.delayAfterMessagesFrom * 1000));
                                }
                            }
                            else
                            {
                                item.sendStatusModel.isDone = true;
                                item.sendStatusModel.sendStatusEnum = SendStatusEnum.GroupAdminOnly;
                                item.sendStatusModel.IsSuccess = false;
                                totalCounter++;
                                worker.ReportProgress(totalCounter * 100 / wASenderGroupTransModel.groupList.Count());
                                continue;
                            }

                        }
                        else
                        {
                            item.sendStatusModel.isDone = true;
                            item.sendStatusModel.sendStatusEnum = SendStatusEnum.GroupNotFound;
                            item.sendStatusModel.IsSuccess = false;
                            totalCounter++;
                            worker.ReportProgress(totalCounter * 100 / wASenderGroupTransModel.groupList.Count());
                            continue;
                        }
                    }
                }
                catch (Exception ex)
                {
                    item.sendStatusModel.isDone = true;
                    item.sendStatusModel.sendStatusEnum = SendStatusEnum.Failed;
                    totalCounter++;
                    worker.ReportProgress(totalCounter * 100 / wASenderGroupTransModel.groupList.Count());
                    continue;
                }

                totalCounter++;

                var __count = wASenderGroupTransModel.groupList.Count();
                var _percentage = totalCounter * 100 / __count;
                item.sendStatusModel.isDone = true;
                item.sendStatusModel.sendStatusEnum = SendStatusEnum.Success;
                worker.ReportProgress(_percentage);

            }



        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ChangeCampStatus(CampaignStatusEnum.Finish);
            AutomationCommon.GenerateReport(this.wASenderGroupTransModel);
        }

        private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            lblPersentage.Text = e.ProgressPercentage + "% " + Strings.Completed;
        }

        private void btnInitWA_Click(object sender, EventArgs e)
        {
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

        private void btnSTart_Click(object sender, EventArgs e)
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
                initTimer();
            }
            else
            {
                Utils.showAlert(Strings.Processisalreadyrunning, Alerts.Alert.enmType.Info);
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
                foreach (var item in wASenderGroupTransModel.groupList)
                {
                    if (item.sendStatusModel.isDone == true && item.logged == false)
                    {
                        var globalCounter = gridStatus.Rows.Count - 1;
                        gridStatus.Rows.Add();
                        gridStatus.Rows[globalCounter].Cells[0].Value = item.Name;
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

        private void RunGroup_Load(object sender, EventArgs e)
        {
            InitLanguage();
        }

        private void InitLanguage()
        {
            this.Text = Strings.RunGroup;
            materialLabel2.Text = Strings.InitiateWhatsAppScaneQRCodefromyourrmobile;
            btnInitWA.Text = Strings.ClicktoInitiate;
            label5.Text = Strings.Status;
            btnSTart.Text = Strings.Start;
            label7.Text = Strings.Status;
            label8.Text = Strings.Log;
            gridStatus.Columns[0].HeaderText = Strings.ChatName;
            gridStatus.Columns[1].HeaderText = Strings.Status;
            label4.Text = Strings.ImportentNotes;
            label1.Text = Strings.Keepapplicationopenwhilesendingmessagesanduntilallmessagesaresentfromyourmobile;
            label2.Text = Strings.ClearWhatsAppchathistoryafter5001000150020000messagesasperyourphoneconfiguration;
            label3.Text = Strings.WaSendertendstosubmitmessagestoyourphoneisnotresponsiblefordeliveryofthemessage;
        }

    }
}
