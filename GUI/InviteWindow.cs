using System;
using System.Windows.Forms;
using System.IO;
using Chess.Figures;
using Chess.Core;


namespace Chess.GUI
{
    public partial class InviteWindow : Form
    {
        public delegate void OnChoiceEventHandler(object sender, OnChoiceEventArgs e);
        public event OnChoiceEventHandler OnChoice;
        private bool choiceMade = false;
        private delegate void LoadingDelegate(int delay);
      
       
        public InviteWindow()
        {
         
            InitializeComponent();
           
        }

        private void OfflineGameButton_Click(object sender, EventArgs e)
        {
           
            if (OnChoice != null)
            {
                OnChoice(this, new OnChoiceEventArgs(OnChoiceEventArgs.GameType.OFFLINE));
                choiceMade = true;
            }
            Close();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {

            Close();
        }

        private void InviteWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!choiceMade && OnChoice != null)
                OnChoice(this, new OnChoiceEventArgs(OnChoiceEventArgs.GameType.EXIT));
            choiceMade = false;
            
        }

        private void Skin_Click(object sender, EventArgs e)
        {
            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
                string path = folderBrowser.SelectedPath;
                string folder = new DirectoryInfo(path).Name;

                Figure.Skin = folder;

            }
        }
    }


    public class OnChoiceEventArgs: EventArgs
    {
        public enum GameType { OFFLINE, EXIT };
        private GameType gtype;
        public GameType Type { get { return gtype; } }
                public OnChoiceEventArgs(GameType connectionType)
        {
            
            gtype = connectionType;
        }
    }
}
