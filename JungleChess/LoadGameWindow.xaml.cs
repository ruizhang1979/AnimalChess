using System.Collections.Generic;
using System.Windows;

namespace JungleChess
{
    /// <summary>
    /// Interaction logic for LoadGameWindow.xaml
    /// </summary>
    public partial class LoadGameWindow : Window
    {
        public LoadGameWindow(IEnumerable<GameObject> histories)
        {
            InitializeComponent();
            DataContext = new { GameHistories = histories };
        }


        private void btnDialogOk_Click(object sender, RoutedEventArgs e)
        {
            var game = Histories.SelectedItem as GameObject;
            IdToLoad = game.Id;
            DialogResult = true;
        }

        public int IdToLoad { get; set; }
    }
}
