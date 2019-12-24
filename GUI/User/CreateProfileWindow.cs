using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Chess.Core.User;

namespace Chess.GUI.User
{
    public partial class CreateProfileWindow : Form
    {
        public Profile rv = null;

        public CreateProfileWindow()
        {
            InitializeComponent();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            rv = new Profile(NicknameBox.Text, EmailBox.Text);
            Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
