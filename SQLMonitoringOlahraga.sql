-- ============================================================
-- FULL QUERY SQL: Sistem Monitoring Aktivitas Olahraga
-- REVISI FINAL (Sesuai Desain Asli Project)
-- ============================================================

USE master;
GO

-- Hapus database lama jika sudah ada, lalu buat ulang
IF EXISTS (SELECT name FROM sys.databases WHERE name = N'DB_MonitoringOlahraga')
BEGIN
    ALTER DATABASE DB_MonitoringOlahraga SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE DB_MonitoringOlahraga;
END
GO

CREATE DATABASE DB_MonitoringOlahraga;
GO

USE DB_MonitoringOlahraga;
GO

-- ============================================================
-- 1. DDL - CREATE TABLES
-- ============================================================

CREATE TABLE [User] (
    id_user   INT IDENTITY(1,1) PRIMARY KEY,
    nama      VARCHAR(100) NOT NULL,
    email     VARCHAR(100) UNIQUE NOT NULL,
    username  VARCHAR(50)  UNIQUE NOT NULL,
    password  VARCHAR(255) NOT NULL
);
GO

CREATE TABLE AktivitasOlahraga (
    id_aktivitas    INT IDENTITY(1,1) PRIMARY KEY,
    id_user         INT NOT NULL,
    nama_olahraga   VARCHAR(100) NOT NULL,
    kalori_per_menit INT NOT NULL,
    durasi_menit    INT NOT NULL,
    total_kalori    INT NOT NULL, -- Kolom INT biasa (Dihitung di SP)
    tanggal         DATE DEFAULT CAST(GETDATE() AS DATE),
    CONSTRAINT FK_Aktivitas_User FOREIGN KEY (id_user) REFERENCES [User](id_user)
);
GO

CREATE TABLE Laporan (
    id_laporan              INT IDENTITY(1,1) PRIMARY KEY,
    id_user                 INT NOT NULL,
    periode_awal            DATE NOT NULL,
    periode_akhir           DATE NOT NULL,
    total_keseluruhan_kalori BIGINT NOT NULL,
    tanggal_dibuat          DATETIME DEFAULT GETDATE(),
    CONSTRAINT FK_Laporan_User FOREIGN KEY (id_user) REFERENCES [User](id_user)
);
GO

-- ============================================================
-- 2. VIEWS
-- ============================================================

-- View: Riwayat Aktivitas
CREATE VIEW vw_RiwayatAktivitas AS
SELECT
    a.id_aktivitas,
    a.id_user,
    u.nama        AS nama_user,
    a.nama_olahraga,
    a.kalori_per_menit,
    a.durasi_menit,
    a.total_kalori,
    a.tanggal
FROM AktivitasOlahraga a
JOIN [User] u ON a.id_user = u.id_user;
GO

-- View: Data Laporan
CREATE VIEW vw_DataLaporan AS
SELECT
    l.id_laporan,
    l.id_user,
    u.nama              AS nama_user,
    l.periode_awal,
    l.periode_akhir,
    l.total_keseluruhan_kalori,
    l.tanggal_dibuat
FROM Laporan l
JOIN [User] u ON l.id_user = u.id_user;
GO

-- ============================================================
-- 3. STORED PROCEDURES - USER
-- ============================================================

CREATE PROCEDURE sp_GetAllUsers AS
BEGIN
    SELECT id_user, nama, email, username, password FROM [User] ORDER BY nama;
END
GO

CREATE PROCEDURE sp_InsertUser
    @nama     VARCHAR(100),
    @username VARCHAR(50),
    @email    VARCHAR(100),
    @password VARCHAR(255)
AS
BEGIN
    INSERT INTO [User] (nama, email, username, password)
    VALUES (@nama, @email, @username, @password);
END
GO

CREATE PROCEDURE sp_UpdateUser
    @id_user  INT,
    @nama     VARCHAR(100),
    @username VARCHAR(50),
    @email    VARCHAR(100),
    @password VARCHAR(255)
AS
BEGIN
    UPDATE [User]
    SET nama = @nama, email = @email, username = @username, password = @password
    WHERE id_user = @id_user;
END
GO

CREATE PROCEDURE sp_DeleteUser
    @id_user INT
AS
BEGIN
    DELETE FROM [User] WHERE id_user = @id_user;
END
GO

-- (Catatan: Rentan SQL Injection - sengaja untuk keperluan tugas edukasi)
CREATE PROCEDURE sp_LoginUser
    @username VARCHAR(50),
    @password VARCHAR(255)
