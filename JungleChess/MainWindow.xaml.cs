using System;
using System.Windows;

namespace JungleChess
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ChessBoard _ChessBoard;
        public MainWindow()
        {
            InitializeComponent();
            _ChessBoard = new ChessBoard(Math.Min(ChessBoard.Width, ChessBoard.Height));
            DataContext = _ChessBoard;
        }

        private void NewGame(object sender, RoutedEventArgs e)
        {
            if (TryEndGame())
            {
                _ChessBoard.NewGame();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!TryEndGame())
            {
                e.Cancel = true;
            }
        }

        private bool TryEndGame()
        {
            var close = true;
            if (_ChessBoard.IsDirty)
            {
                var result = MessageBox.Show(
                    "Do you want to save the game?",
                    "Jungle Chess",
                    MessageBoxButton.YesNoCancel,
                    MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    _ChessBoard.SaveGame();
                }
                else if (result == MessageBoxResult.Cancel)
                {
                    close = false;
                }
            }
            if (close)
            {
                _ChessBoard.ClearBord();
            }
            return close;
        }

        private void SaveGame(object sender, RoutedEventArgs e)
        {
            if (_ChessBoard.SaveGame())
            {
                MessageBox.Show("Game has been saved");
            }
        }

        private void LoadGame(object sender, RoutedEventArgs e)
        {
            var histories = GameSerializer.GetHistories();
            var window = new LoadGameWindow(histories);
            if (window.ShowDialog() == true)
            {
                if (TryEndGame())
                {
                    _ChessBoard.LoadGame(window.IdToLoad);
                }
            }
        }
    }
}
