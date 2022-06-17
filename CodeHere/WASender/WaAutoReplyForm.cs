using MaterialSkin.Controls;
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
using WaAutoReplyBot.enums;
using WaAutoReplyBot.Models;
using WASender;
using WASender.enums;
using WASender;

namespace WaAutoReplyBot
{
    public partial class WaAutoReplyForm : MyMaterialForm
    {
        List<RuleTransactionModel> ruleTransactionModelList;
        DataTable dtEmp;
        InitStatusEnum initStatusEnum;
        IWebDriver driver;
        System.Windows.Forms.Timer timerInitChecker;
        BackgroundWorker worker;
        private static bool IsRunning = false;
        private static string strLog = "";
        System.Windows.Forms.Timer timerRunner;
        WaSenderForm WaSenderForm;
        Logger logger;

        public WaAutoReplyForm(WaSenderForm _WaSenderForm)
        {
            logger = new Logger("AutoReplyBot");
            WaSenderForm = _WaSenderForm;
            InitializeComponent();
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
            txtLog.Text = strLog;
        }

        public static void WriteLog(string msg)
        {
            strLog = msg + Environment.NewLine + strLog;
        }

        private void materialButton2_Click(object sender, EventArgs e)
        {
            AddRule addRule = new AddRule(new RuleTransactionModel(), this);
            addRule.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            init();
            initLanguage();
        }

        private void initLanguage()
        {
            this.Text = Strings.WhatsAppBot;
            materialLabel1.Text = Strings.Rules;
            materialButton2.Text = Strings.AddRule;
            label7.Text = Strings.Status;
            materialLabel2.Text = Strings.Log;
            materialButton1.Text = Strings.Start;
            materialButton4.Text = Strings.Stop;
        }

        private void init()
        {
            ruleTransactionModelList = new List<RuleTransactionModel>();
            dtEmp = new DataTable();
            dtEmp.Columns.Add("IsActive", typeof(bool));
            dtEmp.Columns.Add("User Input", typeof(string));
            dtEmp.Columns.Add("Type", typeof(string));
            dtEmp.Columns.Add("Messages", typeof(string));
            gridRulesets.DataSource = dtEmp;
            gridRulesets.Columns[0].Width = 60;
            gridRulesets.Columns[0].ReadOnly = false;
            gridRulesets.Columns[1].ReadOnly = true;
            gridRulesets.Columns[2].ReadOnly = true;
            gridRulesets.Columns[3].ReadOnly = true;

            string ObjData = Newtonsoft.Json.JsonConvert.SerializeObject(this.ruleTransactionModelList);
            String FolderPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            String path = Path.Combine(FolderPath, "WaAutoreplyRules.json");
            if (File.Exists(path))
            {
                string text = File.ReadAllText(path);
                var tempruleTransactionModelList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<RuleTransactionModel>>(text);

                foreach (RuleTransactionModel ruleTransactionModel in tempruleTransactionModelList.ToList())
                {
                    AddRuleTRansaction(ruleTransactionModel);
                }
            }
        }

        public void RemoveItem()
        {
            var ss = gridRulesets.CurrentCell.RowIndex;
            this.ruleTransactionModelList.RemoveAt(ss);
            dtEmp.Rows.RemoveAt(ss);
        }

        public void HandleChieldEditMode()
        {
            try
            {
                var ss = gridRulesets.CurrentCell.RowIndex;
                this.ruleTransactionModelList[ss].IsEditMode = false;
            }
            catch (Exception ex)
            {

            }
        }

