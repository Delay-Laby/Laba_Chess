using System;
using Chess.Figures;

namespace Chess.Core
{
	public interface IGameControl
	{
		void FigureMoved(Position oldPos, Position newPos);
		void SpotSelected(Position spotPos);
        bool SpotFocused(Position spotPos);
        void MessageReceived(String message);
	}
}

