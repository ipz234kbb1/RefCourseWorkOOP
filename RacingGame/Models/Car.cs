using System;

namespace RacingGame.Models
{
    public class Car : GameObject
    {
        private const double MinX = 105;
        private const double MaxX = 595;
        private const double CarWidth = 95;
        private const double CarHeight = 150;
        public bool MovingLeft { get; set; }
        public bool MovingRight { get; set; }

        public string Image { get; set; }
        public int Price { get; set; }

        public void UpdatePosition(double horizontalSpeed)
        {
            if (MovingLeft)
            {
                X = Math.Max(MinX, X - horizontalSpeed);
            }
            else if (MovingRight)
            {
                X = Math.Min(MaxX, X + horizontalSpeed);
            }
        }

        public override bool CheckCollision(double x, double y, double width, double height)
        {
            return IsColliding(x, y, width, height, X, Y, CarWidth, CarHeight);
        }
        
    }
}
