using System;

namespace RacingGame.Models
{
    public class Obstacle : GameObject
    {
        private bool _isActive;
        private const double ObstacleWidth = 90;
        private const double ObstacleHeight = 150;
        private const double PositionOffset = 20;
        public bool IsActive
        {
            get => _isActive;
            set
            {
                _isActive = value;
                OnPropertyChanged(nameof(IsActive));
            }
        }

        public void Activate()
        {
            IsActive = true;
        }

        public void Deactivate()
        {
            IsActive = false;
        }

        public static readonly string[] ObstacleImages =
        {
            "/Resources/Cars/car2.png",
            "/Resources/Cars/car3.png",
            "/Resources/Cars/car4.png",
            "/Resources/Cars/car5.png",
            "/Resources/Cars/car6.png",
            "/Resources/Cars/car1.png",
            "/Resources/Cars/car7.png"
        };

        public static Obstacle SpawnObstacle(Random random, int[] lanePositions)
        {
            int lane = random.Next(0, 4);

            var newObstacle = new Obstacle
            {
                Image = ObstacleImages[random.Next(0, ObstacleImages.Length)],
                X = lanePositions[lane] - PositionOffset,
                Y = -160
            };

            return newObstacle;
        }

        public override bool CheckCollision(double x, double y, double width, double height)
        {
            return IsColliding(x, y, width, height, X, Y, ObstacleWidth, ObstacleHeight);
        }
        
    }
}
