using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Threading;
using RacingGame.Models;

namespace RacingGame.ViewModels
{
    public class GameViewModel : BaseViewModel
    {
        private readonly DispatcherTimer _gameTimer;
        private readonly Random _random;
        private double _lastFuelSpawnDistance;
        private bool _coinsSaved;
        private bool _distanceUpdated;

        public Car Car { get; set; }
        public GameMap Map { get; set; }
        public GameStats Stats { get; set; }

        public ObservableCollection<Coin> Coins { get; set; }
        public ObservableCollection<Obstacle> Obstacles { get; set; }
        public ObservableCollection<Fuel> Fuels { get; set; }

        public ICommand MoveLeftCommand { get; }
        public ICommand MoveRightCommand { get; }
        public ICommand StopMoveCommand { get; }
        public ICommand GoToMainMenuCommand { get; }
        public ICommand RetryGameCommand { get; }
        public ICommand PauseGameCommand { get; }
        public ICommand ResumeGameCommand { get; }

        private readonly User _currentUser;

        public event Action RequestMainMenu;
        public event Action RequestRetryGame;

        private static readonly int[] LanePositions = { 150, 295, 440, 590 };

        public GameViewModel(string carImage, string mapImage, User currentUser)
        {
            Car = new Car
            {
                Image = carImage,
                X = 327,
                Y = 600
            };

            Map = new GameMap
            {
                Image = mapImage,
                Map1Y = 0,
                Map2Y = -900
            };

            Stats = new GameStats
            {
                Distance = 0,
                Fuel = 100,
                CoinCount = 0,
                Speed = 5,
                HorizontalSpeed = 5
            };

            _random = new Random();
            _gameTimer = new DispatcherTimer();
            _gameTimer.Interval = TimeSpan.FromMilliseconds(1000 / 120);
            _gameTimer.Tick += GameTick;
            _gameTimer.Start();

            MoveLeftCommand = new RelayCommand(MoveLeft);
            MoveRightCommand = new RelayCommand(MoveRight);
            StopMoveCommand = new RelayCommand(StopMove);
            GoToMainMenuCommand = new RelayCommand(GoToMainMenu);
            RetryGameCommand = new RelayCommand(RetryGame);
            PauseGameCommand = new RelayCommand(PauseGame);
            ResumeGameCommand = new RelayCommand(ResumeGame);

            Coins = new ObservableCollection<Coin>();
            Obstacles = new ObservableCollection<Obstacle>();
            Fuels = new ObservableCollection<Fuel>();

            _currentUser = currentUser;
            _coinsSaved = false;
            _distanceUpdated = false;
        }

        public bool IsPaused
        {
            get => Stats.IsPaused;
            set
            {
                Stats.IsPaused = value;
                OnPropertyChanged(nameof(IsPaused));
            }
        }

        private void GameTick(object sender, EventArgs e)
        {
            if (!Stats.IsGameOver)
            {
                if (Stats.Speed > 0 && !Stats.IsSlowingDown)
                {
                    Stats.Distance += Stats.Speed / 60; 
                }

                if (Stats.Fuel > 0)
                {
                    Stats.Fuel -= 0.1;
                }
                else
                {
                    StartSlowingDown();
                }

                if (Stats.IsSlowingDown)
                {
                    SlowDownCar();
                }

                UpdateMapPosition();
                AddGameObjects();
                UpdateGameObjectPositions();
                CheckCollisions();
                UpdateCarPosition();
            }
        }

        private void StartSlowingDown()
        {
            Stats.IsSlowingDown = true;
        }

        private void SlowDownCar()
        {
            if (Stats.Speed > 0)
            {
                Stats.Speed -= 0.03;
            }
            else
            {
                EndGameDueToFuel();
            }
        }

        private void EndGameDueToFuel()
        {
            Stats.IsGameOver = true;
            _gameTimer.Stop();
            SaveUserProgress();
        }

        private void SaveUserProgress()
        {
            if (!_coinsSaved || !_distanceUpdated)
            {
                if (!_coinsSaved)
                {
                    _currentUser.money += Stats.CoinCount;
                    _coinsSaved = true;
                }

                if (!_distanceUpdated && Stats.Distance > _currentUser.distance)
                {
                    _currentUser.distance = Stats.Distance;
                    _distanceUpdated = true;
                }

                using (var context = new ApplicationContext())
                {
                    var user = context.Users.Find(_currentUser.id);
                    if (user != null)
                    {
                        user.money = _currentUser.money;
                        user.distance = _currentUser.distance;
                        context.SaveChanges();
                    }
                }
            }
        }

        private void UpdateMapPosition()
        {
            Map.Map1Y += Stats.Speed;
            Map.Map2Y += Stats.Speed;

            if (Map.Map1Y >= 900)
            {
                Map.Map1Y = Map.Map2Y - 899;
            }

            if (Map.Map2Y >= 900)
            {
                Map.Map2Y = Map.Map1Y - 899;
            }
        }

