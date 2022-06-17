using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WaAutoReplyBot;
using WaAutoReplyBot.Models;

namespace WASender
{
    public partial class KeyMarker : MyMaterialPopOp
    {
        WaSenderForm waSenderForm;
        public KeyMarker(WaSenderForm _waSenderForm)
        {
            this.waSenderForm = _waSenderForm;
            InitializeComponent();
            init();
        }

        private void init()
        {
            this.Text = Strings.KeyMarkers;
            btnAddNew.Text = Strings.AddNew;
            LoadMarkers();
        }

        public void LoadMarkers()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(Strings.KeyMarkers, typeof(string));

            String keyMarkersTxtFilepath = Config.GetKeyMarkersFilePath();
            if (File.Exists(keyMarkersTxtFilepath))
            {
                string existingText = File.ReadAllText(keyMarkersTxtFilepath);
                foreach (var marker in existingText.Split('\n'))
                {
                    if (marker != "\r" && marker.Trim() != "")
                    {
                        dt.Rows.Add(marker);
                    }
                }
            }
            if (dt.Rows.Count > 0)
            {
                gridMarker.DataSource = dt;
                gridMarker.Columns[1].Width = 250;
            }
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            AddKeyMarker addKeyMarker = new AddKeyMarker(this);
            addKeyMarker.ShowDialog();
        }

        private void gridMarker_DoubleClick(object sender, EventArgs e)
        {
            var ss = gridMarker.CurrentRow.Cells[1].Value.ToString().Replace("\r", "");
            AddKeyMarker addKeyMarker = new AddKeyMarker(this, ss);
            addKeyMarker.ShowDialog();
        }

        private void gridMarker_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (gridMarker.Columns[e.ColumnIndex].Name == "Select")
            {
                var ss = gridMarker.CurrentRow.Cells[1].Value.ToString().Replace("\r", "");
                waSenderForm.AddKeyMarker(ss);
                this.Close();
            }
        }

        private void KeyMarker_Load(object sender, EventArgs e)
        {
            InitLanguage();
        }

        private void InitLanguage()
        {
            this.Text = Strings.KeyMarker;
            btnAddNew.Text = Strings.AddNew;
        }
    }
}
