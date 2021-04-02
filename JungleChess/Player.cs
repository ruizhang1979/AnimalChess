namespace JungleChess
{
    public class Player : ViewModelBase
    {
        public string Name { get; set; }
        private PieceColor _Color;
        public PieceColor Color
        {
            get => _Color;
            set { _Color = value; RaisePropertyChanged(); }
        }
        public bool MoveFirst { get; set; }
    }
}
