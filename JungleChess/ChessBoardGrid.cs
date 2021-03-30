using Microsoft.VisualStudio.PlatformUI;
using System.Drawing;
using System.Windows.Input;

namespace JungleChess
{
    public class ChessBoardGrid : ViewModelBase
    {
        public ChessBoardGrid()
        {
            MouseDoubleClickCommand = new DelegateCommand(PieceDbClicked);
        }
        public ICommand MouseDoubleClickCommand { get; set; }

        private double _SideLength;
        public double SideLength
        {
            get => _SideLength;
            set { _SideLength = value; RaisePropertyChanged(); }
        }

        private Point _Pos;
        public Point Pos
        {
            get => _Pos;
            set { _Pos = value; RaisePropertyChanged(); }
        }

        private ChessPiece _PieceInHole;
        public ChessPiece PieceInHole
        {
            get => _PieceInHole;
            set
            {
                var pos = new Point(0, (int)(SideLength / 2));
                value.Pos = pos;
                _PieceInHole = value;
                RaisePropertyChanged();
            }
        }

        private ChessPiece _PieceOnLand;
        public ChessPiece PieceOnLand
        {
            get => _PieceOnLand;
            set
            {
                var pos = new Point((int)(SideLength / 4), (int)(SideLength / 4));
                value.Pos = pos;
                _PieceOnLand = value;
                RaisePropertyChanged();
            }
        }

        private ChessPiece _PieceOnTree;
        public ChessPiece PieceOnTree
        {
            get => _PieceOnTree;
            set
            {
                var pos = new Point((int)(SideLength / 2), 0);
                value.Pos = pos;
                _PieceOnTree = value;
                RaisePropertyChanged();
            }
        }

        private void PieceDbClicked(object sender)
        {
            if (sender.ToString() == "PieceOnLand")
            {
                Engine.TryFaceUp(this);
            }
        }
    }
}
