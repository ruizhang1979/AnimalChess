using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

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

        public Image Image 
        {
            get;
            private set;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void RaisePropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
