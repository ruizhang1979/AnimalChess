using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;

namespace JungleChess
{
    public class ChessBoard : ViewModelBase
    {
        public ObservableCollection<ChessBoardGrid> BoardGrids { get; set; }

        private double _GridSideLength;

        public ChessBoard(double boardLength)
        {
            _GridSideLength = boardLength / 4;
            NewGame();
        }

        public void NewGame()
        {
            var pieceLen = _GridSideLength / 2;
            var grids = new List<ChessBoardGrid>
            {
                new ChessBoardGrid { SideLength = _GridSideLength, PieceOnLand = new ChessPiece (PieceType.Mouse, Player.Red){ SideLength = pieceLen } },
                new ChessBoardGrid { SideLength = _GridSideLength, PieceOnLand = new ChessPiece (PieceType.Cat, Player.Red){ SideLength = pieceLen } },
                new ChessBoardGrid { SideLength = _GridSideLength, PieceOnLand = new ChessPiece (PieceType.Dog, Player.Red){ SideLength = pieceLen } },
                new ChessBoardGrid { SideLength = _GridSideLength, PieceOnLand = new ChessPiece (PieceType.Wolf, Player.Red){ SideLength = pieceLen } },
                new ChessBoardGrid { SideLength = _GridSideLength, PieceOnLand = new ChessPiece (PieceType.Leopard, Player.Red){ SideLength = pieceLen } },
                new ChessBoardGrid { SideLength = _GridSideLength, PieceOnLand = new ChessPiece (PieceType.Tiger, Player.Red){ SideLength = pieceLen } },
                new ChessBoardGrid { SideLength = _GridSideLength, PieceOnLand = new ChessPiece (PieceType.Lion, Player.Red){ SideLength = pieceLen } },
                new ChessBoardGrid { SideLength = _GridSideLength, PieceOnLand = new ChessPiece (PieceType.Elephant, Player.Red){ SideLength = pieceLen } },
                new ChessBoardGrid { SideLength = _GridSideLength, PieceOnLand = new ChessPiece (PieceType.Mouse, Player.Black){ SideLength = pieceLen } },
                new ChessBoardGrid { SideLength = _GridSideLength, PieceOnLand = new ChessPiece (PieceType.Cat, Player.Black){ SideLength = pieceLen } },
                new ChessBoardGrid { SideLength = _GridSideLength, PieceOnLand = new ChessPiece (PieceType.Dog, Player.Black){ SideLength = pieceLen } },
                new ChessBoardGrid { SideLength = _GridSideLength, PieceOnLand = new ChessPiece (PieceType.Wolf, Player.Black){ SideLength = pieceLen } },
                new ChessBoardGrid { SideLength = _GridSideLength, PieceOnLand = new ChessPiece (PieceType.Leopard, Player.Black){ SideLength = pieceLen } },
                new ChessBoardGrid { SideLength = _GridSideLength, PieceOnLand = new ChessPiece (PieceType.Tiger, Player.Black){ SideLength = pieceLen } },
                new ChessBoardGrid { SideLength = _GridSideLength, PieceOnLand = new ChessPiece (PieceType.Lion, Player.Black){ SideLength = pieceLen } },
                new ChessBoardGrid { SideLength = _GridSideLength, PieceOnLand = new ChessPiece (PieceType.Elephant, Player.Black){ SideLength = pieceLen } }
            };
            BoardGrids = new ObservableCollection<ChessBoardGrid>(grids.OrderBy(i => Guid.NewGuid()));
            foreach (var grid in BoardGrids)
            {
                grid.Pos = MapIndexToCanvasPoint(BoardGrids.IndexOf(grid));
            }
        }

        private Point MapIndexToCanvasPoint(int index)
        {
            return new Point(index % 4, index / 4);
        }
    }
}
