using RacingGame.Models;
using RacingGame.Views;
using System;
using System.Windows.Input;

namespace RacingGame.ViewModels
{
    public class MapAndCarSelectionViewModel : BaseViewModel
    {
        private string _selectedMapImage;
        private string _selectedCarImage;
        private User _currentUser;

        public ICommand PreviousMapCommand { get; }
        public ICommand NextMapCommand { get; }
        public ICommand PreviousCarCommand { get; }
        public ICommand NextCarCommand { get; }
        public ICommand BackToMainMenuCommand { get; }
        public ICommand StartGameCommand { get; }

        private readonly MainWindowViewModel _mainWindowViewModel;

        public event Action<string, string> RequestStartGame;

        private readonly string[] _mapImages = { "/Resources/Maps/map1.jpg", "/Resources/Maps/map2.jpg", "/Resources/Maps/map3.jpg" };
        private readonly string[] _carImages = { "/Resources/Cars/car2.png", "/Resources/Cars/car3.png", "/Resources/Cars/car4.png", "/Resources/Cars/car5.png", "/Resources/Cars/car6.png", "/Resources/Cars/car1.png", "/Resources/Cars/car7.png" };

        private int _currentMapIndex;
        private int _currentCarIndex;

        public MapAndCarSelectionViewModel(MainWindowViewModel mainWindowViewModel)
        {
            _mainWindowViewModel = mainWindowViewModel;

            PreviousMapCommand = new RelayCommand(PreviousMap);
            NextMapCommand = new RelayCommand(NextMap);
            PreviousCarCommand = new RelayCommand(PreviousCar);
            NextCarCommand = new RelayCommand(NextCar);
            BackToMainMenuCommand = new RelayCommand(BackToMainMenu);
            StartGameCommand = new RelayCommand(StartGame);

            _currentMapIndex = 0;
            _currentCarIndex = 0;
            SelectedMapImage = _mapImages[_currentMapIndex];
            SelectedCarImage = _carImages[_currentCarIndex];

            CurrentUser = _mainWindowViewModel.CurrentUser;
        }

        public string SelectedMapImage
        {
            get => _selectedMapImage;
            set
            {
                _selectedMapImage = value;
                OnPropertyChanged(nameof(SelectedMapImage));
            }
        }

        public string SelectedCarImage
        {
            get => _selectedCarImage;
            set
            {
                _selectedCarImage = value;
                OnPropertyChanged(nameof(SelectedCarImage));
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

        private void PreviousMap()
        {
            _currentMapIndex = (_currentMapIndex - 1 + _mapImages.Length) % _mapImages.Length;
            SelectedMapImage = _mapImages[_currentMapIndex];
        }

        private void NextMap()
        {
            _currentMapIndex = (_currentMapIndex + 1) % _mapImages.Length;
            SelectedMapImage = _mapImages[_currentMapIndex];
        }

        private void PreviousCar()
        {
            _currentCarIndex = (_currentCarIndex - 1 + _carImages.Length) % _carImages.Length;
            SelectedCarImage = _carImages[_currentCarIndex];
        }

        private void NextCar()
        {
            _currentCarIndex = (_currentCarIndex + 1) % _carImages.Length;
            SelectedCarImage = _carImages[_currentCarIndex];
        }

        private void BackToMainMenu()
        {
            _mainWindowViewModel.CurrentView = new MainMenuView();
        }

        private void StartGame()
        {
            RequestStartGame?.Invoke(SelectedCarImage, SelectedMapImage);
        }
    }
}
