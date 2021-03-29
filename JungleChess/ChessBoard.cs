using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;

namespace JungleChess
{
    public class ChessBoard : INotifyPropertyChanged
    {
        public ObservableCollection<ChessPiece> Pieces { get; set; }

        private double BoardWidth;
        private double BoardHeight;

        public ChessBoard(double boardWidth, double boardHeight)
        {
            BoardWidth = boardWidth;
            BoardHeight = boardHeight;
            NewGame();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NewGame()
        {
            var pieceWidth = BoardWidth / 8;
            var pieceHeight = BoardHeight / 8;
            
            var pieces = new List<ChessPiece>
            {
                new ChessPiece { ImageSource="/Res/mouse_red.png", Type = PieceType.Mouse, Player = Player.Red, Width = pieceWidth, Height = pieceHeight },
                new ChessPiece { ImageSource="/Res/cat_red.png", Type = PieceType.Cat, Player = Player.Red, Width = pieceWidth, Height = pieceHeight },
                new ChessPiece { ImageSource="/Res/dog_red.png", Type = PieceType.Dog, Player = Player.Red, Width = pieceWidth, Height = pieceHeight },
                new ChessPiece { ImageSource="/Res/wolf_red.png", Type = PieceType.Wolf, Player = Player.Red, Width = pieceWidth, Height = pieceHeight },
                new ChessPiece { ImageSource="/Res/Leopard_red.png", Type = PieceType.Leopard, Player = Player.Red, Width = pieceWidth, Height = pieceHeight },
                new ChessPiece { ImageSource="/Res/tiger_red.png", Type = PieceType.Tiger, Player = Player.Red, Width = pieceWidth, Height = pieceHeight },
                new ChessPiece { ImageSource="/Res/lion_red.png", Type = PieceType.Lion, Player = Player.Red, Width = pieceWidth, Height = pieceHeight },
                new ChessPiece { ImageSource="/Res/elephant_red.png", Type = PieceType.Elephant, Player = Player.Red, Width = pieceWidth, Height = pieceHeight },
                new ChessPiece { ImageSource="/Res/mouse_black.png", Type = PieceType.Mouse, Player = Player.Black, Width = pieceWidth, Height = pieceHeight },
                new ChessPiece { ImageSource="/Res/cat_black.png", Type = PieceType.Cat, Player = Player.Black, Width = pieceWidth, Height = pieceHeight },
                new ChessPiece { ImageSource="/Res/dog_black.png", Type = PieceType.Dog, Player = Player.Black, Width = pieceWidth, Height = pieceHeight },
                new ChessPiece { ImageSource="/Res/wolf_black.png", Type = PieceType.Wolf, Player = Player.Black, Width = pieceWidth, Height = pieceHeight },
                new ChessPiece { ImageSource="/Res/Leopard_black.png", Type = PieceType.Leopard, Player = Player.Black, Width = pieceWidth, Height = pieceHeight },
                new ChessPiece { ImageSource="/Res/tiger_black.png", Type = PieceType.Tiger, Player = Player.Black, Width = pieceWidth, Height = pieceHeight },
                new ChessPiece { ImageSource="/Res/lion_black.png", Type = PieceType.Lion, Player = Player.Black, Width = pieceWidth, Height = pieceHeight },
                new ChessPiece { ImageSource="/Res/elephant_black.png", Type = PieceType.Elephant, Player = Player.Black, Width = pieceWidth, Height = pieceHeight }
            };
            Pieces = new ObservableCollection<ChessPiece>(pieces.OrderBy(i => Guid.NewGuid()));
            foreach (var piece in Pieces)
            {
                piece.Pos = MapIndexToCanvasPoint(Pieces.IndexOf(piece));
            }
            RaisePropertyChanged("Pieces");
        }

        private Point MapIndexToCanvasPoint(int index)
        {
            return new Point((int)(index % 4 * (BoardWidth / 4) + BoardWidth / 16),
                             (int)(index / 4 * (BoardHeight / 4) + BoardHeight / 16));
        }
        protected virtual void RaisePropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
