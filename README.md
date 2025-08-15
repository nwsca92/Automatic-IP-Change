
<img width="705" height="349" alt="Screenshot 2025-08-15 151351" src="https://github.com/user-attachments/assets/7c3d6b3b-e55d-4f4e-b9fc-6452238f5704" />


# 🛠️ Network Configuration Tool (VB.NET)

Aplikasi desktop sederhana berbasis **Windows Forms (VB.NET)** yang memudahkan pengguna mengelola pengaturan jaringan seperti IP Address, Gateway, dan DNS — tanpa perlu masuk ke pengaturan manual atau mengetik di Command Prompt.
Cocok digunakan oleh teknisi IT, administrator jaringan, atau pengguna yang sering berpindah jaringan (kantor, rumah, hotspot).

## 🔧 Fitur Utama

✅ Ganti ke alamat IP statis secara cepat  
✅ Kembali ke DHCP (IP otomatis) dengan satu tombol  
✅ Simpan dan muat pengaturan favorit  
✅ Deteksi status koneksi internet (online/offline) secara real-time  
✅ Tampilkan informasi IPv4, gateway, DNS, dan status jaringan  
✅ StatusStrip informatif dengan:
   - Status online/offline 🌐 / 🔴
   - Tampilan IP saat ini
   - Jam real-time  
✅ Dibangun sebagai **self-contained** — bisa langsung jalan di komputer tanpa instal .NET  
✅ Dijalankan dengan hak administrator (untuk akses perintah `netsh`)  

## ⚙️ Teknologi yang Digunakan

- **Bahasa Pemrograman**: VB.NET
- **Framework**: .NET 8
- **UI**: Windows Forms
- **Library**: `System.Net.NetworkInformation`, `System.Diagnostics`

## 🚀 Cara Menjalankan

### Opsi 1: Langsung Jalankan (.exe)

1. Download folder `publish` dari [Release]((https://github.com/nwsca92/Automatic-IP-Change)
2. Ekstrak ke komputer Windows
3. **Klik kanan `IP-Change.exe`
4. Gunakan sesuai kebutuhan

> 💡 Aplikasi sudah termasuk .NET Runtime (self-contained), jadi tidak perlu instal .NET terlebih dahulu.

### Opsi 2: Jalankan dari Kode Sumber

1. Clone repositori ini:
   ```bash
   git clone https://github.com/nwsca92/Automatic-IP-Change
