using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Baitap01
{
    public static class DatabaseMigration
    {
        public static void Migrate()
        {
            string connectionString = @"Data Source=HUONGLT\SQLEXPRESS;Initial Catalog=QuanLyQuanCaFe;Integrated Security=True";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // 1. Alter HoaDonBan to add LoaiDon if not exists
                    string sqlAlterHoaDonBan = @"
                        IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[HoaDonBan]') AND name = 'LoaiDon')
                        BEGIN
                            ALTER TABLE [dbo].[HoaDonBan] ADD [LoaiDon] NVARCHAR(50) NULL DEFAULT N'Tại quán';
                        END
                    ";
                    ExecuteQuery(conn, sqlAlterHoaDonBan);

                    // 2. Create LoaiNguyenLieu
                    string sqlLoaiNguyenLieu = @"
                        IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LoaiNguyenLieu]') AND type in (N'U'))
                        BEGIN
                            CREATE TABLE [dbo].[LoaiNguyenLieu](
                                [MaLNL] [nvarchar](50) NOT NULL PRIMARY KEY,
                                [TenLNL] [nvarchar](100) NULL
                            )
                        END
                    ";
                    ExecuteQuery(conn, sqlLoaiNguyenLieu);

                    // 3. Create NguyenLieu
                    string sqlNguyenLieu = @"
                        IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[NguyenLieu]') AND type in (N'U'))
                        BEGIN
                            CREATE TABLE [dbo].[NguyenLieu](
                                [MaNguyenLieu] [nvarchar](50) NOT NULL PRIMARY KEY,
                                [TenNguyenLieu] [nvarchar](100) NULL,
                                [MaLNL] [nvarchar](50) NULL,
                                [DonViTinh] [nvarchar](50) NULL,
                                [GiaNhap] [float] NULL,
                                [SoLuongTon] [int] NULL DEFAULT 0,
                                FOREIGN KEY (MaLNL) REFERENCES LoaiNguyenLieu(MaLNL)
                            )
                        END
                        ELSE
                        BEGIN
                            -- Auto-fix if we accidentally created it with MaNL
                            IF EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[NguyenLieu]') AND name = 'MaNL')
                            BEGIN
                                EXEC sp_rename 'dbo.NguyenLieu.MaNL', 'MaNguyenLieu', 'COLUMN';
                                EXEC sp_rename 'dbo.NguyenLieu.TenNL', 'TenNguyenLieu', 'COLUMN';
                            END
                        END
                    ";
                    ExecuteQuery(conn, sqlNguyenLieu);

                    // 4. Create QLTK if not exists
                    string sqlQLTK = @"
                        IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[QLTK]') AND type in (N'U'))
                        BEGIN
                            CREATE TABLE [dbo].[QLTK](
                                [TK] [nvarchar](50) NOT NULL PRIMARY KEY,
                                [MK] [nvarchar](50) NULL,
                                [VT] [int] NULL,
                                [MaNV] [nvarchar](50) NULL
                            )
                            -- Add default admin account
                            INSERT INTO [dbo].[QLTK] (TK, MK, VT, MaNV) VALUES ('admin', 'admin', 1, NULL)
                        END
                    ";
                    ExecuteQuery(conn, sqlQLTK);

                    // 5. Create DiemDanh (Attendance) table
                    string sqlDiemDanh = @"
                        IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DiemDanh]') AND type in (N'U'))
                        BEGIN
                            CREATE TABLE [dbo].[DiemDanh](
                                [ID] INT IDENTITY(1,1) PRIMARY KEY,
                                [Ngay] DATE NOT NULL,
                                [MaNhanVien] VARCHAR(50) NOT NULL,
                                [MaCa] VARCHAR(50) NOT NULL,
                                [TrangThai] NVARCHAR(50) NULL,
                                [GioVao] TIME NULL,
                                [GioRa] TIME NULL,
                                [GhiChu] NVARCHAR(255) NULL
                            )
                        END
                    ";
                    ExecuteQuery(conn, sqlDiemDanh);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi cập nhật cấu trúc Database: " + ex.Message);
            }
        }

        private static void ExecuteQuery(SqlConnection conn, string query)
        {
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.ExecuteNonQuery();
            }
        }
    }
}
