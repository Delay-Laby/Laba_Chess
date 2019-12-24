using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Chess.GUI;
using Chess.GUI.User;
using Chess.Core.User;
using Chess.Figures;
using Chess.Network;
using System.Threading;

namespace Chess.Core
{
    class GameCore : IGameControl
    {
        private PlayWindow playWindow;
        private InviteWindow inviteWindow;
        private CoreMatrix matrix;
        private Chess.Figures.FigureColor runColor;
        private Position figurePos;
        private PlayersState pState;
        private bool endGameLock, strokeLock;
        private bool networkGame;
        private BaseNetwork network;
        private ProfileCollection pCollection = null;
        private System.Windows.Forms.Timer PlayerClock;

        public GameCore()
        {
            Application.EnableVisualStyles();
            runColor = FigureColor.WHITE;
            pState = new PlayersState();
            pCollection = new ProfileCollection("Users.db");
        }

        public void Initialize()
        {
            matrix = new CoreMatrix();
            playWindow = new PlayWindow(this, "Chess", new GuiMatrix(matrix));
            playWindow.FormClosed += new FormClosedEventHandler(PlayWindowClose);

            PlayerClock = new System.Windows.Forms.Timer();
            PlayerClock.Tick += new EventHandler(PlayerClock_Tick);
            PlayerClock.Interval = 1000;

            inviteWindow = new InviteWindow(pCollection);
            inviteWindow.OnChoice += new InviteWindow.OnChoiceEventHandler(InviteWindowMessageReceived);
            inviteWindow.Show();
            Application.Run();
        }

        //for console game start initialization
        public void Initialize(string ip)
        {

            matrix = new CoreMatrix();
            playWindow = new PlayWindow(this, "Chess", new GuiMatrix(matrix));
            playWindow.FormClosed += new FormClosedEventHandler(PlayWindowClose);

            PlayerClock = new System.Windows.Forms.Timer();
            PlayerClock.Tick += new EventHandler(PlayerClock_Tick);
            PlayerClock.Interval = 1000;


            if (ip == "0.0.0.0")
                StartServer();
            else
                StartClient(ip);

            PlayerClock.Start();
            playWindow.Show();

            Debug.NewMessage("play window show");


            Application.Run();
        }

        public void PlayerClock_Tick(Object sender, EventArgs e)
        {
            playWindow.PlayerClock_Tick(runColor);
        }

        private void ReInitialize()
        {
            runColor = FigureColor.WHITE;
            matrix = new CoreMatrix();
            playWindow = new PlayWindow(this, "Chess", new GuiMatrix(matrix));
            playWindow.FormClosed += new FormClosedEventHandler(PlayWindowClose);

            inviteWindow = new InviteWindow(pCollection);
            inviteWindow.OnChoice += new InviteWindow.OnChoiceEventHandler(InviteWindowMessageReceived);
            inviteWindow.Show();
        }

        private void CheckForMate(FigureColor kingColor)
        {
            if (King.IsCheckState(matrix, kingColor))
            {
                int solutionsCount = 0;
                //checking all figures for king check or mate state
                for (int i = 0; i < 8; i++)
                    for (int j = 0; j < 8; j++)
                    {
                        Position curPos = new Position(i, j);
                        if (matrix.HasFigureAt(curPos))
                        {
                            Figure figure = matrix.FigureAt(curPos);

                            if (figure.Color == kingColor)
                            {
                                solutionsCount += figure.GetAvailableOnCheckPositons(curPos, matrix).Count;
                            }
                        }
                    }

                if (solutionsCount > 0)
                {
                    pState.SetState(kingColor, PlayerState.CHECK);
                    Console.WriteLine("King in danger! ");
                }
                else
                { //if solutions count leater than zero - is mate state
                    pState.SetState(kingColor, PlayerState.CHECKMATE);
                    Console.WriteLine("King have some trouble! ");
                }
                playWindow.matrix.SetChecked(matrix.GetKing(kingColor));
            }
        }

