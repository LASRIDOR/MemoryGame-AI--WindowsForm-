namespace UI
{
    public partial class FormLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLogin));
            this.SecondPlayerNameTextBox = new System.Windows.Forms.TextBox();
            this.FirstPlayerNameLabel = new System.Windows.Forms.Label();
            this.SecondPlayerNameLabel = new System.Windows.Forms.Label();
            this.FirstPlayerNameTextBox = new System.Windows.Forms.TextBox();
            this.StartButtom = new System.Windows.Forms.Button();
            this.AgainstAFriendButtom = new System.Windows.Forms.Button();
            this.BoardSizeLabel = new System.Windows.Forms.Label();
            this.BoardSizeButtom = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // SecondPlayerNameTextBox
            // 
            this.SecondPlayerNameTextBox.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            resources.ApplyResources(this.SecondPlayerNameTextBox, "SecondPlayerNameTextBox");
            this.SecondPlayerNameTextBox.Name = "SecondPlayerNameTextBox";
            this.SecondPlayerNameTextBox.UseWaitCursor = true;
            // 
            // FirstPlayerNameLabel
            // 
            resources.ApplyResources(this.FirstPlayerNameLabel, "FirstPlayerNameLabel");
            this.FirstPlayerNameLabel.ForeColor = System.Drawing.Color.Black;
            this.FirstPlayerNameLabel.Name = "FirstPlayerNameLabel";
            // 
            // SecondPlayerNameLabel
            // 
            resources.ApplyResources(this.SecondPlayerNameLabel, "SecondPlayerNameLabel");
            this.SecondPlayerNameLabel.ForeColor = System.Drawing.Color.Black;
            this.SecondPlayerNameLabel.Name = "SecondPlayerNameLabel";
            // 
            // FirstPlayerNameTextBox
            // 
            resources.ApplyResources(this.FirstPlayerNameTextBox, "FirstPlayerNameTextBox");
            this.FirstPlayerNameTextBox.Name = "FirstPlayerNameTextBox";
            // 
            // StartButtom
            // 
            this.StartButtom.BackColor = System.Drawing.Color.Lime;
            this.StartButtom.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.StartButtom, "StartButtom");
            this.StartButtom.Name = "StartButtom";
            this.StartButtom.UseVisualStyleBackColor = false;
            this.StartButtom.Click += new System.EventHandler(this.StartButtom_Click);
            // 
            // AgainstAFriendButtom
            // 
            resources.ApplyResources(this.AgainstAFriendButtom, "AgainstAFriendButtom");
            this.AgainstAFriendButtom.Name = "AgainstAFriendButtom";
            this.AgainstAFriendButtom.UseVisualStyleBackColor = true;
            this.AgainstAFriendButtom.Click += new System.EventHandler(this.AgainstFriend_click);
            // 
            // BoardSizeLabel
            // 
            resources.ApplyResources(this.BoardSizeLabel, "BoardSizeLabel");
            this.BoardSizeLabel.Name = "BoardSizeLabel";
            // 
            // BoardSizeButtom
            // 
            this.BoardSizeButtom.BackColor = System.Drawing.Color.MediumSlateBlue;
            resources.ApplyResources(this.BoardSizeButtom, "BoardSizeButtom");
            this.BoardSizeButtom.Name = "BoardSizeButtom";
            this.BoardSizeButtom.UseVisualStyleBackColor = false;
            this.BoardSizeButtom.Click += new System.EventHandler(this.BoardSizeButtom_Click);
            // 
            // FormLogin
            // 
            this.AcceptButton = this.StartButtom;
            this.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.AllowDrop = true;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.BoardSizeButtom);
            this.Controls.Add(this.BoardSizeLabel);
            this.Controls.Add(this.AgainstAFriendButtom);
            this.Controls.Add(this.StartButtom);
            this.Controls.Add(this.FirstPlayerNameTextBox);
            this.Controls.Add(this.SecondPlayerNameLabel);
            this.Controls.Add(this.FirstPlayerNameLabel);
            this.Controls.Add(this.SecondPlayerNameTextBox);
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormLogin";
            this.ShowIcon = false;
            this.Tag = string.Empty;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormLogin_FormClosing);
            this.Load += new System.EventHandler(this.FormLogin_Load);
            this.Enter += new System.EventHandler(this.BoardSizeButtom_Click);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox SecondPlayerNameTextBox;
        private System.Windows.Forms.Label FirstPlayerNameLabel;
        private System.Windows.Forms.Label SecondPlayerNameLabel;
        private System.Windows.Forms.TextBox FirstPlayerNameTextBox;
        private System.Windows.Forms.Button StartButtom;
        private System.Windows.Forms.Button AgainstAFriendButtom;
        private System.Windows.Forms.Label BoardSizeLabel;
        private System.Windows.Forms.Button BoardSizeButtom;
    }
}