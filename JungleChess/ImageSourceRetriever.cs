using System.Collections.Generic;

namespace JungleChess
{
    public class ImageSourceRetriever
    {
        static private readonly IDictionary<(PieceType, Player), string> _ImageSourceDic = new Dictionary<(PieceType, Player), string>
        {
            { (PieceType.Mouse, Player.Red),"/Res/mouse_red.png" },
            { (PieceType.Cat, Player.Red),"/Res/cat_red.png" },
            { (PieceType.Dog, Player.Red),"/Res/dog_red.png" },
            { (PieceType.Wolf, Player.Red),"/Res/wolf_red.png" },
            { (PieceType.Leopard, Player.Red),"/Res/Leopard_red.png" },
            { (PieceType.Tiger, Player.Red),"/Res/tiger_red.png" },
            { (PieceType.Lion, Player.Red),"/Res/lion_red.png" },
            { (PieceType.Elephant, Player.Red),"/Res/elephant_red.png" },
            { (PieceType.Mouse, Player.Black),"/Res/mouse_black.png" },
            { (PieceType.Cat, Player.Black),"/Res/cat_black.png" },
            { (PieceType.Dog, Player.Black),"/Res/dog_black.png" },
            { (PieceType.Wolf, Player.Black),"/Res/wolf_black.png" },
            { (PieceType.Leopard, Player.Black),"/Res/Leopard_black.png" },
            { (PieceType.Tiger, Player.Black),"/Res/tiger_black.png" },
            { (PieceType.Lion, Player.Black),"/Res/lion_black.png" },
            { (PieceType.Elephant, Player.Black),"/Res/elephant_black.png" }
        };
        static public string RetrieveImageSource(PieceType pieceType, Player player)
        {
            return _ImageSourceDic[(pieceType, player)];
        }
    }
}