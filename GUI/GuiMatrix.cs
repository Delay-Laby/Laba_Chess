using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Chess.Figures;
using Chess.Core;

namespace Chess.GUI
{
    public class Spot
    {
        public int X;
        public int Y;
        public Color sColor;
        public Image image;
        public bool Highlighted { get; set; }  // Для выделения возможных шагов
        public bool Focused { get; set; }      // Мышь наведена на эту клетку
        public bool Selected { get; set; }
        public bool Check { get; set; }
        public Spot(int x, int y, Color clr, Image img)
        {
            X = x;
            Y = y;
            sColor = clr;
            image = img;
            Highlighted = false;
            Focused = false;
            Selected = false;
        }
    }

    public class GuiMatrix
    {
        private Point oldFocused;
        private Point oldSelected;
        private Spot [,] sMatrix;

		public GuiMatrix(CoreMatrix coreMatrix)
        {
            oldFocused = new Point(int.MaxValue, int.MaxValue);
            oldSelected = new Point(int.MaxValue, int.MaxValue);
            sMatrix = new Spot[8, 8];
            Position pos = new Position();
            for (pos.X = 0; !pos.errorFlag; pos.X++)
            {
                for (pos.Y = 0; !pos.errorFlag; pos.Y++)
                {
                    if ((pos.X + pos.Y) % 2 != 0)
                    {
                        try
                        {
                            sMatrix[pos.X, pos.Y] = new Spot(pos.X, pos.Y, Color.CadetBlue, coreMatrix.FigureAt(pos).image);
                        }
                        catch (System.NullReferenceException)
                        {
                            sMatrix[pos.X, pos.Y] = new Spot(pos.X, pos.Y, Color.CadetBlue, null);
                        }
                    }
                    else
                    {
                        try
                        {
                            sMatrix[pos.X, pos.Y] = new Spot(pos.X, pos.Y, Color.White, coreMatrix.FigureAt(pos).image);
                        }
                        catch (System.NullReferenceException)
                        {
                            sMatrix[pos.X, pos.Y] = new Spot(pos.X, pos.Y, Color.White, null);
                        }
                       
                    }
                }
            }
        }

        public Spot GetSpot(int i, int j)
        {
            return sMatrix[i, j];
        }
        public Spot GetSpot(Position pos)
        {
            return sMatrix[pos.X, pos.Y];
        }
        
		public bool SetFocused(int i, int j)
        {
            if (oldFocused.X == i && oldFocused.Y == j) return false;
            if (oldFocused.X != int.MaxValue) sMatrix[oldFocused.X, oldFocused.Y].Focused = false;
            oldFocused.X = i;
            oldFocused.Y = j;
            sMatrix[i, j].Focused = true;
            return true;
        }

		public bool SetHighlighted (Position pos)
		{
			sMatrix[pos.X, pos.Y].Highlighted = true;
			return true;
		}

		public bool IsHighlighted (Position pos)
		{
			return sMatrix[pos.X, pos.Y].Highlighted;
		}

        public bool SetChecked(Position pos)
        {
            sMatrix[pos.X, pos.Y].Check = true;
            return true;
        }
		public bool SetHighlighted (List<Position> positions)
		{
			UnsetHiglight();

			foreach( Position pos in positions)
			{
				SetHighlighted(pos);
			}
			return true;
		}

		public void UnsetHiglight ()
		{
			foreach (Spot spot in sMatrix) {
				spot.Highlighted = false;
                spot.Check = false;
			}
		}

        public void ResetAllAttribures()
        {
            foreach (Spot spot in sMatrix)
            {
                spot.Highlighted = false;
                spot.Selected = false;
            }
        }

		//Making square selected
        public bool SetSelected(int i, int j)
        {
            UnsetHiglight();
            if (oldSelected.X == i && oldSelected.Y == j)
            {
                sMatrix[i, j].Selected = !sMatrix[i, j].Selected;
                oldSelected.X = int.MaxValue;
                oldSelected.Y = int.MaxValue;
                return false;
            }
            if (oldSelected.X != int.MaxValue) sMatrix[oldSelected.X, oldSelected.Y].Selected = false;
            oldSelected.X = i;
            oldSelected.Y = j;
            sMatrix[i, j].Selected = true;
            return true;
        }

		public bool SetSelected(Position pos)
        {
			return SetSelected(pos.X, pos.Y);
        }

		//Set up image
        public bool SetImage(Image img, Position pos)
        {
            sMatrix[pos.X, pos.Y].image = img;
            return true;
        }
		//Move image from one square to another
        public bool MoveImage(Position posOld, Position posNew)
        {
            sMatrix[posNew.X, posNew.Y].image = sMatrix[posOld.X, posOld.Y].image;
            sMatrix[posOld.X, posOld.Y].image = null;
            return true;
        }
    }
}
