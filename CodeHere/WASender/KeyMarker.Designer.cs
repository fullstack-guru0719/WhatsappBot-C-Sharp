namespace WASender
{
    partial class KeyMarker
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.materialCard1 = new MaterialSkin.Controls.MaterialCard();
            this.gridMarker = new System.Windows.Forms.DataGridView();
            this.Select = new System.Windows.Forms.DataGridViewButtonColumn();
            this.btnAddNew = new MaterialSkin.Controls.MaterialButton();
            this.materialCard1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridMarker)).BeginInit();
            this.SuspendLayout();
            // 
            // materialCard1
            // 
            this.materialCard1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.materialCard1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.materialCard1.Controls.Add(this.gridMarker);
            this.materialCard1.Depth = 0;
            this.materialCard1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialCard1.Location = new System.Drawing.Point(17, 145);
            this.materialCard1.Margin = new System.Windows.Forms.Padding(14);
            this.materialCard1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialCard1.Name = "materialCard1";
            this.materialCard1.Padding = new System.Windows.Forms.Padding(14);
            this.materialCard1.Size = new System.Drawing.Size(631, 454);
            this.materialCard1.TabIndex = 0;
            // 
            // gridMarker
            // 
            this.gridMarker.AllowUserToAddRows = false;
            this.gridMarker.AllowUserToDeleteRows = false;
            this.gridMarker.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridMarker.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Select});
            this.gridMarker.Location = new System.Drawing.Point(17, 30);
            this.gridMarker.Name = "gridMarker";
            this.gridMarker.ReadOnly = true;
            this.gridMarker.RowTemplate.Height = 24;
            this.gridMarker.Size = new System.Drawing.Size(597, 407);
            this.gridMarker.TabIndex = 0;
            this.gridMarker.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridMarker_CellClick);
            this.gridMarker.DoubleClick += new System.EventHandler(this.gridMarker_DoubleClick);
            // 
            // Select
            // 
            this.Select.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Select.HeaderText = "";
            this.Select.Name = "Select";
            this.Select.ReadOnly = true;
            this.Select.Text = "<< Add";
            this.Select.UseColumnTextForButtonValue = true;
            // 
            // btnAddNew
            // 
            this.btnAddNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddNew.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnAddNew.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnAddNew.Depth = 0;
            this.btnAddNew.HighEmphasis = true;
            this.btnAddNew.Icon = global::WASender.Properties.Resources.icons8_add_new_24px1;
            this.btnAddNew.Location = new System.Drawing.Point(525, 89);
            this.btnAddNew.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnAddNew.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnAddNew.Name = "btnAddNew";
            this.btnAddNew.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnAddNew.Size = new System.Drawing.Size(114, 36);
            this.btnAddNew.TabIndex = 1;
            this.btnAddNew.Text = "Add New";
            this.btnAddNew.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnAddNew.UseAccentColor = false;
            this.btnAddNew.UseVisualStyleBackColor = true;
            this.btnAddNew.Click += new System.EventHandler(this.btnAddNew_Click);
            // 
            // KeyMarker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(665, 616);
            this.Controls.Add(this.btnAddNew);
            this.Controls.Add(this.materialCard1);
            this.Name = "KeyMarker";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "KeyMarker";
            this.Load += new System.EventHandler(this.KeyMarker_Load);
            this.materialCard1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridMarker)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MaterialSkin.Controls.MaterialCard materialCard1;
        private MaterialSkin.Controls.MaterialButton btnAddNew;
        private System.Windows.Forms.DataGridView gridMarker;
        private System.Windows.Forms.DataGridViewButtonColumn Select;
    }
}