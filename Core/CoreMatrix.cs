#define TEST

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Chess.GUI;
using Chess.Figures;
using Chess.Core;


namespace Chess.Core
{
    public class CoreMatrix: ICloneable
    {

		private Position KingWhite;

		private Position KingBlack;

        private Figure[,] sMatrix;
        
        public CoreMatrix()
        {
			 sMatrix = new Figure[8, 8];



             //WHITE          

             sMatrix[0, 7] = new Rock(FigureColor.WHITE);
             sMatrix[7, 7] = new Rock(FigureColor.WHITE);


             sMatrix[4, 7] = new King(FigureColor.WHITE);
             sMatrix[6, 7] = new Knight(FigureColor.WHITE);
             sMatrix[1, 7] = new Knight(FigureColor.WHITE);
             sMatrix[2, 7] = new Bishop(FigureColor.WHITE);
             sMatrix[5, 7] = new Bishop(FigureColor.WHITE);
             sMatrix[3, 7] = new Queen(FigureColor.WHITE);
            for (int i = 0; i < 8; i++)
             {
                 sMatrix[i, 6] = new Pawn(FigureColor.WHITE);
             }

             //BLACK
             sMatrix[0, 0] = new Rock(FigureColor.BLACK);
             sMatrix[7, 0] = new Rock(FigureColor.BLACK);

             sMatrix[6, 0] = new Knight(FigureColor.BLACK);
             sMatrix[1, 0] = new Knight(FigureColor.BLACK);

             sMatrix[2, 0] = new Bishop(FigureColor.BLACK);
             sMatrix[5, 0] = new Bishop(FigureColor.BLACK);

             sMatrix[4, 0] = new King(FigureColor.BLACK);
             sMatrix[3, 0] = new Queen(FigureColor.BLACK);

             for (int i = 0; i < 8; i++)
             {
                 sMatrix[i, 1] = new Pawn(FigureColor.BLACK);
             }

			KingBlack = new Position(4, 0);
			KingWhite = new Position( 4, 7);
        }
        
		public Figure FigureAt(Position pos)
        {
         
            return sMatrix[pos.X, pos.Y];
        }
        
		public Figure FigureAt(int x, int y)
        {
            if (x > 7 || y > 7 || x < 0 || y < 0)
                return null;
            return sMatrix[x, y];
        }


		public void MoveFigure (Position oldPos, Position newPos)
		{
			if (! HasFigureAt (oldPos))
				throw (new NullReferenceException ("I don't have figure at " + oldPos.X + ' ' + oldPos.Y));

			//changing king position
			if (FigureAt (oldPos) is King) {

				if(FigureAt (oldPos).Color == FigureColor.WHITE )
				{
					KingWhite = newPos;
				}
				else
				{
					KingBlack = newPos;
				}
			}

			sMatrix [newPos.X, newPos.Y] = sMatrix [oldPos.X, oldPos.Y];
			sMatrix [oldPos.X, oldPos.Y] = null; 
		}

		public bool HasFigureAt (Position pos)
		{
			return (sMatrix[pos.X, pos.Y] !=null);
		}

        public bool HasFigureAt( int x, int y )
        {
            if (x > 7 || y > 7 || x<0 || y<0)
                return false;

            return (sMatrix[x, y] != null);
        }
  
		public Position GetKing (FigureColor color)
		{
			if (color == FigureColor.WHITE) {
				return KingWhite;
			} else {
				return KingBlack;
			}
		}

		public Object Clone()
		{
			CoreMatrix clone = new CoreMatrix();

			for(int i=0; i<8; i++)
				for(int j=0; j<8; j++)
					clone.sMatrix[i, j] = sMatrix[i, j];

			clone.KingBlack = KingBlack;
			clone.KingWhite = KingWhite;

			return clone;
		}
        
        public void SetFigure(Figure f, Position pos)
        {
            sMatrix[pos.X, pos.Y] = f;
        }
	}
}
