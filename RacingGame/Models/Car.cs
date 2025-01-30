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

        public bool CheckCollision(double x, double y, double width, double height)
        {
            return IsColliding(x, y, width, height, X, Y, 95, 150);
        }

        private bool IsColliding(double x1, double y1, double width1, double height1, double x2, double y2, double width2, double height2)
        {
            double collisionMargin = 13;
            return !(x1 + collisionMargin > x2 + width2 - collisionMargin || x1 + width1 - collisionMargin < x2 + collisionMargin ||
                     y1 + collisionMargin > y2 + height2 - collisionMargin || y1 + height1 - collisionMargin < y2 + collisionMargin);
        }
    }
}
