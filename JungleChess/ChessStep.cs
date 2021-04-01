using System.Collections.Generic;

namespace JungleChess
{
    public class ChessStep
    {
        public IList<ChessBoardGrid> CurrentBoardGrids { get;  set; }
        public Player CurrentPlayer { get; set; }
    }
}
