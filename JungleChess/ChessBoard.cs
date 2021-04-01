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
        static private IList<ChessStep> _Steps = new List<ChessStep>();

        public ObservableCollection<ChessBoardGrid> BoardGrids { get; set; }

        public ObservableCollection<ChessPiece> LostRedPieces { get; set; }
        public ObservableCollection<ChessPiece> LostBlackPieces { get; set; }

        public string RedPlayerName { get; set; } = "Zhang HaoXuan";
        public string BlackPlayerName { get; set; } = "Zhang Rui";

        private Player _CurrentPlayer;
        public Player CurrentPlayer
        {
            get => _CurrentPlayer;
            set { _CurrentPlayer = value; RaisePropertyChanged(); }
        }

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
            _CurrentPlayer = Player.Red;
            ChessBoardGrid.SideLength = _GridSideLength;
            ChessPiece.SideLength = _GridSideLength / 2;
            ChessPiece.SideLengthThumbnail = 4;
            var grids = new List<ChessBoardGrid>
            {
                new ChessBoardGrid(PieceType.Mouse, Player.Red),
                new ChessBoardGrid(PieceType.Cat, Player.Red),
                new ChessBoardGrid(PieceType.Dog, Player.Red),
                new ChessBoardGrid(PieceType.Wolf, Player.Red),
                new ChessBoardGrid(PieceType.Leopard, Player.Red) ,
                new ChessBoardGrid(PieceType.Tiger, Player.Red),
                new ChessBoardGrid(PieceType.Lion, Player.Red),
                new ChessBoardGrid(PieceType.Elephant, Player.Red),
                new ChessBoardGrid(PieceType.Mouse, Player.Black),
                new ChessBoardGrid(PieceType.Cat, Player.Black),
                new ChessBoardGrid(PieceType.Dog, Player.Black),
                new ChessBoardGrid(PieceType.Wolf, Player.Black),
                new ChessBoardGrid(PieceType.Leopard, Player.Black),
                new ChessBoardGrid(PieceType.Tiger, Player.Black),
                new ChessBoardGrid(PieceType.Lion, Player.Black),
                new ChessBoardGrid(PieceType.Elephant, Player.Black)
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
                if (!Engine.TrySelect(piece, this))
                {
                    Engine.TryMove(piece, this);
                }
            }
        }

        private void PieceDbClicked(object obj)
        {
            if (obj is ChessPiece piece)
            {
                Engine.TryFaceUp(piece, this);
            }
        }

        private void GridClicked(object obj)
        {
            if (obj is ChessBoardGrid grid)
            {
                Engine.TryMove(grid, this);
            }
        }
    }
}
