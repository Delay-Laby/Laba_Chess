

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
        [STAThread]
        static void Main(string[] arguments)
        {
            GameCore game = new GameCore();
           
            game.Initialize();
           
        }

    }
}
