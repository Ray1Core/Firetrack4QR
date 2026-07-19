# 🔥 Firetrack – QR-Based Inventory & Asset Accountability System

**Firetrack** is a cross‑platform mobile application built with **.NET MAUI** that modernizes equipment tracking for the **Bureau of Fire Protection – Cebu City Station**. It replaces manual logbooks with a digital, QR‑driven system that ensures accountability, transparency, and ease of use for firefighters and logistics officers.

![.NET MAUI](https://img.shields.io/badge/.NET%20MAUI-8.0-blueviolet)  
![Platform](https://img.shields.io/badge/platform-Android%20%7C%20Windows-lightgrey)  
![License](https://img.shields.io/badge/license-MIT-green)

---

## 📖 Table of Contents

- [Features](#-features)
- [Technologies Used](#-technologies-used)
- [Prerequisites](#-prerequisites)
- [Installation & Setup](#-installation--setup)
- [Running the App](#-running-the-app)
- [Configuration](#-configuration)
- [Usage Guide](#-usage-guide)
- [Project Structure](#-project-structure)
- [Contributing](#-contributing)
- [License](#-license)
- [Acknowledgments](#-acknowledgments)

---

## ✨ Features

| Feature | Description |
|---------|-------------|
| **🔐 Login & Role Management** | Secure login with password hashing (BCrypt). Supports two roles: **Admin** and **Personnel**. |
| **📦 Digital Locker (Dashboard)** | Every firefighter sees a personal list of equipment currently assigned to them. |
| **📱 QR Code Generation** | Admin can generate unique QR stickers for each equipment item (disabled in current build – placeholder UI). |
| **📷 QR Code Scanning** | Scan equipment QR codes to instantly retrieve details (disabled in current build – placeholder UI). |
| **🔄 Dual‑Scan Handshake (Transfer)** | Admin scans/selects an officer and an equipment to transfer accountability. Creates an immutable transaction record. |
| **⚠️ Damage Reporting** | Personnel can report damaged equipment by taking/selecting a photo and adding remarks. The equipment status is updated, and a transaction is logged. |
| **📄 Automated ICS PDF Generation** | After a transfer, the system generates a pre‑filled **Inventory Custodian Slip (ICS)** in PDF format, ready for wet‑ink signature. |
| **📋 Clearance Audit** | Admin can view all equipment assigned to any officer and mark items as returned, creating a complete clearance trail. |
| **👥 User Management** | Admin can create new user accounts (Personnel or Admin) directly from the app. |
| **🔒 Role‑Based Access Control** | Sensitive features (Transfer, Manage Users, Clearance, Generate QR) are visible only to Admin. |
| **📊 Local SQLite Database** | All data (users, equipment, transactions) is stored locally with foreign‑key enforcement and indexes for performance. |

---

## 🛠️ Technologies Used

- **.NET MAUI** – Cross‑platform UI framework (.NET 8)
- **SQLite** – Local database via `sqlite-net-pcl`
- **MVVM** – Model‑View‑ViewModel pattern with `INotifyPropertyChanged`
- **BCrypt.Net-Next** – Password hashing
- **PdfSharp.Maui** – PDF generation for ICS forms
- **MediaPicker** – Photo selection for damage reports
- **ZXing.Net.Maui** – QR scanning/generation (disabled for now)
- **CommunityToolkit.Mvvm** – Optional (not used; custom base ViewModel)

---

## 🧰 Prerequisites

- **Visual Studio 2022** (v17.8 or later) with the **.NET MAUI workload** installed.
- **.NET 8 SDK** (included with Visual Studio).
- **Android SDK** (for Android deployment) – or simply run on Windows.
- **Git** (optional, for cloning the repository).

---

## 📥 Installation & Setup

### 1. Clone the repository

```bash
git clone https://github.com/your-username/Firetrack.git
cd Firetrack
2. Open the solution
Open Firetrack.sln in Visual Studio.

3. Restore NuGet packages
All required packages will be restored automatically on build. If not, open Package Manager Console and run:

powershell
dotnet restore
4. Build the solution
Press Ctrl+Shift+B or select Build > Build Solution.

🚀 Running the App
On Windows
Select Windows Machine from the debug target dropdown.

Press F5 to start debugging.

On Android
Open Android Device Manager and create/start an emulator (e.g., Pixel 5, API 33).

Select the emulator from the debug target.

Press F5 to deploy and debug.

Note: The QR scanner and generator are currently disabled. They will show placeholder messages until a stable library is re‑integrated.

⚙️ Configuration
Database
The SQLite database is created automatically at first launch.

File location:
{AppDataDirectory}/firetrack.db3
(e.g., C:\Users\[User]\AppData\Local\Firetrack\firetrack.db3)

Seed Data
The app seeds default users and sample equipment on first run:

Username	Password	Role
admin	admin123	Admin
user	user123	Personnel
Passwords are hashed using BCrypt – they are stored securely even in the prototype.

📖 Usage Guide
Login
Use the default credentials above or create your own via Manage Users (Admin only).

After login, you are redirected to the Dashboard.

Dashboard (Digital Locker)
Shows all equipment currently assigned to you.

Personnel can report damage on any item by clicking the ⚠️ Report button.

Admin has additional buttons:

Generate QR – (disabled) generates QR codes.

Transfer – transfers equipment to another officer.

Manage Users – creates new accounts.

Clearance – audits equipment for retiring officers.

Transferring Equipment (Admin only)
Go to Transfer Equipment.

Select the receiving officer.

Select the equipment (or type its QR code manually).

Click Transfer Equipment.

The equipment status updates to Issued, a transaction is logged, and you are redirected to the ICS generation page.

Click Generate & Download ICS to produce a PDF.

Reporting Damage (Personnel/Admin)
On the Dashboard, find the equipment and click ⚠️ Report.

Fill in the damage description and attach a photo.

Click Submit Report.

The equipment status changes to Damaged, and a transaction is created.

Clearance Audit (Admin only)
Go to Clearance Audit.

Select an officer from the picker.

View all equipment assigned to them.

Select an item and click Mark Selected as Returned.

The equipment becomes Available and a Return transaction is logged.

📂 Project Structure
text
Firetrack/
├── Models/               # Data entities (Equipment, Transaction, User)
├── ViewModels/           # Business logic (Login, Dashboard, Transfer, etc.)
├── Views/                # XAML pages + code-behind
├── Services/             # DatabaseService, PdfGenerationService
├── Converters/           # Value converters for UI (Status → Color, etc.)
├── Platforms/            # Platform-specific files (Android, Windows, etc.)
├── Resources/            # App icons, fonts, styles
├── App.xaml              # Application resources
├── AppShell.xaml         # Shell navigation routes
└── MauiProgram.cs        # App builder and dependency injection
🤝 Contributing
Contributions are welcome! Please follow these steps:

Fork the repository.

Create a feature branch (git checkout -b feature/YourFeature).

Commit your changes (git commit -m 'Add some feature').

Push to the branch (git push origin feature/YourFeature).

Open a Pull Request.

📄 License
Distributed under the MIT License. See LICENSE for more information.

🙏 Acknowledgments
Bureau of Fire Protection – Cebu City Station for providing the use case and requirements.

Asian College of Technology for academic support.

The .NET MAUI community for excellent libraries and tooling.

📧 Contact
Project Link: https://github.com/your-username/Firetrack
Team: MECHIE SASARITA, RAFFY MANZANARES, RAYMUND J. RODRIGUEZ JR., JOSH GABRIEL DELA CUESTA
Adviser: ENRIQUE T. MANAL III

Made with ❤️ for the firefighters of Cebu City.

text

---

## ✅ Next Steps

1. **Create** a new file in your project root called `README.md`.
2. **Copy and paste** the content above into it.
3. **Replace** `your-username` with your actual GitHub username in the repository link.
4. **Commit and push** to GitHub.

The README is now fully populated with all the features, setup instructions, and usage details of your Firetrack project. It will look professional on your GitHub repository.
