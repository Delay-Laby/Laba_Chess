using System.Collections.Generic;
using Chess.Core;

namespace Chess
{
	namespace Figures
	{

		public class Queen: Figure
		{
            public Queen(FigureColor color)
                : base(color)
			{
				diff = false;
				LoadBitmap("queen");
			}

            public override List<Position> GetAvailableMovePossitons(Position currentPos)
			{
				List<Position> available = new List<Position> ();
				//checking for vertical and horizontal move
				for (int i=0; i<8; i++) {
					if(currentPos.X!=i)
						available.Add( new Position(i, currentPos.Y) );
					if(currentPos.Y!= i)
						available.Add( new Position(currentPos.X, i) );
				}
				//end checking


				//for dioganaly move
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

				//end checking
				return available;
			}

            public override List<Position> GetAvailableAtackPositons(Position currentPos, CoreMatrix matrix)
            {
                List<Position> available = new List<Position>();
                //checking for vertical and horizontal move
                for (int i = currentPos.X + 1; i < 8; i++)
                {
                    if (matrix.FigureAt(i, currentPos.Y) == null) available.Add(new Position(i, currentPos.Y));
                    else if (matrix.FigureAt(i, currentPos.Y).Color != this.Color)
                    {
                        available.Add(new Position(i, currentPos.Y));
                        break;
                    }
                    else break;
                }
                for (int i = currentPos.X - 1; i >= 0; i--)
                {
                    if (matrix.FigureAt(i, currentPos.Y) == null) available.Add(new Position(i, currentPos.Y));
                    else if (matrix.FigureAt(i, currentPos.Y).Color != this.Color)
                    {
                        available.Add(new Position(i, currentPos.Y));
                        break;
                    }
                    else break;
                }
                for (int j = currentPos.Y + 1; j < 8; j++)
                {
                    if (matrix.FigureAt(currentPos.X, j) == null) available.Add(new Position(currentPos.X, j));
                    else if (matrix.FigureAt(currentPos.X, j).Color != this.Color)
                    {
                        available.Add(new Position(currentPos.X, j));
                        break;
                    }
                    else break;
                }
                for (int j = currentPos.Y - 1; j >= 0; j--)
                {
                    if (matrix.FigureAt(currentPos.X, j) == null) available.Add(new Position(currentPos.X, j));
                    else if (matrix.FigureAt(currentPos.X, j).Color != this.Color)
                    {
                        available.Add(new Position(currentPos.X, j));
                        break;
                    }
                    else break;
                }
                //end checking


                //for dioganaly move
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

                //end checking
                return available;
            }

            public override string ToString()
            {
                return "queen";
            }
		}
	}
}

