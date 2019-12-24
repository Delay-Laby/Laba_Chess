using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Chess.Figures;
using System.Collections.Generic;
using System.Text.RegularExpressions;


namespace Chess.Network
{
    public enum NetWorkType { SERVER, CLIENT };

	public class BaseNetwork
	{
		//out stack 
		Queue<String> outCommands;
		//network thread
		protected Thread thread;
		//io socket
		protected Socket socket;
		//buffer size
		protected readonly Int32 BUFF_SIZE = 1024;
		//thread
		protected bool connected;
        //work 
        public bool IsWork = true;
        //buffer
        protected byte[] buffer;
        //network type
        public NetWorkType type;

        #region  events implementation
        //event - Figure Moved
        
        public delegate void MoveFigureEventHandler(MoveFigureEventArgs args);

        public event MoveFigureEventHandler FigureMoved;

        public class MoveFigureEventArgs : EventArgs
        {
            public Position OldPos;
            public Position NewPos;

            public MoveFigureEventArgs(Position oldPos, Position newPos)
            {
                OldPos = oldPos;
                NewPos = newPos;
            }
        }

        //event - Message Received
        public delegate void MessageRecivedHandler(MessageReceivedEventArgs args);
        
        public event MessageRecivedHandler MessageReceived;

        public class MessageReceivedEventArgs : EventArgs
        {
            public string Message;

            public MessageReceivedEventArgs(string msg)
            {
                Message = msg;
            }
        }
        
        //event - End game
        public delegate void GeneralNetworkHandler();

        public event GeneralNetworkHandler GameEndedEvent;
        public event GeneralNetworkHandler ConnectionEstablishedEvent;
        public event GeneralNetworkHandler DisconnectedEvent;
        #endregion
        //socket connected to client or to server property
		public bool IsConnected {
			get {
				return connected;
			}
		}
      
		public BaseNetwork ()
		{
			connected = false;
			outCommands = new Queue<String>();
            buffer = new byte[BUFF_SIZE];
		}
        //io socket cycle
        //waiting for input and output data
		protected virtual void SocketIO ()
		{
            IOHandler();
		}
        //reveiving\sendig data from\to socket stream
        protected virtual void IOHandler()
        {
            
            Debug.NewMessage(this.ToString() + " " + " connection established.");
            
            if(ConnectionEstablishedEvent != null )
                ConnectionEstablishedEvent();

            while (IsWork)
            {
                //recieveing data from socket
                if (socket.Available > 0)
                    ReceiveData();
                //sending data via socket
                if (outCommands.Count != 0)
                    SendCommand();
                if (!socket.Connected)
                    Disconnect();
                //waiting new data
                Thread.Sleep(10);
            }
           
        }
        
        //receive data from socket
		protected void ReceiveData ()
		{
            String[] strings = ReceiveCommand();
            foreach(string str in strings)
			    DataProcessing( str );
		}
        //receiving string from socket stream
		protected String[] ReceiveCommand ()
		{
			byte[] buffer = new byte[BUFF_SIZE];

			int bytes =0;

			while(bytes == 0)
				bytes = socket.Receive(buffer);

            Debug.NewMessage(this.ToString() + " " + "readed " + bytes + " bytes from socket");
			string readed = System.Text.Encoding.UTF8.GetString(buffer);
            Debug.NewMessage(this.ToString() + " " + readed);
			return readed.Split(NetworkDef.SPLITTER);
		}
        //get byte array as a UTF8 string
        protected byte[] StringToBuffer(string command)
        {
            return System.Text.Encoding.UTF8.GetBytes(command + NetworkDef.SPLITTER);
        }
        //writing string to socket stream
		protected void SendCommand (string command)
		{
			byte[] buffer = System.Text.Encoding.UTF8.GetBytes(command +NetworkDef.SPLITTER);
			socket.Send (buffer);
		}
        //get command from socket out queue and write to socket stream
		protected void SendCommand()
		{
			SendCommand( GetCommand() );
		}
        //disconnect from server\client
		public void Disconnect ()
		{
			if (!IsConnected) {
				return;
			} else {
				connected = false;
                IsWork = false;
                socket.Shutdown (SocketShutdown.Both);
				socket.Close ();
               if (DisconnectedEvent != null )
                DisconnectedEvent();
			}
		}
        //waiting for command from s
		protected void WaitCommand ()
		{
			while (outCommands.Count < 0) {
				Thread.Sleep(100);
				System.Console.Write (this.ToString() +": Wait for command");
			}
		}
        //parse incomming command string
		protected void DataProcessing(string received)
		{
                Debug.NewMessage(this.ToString() + " parsing command " + received);

				if(received.StartsWith(NetworkDef.MOVE))
				{
                    string pattern = NetworkDef.MOVE + "\\s(\\d)\\s(\\d)\\s(\\d)\\s(\\d)";
                    string[] parsed = Regex.Split(received, pattern);
                    
					Position oldPos = new Position( int.Parse(parsed[1]), int.Parse(parsed[2]));
                    Position newPos = new Position(int.Parse(parsed[3]), int.Parse(parsed[4]));
                    
                    if (FigureMoved != null)
                        FigureMoved(new MoveFigureEventArgs(oldPos, newPos));
				    
                    Console.WriteLine(Thread.CurrentThread.Name +" " + "figure moved" );
                }
				else if(received.StartsWith(NetworkDef.MSG))
				{
                    string pattern = NetworkDef.MSG + "\\s(.*)";
                    string[] parsed = Regex.Split(received, pattern);

                    if (MessageReceived != null)
                        MessageReceived(new MessageReceivedEventArgs(parsed[1]));
				}
				else if(received.StartsWith(NetworkDef.END))
				{

                    if (GameEndedEvent != null)
                        GameEndedEvent();
				}
                else if (received.StartsWith(NetworkDef.OK))
                {

                }
		}
       
        #region work with queue
        //add move figure command to queue
		public void Add_MoveFigure (Position oldPos, Position newPos)
		{
			string command = NetworkDef.MOVE +' ' +oldPos.X + ' ' +oldPos.Y
				+' ' +newPos.X +' ' +newPos.Y;

			lock (outCommands) {
				outCommands.Enqueue(command);
			}
		}
        //add change figure
        public void Add_ChangeFigure(Position pos, Figure figure, FigureColor color)
        {
            string command = NetworkDef.CHANGE + ' ' + pos.X + ' ' + pos.Y
                + ' ' + figure.ToString();

            lock (outCommands)
            {
                outCommands.Enqueue(command);
            }
        }
        //add send message command to queue
		public void Add_Message (string mes)
		{
			string command = NetworkDef.MSG +' ' +mes;

			lock (outCommands) {
				outCommands.Enqueue(command);
			}
		}
	    //add end command to queue
		public void Add_End (string mes)
		{
			string command = NetworkDef.END;

			lock (outCommands) {
				outCommands.Enqueue(command);
			}
		}
        //get command from queue
		protected string GetCommand()
		{
			    lock (outCommands) {
				    string r = outCommands.Dequeue();
				    return r;
			    }
           
        }
        #endregion
    }
}

