using MaterialSkin;
using MaterialSkin.Controls;
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
using WASender.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Diagnostics;
using System.Threading;

namespace WASender
{
    public partial class RunSingle : MaterialForm
    {

        MaterialSkin.MaterialSkinManager materialSkinManager;
        WASenderSingleTransModel wASenderSingleTransModel;
        WaSenderForm waSenderForm;
        InitStatusEnum initStatusEnum;
        CampaignStatusEnum campaignStatusEnum;
        IWebDriver driver;
        System.Windows.Forms.Timer timerInitChecker;
        System.Windows.Forms.Timer timerRunner;
        BackgroundWorker worker;


        public RunSingle(WASenderSingleTransModel _wASenderSingleTransModel, WaSenderForm _waSenderForm)
        {
            InitializeComponent();
            materialSkinManager = MaterialSkin.MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkin.MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Green700, Primary.Green400, Primary.Blue900, Accent.Green700, TextShade.WHITE);

            this.waSenderForm = _waSenderForm;
            this.wASenderSingleTransModel = _wASenderSingleTransModel;
            this.Text = _wASenderSingleTransModel.CampaignName;
        }

        private void RunForm_FormClosed(object sender, FormClosedEventArgs e)
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

        private void RunSingle_Load(object sender, EventArgs e)
        {
            init();

            initLanguage();
        }

