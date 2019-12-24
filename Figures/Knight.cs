using System.Collections.Generic;
using Chess.Core;

namespace Chess
{
    namespace Figures
	{

		public class Knight: Figure
		{
            public Knight(FigureColor color)
                : base(color)
			{
				diff = false;
				LoadBitmap("knight");
			}

            public override List<Position> GetAvailableMovePossitons(Position currentPos)
			{
				List<Position> available = new List<Position> ();

				int x;
				int y;

				//search algoritm
				x = currentPos.X + 1;
				y = currentPos.Y + 2;
				if (IsInField (x, y)) 
					available.Add (new Position(x, y));

				x=currentPos.X-1;
				if(IsInField(x, y))
					available.Add ( new Position(x, y) );

				y=currentPos.Y-2;
				x=currentPos.X+1;
				if( IsInField(x, y) )
					available.Add ( new Position(x, y) );

				x=currentPos.X-1;
				if( IsInField(x, y) )
					available.Add ( new Position(x, y) );

				x=currentPos.X+2;
				y=currentPos.Y+1;
				if(IsInField(x, y))
					available.Add ( new Position(x, y) );

				y=currentPos.Y-1;
				if( IsInField(x, y) )
					available.Add ( new Position(x,y) );

				x=currentPos.X-2;
				y=currentPos.Y+1;
				if(IsInField(x, y))
					available.Add ( new Position(x, y) );

				y=currentPos.Y-1;
				if(IsInField(x, y))
					available.Add ( new Position(x, y) );
				//end search algoritm

				return available;
			}
            public override List<Position> GetAvailableAtackPositons(Position currentPos, CoreMatrix matrix)
            {
                List<Position> available = new List<Position> ();

				int x;
				int y;

				//search algoritm
				x = currentPos.X + 1;
				y = currentPos.Y + 2;
				if (IsInField (x, y) && (matrix.FigureAt(x, y) == null || matrix.FigureAt(x, y).Color != this.Color)) 
					available.Add (new Position(x, y));
			
				x=currentPos.X-1;
                if (IsInField(x, y) && (matrix.FigureAt(x, y) == null || matrix.FigureAt(x, y).Color != this.Color))
					available.Add ( new Position(x, y) );

				y=currentPos.Y-2;
				x=currentPos.X+1;
                if (IsInField(x, y) && (matrix.FigureAt(x, y) == null || matrix.FigureAt(x, y).Color != this.Color))
					available.Add ( new Position(x, y) );

				x=currentPos.X-1;
                if (IsInField(x, y) && (matrix.FigureAt(x, y) == null || matrix.FigureAt(x, y).Color != this.Color))
					available.Add ( new Position(x, y) );

				x=currentPos.X+2;
				y=currentPos.Y+1;
                if (IsInField(x, y) && (matrix.FigureAt(x, y) == null || matrix.FigureAt(x, y).Color != this.Color))
					available.Add ( new Position(x, y) );

				y=currentPos.Y-1;
                if (IsInField(x, y) && (matrix.FigureAt(x, y) == null || matrix.FigureAt(x, y).Color != this.Color))
					available.Add ( new Position(x,y) );

				x=currentPos.X-2;
				y=currentPos.Y+1;
                if (IsInField(x, y) && (matrix.FigureAt(x, y) == null || matrix.FigureAt(x, y).Color != this.Color))
					available.Add ( new Position(x, y) );

				y=currentPos.Y-1;
                if (IsInField(x, y) && (matrix.FigureAt(x, y) == null || matrix.FigureAt(x, y).Color != this.Color))
					available.Add ( new Position(x, y) );
				//end search algoritm

				return available;
            }
            public override string ToString()
            {
                return "knight";
            }
		}
	}
}

