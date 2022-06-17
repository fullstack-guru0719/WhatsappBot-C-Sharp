using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WASender.Models;

namespace WASender
{
    public static class Utils
    {
        public static void showAlert(string message,Alerts.Alert.enmType alertType)
        {
            Alerts.Alert alert = new Alerts.Alert();
            alert.showAlert(message, alertType);
        }

        public static string ExtractBetweenTwoStrings(string FullText, string StartString, string EndString, bool IncludeStartString, bool IncludeEndString)
        {
            try
            {
                int Pos1 = FullText.IndexOf(StartString) + StartString.Length;
                int Pos2 = FullText.IndexOf(EndString, Pos1);
                return ((IncludeStartString) ? StartString : "") + FullText.Substring(Pos1, Pos2 - Pos1) + ((IncludeEndString) ? EndString : "");
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public static int getRandom(int min, int max)
        {
            try
            {
                return new Random().Next(min, max);
            }
            catch (Exception ex)
            {

                return min;
            }
        }

        public static void setTooltiop(Control control, string caption)
        {
            System.Windows.Forms.ToolTip ToolTip1 = new System.Windows.Forms.ToolTip();
            ToolTip1.BackColor = System.Drawing.Color.Black;
            ToolTip1.ForeColor = System.Drawing.Color.Wheat;
            ToolTip1.SetToolTip(control, caption);
        }

        public static void selectFileForMessage(MaterialListView lstView)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = Strings.SelectExcel;
            openFileDialog.DefaultExt = "";
            openFileDialog.Multiselect = false;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string file = openFileDialog.FileName;
                lstView.Items.Add(file);
            }
        }

        public static void removeListViewItem(KeyEventArgs e, MaterialListView lstView)
        {
            if (e.KeyCode == Keys.Delete)
            {
                var removables = new List<ListViewItem>();
                foreach (ListViewItem item in lstView.SelectedItems)
                {
                    removables.Add(item);
                }
                foreach (var item in removables)
                {
                    lstView.Items.Remove(item);
                }
            }
        }

        public static DialogResultModel ShowDialog(string text, string caption)
        {
            Form prompt = new Form()
            {
                Width = 500,
                Height = 350,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = caption,
                StartPosition = FormStartPosition.CenterScreen
            };
            Label textLabel = new Label() { Left = 50, Top = 20, Text = text,Width = 300 };
            TextBox textBox = new TextBox() { Left = 50, Top = 50, Width = 400 };

            Label textLabelMessageType = new Label() { Left = 50, Top = 90, Text = Strings.MessageSendingType, Width = 300 };
            ComboBox comboBox = new ComboBox() { Left = 50, Top = 120,  Width = 300 };
            comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            Dictionary<string, string> test = new Dictionary<string, string>();
            test.Add("1", Strings.CopyPaste);
            test.Add("2", Strings.Typeamessage);
            comboBox.DataSource = new BindingSource(test, null);
            comboBox.DisplayMember = "Value";
            comboBox.ValueMember = "Key";
            comboBox.SelectedItem = "1";
           // string value = ((KeyValuePair<string, string>)comboBox.SelectedItem).Value;


            Button confirmation = new Button() { Text = Strings.OK, Left = 350, Width = 100, Top = 200, DialogResult = DialogResult.OK };
            confirmation.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(textLabel);
            prompt.Controls.Add(textLabelMessageType);
            prompt.Controls.Add(comboBox);
            prompt.AcceptButton = confirmation;

            DialogResultModel dialogResultModel = new DialogResultModel();
            dialogResultModel.MessageType = 1;

            // return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : "";
            if (prompt.ShowDialog() == DialogResult.OK)
            {
                dialogResultModel.CampaignName = textBox.Text;
                dialogResultModel.MessageType = Convert.ToInt32(comboBox.SelectedValue);
                return dialogResultModel;
            }
            else
            {
                return dialogResultModel;            
            }

        }

    }
}
