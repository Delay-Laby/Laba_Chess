using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Chess.Figures;
using Chess.Core;

namespace Chess.GUI
{
    public partial class PlayWindow : Form
    {
        private const int sqSize = 80;   // размер квадрата
        private const int offset = 25;   // отступ от края формы
        //Field 
        public GuiMatrix matrix = null;
        public readonly Color ColorSelected = Color.PaleGoldenrod;


        public enum MessageOwner
        {
            Player1,
            Player2,
            System
        };

        private System.Windows.Forms.Timer mouseTracker;

        private BufferedGraphics graph = null;
        private System.Drawing.Pen pen = null;
        private IGameControl control;
        private bool formBusy = false;

        private int p1Time = 0, p2Time = 0;


        public bool NetworkEnabled  {
            set  {
                if (value == true) {
                    commandLine.Visible = true;
                    sendButton.Visible = true;
                }
                else  {
                    commandLine.Visible = false;
                    sendButton.Visible = false;
                }
            }
        }

        public delegate void DrawAsyncDelegate(object sender, EventArgs e);

        public PlayWindow (IGameControl gameControl, string title, GuiMatrix guiMatrix)
		{
			this.Text = title;
			InitializeComponent ();
			matrix = guiMatrix;
			control = gameControl;
		}
          

        //draw chess field
        private void Draw(object sender, EventArgs e)
        {
            while (formBusy) Thread.Sleep(1);
            formBusy = true;
            if (matrix == null)
                return;
            if (e != null)
                Thread.Sleep((int)sender);
            Spot tmpSpot;
            pen.Color = this.BackColor;
            graph.Graphics.FillRectangle(pen.Brush, 0, 0, sqSize * 8 + offset + 1, sqSize * 8 + offset + 1);

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {

                    if ((tmpSpot = matrix.GetSpot(i, j)) == null)
                        continue;

                    //Выбор цвета квадрата
                    if (tmpSpot.Check)
                    {
                        if (tmpSpot.sColor != Color.White) pen.Color = Color.FromArgb(255, 100, 100);
                        else pen.Color = Color.FromArgb(255, 150, 150);
                    }
                    else if (tmpSpot.Highlighted)	//highlighted spot
                    {
                        if (tmpSpot.sColor != Color.White) pen.Color = Color.FromArgb(50, 100, 75);
                        else pen.Color = Color.FromArgb(100, 255, 125);
                    }
                    else if(tmpSpot.Selected) pen.Color = ColorSelected;	//selected spot
                    else pen.Color = tmpSpot.sColor;						//simple spot
                        

                    //отрисовка квадрата
                    if (tmpSpot.Focused)
                    {
                        pen.Color = Color.FromArgb(pen.Color.R - 20, pen.Color.G - 20, pen.Color.B - 20);
                        graph.Graphics.FillRectangle(pen.Brush, offset + sqSize * i, sqSize * j, sqSize, sqSize);

                        pen.Color = Color.FromArgb(pen.Color.R + 20, pen.Color.G + 20, pen.Color.B + 20);
                        graph.Graphics.FillRectangle(pen.Brush, offset + sqSize * i + 10, sqSize * j + 10, sqSize - 20, sqSize - 20);
                    }
                    else
                    {
                        graph.Graphics.FillRectangle(pen.Brush, offset + sqSize * i, sqSize * j, sqSize, sqSize);
                    }

                    //отрисовка изображения фигуры
                    if (tmpSpot.image != null)
                    {
                        DrawFigure(new Position(i, j), tmpSpot.image);
                    }
                }
            }

            //literal drawing
            pen.Color = Color.Gray;
            graph.Graphics.DrawRectangle(pen, offset, 0, sqSize * 8, sqSize * 8);
            pen.Color = Color.Black;
            char ch = 'A';
            for (int i = 0; i < 8; i++)
            {//вывод букв
                graph.Graphics.DrawString(Convert.ToString(ch), this.Font, pen.Brush, offset - 4 + sqSize / 2 + sqSize * i, sqSize * 8 + 5);
                ch++;
            }
            for (int i = 0; i < 8; i++)
            {//вывод цифр
                graph.Graphics.DrawString(Convert.ToString((i - 8) * (-1)), this.Font, pen.Brush, 8, sqSize / 2 - 4 + sqSize * i);
                ch++;
            }

            graph.Render();

            formBusy = false;
        }
        //draw image on field in p position
        private void DrawFigure(Position p, System.Drawing.Image img)
        {
            graph.Graphics.DrawImage(img, p.X * sqSize + offset, p.Y * sqSize);
        }
        // Sync drawing
        public void ReDraw()
        {
            Draw(null, null);
        }
        // Manual sync/async drawing
        public void ReDraw(bool async)
        {
            if (async)
            {
                DrawAsyncDelegate d = new DrawAsyncDelegate(Draw);
                d.BeginInvoke(null, null, null, null);
            }
            else
                Draw(null, null);
        }
        //Асинхронная отрисовкай с задержкой на delay милисекунд
        public void ReDraw(int delay)
        {
            DrawAsyncDelegate d = new DrawAsyncDelegate(Draw);
            d.BeginInvoke(delay, new EventArgs(), null, null);
        }
        //Window shown event handler
        private void PlayWindow_Shown(object sender, EventArgs e)
        {
            BufferedGraphicsContext graphContext = BufferedGraphicsManager.Current;
            graphContext.MaximumBuffer = new System.Drawing.Size(sqSize * 8 + offset + 1, sqSize * 8 + offset + 1);
            graph = graphContext.Allocate(this.CreateGraphics(), new Rectangle(0, 0, sqSize * 8 + offset + 1, sqSize * 8 + offset + 1));

            pen = new System.Drawing.Pen(Color.CadetBlue);

            mouseTracker = new System.Windows.Forms.Timer();
            mouseTracker.Interval = 40;
            mouseTracker.Tick += new EventHandler(PlayWindow_MouseTracking);
            mouseTracker.Start();

            ReDraw(50);
        }

