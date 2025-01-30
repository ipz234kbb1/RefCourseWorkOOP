using RacingGame.Models;
using RacingGame.Views;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace RacingGame.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public ICommand LogInCommand { get; }
        public ICommand BackToMainMenuCommand { get; }

        private string _login;
        private string _password;
        private readonly MainWindowViewModel _mainWindowViewModel;

        public LoginViewModel(MainWindowViewModel mainWindowViewModel)
        {
            _mainWindowViewModel = mainWindowViewModel;
            LogInCommand = new RelayCommand(LogIn);
            BackToMainMenuCommand = new RelayCommand(BackToMainMenu);
        }

        public string Login
        {
            get => _login;
            set
            {
                _login = value;
                OnPropertyChanged(nameof(Login));
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        private void LogIn()
        {
            if (string.IsNullOrEmpty(Login) || string.IsNullOrEmpty(Password))
            {
                MessageBox.Show("Будь ласка, введіть логін та пароль.");
                return;
            }

            using (var context = new ApplicationContext())
            {
                var user = context.Users.FirstOrDefault(u => u.login == Login && u.pass == Password);

                if (user != null)
                {
                    _mainWindowViewModel.CurrentUser = user;
                    MessageBox.Show("Вхід успішний!");
                    _mainWindowViewModel.CurrentView = new MainMenuView();
                }
                else
                {
                    MessageBox.Show("Неправильний логін або пароль.");
                }
            }
        }

        private void BackToMainMenu()
        {
            _mainWindowViewModel.CurrentView = new MainMenuView();
        }
    }
}
