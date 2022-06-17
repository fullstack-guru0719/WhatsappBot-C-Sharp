namespace WASender.Alerts
{
    partial class Alert
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
            this.components = new System.ComponentModel.Container();
            this.lblMsg = new System.Windows.Forms.Label();
            this.timeranimate = new System.Windows.Forms.Timer(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnClose = new MaterialSkin.Controls.MaterialButton();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblMsg
            // 
            this.lblMsg.AutoSize = true;
            this.lblMsg.ForeColor = System.Drawing.Color.White;
            this.lblMsg.Location = new System.Drawing.Point(67, 24);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(135, 23);
            this.lblMsg.TabIndex = 0;
            this.lblMsg.Text = "Message Text";
            // 
            // timeranimate
            // 
            this.timeranimate.Tick += new System.EventHandler(this.timeranimate_Tick);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::WASender.Properties.Resources.icons8_info_48px;
            this.pictureBox1.Location = new System.Drawing.Point(12, 24);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(49, 50);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // btnClose
            // 
            this.btnClose.AutoSize = false;
            this.btnClose.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.BackgroundImage = global::WASender.Properties.Resources.icons8_close_window_24px;
            this.btnClose.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnClose.Depth = 0;
            this.btnClose.HighEmphasis = true;
            this.btnClose.Icon = global::WASender.Properties.Resources.icons8_close_window_24px;
            this.btnClose.Image = global::WASender.Properties.Resources.icons8_close_window_24px;
            this.btnClose.Location = new System.Drawing.Point(384, 9);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnClose.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnClose.Name = "btnClose";
            this.btnClose.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnClose.Size = new System.Drawing.Size(40, 36);
            this.btnClose.TabIndex = 4;
            this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnClose.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnClose.UseAccentColor = false;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click_1);
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // Alert
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.CornflowerBlue;
            this.ClientSize = new System.Drawing.Size(441, 123);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lblMsg);
            this.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Alert";
            this.Text = "Info";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Timer timeranimate;
        private MaterialSkin.Controls.MaterialButton btnClose;
        private System.Windows.Forms.ImageList imageList1;
    }
}