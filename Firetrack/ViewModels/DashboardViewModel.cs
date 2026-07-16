using Firetrack.Models;
using Firetrack.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace Firetrack.ViewModels
{
    public class DashboardViewModel : ViewModelBase
    {
        private string _welcomeMessage = string.Empty;
        private ObservableCollection<EquipmentModel> _myEquipment = new();

        public string WelcomeMessage
        {
            get => _welcomeMessage;
            set { _welcomeMessage = value; OnPropertyChanged(); }
        }

        public ObservableCollection<EquipmentModel> MyEquipment
        {
            get => _myEquipment;
            set { _myEquipment = value; OnPropertyChanged(); }
        }

        // Navigation commands
        public ICommand GoToScannerCommand { get; }
        public ICommand GoToTransferCommand { get; }   // <-- NEW
        public ICommand LogoutCommand { get; }

        public DashboardViewModel()
        {
            WelcomeMessage = $"Welcome, {App.CurrentUser?.FullName ?? "Firefighter"}!";
            LogoutCommand = new Command(OnLogout);

            GoToScannerCommand = new Command(async () =>
                await Shell.Current.GoToAsync("ScannerPage"));

            GoToTransferCommand = new Command(async () =>   // <-- NEW
                await Shell.Current.GoToAsync("TransferPage"));

            LoadEquipment();
        }

        private async void LoadEquipment()
        {
            if (App.CurrentUser == null) return;
            var db = App.Database;
            if (db == null) return;

            var equipment = await db.GetEquipmentsAssignedToUserAsync(App.CurrentUser.Username);
            MyEquipment.Clear();
            foreach (var item in equipment)
                MyEquipment.Add(item);
        }

        private async void OnLogout()
        {
            App.CurrentUser = null;
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}