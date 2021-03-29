using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

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

    public class ChessPiece : INotifyPropertyChanged
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
                    RaisePropertyChanged();
                }
                else
                {
                    return;
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

        private PieceType _Type;
        public PieceType Type
        {
            get => _Type; 
            set { _Type = value; RaisePropertyChanged(); }
        }

        private Player _Player;
        public Player Player
        {
            get => _Player;
            set { _Player = value; RaisePropertyChanged(); }
        }

        private ImageSource _Image;
        public ImageSource Image 
        {
            get => _Image;
            private set { _Image = value; RaisePropertyChanged(); }
        }

        public ChessPiece()
        {
            FaceUp = true;
            var fullFilePath = @"http://www.americanlayout.com/wp/wp-content/uploads/2012/08/C-To-Go-300x300.png";
            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(fullFilePath, UriKind.Absolute);
            bitmap.EndInit();
            Image = bitmap;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void RaisePropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
