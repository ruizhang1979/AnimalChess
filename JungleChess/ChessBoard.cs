using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Text;

namespace JungleChess
{
    public class ChessBoard
    {
        public ObservableCollection<ChessPiece> Pieces =
        new ObservableCollection<ChessPiece>
        {
            new ChessPiece{Pos=new Point(0, 0), Type=PieceType.Mouse, Player=Player.Red},
            new ChessPiece{Pos=new Point(0, 1), Type=PieceType.Cat, Player=Player.Red},
            new ChessPiece { Pos = new Point(0, 2), Type = PieceType.Dog, Player = Player.Red },
            new ChessPiece { Pos = new Point(0, 3), Type = PieceType.Wolf, Player = Player.Red },
            new ChessPiece { Pos = new Point(1, 0), Type = PieceType.Leopard, Player = Player.Red },
            new ChessPiece { Pos = new Point(1, 1), Type = PieceType.Tiger, Player = Player.Red },
            new ChessPiece { Pos = new Point(1, 2), Type = PieceType.Lion, Player = Player.Red },
            new ChessPiece { Pos = new Point(1, 3), Type = PieceType.Elephant, Player = Player.Red },
            new ChessPiece { Pos = new Point(2, 0), Type = PieceType.Mouse, Player = Player.Black },
            new ChessPiece { Pos = new Point(2, 1), Type = PieceType.Cat, Player = Player.Black },
            new ChessPiece { Pos = new Point(2, 2), Type = PieceType.Dog, Player = Player.Black },
            new ChessPiece { Pos = new Point(2, 3), Type = PieceType.Wolf, Player = Player.Black },
            new ChessPiece { Pos = new Point(3, 0), Type = PieceType.Leopard, Player = Player.Black },
            new ChessPiece { Pos = new Point(3, 1), Type = PieceType.Tiger, Player = Player.Black },
            new ChessPiece { Pos = new Point(3, 2), Type = PieceType.Lion, Player = Player.Black },
            new ChessPiece { Pos = new Point(3, 3), Type = PieceType.Elephant, Player = Player.Black }
        };
    }
}
