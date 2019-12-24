namespace Chess.GUI
{
    partial class InviteWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InviteWindow));
            this.OfflineGameButton = new System.Windows.Forms.Button();
            this.OnlineGameButton = new System.Windows.Forms.Button();
            this.ExitButton = new System.Windows.Forms.Button();
            this.layoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.StartServerButton = new System.Windows.Forms.Button();
            this.StartClientButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.IPBox = new System.Windows.Forms.TextBox();
            this.ConnectButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.status = new System.Windows.Forms.Label();
            this.LoginButton = new System.Windows.Forms.Button();
            this.layoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // OfflineGameButton
            // 
            this.OfflineGameButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.OfflineGameButton.AutoSize = true;
            this.OfflineGameButton.Location = new System.Drawing.Point(3, 3);
            this.OfflineGameButton.Name = "OfflineGameButton";
            this.OfflineGameButton.Size = new System.Drawing.Size(125, 25);
            this.OfflineGameButton.TabIndex = 0;
            this.OfflineGameButton.Text = "Start offline game";
            this.OfflineGameButton.UseVisualStyleBackColor = true;
            this.OfflineGameButton.Click += new System.EventHandler(this.OfflineGameButton_Click);
            // 
            // OnlineGameButton
            // 
            this.OnlineGameButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.OnlineGameButton.AutoSize = true;
            this.OnlineGameButton.Location = new System.Drawing.Point(3, 34);
            this.OnlineGameButton.Name = "OnlineGameButton";
            this.OnlineGameButton.Size = new System.Drawing.Size(125, 25);
            this.OnlineGameButton.TabIndex = 1;
            this.OnlineGameButton.Text = "Start online game";
            this.OnlineGameButton.UseVisualStyleBackColor = true;
            this.OnlineGameButton.Click += new System.EventHandler(this.OnlineGameButton_Click);
            // 
            // ExitButton
            // 
            this.ExitButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ExitButton.AutoSize = true;
            this.ExitButton.Location = new System.Drawing.Point(3, 65);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(125, 25);
            this.ExitButton.TabIndex = 2;
            this.ExitButton.Text = "Exit";
            this.ExitButton.UseVisualStyleBackColor = true;
            this.ExitButton.Click += new System.EventHandler(this.ExitButton_Click);
            // 
            // layoutPanel
            // 
            this.layoutPanel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.layoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.layoutPanel.Controls.Add(this.OfflineGameButton);
            this.layoutPanel.Controls.Add(this.OnlineGameButton);
            this.layoutPanel.Controls.Add(this.ExitButton);
            this.layoutPanel.Controls.Add(this.StartServerButton);
            this.layoutPanel.Controls.Add(this.StartClientButton);
            this.layoutPanel.Controls.Add(this.label1);
            this.layoutPanel.Controls.Add(this.IPBox);
            this.layoutPanel.Controls.Add(this.ConnectButton);
            this.layoutPanel.Controls.Add(this.CancelButton);
            this.layoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.layoutPanel.Location = new System.Drawing.Point(27, 26);
            this.layoutPanel.Name = "layoutPanel";
            this.layoutPanel.Size = new System.Drawing.Size(131, 126);
            this.layoutPanel.TabIndex = 5;
            // 
            // StartServerButton
            // 
            this.StartServerButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.StartServerButton.AutoSize = true;
            this.StartServerButton.Location = new System.Drawing.Point(3, 96);
            this.StartServerButton.Name = "StartServerButton";
            this.StartServerButton.Size = new System.Drawing.Size(125, 25);
            this.StartServerButton.TabIndex = 5;
            this.StartServerButton.Text = "Start Server";
            this.StartServerButton.UseVisualStyleBackColor = true;
            this.StartServerButton.Click += new System.EventHandler(this.StartServerButton_Click);
            // 
            // StartClientButton
            // 
            this.StartClientButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.StartClientButton.AutoSize = true;
            this.StartClientButton.Location = new System.Drawing.Point(134, 3);
            this.StartClientButton.Name = "StartClientButton";
            this.StartClientButton.Size = new System.Drawing.Size(125, 25);
            this.StartClientButton.TabIndex = 5;
            this.StartClientButton.Text = "Start Client";
            this.StartClientButton.UseVisualStyleBackColor = true;
            this.StartClientButton.Click += new System.EventHandler(this.StartClientButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(134, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Enter IP adress:";
            // 
            // IPBox
            // 
            this.IPBox.Location = new System.Drawing.Point(134, 47);
            this.IPBox.Name = "IPBox";
            this.IPBox.Size = new System.Drawing.Size(125, 20);
            this.IPBox.TabIndex = 4;
            // 
            // ConnectButton
            // 
            this.ConnectButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ConnectButton.AutoSize = true;
            this.ConnectButton.Location = new System.Drawing.Point(134, 73);
            this.ConnectButton.Name = "ConnectButton";
            this.ConnectButton.Size = new System.Drawing.Size(125, 25);
            this.ConnectButton.TabIndex = 5;
            this.ConnectButton.Text = "Connect";
            this.ConnectButton.UseVisualStyleBackColor = true;
            this.ConnectButton.Click += new System.EventHandler(this.ConnectButton_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.CancelButton.AutoSize = true;
            this.CancelButton.Location = new System.Drawing.Point(265, 3);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(125, 25);
            this.CancelButton.TabIndex = 6;
            this.CancelButton.Text = "Canсel";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // status
            // 
            this.status.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.status.AutoSize = true;
            this.status.Location = new System.Drawing.Point(1, 187);
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(0, 13);
            this.status.TabIndex = 6;
            // 
            // LoginButton
            // 
            this.LoginButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.LoginButton.Location = new System.Drawing.Point(107, 177);
            this.LoginButton.Name = "LoginButton";
            this.LoginButton.Size = new System.Drawing.Size(75, 23);
            this.LoginButton.TabIndex = 7;
            this.LoginButton.Text = "Login";
            this.LoginButton.UseVisualStyleBackColor = true;
            this.LoginButton.Click += new System.EventHandler(this.LoginButton_Click);
            // 
            // InviteWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(184, 202);
            this.Controls.Add(this.LoginButton);
            this.Controls.Add(this.status);
            this.Controls.Add(this.layoutPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(200, 200);
            this.Name = "InviteWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chess";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.InviteWindow_FormClosed);
            this.layoutPanel.ResumeLayout(false);
            this.layoutPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button OfflineGameButton;
        private System.Windows.Forms.Button OnlineGameButton;
        private System.Windows.Forms.Button ExitButton;
        private System.Windows.Forms.FlowLayoutPanel layoutPanel;
        private System.Windows.Forms.Button StartServerButton;
        private System.Windows.Forms.Button StartClientButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox IPBox;
        private System.Windows.Forms.Button ConnectButton;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Label status;
        private System.Windows.Forms.Button LoginButton;

    }
}