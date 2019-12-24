#define DEBUG

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Chess.Core;
using System.Threading;

namespace Chess
{
    static class Program
    {
        static void Main(string[] arguments)
        {

            Thread.CurrentThread.Name = "MainThread";

			GameCore game = new GameCore();
#if DEBUG
            System.Console.WriteLine("Game initialization");
#endif

			if(arguments.Count () < 1)
				game.Initialize();
			else if(arguments[0] == "client")
				game.Initialize(arguments[1]);
			else if(arguments[0]=="server")
				game.Initialize("0.0.0.0");
		}

		static public void Debug( string msg )
		{
#if DEBUG
			System.Console.WriteLine( msg );
#endif
		}

    }
}