        private void AddGameObjects()
        {
            if (_random.Next(0, 100) < 1)
            {
                int objectType = _random.Next(0, 2);

                if (objectType == 0)
                {
                    var newCoins = Coin.SpawnCoins(_random, LanePositions);
                    foreach (var coin in newCoins)
                    {
                        if (!IsSpawningOnOtherObjects(coin.X, coin.Y, 70, 70))
                        {
                            Coins.Add(coin);
                        }
                    }
                }
                else if (objectType == 1)
                {
                    var newObstacle = Obstacle.SpawnObstacle(_random, LanePositions);
                    if (!IsSpawningOnOtherObjects(newObstacle.X, newObstacle.Y, 90, 150))
                    {
                        Obstacles.Add(newObstacle);
                    }
                }
            }

            if (Stats.Distance - _lastFuelSpawnDistance >= 80)
            {
                var newFuel = Fuel.SpawnFuel(_random, LanePositions);
                if (!IsSpawningOnOtherObjects(newFuel.X, newFuel.Y, 70, 70))
                {
                    Fuels.Add(newFuel);
                    _lastFuelSpawnDistance = Stats.Distance;
                }
            }
        }

        private void UpdateGameObjectPositions()
        {
            foreach (var coin in Coins)
            {
                coin.Y += Stats.Speed;
            }

            foreach (var obstacle in Obstacles)
            {
                obstacle.Y += Stats.Speed;
            }

            foreach (var fuel in Fuels)
            {
                fuel.Y += Stats.Speed;
            }
        }

        private void UpdateCarPosition()
        {
            Car.UpdatePosition(Stats.HorizontalSpeed);
        }

        public void CheckCollisions()
        {
            for (int i = Coins.Count - 1; i >= 0; i--)
            {
                var coin = Coins[i];
                if (coin.CheckCollision(Car.X, Car.Y, 100, 150))
                {
                    Stats.CoinCount += coin.Value;
                    coin.Collect();
                    Coins.RemoveAt(i);
                }
            }

            for (int i = Obstacles.Count - 1; i >= 0; i--)
            {
                var obstacle = Obstacles[i];
                if (obstacle.CheckCollision(Car.X, Car.Y, 100, 150))
                {
                    Stats.Speed = 0;
                    Stats.IsGameOver = true;
                    obstacle.Deactivate();
                    _gameTimer.Stop();
                    SaveUserProgress();
                }
            }

            for (int i = Fuels.Count - 1; i >= 0; i--)
            {
                var fuel = Fuels[i];
                if (fuel.CheckCollision(Car.X, Car.Y, 100, 150))
                {
                    Stats.IsGameOver = false;
                    Stats.Fuel = 100;
                    _gameTimer.Start();
                    Fuels.RemoveAt(i);
                }
            }
        }

        private bool IsSpawningOnOtherObjects(double x, double y, double width, double height)
        {
            foreach (var coin in Coins)
            {
                if (coin.CheckCollision(x, y, width, height))
                {
                    return true;
                }
            }

            foreach (var obstacle in Obstacles)
            {
                if (obstacle.CheckCollision(x, y, width, height))
                {
                    return true;
                }
            }

            foreach (var fuel in Fuels)
            {
                if (fuel.CheckCollision(x, y, width, height))
                {
                    return true;
                }
            }

            return false;
        }

        private void MoveLeft()
        {
            Car.MovingLeft = true;
        }

        private void MoveRight()
        {
            Car.MovingRight = true;
        }

        private void StopMove()
        {
            Car.MovingLeft = false;
            Car.MovingRight = false;
        }

        private void GoToMainMenu()
        {
            SaveUserProgress();
            RequestMainMenu?.Invoke();
        }

        private void RetryGame()
        {
            SaveUserProgress();
            ResetGame();
            RequestRetryGame?.Invoke();
        }

        private void ResetGame()
        {
            Stats.Distance = 0;
            Stats.Fuel = 100;
            Stats.CoinCount = 0;
            Stats.Speed = 5;
            Stats.HorizontalSpeed = 5;
            Stats.IsSlowingDown = false;
            Stats.IsGameOver = false;
            _lastFuelSpawnDistance = 0;
            Coins.Clear();
            Obstacles.Clear();
            Fuels.Clear();
            Car.X = 327;
            Car.Y = 600;
            _gameTimer.Start();
            _coinsSaved = false;
            _distanceUpdated = false;
        }

        private void PauseGame()
        {
            IsPaused = true;
            _gameTimer.Stop();
        }

        private void ResumeGame()
        {
            IsPaused = false;
            _gameTimer.Start();
        }
    }
}
