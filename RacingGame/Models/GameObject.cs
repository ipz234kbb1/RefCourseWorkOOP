using System.ComponentModel;

namespace RacingGame.Models
{
    public class GameObject : INotifyPropertyChanged
    {
        private string _image;
        private double _x;
        private double _y;

        public string Image
        {
            get => _image;
            set
            {
                _image = value;
                OnPropertyChanged(nameof(Image));
            }
        }

        public double X
        {
            get => _x;
            set
            {
                _x = value;
                OnPropertyChanged(nameof(X));
            }
        }

        public double Y
        {
            get => _y;
            set
            {
                _y = value;
                OnPropertyChanged(nameof(Y));
            }
        }
        
        protected bool IsColliding(double x1, double y1, double width1, double height1, double x2, double y2, double width2, double height2)
        {
            double collisionMargin = 13;
            return !(x1 + collisionMargin > x2 + width2 - collisionMargin || x1 + width1 - collisionMargin < x2 + collisionMargin ||
                     y1 + collisionMargin > y2 + height2 - collisionMargin || y1 + height1 - collisionMargin < y2 + collisionMargin);
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
