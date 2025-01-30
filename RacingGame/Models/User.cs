using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace RacingGame.Models
{
    public class User
    {
        public int id {  get; set; }
        public string login {  get; set; }
        public string pass {  get; set; }
        public int money { get; set; }
        public double distance { get; set; }

        public User() { }

        public User(string login, string pass, int money, int distance)
        {
            this.login = login;
            this.pass = pass;
            this.money = money;
            this.distance = distance;
        }
    }
}
