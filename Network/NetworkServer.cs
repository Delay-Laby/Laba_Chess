using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Chess.Network
{
	public class NetworkServer: BaseNetwork
	{
		private Socket tcpListener;

		public NetworkServer (): base()
		{
            type = NetWorkType.SERVER;
		}

		public void StartServer()
		{
    		thread = new Thread(SocketIO);
			thread.Name = "ServerThread";
			thread.IsBackground = true;
			thread.Priority = ThreadPriority.BelowNormal;
			thread.Start();
		}

		protected override void SocketIO ()
		{
            tcpListener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            tcpListener.Bind(new IPEndPoint(IPAddress.Any, NetworkDef.PORT));
            tcpListener.Listen(1);

			socket = tcpListener.Accept ();
			tcpListener.Close ();

			SendCommand (NetworkDef.VERSION);

			if ( ReceiveCommand ()[0] == NetworkDef.OK) {
			} else {
				
				return;
			}
 
            IOHandler();
		}
			
}
}