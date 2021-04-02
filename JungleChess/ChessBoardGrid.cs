using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;

namespace JungleChess
{
    public enum Situation
    {
        Hole,
        Land,
        Tree
    }

    public class ChessBoardGrid : ViewModelBase
    {
        public ChessBoardGrid() { }
        public ChessBoardGrid(ChessPiece piece)
        {
            if (piece != null)
            {
                piece.Pos = CalculatePiecePosByIndex(1);
            }
            Pieces = new ObservableCollection<ChessPiece> { null, piece, null };
        }

        internal ChessBoardGrid DeepCopy()
        {
            return new ChessBoardGrid()
            {
                _Pos = this.Pos,
                Pieces = new ObservableCollection<ChessPiece>(this.Pieces.Select(x => x?.DeepCopy())),
            };
        }

        public ObservableCollection<ChessPiece> Pieces { get; set; }

        static public double SideLength { get; set; }

        private Point _Pos;
        public Point Pos
        {
            get => _Pos;
            set { _Pos = value; RaisePropertyChanged(); }
        }

        internal bool TryMovePieceTo(ChessPiece piece)
        {
            //The piece move to an empty grid
            if (!Pieces.Any(x => x != null))
            {
                MovePiece(piece, 1);
                return true;
            }
            //The piece attacks another faceup piece on land
            else if (Pieces[1].FaceUp)
            {
                if (piece.Color != Pieces[1].Color &&
                    (piece.PieceType >= Pieces[1].PieceType ||
                    (piece.PieceType == PieceType.Mouse && Pieces[1].PieceType == PieceType.Elephant)))
                {
                    if (piece.PieceType == Pieces[1].PieceType)
                    {
                        Pieces[1] = null;
                    }
                    else
                    {
                        MovePiece(piece, 1);
                    }
                    return true;
                }
            }
            //The piece is mouse any try to move to the hole of another grid
            else if (piece.PieceType == PieceType.Mouse)
            {
                if (Pieces[0] == null)
                {
                    MovePiece(piece, 0);
                }
                else
                {
                    Pieces[0] = null;
                }
                return true;
            }
            //The piece is a cat or leopard and try to move to the tree of another grid
            else if (piece.PieceType == PieceType.Cat || piece.PieceType == PieceType.Leopard)
            {
                if (Pieces[2] == null)
                {
                    MovePiece(piece, 2);
                    return true;
                }
                else if (Pieces[2].Color != piece.Color && piece.PieceType >= Pieces[2].PieceType)
                {
                    if (Pieces[2].PieceType == piece.PieceType)
                    {
                        Pieces[2] = null;
                    }
                    else
                    {
                        MovePiece(piece, 2);
                    }
                    return true;
                }
            }
            return false;
        }

        private void MovePiece(ChessPiece piece, int index)
        {
            piece.Pos = CalculatePiecePosByIndex(index);
            Pieces[index] = piece;
        }

        private Point CalculatePiecePosByIndex(int index)
        {
            return new Point((int)(index * (SideLength / 4)),
                (int)((SideLength / 2) - index * (SideLength / 4)));
        }
    }
}
