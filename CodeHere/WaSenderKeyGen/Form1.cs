using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //txtKey.Text = Base64Encode(txtActivationCode.Text);
            try
            {
                ActivationModel activationModel = new ActivationModel();
                activationModel.ActivationCode = Base64Encode(txtActivationCode.Text);
                activationModel.validDays = Convert.ToInt32(txtDays.Text);
                activationModel.StartDate = DateTime.Now;
                activationModel.EndDate = activationModel.StartDate;
                activationModel.EndDate=activationModel.EndDate.AddDays(activationModel.validDays);
                string jsonval = Newtonsoft.Json.JsonConvert.SerializeObject(activationModel);
                txtKey.Text = Base64Encode(jsonval);

            }
            catch (Exception ex)
            {


            }


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //txtActivationCode.Text = Security.FingerPrint.Value();



        }


        public string Base64Encode(string plainText)
        {



            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        class ActivationModel
        {

            public string ActivationCode { get; set; }
            public int validDays { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
        }
    }
}