        private void CheckForMate()
        {
            Program.Debug("Checking state...");
            pState.ResetGameState();

            CheckForMate(FigureColor.BLACK);
            CheckForMate(FigureColor.WHITE);

            PlayerState black = pState.GetState(FigureColor.BLACK);
            PlayerState white = pState.GetState(FigureColor.WHITE);


            if (black == PlayerState.CHECK)
            {
                playWindow.PrintToConsole("System: ", System.Drawing.Color.Red);
                playWindow.PrintToConsoleLn("Player 2: check!", System.Drawing.Color.Black);
                if (network != null && pCollection.Current != null) pCollection.Current.IncrementScore();
            }
            else if (black == PlayerState.CHECKMATE)
            {
                playWindow.PrintToConsole("System: ", System.Drawing.Color.Red);
                playWindow.PrintToConsoleLn("Player 2: mate!", System.Drawing.Color.Black);
                if (network != null && pCollection.Current != null && runColor == FigureColor.BLACK) pCollection.Current.GameWon(0);
                if (network != null && pCollection.Current != null && runColor == FigureColor.WHITE) pCollection.Current.GameLost(0);
                MessageBox.Show("Player 2 mate!");
            }

            if (white == PlayerState.CHECK)
            {
                playWindow.PrintToConsole("System: ", System.Drawing.Color.Red);
                playWindow.PrintToConsoleLn("Player 2: check!", System.Drawing.Color.Black);
                if (network != null && pCollection.Current != null) pCollection.Current.IncrementScore();
            }
            else if (white == PlayerState.CHECKMATE)
            {
                playWindow.PrintToConsole("System: ", System.Drawing.Color.Red);
                playWindow.PrintToConsoleLn("Player 1: mate!", System.Drawing.Color.Black);
                if (network != null && pCollection.Current != null && runColor == FigureColor.WHITE) pCollection.Current.GameWon(0);
                if (network != null && pCollection.Current != null && runColor == FigureColor.BLACK) pCollection.Current.GameLost(0);
                MessageBox.Show("Player 1 mate!");
            }

            if (pState.GetState(FigureColor.BLACK) == PlayerState.CHECKMATE ||
                pState.GetState(FigureColor.WHITE) == PlayerState.CHECKMATE)
                EndGame();
        }

        private void StartLocal()
        {
            playWindow.NetworkEnabled = false;
            strokeLock = false;
            endGameLock = false;
            networkGame = false;
            PlayerClock.Start();
            playWindow.Show();
        }

        private void EndGame()
        {
            endGameLock = true;

            if (network != null)
            {
                network.Disconnect();
                network = null;
            }
        }

        private void MoveFigure(Position oldPos, Position newPos)
        {
            //changing play window cursor
            playWindow.Cursor = Cursors.WaitCursor;

            //rock change implementation
            if (newPos.Equals(matrix.GetKing(runColor)) && !matrix.FigureAt(matrix.GetKing(runColor)).IsMoved)
            {

                int dx = oldPos.X - newPos.X;
                dx = (dx > 0) ? (1) : (-1);

                Position kingPos = new Position(newPos.X + dx * 2, newPos.Y);
                Position rockPos = new Position(newPos.X + dx, newPos.Y);

                matrix.MoveFigure(newPos, kingPos);
                matrix.FigureAt(kingPos).IncreaseSteps();
                playWindow.matrix.MoveImage(newPos, kingPos);

                matrix.MoveFigure(oldPos, rockPos);
                matrix.FigureAt(rockPos).IncreaseSteps();
                playWindow.matrix.MoveImage(oldPos, rockPos);

                //print message in system log console
                playWindow.PrintToConsole("System: ", System.Drawing.Color.Red);
                playWindow.PrintToConsoleLn("King-rock change", System.Drawing.Color.Green);

            }
            else
            { //simple figure move
                //print message in system log console

                if (runColor == FigureColor.WHITE)
                {
                    playWindow.PrintToConsole("System: ", System.Drawing.Color.Red);
                    playWindow.PrintToConsoleLn("Player1 moved " + matrix.FigureAt(oldPos).ToString() + " from " +
                        (char)('A' + oldPos.X) + Convert.ToString(8 - oldPos.Y) + " to " +
                        (char)('A' + newPos.X) + Convert.ToString(8 - newPos.Y), System.Drawing.Color.FromArgb(64, 128, 255));
                }
                else
                {
                    playWindow.PrintToConsole("System: ", System.Drawing.Color.Red);
                    playWindow.PrintToConsoleLn("Player2 moved " + matrix.FigureAt(oldPos).ToString() + " from " +
                        (char)('A' + oldPos.X) + Convert.ToString(8 - oldPos.Y) + " to " +
                        (char)('A' + newPos.X) + Convert.ToString(8 - newPos.Y), System.Drawing.Color.FromArgb(128, 64, 255));

                }
                Figure figure = matrix.FigureAt(oldPos);
                matrix.MoveFigure(oldPos, newPos);
                playWindow.matrix.MoveImage(oldPos, newPos);
                figure.IncreaseSteps();

                //checking pawn move gap
                if (figure is Pawn)
                {
                    ((Pawn)figure).TwoStepState = (Math.Abs(oldPos.Y - newPos.Y) == 2);

                    //Проверка на замену пешки другой фигурой
                    if ((runColor == FigureColor.WHITE && newPos.Y == 0) || (runColor == FigureColor.BLACK && newPos.Y == 7))
                    {
                        if ((network.type == NetWorkType.SERVER && runColor == FigureColor.WHITE) || (network.type == NetWorkType.CLIENT && runColor == FigureColor.BLACK))
                        {
                            FigureChoiceWindow w = new FigureChoiceWindow(playWindow.GetPosOnScreen(newPos), runColor);
                            w.ShowDialog(playWindow);
                            matrix.SetFigure(w.Result, newPos);
                            playWindow.matrix.SetImage(w.Result.image, newPos);
                            playWindow.ReDraw(true);
                        }
                    }
                    else
                    {
                        GetInPass(oldPos, newPos);
                        ((Pawn)matrix.FigureAt(newPos)).NeighborsFigures[1] = matrix.FigureAt(newPos.X + 1, newPos.Y);
                        ((Pawn)matrix.FigureAt(newPos)).NeighborsFigures[0] = matrix.FigureAt(newPos.X - 1, newPos.Y);
                    }
                }



            }
            //changing color changing
            runColor = (runColor == FigureColor.WHITE) ? (FigureColor.BLACK) : (FigureColor.WHITE);
            CheckForMate();
            playWindow.matrix.ResetAllAttribures();
            playWindow.Cursor = Cursors.Default;
            playWindow.ReDraw();
        }

