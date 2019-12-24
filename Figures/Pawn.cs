using System.Collections.Generic;
using Chess.Core;

namespace Chess
{
    namespace Figures
    {
        public class Pawn : Figure
        {
			public bool TwoStepState = false;
            public Figure[] NeighborsFigures;

            public Pawn(FigureColor color)
                : base(color)
            {
                diff = true;

                NeighborsFigures = new Figure[2];
                NeighborsFigures[0] = null;
                NeighborsFigures[1] = null;

                LoadBitmap("pawn");
            }

            public override List<Position> GetAvailableMovePossitons(Position currentPos)
            {

                List<Position> available = new List<Position>();
                int m;

                //white figures below
                if (Color == Chess.Figures.FigureColor.WHITE)
                {
                    m = -1;
                }
                else
                {  //black fugures upwardly
                    m = 1;
                }

                Position pos = new Position();
                pos.X = currentPos.X;
                pos.Y = currentPos.Y + 1 * m;
                available.Add(pos);

                if (IsMoved)
                {
                    Position pos2 = new Position();
                    pos2.X = currentPos.X;
                    pos2.Y = currentPos.Y + 2 * m;
                    available.Add(pos2);
                }

                return available;
            }

            public override List<Position> GetAvailableAtackPositons(Position currentPos, CoreMatrix matrix)
            {
                List<Position> available = new List<Position>();
                int m;

                //white figures below
                if (Color == Chess.Figures.FigureColor.WHITE)
                {
                    m = -1;
                }
                else
                {  //black fugures upwardly
                    m = 1;
                }

                //gap forward
                Position pos = new Position();
                pos.X = currentPos.X;
                pos.Y = currentPos.Y + 1 * m;

                if( !matrix.HasFigureAt(pos) )
                    available.Add(pos);

                //gap forward dioginaly
                foreach (int k in new int[] { 1, -1 })
                {
                    Position pawnPos = new Position(currentPos.X+k, currentPos.Y+m);

                    if (matrix.HasFigureAt(pawnPos.X, pawnPos.Y))
                    {
                        Figure fig = matrix.FigureAt(pawnPos);
                        if (fig.Color != Color)
                            available.Add(pawnPos);
                    }
                }

                //two gap forward
                if (!IsMoved && ! matrix.HasFigureAt(pos))
                {
                    Position pos2 = new Position();
                    pos2.X = currentPos.X;
                    pos2.Y = currentPos.Y + 2 * m;
                    if( !matrix.HasFigureAt(pos2) )
                     available.Add(pos2);
                }

                //pawn pass implementation
                foreach (int k in new int[] { 1, -1 })
                {
                    Position pawnPos = new Position(currentPos.X+k, currentPos.Y);
                    if (matrix.HasFigureAt(pawnPos.X, pawnPos.Y))
                    {
                        Figure neighFigure = matrix.FigureAt(pawnPos);

                        //neighbors figure - enemy figure
                        if (neighFigure.Color != Color)
                        {
                            //last state neighbors figure is not Pawn
                            // if k == -1 - it is left neighbors
                            // if k == 1 - it is right neighbors
                            int neighborsIndex = (k == -1) ? 0 : 1;

                            if (  NeighborsFigures[neighborsIndex] == null ||  
                                ( NeighborsFigures[neighborsIndex] != null && 
                                !(NeighborsFigures[neighborsIndex] is Pawn))  )
                            {
                                //It's enemy Pawn
                                if (neighFigure is Pawn)
                                {
                                    //Enemy pawn moved two step
                                    if (((Pawn)neighFigure).TwoStepState && ((Pawn)neighFigure).StepCount == 1)
                                    {
                                        available.Add(new Position(pawnPos.X, pawnPos.Y+m));
                                    }
                                }
                            }
                        }
                    }
                }
                return available;
            }

            public override string ToString()
            {
                return "pawn";
            }
        }
    }
}
