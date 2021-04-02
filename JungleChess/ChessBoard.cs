using Microsoft.VisualStudio.PlatformUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Windows.Input;

namespace JungleChess
{
    public class ChessBoard : ViewModelBase
    {
        public bool IsDirty;
        public int Id;

        public IList<ChessStep> Steps = new List<ChessStep>();

        private ObservableCollection<ChessBoardGrid> _BoardGrids;
        public ObservableCollection<ChessBoardGrid> BoardGrids
        {
            get => _BoardGrids;
            set
            {
                _BoardGrids = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<ChessPiece> ALostPieces { get; set; } = new ObservableCollection<ChessPiece>();
        public ObservableCollection<ChessPiece> BLostPieces { get; set; } = new ObservableCollection<ChessPiece>();

        public Player PlayerA { get; set; }
        public Player PlayerB { get; set; }

        private int _CurrentStep = -1;
        public int CurrentStep
        {
            get => _CurrentStep;
            set
            {
                _CurrentStep = value;
                RaisePropertyChanged();
                RaisePropertyChanged("IsReadOnly");
            }
        }
        public bool IsReadOnly
        {
            get => !string.IsNullOrWhiteSpace(Winner) || CurrentStep != Steps.Count - 1;
        }

        private Player _CurrentPlayer;
        public Player CurrentPlayer
        {
            get => _CurrentPlayer;
            set { _CurrentPlayer = value; RaisePropertyChanged(); }
        }

        private string _Winner;
        public string Winner
        {
            get => _Winner;
            set
            {
                _Winner = value;
                RaisePropertyChanged();
                RaisePropertyChanged("IsReadOnly");
            }
        }

        public ICommand PieceMouseClickCommand { get; set; }
        public ICommand PieceMouseDoubleClickCommand { get; set; }
        public ICommand GridMouseClickCommand { get; set; }
        public ICommand SurrenderClickCommand { get; set; }
        public ICommand DrawClickCommand { get; set; }
        public ICommand NextClickCommand { get; set; }
        public ICommand PreviousClickCommand { get; set; }
        public ICommand PassClickCommand { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "VSTHRD012:Provide JoinableTaskFactory where allowed", Justification = "<Pending>")]
        public ChessBoard(double boardLength)
        {
            ChessBoardGrid.SideLength = boardLength / 4;
            ChessPiece.SideLength = ChessBoardGrid.SideLength / 2;
            ChessPiece.SideLengthThumbnail = ChessPiece.SideLength / 2;
            PieceMouseDoubleClickCommand = new DelegateCommand(PieceDbClicked);
            PieceMouseClickCommand = new DelegateCommand(PieceClicked);
            GridMouseClickCommand = new DelegateCommand(GridClicked);
            SurrenderClickCommand = new DelegateCommand(Surrender);
            DrawClickCommand = new DelegateCommand(Draw);
            NextClickCommand = new DelegateCommand(NextStep);
            PreviousClickCommand = new DelegateCommand(PreviousStep);
            PassClickCommand = new DelegateCommand(Pass);

            CreateEmptyBoard();
        }

        private void CreateEmptyBoard()
        {
            BoardGrids = new ObservableCollection<ChessBoardGrid>
                 (Enumerable.Range(0, 16).Select(x => new ChessBoardGrid(null) { Pos = ConvertIndexToBoardGrid(x) }));
        }

        private void Pass(object obj)
        {
            Engine.FinishCurrentTurn(this);
        }

        private void PreviousStep(object obj)
        {
            if (CurrentStep > 0)
            {
                CurrentStep--;
                LoadFromStep(CurrentStep);
            }
        }

        private void NextStep(object obj)
        {
            if (CurrentStep < Steps.Count - 1)
            {
                CurrentStep++;
                LoadFromStep(CurrentStep);
            }
        }

        private void Draw(object obj)
        {
            Winner = "Draw";
            IsDirty = true;
        }

        private void Surrender(object obj)
        {
            var winner = CurrentPlayer == PlayerA ? PlayerB : PlayerA;
            Winner = $"{winner.Name} Win!";
            IsDirty = true;
        }

        internal void ClearBord()
        {
            Steps.Clear();
            ALostPieces.Clear();
            BLostPieces.Clear();
            CreateEmptyBoard();
            PlayerA = PlayerB = CurrentPlayer = null;
            Id = 0;
            IsDirty = false;
            CurrentStep = -1;
            Winner = null;
        }

        internal bool SaveGame()
        {
            if (IsReadOnly && !IsDirty)
            {
                return false;
            }
            GameSerializer.Save(this);
            IsDirty = false;
            return true;
        }

        internal void LoadGame(int id)
        {
            var game = GameSerializer.Load(id);
            if (game == null)
            {
                return;
            }
            ClearBord();
            PlayerA = game.PlayerA;
            PlayerB = game.PlayerB;
            Id = game.Id;
            Steps = game.Steps;
            Winner = game.Winner;
            CurrentStep = Steps.Count - 1;
            LoadFromStep(CurrentStep);
        }

        private void LoadFromStep(int step)
        {
            var chessStep = Steps[step];
            BoardGrids = new ObservableCollection<ChessBoardGrid>(chessStep.CurrentBoardGrids);
            CurrentPlayer = chessStep.CurrentPlayer;
            Engine.UpdateLostPieces(this);

        }

        internal void NewGame()
        {
            if (!CreatePlayers())
            {
                return;
            }
            var pieces = CreateRandomPieces(); ;
            BoardGrids = new ObservableCollection<ChessBoardGrid>
                (pieces.Select(x => new ChessBoardGrid(x) { Pos = ConvertIndexToBoardGrid(pieces.IndexOf(x)) }));

        }

        private bool CreatePlayers()
        {
            var inputBox = new PlayersInputBox();
            if (inputBox.ShowDialog() == true)
            {
                PlayerA = new Player { Name = inputBox.PlayerAName, MoveFirst = true };
                PlayerB = new Player { Name = inputBox.PlayerBName };
                CurrentPlayer = PlayerA;
                return true;
            }
            return false;
        }
        private ObservableCollection<ChessPiece> CreateRandomPieces()
        {
            var pieces = new List<ChessPiece>
            {
                new ChessPiece(PieceType.Mouse, PieceColor.Red),
                new ChessPiece(PieceType.Cat, PieceColor.Red),
                new ChessPiece(PieceType.Dog, PieceColor.Red),
                new ChessPiece(PieceType.Wolf, PieceColor.Red),
                new ChessPiece(PieceType.Leopard, PieceColor.Red) ,
                new ChessPiece(PieceType.Tiger, PieceColor.Red),
                new ChessPiece(PieceType.Lion, PieceColor.Red),
                new ChessPiece(PieceType.Elephant, PieceColor.Red),
                new ChessPiece(PieceType.Mouse, PieceColor.Green),
                new ChessPiece(PieceType.Cat, PieceColor.Green),
                new ChessPiece(PieceType.Dog, PieceColor.Green),
                new ChessPiece(PieceType.Wolf, PieceColor.Green),
                new ChessPiece(PieceType.Leopard, PieceColor.Green),
                new ChessPiece(PieceType.Tiger, PieceColor.Green),
                new ChessPiece(PieceType.Lion, PieceColor.Green),
                new ChessPiece(PieceType.Elephant, PieceColor.Green)
            };
            return new ObservableCollection<ChessPiece>(pieces.OrderBy(i => Guid.NewGuid()));
        }

        private Point ConvertIndexToBoardGrid(int index)
        {
            return new Point(index % 4, index / 4);
        }

        private void PieceClicked(object obj)
        {
            if (IsReadOnly)
            {
                return;
            }
            if (obj is ChessPiece piece)
            {
                if (!Engine.TrySelect(piece, this))
                {
                    Engine.TryMove(piece, this);
                }
            }
        }

        private void PieceDbClicked(object obj)
        {
            if (IsReadOnly)
            {
                return;
            }
            if (obj is ChessPiece piece)
            {
                Engine.TryFaceUp(piece, this);
            }
        }

        private void GridClicked(object obj)
        {
            if (IsReadOnly)
            {
                return;
            }
            if (obj is ChessBoardGrid grid)
            {
                Engine.TryMove(grid, this);
            }
        }
    }
}
