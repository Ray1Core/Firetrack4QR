using Firetrack.Models;
using Firetrack.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace Firetrack.ViewModels
{
    public class TransferViewModel : ViewModelBase
    {
        private readonly DatabaseService _db;
        private UserModel? _selectedOfficer;
        private EquipmentModel? _selectedEquipment;
        private string _manualEquipmentQR = string.Empty;
        private string _statusMessage = string.Empty;

        public ObservableCollection<UserModel> Users { get; set; } = new();
        public ObservableCollection<EquipmentModel> EquipmentList { get; set; } = new();

        public UserModel? SelectedOfficer
        {
            get => _selectedOfficer;
            set { _selectedOfficer = value; OnPropertyChanged(); }
        }

        public EquipmentModel? SelectedEquipment
        {
            get => _selectedEquipment;
            set { _selectedEquipment = value; OnPropertyChanged(); }
        }

        public string ManualEquipmentQR
        {
            get => _manualEquipmentQR;
            set { _manualEquipmentQR = value; OnPropertyChanged(); }
        }

        public string StatusMessage
        {
            get => _statusMessage;
            set { _statusMessage = value; OnPropertyChanged(); }
        }

        public ICommand TransferCommand { get; }
        public ICommand GoBackCommand { get; }

        public TransferViewModel()
        {
            _db = App.Database!;
            TransferCommand = new Command(OnTransfer);
            GoBackCommand = new Command(async () => await Shell.Current.GoToAsync(".."));
            _ = LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            var userList = await _db.GetUsersAsync();
            Users.Clear();
            foreach (var u in userList)
                Users.Add(u);

            var eqList = await _db.GetEquipmentsAsync();
            EquipmentList.Clear();
            foreach (var eq in eqList)
                EquipmentList.Add(eq);
        }

        private async void OnTransfer()
        {
            if (SelectedOfficer == null)
            {
                StatusMessage = "Please select the receiving officer.";
                return;
            }

            EquipmentModel? equipment = SelectedEquipment;
            if (equipment == null && !string.IsNullOrWhiteSpace(ManualEquipmentQR))
            {
                equipment = await _db.GetEquipmentByQRAsync(ManualEquipmentQR.Trim());
                if (equipment == null)
                {
                    StatusMessage = "No equipment found with that QR code.";
                    return;
                }
            }
            else if (equipment == null)
            {
                StatusMessage = "Please select or enter an equipment QR code.";
                return;
            }

            var transaction = new TransactionModel
            {
                EquipmentQR = equipment.QRCode,
                FromUser = App.CurrentUser?.Username ?? "admin",
                ToUser = SelectedOfficer.Username,
                Timestamp = DateTime.Now,
                Action = "Issue",
                Remarks = $"Issued to {SelectedOfficer.FullName}"
            };

            equipment.AssignedToUsername = SelectedOfficer.Username;
            equipment.Status = "Issued";
            equipment.LastUpdated = DateTime.Now;

            await _db.SaveTransactionAsync(transaction);
            await _db.SaveEquipmentAsync(equipment);

            StatusMessage = $"✅ Equipment '{equipment.Name}' issued to {SelectedOfficer.FullName}.";

            // Clear fields
            SelectedEquipment = null;
            ManualEquipmentQR = string.Empty;
            SelectedOfficer = null;

            // Refresh the list
            await LoadDataAsync();
        }
    }
}