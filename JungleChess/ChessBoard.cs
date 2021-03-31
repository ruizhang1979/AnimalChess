using Microsoft.VisualStudio.PlatformUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Windows.Input;

namespace JungleChess
{
    public class ChessBoard : ViewModelBase
    {
        public ObservableCollection<ChessBoardGrid> BoardGrids { get; set; }

        public ObservableCollection<ChessPiece> LostRedPieces { get; set; }
        public ObservableCollection<ChessPiece> LostBlackPieces { get; set; }

        public string RedPlayerName { get; set; } = "Zhang HaoXuan";
        public string BlackPlayerName { get; set; } = "Zhang Rui";

        public bool CurrentPlayer => Engine.CurrentPlayer == Player.Red;

        public ICommand PieceMouseClickCommand { get; set; }
        public ICommand PieceMouseDoubleClickCommand { get; set; }
        public ICommand GridMouseClickCommand { get; set; }

        private double _GridSideLength;

        public ChessBoard(double boardLength)
        {
            PieceMouseDoubleClickCommand = new DelegateCommand(PieceDbClicked);
            PieceMouseClickCommand = new DelegateCommand(PieceClicked);
            GridMouseClickCommand = new DelegateCommand(GridClicked);
            _GridSideLength = boardLength / 4;
            NewGame();
        }

        public void NewGame()
        {
            LostRedPieces = new ObservableCollection<ChessPiece>();
            LostBlackPieces = new ObservableCollection<ChessPiece>();
            Engine.CurrentPlayer = Player.Red;
            var pieceLen = _GridSideLength / 2;
            var grids = new List<ChessBoardGrid>
            {
                new ChessBoardGrid(PieceType.Mouse, Player.Red, _GridSideLength),
                new ChessBoardGrid(PieceType.Cat, Player.Red, _GridSideLength),
                new ChessBoardGrid(PieceType.Dog, Player.Red, _GridSideLength),
                new ChessBoardGrid(PieceType.Wolf, Player.Red, _GridSideLength),
                new ChessBoardGrid(PieceType.Leopard, Player.Red, _GridSideLength) ,
                new ChessBoardGrid(PieceType.Tiger, Player.Red, _GridSideLength),
                new ChessBoardGrid(PieceType.Lion, Player.Red, _GridSideLength),
                new ChessBoardGrid(PieceType.Elephant, Player.Red, _GridSideLength),
                new ChessBoardGrid(PieceType.Mouse, Player.Black, _GridSideLength),
                new ChessBoardGrid(PieceType.Cat, Player.Black, _GridSideLength),
                new ChessBoardGrid(PieceType.Dog, Player.Black, _GridSideLength),
                new ChessBoardGrid(PieceType.Wolf, Player.Black, _GridSideLength),
                new ChessBoardGrid(PieceType.Leopard, Player.Black, _GridSideLength),
                new ChessBoardGrid(PieceType.Tiger, Player.Black, _GridSideLength),
                new ChessBoardGrid(PieceType.Lion, Player.Black, _GridSideLength),
                new ChessBoardGrid(PieceType.Elephant, Player.Black, _GridSideLength)
            };
            BoardGrids = new ObservableCollection<ChessBoardGrid>(grids.OrderBy(i => Guid.NewGuid()));
            foreach (var grid in BoardGrids)
            {
                grid.Pos = ConvertIndexToBoardGrid(BoardGrids.IndexOf(grid));
            }
        }

        private Point ConvertIndexToBoardGrid(int index)
        {
            return new Point(index % 4, index / 4);
        }

        private void PieceClicked(object obj)
        {
            if (obj is ChessPiece piece)
            {
                if (!Engine.TrySelect(piece, BoardGrids))
                {
                    var piecesLost = Engine.TryMove(piece, BoardGrids);
                    if (piecesLost != null)
                    {
                        UpdateLostPieces(piecesLost);
                    }
                }
                RaisePropertyChanged("CurrentPlayer");
            }
        }

        private void UpdateLostPieces(IList<ChessPiece> piecesLost)
        {
            foreach (var lost in piecesLost)
            {
                lost.SideLength = 40;
                if (lost.Player == Player.Red)
                {
                    LostRedPieces.Add(lost);
                    //RaisePropertyChanged("LostRedPieces");
                }
                else
                {
                    LostBlackPieces.Add(lost);
                    RaisePropertyChanged("LostBlackPieces");
                }
            }
        }

        private void PieceDbClicked(object obj)
        {
            if (obj is ChessPiece piece)
            {
                Engine.TryFaceUp(piece, BoardGrids);
                RaisePropertyChanged("CurrentPlayer");
            }
        }

        private void GridClicked(object obj)
        {
            if (obj is ChessBoardGrid grid)
            {
                var piecesLost = Engine.TryMove(grid, BoardGrids);
                if (piecesLost != null)
                {
                    UpdateLostPieces(piecesLost);
                }
                RaisePropertyChanged("CurrentPlayer");
            }
        }
    }
}
