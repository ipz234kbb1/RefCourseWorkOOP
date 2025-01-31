using System;

namespace RacingGame.Models
{
    public class Car : GameObject
    {
        public bool MovingLeft { get; set; }
        public bool MovingRight { get; set; }

        public string Image { get; set; }
        public int Price { get; set; }

        public void UpdatePosition(double horizontalSpeed)
        {
            if (MovingLeft)
            {
                X = Math.Max(105, X - horizontalSpeed);
            }
            else if (MovingRight)
            {
                X = Math.Min(595, X + horizontalSpeed);
            }
        }

        public override bool CheckCollision(double x, double y, double width, double height)
        {
            return IsColliding(x, y, width, height, X, Y, 95, 150);
        }
        
    }
}
