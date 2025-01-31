using RacingGame.Models;
using RacingGame.Views;
using System.Windows.Input;
using System.Data.SQLite;
using System.Windows;
using System.Linq;

namespace RacingGame.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        public ICommand RegisterCommand { get; }
        public ICommand BackToMainMenuCommand { get; }

        private string _login;
        private string _password;
        private string _confirmPassword;
        private readonly MainWindowViewModel _mainWindowViewModel;

        public RegisterViewModel(MainWindowViewModel mainWindowViewModel)
        {
            _mainWindowViewModel = mainWindowViewModel;
            RegisterCommand = new RelayCommand(Register);
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

        public string ConfirmPassword
        {
            get => _confirmPassword;
            set
            {
                _confirmPassword = value;
                OnPropertyChanged(nameof(ConfirmPassword));
            }
        }

        private void Register()
        {
            if (string.IsNullOrEmpty(Login) || string.IsNullOrEmpty(Password))
            {
                MessageBox.Show("Введіть логін та пароль.");
                return;
            }

            if (Password != ConfirmPassword)
            {
                MessageBox.Show("Паролі не співпадають.");
                return;
            }

            using (var context = new ApplicationContext())
            {
                var userExists = context.Users.Any(u => u.login == Login);
                if (userExists)
                {
                    MessageBox.Show("Аккаунт з таким логіном вже існує!. Оберіть інший логін.");
                    return;
                }

                var user = new User(Login, Password, 0, 0);
                context.Users.Add(user);
                context.SaveChanges();

                MessageBox.Show("Реєстрація успішна.");
                BackToMainMenu();
            }
        }

        private void BackToMainMenu()
        {
            _mainWindowViewModel.CurrentView = new MainMenuView();
        }
    }
}
