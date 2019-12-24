using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;


namespace Chess.Network
{
	public class NetworkClient: BaseNetwork
	{
        private IPAddress serverIP;

		public NetworkClient (): base()
		{
            type = NetWorkType.CLIENT;
		}
		public void ConnetcToServer (string ip)
		{
			if( IsConnected ) {
				throw new SocketException(255);
			}

            serverIP = IPAddress.Parse(ip);

     		//invoke intrerface notify
			connected = true;
			thread = new Thread( SocketIO );
			thread.IsBackground = true;
			thread.Priority = ThreadPriority.BelowNormal;
			thread.Name = "ClientThread";
			thread.Start ();
		}

        protected override void SocketIO()
        {
            
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(serverIP, NetworkDef.PORT);

            string r = ReceiveCommand()[0];

            if (r.Equals(NetworkDef.VERSION))
            {
                SendCommand(NetworkDef.OK);

            }
            else
            {
                SendCommand(NetworkDef.ERROR);
                Disconnect();
                return;
            }

            IOHandler();
        }
	}
}

