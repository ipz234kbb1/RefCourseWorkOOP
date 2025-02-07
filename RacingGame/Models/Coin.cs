using System.Collections.ObjectModel;
using System;

namespace RacingGame.Models
{
    public class Coin : GameObject
    {
        private int _value;
        
        private const double CoinWidth = 70;
        private const double CoinHeight = 70;
        private const int CoinSpawnStep = 80;
        public int Value
        {
            get => _value;
            set
            {
                _value = value;
                OnPropertyChanged(nameof(Value));
            }
        }

        public void Collect()
        {
        }

        public static ObservableCollection<Coin> SpawnCoins(Random random, int[] lanePositions)
        {
            int lane = random.Next(0, 4);
            int coinValue = random.Next(0, 4);
            int coinAmount = 5;

            switch (coinValue)
            {
                case 0:
                    coinAmount = 5;
                    break;
                case 1:
                    coinAmount = 25;
                    break;
                case 2:
                    coinAmount = 100;
                    break;
                case 3:
                    coinAmount = 500;
                    break;
            }

            var coins = new ObservableCollection<Coin>();
            SpawnCoinCluster(lanePositions, lane, coinAmount, coins);

            return coins;
        }

        public override bool CheckCollision(double x, double y, double width, double height)
        {
            return IsColliding(x, y, width, height, X, Y, CoinWidth, CoinHeight);
        }

        private static void SpawnCoinCluster(int[] lanePositions, int lane, int coinAmount, ObservableCollection<Coin> coins)
        {
            for (int i = 0; i < 5; i++)
            {
                var newCoin = new Coin
                {
                    Image = $"/Resources/Items/Coin{coinAmount}.png",
                    X = lanePositions[lane],
                    Y = -CoinHeight - (i * CoinSpawnStep),
                    Value = coinAmount
                };

                coins.Add(newCoin);
            }
        }
    }
}
