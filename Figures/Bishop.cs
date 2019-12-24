using System.Collections.Generic;
using Chess.Core;

namespace Chess
{
	namespace Figures
	{

		public class Bishop: Figure
		{
            public Bishop(FigureColor color)
                : base(color)
			{
				diff = false;
				LoadBitmap("bishop");
			}

            public override List<Position> GetAvailableMovePossitons(Position currentPos)
			{
				List<Position> available = new List<Position> ();


				//checking available positions
				int x = currentPos.X+1;
				int y = currentPos.Y+1;

				while( IsInField( x, y) )
					available.Add ( new Position( x++, y++) );

				x = currentPos.X-1;
				y = currentPos.Y+1;

				while( IsInField( x, y) )
					available.Add ( new Position( x--, y++) );

				x = currentPos.X-1;
				y = currentPos.Y-1;

				while( IsInField( x, y) )
					available.Add ( new Position( x--, y--) );

				x = currentPos.X+1;
				y = currentPos.Y-1;

				while( IsInField( x, y) )
					available.Add ( new Position( x++, y--) );

				return available;
			}

            public override List<Position> GetAvailableAtackPositons(Position currentPos, CoreMatrix matrix)
            {
                List<Position> available = new List<Position>();


                //checking available positions
                int x = currentPos.X + 1;
                int y = currentPos.Y + 1;

                while (IsInField(x, y))
                {
                    if (matrix.FigureAt(x, y) != null)
                    {
                        if (matrix.FigureAt(x, y).Color != this.Color)
                        {
                            available.Add(new Position(x, y));
                            break;
                        }
                        else break;
                    }
                    else available.Add(new Position(x++, y++));
                }

                x = currentPos.X - 1;
                y = currentPos.Y + 1;

                while (IsInField(x, y))
                {
                    if (matrix.FigureAt(x, y) != null)
                    {
                        if (matrix.FigureAt(x, y).Color != this.Color)
                        {
                            available.Add(new Position(x, y));
                            break;
                        }
                        else break;
                    }
                    else available.Add(new Position(x--, y++));
                }

                x = currentPos.X - 1;
                y = currentPos.Y - 1;

                while (IsInField(x, y))
                {
                    if (matrix.FigureAt(x, y) != null)
                    {
                        if (matrix.FigureAt(x, y).Color != this.Color)
                        {
                            available.Add(new Position(x, y));
                            break;
                        }
                        else break;
                    }
                    else available.Add(new Position(x--, y--));
                }

                x = currentPos.X + 1;
                y = currentPos.Y - 1;

                while (IsInField(x, y))
                {
                    if (matrix.FigureAt(x, y) != null)
                    {
                        if (matrix.FigureAt(x, y).Color != this.Color)
                        {
                            available.Add(new Position(x, y));
                            break;
                        }
                        else break;
                    }
                    else available.Add(new Position(x++, y--));
                }

                return available;
            }

            public override string ToString()
            {
                return "bishop";
            }
		}
	}
}