        private void GetInPass(Position oldPos, Position newPos)
        {
            int k = newPos.X - oldPos.X;

            if (matrix.HasFigureAt(oldPos.X + k, oldPos.Y))
            {
                Position neighPos = new Position(oldPos.X + k, oldPos.Y);
                Figure neighFigure = matrix.FigureAt(neighPos);
                Pawn ourPawn = matrix.FigureAt(newPos) as Pawn;
                //neighbors figure - enemy figure
                if (neighFigure.Color != matrix.FigureAt(newPos).Color)
                {
                    //last state neighbors figure is not Pawn
                    // if k == -1 - it is left neighbors
                    // if k == 1 - it is right neighbors
                    int neighborsIndex = (k == -1) ? 0 : 1;

                    if (ourPawn.NeighborsFigures[neighborsIndex] == null ||
                        (ourPawn.NeighborsFigures[neighborsIndex] != null &&
                        !(ourPawn.NeighborsFigures[neighborsIndex] is Pawn)))
                    {
                        //It's enemy Pawn
                        if (neighFigure is Pawn)
                        {
                            //Enemy pawn moved two step
                            if (((Pawn)neighFigure).TwoStepState && (neighFigure).StepCount == 1)
                            {
                                matrix.SetFigure(null, neighPos);
                                playWindow.matrix.SetImage(null, neighPos);
                            }
                        }
                    }
                }
            }
        }

        private void RegisterNetEventHandlers()
        {
            network.FigureMoved += FigureMovedHandler;
            network.MessageReceived += MessageReceivedHandler;
            network.GameEndedEvent += GameEnded;
            network.ConnectionEstablishedEvent += ConnectedHandler;
            network.DisconnectedEvent += Disconnectedhandler;
        }

        private void StartServer()
        {
            playWindow.NetworkEnabled = true;
            strokeLock = true;
            endGameLock = false;
            playWindow.Text = "Chess - Server";
            networkGame = true;
            
            playWindow.Show();

            try
            {
                network = new NetworkServer();
                RegisterNetEventHandlers();

                ((NetworkServer)network).StartServer();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Server Error");
                Debug.NewMessage(this.ToString() + e.Message);
            }

            Debug.NewMessage(this.ToString() + " serve initalizated...");
        }

