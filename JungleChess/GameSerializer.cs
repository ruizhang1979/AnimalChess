using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JungleChess
{
    public class GameObject
    {
        public int Id { get; set; }
        public IList<ChessStep> Steps { get; set; }
        public string Time { get; set; }
        public Player PlayerA { get; set; }
        public Player PlayerB { get; set; }
        public string Winner { get; set; }
    }

    internal class GameSerializer
    {
        private const string _ConnectionStr = "JungleChess";
        static public void Save(ChessBoard chessBoard)
        {
            using var db = new LiteDatabase(_ConnectionStr);
            var gameObject = new GameObject
            {
                Id = chessBoard.Id,
                Steps = chessBoard.Steps,
                PlayerA = chessBoard.PlayerA,
                PlayerB = chessBoard.PlayerB,
                Time = DateTime.Now.ToString(),
                Winner = chessBoard.Winner
            };
            var collection = db.GetCollection<GameObject>("Games");
            var game = collection.FindById(gameObject.Id);
            if (game != null)
            {
                collection.Update(gameObject);
            }
            else
            {
                collection.Insert(gameObject);
            }
            chessBoard.Id = gameObject.Id;
        }
        static internal GameObject Load(int id)
        {
            using var db = new LiteDatabase(_ConnectionStr);
            var collection = db.GetCollection<GameObject>("Games");
            return collection.FindById(id);
        }

        static internal IEnumerable<GameObject> GetHistories()
        {
            using var db = new LiteDatabase(_ConnectionStr);
            var collection = db.GetCollection<GameObject>("Games");
            return collection.FindAll().ToList();
        }
    }
}
