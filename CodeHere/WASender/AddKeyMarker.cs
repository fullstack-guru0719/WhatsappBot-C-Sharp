using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using WaAutoReplyBot;
using System.Linq;


namespace WASender
{
    public partial class AddKeyMarker : MyMaterialPopOp
    {
        KeyMarker keyMarker;
        String PreviousText;
        public AddKeyMarker(KeyMarker _keyMarker, string previous = "")
        {
            keyMarker = _keyMarker;
            PreviousText = previous;
            InitializeComponent();
            init();
        }

        private void init()
        {
            this.Text = Strings.AddKeyMarker;
            this.btnSave.Text = Strings.SaveNClose;
            this.btnDelete.Text = Strings.Delete;
            if (PreviousText != "")
            {
                materialMultiLineTextBox21.Text = PreviousText;
            }
            else
            {
                btnDelete.Enabled = false;
            }

        }



        private void btnSave_Click(object sender, EventArgs e)
        {
            string msg = materialMultiLineTextBox21.Text;
            if ((!msg.StartsWith("{{") || !msg.EndsWith("}}")) || msg.Trim().Contains(Environment.NewLine))
            {
                MaterialSnackBar SnackBarMessage = new MaterialSnackBar(Strings.KeyMarkerFormatinIncorrect, Strings.OK, true);
                SnackBarMessage.Show(this);
            }
            else if (!msg.StartsWith("{{ KEY :"))
            {
                MaterialSnackBar SnackBarMessage = new MaterialSnackBar(Strings.WrongKey, Strings.OK, true);
                SnackBarMessage.Show(this);
            }
            else
            {
                String keyMarkersTxtFilepath = Config.GetKeyMarkersFilePath();
                string existingText = "";

                if (!File.Exists(keyMarkersTxtFilepath))
                {
                    File.Create(keyMarkersTxtFilepath).Close();
                }
                else
                {
                    existingText = File.ReadAllText(keyMarkersTxtFilepath);
                }
                if (PreviousText == "")
                {
                    if (existingText.Contains(msg))
                    {
                        MaterialSnackBar SnackBarMessage = new MaterialSnackBar(Strings.SelectedKeyMarkeralreadyExist, Strings.OK, true);
                        SnackBarMessage.Show(this);
                        return;
                    }
                    else
                    {
                        string NewText = existingText + Environment.NewLine + msg;
                        File.WriteAllText(keyMarkersTxtFilepath, NewText);
                    }

                }
                else
                {
                    string NewText = existingText.Replace(PreviousText, msg) + Environment.NewLine;
                    int count = NewText.Split('\n').Where(x => x.Trim().Replace("\r", "") == msg).Count();
                    if (count > 1)
                    {
                        MaterialSnackBar SnackBarMessage = new MaterialSnackBar(Strings.SelectedKeyMarkeralreadyExist, Strings.OK, true);
                        SnackBarMessage.Show(this);
                        return;
                    }
                    else
                    {
                        File.WriteAllText(keyMarkersTxtFilepath, NewText);
                    }

                }


                keyMarker.LoadMarkers();
                this.Hide();


            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            String keyMarkersTxtFilepath = Config.GetKeyMarkersFilePath();
            string existingText = File.ReadAllText(keyMarkersTxtFilepath);
            string NewText = existingText.Replace(PreviousText, "");
            File.WriteAllText(keyMarkersTxtFilepath, NewText);
            keyMarker.LoadMarkers();
            this.Hide();
        }

        private void AddKeyMarker_Load(object sender, EventArgs e)
        {
            InitLanguage();
        }

        private void InitLanguage()
        {
            this.Text = Strings.AddKeyMarker;
            this.btnDelete .Text= Strings.Delete;
            this.btnSave.Text = Strings.Save;
        }
    }
}