        public void AddRuleTRansaction(RuleTransactionModel _ruleTransactionModel, bool addtoTrans = true)
        {
            if (_ruleTransactionModel.IsEditMode != true)
            {
                if (addtoTrans == true)
                {
                    ruleTransactionModelList.Add(_ruleTransactionModel);
                }
                dtEmp.Rows.Add(true, _ruleTransactionModel.userInput, _ruleTransactionModel.operatorsEnum, _ruleTransactionModel.messages.Count());
            }
            else
            {
                var ss = gridRulesets.CurrentCell.RowIndex;
                this.ruleTransactionModelList[ss] = _ruleTransactionModel;
                this.ruleTransactionModelList[ss].IsEditMode = false;
                dtEmp.Rows[ss][1] = this.ruleTransactionModelList[ss].userInput;
                dtEmp.Rows[ss][2] = this.ruleTransactionModelList[ss].operatorsEnum;
                dtEmp.Rows[ss][3] = this.ruleTransactionModelList[ss].messages.Count();
            }
        }

        private void gridRulesets_DoubleClick(object sender, EventArgs e)
        {
            var ss = gridRulesets.CurrentCell.RowIndex;
            this.ruleTransactionModelList[ss].IsEditMode = true;
            AddRule addRule = new AddRule(this.ruleTransactionModelList[ss], this);
            addRule.ShowDialog();
        }

