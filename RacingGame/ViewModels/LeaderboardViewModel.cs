using RacingGame.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace RacingGame.ViewModels
{
    public class LeaderboardViewModel : BaseViewModel
    {
        private ObservableCollection<User> _users;

        public ObservableCollection<User> Users
        {
            get => _users;
            set
            {
                _users = value;
                OnPropertyChanged(nameof(Users));
            }
        }

        public ICommand BackToMainMenuCommand { get; set; }

        public LeaderboardViewModel()
        {
            LoadUsers();
        }

        private void LoadUsers()
        {
            using (var context = new ApplicationContext())
            {
                var users = context.Users.AsQueryable().OrderByDescending(u => u.distance).ToList();
                Users = new ObservableCollection<User>(users);
            }
        }
    }
}
