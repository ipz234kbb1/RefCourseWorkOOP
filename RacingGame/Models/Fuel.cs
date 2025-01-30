using System;

namespace RacingGame.Models
{
    public class Fuel : GameObject
    {
        public int Amount { get; set; }

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

        public bool CheckCollision(double x, double y, double width, double height)
        {
            return IsColliding(x, y, width, height, X, Y, 70, 70);
        }

        private bool IsColliding(double x1, double y1, double width1, double height1, double x2, double y2, double width2, double height2)
        {
            double collisionMargin = 13;
            return !(x1 + collisionMargin > x2 + width2 - collisionMargin || x1 + width1 - collisionMargin < x2 + collisionMargin ||
                     y1 + collisionMargin > y2 + height2 - collisionMargin || y1 + height1 - collisionMargin < y2 + collisionMargin);
        }
    }
}
