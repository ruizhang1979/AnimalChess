using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

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

        public void ReSize(double boardWidth, double boardHeight)
        {
            BoardWidth = boardWidth;
            BoardHeight = boardHeight;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NewGame()
        {
            var indexes = Enumerable.Range(0, 16).OrderBy(i => Guid.NewGuid()).ToArray();

            Pieces = new ObservableCollection<ChessPiece>
            {
                new ChessPiece { Type = PieceType.Mouse, Player = Player.Red, Pos = MapIndexToCanvasPoint(indexes[0]) },
                new ChessPiece { Type = PieceType.Cat, Player = Player.Red, Pos = MapIndexToCanvasPoint(indexes[1]) },
                new ChessPiece { Type = PieceType.Dog, Player = Player.Red, Pos = MapIndexToCanvasPoint(indexes[2]) },
                new ChessPiece { Type = PieceType.Wolf, Player = Player.Red, Pos = MapIndexToCanvasPoint(indexes[3]) },
                new ChessPiece { Type = PieceType.Leopard, Player = Player.Red, Pos = MapIndexToCanvasPoint(indexes[4]) },
                new ChessPiece { Type = PieceType.Tiger, Player = Player.Red, Pos = MapIndexToCanvasPoint(indexes[5]) },
                new ChessPiece { Type = PieceType.Lion, Player = Player.Red, Pos = MapIndexToCanvasPoint(indexes[6]) },
                new ChessPiece { Type = PieceType.Elephant, Player = Player.Red, Pos = MapIndexToCanvasPoint(indexes[7]) },
                new ChessPiece { Type = PieceType.Mouse, Player = Player.Black, Pos = MapIndexToCanvasPoint(indexes[8]) },
                new ChessPiece { Type = PieceType.Cat, Player = Player.Black, Pos = MapIndexToCanvasPoint(indexes[9]) },
                new ChessPiece { Type = PieceType.Dog, Player = Player.Black, Pos = MapIndexToCanvasPoint(indexes[10]) },
                new ChessPiece { Type = PieceType.Wolf, Player = Player.Black, Pos = MapIndexToCanvasPoint(indexes[11]) },
                new ChessPiece { Type = PieceType.Leopard, Player = Player.Black, Pos = MapIndexToCanvasPoint(indexes[12]) },
                new ChessPiece { Type = PieceType.Tiger, Player = Player.Black, Pos = MapIndexToCanvasPoint(indexes[13]) },
                new ChessPiece { Type = PieceType.Lion, Player = Player.Black, Pos = MapIndexToCanvasPoint(indexes[14]) },
                new ChessPiece { Type = PieceType.Elephant, Player = Player.Black, Pos = MapIndexToCanvasPoint(indexes[15]) }
            };

            RaisePropertyChanged("Pieces");
        }

        private Point MapIndexToCanvasPoint(int index)
        {
            return new Point(index % 4, index / 4);
        }

        protected virtual void RaisePropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
