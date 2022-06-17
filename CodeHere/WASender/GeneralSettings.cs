using MaterialSkin.Controls;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WaAutoReplyBot;
using WASender.Models;
using System.Web.Script.Serialization;
using System.Collections;

namespace WASender
{
    public partial class GeneralSettings : MyMaterialPopOp
    {
        WaSenderForm waSenderForm;
        public GeneralSettings()
        {
            
            InitializeComponent();
            init();
        }

        private void init()
        {
            this.Name = Strings.GeneralSettings;
            this.txtChromePath.Hint = Strings.ChromeProfilePath;
            this.btnSave.Text = Strings.Save;

            getData();
        }

        private void getData()
        {
            String GetGeneralSettingsFilePath = Config.GetGeneralSettingsFilePath();

            if (File.Exists(GetGeneralSettingsFilePath))
            {
                string json= File.ReadAllText(GetGeneralSettingsFilePath);
                GeneralSettingsModel generalSettingsModel = JsonConvert.DeserializeObject<GeneralSettingsModel>(json);
                if (generalSettingsModel.ChromeProfilePath != null && generalSettingsModel.ChromeProfilePath != "")
                {
                    txtChromePath.Text = generalSettingsModel.ChromeProfilePath;
                }
            }
            

        }

        private void GeneralSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
           // waSenderForm.formReturn(true);
        }

        private void materialButton15_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            String GetGeneralSettingsFilePath = Config.GetGeneralSettingsFilePath();
           
            if (!File.Exists(GetGeneralSettingsFilePath))
            {
                File.Create(GetGeneralSettingsFilePath).Close();
            }
            


