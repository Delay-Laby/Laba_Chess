namespace Chess.GUI.User
{
    partial class ProfileWindow
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
            this.ProfileData = new System.Windows.Forms.DataGridView();
            this.Nickname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.eMail = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Score = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Win_count = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Loss_count = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AddButton = new System.Windows.Forms.Button();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.SelectButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ProfileData)).BeginInit();
            this.SuspendLayout();
            // 
            // ProfileData
            // 
            this.ProfileData.AllowUserToAddRows = false;
            this.ProfileData.AllowUserToDeleteRows = false;
            this.ProfileData.AllowUserToResizeRows = false;
            this.ProfileData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ProfileData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Nickname,
            this.eMail,
            this.Score,
            this.Win_count,
            this.Loss_count});
            this.ProfileData.Dock = System.Windows.Forms.DockStyle.Left;
            this.ProfileData.Location = new System.Drawing.Point(0, 0);
            this.ProfileData.MultiSelect = false;
            this.ProfileData.Name = "ProfileData";
            this.ProfileData.ReadOnly = true;
            this.ProfileData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ProfileData.Size = new System.Drawing.Size(614, 427);
            this.ProfileData.TabIndex = 0;
            // 
            // Nickname
            // 
            this.Nickname.Frozen = true;
            this.Nickname.HeaderText = "Nickname";
            this.Nickname.Name = "Nickname";
            this.Nickname.ReadOnly = true;
            this.Nickname.Width = 120;
            // 
            // eMail
            // 
            this.eMail.FillWeight = 150F;
            this.eMail.Frozen = true;
            this.eMail.HeaderText = "e-mail";
            this.eMail.Name = "eMail";
            this.eMail.ReadOnly = true;
            this.eMail.Width = 150;
            // 
            // Score
            // 
            this.Score.FillWeight = 80F;
            this.Score.Frozen = true;
            this.Score.HeaderText = "Score";
            this.Score.Name = "Score";
            this.Score.ReadOnly = true;
            // 
            // Win_count
            // 
            this.Win_count.FillWeight = 80F;
            this.Win_count.Frozen = true;
            this.Win_count.HeaderText = "Win";
            this.Win_count.Name = "Win_count";
            this.Win_count.ReadOnly = true;
            // 
            // Loss_count
            // 
            this.Loss_count.FillWeight = 80F;
            this.Loss_count.Frozen = true;
            this.Loss_count.HeaderText = "Loss";
            this.Loss_count.Name = "Loss_count";
            this.Loss_count.ReadOnly = true;
            // 
            // AddButton
            // 
            this.AddButton.Location = new System.Drawing.Point(643, 21);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(75, 23);
            this.AddButton.TabIndex = 1;
            this.AddButton.Text = "Add";
            this.AddButton.UseVisualStyleBackColor = true;
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // DeleteButton
            // 
            this.DeleteButton.Location = new System.Drawing.Point(643, 50);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(75, 23);
            this.DeleteButton.TabIndex = 2;
            this.DeleteButton.Text = "Delete";
            this.DeleteButton.UseVisualStyleBackColor = true;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // SelectButton
            // 
            this.SelectButton.Location = new System.Drawing.Point(643, 79);
            this.SelectButton.Name = "SelectButton";
            this.SelectButton.Size = new System.Drawing.Size(75, 23);
            this.SelectButton.TabIndex = 3;
            this.SelectButton.Text = "Select";
            this.SelectButton.UseVisualStyleBackColor = true;
            this.SelectButton.Click += new System.EventHandler(this.SelectButton_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.Location = new System.Drawing.Point(643, 392);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 4;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // ProfileWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(742, 427);
            this.ControlBox = false;
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.SelectButton);
            this.Controls.Add(this.DeleteButton);
            this.Controls.Add(this.AddButton);
            this.Controls.Add(this.ProfileData);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "ProfileWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ProfileWindow";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ProfileWindow_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.ProfileData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView ProfileData;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nickname;
        private System.Windows.Forms.DataGridViewTextBoxColumn eMail;
        private System.Windows.Forms.DataGridViewTextBoxColumn Score;
        private System.Windows.Forms.DataGridViewTextBoxColumn Win_count;
        private System.Windows.Forms.DataGridViewTextBoxColumn Loss_count;
        private System.Windows.Forms.Button AddButton;
        private System.Windows.Forms.Button DeleteButton;
        private System.Windows.Forms.Button SelectButton;
        private System.Windows.Forms.Button CancelButton;
    }
}