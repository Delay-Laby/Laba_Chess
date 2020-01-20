using System;
using Chess.Figures;

namespace Chess.Core
{
	public interface IGameControl
	{
	
		void SpotSelected(Position spotPos);
        bool SpotFocused(Position spotPos);
	}
}

