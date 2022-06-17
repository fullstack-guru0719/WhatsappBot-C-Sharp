namespace WaAutoReplyBot
{
    partial class AddRule
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddRule));
            this.materialCard1 = new MaterialSkin.Controls.MaterialCard();
            this.cboCondition = new MaterialSkin.Controls.MaterialComboBox();
            this.materialLabel1 = new System.Windows.Forms.Label();
            this.txtUserInput = new MaterialSkin.Controls.MaterialTextBox();
            this.materialLabel2 = new System.Windows.Forms.Label();
            this.materialLabel3 = new System.Windows.Forms.Label();
            this.materialCard2 = new MaterialSkin.Controls.MaterialCard();
            this.lstMessages = new MaterialSkin.Controls.MaterialListView();
            this.Message = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Attachments = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnDelete = new MaterialSkin.Controls.MaterialButton();
            this.materialButton3 = new MaterialSkin.Controls.MaterialButton();
            this.materialButton2 = new MaterialSkin.Controls.MaterialButton();
            this.materialButton1 = new MaterialSkin.Controls.MaterialButton();
            this.materialCheckbox1 = new MaterialSkin.Controls.MaterialCheckbox();
            this.materialCard1.SuspendLayout();
            this.materialCard2.SuspendLayout();
            this.SuspendLayout();
            // 
            // materialCard1
            // 
            this.materialCard1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.materialCard1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.materialCard1.Controls.Add(this.cboCondition);
            this.materialCard1.Controls.Add(this.materialLabel1);
            this.materialCard1.Controls.Add(this.txtUserInput);
            this.materialCard1.Depth = 0;
            this.materialCard1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialCard1.Location = new System.Drawing.Point(17, 129);
            this.materialCard1.Margin = new System.Windows.Forms.Padding(14);
            this.materialCard1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialCard1.Name = "materialCard1";
            this.materialCard1.Padding = new System.Windows.Forms.Padding(14);
            this.materialCard1.Size = new System.Drawing.Size(608, 178);
            this.materialCard1.TabIndex = 0;
            // 
            // cboCondition
            // 
            this.cboCondition.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboCondition.AutoResize = false;
            this.cboCondition.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cboCondition.Depth = 0;
            this.cboCondition.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cboCondition.DropDownHeight = 174;
            this.cboCondition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCondition.DropDownWidth = 121;
            this.cboCondition.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.cboCondition.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.cboCondition.FormattingEnabled = true;
            this.cboCondition.Hint = "Condition";
            this.cboCondition.IntegralHeight = false;
            this.cboCondition.ItemHeight = 43;
            this.cboCondition.Location = new System.Drawing.Point(237, 94);
            this.cboCondition.MaxDropDownItems = 4;
            this.cboCondition.MouseState = MaterialSkin.MouseState.OUT;
            this.cboCondition.Name = "cboCondition";
            this.cboCondition.Size = new System.Drawing.Size(335, 49);
            this.cboCondition.StartIndex = 0;
            this.cboCondition.TabIndex = 2;
            // 
            // materialLabel1
            // 
            this.materialLabel1.AutoSize = true;
            this.materialLabel1.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialLabel1.Location = new System.Drawing.Point(16, 108);
            this.materialLabel1.Name = "materialLabel1";
            this.materialLabel1.Size = new System.Drawing.Size(45, 19);
            this.materialLabel1.TabIndex = 1;
            this.materialLabel1.Text = "Which";
            // 
            // txtUserInput
            // 
            this.txtUserInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUserInput.AnimateReadOnly = false;
            this.txtUserInput.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtUserInput.Depth = 0;
            this.txtUserInput.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtUserInput.Hint = "User Send...";
            this.txtUserInput.LeadingIcon = null;
            this.txtUserInput.Location = new System.Drawing.Point(17, 17);
            this.txtUserInput.MaxLength = 50;
            this.txtUserInput.MouseState = MaterialSkin.MouseState.OUT;
            this.txtUserInput.Multiline = false;
            this.txtUserInput.Name = "txtUserInput";
            this.txtUserInput.Size = new System.Drawing.Size(555, 50);
            this.txtUserInput.TabIndex = 0;
            this.txtUserInput.Text = "";
            this.txtUserInput.TrailingIcon = null;
            // 
            // materialLabel2
            // 
            this.materialLabel2.AutoSize = true;
            this.materialLabel2.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialLabel2.Location = new System.Drawing.Point(14, 85);
            this.materialLabel2.Name = "materialLabel2";
            this.materialLabel2.Size = new System.Drawing.Size(23, 19);
            this.materialLabel2.TabIndex = 2;
            this.materialLabel2.Text = "If...";
            // 
            // materialLabel3
            // 
            this.materialLabel3.AutoSize = true;
            this.materialLabel3.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialLabel3.Location = new System.Drawing.Point(14, 332);
            this.materialLabel3.Name = "materialLabel3";
            this.materialLabel3.Size = new System.Drawing.Size(129, 19);
            this.materialLabel3.TabIndex = 3;
            this.materialLabel3.Text = "Then, Reply with...";
            // 
            // materialCard2
            // 
            this.materialCard2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.materialCard2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.materialCard2.Controls.Add(this.lstMessages);
            this.materialCard2.Depth = 0;
            this.materialCard2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialCard2.Location = new System.Drawing.Point(17, 381);
            this.materialCard2.Margin = new System.Windows.Forms.Padding(14);
            this.materialCard2.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialCard2.Name = "materialCard2";
            this.materialCard2.Padding = new System.Windows.Forms.Padding(14);
            this.materialCard2.Size = new System.Drawing.Size(608, 195);
            this.materialCard2.TabIndex = 4;
            // 
            // lstMessages
            // 
            this.lstMessages.AutoSizeTable = false;
            this.lstMessages.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.lstMessages.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstMessages.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Message,
            this.Attachments});
            this.lstMessages.Depth = 0;
            this.lstMessages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstMessages.FullRowSelect = true;
            this.lstMessages.Location = new System.Drawing.Point(14, 14);
            this.lstMessages.MinimumSize = new System.Drawing.Size(200, 100);
            this.lstMessages.MouseLocation = new System.Drawing.Point(-1, -1);
            this.lstMessages.MouseState = MaterialSkin.MouseState.OUT;
            this.lstMessages.MultiSelect = false;
            this.lstMessages.Name = "lstMessages";
            this.lstMessages.OwnerDraw = true;
            this.lstMessages.Size = new System.Drawing.Size(580, 167);
            this.lstMessages.TabIndex = 0;
            this.lstMessages.UseCompatibleStateImageBehavior = false;
            this.lstMessages.View = System.Windows.Forms.View.Details;
            this.lstMessages.DoubleClick += new System.EventHandler(this.lstMessages_DoubleClick);
            this.lstMessages.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstMessages_KeyDown);
            // 
            // Message
            // 
            this.Message.Text = "Message";
            this.Message.Width = 350;
            // 
            // Attachments
            // 
            this.Attachments.Text = "Attachments";
            this.Attachments.Width = 160;
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDelete.AutoSize = false;
            this.btnDelete.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnDelete.BackColor = System.Drawing.Color.DarkRed;
            this.btnDelete.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnDelete.Depth = 0;
            this.btnDelete.HighEmphasis = true;
            this.btnDelete.Icon = null;
            this.btnDelete.Location = new System.Drawing.Point(241, 607);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnDelete.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnDelete.Size = new System.Drawing.Size(146, 36);
            this.btnDelete.TabIndex = 8;
            this.btnDelete.Text = "Delete";
            this.btnDelete.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Text;
            this.btnDelete.UseAccentColor = false;
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Visible = false;
            this.btnDelete.Click += new System.EventHandler(this.materialButton4_Click);
            // 
            // materialButton3
            // 
            this.materialButton3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.materialButton3.AutoSize = false;
            this.materialButton3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.materialButton3.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.materialButton3.Depth = 0;
            this.materialButton3.HighEmphasis = true;
            this.materialButton3.Icon = null;
            this.materialButton3.Location = new System.Drawing.Point(496, 607);
            this.materialButton3.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.materialButton3.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialButton3.Name = "materialButton3";
            this.materialButton3.NoAccentTextColor = System.Drawing.Color.Empty;
            this.materialButton3.Size = new System.Drawing.Size(121, 36);
            this.materialButton3.TabIndex = 7;
            this.materialButton3.Text = "Save";
            this.materialButton3.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.materialButton3.UseAccentColor = false;
            this.materialButton3.UseVisualStyleBackColor = true;
            this.materialButton3.Click += new System.EventHandler(this.materialButton3_Click);
            // 
            // materialButton2
            // 
            this.materialButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.materialButton2.AutoSize = false;
            this.materialButton2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.materialButton2.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.materialButton2.Depth = 0;
            this.materialButton2.HighEmphasis = true;
            this.materialButton2.Icon = null;
            this.materialButton2.Location = new System.Drawing.Point(17, 607);
            this.materialButton2.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.materialButton2.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialButton2.Name = "materialButton2";
            this.materialButton2.NoAccentTextColor = System.Drawing.Color.Empty;
            this.materialButton2.Size = new System.Drawing.Size(146, 36);
            this.materialButton2.TabIndex = 6;
            this.materialButton2.Text = "Cancel";
            this.materialButton2.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Text;
            this.materialButton2.UseAccentColor = false;
            this.materialButton2.UseVisualStyleBackColor = true;
            this.materialButton2.Click += new System.EventHandler(this.materialButton2_Click);
            // 
            // materialButton1
            // 
            this.materialButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.materialButton1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.materialButton1.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.materialButton1.Depth = 0;
            this.materialButton1.HighEmphasis = true;
            this.materialButton1.Icon = null;
            this.materialButton1.Location = new System.Drawing.Point(496, 326);
            this.materialButton1.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.materialButton1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialButton1.Name = "materialButton1";
            this.materialButton1.NoAccentTextColor = System.Drawing.Color.Empty;
            this.materialButton1.Size = new System.Drawing.Size(121, 36);
            this.materialButton1.TabIndex = 5;
            this.materialButton1.Text = "Add Message";
            this.materialButton1.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Text;
            this.materialButton1.UseAccentColor = false;
            this.materialButton1.UseVisualStyleBackColor = true;
            this.materialButton1.Click += new System.EventHandler(this.materialButton1_Click);
            // 
            // materialCheckbox1
            // 
            this.materialCheckbox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.materialCheckbox1.AutoSize = true;
            this.materialCheckbox1.Depth = 0;
            this.materialCheckbox1.Location = new System.Drawing.Point(503, 80);
            this.materialCheckbox1.Margin = new System.Windows.Forms.Padding(0);
            this.materialCheckbox1.MouseLocation = new System.Drawing.Point(-1, -1);
            this.materialCheckbox1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialCheckbox1.Name = "materialCheckbox1";
            this.materialCheckbox1.ReadOnly = false;
            this.materialCheckbox1.Ripple = true;
            this.materialCheckbox1.Size = new System.Drawing.Size(122, 37);
            this.materialCheckbox1.TabIndex = 9;
            this.materialCheckbox1.Text = "Use fallback";
            this.materialCheckbox1.UseVisualStyleBackColor = true;
            this.materialCheckbox1.CheckedChanged += new System.EventHandler(this.materialCheckbox1_CheckedChanged);
            // 
            // AddRule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(642, 667);
            this.Controls.Add(this.materialCheckbox1);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.materialButton3);
            this.Controls.Add(this.materialButton2);
            this.Controls.Add(this.materialButton1);
            this.Controls.Add(this.materialCard2);
            this.Controls.Add(this.materialLabel3);
            this.Controls.Add(this.materialLabel2);
            this.Controls.Add(this.materialCard1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddRule";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add Rule";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AddRule_FormClosing);
            this.Load += new System.EventHandler(this.AddRule_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AddRule_KeyDown);
            this.materialCard1.ResumeLayout(false);
            this.materialCard1.PerformLayout();
            this.materialCard2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MaterialSkin.Controls.MaterialCard materialCard1;
        private MaterialSkin.Controls.MaterialComboBox cboCondition;
        private System.Windows.Forms.Label materialLabel1;
        private MaterialSkin.Controls.MaterialTextBox txtUserInput;
        private System.Windows.Forms.Label materialLabel2;
        private System.Windows.Forms.Label materialLabel3;
        private MaterialSkin.Controls.MaterialCard materialCard2;
        private MaterialSkin.Controls.MaterialListView lstMessages;
        private System.Windows.Forms.ColumnHeader Message;
        private System.Windows.Forms.ColumnHeader Attachments;
        private MaterialSkin.Controls.MaterialButton materialButton1;
        private MaterialSkin.Controls.MaterialButton materialButton2;
        private MaterialSkin.Controls.MaterialButton materialButton3;
        private MaterialSkin.Controls.MaterialButton btnDelete;
        private MaterialSkin.Controls.MaterialCheckbox materialCheckbox1;
    }
}