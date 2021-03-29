using System.Windows;

namespace JungleChess
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ChessBoard chessBoard;
        public MainWindow()
        {
            InitializeComponent();
            chessBoard = new ChessBoard((int)ChessBoard.Width, (int)ChessBoard.Height);
            DataContext = chessBoard;
        }
    }
}
