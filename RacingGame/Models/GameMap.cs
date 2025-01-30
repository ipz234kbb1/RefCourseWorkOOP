namespace RacingGame.Models
{
    public class GameMap : GameObject
    {
        private double _map1Y;
        private double _map2Y;

        public string Image { get; set; }
        public int Price { get; set; }
        public double Map1Y
        {
            get => _map1Y;
            set
            {
                _map1Y = value;
                OnPropertyChanged(nameof(Map1Y));
            }
        }

        public double Map2Y
        {
            get => _map2Y;
            set
            {
                _map2Y = value;
                OnPropertyChanged(nameof(Map2Y));
            }
        }
    }
}