        public void PlayerClock_Tick(Chess.Figures.FigureColor clr)
        {
            if (clr == FigureColor.WHITE)
            {
                p1Time++;
                Player1Time.Text = ((int)(p1Time / 60) < 10) ? '0' + Convert.ToString((int)(p1Time / 60)) : Convert.ToString((int)(p1Time / 60));
                Player1Time.Text += ':';
                Player1Time.Text += ((int)((p1Time / 60.0 - (int)(p1Time / 60)) * 60) < 10) ? '0' + Convert.ToString((int)((p1Time / 60.0 - (int)(p1Time / 60)) * 60)) : Convert.ToString((int)((p1Time / 60.0 - (int)(p1Time / 60)) * 60));
            }
            else
            {
                p2Time++;
                Player2Time.Text = ((int)(p2Time / 60) < 10) ? '0' + Convert.ToString((int)(p2Time / 60)) : Convert.ToString((int)(p2Time / 60));
                Player2Time.Text += ':';
                Player2Time.Text += ((int)((p2Time / 60.0 - (int)(p2Time / 60)) * 60) < 10) ? '0' + Convert.ToString((int)((p2Time / 60.0 - (int)(p2Time / 60)) * 60)) : Convert.ToString((int)((p2Time / 60.0 - (int)(p2Time / 60)) * 60));
            }
        }

        //Cursor moved event
        private void PlayWindow_MouseTracking(object sender, EventArgs e)
        {
            Point pt = PointToClient(Cursor.Position);
            pt.X -= offset;
            pt.X /= sqSize;
            pt.Y /= sqSize;

            if (pt.X >= 8 || pt.Y >= 8 || pt.X < 0 || pt.Y < 0 ||
                !(control.SpotFocused(new Position(pt.X, pt.Y)) ||
                (matrix.GetSpot(pt.X, pt.Y) != null && matrix.GetSpot(pt.X, pt.Y).Highlighted)))
                return;
            if (matrix.SetFocused(pt.X, pt.Y))
                ReDraw(true);

        }

        public void PrintToConsoleLn(string str, Color clr)
        {
            gameConsole.SelectionColor = clr;
            gameConsole.AppendText(str);
            gameConsole.ScrollToCaret();
        }
        public void PrintToConsole(string str, Color clr)
        {
            gameConsole.SelectionColor = clr;
            if (gameConsole.Text != "") gameConsole.AppendText(Environment.NewLine + str);
            else gameConsole.AppendText(str);
            gameConsole.ScrollToCaret();
        }



        public void PrintMessage(string str, MessageOwner owner)
        {
            switch (owner)
            {
                case MessageOwner.Player1:
                    PrintToConsole("Player1: ", Color.Green);
                    PrintToConsoleLn(str, Color.FromArgb(64, 128, 255));
                    break;

                case MessageOwner.Player2:
                    PrintToConsole("Player2: ", Color.Green);
                    PrintToConsoleLn(str, Color.FromArgb(128, 64, 255));
                    break;

                case MessageOwner.System:
                    PrintToConsole("System: ", Color.Red);
                    PrintToConsoleLn(str, Color.Black);
                    break;
            }
        }

        //Mouse down event handler
        private void PlayWindow_Click(object sender, MouseEventArgs e)
        {
            Point pt = PointToClient(Cursor.Position);

            pt.X -= offset;
            pt.X /= sqSize;
            pt.Y /= sqSize;

            if (pt.X >= 8 || pt.Y >= 8 || pt.X < 0 || pt.Y < 0)
                return;

            Position mouseClickedPos = new Position(pt.X, pt.Y);

            if (control.SpotFocused(mouseClickedPos))
            {
                //invoke interface method
                control.SpotSelected(mouseClickedPos);
            }
            ReDraw(true);
        }
        //If window moved. Window events handlers
        private void PlayWindow_Move(object sender, EventArgs e)
        {
            if (graph != null)
                ReDraw(true);
        }
        //If window deactivated
        private void PlayWindow_Deactivate(object sender, EventArgs e)
        {
            if (mouseTracker != null)
                mouseTracker.Stop();
            if (graph != null)
                ReDraw(true);
        }
        //If windows activated
        private void PlayWindow_Activated(object sender, EventArgs e)
        {
            if (mouseTracker != null && !mouseTracker.Enabled)
                mouseTracker.Start();
            if (graph != null)
                ReDraw(true);
        }

        private void sendButton_Click(object sender, EventArgs e)
        {
            PrintToConsole("Player1: ", Color.Green);
            PrintToConsoleLn(commandLine.Text, Color.FromArgb(64, 128, 255));
            commandLine.Text = "";
            control.MessageReceived(commandLine.Text);
        }

        private void commandLine_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                sendButton_Click(sender, e);
            }
        }
        public Point GetPosOnScreen(Position pt)
        {
            Point wpt = PointToScreen(new Point(pt.X*sqSize + sqSize / 2 + offset, pt.Y*sqSize + sqSize / 2));
            if (pt.X > 4) wpt.X -= 2 * sqSize;
            if (pt.Y > 4) wpt.Y -= 2 * sqSize;
            return wpt;
        }
    }
}
