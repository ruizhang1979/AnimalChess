using System.Drawing;

namespace JungleChess
{
    public enum PieceType
    {
        Mouse,
        Cat,
        Dog,
        Wolf,
        Leopard,
        Tiger,
        Lion,
        Elephant
    }

    public enum Player
    {
        Red,
        Black
    }

    public enum Situation
    {
        Hole,
        Land,
        Tree
    }

    public class ChessPiece : ViewModelBase
    {
        private bool _FaceUp;
        public bool FaceUp
        {
            get => _FaceUp;
            set
            {
                if (!_FaceUp)
                {
                    _FaceUp = true;
                    ImageSource = ImageSourceRetriever.RetrieveImageSource(PieceType, Player);
                }
            }
        }

        private bool _Selected;
        public bool Selected
        {
            get => _Selected;
            set { _Selected = value; RaisePropertyChanged(); }
        }

        private Point _Pos;
        public Point Pos
        {
            get => _Pos;
            set { _Pos = value; RaisePropertyChanged(); }
        }

        public Situation Situation;

        public PieceType PieceType { get; private set; }

        private double _SideLength;
        public double SideLength
        {
            get => _SideLength;
            set { _SideLength = value; RaisePropertyChanged(); }
        }

        public Player Player { get; private set; }

        private string _ImageSource;
        public string ImageSource
        {
            get => _ImageSource;
            set { _ImageSource = value; RaisePropertyChanged(); }
        }

        public ChessPiece(PieceType pieceType, Player player)
        {
            PieceType = pieceType;
            Player = player;
            Situation = Situation.Land;
            ImageSource = "/Res/back.png";
        }
    }
}
