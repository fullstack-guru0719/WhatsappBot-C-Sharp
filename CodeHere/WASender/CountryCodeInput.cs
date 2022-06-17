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
    public partial class CountryCodeInput : MyMaterialPopOp
    {
        WaSenderForm waSenderForm;
        public CountryCodeInput(WaSenderForm _WaSenderForm)
        {
            waSenderForm = _WaSenderForm;
            InitializeComponent();

            init();
        }

        private void init()
        {
            this.Text = Strings.EnterCountryCode;
            materialButton1.Text = Strings.OK;
        }

        private void materialButton1_Click(object sender, EventArgs e)
        {
            try
            {
                int cc = Convert.ToInt32(materialMaskedTextBox1.Text);
                waSenderForm.CountryCOdeAdded(materialMaskedTextBox1.Text);
                this.Close();
            }
            catch (Exception ex)
            {
                
            }
            
        }
    }
}