            GeneralSettingsModel generalSettingsModel = new GeneralSettingsModel();
            if (this.txtChromePath.Text != "")
            {

                if (!Directory.Exists(txtChromePath.Text))
                {
                    MaterialSnackBar SnackBarMessage = new MaterialSnackBar(Strings.InputPathisnotCorrectfolderpath, Strings.OK, true);
                    SnackBarMessage.Show(this);
                    return;
                }

                generalSettingsModel.ChromeProfilePath = txtChromePath.Text;
                string Json = JsonConvert.SerializeObject(generalSettingsModel,Formatting.Indented);

                File.WriteAllText(GetGeneralSettingsFilePath,Json);

                MaterialSnackBar SnackBarMessage1 = new MaterialSnackBar(Strings.SettingsSavedSuccessfully, Strings.OK, true);
                SnackBarMessage1.Show(this);
            }
        }

        private void materialButton1_Click(object sender, EventArgs e)
        {

            List<LanguageModel> languageModelList = new List<LanguageModel>();
            languageModelList.Add(new LanguageModel { name = "AppName", value = "Wa Sender 1.0" });
            languageModelList.Add(new LanguageModel { name = "Column1", value = "Column1" });
            languageModelList.Add(new LanguageModel { name = "Number", value = "Number" });
            languageModelList.Add(new LanguageModel { name = "Column2", value = "Column2" });
            languageModelList.Add(new LanguageModel { name = "DownloadSample", value = "Download Sample Excel" });


            languageModelList.Add(new LanguageModel { name = "UploadSampleExcel", value = "Upload Excel" });
            languageModelList.Add(new LanguageModel { name = "ContectSender", value = "Contect Sender" });
            languageModelList.Add(new LanguageModel { name = "GroupSender", value = "Group Sender" });
            languageModelList.Add(new LanguageModel { name = "Tools", value = "Tools" });
            languageModelList.Add(new LanguageModel { name = "Filedownloadedsuccessfully", value = "File downloaded successfully !" });
            languageModelList.Add(new LanguageModel { name = "WABulkSender", value = "WA Bulk Sender" });
            languageModelList.Add(new LanguageModel { name = "SelectExcel", value = "Select Excel" });
            languageModelList.Add(new LanguageModel { name = "PleaseselectExcelfilesformatonly", value = "Please select Excel files ( .xls ) format only" });
            languageModelList.Add(new LanguageModel { name = "AddNew", value = "Add New" });
            languageModelList.Add(new LanguageModel { name = "KeyMarkers", value = "Key Markers" });
            languageModelList.Add(new LanguageModel { name = "AddKeyMarker", value = "Add KeyMarker" });
            languageModelList.Add(new LanguageModel { name = "SaveNClose", value = "Save && Close" });

            languageModelList.Add(new LanguageModel { name = "KeyMarkerFormatinIncorrect", value = "Key Marker Format in Incorrect" });
            languageModelList.Add(new LanguageModel { name = "OK", value = "OK" });
            languageModelList.Add(new LanguageModel { name = "SelectedKeyMarkeralreadyExist", value = "Selected KeyMarker already Exist" });
            languageModelList.Add(new LanguageModel { name = "Delete", value = "Delete" });
            languageModelList.Add(new LanguageModel { name = "WrongKey", value = "Key Marker Starts with {{ KEY : " });
            languageModelList.Add(new LanguageModel { name = "Target", value = "Target" });
            languageModelList.Add(new LanguageModel { name = "Messages", value = "Messages" });
            languageModelList.Add(new LanguageModel { name = "MessageOne", value = "Message 1" });
            languageModelList.Add(new LanguageModel { name = "MessageTwo", value = "Message 2" });
            languageModelList.Add(new LanguageModel { name = "MessageThree", value = "Message 3" });
            languageModelList.Add(new LanguageModel { name = "MessageFour", value = "Message 4" });
            languageModelList.Add(new LanguageModel { name = "MessageFive", value = "Message 5" });
            languageModelList.Add(new LanguageModel { name = "Attachments", value = "Attachments" });
            languageModelList.Add(new LanguageModel { name = "Addfile", value = "Add file" });
            languageModelList.Add(new LanguageModel { name = "Yourfirstmessage", value = "* Your first message" });
            languageModelList.Add(new LanguageModel { name = "YourSecondmessage", value = "* Your Second message" });
            languageModelList.Add(new LanguageModel { name = "YourThirdmessage", value = "* Your Third message" });
            languageModelList.Add(new LanguageModel { name = "YourFourthmessage", value = "* Your Fourth message" });
            languageModelList.Add(new LanguageModel { name = "YourFifthmessage", value = "* Your Fifth message" });
            languageModelList.Add(new LanguageModel { name = "DelaySettings", value = "Delay Settings" });
            languageModelList.Add(new LanguageModel { name = "Wait", value = "Wait" });
            languageModelList.Add(new LanguageModel { name = "secondsafterevery", value = "seconds after every " });
            languageModelList.Add(new LanguageModel { name = "to", value = "to" });
            languageModelList.Add(new LanguageModel { name = "Clear", value = "Clear" });
            languageModelList.Add(new LanguageModel { name = "StartCampaign", value = "Start Campaign" });
            languageModelList.Add(new LanguageModel { name = "secondsbeforeeverymessage", value = "seconds before every message" });
            languageModelList.Add(new LanguageModel { name = "AddKeyMarkers", value = "Add KeyMarkers" });
            languageModelList.Add(new LanguageModel { name = "RandomNumber", value = "Random Number" });
            languageModelList.Add(new LanguageModel { name = "PleaseEnterCampaignName", value = "Please Enter Campaign Name" });
            languageModelList.Add(new LanguageModel { name = "CampaignName", value = "Campaign Name" });
            languageModelList.Add(new LanguageModel { name = "UntitledGroupCampaign", value = "Untitled Group Campaign" });
            languageModelList.Add(new LanguageModel { name = "UntitledCampaign", value = "Untitled Campaign" });

            languageModelList.Add(new LanguageModel { name = "RowNo", value = "Row No" });
            languageModelList.Add(new LanguageModel { name = "Errors", value = "Errors" });
            languageModelList.Add(new LanguageModel { name = "WhatsAppBot", value = "WhatsApp Bot" });
            languageModelList.Add(new LanguageModel { name = "Rules", value = "Rules" });
            languageModelList.Add(new LanguageModel { name = "AddRule", value = "Add Rule" });
            languageModelList.Add(new LanguageModel { name = "Log", value = "Log" });
            languageModelList.Add(new LanguageModel { name = "Status", value = "Status" });
            languageModelList.Add(new LanguageModel { name = "Start", value = "Start" });
            languageModelList.Add(new LanguageModel { name = "Stop", value = "Stop" });
            languageModelList.Add(new LanguageModel { name = "IsActive", value = "IsActive" });
            languageModelList.Add(new LanguageModel { name = "UserInput", value = "User Input" });
            languageModelList.Add(new LanguageModel { name = "Type", value = "Type" });
            languageModelList.Add(new LanguageModel { name = "Match", value = "Match" });
            languageModelList.Add(new LanguageModel { name = "ReplySend", value = "Reply Send " });
            languageModelList.Add(new LanguageModel { name = "NotMatch", value = "Not Match " });
            languageModelList.Add(new LanguageModel { name = "Fallback", value = "Fallback" });
            languageModelList.Add(new LanguageModel { name = "PleaseaddRules", value = "Please add Rules" });
            languageModelList.Add(new LanguageModel { name = "Run", value = "Run" });
            languageModelList.Add(new LanguageModel { name = "InitiateWhatsAppScaneQRCodefromyourrmobile", value = "Initiate WhatsApp && Scane QR Code from your mobile" });
            languageModelList.Add(new LanguageModel { name = "ClicktoInitiate", value = "Click to Initiate" });
            languageModelList.Add(new LanguageModel { name = "ChatName", value = "ChatName" });
            languageModelList.Add(new LanguageModel { name = "ImportentNotes", value = "Importent Notes" });
            languageModelList.Add(new LanguageModel { name = "Keepapplicationopenwhilesendingmessagesanduntilallmessagesaresentfromyourmobile", value = "1) Keep application open while sending messages and until all messages are sent from your mobile" });
            languageModelList.Add(new LanguageModel { name = "ClearWhatsAppchathistoryafter5001000150020000messagesasperyourphoneconfiguration", value = "2) Clear WhatsApp chat history after 500/1000/1500/20000 messages as per your phone configuration" });
            languageModelList.Add(new LanguageModel { name = "WaSendertendstosubmitmessagestoyourphoneisnotresponsiblefordeliveryofthemessage", value = "3) WaSender tends to submit messages to your phone, is not responsible for delivery of the message" });
            languageModelList.Add(new LanguageModel { name = "Completed", value = "Completed" });
            languageModelList.Add(new LanguageModel { name = "PleasefollowStepNo1FirstInitialiseWhatsapp", value = "Please follow Step No 1. First, Initialise Whatsapp" });
            languageModelList.Add(new LanguageModel { name = "Processisalreadyrunning", value = "Process is already running" });
            languageModelList.Add(new LanguageModel { name = "RunGroup", value = "Run Group" });
            languageModelList.Add(new LanguageModel { name = "KeyMarker", value = "KeyMarker" });
            languageModelList.Add(new LanguageModel { name = "Select", value = "Select" });
            languageModelList.Add(new LanguageModel { name = "GroupsJoiner", value = "Groups Joiner" });
            languageModelList.Add(new LanguageModel { name = "GroupJoin", value = "Group Join" });
            languageModelList.Add(new LanguageModel { name = "secondsbeforeeveryGroupJoin", value = " seconds before every Group Join" });
            languageModelList.Add(new LanguageModel { name = "StartJoining", value = "Start Joining" });
            languageModelList.Add(new LanguageModel { name = "GroupLink", value = "Group Link" });
            languageModelList.Add(new LanguageModel { name = "PleaseFollowStepnoThree", value = "Please Follow Step no 3" });
            languageModelList.Add(new LanguageModel { name = "GrabGroupLinks", value = "Grab Group Links" });
            languageModelList.Add(new LanguageModel { name = "OpenBrowser", value = "Open Browser" });
            languageModelList.Add(new LanguageModel { name = "Clickbellowbuttontoopenbrowser", value = "Click bellow button to open browser" });
            languageModelList.Add(new LanguageModel { name = "Navigatetoanywebsitewherelistedgrouplinkstheclickbellowbellowbuton", value = "Navigate to any website , where listed group links, the click bellow bellow buton" });
            languageModelList.Add(new LanguageModel { name = "StartGrabbing", value = "Start Grabbing" });
            languageModelList.Add(new LanguageModel { name = "NoGroupLinkfoundincurrentPage", value = "No Group Link found in current Page" });
            languageModelList.Add(new LanguageModel { name = "GrabChatList", value = "Grab Chat List" });
            languageModelList.Add(new LanguageModel { name = "InitiateWhatsAppScaneQRCodefromyourmobile", value = "Initiate WhatsApp && Scane QR Code from your mobile" });
            languageModelList.Add(new LanguageModel { name = "GetGroupMember", value = "Get Group Member" });
            languageModelList.Add(new LanguageModel { name = "OpenanyGroupchatClickbuttonbellow", value = "Open any Group chat && Click button bellow" });
            languageModelList.Add(new LanguageModel { name = "PleaseGotoanygroupchat", value = "Please Go to any group chat" });
            languageModelList.Add(new LanguageModel { name = "fallback", value = "fallback" });
            languageModelList.Add(new LanguageModel { name = "Usefallback", value = "Use fallback" });
            languageModelList.Add(new LanguageModel { name = "If", value = "If..." });
            languageModelList.Add(new LanguageModel { name = "UserSend", value = "User Send..." });
            languageModelList.Add(new LanguageModel { name = "Condition", value = "Condition" });
            languageModelList.Add(new LanguageModel { name = "Which", value = "Which" });
            languageModelList.Add(new LanguageModel { name = "ThenReplywith", value = "Then, Reply with..." });
            languageModelList.Add(new LanguageModel { name = "AddMessage", value = "Add Message" });
            languageModelList.Add(new LanguageModel { name = "Message", value = "Message" });
            languageModelList.Add(new LanguageModel { name = "Cancel", value = "Cancel" });
            languageModelList.Add(new LanguageModel { name = "Save", value = "Save" });
            languageModelList.Add(new LanguageModel { name = "Confirm", value = "Confirm" });
            languageModelList.Add(new LanguageModel { name = "AreyousuretodeletethisRule", value = "Are you sure to delete this Rule" });
            languageModelList.Add(new LanguageModel { name = "ReplyMessage", value = "Reply Message" });
            languageModelList.Add(new LanguageModel { name = "TypeYourMessagehere", value = "Type Your Message here" });
            languageModelList.Add(new LanguageModel { name = "Files", value = "Files" });
            languageModelList.Add(new LanguageModel { name = "Add", value = "Add" });
            languageModelList.Add(new LanguageModel { name = "GroupNames", value = "Group Names" });
            languageModelList.Add(new LanguageModel { name = "PleaseCheckMobileNumberyouhaveadded", value = "Please Check Mobile Number you have added" });
            languageModelList.Add(new LanguageModel { name = "ShouldNotbeempty", value = "Should Not be empty" });
            languageModelList.Add(new LanguageModel { name = "ContactNumberShouldNotbeEmpty", value = "Contact Number Should Not be empty" });
            languageModelList.Add(new LanguageModel { name = "MessageShouldNotbeEmpty", value = "Message Should Not be empty" });
            languageModelList.Add(new LanguageModel { name = "delayAfterMessagesShouldNotbeEmpty", value = "Delay After Messages Should Not be empty" });
            languageModelList.Add(new LanguageModel { name = "delayAfterMessagesFromShouldNotbeEmpty", value = "Delay After Messages From  Should Not be empty" });
            languageModelList.Add(new LanguageModel { name = "delayAfterMessagesTOShouldNotbeEmpty", value = "Delay After Messages To Should Not be empty" });
            languageModelList.Add(new LanguageModel { name = "delayAfterEveryMessageFromShouldNotbeEmpty", value = "Delay After Every Message From Should Not be empty" });
            languageModelList.Add(new LanguageModel { name = "delayAfterEveryMessageToShouldNotbeEmpty", value = "Delay AfterEvery Message To Should Not be empty" });
            languageModelList.Add(new LanguageModel { name = "ShouldGraterthenoero", value = "Should Grater then 0" });
            languageModelList.Add(new LanguageModel { name = "delayAfterMessages_ShouldGraterthenoero", value = "Delay After Messages Should Grater then 0" });
            languageModelList.Add(new LanguageModel { name = "delayAfterMessagesFrom_ShouldGraterthenoero", value = "Delay After Messages From  Should Grater then 0" });
            languageModelList.Add(new LanguageModel { name = "delayAfterMessagesTo_ShouldGraterthenoero", value = "Delay After Messages To Should Grater then 0" });
            languageModelList.Add(new LanguageModel { name = "delayAfterEveryMessageFrom_ShouldGraterthenoero", value = "Delay After Every Message From Should Grater then 0" });
            languageModelList.Add(new LanguageModel { name = "delayAfterEveryMessageTo_ShouldGraterthenoero", value = "Delay After Every Message To Should Grater then 0" });
            languageModelList.Add(new LanguageModel { name = "thetoamountismustbegraterthenstartingamount", value = "the 'to' amount is must be grater then starting amount" });
            languageModelList.Add(new LanguageModel { name = "UsermessageMustEmptyincaseoffallback", value = "User message Must Empty in case of fallback" });
            languageModelList.Add(new LanguageModel { name = "PleaseAddatleastoneMessage", value = "Please Add at least one Message" });
            languageModelList.Add(new LanguageModel { name = "PleaseEnterAmessage", value = "Please Enter A message " });
            languageModelList.Add(new LanguageModel { name = "PleaseaddatleastoneGroupinlist", value = "Please add at least one Group in list" });
            languageModelList.Add(new LanguageModel { name = "PleaseaddatleastoneContactinlist", value = "Please add at least one Contact in list" });
            languageModelList.Add(new LanguageModel { name = "ClickbellowButton", value = "1) Click bellow Button" });
            languageModelList.Add(new LanguageModel { name = "ScanQRCode", value = "2) Scan QR Code" });
            languageModelList.Add(new LanguageModel { name = "WaitfortheExcel", value = "3) Wait for the Excel" });
            languageModelList.Add(new LanguageModel { name = "Now", value = "Now" });
            languageModelList.Add(new LanguageModel { name = "GrabNow", value = "Grab Now" });
            languageModelList.Add(new LanguageModel { name = "GrabGroupMembers", value = "Grab Group Members" });
            languageModelList.Add(new LanguageModel { name = "ClickbellowButtonScanQRCode", value = "1) Click bellow Button Scan QR Code" });
            languageModelList.Add(new LanguageModel { name = "OpenAnyGroup", value = "2) Open Any Group" });
            languageModelList.Add(new LanguageModel { name = "GrabWhatsAppGroupLinksfromwebpage", value = "Grab WhatsApp Group Links from web page" });
            languageModelList.Add(new LanguageModel { name = "OpenAnywebpagewheregivengrouplinks", value = "2) Open Any web page where given group links" });
            languageModelList.Add(new LanguageModel { name = "ThenClickonSTARTButton", value = "3) Then Click on START Button" });
            languageModelList.Add(new LanguageModel { name = "AutoGroupJoiner", value = "Auto Group Joiner" });
            languageModelList.Add(new LanguageModel { name = "AddUploadGroupLinks", value = "1) Add / Upload Group Links" });
            languageModelList.Add(new LanguageModel { name = "ScanWAQRCode", value = "2) Scan WA QR Code" });
            languageModelList.Add(new LanguageModel { name = "StartNow", value = "Start Now" });
            languageModelList.Add(new LanguageModel { name = "WhatsAppAutoResponderBot", value = "WhatsApp Auto Responder Bot" });
            languageModelList.Add(new LanguageModel { name = "SetRules", value = "1) Set Rules" });
            languageModelList.Add(new LanguageModel { name = "AddReplyMessages", value = "2) Add Reply Messages" });
            languageModelList.Add(new LanguageModel { name = "GeneralSettings", value = "General Settings" });
            languageModelList.Add(new LanguageModel { name = "ChromeProfilePath", value = "Chrome Profile Path" });
            languageModelList.Add(new LanguageModel { name = "InputPathisnotCorrectfolderpath", value = "Input Path is not Correct folder path" });
            languageModelList.Add(new LanguageModel { name = "SettingsSavedSuccessfully", value = "Settings Saved Successfully" });
            languageModelList.Add(new LanguageModel { name = "MessageSendingType", value = "Message Sending Type" });
            languageModelList.Add(new LanguageModel { name = "CopyPaste", value = "Copy Paste" });
            languageModelList.Add(new LanguageModel { name = "Typeamessage", value = "Type a message" });
            languageModelList.Add(new LanguageModel { name = "Activate", value = "Activate " });
            languageModelList.Add(new LanguageModel { name = "ActivateAppName", value = "Activate" });
            languageModelList.Add(new LanguageModel { name = "YourActivationCodeis", value = "Your Activation Code is ..." });
            languageModelList.Add(new LanguageModel { name = "ProvideYourActivationKeyHere", value = "Provide Your Activation Key Here ..." });
            languageModelList.Add(new LanguageModel { name = "ActivateNow", value = "Activate Now" });
            languageModelList.Add(new LanguageModel { name = "Exit", value = "Exit" });
            languageModelList.Add(new LanguageModel { name = "ActivationSuccessfull", value = "Activation Successfull" });
            languageModelList.Add(new LanguageModel { name = "InvalidActivationKey", value = "Invalid Activation Key" });




            languageModelList.Add(new LanguageModel { name = "Name", value = "Name" });
            languageModelList.Add(new LanguageModel { name = "WhatsAppNumberFilter", value = "WhatsApp Number Filter" });
            languageModelList.Add(new LanguageModel { name = "AddUploadNumbers", value = "1) Add / Upload Numbers" });


            languageModelList.Add(new LanguageModel { name = "NumberCheck", value = "Number Check" });
            languageModelList.Add(new LanguageModel { name = "secondsbeforeeveryNumberCheck", value = "seconds before every Number Check" });
            languageModelList.Add(new LanguageModel { name = "StartChecking", value = "Start Checking" });
            languageModelList.Add(new LanguageModel { name = "ContactListGrabber", value = "Contact List Grabber" });
            languageModelList.Add(new LanguageModel { name = "HitGrabNowButton", value = "2) Hit Grab Now Button" });


            languageModelList.Add(new LanguageModel { name = "GrabActiveGroupMembers", value = "Grab Active Group Members" });
            languageModelList.Add(new LanguageModel { name = "TotalFound ", value = "Total Found" });
            languageModelList.Add(new LanguageModel { name = "Export", value = "Export" });
            languageModelList.Add(new LanguageModel { name = "Nothingtoexport", value = "Nothing to export" });
            foreach (var item in languageModelList)
            {
                item.value = TranslateText(item.value);
            }


            var json = JsonConvert.SerializeObject(languageModelList);

        }

        public string TranslateText(string input)
        {
            string url = String.Format
            ("https://translate.googleapis.com/translate_a/single?client=gtx&sl={0}&tl={1}&dt=t&q={2}",
             "en", "es", Uri.EscapeUriString(input));
            HttpClient httpClient = new HttpClient();
            string result = httpClient.GetStringAsync(url).Result;
            var jsonData = new JavaScriptSerializer().Deserialize<List<dynamic>>(result);
            var translationItems = jsonData[0];
            string translation = "";
            foreach (object item in translationItems)
            {
                IEnumerable translationLineObject = item as IEnumerable;
                IEnumerator translationLineString = translationLineObject.GetEnumerator();
                translationLineString.MoveNext();
                translation += string.Format(" {0}", Convert.ToString(translationLineString.Current));
            }
            if (translation.Length > 1) { translation = translation.Substring(1); };
            return translation;
        }

        private void materialButton2_Click(object sender, EventArgs e)
        {
            using (StreamReader r = new StreamReader("languages\\English.json"))
            {
                string GeneralSettingJson  = r.ReadToEnd();
                var dict = JsonConvert.DeserializeObject<List<WASender.Models.LanguageModel>>(GeneralSettingJson);

                foreach (var item in dict)
                {
                    item.value = TranslateText(item.value);
                }

                var json = JsonConvert.SerializeObject(dict);
            }
        }
    }
}
