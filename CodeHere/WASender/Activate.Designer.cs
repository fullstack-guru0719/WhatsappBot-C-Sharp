namespace WASender
{
    partial class Activate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Activate));
            this.materialCard1 = new MaterialSkin.Controls.MaterialCard();
            this.materialButton1 = new MaterialSkin.Controls.MaterialButton();
            this.btnActivate = new MaterialSkin.Controls.MaterialButton();
            this.txtKey = new MaterialSkin.Controls.MaterialMultiLineTextBox2();
            this.label1 = new System.Windows.Forms.Label();
            this.txtActivationCode = new MaterialSkin.Controls.MaterialTextBox2();
            this.lblActivationCode = new System.Windows.Forms.Label();
            this.materialCard1.SuspendLayout();
            this.SuspendLayout();
            // 
            // materialCard1
            // 
            this.materialCard1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.materialCard1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.materialCard1.Controls.Add(this.materialButton1);
            this.materialCard1.Controls.Add(this.btnActivate);
            this.materialCard1.Controls.Add(this.txtKey);
            this.materialCard1.Controls.Add(this.label1);
            this.materialCard1.Controls.Add(this.txtActivationCode);
            this.materialCard1.Controls.Add(this.lblActivationCode);
            this.materialCard1.Depth = 0;
            this.materialCard1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialCard1.Location = new System.Drawing.Point(17, 106);
            this.materialCard1.Margin = new System.Windows.Forms.Padding(14);
            this.materialCard1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialCard1.Name = "materialCard1";
            this.materialCard1.Padding = new System.Windows.Forms.Padding(14);
            this.materialCard1.Size = new System.Drawing.Size(588, 429);
            this.materialCard1.TabIndex = 0;
            // 
            // materialButton1
            // 
            this.materialButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.materialButton1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.materialButton1.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.materialButton1.Depth = 0;
            this.materialButton1.HighEmphasis = true;
            this.materialButton1.Icon = null;
            this.materialButton1.Location = new System.Drawing.Point(20, 373);
            this.materialButton1.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.materialButton1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialButton1.Name = "materialButton1";
            this.materialButton1.NoAccentTextColor = System.Drawing.Color.Empty;
            this.materialButton1.Size = new System.Drawing.Size(64, 36);
            this.materialButton1.TabIndex = 5;
            this.materialButton1.Text = "Exit";
            this.materialButton1.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.materialButton1.UseAccentColor = false;
            this.materialButton1.UseVisualStyleBackColor = true;
            this.materialButton1.Click += new System.EventHandler(this.materialButton1_Click);
            // 
            // btnActivate
            // 
            this.btnActivate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnActivate.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnActivate.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnActivate.Depth = 0;
            this.btnActivate.HighEmphasis = true;
            this.btnActivate.Icon = null;
            this.btnActivate.Location = new System.Drawing.Point(435, 373);
            this.btnActivate.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnActivate.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnActivate.Name = "btnActivate";
            this.btnActivate.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnActivate.Size = new System.Drawing.Size(128, 36);
            this.btnActivate.TabIndex = 4;
            this.btnActivate.Text = "Activate Now";
            this.btnActivate.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnActivate.UseAccentColor = false;
            this.btnActivate.UseVisualStyleBackColor = true;
            this.btnActivate.Click += new System.EventHandler(this.btnActivate_Click);
            // 
            // txtKey
            // 
            this.txtKey.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtKey.AnimateReadOnly = false;
            this.txtKey.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.txtKey.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.txtKey.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtKey.Depth = 0;
            this.txtKey.HideSelection = true;
            this.txtKey.Location = new System.Drawing.Point(17, 208);
            this.txtKey.MaxLength = 32767;
            this.txtKey.MouseState = MaterialSkin.MouseState.OUT;
            this.txtKey.Name = "txtKey";
            this.txtKey.PasswordChar = '\0';
            this.txtKey.ReadOnly = false;
            this.txtKey.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtKey.SelectedText = "";
            this.txtKey.SelectionLength = 0;
            this.txtKey.SelectionStart = 0;
            this.txtKey.ShortcutsEnabled = true;
            this.txtKey.Size = new System.Drawing.Size(546, 133);
            this.txtKey.TabIndex = 3;
            this.txtKey.TabStop = false;
            this.txtKey.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtKey.UseSystemPasswordChar = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 173);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(234, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Provide Your Activation Key Here ...";
            // 
            // txtActivationCode
            // 
            this.txtActivationCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtActivationCode.AnimateReadOnly = false;
            this.txtActivationCode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.txtActivationCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.txtActivationCode.Depth = 0;
            this.txtActivationCode.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtActivationCode.HideSelection = true;
            this.txtActivationCode.LeadingIcon = null;
            this.txtActivationCode.Location = new System.Drawing.Point(20, 43);
            this.txtActivationCode.MaxLength = 32767;
            this.txtActivationCode.MouseState = MaterialSkin.MouseState.OUT;
            this.txtActivationCode.Name = "txtActivationCode";
            this.txtActivationCode.PasswordChar = '\0';
            this.txtActivationCode.PrefixSuffixText = null;
            this.txtActivationCode.ReadOnly = true;
            this.txtActivationCode.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtActivationCode.SelectedText = "";
            this.txtActivationCode.SelectionLength = 0;
            this.txtActivationCode.SelectionStart = 0;
            this.txtActivationCode.ShortcutsEnabled = true;
            this.txtActivationCode.Size = new System.Drawing.Size(551, 48);
            this.txtActivationCode.TabIndex = 1;
            this.txtActivationCode.TabStop = false;
            this.txtActivationCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtActivationCode.TrailingIcon = null;
            this.txtActivationCode.UseSystemPasswordChar = false;
            // 
            // lblActivationCode
            // 
            this.lblActivationCode.AutoSize = true;
            this.lblActivationCode.Location = new System.Drawing.Point(17, 14);
            this.lblActivationCode.Name = "lblActivationCode";
            this.lblActivationCode.Size = new System.Drawing.Size(166, 17);
            this.lblActivationCode.TabIndex = 0;
            this.lblActivationCode.Text = "Your Activation Code is...";
            // 
            // Activate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(622, 552);
            this.Controls.Add(this.materialCard1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Activate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Activate";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Activate_FormClosed);
            this.Load += new System.EventHandler(this.Activate_Load);
            this.materialCard1.ResumeLayout(false);
            this.materialCard1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MaterialSkin.Controls.MaterialCard materialCard1;
        private System.Windows.Forms.Label lblActivationCode;
        private MaterialSkin.Controls.MaterialTextBox2 txtActivationCode;
        private System.Windows.Forms.Label label1;
        private MaterialSkin.Controls.MaterialMultiLineTextBox2 txtKey;
        private MaterialSkin.Controls.MaterialButton btnActivate;
        private MaterialSkin.Controls.MaterialButton materialButton1;
    }
}