        private void ChangeInitStatus(InitStatusEnum _initStatus)
        {
            this.initStatusEnum = _initStatus;
            AutomationCommon.ChangeInitStatus(_initStatus, lblInitStatus);
            if (_initStatus == InitStatusEnum.Initialised || _initStatus == InitStatusEnum.Initialising || _initStatus == InitStatusEnum.Started)
            {
                materialCard1.Enabled = false;
            }
            else
            {
                materialCard1.Enabled = true;
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
                    ChangeInitStatus(InitStatusEnum.Initialised);
                    timerInitChecker.Stop();
                    initBackgroundWorker();
                    Activate();
                    WaAutoReplyForm.IsRunning = true;
                    worker.RunWorkerAsync();
                    initTimer();
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
            ChangeInitStatus(InitStatusEnum.Started);
        }


        private List<RuleTransactionModel> ExactStartsWith(List<RuleTransactionModel> _RuleTransactionModelList, string userInput)
        {
            List<RuleTransactionModel> matchedRules = new List<RuleTransactionModel>();
            foreach (var item in _RuleTransactionModelList)
            {
                if (userInput.ToUpper().StartsWith(item.userInput.ToUpper()))
                {
                    matchedRules.Add(item);
                }
            }
            return matchedRules;
        }

        private List<RuleTransactionModel> ExactEndsWith(List<RuleTransactionModel> _RuleTransactionModelList, string userInput)
        {
            List<RuleTransactionModel> matchedRules = new List<RuleTransactionModel>();
            foreach (var item in _RuleTransactionModelList)
            {
                if (userInput.ToUpper().EndsWith(item.userInput.ToUpper()))
                {
                    matchedRules.Add(item);
                }
            }
            return matchedRules;
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            List<RuleTransactionModel> exactMessageList = this.ruleTransactionModelList.Where(x => x.operatorsEnum == OperatorsEnum.Exact).ToList();
            List<RuleTransactionModel> containsMessageList = this.ruleTransactionModelList.Where(x => x.operatorsEnum == OperatorsEnum.Contains).ToList();
            List<RuleTransactionModel> startFromMessageList = this.ruleTransactionModelList.Where(x => x.operatorsEnum == OperatorsEnum.StartFrom).ToList();
            List<RuleTransactionModel> endsWithFromMessageList = this.ruleTransactionModelList.Where(x => x.operatorsEnum == OperatorsEnum.EndsWith).ToList();
            List<RuleTransactionModel> fallbackMesageList = this.ruleTransactionModelList.Where(x => x.IsFallBack == true).ToList();

            while (WaAutoReplyForm.IsRunning == true)
            {

                Thread.Sleep(1000);
                try
                {
                    AutomationCommon.checkConnection(driver);
                    By unreadmessage = By.ClassName("_1pJ9J");

                    By messageby = By.ClassName("_2wUmf");

                    if (AutomationCommon.IsElementPresent(unreadmessage, driver) || AutomationCommon.IsElementPresent(messageby, driver))
                    {
                        logger.WriteLog("New UNread Found");
                        By unreadmessageHolder = By.ClassName("_1pJ9J");

                        if (AutomationCommon.IsElementPresent(unreadmessageHolder, driver) || AutomationCommon.IsElementPresent(messageby, driver))
                        {
                            bool IsGroup = false;
                            if (AutomationCommon.IsElementPresent(messageby, driver))
                            {
                                var lastMessage = driver.FindElements(messageby);
                                try
                                {
                                    var lm = lastMessage[lastMessage.Count() - 1];
                                    string sdds = lm.GetAttribute("class");
                                    if (sdds.Contains("message-in"))
                                    {
                                        try
                                        {
                                            var SenderName = driver.FindElements(By.ClassName("a71At"));
                                            if (SenderName.Count() == 0)
                                            {
                                                IsGroup = false;
                                            }
                                            else
                                            {
                                                IsGroup = true;
                                            }
                                        }
                                        catch (Exception ex)
                                        {

                                        }

                                    }
                                    else
                                    {
                                        IsGroup = true;
                                    }
                                }
                                catch (Exception ex)
                                {

                                }
                            }
                            if (AutomationCommon.IsElementPresent(unreadmessageHolder, driver))
                            {
                                IWebElement UnreadChat = driver.FindElement(unreadmessageHolder).FindElement(By.XPath("..")).FindElement(By.XPath("..")).FindElement(By.XPath("..")).FindElement(By.XPath("..")).FindElement(By.XPath(".."));

                                string UnreadChatHtml = UnreadChat.GetAttribute("innerHTML");


                                if (UnreadChatHtml.Contains("data-testid=\"default-group\""))
                                {
                                    IsGroup = true;
                                    logger.WriteLog("Is Group");
                                }

                                if (AutomationCommon.IsElementPresent(unreadmessageHolder, driver))
                                {


                                    UnreadChat.Click();
                                }

                            }

                            logger.WriteLog("_1pJ9J Found");
                            


                            if (IsGroup == false)
                            {
                                logger.WriteLog("Not Group");
                                var messageins = driver.FindElements(By.ClassName("message-in"));
                                try
                                {
                                    string lastMessageText = messageins[messageins.Count() - 1].FindElement(By.ClassName("selectable-text")).Text;
                                    logger.WriteLog("lastMessageText = " + lastMessageText);
                                    if (ExactStartsWith(startFromMessageList, lastMessageText).Count() > 0)
                                    {
                                        logger.WriteLog("Match with ExactStartsWith");
                                        RuleTransactionModel model = ExactStartsWith(startFromMessageList, lastMessageText).LastOrDefault();
                                        sendMessage(model);
                                    }
                                    else if (ExactEndsWith(endsWithFromMessageList, lastMessageText).Count() > 0)
                                    {
                                        logger.WriteLog("Match with ExactEndsWith");
                                        RuleTransactionModel model = ExactEndsWith(endsWithFromMessageList, lastMessageText).LastOrDefault();
                                        sendMessage(model);
                                    }
                                    else if (exactMessageList.Where(x => x.userInput.ToUpper() == lastMessageText.ToUpper()).Count() > 0)
                                    {
                                        logger.WriteLog("Match with exactMessage");
                                        RuleTransactionModel model = exactMessageList.Where(x => x.userInput.ToUpper() == lastMessageText.ToUpper()).LastOrDefault();
                                        sendMessage(model);
                                    }
                                    else if (containsMessageList.Where(x => x.userInput.ToUpper().Contains(lastMessageText.ToUpper())).Count() > 0)
                                    {
                                        logger.WriteLog("Match with contains");
                                        RuleTransactionModel model = containsMessageList.Where(x => x.userInput.ToUpper().Contains(lastMessageText.ToUpper())).LastOrDefault();
                                        sendMessage(model);
                                    }
                                    else
                                    {
                                        // if its not group  
                                        logger.WriteLog("Fallback");
                                        RuleTransactionModel model = fallbackMesageList[Utils.getRandom(0, fallbackMesageList.Count() - 1)];
                                        sendMessage(model);
                                    }

                                }
                                catch (Exception ex)
                                {
                                    logger.WriteLog(ex.Message);
                                    if (fallbackMesageList.Count() > 0)
                                    {
                                        logger.WriteLog("Fallback Found");
                                        RuleTransactionModel model = fallbackMesageList[Utils.getRandom(0, fallbackMesageList.Count() - 1)];
                                        sendMessage(model);
                                    }
                                    else
                                    {
                                        logger.WriteLog("Fallback not found");
                                    }
                                }

                               
                            }

                            try
                            {

                                IWebElement menu = driver.FindElement(By.XPath("//*[@id='main']/header/div[3]/div/div[2]/div/div | //*[@id='main']/header/div[3]/div/div[3]/div/div/span"));
                                menu.Click();
                                Thread.Sleep(500);
                                logger.WriteLog("Menu Clicked");

                                IWebElement menuHolder = driver.FindElement(By.CssSelector("[data-testid='contact-menu-dropdown']"));

                                var lis = menuHolder.FindElements(By.ClassName("_2qR8G"));
                                logger.WriteLog("lis.Count()= " + lis.Count());

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
                                    logger.WriteLog("Window Reloaded");
                                }
                                By prifileBy = By.XPath("//*[@class='_1PzAL']");
                                if (AutomationCommon.IsElementPresent(prifileBy, driver))
                                {
                                    IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                                    js.ExecuteScript("window.location.href=''");
                                    logger.WriteLog("prifileBy found");
                                }
                                else
                                {
                                    logger.WriteLog("prifileBy not found");
                                }
                            }
                            catch (Exception Ex)
                            {
                                logger.WriteLog(Ex.Message);
                            }

                        }

                    }
                }
                catch (Exception ex)
                {
                    try
                    {

                        IWebElement menu = driver.FindElement(By.XPath("//*[@id='main']/header/div[3]/div/div[2]/div/div | //*[@id='main']/header/div[3]/div/div[3]/div/div/span"));
                        menu.Click();
                        Thread.Sleep(500);
                        logger.WriteLog("Menu Clicked");

                        IWebElement menuHolder = driver.FindElement(By.CssSelector("[data-testid='contact-menu-dropdown']"));

                        var lis = menuHolder.FindElements(By.ClassName("_2qR8G"));
                        logger.WriteLog("lis.Count()= " + lis.Count());

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
                            logger.WriteLog("Window Reloaded");
                        }
                        By prifileBy = By.XPath("//*[@class='_1PzAL']");
                        if (AutomationCommon.IsElementPresent(prifileBy, driver))
                        {
                            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                            js.ExecuteScript("window.location.href=''");
                            logger.WriteLog("prifileBy found");
                        }
                        else
                        {
                            logger.WriteLog("prifileBy not found");
                        }
                    }
                    catch (Exception Ex)
                    {
                        logger.WriteLog(Ex.Message);
                    }
                }
            }
        }

