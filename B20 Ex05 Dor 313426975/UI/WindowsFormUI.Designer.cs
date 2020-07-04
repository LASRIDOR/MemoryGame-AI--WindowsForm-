namespace UI
{
    partial class MemoryGameUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MemoryGameUI));
            this.SecondPlayerNameTextBox = new System.Windows.Forms.TextBox();
            this.FirstPlayerNameLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SecondPlayerNameLabel = new System.Windows.Forms.Label();
            this.FirstPlayerNameTextBox = new System.Windows.Forms.TextBox();
            this.StartButtom = new System.Windows.Forms.Button();
            this.AgainstAFriendButtom = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.BoardSizeButtom = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // SecondPlayerNameTextBox
            // 
            this.SecondPlayerNameTextBox.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            resources.ApplyResources(this.SecondPlayerNameTextBox, "SecondPlayerNameTextBox");
            this.SecondPlayerNameTextBox.Name = "SecondPlayerNameTextBox";
            this.SecondPlayerNameTextBox.UseWaitCursor = true;
            this.SecondPlayerNameTextBox.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // FirstPlayerNameLabel
            // 
            resources.ApplyResources(this.FirstPlayerNameLabel, "FirstPlayerNameLabel");
            this.FirstPlayerNameLabel.ForeColor = System.Drawing.Color.Black;
            this.FirstPlayerNameLabel.Name = "FirstPlayerNameLabel";
            this.FirstPlayerNameLabel.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
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
            this.FirstPlayerNameTextBox.TextChanged += new System.EventHandler(this.textBox1_TextChanged_1);
            // 
            // StartButtom
            // 
            this.StartButtom.BackColor = System.Drawing.Color.Lime;
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
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            this.label1.Click += new System.EventHandler(this.label1_Click_1);
            // 
            // BoardSizeButtom
            // 
            this.BoardSizeButtom.BackColor = System.Drawing.Color.MediumSlateBlue;
            resources.ApplyResources(this.BoardSizeButtom, "BoardSizeButtom");
            this.BoardSizeButtom.Name = "BoardSizeButtom";
            this.BoardSizeButtom.UseVisualStyleBackColor = false;
            this.BoardSizeButtom.Click += new System.EventHandler(this.BoardSizeButtom_Click);
            // 
            // MemoryGameUI
            // 
            this.AcceptButton = this.StartButtom;
            this.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.AllowDrop = true;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.BoardSizeButtom);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.AgainstAFriendButtom);
            this.Controls.Add(this.StartButtom);
            this.Controls.Add(this.FirstPlayerNameTextBox);
            this.Controls.Add(this.SecondPlayerNameLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.FirstPlayerNameLabel);
            this.Controls.Add(this.SecondPlayerNameTextBox);
            this.ForeColor = System.Drawing.Color.Black;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MemoryGameUI";
            this.ShowIcon = false;
            this.Tag = "";
            this.Load += new System.EventHandler(this.WindowsFormUI_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox SecondPlayerNameTextBox;
        private System.Windows.Forms.Label FirstPlayerNameLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label SecondPlayerNameLabel;
        private System.Windows.Forms.TextBox FirstPlayerNameTextBox;
        private System.Windows.Forms.Button StartButtom;
        private System.Windows.Forms.Button AgainstAFriendButtom;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BoardSizeButtom;
    }
}