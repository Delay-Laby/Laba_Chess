using System;

namespace Chess.Network
{
	public class NetworkDef
	{
		public static String OK= "OK";
		public static String ERROR = "ERROR";
		public static String MOVE = "MOVE";
		public static String END = "END";
		public static String MSG ="MSG";
        public static String CHANGE = "CHANGE";
		public static String VERSION = "VERSION_"+System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
		public static char SPLITTER = '\n';
		public static Int16 PORT = 6080;
	}
}

