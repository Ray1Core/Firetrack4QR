using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firetrack.Models
{
    public class EquipmentModel
    {
        [SQLite.PrimaryKey, SQLite.AutoIncrement]
        public int EquipmentId { get; set; }
        public string QRCode { get; set; } = string.Empty;   // unique QR identifier
        public string Name { get; set; } = string.Empty;     // e.g. "Fire Hose 1"
        public string Type { get; set; } = string.Empty;     // Hose, Nozzle, Tool
        public string Status { get; set; } = "Available";    // Available, Issued, Damaged, InRepair
        public string? AssignedToUsername { get; set; }      // username of current custodian
        public string? PhotoPath { get; set; }               // for damage report
        public string? Remarks { get; set; }
        public DateTime? LastUpdated { get; set; }
    }
}