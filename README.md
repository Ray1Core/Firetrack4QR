# 🔥 FireTrack – QR‑Based Inventory & Asset Accountability System

[![.NET MAUI](https://img.shields.io/badge/.NET%20MAUI-9.0-blueviolet)](https://dotnet.microsoft.com/apps/maui)
[![SQLite](https://img.shields.io/badge/SQLite-3-blue)](https://www.sqlite.org/)
[![License](https://img.shields.io/badge/License-MIT-green)](LICENSE)
[![Platforms](https://img.shields.io/badge/Platform-Android%20%7C%20Windows-lightgrey)]()

**FireTrack** is a cross‑platform mobile application built with **.NET MAUI** to modernise equipment tracking for the **Bureau of Fire Protection – Cebu City Station**. It replaces manual logbooks with a digital, QR‑driven system that ensures **chain of custody**, **condition‑based disposal**, and **automated clearance** for retiring officers.

---

## 📖 Table of Contents

- [Overview](#-overview)
- [Features](#-features)
- [Technologies](#-technologies)
- [Architecture](#-architecture)
- [Setup & Installation](#-setup--installation)
- [Running the App](#-running-the-app)
- [Screenshots](#-screenshots)
- [Usage Guide](#-usage-guide)
- [Contributing](#-contributing)
- [License](#-license)
- [Acknowledgements](#-acknowledgements)

---

## 📋 Overview

FireTrack was developed as a capstone project for the **Bachelor of Science in Information Technology** at Asian College of Technology – Cebu. It addresses a real‑world challenge at the Cebu City Fire Station: the lack of unique identifiers on common equipment (hoses, nozzles, tools) leads to undocumented transfers, lost items, and financial liability for retiring officers.

The system introduces:

- **QR stickers** on every piece of gear.
- A **dual‑scan handshake** to record official transfers.
- **Photo‑based damage reporting** with automated notifications.
- **PDF generation** of Inventory Custodian Slips (ICS).
- An **admin dashboard** for user management, clearance audits, and reporting.

---

## ✨ Features

### 🔐 Authentication & Roles
- Secure login with **BCrypt password hashing**.
- Two roles: **Admin** (Chief/Supply Officer) and **Personnel** (Fire Officers).
- Role‑based access control – sensitive features are hidden from non‑admin users.

### 📦 Digital Locker (Dashboard)
- Each firefighter sees a personalised list of equipment assigned to them.
- Quick **Report Damage** button on each item.

### 📷 QR Generation & Scanning *(currently disabled – awaiting stable library)*
- Generate unique QR codes for every new equipment.
- Scan QR codes to instantly view equipment details and history.

### 🔄 Dual‑Scan Handshake (Transfer)
- Admin selects a receiving officer and identifies equipment (via picker or manual QR).
- A `Transaction` record is created, and the equipment status updates to `Issued`.
- After transfer, the app automatically opens the **ICS PDF generation** page.

### 📄 Automated ICS PDF Generation
- Generates a professionally formatted Inventory Custodian Slip (BFP‑style).
- Pre‑filled with equipment details, custodian info, issuing officer, and date.
- PDF is saved locally and opened automatically.

### 📸 Damage Reporting
- Personnel can report damaged equipment with **photo evidence**.
- Photos are stored locally and associated with the equipment.
- A `Transaction` of type `ReportDamage` is created.
- Admin receives immediate visual proof.

### 🧹 Clearance Audit (Admin‑only)
- Admin selects a retiring officer and sees all equipment currently assigned to them.
- Each item can be marked as **Returned**, which updates its status and creates a `Return` transaction.
- Simplifies the clearance process and eliminates manual reconciliation.

### 👤 User Management (Admin‑only)
- Admin can create new user accounts (Personnel or Admin).
- Usernames are unique; passwords are hashed before storage.

---

## 🛠️ Technologies

| Component | Technology |
|-----------|------------|
| **Framework** | .NET MAUI 9.0 (cross‑platform) |
| **Language** | C# |
| **Architecture** | MVVM (Model‑View‑ViewModel) |
| **Database** | SQLite (local) – with foreign keys, indexes, unique constraints |
| **PDF Generation** | PdfSharp.Maui |
| **QR Code Support** | ZXing.Net.Maui (currently disabled for stability) |
| **Password Hashing** | BCrypt.Net‑Next |
| **UI** | XAML + custom styles |
| **Version Control** | Git + GitHub |

---

## 🧩 Architecture

FireTrack follows the **MVVM** pattern, ensuring clear separation of concerns:

- **Models** – Data entities (`UserModel`, `EquipmentModel`, `TransactionModel`).
- **ViewModels** – Business logic, commands, and data binding.
- **Views** – XAML pages with minimal code‑behind.

The **DatabaseService** handles all CRUD operations and enforces foreign key constraints. Navigation is managed via **Shell routing**.

---

## 💻 Setup & Installation

### Prerequisites
- [Visual Studio 2022](https://visualstudio.microsoft.com/) with **.NET MAUI** workload installed.
- [Android SDK](https://developer.android.com/studio) (if targeting Android).
- Windows 10/11 (for Windows target).

### Steps

1. **Clone the repository**
   ```bash
   git clone https://github.com/your-username/Firetrack.git
   cd Firetrack
Open the solution

Double‑click Firetrack.sln in Visual Studio.

Restore NuGet packages

Right‑click the solution → Restore NuGet Packages.

Set the startup project

Right‑click Firetrack → Set as Startup Project.

Build the solution

Press Ctrl+Shift+B or go to Build → Build Solution.

🚀 Running the App
Select a target platform (Android emulator or Windows Machine) from the toolbar.

Press F5 to debug or Ctrl+F5 to run without debugging.

Default credentials (seeded automatically):

Admin: admin / admin123

Personnel: user / user123

💡 The app automatically creates these users and sample equipment on first launch.
