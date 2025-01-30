using System.Windows;
using System.Windows.Interop; // Добавьте это пространство имен для использования SystemParameters
using RacingGame.ViewModels;

namespace RacingGame.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
            DataContext = new MainWindowViewModel();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is MainWindowViewModel viewModel)
            {
                viewModel.PropertyChanged += ViewModel_PropertyChanged;
            }
        }

        private void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "CurrentView" && DataContext is MainWindowViewModel viewModel)
            {
                if (viewModel.CurrentView is GameControl)
                {
                    MoveWindowToTopAndResize();
                }
            }
        }

        private void MoveWindowToTopAndResize()
        {
            this.Top = 0;
            this.Left = (SystemParameters.WorkArea.Width - this.Width) / 2;
            this.Height = SystemParameters.WorkArea.Height;
        }
    }
}
