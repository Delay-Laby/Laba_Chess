using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Chess.Core;
using Chess.Core.User;

namespace Chess.GUI.User
{
    public partial class ProfileWindow : Form
    {
        public Profile rv = null;
        private ProfileCollection pCollection;
        public ProfileWindow(ProfileCollection lst)
        {
            pCollection = lst;
            InitializeComponent();
            foreach (Profile item in pCollection)
            {
                ProfileData.Rows.Add(item.NickName, item.eMail, item.Score, item.WinCount, item.LossCount);
            }
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            CreateProfileWindow cpw = new CreateProfileWindow();
            cpw.ShowDialog();
            if (cpw.rv != null)
            {
                pCollection.Add(cpw.rv);
                ProfileData.Rows.Add(cpw.rv.NickName, cpw.rv.eMail, cpw.rv.Score, cpw.rv.WinCount, cpw.rv.LossCount);
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (pCollection.Count == 0) return;
            pCollection.Remove(new Profile((string)ProfileData.SelectedRows[0].Cells[0].Value, (string)ProfileData.SelectedRows[0].Cells[1].Value));
            ProfileData.Rows.Remove(ProfileData.SelectedRows[0]);
        }

        private void SelectButton_Click(object sender, EventArgs e)
        {
            if (pCollection.Count == 0) return;
            Profile tmp = new Profile((string)ProfileData.SelectedRows[0].Cells[0].Value, (string)ProfileData.SelectedRows[0].Cells[1].Value);
            foreach (Profile item in pCollection)
            {
                if (item.Equals(tmp))
                {
                    rv = item;
                    break;
                }
            }
            Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ProfileWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            pCollection.Save("Users.db");
        }
    }
}
