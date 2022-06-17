namespace WASender
{
    partial class RunGroup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RunGroup));
            this.btnInitWA = new MaterialSkin.Controls.MaterialButton();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.label8 = new System.Windows.Forms.Label();
            this.materialCard4 = new MaterialSkin.Controls.MaterialCard();
            this.gridStatus = new System.Windows.Forms.DataGridView();
            this.ChatName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.materialCard3 = new MaterialSkin.Controls.MaterialCard();
            this.lblPersentage = new System.Windows.Forms.Label();
            this.btnSTart = new MaterialSkin.Controls.MaterialButton();
            this.lblRunStatus = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.materialLabel2 = new System.Windows.Forms.Label();
            this.lblInitStatus = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.materialCard1 = new MaterialSkin.Controls.MaterialCard();
            this.label5 = new System.Windows.Forms.Label();
            this.materialCard2 = new MaterialSkin.Controls.MaterialCard();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.materialCard4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.materialCard3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.materialCard1.SuspendLayout();
            this.materialCard2.SuspendLayout();
            this.SuspendLayout();
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
            // pictureBox3
            // 
            this.pictureBox3.Image = global::WASender.Properties.Resources.nn3;
            this.pictureBox3.Location = new System.Drawing.Point(21, 14);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(85, 77);
            this.pictureBox3.TabIndex = 8;
            this.pictureBox3.TabStop = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(148, 14);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(32, 17);
            this.label8.TabIndex = 7;
            this.label8.Text = "Log";
            // 
            // materialCard4
            // 
            this.materialCard4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.materialCard4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.materialCard4.Controls.Add(this.gridStatus);
            this.materialCard4.Controls.Add(this.pictureBox3);
            this.materialCard4.Controls.Add(this.label8);
            this.materialCard4.Depth = 0;
            this.materialCard4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialCard4.Location = new System.Drawing.Point(12, 388);
            this.materialCard4.Margin = new System.Windows.Forms.Padding(14);
            this.materialCard4.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialCard4.Name = "materialCard4";
            this.materialCard4.Padding = new System.Windows.Forms.Padding(14);
            this.materialCard4.Size = new System.Drawing.Size(1029, 212);
            this.materialCard4.TabIndex = 7;
            // 
            // gridStatus
            // 
            this.gridStatus.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridStatus.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ChatName,
            this.Status});
            this.gridStatus.Location = new System.Drawing.Point(148, 45);
            this.gridStatus.Name = "gridStatus";
            this.gridStatus.RowTemplate.Height = 24;
            this.gridStatus.Size = new System.Drawing.Size(858, 150);
            this.gridStatus.TabIndex = 9;
            // 
            // ChatName
            // 
            this.ChatName.HeaderText = "ChatName";
            this.ChatName.Name = "ChatName";
            this.ChatName.ReadOnly = true;
            this.ChatName.Width = 300;
            // 
            // Status
            // 
            this.Status.HeaderText = "Status";
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            this.Status.Width = 300;
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
            // materialCard3
            // 
            this.materialCard3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.materialCard3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.materialCard3.Controls.Add(this.lblPersentage);
            this.materialCard3.Controls.Add(this.btnSTart);
            this.materialCard3.Controls.Add(this.lblRunStatus);
            this.materialCard3.Controls.Add(this.label7);
            this.materialCard3.Controls.Add(this.pictureBox2);
            this.materialCard3.Depth = 0;
            this.materialCard3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialCard3.Location = new System.Drawing.Point(12, 239);
            this.materialCard3.Margin = new System.Windows.Forms.Padding(14);
            this.materialCard3.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialCard3.Name = "materialCard3";
            this.materialCard3.Padding = new System.Windows.Forms.Padding(14);
            this.materialCard3.Size = new System.Drawing.Size(1029, 130);
            this.materialCard3.TabIndex = 6;
            // 
            // lblPersentage
            // 
            this.lblPersentage.AutoSize = true;
            this.lblPersentage.Location = new System.Drawing.Point(649, 52);
            this.lblPersentage.Name = "lblPersentage";
            this.lblPersentage.Size = new System.Drawing.Size(0, 17);
            this.lblPersentage.TabIndex = 13;
            // 
            // btnSTart
            // 
            this.btnSTart.AutoSize = false;
            this.btnSTart.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnSTart.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnSTart.Depth = 0;
            this.btnSTart.HighEmphasis = true;
            this.btnSTart.Icon = global::WASender.Properties.Resources.icons8_play_24px;
            this.btnSTart.Image = global::WASender.Properties.Resources.icons8_linking_24px;
            this.btnSTart.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSTart.Location = new System.Drawing.Point(151, 42);
            this.btnSTart.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnSTart.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnSTart.Name = "btnSTart";
            this.btnSTart.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnSTart.Size = new System.Drawing.Size(148, 36);
            this.btnSTart.TabIndex = 10;
            this.btnSTart.Text = "Start";
            this.btnSTart.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnSTart.UseAccentColor = false;
            this.btnSTart.UseVisualStyleBackColor = true;
            this.btnSTart.Click += new System.EventHandler(this.btnSTart_Click);
            // 
            // lblRunStatus
            // 
            this.lblRunStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRunStatus.AutoSize = true;
            this.lblRunStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRunStatus.Location = new System.Drawing.Point(899, 52);
            this.lblRunStatus.Name = "lblRunStatus";
            this.lblRunStatus.Size = new System.Drawing.Size(107, 17);
            this.lblRunStatus.TabIndex = 9;
            this.lblRunStatus.Text = "Not Initialised";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(836, 52);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(48, 17);
            this.label7.TabIndex = 8;
            this.label7.Text = "Status";
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
            // lblInitStatus
            // 
            this.lblInitStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblInitStatus.AutoSize = true;
            this.lblInitStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInitStatus.Location = new System.Drawing.Point(899, 56);
            this.lblInitStatus.Name = "lblInitStatus";
            this.lblInitStatus.Size = new System.Drawing.Size(107, 17);
            this.lblInitStatus.TabIndex = 7;
            this.lblInitStatus.Text = "Not Initialised";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(147, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(632, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "1) Keep application open while sending messages and until all messages are sent f" +
    "rom your mobile";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(148, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(108, 17);
            this.label4.TabIndex = 5;
            this.label4.Text = "Importent Notes";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(147, 118);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(646, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "3) WaSender tends to submit messages to your phone, is not responsible for delive" +
    "ry of the message";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(148, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(655, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "2) Clear WhatsApp chat history after 500/1000/1500/20000 messages as per your pho" +
    "ne configuration";
            // 
            // materialCard1
            // 
            this.materialCard1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.materialCard1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.materialCard1.Controls.Add(this.label4);
            this.materialCard1.Controls.Add(this.label3);
            this.materialCard1.Controls.Add(this.label2);
            this.materialCard1.Controls.Add(this.label1);
            this.materialCard1.Cursor = System.Windows.Forms.Cursors.Default;
            this.materialCard1.Depth = 0;
            this.materialCard1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialCard1.Location = new System.Drawing.Point(12, 613);
            this.materialCard1.Margin = new System.Windows.Forms.Padding(14);
            this.materialCard1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialCard1.Name = "materialCard1";
            this.materialCard1.Padding = new System.Windows.Forms.Padding(14);
            this.materialCard1.Size = new System.Drawing.Size(1029, 157);
            this.materialCard1.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(836, 56);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 17);
            this.label5.TabIndex = 6;
            this.label5.Text = "Status";
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
            this.materialCard2.Location = new System.Drawing.Point(12, 90);
            this.materialCard2.Margin = new System.Windows.Forms.Padding(14);
            this.materialCard2.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialCard2.Name = "materialCard2";
            this.materialCard2.Padding = new System.Windows.Forms.Padding(14);
            this.materialCard2.Size = new System.Drawing.Size(1029, 130);
            this.materialCard2.TabIndex = 5;
            // 
            // RunGroup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1058, 787);
            this.Controls.Add(this.materialCard4);
            this.Controls.Add(this.materialCard3);
            this.Controls.Add(this.materialCard1);
            this.Controls.Add(this.materialCard2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "RunGroup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RunGroup";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.RunGroup_FormClosed);
            this.Load += new System.EventHandler(this.RunGroup_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.materialCard4.ResumeLayout(false);
            this.materialCard4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.materialCard3.ResumeLayout(false);
            this.materialCard3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.materialCard1.ResumeLayout(false);
            this.materialCard1.PerformLayout();
            this.materialCard2.ResumeLayout(false);
            this.materialCard2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MaterialSkin.Controls.MaterialButton btnInitWA;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label label8;
        private MaterialSkin.Controls.MaterialCard materialCard4;
        private System.Windows.Forms.PictureBox pictureBox1;
        private MaterialSkin.Controls.MaterialCard materialCard3;
        private System.Windows.Forms.Label lblPersentage;
        private MaterialSkin.Controls.MaterialButton btnSTart;
        private System.Windows.Forms.Label lblRunStatus;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label materialLabel2;
        private System.Windows.Forms.Label lblInitStatus;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private MaterialSkin.Controls.MaterialCard materialCard1;
        private System.Windows.Forms.Label label5;
        private MaterialSkin.Controls.MaterialCard materialCard2;
        private System.Windows.Forms.DataGridView gridStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn ChatName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;

    }
}