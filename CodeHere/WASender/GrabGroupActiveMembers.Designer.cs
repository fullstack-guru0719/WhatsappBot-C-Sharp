namespace WASender
{
    partial class GrabGroupActiveMembers
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GrabGroupActiveMembers));
            this.materialCard3 = new MaterialSkin.Controls.MaterialCard();
            this.lblRunStatus = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.materialButton2 = new MaterialSkin.Controls.MaterialButton();
            this.materialLabel1 = new System.Windows.Forms.Label();
            this.materialButton1 = new MaterialSkin.Controls.MaterialButton();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.materialCard2 = new MaterialSkin.Controls.MaterialCard();
            this.lblInitStatus = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.materialLabel2 = new System.Windows.Forms.Label();
            this.btnInitWA = new MaterialSkin.Controls.MaterialButton();
            this.savesampleExceldialog = new System.Windows.Forms.SaveFileDialog();
            this.materialCard1 = new MaterialSkin.Controls.MaterialCard();
            this.materialButton3 = new MaterialSkin.Controls.MaterialButton();
            this.lbltotalfoundCount = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.materialCard3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.materialCard2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.materialCard1.SuspendLayout();
            this.SuspendLayout();
            // 
            // materialCard3
            // 
            this.materialCard3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.materialCard3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.materialCard3.Controls.Add(this.lblRunStatus);
            this.materialCard3.Controls.Add(this.label7);
            this.materialCard3.Controls.Add(this.materialButton2);
            this.materialCard3.Controls.Add(this.materialLabel1);
            this.materialCard3.Controls.Add(this.materialButton1);
            this.materialCard3.Controls.Add(this.pictureBox2);
            this.materialCard3.Depth = 0;
            this.materialCard3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialCard3.Location = new System.Drawing.Point(17, 234);
            this.materialCard3.Margin = new System.Windows.Forms.Padding(14);
            this.materialCard3.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialCard3.Name = "materialCard3";
            this.materialCard3.Padding = new System.Windows.Forms.Padding(14);
            this.materialCard3.Size = new System.Drawing.Size(1024, 130);
            this.materialCard3.TabIndex = 11;
            // 
            // lblRunStatus
            // 
            this.lblRunStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRunStatus.AutoSize = true;
            this.lblRunStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRunStatus.Location = new System.Drawing.Point(894, 68);
            this.lblRunStatus.Name = "lblRunStatus";
            this.lblRunStatus.Size = new System.Drawing.Size(107, 17);
            this.lblRunStatus.TabIndex = 15;
            this.lblRunStatus.Text = "Not Initialised";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(831, 68);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(48, 17);
            this.label7.TabIndex = 14;
            this.label7.Text = "Status";
            // 
            // materialButton2
            // 
            this.materialButton2.AutoSize = false;
            this.materialButton2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.materialButton2.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.materialButton2.Depth = 0;
            this.materialButton2.HighEmphasis = true;
            this.materialButton2.Icon = global::WASender.Properties.Resources.icons8_manual_48px1;
            this.materialButton2.Image = global::WASender.Properties.Resources.icons8_linking_24px;
            this.materialButton2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.materialButton2.Location = new System.Drawing.Point(436, 58);
            this.materialButton2.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.materialButton2.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialButton2.Name = "materialButton2";
            this.materialButton2.NoAccentTextColor = System.Drawing.Color.Empty;
            this.materialButton2.Size = new System.Drawing.Size(247, 36);
            this.materialButton2.TabIndex = 13;
            this.materialButton2.Text = "Stop";
            this.materialButton2.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.materialButton2.UseAccentColor = false;
            this.materialButton2.UseVisualStyleBackColor = true;
            this.materialButton2.Click += new System.EventHandler(this.materialButton2_Click);
            // 
            // materialLabel1
            // 
            this.materialLabel1.AutoSize = true;
            this.materialLabel1.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialLabel1.ForeColor = System.Drawing.Color.Black;
            this.materialLabel1.Location = new System.Drawing.Point(148, 29);
            this.materialLabel1.Name = "materialLabel1";
            this.materialLabel1.Size = new System.Drawing.Size(292, 18);
            this.materialLabel1.TabIndex = 12;
            this.materialLabel1.Text = "Open any Group chat && Click button bellow";
            // 
            // materialButton1
            // 
            this.materialButton1.AutoSize = false;
            this.materialButton1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.materialButton1.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.materialButton1.Depth = 0;
            this.materialButton1.HighEmphasis = true;
            this.materialButton1.Icon = global::WASender.Properties.Resources.icons8_software_installer_24px;
            this.materialButton1.Image = global::WASender.Properties.Resources.icons8_linking_24px;
            this.materialButton1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.materialButton1.Location = new System.Drawing.Point(148, 58);
            this.materialButton1.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.materialButton1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialButton1.Name = "materialButton1";
            this.materialButton1.NoAccentTextColor = System.Drawing.Color.Empty;
            this.materialButton1.Size = new System.Drawing.Size(247, 36);
            this.materialButton1.TabIndex = 5;
            this.materialButton1.Text = "Start Grabbing";
            this.materialButton1.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.materialButton1.UseAccentColor = false;
            this.materialButton1.UseVisualStyleBackColor = true;
            this.materialButton1.Click += new System.EventHandler(this.materialButton1_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::WASender.Properties.Resources.nn2;
            this.pictureBox2.Location = new System.Drawing.Point(21, 17);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(85, 77);
            this.pictureBox2.TabIndex = 4;
            this.pictureBox2.TabStop = false;
            // 
            // materialCard2
            // 
            this.materialCard2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.materialCard2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.materialCard2.Controls.Add(this.lblInitStatus);
            this.materialCard2.Controls.Add(this.label5);
            this.materialCard2.Controls.Add(this.pictureBox1);
            this.materialCard2.Controls.Add(this.materialLabel2);
            this.materialCard2.Controls.Add(this.btnInitWA);
            this.materialCard2.Depth = 0;
            this.materialCard2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialCard2.Location = new System.Drawing.Point(17, 87);
            this.materialCard2.Margin = new System.Windows.Forms.Padding(14);
            this.materialCard2.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialCard2.Name = "materialCard2";
            this.materialCard2.Padding = new System.Windows.Forms.Padding(14);
            this.materialCard2.Size = new System.Drawing.Size(1024, 130);
            this.materialCard2.TabIndex = 10;
            // 
            // lblInitStatus
            // 
            this.lblInitStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblInitStatus.AutoSize = true;
            this.lblInitStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInitStatus.Location = new System.Drawing.Point(894, 56);
            this.lblInitStatus.Name = "lblInitStatus";
            this.lblInitStatus.Size = new System.Drawing.Size(107, 17);
            this.lblInitStatus.TabIndex = 7;
            this.lblInitStatus.Text = "Not Initialised";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(831, 56);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 17);
            this.label5.TabIndex = 6;
            this.label5.Text = "Status";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::WASender.Properties.Resources.nn1;
            this.pictureBox1.Location = new System.Drawing.Point(21, 33);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(85, 77);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // materialLabel2
            // 
            this.materialLabel2.AutoSize = true;
            this.materialLabel2.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialLabel2.ForeColor = System.Drawing.Color.Black;
            this.materialLabel2.Location = new System.Drawing.Point(148, 33);
            this.materialLabel2.Name = "materialLabel2";
            this.materialLabel2.Size = new System.Drawing.Size(370, 18);
            this.materialLabel2.TabIndex = 2;
            this.materialLabel2.Text = "Initiate WhatsApp && Scane QR Code from yourr mobile";
            // 
            // btnInitWA
            // 
            this.btnInitWA.AutoSize = false;
            this.btnInitWA.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnInitWA.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnInitWA.Depth = 0;
            this.btnInitWA.HighEmphasis = true;
            this.btnInitWA.Icon = global::WASender.Properties.Resources.icons8_linking_24px;
            this.btnInitWA.Image = global::WASender.Properties.Resources.icons8_linking_24px;
            this.btnInitWA.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnInitWA.Location = new System.Drawing.Point(148, 67);
            this.btnInitWA.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnInitWA.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnInitWA.Name = "btnInitWA";
            this.btnInitWA.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnInitWA.Size = new System.Drawing.Size(247, 36);
            this.btnInitWA.TabIndex = 1;
            this.btnInitWA.Text = "Click to Initiate";
            this.btnInitWA.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnInitWA.UseAccentColor = false;
            this.btnInitWA.UseVisualStyleBackColor = true;
            this.btnInitWA.Click += new System.EventHandler(this.btnInitWA_Click);
            // 
            // materialCard1
            // 
            this.materialCard1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.materialCard1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.materialCard1.Controls.Add(this.materialButton3);
            this.materialCard1.Controls.Add(this.lbltotalfoundCount);
            this.materialCard1.Controls.Add(this.label1);
            this.materialCard1.Depth = 0;
            this.materialCard1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialCard1.Location = new System.Drawing.Point(17, 376);
            this.materialCard1.Margin = new System.Windows.Forms.Padding(14);
            this.materialCard1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialCard1.Name = "materialCard1";
            this.materialCard1.Padding = new System.Windows.Forms.Padding(14);
            this.materialCard1.Size = new System.Drawing.Size(1024, 130);
            this.materialCard1.TabIndex = 12;
            // 
            // materialButton3
            // 
            this.materialButton3.AutoSize = false;
            this.materialButton3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.materialButton3.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.materialButton3.Depth = 0;
            this.materialButton3.HighEmphasis = true;
            this.materialButton3.Icon = global::WASender.Properties.Resources.icons8_xls_export_48px;
            this.materialButton3.Image = global::WASender.Properties.Resources.icons8_linking_24px;
            this.materialButton3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.materialButton3.Location = new System.Drawing.Point(436, 41);
            this.materialButton3.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.materialButton3.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialButton3.Name = "materialButton3";
            this.materialButton3.NoAccentTextColor = System.Drawing.Color.Empty;
            this.materialButton3.Size = new System.Drawing.Size(247, 36);
            this.materialButton3.TabIndex = 14;
            this.materialButton3.Text = "Export";
            this.materialButton3.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.materialButton3.UseAccentColor = false;
            this.materialButton3.UseVisualStyleBackColor = true;
            this.materialButton3.Click += new System.EventHandler(this.materialButton3_Click);
            // 
            // lbltotalfoundCount
            // 
            this.lbltotalfoundCount.AutoSize = true;
            this.lbltotalfoundCount.Location = new System.Drawing.Point(250, 60);
            this.lbltotalfoundCount.Name = "lbltotalfoundCount";
            this.lbltotalfoundCount.Size = new System.Drawing.Size(16, 17);
            this.lbltotalfoundCount.TabIndex = 1;
            this.lbltotalfoundCount.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(148, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Total Found : ";
            // 
            // GrabGroupActiveMembers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1058, 785);
            this.Controls.Add(this.materialCard1);
            this.Controls.Add(this.materialCard3);
            this.Controls.Add(this.materialCard2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GrabGroupActiveMembers";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GrabGroupActiveMembers";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GrabGroupActiveMembers_FormClosing);
            this.Load += new System.EventHandler(this.GrabGroupActiveMembers_Load);
            this.materialCard3.ResumeLayout(false);
            this.materialCard3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.materialCard2.ResumeLayout(false);
            this.materialCard2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.materialCard1.ResumeLayout(false);
            this.materialCard1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MaterialSkin.Controls.MaterialCard materialCard3;
        private System.Windows.Forms.Label materialLabel1;
        private MaterialSkin.Controls.MaterialButton materialButton1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private MaterialSkin.Controls.MaterialCard materialCard2;
        private System.Windows.Forms.Label lblInitStatus;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label materialLabel2;
        private MaterialSkin.Controls.MaterialButton btnInitWA;
        private System.Windows.Forms.SaveFileDialog savesampleExceldialog;
        private MaterialSkin.Controls.MaterialButton materialButton2;
        private System.Windows.Forms.Label lblRunStatus;
        private System.Windows.Forms.Label label7;
        private MaterialSkin.Controls.MaterialCard materialCard1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbltotalfoundCount;
        private MaterialSkin.Controls.MaterialButton materialButton3;
    }
}