namespace Chess.GUI
{
    partial class PlayWindow
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlayWindow));
            this.gameConsole = new System.Windows.Forms.RichTextBox();
            this.commandLine = new System.Windows.Forms.TextBox();
            this.sendButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Player1Time = new System.Windows.Forms.Label();
            this.Player2Time = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // gameConsole
            // 
            this.gameConsole.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gameConsole.BackColor = System.Drawing.SystemColors.Window;
            this.gameConsole.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.gameConsole.Cursor = System.Windows.Forms.Cursors.Default;
            this.gameConsole.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.gameConsole.Location = new System.Drawing.Point(667, 0);
            this.gameConsole.Margin = new System.Windows.Forms.Padding(5, 3, 3, 3);
            this.gameConsole.MaxLength = 1000000;
            this.gameConsole.Name = "gameConsole";
            this.gameConsole.ReadOnly = true;
            this.gameConsole.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.gameConsole.Size = new System.Drawing.Size(316, 560);
            this.gameConsole.TabIndex = 100;
            this.gameConsole.TabStop = false;
            this.gameConsole.Text = "";
            // 
            // commandLine
            // 
            this.commandLine.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.commandLine.Location = new System.Drawing.Point(667, 561);
            this.commandLine.Name = "commandLine";
            this.commandLine.Size = new System.Drawing.Size(242, 20);
            this.commandLine.TabIndex = 0;
            this.commandLine.Visible = false;
            this.commandLine.KeyDown += new System.Windows.Forms.KeyEventHandler(this.commandLine_KeyDown);
            // 
            // sendButton
            // 
            this.sendButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.sendButton.Location = new System.Drawing.Point(909, 560);
            this.sendButton.Name = "sendButton";
            this.sendButton.Size = new System.Drawing.Size(75, 22);
            this.sendButton.TabIndex = 1;
            this.sendButton.Text = "Send";
            this.sendButton.UseVisualStyleBackColor = true;
            this.sendButton.Visible = false;
            this.sendButton.Click += new System.EventHandler(this.sendButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(675, 587);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 101;
            this.label1.Text = "Player 1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(819, 587);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 102;
            this.label2.Text = "Player 2";
            // 
            // Player1Time
            // 
            this.Player1Time.AutoSize = true;
            this.Player1Time.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Player1Time.Location = new System.Drawing.Point(671, 601);
            this.Player1Time.Name = "Player1Time";
            this.Player1Time.Size = new System.Drawing.Size(102, 39);
            this.Player1Time.TabIndex = 103;
            this.Player1Time.Text = "00:00";
            // 
            // Player2Time
            // 
            this.Player2Time.AutoSize = true;
            this.Player2Time.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Player2Time.Location = new System.Drawing.Point(815, 601);
            this.Player2Time.Name = "Player2Time";
            this.Player2Time.Size = new System.Drawing.Size(102, 39);
            this.Player2Time.TabIndex = 104;
            this.Player2Time.Text = "00:00";
            // 
            // PlayWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(984, 666);
            this.Controls.Add(this.Player2Time);
            this.Controls.Add(this.Player1Time);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.sendButton);
            this.Controls.Add(this.commandLine);
            this.Controls.Add(this.gameConsole);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "PlayWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Activated += new System.EventHandler(this.PlayWindow_Activated);
            this.Deactivate += new System.EventHandler(this.PlayWindow_Deactivate);
            this.Shown += new System.EventHandler(this.PlayWindow_Shown);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PlayWindow_Click);
            this.Move += new System.EventHandler(this.PlayWindow_Move);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox gameConsole;
        private System.Windows.Forms.TextBox commandLine;
        private System.Windows.Forms.Button sendButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label Player1Time;
        private System.Windows.Forms.Label Player2Time;


    }
}

