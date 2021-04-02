using System.Collections.Generic;

namespace JungleChess
{
    public class ImageSourceRetriever
    {
        static private readonly IDictionary<(PieceType, PieceColor), string> _ImageSourceDic = new Dictionary<(PieceType, PieceColor), string>
        {
            { (PieceType.Mouse, PieceColor.Red),"/Res/mouse_red.png" },
            { (PieceType.Cat, PieceColor.Red),"/Res/cat_red.png" },
            { (PieceType.Dog, PieceColor.Red),"/Res/dog_red.png" },
            { (PieceType.Wolf, PieceColor.Red),"/Res/wolf_red.png" },
            { (PieceType.Leopard, PieceColor.Red),"/Res/Leopard_red.png" },
            { (PieceType.Tiger, PieceColor.Red),"/Res/tiger_red.png" },
            { (PieceType.Lion, PieceColor.Red),"/Res/lion_red.png" },
            { (PieceType.Elephant, PieceColor.Red),"/Res/elephant_red.png" },
            { (PieceType.Mouse, PieceColor.Green),"/Res/mouse_black.png" },
            { (PieceType.Cat, PieceColor.Green),"/Res/cat_black.png" },
            { (PieceType.Dog, PieceColor.Green),"/Res/dog_black.png" },
            { (PieceType.Wolf, PieceColor.Green),"/Res/wolf_black.png" },
            { (PieceType.Leopard, PieceColor.Green),"/Res/Leopard_black.png" },
            { (PieceType.Tiger, PieceColor.Green),"/Res/tiger_black.png" },
            { (PieceType.Lion, PieceColor.Green),"/Res/lion_black.png" },
            { (PieceType.Elephant, PieceColor.Green),"/Res/elephant_black.png" }
        };
        static public string RetrieveImageSource(PieceType pieceType, PieceColor color, bool faceUp)
        {
            if (!faceUp)
            {
                return "/Res/back.png";
            }
            else
                return _ImageSourceDic[(pieceType, color)];
        }
    }
}