namespace JungleChess
{
    public class Engine
    {
        static public void TryFaceUp(ChessBoardGrid boardGrid)
        {
            if (boardGrid.PieceOnTree == null && boardGrid.PieceInHole == null && !boardGrid.PieceOnLand.FaceUp)
            {
                boardGrid.PieceOnLand.FaceUp = true;
            }
        }
    }
}
