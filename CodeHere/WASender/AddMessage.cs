using FluentValidation.Results;
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
using WaAutoReplyBot.Models;
using WaAutoReplyBot.Validators;
using WASender;

namespace WaAutoReplyBot
{
    public partial class AddMessage : MyMaterialPopOp
    {
        MessageModel messageModel;
        AddRule addRule;
        public AddMessage(MessageModel _messageModel, AddRule _addRule)
        {
            messageModel = _messageModel;
            addRule = _addRule;
            InitializeComponent();
        }

        private void materialButton3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void materialButton1_Click(object sender, EventArgs e)
        {
            Utils.selectFileForMessage(lstViewFiles);
        }

        private void lstViewFiles_KeyDown(object sender, KeyEventArgs e)
        {
            Utils.removeListViewItem(e, lstViewFiles);
        }

        private void materialButton2_Click(object sender, EventArgs e)
        {
            this.messageModel.LongMessage = txtLongMessage.Text;
            this.messageModel.Files = new List<string>();
            

            foreach (ListViewItem item in lstViewFiles.Items)
            {
                this.messageModel.Files.Add(item.Text);        
            }

            List<ValidationFailure> errors = new MessageModelValidator().Validate(this.messageModel).Errors.ToList();
            if (errors.Count() > 0)
            {
                MaterialSnackBar SnackBarMessage = new MaterialSnackBar(errors[0].ErrorMessage, Strings.OK, true);
                SnackBarMessage.Show(this);
            }
            else
            {
                this.addRule.AddNewMesage(this.messageModel);
                this.Close();
            }

        }

        private void AddMessage_Load(object sender, EventArgs e)
        {
            init();
            initLanguage();
        }

        private void initLanguage()
        {
            this.Text = Strings.ReplyMessage;
            txtLongMessage.Hint = Strings.TypeYourMessagehere;
            materialButton1.Text= Strings.Addfile;
            materialButton3.Text = Strings.Cancel;
            materialButton2.Text = Strings.Add;
            lstViewFiles.Columns[0].Text = Strings.Files;
        }

        private void init()
        {
            this.txtLongMessage.Text = this.messageModel.LongMessage;
            if (this.messageModel.Files == null)
                this.messageModel.Files = new List<string>();

            foreach (var item in this.messageModel.Files)
            {
                lstViewFiles.Items.Add(item);
            }
        }

        private void AddMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        
    }
}
