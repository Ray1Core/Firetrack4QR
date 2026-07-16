using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Firetrack.Models;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls;

namespace Firetrack.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private string _username = string.Empty;
        private string _password = string.Empty;
        private string _errorMessage = string.Empty;

        public string Username
        {
            get => _username;
            set { _username = value; OnPropertyChanged(); }
        }

        public string Password
        {
            get => _password;
            set { _password = value; OnPropertyChanged(); }
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set { _errorMessage = value; OnPropertyChanged(); }
        }

        public ICommand LoginCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new Command(OnLogin);
        }

        private async void OnLogin()
        {
            var db = App.Database;
            if (db == null)
            {
                ErrorMessage = "Database not available";
                return;
            }

            // Validate against database
            var user = await db.GetUserByUsernameAsync(Username);
            if (user != null && user.Password == Password)  // later hash passwords
            {
                App.CurrentUser = user;
                ErrorMessage = "";
                await Shell.Current.GoToAsync("//DashboardPage");
            }
            else
            {
                ErrorMessage = "Invalid username or password";
            }
        }
    }
}