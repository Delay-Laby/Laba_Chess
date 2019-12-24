using System.Collections.Generic;
using Chess.Core;

namespace Chess
{
	namespace Figures
	{
		public class Rock : Figure
		{
			public Rock (FigureColor color)
                : base(color)
			{
				diff = false;
				LoadBitmap ("rock");
			}

			public override List<Position> GetAvailableMovePossitons (Position currentPos)
			{
				List<Position> available = new List<Position> ();
				for (int i = 0; i < 8; i++) {
					if (currentPos.X != i)
						available.Add (new Position (i, currentPos.Y));
					if (currentPos.Y != i)
						available.Add (new Position (currentPos.X, i));
				}
				return available;
			}
            
			public override List<Position> GetAvailableAtackPositons (Position currentPos, CoreMatrix matrix)
			{
				List<Position> available = new List<Position> ();
                //by X
				for (int i = currentPos.X + 1; i < 8; i++) {
					if (matrix.FigureAt (i, currentPos.Y) == null)
						available.Add (new Position (i, currentPos.Y));
					else if (matrix.FigureAt (i, currentPos.Y).Color != this.Color) {
                        available.Add(new Position(i, currentPos.Y));
                        break;
                    }
                    else if(matrix.FigureAt(i, currentPos.Y) is King && !IsMoved && !matrix.FigureAt(i, currentPos.Y).IsMoved) {
                        CoreMatrix tmpMatrix;
                        bool can = true;
                        for (int x = currentPos.X + 1; x <= i; x++)
                        {
                            tmpMatrix = (CoreMatrix)matrix.Clone();
                            Position kingPos = tmpMatrix.GetKing(color);
                            tmpMatrix.MoveFigure(kingPos, new Position(x, currentPos.Y));

                            if (King.IsCheckState(tmpMatrix, color))
                            {
                                can = false;
                                break;
                            }
                        }

                        //figure is not by atack 
                        if (can)
                            available.Add(new Position(i, currentPos.Y));
                        break;
                    }
                    else
						break;
				}

                //by X
				for (int i = currentPos.X - 1; i >= 0; i--) {
					if (matrix.FigureAt (i, currentPos.Y) == null)
						available.Add (new Position (i, currentPos.Y));
					else if (matrix.FigureAt (i, currentPos.Y).Color != this.Color) {
                        available.Add (new Position (i, currentPos.Y));
						break;
                    }
                    else if(matrix.FigureAt(i, currentPos.Y) is King && !IsMoved && !matrix.FigureAt(i, currentPos.Y).IsMoved)
                    {
                        CoreMatrix tmpMatrix;
                        bool can = true;
                        for (int x = currentPos.X - 1; x <= i; x-- )
                        {
                            tmpMatrix = (CoreMatrix)matrix.Clone();
                            Position kingPos = tmpMatrix.GetKing(color);
                            tmpMatrix.MoveFigure(kingPos, new Position(x, currentPos.Y));

                            if (King.IsCheckState(tmpMatrix, color))
                            {
                                can = false;
                                break;
                            }
                        }

                        //figure is not by atack 
                        if (can)
                            available.Add(new Position(i, currentPos.Y));
                        break;
					} else
						break;
				}
                //by Y
				for (int j = currentPos.Y + 1; j < 8; j++) {
					if (!matrix.HasFigureAt (new Position (currentPos.X, j))) 
						available.Add (new Position (currentPos.X, j));
					else if (matrix.FigureAt (currentPos.X, j).Color != this.Color ||
                        (matrix.FigureAt(currentPos.X, j) is King && !IsMoved && !matrix.FigureAt(currentPos.X, j).IsMoved))
                    {
						available.Add (new Position (currentPos.X, j));
						break;
					} else
						break;
				}
                //by Y
				for (int j = currentPos.Y - 1; j >= 0; j--) { 
					if (matrix.FigureAt (currentPos.X, j) == null)
						available.Add (new Position (currentPos.X, j));
					else if (matrix.FigureAt (currentPos.X, j).Color != this.Color ||
                        (matrix.FigureAt(currentPos.X, j) is King && !IsMoved && !matrix.FigureAt(currentPos.X, j).IsMoved))
                    {
						available.Add (new Position (currentPos.X, j));
						break;
					} else
						break;
				}
				return available;
			}

			public override List<Position> GetAvailableOnCheckPositons (Position currentPos, CoreMatrix matrix)
			{
				List<Position> availlablePos = GetAvailableAtackPositons (currentPos, matrix);
				List<Position> validPos = new List<Position> ();

				CoreMatrix tmpMatrix;
				tmpMatrix = (CoreMatrix)matrix.Clone (); 

				foreach (Position pos in availlablePos) {
					Position kingPos = tmpMatrix.GetKing (color);

					if( kingPos.Equals( pos ) ) {
						int dx = currentPos.X - pos.X;
						dx = (dx > 0) ? (1) : (-1);

						Position newKingPos = new Position (pos.X + dx * 2, pos.Y);
						Position rockPos = new Position (pos.X + dx, pos.Y);

						tmpMatrix.MoveFigure (kingPos, newKingPos);
						tmpMatrix.MoveFigure (currentPos, rockPos);

						if (!King.IsCheckState (tmpMatrix, color))
							validPos.Add (pos);

						tmpMatrix.MoveFigure (newKingPos, kingPos);
						tmpMatrix.MoveFigure (rockPos, currentPos);

					}
					else{
						tmpMatrix.MoveFigure (currentPos, pos);
						if (!King.IsCheckState (tmpMatrix, color))
							validPos.Add (pos);
						tmpMatrix.MoveFigure (pos, currentPos);
					}
				}
			
				return validPos;
			}

			public override string ToString ()
			{
				return "rock";
			}
		}
	}
}
