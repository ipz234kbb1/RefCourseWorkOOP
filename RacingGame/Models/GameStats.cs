using RacingGame.ViewModels;

namespace RacingGame.Models
{
    public class GameStats : BaseViewModel
    {
        private double _distance;
        private double _fuel;
        private int _coinCount;
        private double _speed;
        private double _horizontalSpeed;
        private bool _isGameOver;
        private bool _isPaused;
        private bool _isSlowingDown;

        public double Distance
        {
            get => _distance;
            set
            {
                _distance = value;
                OnPropertyChanged(nameof(Distance));
                UpdateSpeed();
                UpdateHorizontalSpeed();
            }
        }

        public double Fuel
        {
            get => _fuel;
            set
            {
                _fuel = value;
                OnPropertyChanged(nameof(Fuel));
            }
        }

        public int CoinCount
        {
            get => _coinCount;
            set
            {
                _coinCount = value;
                OnPropertyChanged(nameof(CoinCount));
            }
        }

        public double Speed
        {
            get => _speed;
            set
            {
                _speed = value;
                OnPropertyChanged(nameof(Speed));
            }
        }

        public double HorizontalSpeed
        {
            get => _horizontalSpeed;
            set
            {
                _horizontalSpeed = value;
                OnPropertyChanged(nameof(HorizontalSpeed));
            }
        }

        public bool IsGameOver
        {
            get => _isGameOver;
            set
            {
                _isGameOver = value;
                OnPropertyChanged(nameof(IsGameOver));
            }
        }

        public bool IsPaused
        {
            get => _isPaused;
            set
            {
                _isPaused = value;
                OnPropertyChanged(nameof(IsPaused));
            }
        }

        public bool IsSlowingDown
        {
            get => _isSlowingDown;
            set
            {
                _isSlowingDown = value;
                OnPropertyChanged(nameof(IsSlowingDown));
            }
        }

        public void UpdateSpeed()
        {
            Speed = 5 + (Distance / 100);
        }

        public void UpdateHorizontalSpeed()
        {
            HorizontalSpeed = 5 + (Distance / 100);
        }
    }
}
