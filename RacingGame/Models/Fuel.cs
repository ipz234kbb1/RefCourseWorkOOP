using System;

namespace RacingGame.Models
{
    public class Fuel : GameObject
    {
        public int Amount { get; set; }

        private const double FuelWidth = 70;
        private const double FuelHeight = 70;
        
        public static Fuel SpawnFuel(Random random, int[] lanePositions)
        {
            int lane = random.Next(0, 4);
            return new Fuel
            {
                Image = "/Resources/Items/fuel.png",
                X = lanePositions[lane],
                Y = -70,
                Amount = 100
            };
        }

        public override bool CheckCollision(double x, double y, double width, double height)
        {
            return IsColliding(x, y, width, height, X, Y, FuelWidth, FuelHeight);
        }
        
    }
}
