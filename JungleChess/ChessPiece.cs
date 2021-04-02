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

    public enum PieceColor
    {
        None,
        Red,
        Green
    }

    public class ChessPiece : ViewModelBase
    {
        private PieceColor _Color;
        public PieceColor Color
        {
            get => _Color;
            set
            {
                _Color = value;
                ImageSource = ImageSourceRetriever.RetrieveImageSource(PieceType, _Color, FaceUp);
            }
        }

        private bool _FaceUp;
        public bool FaceUp
        {
            get => _FaceUp;
            set
            {
                _FaceUp = value;
                ImageSource = ImageSourceRetriever.RetrieveImageSource(PieceType, Color, _FaceUp);
            }
        }

        private PieceType _PieceType;
        public PieceType PieceType
        {
            get => _PieceType;
            set
            {
                _PieceType = value;
                ImageSource = ImageSourceRetriever.RetrieveImageSource(_PieceType, Color, FaceUp);
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

        static public double SideLength { get; set; }
        static public double SideLengthThumbnail { get; set; }

        private string _ImageSource;
        public string ImageSource
        {
            get => _ImageSource;
            set { _ImageSource = value; RaisePropertyChanged(); }
        }

        public ChessPiece(PieceType pieceType, PieceColor color)
        {
            _PieceType = pieceType;
            _Color = color;
            _ImageSource = "/Res/back.png";
        }

        internal ChessPiece DeepCopy()
        {
            return new ChessPiece(this.PieceType, this.Color)
            {
                _ImageSource = this.ImageSource,
                _Pos = this.Pos,
                _Selected = this.Selected,
                _FaceUp = this.FaceUp
            };
        }
    }
}
