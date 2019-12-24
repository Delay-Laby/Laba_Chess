using System;
using Chess.Core;

namespace Chess
{
	namespace Figures
	{
		public enum FigureColor
		{
			BLACK,
			WHITE }
		;

		public class Position: Object
		{
			public Position (int x, int y)
			{
				this.x = x;
				this.y = y;
				errFlag = false;
                if (x > 10 || y > 10) throw new WrongPositionException(this);
			}

			public Position ()
			{
				this.x = 0;
				this.y = 0;
				errFlag = false;
			}

			private bool errFlag;

			public bool errorFlag { 
				get {
					bool tmp = errFlag;
					errFlag = false;
					return tmp;
				}
			}

			private int x;

			public int X {
				get {
					return x;
				}
				set {
                    if (value < 8 && value >= 0)
                    {
                        x = value;
                        errFlag = false;
                    }
                    else
                    {
                        errFlag = true;
                    }
				}
			}

			private int y;

			public int Y {
				get {
					return y;
				}
				set {
					if (value < 8 && value >= 0) {
						y = value;
						errFlag = false;
					} else
						errFlag = true;
				}
			}

			public override bool Equals (object obj)
			{
				return (X == ((Position)obj).X) && (Y == ((Position)obj).Y);
			}

			public override int GetHashCode ()
			{
				return 0;
			}

		}
	
		public enum PlayerState
		{
			NORMAL,
			CHECK,
			CHECKMATE }
		;

		public class PlayersState
		{
			public PlayersState ()
			{
				black = PlayerState.NORMAL;
				white = PlayerState.NORMAL;
			}

			private PlayerState black;
			private PlayerState white;

			public PlayerState GetState (FigureColor color)
			{
				if (color == FigureColor.WHITE) {
					return white;
				} else {
					return black;
				}
			}

			public void SetState(FigureColor color, PlayerState state)
			{
				if (color == FigureColor.WHITE) {
					white = state;
				} else {
					black = state;
				}
			}

			public void ResetGameState ()
			{
				white = PlayerState.NORMAL;
				black = PlayerState.NORMAL;
			}


		}
	}
}
