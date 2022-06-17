namespace WASender
{
    partial class ContactGrabber
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ContactGrabber));
            this.materialCard2 = new MaterialSkin.Controls.MaterialCard();
            this.lblInitStatus = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.materialLabel2 = new System.Windows.Forms.Label();
            this.btnInitWA = new MaterialSkin.Controls.MaterialButton();
            this.materialCard3 = new MaterialSkin.Controls.MaterialCard();
            this.lblRunStatus = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.materialButton1 = new MaterialSkin.Controls.MaterialButton();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.savesampleExceldialog = new System.Windows.Forms.SaveFileDialog();
            this.materialCard2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.materialCard3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
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
            this.materialCard2.TabIndex = 8;
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
            // materialCard3
            // 
            this.materialCard3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.materialCard3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.materialCard3.Controls.Add(this.lblRunStatus);
            this.materialCard3.Controls.Add(this.label7);
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
            this.materialCard3.TabIndex = 9;
            // 
            // lblRunStatus
            // 
            this.lblRunStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRunStatus.AutoSize = true;
            this.lblRunStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRunStatus.Location = new System.Drawing.Point(894, 50);
            this.lblRunStatus.Name = "lblRunStatus";
            this.lblRunStatus.Size = new System.Drawing.Size(107, 17);
            this.lblRunStatus.TabIndex = 11;
            this.lblRunStatus.Text = "Not Initialised";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(831, 50);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(48, 17);
            this.label7.TabIndex = 10;
            this.label7.Text = "Status";
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
            this.materialButton1.Location = new System.Drawing.Point(151, 40);
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
            // ContactGrabber
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1040, 738);
            this.Controls.Add(this.materialCard2);
            this.Controls.Add(this.materialCard3);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ContactGrabber";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ContactGrabber";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ContactGrabber_FormClosed);
            this.Load += new System.EventHandler(this.ContactGrabber_Load);
            this.materialCard2.ResumeLayout(false);
            this.materialCard2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.materialCard3.ResumeLayout(false);
            this.materialCard3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private MaterialSkin.Controls.MaterialCard materialCard2;
        private System.Windows.Forms.Label lblInitStatus;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label materialLabel2;
        private MaterialSkin.Controls.MaterialButton btnInitWA;
        private MaterialSkin.Controls.MaterialCard materialCard3;
        private System.Windows.Forms.Label lblRunStatus;
        private System.Windows.Forms.Label label7;
        private MaterialSkin.Controls.MaterialButton materialButton1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.SaveFileDialog savesampleExceldialog;
    }
}