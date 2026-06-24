; Inno Setup Script - Sistem Monitoring Olahraga
; Generated & customized for MonitoringOlahraga (WinForms, .NET 4.7.2, x64)
; Publisher : TI UMY

#define MyAppName      "Sistem Monitoring Olahraga"
#define MyAppVersion   "1.0"
#define MyAppPublisher "TI UMY"
#define MyAppURL       "https://www.monitoringolahraga.com"
#define MyAppExeName   "MonitoringOlahraga.exe"

; ─────────────────────────────────────────────
;  [Setup] — konfigurasi global installer
; ─────────────────────────────────────────────
[Setup]
; GUID unik untuk aplikasi ini — JANGAN diganti di versi berikutnya
AppId={{B7FA493D-8CE8-405A-8D09-F01F51787551}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}

; Direktori install default: C:\Program Files\Sistem Monitoring Olahraga
DefaultDirName={autopf}\{#MyAppName}

; Ikon yang muncul di daftar uninstall (Control Panel)
UninstallDisplayIcon={app}\{#MyAppExeName}

; Hanya bisa diinstall di mesin 64-bit (sesuai build target x64)
ArchitecturesAllowed=x64compatible
ArchitecturesInstallIn64BitMode=x64compatible

; Sembunyikan pilihan program group (Start Menu group) supaya lebih clean
DisableProgramGroupPage=yes

; Lokasi output file installer .exe hasil compile
OutputDir=D:\Semester 4\PABD\APP-UCP
OutputBaseFilename=MonitoringOlahraga Setup

; Kompresi maksimal — file installer jadi lebih kecil
Compression=lzma2/ultra64
SolidCompression=yes

WizardStyle=modern

; ─────────────────────────────────────────────
;  [Languages]
; ─────────────────────────────────────────────
[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

; ─────────────────────────────────────────────
;  [Tasks] — pilihan opsional saat install
; ─────────────────────────────────────────────
[Tasks]
; Shortcut di desktop — default tidak dicentang (Flags: unchecked)
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; \
  GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

; ─────────────────────────────────────────────
;  [Files] — file yang dikemas ke dalam installer
; ─────────────────────────────────────────────
[Files]
; Salin semua file dari folder Release x64 (exe, dll Crystal Reports,
; ExcelDataReader, LaporanAktivitas.rpt, dan dependensi lainnya)
Source: "D:\Semester 4\PABD\UCP1\MonitoringOlahraga\MonitoringOlahraga\bin\x64\Release\*"; \
  DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs

; ─────────────────────────────────────────────
;  [Icons] — shortcut Start Menu & Desktop
; ─────────────────────────────────────────────
[Icons]
; Shortcut di Start Menu
Name: "{autoprograms}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"

; Shortcut di Desktop (hanya jika user mencentang task "desktopicon")
Name: "{autodesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; \
  Tasks: desktopicon

; ─────────────────────────────────────────────
;  [Run] — jalankan aplikasi setelah install
; ─────────────────────────────────────────────
[Run]
Filename: "{app}\{#MyAppExeName}"; \
  Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; \
  Flags: nowait postinstall skipifsilent
