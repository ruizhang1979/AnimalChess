using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            chessBoard.ReSize((int)ChessBoard.Width, (int)ChessBoard.Height);
        }
    }
}
