using System;
using System.Collections.Generic;
using System.Linq;

namespace JungleChess
{
    public class Engine
    {
        static public void TryFaceUp(ChessPiece piece, ChessBoard chessBoard)
        {
            var boardGrid = chessBoard.BoardGrids.First(x => x.Pieces.Contains(piece));
            if (boardGrid.Pieces[0] == null && boardGrid.Pieces[2] == null && !boardGrid.Pieces[1].FaceUp)
            {
                boardGrid.Pieces[1].FaceUp = true;
                FinishCurrentTurn(chessBoard);
            }
        }

        private static void FinishCurrentTurn(ChessBoard chessBoard)
        {
            ClearSelection(chessBoard.BoardGrids);
            ChangePlayer(chessBoard);
        }

        static public bool TrySelect(ChessPiece piece, ChessBoard chessBoard)
        {
            if (!piece.FaceUp || piece.Player != chessBoard.CurrentPlayer)
            {
                return false;
            }
            if (!piece.Selected)
            {
                ClearSelection(chessBoard.BoardGrids);
                piece.Selected = true;
            }
            return true;
        }


        static void ClearSelection(IList<ChessBoardGrid> boardGrids)
        {
            var selectedPiece = boardGrids.SelectMany(x => x.Pieces).FirstOrDefault(y => y != null && y.Selected);
            if (selectedPiece != null)
            {
                selectedPiece.Selected = false;
            }
        }

        internal static void TryMove(ChessPiece targetPiece, ChessBoard chessBoard)
        {
            var targetGrid = chessBoard.BoardGrids.First(x => x.Pieces.Contains(targetPiece));
            TryMove(targetGrid, chessBoard);
        }

        internal static void TryMove(ChessBoardGrid targetGrid, ChessBoard chessBoard)
        {
            var selectedPiece = chessBoard.BoardGrids.SelectMany(x => x.Pieces).FirstOrDefault(y => y != null && y.Selected);
            if (selectedPiece == null || !IsTargetGridInRange(selectedPiece, targetGrid, chessBoard.BoardGrids))
            {
                return;
            }
            var containerGrid = chessBoard.BoardGrids.First(x => x.Pieces.Contains(selectedPiece));
            var indexOfPieceInGrid = containerGrid.Pieces.IndexOf(selectedPiece);
            if (targetGrid.TryMovePieceTo(selectedPiece, out IList<ChessPiece> pieceLost))
            {
                containerGrid.Pieces[indexOfPieceInGrid] = null;
                if (pieceLost != null)
                {
                    AddLostPieces(pieceLost, chessBoard);
                }
                FinishCurrentTurn(chessBoard);
            }
        }

        public static void ChangePlayer(ChessBoard chessBoard)
        {
            chessBoard.CurrentPlayer = chessBoard.CurrentPlayer == Player.Red ? Player.Black : Player.Red;
        }

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

        private static void AddLostPieces(IList<ChessPiece> piecesLost, ChessBoard chessBoard)
        {
            foreach (var lost in piecesLost)
            {
                if (lost.Player == Player.Red)
                {
                    chessBoard.LostRedPieces.Add(lost);
                }
                else
                {
                    chessBoard.LostBlackPieces.Add(lost);
                }
            }
        }
    }
}