        private void StartClient(string ip)
        {
            playWindow.NetworkEnabled = true;

            strokeLock = false;
            endGameLock = false;
            networkGame = true;
            playWindow.Text = "Chess - Client";
            playWindow.Show();

            Debug.NewMessage(this.ToString() + " connecting to server " + ip);
            try
            {
                strokeLock = true;
                network = new NetworkClient();
                RegisterNetEventHandlers();
                ((NetworkClient)network).ConnetcToServer(ip);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error");
                Debug.NewMessage(this.ToString() + " " + e.Message);
            }
        }

        #region gui delegates
        private void PlayWindowClose(Object o, FormClosedEventArgs e)
        {
            PlayerClock.Stop();
            ReInitialize();
        }

        private void InviteWindowMessageReceived(Object o, OnChoiceEventArgs e)
        {
            switch (e.Type)
            {
                case OnChoiceEventArgs.ConnectionType.OFFLINE:
                    StartLocal();
                    break;
                case OnChoiceEventArgs.ConnectionType.SERVER:
                    StartServer();
                    break;
                case OnChoiceEventArgs.ConnectionType.CLIENT:
                    StartClient(e.IP);
                    break;

                case OnChoiceEventArgs.ConnectionType.EXIT:
                    Application.Exit();
                    break;
            }
        }
        #endregion

        #region Network base events handlers
        public void MessageReceivedHandler(BaseNetwork.MessageReceivedEventArgs args)
        {
            playWindow.Invoke(new MethodInvoker(delegate
            {
                playWindow.PrintMessage(args.Message, (network.type == NetWorkType.SERVER) ? (PlayWindow.MessageOwner.Player1) : (PlayWindow.MessageOwner.Player2));
            }));
            Debug.NewMessage("Message received: " + args.Message);
        }

        public void FigureMovedHandler(BaseNetwork.MoveFigureEventArgs args)
        {
            playWindow.Invoke(new MethodInvoker(delegate
            {
                MoveFigure(args.OldPos, args.NewPos);
                strokeLock = false;
            }));

            Console.WriteLine("Figure moved handler called");
            Debug.NewMessage("Figure moved called by " + Thread.CurrentThread.Name);
        }

        public void GameEnded()
        {
            EndGame();
        }

        public void Disconnectedhandler()
        {
            Debug.NewMessage(this.ToString() + " " + " disconnected " + Thread.CurrentThread.Name);
            inviteWindow.Invoke(new MethodInvoker(delegate
            {
                inviteWindow.Show();

                playWindow.Close();
                MessageBox.Show("Network", "Disconnected");

                PlayerClock.Stop();
            }));

        }

        public void ConnectedHandler()
        {
            Debug.NewMessage(this.ToString() + " " + " connected " + Thread.CurrentThread.Name);
            
            inviteWindow.Invoke(new MethodInvoker(delegate
            {
                //accept for move figure
                if (network.type == NetWorkType.SERVER)
                    strokeLock = false;

                inviteWindow.Close();
                PlayerClock.Start();
            }));

        }

        #endregion

        #region IGameControl implementation
        public void FigureMoved(Position oldPos, Position newPos)
        {

        }

        public void SpotSelected(Position spotPos)
        {
            if (endGameLock || strokeLock)
                return;

            Program.Debug("Square selected: " + spotPos.X + ' ' + spotPos.Y);

            //action: move figure handler
            if (playWindow.matrix.IsHighlighted(spotPos) && spotPos != figurePos)
            {
                //send network message to client\server
                if (networkGame)
                {
                    network.Add_MoveFigure(figurePos, spotPos);
                    strokeLock = true;
                }
                MoveFigure(figurePos, spotPos);
                return;
            }

            //action: selecet figure and highlight spots handler
            if (matrix.HasFigureAt(spotPos))
            {
                Figure figure = matrix.FigureAt(spotPos);

                if (figure.Color == runColor && playWindow.matrix.SetSelected(spotPos))
                {
                    playWindow.matrix.SetHighlighted(figure.GetAvailableOnCheckPositons(spotPos, matrix));
                    figurePos = spotPos;
                }
            }

        }

        public bool SpotFocused(Position spotPos)
        {
            if (endGameLock || strokeLock)
                return false;

            if ((matrix.HasFigureAt(spotPos) && matrix.FigureAt(spotPos).Color == runColor)
                || playWindow.matrix.GetSpot(spotPos).Highlighted)
                return true;
            else
                return false;
        }

        public void MessageReceived(String message)
        {
            network.Add_Message(message);
        }

        #endregion
    }
}
