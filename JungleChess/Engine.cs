using System;
using System.Collections.Generic;
using System.Linq;

namespace JungleChess
{
    public class Engine
    {
        static public Player CurrentPlayer { get; set; }

        static public void TryFaceUp(ChessPiece piece, IList<ChessBoardGrid> boardGrids)
        {
            var boardGrid = boardGrids.First(x => x.Pieces.Contains(piece));
            if (boardGrid.Pieces[0] == null && boardGrid.Pieces[2] == null && !boardGrid.Pieces[1].FaceUp)
            {
                boardGrid.Pieces[1].FaceUp = true;
                RemoveCurrentSelection(boardGrids);
                ChangePlayerTurn();
            }
        }

        static public bool TrySelect(ChessPiece piece, IList<ChessBoardGrid> boardGrids)
        {
            if (!piece.FaceUp || piece.Player != CurrentPlayer)
            {
                return false;
            }
            if (!piece.Selected)
            {
                RemoveCurrentSelection(boardGrids);
                piece.Selected = true;
            }
            return true;
        }

        static void RemoveCurrentSelection(IList<ChessBoardGrid> boardGrids)
        {
            var selectedPiece = boardGrids.SelectMany(x => x.Pieces).FirstOrDefault(y => y != null && y.Selected);
            if (selectedPiece != null)
            {
                selectedPiece.Selected = false;
            }
        }

        internal static IList<ChessPiece> TryMove(ChessPiece targetPiece, IList<ChessBoardGrid> boardGrids)
        {
            var targetGrid = boardGrids.First(x => x.Pieces.Contains(targetPiece));
            return TryMove(targetGrid, boardGrids);
        }

        internal static IList<ChessPiece> TryMove(ChessBoardGrid targetGrid, IList<ChessBoardGrid> boardGrids)
        {
            var selectedPiece = boardGrids.SelectMany(x => x.Pieces).FirstOrDefault(y => y != null && y.Selected);
            if (selectedPiece == null || !IsTargetGridInRange(selectedPiece, targetGrid, boardGrids))
            {
                return null;
            }
            var containerGrid = boardGrids.First(x => x.Pieces.Contains(selectedPiece));
            var indexOfPieceInGrid = containerGrid.Pieces.IndexOf(selectedPiece);
            if (targetGrid.TryMovePiece(selectedPiece, out IList<ChessPiece> pieceLost))
            {
                containerGrid.Pieces[indexOfPieceInGrid] = null;
                RemoveCurrentSelection(boardGrids);
                ChangePlayerTurn();
                return pieceLost;
            }
            return null;
        }

        private static void ChangePlayerTurn() => CurrentPlayer = CurrentPlayer == Player.Red ? Player.Black : Player.Red;

        private static bool IsTargetGridInRange(ChessPiece piece, ChessBoardGrid targetGrid, IList<ChessBoardGrid> boardGrids)
        {
            //a leopard is able to go anywhere
            if (piece.PieceType == PieceType.Leopard)
            {
                return true;
            }

            var containerGrid = boardGrids.First(x => x.Pieces.Contains(piece));
            if ((containerGrid.Pos.Y == targetGrid.Pos.Y && Math.Abs(containerGrid.Pos.X - targetGrid.Pos.X) == 1) ||
                (containerGrid.Pos.X == targetGrid.Pos.X && Math.Abs(containerGrid.Pos.Y - targetGrid.Pos.Y) == 1))
            {
                return true;
            }
            return false;
        }
    }
}