        private void sendMessage(RuleTransactionModel model)
        {
            Thread.Sleep(1000);
            By TextBoxBy = By.XPath("//*[@id='main']/footer/div[1]/div/span[2]/div/div[2]/div[1]/div/div[2]");
            IWebElement el = driver.FindElement(By.XPath("//*[@id=\"main\"]/footer/div[1]/div/span[2]/div/div[2]/div[1]/div/div[2]"));

            IWebElement messageTextBox = driver.FindElement(TextBoxBy);

            logger.WriteLog("Sending Reply in");
            By DisappearingmessagesBy = By.ClassName("_1N4rE");
            if (AutomationCommon.IsElementPresent(DisappearingmessagesBy, driver))
            {
                logger.WriteLog("Is Disappearingmessages");
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

            int random = Utils.getRandom(0, model.messages.Count());

            var mesageModel = model.messages[random];

            logger.WriteLog("random = " + random);


            int fileCntr = 0;
            IWebElement image = null;
            if (mesageModel.Files.Count > 0)
            {
                driver.FindElement(By.XPath("//*[@id='main']/footer/div[1]/div/span[2]/div/div[1]/div[2]/div")).Click();
                image = driver.FindElement(By.XPath("//*[@id='main']/footer/div[1]/div/span[2]/div/div[1]/div[2]/div/span/div[1]/div/ul/li[1]/button/input"));
            }
            string filesString = "";
            foreach (var file in mesageModel.Files)
            {
                if (filesString != "")
                {
                    filesString += "\n" + file;
                }
                else
                {
                    filesString += file;
                }
                fileCntr++;
            }

            if (filesString != "")
            {
                image.SendKeys(filesString);
                Thread.Sleep(TimeSpan.FromSeconds(2));
                driver.FindElement(By.XPath("//*[@id='app']/div[1]/div[1]/div[2]/div[2]/span/div[1]/span/div[1]/div/div[2]/div/div[2]/div[2]/div/div")).Click();
                Thread.Sleep(500);
            }


            var messages = mesageModel.LongMessage.Split('\n');
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

            Invoke((Action)(() => { Clipboard.SetText(NewMessage); }));
            try
            {
                messageTextBox.SendKeys(OpenQA.Selenium.Keys.Control + "v");
            }
            catch (Exception x)
            {
                try
                {
                    Invoke((Action)(() => { Clipboard.SetText(NewMessage); }));
                    el.SendKeys(OpenQA.Selenium.Keys.Control + "v");

                    //el.SendKeys("dd"); 

                }
                catch (Exception ex)
                {

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



            AutomationCommon.checkConnection(driver);

            logger.WriteLog("sendButton Click");

            By usernameBy = By.ClassName("_21nHd");
            string userName = "";
            if (AutomationCommon.IsElementPresent(usernameBy, driver))
            {
                userName = driver.FindElement(usernameBy).Text;
            }
            if (!model.IsFallBack)
            {
                WaAutoReplyForm.WriteLog(userName + "\t-- " + Strings.Match + " : \t" + model.operatorsEnum + "\t-- " + Strings.ReplySend + "!");
            }
            else
            {
                WaAutoReplyForm.WriteLog(userName + "\t-- " + Strings.NotMatch + " :\t --" + Strings.Fallback + " \t--" + Strings.ReplySend + "!");
            }


            logger.WriteLog("Reply Sent");
        }




        private void materialButton1_Click(object sender, EventArgs e)
        {
            if (!(ruleTransactionModelList.Count() > 0))
            {
                MaterialSnackBar SnackBarMessage = new MaterialSnackBar(Strings.PleaseaddRules, Strings.OK, true);
                SnackBarMessage.Show(this);
            }
            else
            {
                ChangeInitStatus(InitStatusEnum.Initialising);
                try
                {
                    Config.KillChromeDriverProcess();
                    //ChromeOptions options = new ChromeOptions();
                    //options.AddExcludedArgument("enable-automation");
                    //options.AddAdditionalCapability("useAutomationExtension", false);
                    //options.AddArguments("--profile-directory=Default");
                    //options.AddArguments("--user-data-dir=C:/Users/Admin/AppData/Local/Google/Chrome/User Data");

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
        }

        private void materialButton4_Click(object sender, EventArgs e)
        {
            if (initStatusEnum == InitStatusEnum.Initialised || initStatusEnum == InitStatusEnum.Started)
            {
                try
                {
                    WaAutoReplyForm.IsRunning = false;
                    timerRunner.Stop();
                    worker.CancelAsync();
                    driver.Close();
                    driver.Quit();
                }
                catch (Exception Ex)
                {

                }
                ChangeInitStatus(InitStatusEnum.Stopped);

            }

        }

        private void WaAutoReplyForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            string ObjData = Newtonsoft.Json.JsonConvert.SerializeObject(this.ruleTransactionModelList);
            String FolderPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            String path = Path.Combine(FolderPath, "WaAutoreplyRules.json");
            if (!File.Exists(path))
            {
                File.Create(path).Close();
            }
            File.WriteAllText(path, ObjData);
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
            logger.Complete();
        }

        private void WaAutoReplyForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            WaSenderForm.formReturn(true);
        }
    }
}
