using Firetrack.Models;
using Firetrack.Services;
using Microsoft.Maui.Controls;

namespace Firetrack
{
    public partial class App : Application
    {
        public static UserModel? CurrentUser { get; set; }
        public static DatabaseService? Database { get; private set; }

        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            // Initialize database once
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "firetrack.db3");
            Database = new DatabaseService(dbPath);
            SeedData(); // Fire-and-forget seeding

            // Return the main window with AppShell as the root page
            return new Window(new AppShell());
        }

        private async void SeedData()
        {
            var db = Database!;

            // Ensure test users exist
            var admin = await db.GetUserByUsernameAsync("admin");
            if (admin == null)
            {
                await db.SaveUserAsync(new UserModel
                {
                    Username = "admin",
                    Password = "admin123",   // store hashed later
                    FullName = "Admin Chief",
                    Role = "Admin"
                });
            }

            var user = await db.GetUserByUsernameAsync("user");
            if (user == null)
            {
                await db.SaveUserAsync(new UserModel
                {
                    Username = "user",
                    Password = "user123",
                    FullName = "John Firefighter",
                    Role = "Personnel"
                });
            }

            // Seed equipment (only if empty)
            var equipments = await db.GetEquipmentsAsync();
            if (equipments.Count == 0)
            {
                await db.SaveEquipmentAsync(new EquipmentModel
                {
                    QRCode = "HOSE001",
                    Name = "Fire Hose 1",
                    Type = "Hose",
                    Status = "Available",
                    LastUpdated = DateTime.Now
                });
                await db.SaveEquipmentAsync(new EquipmentModel
                {
                    QRCode = "NOZZLE001",
                    Name = "High Pressure Nozzle",
                    Type = "Nozzle",
                    Status = "Issued",
                    AssignedToUsername = "user",
                    LastUpdated = DateTime.Now
                });
            }
        }
    }
}