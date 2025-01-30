using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using RacingGame.ViewModels;

namespace RacingGame.Views
{
    public partial class GameControl : UserControl
    {
        public GameControl()
        {
            InitializeComponent();
            this.Loaded += GameControl_Loaded;
            this.Unloaded += GameControl_Unloaded;
        }

        private void GameControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.Focus();
            this.KeyDown += GameControl_KeyDown;
            this.KeyUp += GameControl_KeyUp;
        }

        private void GameControl_Unloaded(object sender, RoutedEventArgs e)
        {
            this.KeyDown -= GameControl_KeyDown;
            this.KeyUp -= GameControl_KeyUp;
        }

        private void GameControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (DataContext is GameViewModel viewModel)
            {
                switch (e.Key)
                {
                    case Key.A:
                        viewModel.MoveLeftCommand.Execute(null);
                        break;
                    case Key.D:
                        viewModel.MoveRightCommand.Execute(null);
                        break;
                    case Key.Escape:
                        if (viewModel.IsPaused)
                        {
                            viewModel.ResumeGameCommand.Execute(null);
                        }
                        else
                        {
                            viewModel.PauseGameCommand.Execute(null);
                        }
                        break;
                }
            }
        }

        private void GameControl_KeyUp(object sender, KeyEventArgs e)
        {
            if (DataContext is GameViewModel viewModel)
            {
                if (e.Key == Key.A || e.Key == Key.D)
                {
                    viewModel.StopMoveCommand.Execute(null);
                }
            }
        }
    }
}
