using RacingGame.Models;
using RacingGame.Views;
using System.Windows.Input;

namespace RacingGame.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        public ICommand PlayCommand { get; }
        public ICommand LogInCommand { get; }
        public ICommand LeaderboardCommand { get; }
        public ICommand ExitCommand { get; }
        public ICommand RegisterCommand { get; }

        private object _currentView;
        private User _currentUser;

        public MainWindowViewModel()
        {
            PlayCommand = new RelayCommand(Play);
            LogInCommand = new RelayCommand(LogIn);
            LeaderboardCommand = new RelayCommand(ShowLeaderboard);
            ExitCommand = new RelayCommand(Exit);
            RegisterCommand = new RelayCommand(Register);

            CurrentView = new MainMenuView();
        }

        public object CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged(nameof(CurrentView));
            }
        }

        public User CurrentUser
        {
            get => _currentUser;
            set
            {
                _currentUser = value;
                OnPropertyChanged(nameof(CurrentUser));
            }
        }

        private void Play()
        {
            var mapAndCarSelectionViewModel = new MapAndCarSelectionViewModel(this);
            mapAndCarSelectionViewModel.RequestStartGame += StartGame;
            CurrentView = new MapAndCarSelectionControl { DataContext = mapAndCarSelectionViewModel };
        }

        private void LogIn()
        {
            CurrentView = new LoginControl { DataContext = new LoginViewModel(this) };
        }

        private void ShowLeaderboard()
        {
            var leaderboardViewModel = new LeaderboardViewModel();
            leaderboardViewModel.BackToMainMenuCommand = new RelayCommand(GoToMainMenu);
            CurrentView = new LeaderboardView { DataContext = leaderboardViewModel };
        }

        private void Exit()
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void Register()
        {
            CurrentView = new RegisterControl { DataContext = new RegisterViewModel(this) };
        }

        private void StartGame(string selectedCarImage, string selectedMapImage)
        {
            InitializeGame(selectedCarImage, selectedMapImage);
        }

        private void InitializeGame(string carImage, string mapImage)
        {
            var gameViewModel = new GameViewModel(carImage, mapImage, CurrentUser);
            gameViewModel.RequestMainMenu += GoToMainMenu;
            gameViewModel.RequestRetryGame += RetryGame;
            CurrentView = new GameControl { DataContext = gameViewModel };
        }

        private void GoToMainMenu()
        {
            CurrentView = new MainMenuView();
        }

        private void RetryGame()
        {
        }
    }
}
