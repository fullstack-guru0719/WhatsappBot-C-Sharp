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
using WASender.Properties;

namespace WASender.Alerts
{
    public partial class Alert : Form
    {
        public Alert()
        {
            InitializeComponent();
        }

        public enum enumAction
        { 
            wait,
            start,
            close
        }

        private Alert.enumAction action;
        private int x, y;
        public enum enmType
        {
            Success,
            Warning,
            Error,
            Info
        }

        public void showAlert(string msg, enmType type)
        {
            this.Opacity = 0.0;
            this.StartPosition = FormStartPosition.Manual;

            string fname;

            for (int i = 1; i < 10; i++)
            {
                fname = "alert" + i.ToString();
                Alert frm = (Alert)Application.OpenForms[fname];

                if (frm == null)
                {
                    this.Name = fname;
                    this.x = Screen.PrimaryScreen.WorkingArea.Width - this.Width + 15;
                    this.y = Screen.PrimaryScreen.WorkingArea.Height - this.Height * i - 5 * i;
                    this.Location = new Point(this.x, this.y);
                    break;

                }

            }
            this.x = Screen.PrimaryScreen.WorkingArea.Width - base.Width - 5;


            switch (type)
            {
                case enmType.Success:
                    this.pictureBox1.Image = Resources.success;
                    this.BackColor = Color.SeaGreen;
                    break;
                case enmType.Error:
                    this.pictureBox1.Image = Resources.error;
                    this.BackColor = Color.DarkRed;
                    break;
                case enmType.Info:
                    this.pictureBox1.Image = Resources.icons8_info_48px;
                    this.BackColor = Color.RoyalBlue;
                    break;
                case enmType.Warning:
                    this.pictureBox1.Image = Resources.warning;
                    this.BackColor = Color.DarkOrange;
                    break;
            }

            this.lblMsg.Text = msg;

            this.Show();
            this.action = enumAction.start;
            this.timeranimate.Interval = 1;
            this.timeranimate.Start();


        }

       

        private void timeranimate_Tick(object sender, EventArgs e)
        {
            switch (this.action)
            {
                case enumAction.wait:
                    timeranimate.Interval = 5000;
                    action = enumAction.close;
                    break;
                case Alert.enumAction.start:
                    this.timeranimate.Interval = 1;
                    this.Opacity += 0.1;
                    if (this.x < this.Location.X)
                    {
                        this.Left--;
                    }
                    else
                    {
                        if (this.Opacity == 1.0)
                        {
                            action = Alert.enumAction.wait;
                        }
                    }
                    break;
                case enumAction.close:
                    timeranimate.Interval = 1;
                    this.Opacity -= 0.1;

                    this.Left -= 3;
                    if (base.Opacity == 0.0)
                    {
                        base.Close();
                    }
                    break;
            }
        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            timeranimate.Interval = 1;
            action = enumAction.close;
        }
    }
}