AS
BEGIN
    DECLARE @sql NVARCHAR(500);
    SET @sql = 'SELECT id_user, nama, username FROM [User] WHERE username = ''' 
               + @username + ''' AND password = ''' + @password + '''';
    EXEC sp_executesql @sql;
END
GO

CREATE PROCEDURE sp_CheckUsername
    @username VARCHAR(50)
AS
BEGIN
    SELECT COUNT(*) FROM [User] WHERE username = @username;
END
GO

CREATE PROCEDURE sp_RegisterUser
    @nama     VARCHAR(100),
    @username VARCHAR(50),
    @email    VARCHAR(100),
    @password VARCHAR(255)
AS
BEGIN
    INSERT INTO [User] (nama, email, username, password)
    VALUES (@nama, @email, @username, @password);
END
GO

-- ============================================================
-- 4. STORED PROCEDURES - AKTIVITAS OLAHRAGA
-- ============================================================

-- SP INSERT (Menghitung Total Kalori secara otomatis)
CREATE PROCEDURE sp_InsertAktivitas
    @id_user         INT,
    @nama_olahraga   VARCHAR(100),
    @kalori_per_menit INT,
    @durasi_menit    INT,
    @tanggal         DATE
AS
BEGIN
    -- Logika asli Anda: Menghitung total kalori otomatis di SP
    DECLARE @total_kalori INT = @kalori_per_menit * @durasi_menit;
    
    INSERT INTO AktivitasOlahraga (id_user, nama_olahraga, kalori_per_menit, durasi_menit, total_kalori, tanggal)
    VALUES (@id_user, @nama_olahraga, @kalori_per_menit, @durasi_menit, @total_kalori, @tanggal);
END
GO

-- SP UPDATE (Menghitung ulang Total Kalori jika ada perubahan)
CREATE PROCEDURE sp_UpdateAktivitas
    @id_aktivitas    INT,
    @nama_olahraga   VARCHAR(100),
    @kalori_per_menit INT,
    @durasi_menit    INT,
    @tanggal         DATE
AS
BEGIN
    DECLARE @total_kalori INT = @kalori_per_menit * @durasi_menit;

    UPDATE AktivitasOlahraga
    SET nama_olahraga   = @nama_olahraga,
        kalori_per_menit = @kalori_per_menit,
        durasi_menit    = @durasi_menit,
        total_kalori    = @total_kalori,
        tanggal         = @tanggal
    WHERE id_aktivitas = @id_aktivitas;
END
GO

CREATE PROCEDURE sp_DeleteAktivitas
    @id_aktivitas INT
AS
BEGIN
    DELETE FROM AktivitasOlahraga WHERE id_aktivitas = @id_aktivitas;
END
GO

CREATE PROCEDURE sp_SearchAktivitas
    @keyword VARCHAR(100)
AS
BEGIN
    SELECT
        a.id_aktivitas,
        a.id_user,
        u.nama        AS nama_user,
        a.nama_olahraga,
        a.kalori_per_menit,
        a.durasi_menit,
        a.total_kalori,
        a.tanggal
    FROM AktivitasOlahraga a
    JOIN [User] u ON a.id_user = u.id_user
    WHERE a.nama_olahraga LIKE '%' + @keyword + '%'
    ORDER BY a.tanggal DESC;
END
GO

-- ============================================================
-- 5. TRIGGER (UNTUK MEMENUHI SYARAT TUGAS)
-- ============================================================

-- Trigger Validasi: Mencegah user memasukkan durasi atau kalori bernilai 0 atau minus
CREATE TRIGGER trg_CekValiditasOlahraga
ON AktivitasOlahraga
FOR INSERT, UPDATE
AS
BEGIN
    IF EXISTS (SELECT 1 FROM inserted WHERE durasi_menit <= 0 OR kalori_per_menit <= 0)
    BEGIN
        RAISERROR ('TRIGGER ERROR: Durasi menit dan kalori per menit harus lebih besar dari 0!', 16, 1);
        ROLLBACK TRANSACTION;
    END
END
GO

-- ============================================================
-- 6. DML - DATA SAMPLE (Sesuai dengan data Anda sebelumnya)
-- ============================================================

INSERT INTO [User] (nama, email, username, password) VALUES
('Arby Pangestu',  'arby@email.com',   'arby',  'Arby123'),
('Meilan Ulfia',   'meilan@email.com', 'meilan', '12345');
GO

-- Memanggil SP Insert agar total_kalori terhitung otomatis!
EXEC sp_InsertAktivitas 1, 'Lari Pagi', 10, 30, '2026-04-10';
EXEC sp_InsertAktivitas 2, 'Yoga',       4, 20, '2026-04-12';
EXEC sp_InsertAktivitas 1, 'Voli',     100, 10, '2026-06-12';
EXEC sp_InsertAktivitas 1, 'basket',  2000, 30, '2026-06-25';
EXEC sp_InsertAktivitas 1, 'tangkis', 1000, 33, '2026-06-25';
EXEC sp_InsertAktivitas 1, 'Lari',      10, 30, '2026-06-25';
EXEC sp_InsertAktivitas 2, 'Berenang',  12, 45, '2026-06-25';
GO


