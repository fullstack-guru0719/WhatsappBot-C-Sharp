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
using WaAutoReplyBot.enums;
using WaAutoReplyBot.Models;
using WaAutoReplyBot.Validators;
using FluentValidation.Results;
using WASender;

namespace WaAutoReplyBot
{
    public partial class AddRule : MyMaterialPopOp
    {
        RuleTransactionModel ruleTransactionModel;
        WaAutoReplyForm waAutoReplyForm;
        public AddRule(RuleTransactionModel _ruleTransactionModel, WaAutoReplyForm _waAutoReplyForm)
        {
            InitializeComponent();
            ruleTransactionModel = _ruleTransactionModel;
            waAutoReplyForm = _waAutoReplyForm;
            if (ruleTransactionModel.messages == null)
                ruleTransactionModel.messages = new List<MessageModel>();
        }

        private void materialButton1_Click(object sender, EventArgs e)
        {
            AddMessage addMessage = new AddMessage(new MessageModel(), this);
            addMessage.ShowDialog();
        }

        public void AddNewMesage(MessageModel _messageModel)
        {
            if (_messageModel.IsEditMode != true)
            {
                ListViewItem item = new ListViewItem(new[] { _messageModel.LongMessage, _messageModel.Files.Count().ToString() });
                lstMessages.Items.Add(item);
                ruleTransactionModel.messages.Add(_messageModel);
            }
            else
            {
                var ss = lstMessages.SelectedItems[0].Index;
                ListViewItem item = new ListViewItem(new[] { _messageModel.LongMessage, _messageModel.Files.Count().ToString() });
                lstMessages.Items[ss] = item;
            }
        }

        private void materialButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lstMessages_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                var removables = new List<string>();
                foreach (ListViewItem item in lstMessages.SelectedItems)
                {
                    removables.Add(item.Text);
                }

                foreach (var item in removables)
                {
                    var itemToRemove = ruleTransactionModel.messages.Single(r => r.LongMessage == item);
                    ruleTransactionModel.messages.Remove(itemToRemove);
                }
            }
            Utils.removeListViewItem(e, lstMessages);
        }

        private void AddRule_Load(object sender, EventArgs e)
        {
            init();
            InitLanguage();
        }

        private void InitLanguage()
        {
            this.Text = Strings.AddRule;
            this.materialLabel2.Text = Strings.If;
            this.materialCheckbox1.Text = Strings.Usefallback;
            this.txtUserInput.Hint = Strings.UserSend;
            this.materialLabel1.Text= Strings.Which;
            this.cboCondition.Hint= Strings.Condition;
            materialLabel3.Text = Strings.ThenReplywith;
            materialButton1.Text = Strings.AddMessage;

            lstMessages.Columns[0].Text = Strings.Message;
            lstMessages.Columns[1].Text = Strings.Attachments;

            materialButton2.Text = Strings.Cancel;
            btnDelete.Text = Strings.Delete;
            materialButton3.Text = Strings.Save;
        }

        private void init()
        {
            cboCondition.DataSource = Enum.GetValues(typeof(enums.OperatorsEnum));
            if (!ruleTransactionModel.IsFallBack)
            {
                txtUserInput.Text = ruleTransactionModel.userInput;
            }
            materialCheckbox1.Checked = ruleTransactionModel.IsFallBack;
            
            foreach (var _messageModel in ruleTransactionModel.messages)
            {
                ListViewItem Listitem = new ListViewItem(new[] { _messageModel.LongMessage, _messageModel.Files.Count().ToString() });
                lstMessages.Items.Add(Listitem);
            }
            if (ruleTransactionModel.IsEditMode)
            {
                btnDelete.Visible = true;
            }
            cboCondition.SelectedIndex = cboCondition.FindStringExact(ruleTransactionModel.operatorsEnum.ToString());
        }

        private void materialButton3_Click(object sender, EventArgs e)
        {
            ruleTransactionModel.userInput = txtUserInput.Text;
            ruleTransactionModel.operatorsEnum = (OperatorsEnum)Enum.Parse(typeof(OperatorsEnum), cboCondition.Text);
            ruleTransactionModel.IsSaved = true;
            ruleTransactionModel.IsFallBack = materialCheckbox1.Checked;
            if (ruleTransactionModel.IsFallBack == true)
            {
                ruleTransactionModel.userInput = "(" + Strings.fallback + ")";
            }
            List<ValidationFailure> errors = new RuleTransactionModelValidator().Validate(this.ruleTransactionModel).Errors.ToList();

            if (errors.Count() > 0)
            {
                MaterialSnackBar SnackBarMessage = new MaterialSnackBar(errors[0].ErrorMessage, Strings.OK, true);
                SnackBarMessage.Show(this);
            }
            else
            {
                waAutoReplyForm.AddRuleTRansaction(ruleTransactionModel);
                this.Close();
            }
        }

        private void lstMessages_DoubleClick(object sender, EventArgs e)
        {
            var ss = lstMessages.SelectedItems[0].Index;
            this.ruleTransactionModel.messages[ss].IsEditMode = true;
            AddMessage addMessage = new AddMessage(this.ruleTransactionModel.messages[ss], this);
            addMessage.ShowDialog();
        }

        private void AddRule_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void AddRule_FormClosing(object sender, FormClosingEventArgs e)
        {
            waAutoReplyForm.HandleChieldEditMode();
        }

        private void materialButton4_Click(object sender, EventArgs e)
        {
            MaterialDialog materialDialog = new MaterialDialog(this, Strings.Confirm, Strings.AreyousuretodeletethisRule, Strings.OK, true, Strings.Cancel);
            DialogResult result = materialDialog.ShowDialog(this);
            if (result.ToString() == Strings.OK)
            {
                waAutoReplyForm.RemoveItem();
                this.Close();
            }
        }

        private void materialCheckbox1_CheckedChanged(object sender, EventArgs e)
        {
            if (materialCheckbox1.Checked)
            {
                materialCard1.Enabled = false;
            }
            else {
                materialCard1.Enabled = true;
            }
        }
    }
}
