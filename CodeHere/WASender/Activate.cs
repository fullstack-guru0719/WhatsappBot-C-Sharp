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
using WaAutoReplyBot;

namespace WASender
{
    public partial class Activate : MyMaterialPopOp
    {

        Logger logger;
        WaSenderForm waSenderForm;

        public Activate(WaSenderForm _waSenderForm)
        {
            this.waSenderForm = _waSenderForm;
            InitializeComponent();
            logger = new Logger("Activator");
        }

        private void Activate_Load(object sender, EventArgs e)
        {
            init();
        }

        private void init()
        {
            this.Text = Strings.ActivateAppName;
            lblActivationCode.Text = Strings.YourActivationCodeis;
            label1.Text = Strings.ProvideYourActivationKeyHere;
            btnActivate.Text = Strings.ActivateNow;
            logger.WriteLog("FingerPrint_Value=" + Security.FingerPrint.Value());
            txtActivationCode.Text = Security.FingerPrint.Value();
            materialButton1.Text = Strings.Exit;
        }

        private void materialButton1_Click(object sender, EventArgs e)
        {
            logger.Complete();
            Environment.Exit(1);
        }

        private void btnActivate_Click(object sender, EventArgs e)
        {
            try
            {
                string jsonstring = Config.Base64Decode(txtKey.Text);
                WASender.Models.ActivationModel obj = Newtonsoft.Json.JsonConvert.DeserializeObject<WASender.Models.ActivationModel>(jsonstring);

                if (txtActivationCode.Text == Config.Base64Decode(obj.ActivationCode))
                {
                    if (obj.EndDate < DateTime.Now)
                    {
                        MaterialSnackBar SnackBarMessage = new MaterialSnackBar(Strings.InvalidActivationKey, Strings.OK, true);
                        SnackBarMessage.Show(this);
                    }
                    else
                    {
                        MaterialSnackBar SnackBarMessage = new MaterialSnackBar(Strings.ActivationSuccessfull, Strings.OK, true);
                        SnackBarMessage.Show(this);
                        var NewjsonString = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
                        Config.ActivateProduct(Config.Base64Encode(NewjsonString));
                        this.Hide();
                        waSenderForm.Show();
                        logger.Complete();
                    }

                }
                else
                {
                    MaterialSnackBar SnackBarMessage = new MaterialSnackBar(Strings.InvalidActivationKey, Strings.OK, true);
                    SnackBarMessage.Show(this);
                }
            }
            catch (Exception ex)
            {
                MaterialSnackBar SnackBarMessage = new MaterialSnackBar(Strings.InvalidActivationKey, Strings.OK, true);
                SnackBarMessage.Show(this);
            }

            //if (txtActivationCode.Text == Config.Base64Decode(txtKey.Text))
            //{
            //    MaterialSnackBar SnackBarMessage = new MaterialSnackBar(Strings.ActivationSuccessfull, Strings.OK, true);
            //    SnackBarMessage.Show(this);
            //    Config.ActivateProduct(txtKey.Text);
            //    this.Hide();
            //}
            //else
            //{
            //    MaterialSnackBar SnackBarMessage = new MaterialSnackBar(Strings.InvalidActivationKey, Strings.OK, true);
            //    SnackBarMessage.Show(this);
            //}
        }

        

        private void Activate_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(1);
            logger.Complete();
        }
    }
}
