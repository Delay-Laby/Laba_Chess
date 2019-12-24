using System;
using Chess.Figures;

namespace Chess.Network
{
	public interface INetworkSupport
	{
		void  ChessMoved(Position oldPos, Position newPos);
		void  Connected();
		void  Disconnected();
		void  MessageReceived(String mes);
	}
}

