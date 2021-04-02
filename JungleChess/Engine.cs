using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace JungleChess
{
    internal static class Engine
    {
        static internal void TryFaceUp(ChessPiece piece, ChessBoard chessBoard)
        {
            var boardGrid = chessBoard.BoardGrids.First(x => x.Pieces.Contains(piece));
            if (boardGrid.Pieces[0] == null && boardGrid.Pieces[2] == null && !boardGrid.Pieces[1].FaceUp)
            {
                boardGrid.Pieces[1].FaceUp = true;
                InitializePlayerColor(boardGrid.Pieces[1].Color, chessBoard);
                FinishCurrentTurn(chessBoard);
            }
        }

        private static void InitializePlayerColor(PieceColor color, ChessBoard chessBoard)
        {
            if (!chessBoard.Steps.Any())
            {
                chessBoard.PlayerA.Color = color;
                chessBoard.PlayerB.Color = color == PieceColor.Red ? PieceColor.Green : PieceColor.Red;
            }
        }

        internal static void FinishCurrentTurn(ChessBoard chessBoard)
        {
            ClearSelection(chessBoard.BoardGrids);
            ChangePlayer(chessBoard);
            UpdateLostPieces(chessBoard);
            SaveCurrentStep(chessBoard);
        }

        internal static void UpdateLostPieces(ChessBoard chessBoard)
        {
            chessBoard.ALostPieces.Clear();
            chessBoard.BLostPieces.Clear();

            var lostPieces = GetLostPieces(chessBoard);

            foreach (var lost in lostPieces)
            {
                if (lost.Color == chessBoard.PlayerA.Color)
                {
                    if (!chessBoard.ALostPieces.Any(x => x.PieceType == lost.PieceType))
                    {
                        chessBoard.ALostPieces.Add(lost);
                    }
                }
                else
                {
                    if (!chessBoard.BLostPieces.Any(x => x.PieceType == lost.PieceType))
                    {
                        chessBoard.BLostPieces.Add(lost);
                    }
                }
            }
        }

        private static IList<ChessPiece> GetLostPieces(ChessBoard chessBoard)
        {
            var lostPieces = new List<ChessPiece>();
            var currentPieces = chessBoard.BoardGrids.SelectMany(x => x.Pieces.Where(y => y != null));
            for (var c = PieceColor.Red; c <= PieceColor.Green; c++)
            {
                for (var t = PieceType.Mouse; t <= PieceType.Elephant; t++)
                {
                    if (!currentPieces.Any(x => x.Color == c && x.PieceType == t))
                    {
                        var lostPiece = new ChessPiece(t, c) { FaceUp = true };
                        lostPieces.Add(lostPiece);
                    }
                }
            }
            return lostPieces;
        }

        private static void SaveCurrentStep(ChessBoard chessBoard)
        {
            var boardGridsCopy = new ObservableCollection<ChessBoardGrid>
                (chessBoard.BoardGrids.Select(x => x.DeepCopy()));
            var step = new ChessStep
            {
                CurrentBoardGrids = boardGridsCopy,
                CurrentPlayer = new Player
                {
                    Name = chessBoard.CurrentPlayer.Name,
                    Color = chessBoard.CurrentPlayer.Color,
                    MoveFirst = chessBoard.CurrentPlayer.MoveFirst
                }
            };
            chessBoard.Steps.Add(step);
            chessBoard.IsDirty = true;
            chessBoard.CurrentStep = chessBoard.Steps.Count - 1;
        }

        static internal bool TrySelect(ChessPiece piece, ChessBoard chessBoard)
        {
            if (!piece.FaceUp || piece.Color != chessBoard.CurrentPlayer.Color)
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

        static private void ClearSelection(IList<ChessBoardGrid> boardGrids)
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
            if (targetGrid.TryMovePieceTo(selectedPiece))
            {
                containerGrid.Pieces[indexOfPieceInGrid] = null;
                FinishCurrentTurn(chessBoard);
            }
        }

        internal static void ChangePlayer(ChessBoard chessBoard)
        {
            chessBoard.CurrentPlayer =
                chessBoard.CurrentPlayer == chessBoard.PlayerA ?
                chessBoard.PlayerB : chessBoard.PlayerA;
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
    }
}