        private void initLanguage()
        {
            this.Text = Strings.Run;
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



        private void btnInitWA_Click(object sender, EventArgs e)
        {
            ChangeInitStatus(InitStatusEnum.Initialising);
            try
            {
                Config.KillChromeDriverProcess();
                //ChromeOptions options = new ChromeOptions();
                //options.AddExcludedArgument("enable-automation");
                //options.AddAdditionalCapability("useAutomationExtension", false);
                //options.AddArgument("user-data-dir=" + Config.GetChromeProfileFolder()); 
                var chromeDriverService = ChromeDriverService.CreateDefaultService();
                chromeDriverService.HideCommandPromptWindow = true;


                driver = new ChromeDriver(chromeDriverService, Config.GetChromeOptions());
                driver.Url = "https://web.whatsapp.com";

                checkQRScanDone();
            }
            catch (Exception ex)
            {
                ChangeInitStatus(InitStatusEnum.Unable);
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

        private static bool CanStopWaitingForDelevetry = false;

        [STAThread]
        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            int counter = 0;
            int totalCounter = 0;

            foreach (var item in wASenderSingleTransModel.contactList)
            {
                try
                {
                    By hrefHolder1 = By.ClassName("_2-IT7");
                    By hrefHolder2 = By.ClassName("_1y6Yk");

                    bool IsNumberValid = false;
                    try
                    {
                        Int64 sampleNumber = Convert.ToInt64(item.number);
                        IsNumberValid = true;
                    }
                    catch (Exception ex)
                    {

                    }
                    if (IsNumberValid == true)
                    {
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
                            CanStopWaitingForDelevetry = true;
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
                            CanStopWaitingForDelevetry = true;
                        }
                        else
                        {
                            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                            js.ExecuteScript("window.location.href='https://web.whatsapp.com/send?phone=" + item.number + "&text&app_absent=0'");
                            CanStopWaitingForDelevetry = false;
                        }
                    }
                    else
                    {
                        try
                        {
                            By BackButon = By.XPath("//*[@id=\"app\"]/div/div/div[2]/div[1]/span/div/span/div/header/div/div[1]/button/span");
                            if (AutomationCommon.IsElementPresent(BackButon, driver))
                            {
                                driver.FindElement(BackButon).Click();
                            }

                            By newchatBy = By.XPath("//*[@id=\"side\"]/header/div[2]/div/span/div[2]/div/span");
                            if (AutomationCommon.IsElementPresent(newchatBy, driver))
                            {
                                driver.FindElement(newchatBy).Click();
                                Thread.Sleep(300);

                                By searchChattxtBy = By.XPath("//*[@id=\"app\"]/div/div/div[2]/div[1]/span/div/span/div/div[1]/div/label/div/div[2] | //*[@id=\"app\"]/div/div/div[2]/div[1]/span/div/span/div/div[1]/div/div/div[2]/div/div[2]");


                                if (AutomationCommon.IsElementPresent(searchChattxtBy, driver))
                                {
                                    driver.FindElement(searchChattxtBy).SendKeys(item.number);
                                    Thread.Sleep(300);
                                    By searchResultHolderBy = By.XPath("//*[@id=\"app\"]/div/div/div[2]/div[1]/span/div/span/div/div[2]/div/div/div");
                                    if (AutomationCommon.IsElementPresent(searchResultHolderBy, driver))
                                    {
                                        IWebElement searchResultHolder = driver.FindElement(searchResultHolderBy);
                                        var ResultNames = searchResultHolder.FindElements(By.ClassName("ggj6brxn"));
                                        if (ResultNames.Count() > 0)
                                        {
                                            ResultNames[0].Click();

                                        }

                                    }
                                    else
                                    {
                                        // By BackButon = By.XPath("//*[@id=\"app\"]/div/div/div[2]/div[1]/span/div/span/div/header/div/div[1]/button/span");
                                        if (AutomationCommon.IsElementPresent(BackButon, driver))
                                        {
                                            driver.FindElement(BackButon).Click();
                                        }
                                    }

                                }
                            }
                        }
                        catch (Exception ex)
                        {

                        }
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


                        if (true == true)
                        {


                            By NumberInvalidBox = By.ClassName("_2Nr6U");
                            if (AutomationCommon.IsElementPresent(NumberInvalidBox, driver))
                            {
                                By OkButtonBy = By.ClassName("_20C5O");
                                if (AutomationCommon.IsElementPresent(OkButtonBy, driver))
                                {
                                    driver.FindElement(OkButtonBy).Click();


                                    item.sendStatusModel.isDone = true;
                                    item.sendStatusModel.sendStatusEnum = SendStatusEnum.ContactNotFound;
                                    counter++;
                                    totalCounter++;

                                    var _count = wASenderSingleTransModel.contactList.Count();
                                    var percentage = totalCounter * 100 / _count;
                                    worker.ReportProgress(percentage);
                                    continue;


                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {

                    }

                    AutomationCommon.checkConnection(driver);
                    IWebElement messageTextBox = AutomationCommon.WaitUntilElementVisible(driver, By.XPath("//*[@id=\"main\"]/footer/div[1]/div/span[2]/div/div[2]/div[1]/div/div[1]"), 10);


                    if (messageTextBox == null)
                    {
                        item.sendStatusModel.isDone = true;
                        item.sendStatusModel.sendStatusEnum = SendStatusEnum.ContactNotFound;
                        counter++;
                        totalCounter++;

                        var _count = wASenderSingleTransModel.contactList.Count();
                        var percentage = totalCounter * 100 / _count;
                        worker.ReportProgress(percentage);
                        continue;
                    }


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
                                        if (AutomationCommon.IsElementPresent(okButtonBy, Disappearingmessages)) ;
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

                    int n = Utils.getRandom(0, wASenderSingleTransModel.messages.Where(x => x != null).Count());

                    if (wASenderSingleTransModel.messages.Where(x => x != null).ToList().Count() < n)
                    {
                        n = wASenderSingleTransModel.messages.Where(x => x != null).ToList().Count();
                    }

                    MesageModel mesageModel = wASenderSingleTransModel.messages.Where(x => x != null).ToList()[n];


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

                        By sendFIleBy = By.XPath("//*[@id='app']/div[1]/div[1]/div[2]/div[2]/span/div[1]/span/div[1]/div/div[2]/div/div[2]/div[2]/div/div");

                        if (AutomationCommon.IsElementPresent(sendFIleBy, driver))
                        {
                            driver.FindElement(sendFIleBy).Click();
                        }
                        else
                        {
                            AutomationCommon.WaitUntilElementVisible(driver, sendFIleBy);
                            driver.FindElement(sendFIleBy).Click();
                        }


                        Thread.Sleep(500);
                    }



                    if (wASenderSingleTransModel.MessageSendingType == 2)
                    {
                        var messages = mesageModel.longMessage.Split('\n');

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

                                if (item.parameterModelList != null)
                                {
                                    foreach (var param in item.parameterModelList)
                                    {
                                        if (MsgLine.Contains("{{" + param.ParameterName + "}}"))
                                        {
                                            MsgLine = MsgLine.Replace("{{" + param.ParameterName + "}}", param.ParameterValue);
                                        }
                                    }
                                }


                                IWebElement el = driver.FindElement(By.XPath("//*[@id=\"main\"]/footer/div[1]/div/span[2]/div/div[2]/div[1]/div/div[2]"));
                                try
                                {
                                    var splitter = MsgLine.Trim().Split(' ');

                                    foreach (var _char in splitter)
                                    {
                                        try
                                        {
                                            messageTextBox.SendKeys(_char.ToString() + " ");
                                        }
                                        catch (Exception ex)
                                        {
                                            if (ex.Message.Contains("element not interactable"))
                                            {
                                                try
                                                {
                                                    el.SendKeys(_char.ToString() + " ");
                                                }
                                                catch (Exception exd)
                                                {
                                                    string new_char = _char.Replace("\r", "");
                                                    string script = "arguments[0].innerHTML += '" + new_char + "'";
                                                    ((IJavaScriptExecutor)driver).ExecuteScript(script, el);
                                                    Thread.Sleep(500);
                                                    el.SendKeys(OpenQA.Selenium.Keys.Control + OpenQA.Selenium.Keys.End);
                                                    Thread.Sleep(500);
                                                }
                                            }
                                            else
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

                                                }
                                            }

                                        }
                                    }
                                }
                                catch (Exception ex)
                                {

                                }



                                try
                                {
                                    messageTextBox.SendKeys(OpenQA.Selenium.Keys.Shift + '\n');
                                }
                                catch (Exception ex)
                                {
                                    if (ex.Message.Contains("element not interactable"))
                                    {
                                        el.SendKeys(OpenQA.Selenium.Keys.Shift + '\n');
                                    }
                                }
                            }
                        }
                    }
                    else if (wASenderSingleTransModel.MessageSendingType == 1)
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

                                if (item.parameterModelList != null)
                                {
                                    foreach (var param in item.parameterModelList)
                                    {
                                        if (MsgLine.Contains("{{" + param.ParameterName + "}}"))
                                        {
                                            MsgLine = MsgLine.Replace("{{" + param.ParameterName + "}}", param.ParameterValue);
                                        }
                                    }
                                }

                                MsgLine = MsgLine + "\n";
                                NewMessage = NewMessage + MsgLine;
                            }
                        }


                        Invoke((Action)(() => { Clipboard.SetText(NewMessage); }));
                        try
                        {
                            messageTextBox.SendKeys(OpenQA.Selenium.Keys.Control + "v");
                        }
                        catch (Exception x)
                        {
                            try
                            {
                                IWebElement el = driver.FindElement(By.XPath("//*[@id=\"main\"]/footer/div[1]/div/span[2]/div/div[2]/div[1]/div/div[2]"));
                                Invoke((Action)(() => { Clipboard.SetText(NewMessage); }));
                                el.SendKeys(OpenQA.Selenium.Keys.Control + "v");

                                //el.SendKeys("dd"); 

                            }
                            catch (Exception ex)
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


                    // if (AutomationCommon.IsElementPresent(By.ClassName("_4sWnG"), driver))
                    // {
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
                    // document.querySelector('[data-testid="send"]').click()

                    AutomationCommon.checkConnection(driver);
                    if (CanStopWaitingForDelevetry == false)
                    {
                        bool IsStillSending = AutomationCommon.IsElementPresent(By.XPath("//*[@data-testid=\"msg-time\"]"), driver);

                        if (IsStillSending == true)
                        {
                            AutomationCommon.WaitUntilElementDispose(driver, By.XPath("//*[@data-testid=\"msg-time\"]"), 100);
                        }
                    }
                    AutomationCommon.checkConnection(driver);
                    Thread.Sleep(500);
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

                    }

                    Thread.Sleep(Utils.getRandom(wASenderSingleTransModel.settings.delayAfterEveryMessageFrom * 1000, wASenderSingleTransModel.settings.delayAfterEveryMessageTo * 1000));
                    counter++;

                    if (wASenderSingleTransModel.settings.delayAfterMessages == counter)
                    {
                        counter = 0;
                        Thread.Sleep(Utils.getRandom(wASenderSingleTransModel.settings.delayAfterMessagesFrom * 1000, wASenderSingleTransModel.settings.delayAfterMessagesFrom * 1000));
                    }
                }
                catch (Exception exc)
                {
                    item.sendStatusModel.isDone = true;
                    item.sendStatusModel.sendStatusEnum = SendStatusEnum.Failed;
                }
                totalCounter++;

                var __count = wASenderSingleTransModel.contactList.Count();
                var _percentage = totalCounter * 100 / __count;
                item.sendStatusModel.isDone = true;
                item.sendStatusModel.sendStatusEnum = SendStatusEnum.Success;
                worker.ReportProgress(_percentage);

            }
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ChangeCampStatus(CampaignStatusEnum.Finish);
            AutomationCommon.GenerateReport(this.wASenderSingleTransModel);

        }

        private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            lblPersentage.Text = e.ProgressPercentage + "% " + Strings.Completed;
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
                foreach (var item in wASenderSingleTransModel.contactList)
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
