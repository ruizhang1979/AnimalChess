using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace JungleChess
{
    /// <summary>
    /// Interaction logic for PlayersInputBox.xaml
    /// </summary>
    public partial class PlayersInputBox : Window, INotifyPropertyChanged, IDataErrorInfo
    {
        private string _PlayerAName;
        private string _PlayerBName;

        public event PropertyChangedEventHandler PropertyChanged;

        public PlayersInputBox(string defaultPlayerA = "Player 1", string defaultPlayerB = "Player 2")
        {
            InitializeComponent();
            DataContext = this;
            Title = "Please input players' names.";
            _PlayerAName = defaultPlayerA;
            _PlayerBName = defaultPlayerB;
        }

        private void btnDialogOk_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidatePlayerName(_PlayerAName, "PlayerAName") ||
                !ValidatePlayerName(_PlayerBName, "PlayerBName"))
            {
                return;
            }
            DialogResult = true;
        }

        private bool ValidatePlayerName(string playerName, [CallerMemberName] string propertyName = null)
        {
            var valid = !string.IsNullOrEmpty(playerName);
            error = valid ? null : "Please input a value";
            RaisePropertyChanged(propertyName);
            return valid;
        }

        public string PlayerAName
        {
            set
            {
                _PlayerAName = value;
                ValidatePlayerName(_PlayerAName);
                RaisePropertyChanged();
            }
            get => _PlayerAName;
        }

        public string PlayerBName
        {
            set
            {
                _PlayerBName = value;
                ValidatePlayerName(_PlayerBName);
                RaisePropertyChanged();
            }
            get => _PlayerBName;
        }

        private string error { get; set; }

        string IDataErrorInfo.Error => throw new NotImplementedException();

        public string this[string columnName]
        {
            get
            {
                if (columnName == "PlayerAName" || columnName == "PlayerBName")
                {
                    return error;
                }
                return null;
            }
        }

        private void RaisePropertyChanged(string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
