using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;

namespace JungleChess
{
    public class ChessBoardGrid : ViewModelBase
    {
        public ChessBoardGrid(PieceType type, Player player)
        {
            Pieces = new ObservableCollection<ChessPiece>
            {
                null,
                new ChessPiece(type, player){ Pos = CalculatePos(1) },
                null
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

        internal bool TryMovePieceTo(ChessPiece piece, out IList<ChessPiece> piecesLost)
        {
            piecesLost = new List<ChessPiece>();
            //The piece move to an empty grid
            if (!Pieces.Any(x => x != null))
            {
                MovePiece(piece,1);
                return true;
            }
            //The piece attacks another faceup piece on land
            else if (Pieces[1].FaceUp)
            {
                if (piece.Player != Pieces[1].Player &&
                    (piece.PieceType >= Pieces[1].PieceType ||
                    (piece.PieceType == PieceType.Mouse && Pieces[1].PieceType == PieceType.Elephant)))
                {
                    piecesLost.Add(Pieces[1]);
                    if (piece.PieceType == Pieces[1].PieceType)
                    {
                        piecesLost.Add(piece);
                        Pieces[1] = null;
                    }
                    else
                    {
                        MovePiece(piece,1);
                    }
                    return true;
                }
            }
            //The piece is mouse any try to move to the hole of another grid
            else if (piece.PieceType == PieceType.Mouse)
            {
                if (Pieces[0] == null)
                {
                    MovePiece(piece,0);
                }
                else
                {
                    piecesLost.Add(piece);
                    piecesLost.Add(Pieces[0]);
                    Pieces[0] = null;
                }
                return true;
            }
            //The piece is a cat or leopard and try to move to the tree of another grid
            else if (piece.PieceType == PieceType.Cat || piece.PieceType == PieceType.Leopard)
            {
                if (Pieces[2] == null)
                {
                    MovePiece(piece,2);
                    return true;
                }
                else if (Pieces[2].Player != piece.Player && piece.PieceType >= Pieces[2].PieceType)
                {
                    piecesLost.Add(Pieces[2]);
                    if (Pieces[2].PieceType == piece.PieceType)
                    {
                        piecesLost.Add(piece);
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
            piece.Pos = CalculatePos(index);
            Pieces[index] = piece;
        }

        private Point CalculatePos(int index)
        {
            return new Point((int)(index * (SideLength / 4)),
                (int)((SideLength / 2) - index * (SideLength / 4)));
        }
    }
}
