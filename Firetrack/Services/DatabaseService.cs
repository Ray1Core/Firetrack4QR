using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Firetrack.Models;

namespace Firetrack.Services
{
    public class DatabaseService
    {
        private readonly SQLiteAsyncConnection _database;

        public DatabaseService(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);

            // ========== ENABLE FOREIGN KEY CONSTRAINTS ==========
            _database.ExecuteAsync("PRAGMA foreign_keys = ON;");
            // ===================================================

            // Create tables
            _database.CreateTableAsync<EquipmentModel>().Wait();
            _database.CreateTableAsync<TransactionModel>().Wait();
            _database.CreateTableAsync<UserModel>().Wait();
        }

        // ---------- Equipment ----------
        public Task<List<EquipmentModel>> GetEquipmentsAsync() =>
            _database.Table<EquipmentModel>().ToListAsync();

        public Task<List<EquipmentModel>> GetEquipmentsAssignedToUserAsync(string username) =>
            _database.Table<EquipmentModel>().Where(e => e.AssignedToUsername == username).ToListAsync();

        public Task<int> SaveEquipmentAsync(EquipmentModel equipment) =>
            _database.InsertOrReplaceAsync(equipment);

        public Task<int> DeleteEquipmentAsync(EquipmentModel equipment) =>
            _database.DeleteAsync(equipment);

        // ---------- Transactions ----------
        public Task<int> SaveTransactionAsync(TransactionModel transaction) =>
            _database.InsertAsync(transaction);

        public Task<List<TransactionModel>> GetTransactionsForEquipmentAsync(string qrCode) =>
            _database.Table<TransactionModel>().Where(t => t.EquipmentQR == qrCode).ToListAsync();

        // ---------- Users ----------
        public Task<UserModel> GetUserByUsernameAsync(string username) =>
            _database.Table<UserModel>().FirstOrDefaultAsync(u => u.Username == username);

        public Task<int> SaveUserAsync(UserModel user) =>
            _database.InsertOrReplaceAsync(user);

        // ---------- Get All Users ----------
        public Task<List<UserModel>> GetUsersAsync() =>
            _database.Table<UserModel>().ToListAsync();

        // ---------- Get Equipment by QR Code ----------
        public Task<EquipmentModel?> GetEquipmentByQRAsync(string qrCode) =>
            _database.Table<EquipmentModel>().FirstOrDefaultAsync(e => e.QRCode == qrCode)!;
    }
}