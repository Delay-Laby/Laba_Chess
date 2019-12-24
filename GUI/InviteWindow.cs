using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Chess.Core.User;

namespace Chess.GUI
{
    public partial class InviteWindow : Form
    {
        public delegate void OnChoiceEventHandler(object sender, OnChoiceEventArgs e);
        public event OnChoiceEventHandler OnChoice;
        private bool choiceMade = false;
        private delegate void LoadingDelegate(int delay);
        private LoadingDelegate loading;
        private ProfileCollection pCollection = null;

        public InviteWindow(ProfileCollection profileCollection)
        {
            pCollection = profileCollection;
            InitializeComponent();
            label1.Visible = false;
            IPBox.Visible = false;
            ConnectButton.Visible = false;
            CancelButton.Visible = false;
            StartServerButton.Visible = false;
            StartClientButton.Visible = false;
            loading = new LoadingDelegate(Loading);
        }
        void Loading(int delay)
        {
            while (choiceMade)
            {
                status.Text = "Connecting";
                Thread.Sleep(delay);
                status.Text = "Connecting.";
                Thread.Sleep(delay);
                status.Text = "Connecting..";
                Thread.Sleep(delay);
                status.Text = "Connecting...";
                Thread.Sleep(delay);
            }
            status.Text = "Cancelled";
        }

        private void OfflineGameButton_Click(object sender, EventArgs e)
        {
            if (OnChoice != null)
            {
                OnChoice(this, new OnChoiceEventArgs(OnChoiceEventArgs.ConnectionType.OFFLINE, null));
                choiceMade = true;
            }
            Close();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            if (OnChoice != null)
            {
                OnChoice(this, new OnChoiceEventArgs(OnChoiceEventArgs.ConnectionType.EXIT, null));
                choiceMade = true;
            }
            Close();
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            if (OnChoice != null)
            {
                OnChoice(this, new OnChoiceEventArgs(OnChoiceEventArgs.ConnectionType.CLIENT, IPBox.Text));
                choiceMade = true;
            }
            loading.BeginInvoke(500, null, null);
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            label1.Visible = false;
            IPBox.Visible = false;
            ConnectButton.Visible = false;
            CancelButton.Visible = false;
            StartServerButton.Visible = false;
            StartClientButton.Visible = false;

            OfflineGameButton.Visible = true;
            OnlineGameButton.Visible = true;
            ExitButton.Visible = true;
            choiceMade = false;
        }

        private void StartServerButton_Click(object sender, EventArgs e)
        {
            StartClientButton.Visible = false;
            if (OnChoice != null)
            {
                OnChoice(this, new OnChoiceEventArgs(OnChoiceEventArgs.ConnectionType.SERVER, null)); 
                choiceMade = true;
            }
            loading.BeginInvoke(500, null, null);
        }

        private void StartClientButton_Click(object sender, EventArgs e)
        {
            StartServerButton.Visible = false;
            StartClientButton.Visible = false;

            label1.Visible = true;
            IPBox.Visible = true;
            ConnectButton.Visible = true;
            CancelButton.Visible = true;
        }

        private void OnlineGameButton_Click(object sender, EventArgs e)
        {
            OfflineGameButton.Visible = false;
            OnlineGameButton.Visible = false;
            ExitButton.Visible = false;

            StartServerButton.Visible = true;
            StartClientButton.Visible = true;
            CancelButton.Visible = true;
        }

        private void InviteWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!choiceMade && OnChoice != null)
                OnChoice(this, new OnChoiceEventArgs(OnChoiceEventArgs.ConnectionType.EXIT, null));
            choiceMade = false;
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            if (LoginButton.Text == "Login" && pCollection != null)
            {
                User.ProfileWindow pw = new User.ProfileWindow(pCollection);
                pw.ShowDialog();
                if (pw.rv != null)
                {
                    LoginButton.Text = "Logout";
                    this.Text = "Chess: " + pw.rv.NickName;
                }
            }
            else
            {
                this.Text = "Chess";
                LoginButton.Text = "Login";
            }
        }
    }

    public class OnChoiceEventArgs: EventArgs
    {
        public enum ConnectionType { SERVER, CLIENT, OFFLINE, EXIT };
        private ConnectionType type;
        public ConnectionType Type { get { return type; } }
        private string ip = null;
        public string IP { get { return ip; } }
        public OnChoiceEventArgs(ConnectionType connectionType, string InternetProtocol)
        {
            ip = InternetProtocol;
            type = connectionType;
        }
    }
}
