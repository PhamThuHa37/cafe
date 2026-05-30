-- Tạo cơ sở dữ liệu
CREATE DATABASE QuanLyQuanCaFe;
GO

USE QuanLyQuanCaFe;
GO

-- 1. Bảng ChucVu
CREATE TABLE ChucVu (
    MaChucVu VARCHAR(10) PRIMARY KEY,
    TenChucVu NVARCHAR(50) NOT NULL
);

-- 2. Bảng CaLam
CREATE TABLE CaLam (
    MaCa VARCHAR(10) PRIMARY KEY,
    TenCa NVARCHAR(50) NOT NULL,
    GioBatDau TIME NOT NULL,
    GioKetThuc TIME NOT NULL
);

-- 3. Bảng BangLuong
CREATE TABLE BangLuong (
    MaBangLuong VARCHAR(10) PRIMARY KEY,
    TenBangLuong NVARCHAR(50) NOT NULL,
    LuongCoBan DECIMAL(18, 2) NOT NULL,
    PhuCap DECIMAL(18, 2) DEFAULT 0,
    Thuong DECIMAL(18, 2) DEFAULT 0
);

-- 4. Bảng NhanVien
CREATE TABLE NhanVien (
    MaNhanVien VARCHAR(10) PRIMARY KEY,
    TenNhanVien NVARCHAR(100) NOT NULL,
    DiaChi NVARCHAR(200),
    NgaySinh DATE,
    SoDienthoai VARCHAR(15),
    MaChucVu VARCHAR(10),
    MaBangLuong VARCHAR(10),
    FOREIGN KEY (MaChucVu) REFERENCES ChucVu(MaChucVu),
    FOREIGN KEY (MaBangLuong) REFERENCES BangLuong(MaBangLuong)
);

-- 5. Bảng PhanCa
CREATE TABLE PhanCa (
    MaNhanVien VARCHAR(10),
    MaCa VARCHAR(10),
    PRIMARY KEY (MaNhanVien, MaCa),
    FOREIGN KEY (MaNhanVien) REFERENCES NhanVien(MaNhanVien),
    FOREIGN KEY (MaCa) REFERENCES CaLam(MaCa)
);

-- 6. Bảng KhachHang
CREATE TABLE KhachHang (
    MaKhachHang VARCHAR(10) PRIMARY KEY,
    TenKhachHang NVARCHAR(100) NOT NULL,
    SoDienThoai VARCHAR(15),
    DiemTichLuy INT DEFAULT 0
);

-- 7. Bảng NhaCungCap
CREATE TABLE NhaCungCap (
    MaNhaCungCap VARCHAR(10) PRIMARY KEY,
    TenNhaCungCap NVARCHAR(100) NOT NULL
);

-- 8. Bảng NguyenLieu
CREATE TABLE NguyenLieu (
    MaNguyenLieu VARCHAR(10) PRIMARY KEY,
    TenNguyenLieu NVARCHAR(100) NOT NULL,
    SoLuongHienCo DECIMAL(10, 2) DEFAULT 0,
    DonViTinh NVARCHAR(20),
    HanSuDung DATE,
    GiaNhap DECIMAL(18, 2) NOT NULL
);

-- 9. Bảng HoaDonNhap
CREATE TABLE HoaDonNhap (
    MaHoaDonNhap VARCHAR(10) PRIMARY KEY,
    NgayNhap VARCHAR(50),
    TongTienNhap DECIMAL(18, 2) DEFAULT 0,
    MaNhanVien VARCHAR(10),
    MaNhaCungCap VARCHAR(10),
    FOREIGN KEY (MaNhanVien) REFERENCES NhanVien(MaNhanVien),
    FOREIGN KEY (MaNhaCungCap) REFERENCES NhaCungCap(MaNhaCungCap)
);

-- 10. Bảng ChiTietHoaDonNhap
CREATE TABLE ChiTietHoaDonNhap (
    MaHoaDonNhap VARCHAR(10),
    MaNguyenLieu VARCHAR(10),
    SoLuong DECIMAL(10, 2) NOT NULL,
    DonGia DECIMAL(18, 2) NOT NULL,
    ThanhTien DECIMAL(18, 2) NOT NULL,
    PRIMARY KEY (MaHoaDonNhap, MaNguyenLieu),
    FOREIGN KEY (MaHoaDonNhap) REFERENCES HoaDonNhap(MaHoaDonNhap),
    FOREIGN KEY (MaNguyenLieu) REFERENCES NguyenLieu(MaNguyenLieu)
);

-- 11. Bảng LoaiSanPham
CREATE TABLE LoaiSanPham (
    MaLoaiSanPham VARCHAR(10) PRIMARY KEY,
    TenLoaiSanPham NVARCHAR(50) NOT NULL
);

-- 12. Bảng CongThuc
CREATE TABLE CongThuc (
    MaCongThuc VARCHAR(10) PRIMARY KEY,
    TenCongThuc NVARCHAR(100) NOT NULL,
    CacBuocThucHien NVARCHAR(MAX)
);

-- 13. Bảng SanPham
CREATE TABLE SanPham (
    MaSanPham VARCHAR(10) PRIMARY KEY,
    TenSanPham NVARCHAR(100) NOT NULL,
    GiaBan DECIMAL(18, 2) NOT NULL,
    HinhAnh VARCHAR(255),
    MaLoaiSanPham VARCHAR(10),
    MaCongThuc VARCHAR(10),
    FOREIGN KEY (MaLoaiSanPham) REFERENCES LoaiSanPham(MaLoaiSanPham),
    FOREIGN KEY (MaCongThuc) REFERENCES CongThuc(MaCongThuc)
);

-- 14. Bảng ChiTietCongThuc
CREATE TABLE ChiTietCongThuc (
    MaCongThuc VARCHAR(10),
    MaNguyenLieu VARCHAR(10),
    DinhLuong DECIMAL(10, 2) NOT NULL,
    PRIMARY KEY (MaCongThuc, MaNguyenLieu),
    FOREIGN KEY (MaCongThuc) REFERENCES CongThuc(MaCongThuc),
    FOREIGN KEY (MaNguyenLieu) REFERENCES NguyenLieu(MaNguyenLieu)
);

-- 15. Bảng KhuyenMai
CREATE TABLE KhuyenMai (
    MaKhuyenMai VARCHAR(10) PRIMARY KEY,
    DieuKienApDung NVARCHAR(200),
    ThoiGianBatDau DATETIME,
    ThoiGianKetThuc DATETIME,
    MucKhuyenMai DECIMAL(18, 2) NOT NULL
);

-- 16. Bảng Ban
CREATE TABLE Ban (
    MaBan VARCHAR(10) PRIMARY KEY,
    SoLuong INT DEFAULT 4, -- Số chỗ ngồi tối đa của bàn
    TinhTrang NVARCHAR(30) -- Ví dụ: Trống, Có khách, Đã đặt
);

-- 17. Bảng HoaDonBan
CREATE TABLE HoaDonBan (
    MaHoaDonBan VARCHAR(10) PRIMARY KEY,
    NgayLap VARCHAR(50),
    TrangThai NVARCHAR(50), -- Ví dụ: Đã thanh toán, Chưa thanh toán
    TongTienBan DECIMAL(18, 2) DEFAULT 0,
    MaNhanVien VARCHAR(10),
    MaKhachHang VARCHAR(10),
    MaKhuyenMai VARCHAR(10),
    MaBan VARCHAR(10),
    FOREIGN KEY (MaNhanVien) REFERENCES NhanVien(MaNhanVien),
    FOREIGN KEY (MaKhachHang) REFERENCES KhachHang(MaKhachHang),
    FOREIGN KEY (MaKhuyenMai) REFERENCES KhuyenMai(MaKhuyenMai),
    FOREIGN KEY (MaBan) REFERENCES Ban(MaBan)
);

-- 18. Bảng ChiTietHoaDonBan
CREATE TABLE ChiTietHoaDonBan (
    MaHoaDonBan VARCHAR(10),
    MaSanPham VARCHAR(10),
    SoLuong INT NOT NULL,
    DonGia DECIMAL(18, 2) NOT NULL,
    ThanhTien DECIMAL(18, 2) NOT NULL,
    GhiChu NVARCHAR(200),
    PRIMARY KEY (MaHoaDonBan, MaSanPham),
    FOREIGN KEY (MaHoaDonBan) REFERENCES HoaDonBan(MaHoaDonBan),
    FOREIGN KEY (MaSanPham) REFERENCES SanPham(MaSanPham)
);

-- 1. Insert ChucVu
INSERT INTO ChucVu VALUES ('CV01', N'Quản lý');
INSERT INTO ChucVu VALUES ('CV02', N'Pha chế trưởng');
INSERT INTO ChucVu VALUES ('CV03', N'Nhân viên pha chế');
INSERT INTO ChucVu VALUES ('CV04', N'Nhân viên phục vụ');
INSERT INTO ChucVu VALUES ('CV05', N'Nhân viên thu ngân');
INSERT INTO ChucVu VALUES ('CV06', N'Nhân viên bảo vệ');
INSERT INTO ChucVu VALUES ('CV07', N'Nhân viên vệ sinh');
INSERT INTO ChucVu VALUES ('CV08', N'Kế toán');
INSERT INTO ChucVu VALUES ('CV09', N'Giám sát ca');
INSERT INTO ChucVu VALUES ('CV10', N'Nhân viên Marketing');

-- 2. Insert CaLam
INSERT INTO CaLam VALUES ('CA01', N'Ca Sáng', '06:00:00', '12:00:00');
INSERT INTO CaLam VALUES ('CA02', N'Ca Chiều', '12:00:00', '18:00:00');
INSERT INTO CaLam VALUES ('CA03', N'Ca Tối', '18:00:00', '23:00:00');
INSERT INTO CaLam VALUES ('CA04', N'Ca Đêm', '23:00:00', '06:00:00');
INSERT INTO CaLam VALUES ('CA05', N'Ca Hành Chính', '08:00:00', '17:00:00');
INSERT INTO CaLam VALUES ('CA06', N'Ca Gãy 1', '09:00:00', '13:00:00');
INSERT INTO CaLam VALUES ('CA07', N'Ca Gãy 2', '17:00:00', '21:00:00');
INSERT INTO CaLam VALUES ('CA08', N'Ca Cuối Tuần Sáng', '06:00:00', '12:00:00');
INSERT INTO CaLam VALUES ('CA09', N'Ca Cuối Tuần Tối', '18:00:00', '23:00:00');
INSERT INTO CaLam VALUES ('CA10', N'Ca Hỗ Trợ Tiệc', '15:00:00', '22:00:00');

-- 3. Insert BangLuong
INSERT INTO BangLuong VALUES ('BL01', N'Lương Quản Lý', 15000000, 2000000, 1000000);
INSERT INTO BangLuong VALUES ('BL02', N'Lương Pha Chế Trưởng', 10000000, 1000000, 500000);
INSERT INTO BangLuong VALUES ('BL03', N'Lương Pha Chế', 6500000, 500000, 300000);
INSERT INTO BangLuong VALUES ('BL04', N'Lương Phục Vụ', 5000000, 300000, 200000);
INSERT INTO BangLuong VALUES ('BL05', N'Lương Thu Ngân', 6000000, 400000, 200000);
INSERT INTO BangLuong VALUES ('BL06', N'Lương Bảo Vệ', 5500000, 200000, 100000);
INSERT INTO BangLuong VALUES ('BL07', N'Lương Tạp Vụ', 4800000, 200000, 100000);
INSERT INTO BangLuong VALUES ('BL08', N'Lương Kế Toán', 8500000, 800000, 400000);
INSERT INTO BangLuong VALUES ('BL09', N'Lương Giám Sát', 9000000, 1200000, 600000);
INSERT INTO BangLuong VALUES ('BL10', N'Lương Part-time', 3000000, 100000, 50000);

-- 4. Insert NhanVien
INSERT INTO NhanVien VALUES ('NV01', N'Nguyễn Văn A', N'Hà Nội', '1990-05-12', '0912345678', 'CV01', 'BL01');
INSERT INTO NhanVien VALUES ('NV02', N'Trần Thị B', N'Đà Nẵng', '1993-08-22', '0923456789', 'CV02', 'BL02');
INSERT INTO NhanVien VALUES ('NV03', N'Lê Văn C', N'TP HCM', '1995-11-02', '0934567890', 'CV03', 'BL03');
INSERT INTO NhanVien VALUES ('NV04', N'Phạm Minh D', N'Hải Phòng', '1998-02-15', '0945678901', 'CV04', 'BL04');
INSERT INTO NhanVien VALUES ('NV05', N'Hoàng Thị E', N'Cần Thơ', '1997-07-19', '0956789012', 'CV05', 'BL05');
INSERT INTO NhanVien VALUES ('NV06', N'Vũ Văn F', N'Nghệ An', '1988-04-30', '0967890123', 'CV06', 'BL06');
INSERT INTO NhanVien VALUES ('NV07', N'Đặng Thị G', N'Hà Tĩnh', '1994-12-05', '0978901234', 'CV07', 'BL07');
INSERT INTO NhanVien VALUES ('NV08', N'Bùi Minh H', N'Quảng Ninh', '1992-01-25', '0989012345', 'CV08', 'BL08');
INSERT INTO NhanVien VALUES ('NV09', N'Đỗ Văn I', N'Thanh Hóa', '1991-09-14', '0990123456', 'CV09', 'BL09');
INSERT INTO NhanVien VALUES ('NV10', N'Ngô Thị K', N'Bình Dương', '1999-06-01', '0901234567', 'CV04', 'BL10');

-- 5. Insert PhanCa
INSERT INTO PhanCa VALUES ('NV01', 'CA05');
INSERT INTO PhanCa VALUES ('NV02', 'CA01');
INSERT INTO PhanCa VALUES ('NV03', 'CA01');
INSERT INTO PhanCa VALUES ('NV04', 'CA02');
INSERT INTO PhanCa VALUES ('NV05', 'CA02');
INSERT INTO PhanCa VALUES ('NV06', 'CA03');
INSERT INTO PhanCa VALUES ('NV07', 'CA04');
INSERT INTO PhanCa VALUES ('NV08', 'CA05');
INSERT INTO PhanCa VALUES ('NV09', 'CA03');
INSERT INTO PhanCa VALUES ('NV10', 'CA06');

-- 6. Insert KhachHang
INSERT INTO KhachHang VALUES ('KH01', N'Nguyễn Hoàng Long', '0905111222', 120);
INSERT INTO KhachHang VALUES ('KH02', N'Lê Minh Tú', '0905222333', 50);
INSERT INTO KhachHang VALUES ('KH03', N'Trần Thu Hà', '0905333444', 340);
INSERT INTO KhachHang VALUES ('KH04', N'Phạm Quốc Bảo', '0905444555', 15);
INSERT INTO KhachHang VALUES ('KH05', N'Vũ Thùy Linh', '0905555666', 85);
INSERT INTO KhachHang VALUES ('KH06', N'Hoàng Anh Tuấn', '0905666777', 0);
INSERT INTO KhachHang VALUES ('KH07', N'Đỗ Hải Yến', '0905777888', 500);
INSERT INTO KhachHang VALUES ('KH08', N'Ngô Tiến Đạt', '0905888999', 210);
INSERT INTO KhachHang VALUES ('KH09', N'Bùi Phương Thảo', '0905999000', 95);
INSERT INTO KhachHang VALUES ('KH10', N'Lý Văn Thắng', '0905000111', 11);

-- 7. Insert NhaCungCap
INSERT INTO NhaCungCap VALUES ('NCC01', N'Công ty Cà phê Trung Nguyên');
INSERT INTO NhaCungCap VALUES ('NCC02', N'Đại lý Sữa Vinamilk Chi nhánh 1');
INSERT INTO NhaCungCap VALUES ('NCC03', N'Nhà cung cấp Đường Biên Hòa');
INSERT INTO NhaCungCap VALUES ('NCC04', N'Công ty Nước đá Tinh Khiết');
INSERT INTO NhaCungCap VALUES ('NCC05', N'Vựa Trái Cây Sạch Chợ Đầu Mối');
INSERT INTO NhaCungCap VALUES ('NCC06', N'Tổng kho Nguyên liệu Pha chế TeaPlus');
INSERT INTO NhaCungCap VALUES ('NCC07', N'Công ty Bao bì & Ly nhựa xanh');
INSERT INTO NhaCungCap VALUES ('NCC08', N'Nông trại Trà xanh Bảo Lộc');
INSERT INTO NhaCungCap VALUES ('NCC09', N'Đại lý Thực phẩm Hùng Cường');
INSERT INTO NhaCungCap VALUES ('NCC10', N'Công ty CP Trứng gia cầm Ba Huân');

-- 8. Insert NguyenLieu
INSERT INTO NguyenLieu VALUES ('NL01', N'Hạt Cà phê Robusta', 50.0, 'kg', '2027-12-31', 120000);
INSERT INTO NguyenLieu VALUES ('NL02', N'Sữa đặc Ngôi Sao Phương Nam', 100.0, 'hộp', '2027-06-30', 18000);
INSERT INTO NguyenLieu VALUES ('NL03', N'Sữa tươi tiệt trùng Vinamilk', 60.0, 'lít', '2026-08-15', 25000);
INSERT INTO NguyenLieu VALUES ('NL04', N'Đường cát trắng', 30.0, 'kg', '2028-01-01', 20000);
INSERT INTO NguyenLieu VALUES ('NL05', N'Bột Matcha Nhật Bản', 5.0, 'kg', '2027-03-20', 450000);
INSERT INTO NguyenLieu VALUES ('NL06', N'Siro Sô-cô-la Hershey', 12.0, 'chai', '2027-09-10', 85000);
INSERT INTO NguyenLieu VALUES ('NL07', N'Trà đen Lộc Phát', 15.0, 'kg', '2028-05-14', 150000);
INSERT INTO NguyenLieu VALUES ('NL08', N'Chanh tươi', 10.0, 'kg', '2026-06-10', 25000);
INSERT INTO NguyenLieu VALUES ('NL09', N'Kem béo thực vật Richs', 24.0, 'hộp', '2026-11-20', 32000);
INSERT INTO NguyenLieu VALUES ('NL10', N'Bột ca cao nguyên chất', 8.0, 'kg', '2027-08-18', 180000);

-- 9. Insert HoaDonNhap
INSERT INTO HoaDonNhap VALUES ('HDN01', '28/5/2026', 2400000, 'NV01', 'NCC01');
INSERT INTO HoaDonNhap VALUES ('HDN02', '28/5/2026', 1800000, 'NV01', 'NCC02');
INSERT INTO HoaDonNhap VALUES ('HDN03', '28/5/2026', 600000, 'NV08', 'NCC03');
INSERT INTO HoaDonNhap VALUES ('HDN04', '28/5/2026', 1500000, 'NV08', 'NCC03');
INSERT INTO HoaDonNhap VALUES ('HDN05', '28/5/2026', 2250000, 'NV01', 'NCC05');
INSERT INTO HoaDonNhap VALUES ('HDN06', '28/5/2026', 1020000, 'NV08', 'NCC06');
INSERT INTO HoaDonNhap VALUES ('HDN07', '28/5/2026', 1500000, 'NV01', 'NCC07');
INSERT INTO HoaDonNhap VALUES ('HDN08', '28/5/2026', 768000, 'NV08', 'NCC02');
INSERT INTO HoaDonNhap VALUES ('HDN09', '28/5/2026', 1440000, 'NV01', 'NCC06');
INSERT INTO HoaDonNhap VALUES ('HDN10', '28/5/2026', 1200000, 'NV08', 'NCC01');

-- 10. Insert ChiTietHoaDonNhap
INSERT INTO ChiTietHoaDonNhap VALUES ('HDN01', 'NL01', 20, 120000, 2400000);
INSERT INTO ChiTietHoaDonNhap VALUES ('HDN02', 'NL02', 100, 18000, 1800000);
INSERT INTO ChiTietHoaDonNhap VALUES ('HDN03', 'NL04', 30, 20000, 600000);
INSERT INTO ChiTietHoaDonNhap VALUES ('HDN04', 'NL03', 60, 25000, 1500000);
INSERT INTO ChiTietHoaDonNhap VALUES ('HDN05', 'NL05', 5, 450000, 2250000);
INSERT INTO ChiTietHoaDonNhap VALUES ('HDN06', 'NL06', 12, 85000, 1020000);
INSERT INTO ChiTietHoaDonNhap VALUES ('HDN07', 'NL07', 10, 150000, 1500000);
INSERT INTO ChiTietHoaDonNhap VALUES ('HDN08', 'NL09', 24, 32000, 768000);
INSERT INTO ChiTietHoaDonNhap VALUES ('HDN09', 'NL10', 8, 180000, 1440000);
INSERT INTO ChiTietHoaDonNhap VALUES ('HDN10', 'NL01', 10, 120000, 1200000);

-- 11. Insert LoaiSanPham
INSERT INTO LoaiSanPham VALUES ('LSP01', N'Cà phê truyền thống');
INSERT INTO LoaiSanPham VALUES ('LSP02', N'Cà phê Ý (Máy)');
INSERT INTO LoaiSanPham VALUES ('LSP03', N'Trà trái cây');
INSERT INTO LoaiSanPham VALUES ('LSP04', N'Trà sữa');
INSERT INTO LoaiSanPham VALUES ('LSP05', N'Đá xay (Ice Blended)');
INSERT INTO LoaiSanPham VALUES ('LSP06', N'Sinh tố (Smoothies)');
INSERT INTO LoaiSanPham VALUES ('LSP07', N'Nước ép tươi');
INSERT INTO LoaiSanPham VALUES ('LSP08', N'Sô-cô-la / Ca cao');
INSERT INTO LoaiSanPham VALUES ('LSP10', N'Đồ ăn vặt nhanh');

-- 12. Insert CongThuc
INSERT INTO CongThuc VALUES ('CT01', N'Công thức Cà phê Đen', N'B1: Cho 25g bột cafe vào phin. B2: Ủ 30ml nước sôi. B3: Rót thêm 50ml nước sôi thu cốt.');
INSERT INTO CongThuc VALUES ('CT02', N'Công thức Cà phê Sữa', N'B1: Rót 25ml sữa đặc vào ly. B2: Chiết xuất 50ml cafe đen phin đổ lên. B3: Khuấy đều thêm đá.');
INSERT INTO CongThuc VALUES ('CT03', N'Công thức Bạc Xỉu', N'B1: 30ml sữa đặc + 40ml sữa tươi lắc đều với đá. B2: Tạo bọt 20ml cafe đen rót nhẹ lên bề mặt.');
INSERT INTO CongThuc VALUES ('CT04', N'Công thức Matcha Đá Xay', N'B1: Cho 5g matcha, 40ml sữa tươi, 20ml kem béo, đá viên vào máy. B2: Xay nhuyễn. B3: Xịt kem tươi.');
INSERT INTO CongThuc VALUES ('CT05', N'Công thức Trà Đào Cam Sả', N'B1: Hãm 80ml trà đen với sả. B2: Thêm 20ml siro đào, 10ml nước cốt chanh. B3: Thêm đá và 2 lát đào miếng.');
INSERT INTO CongThuc VALUES ('CT06', N'Công thức Latte', N'B1: Chiết xuất 1 shot Espresso (30ml). B2: Đánh bọt sữa tươi 150ml tạo lớp foam mỏng. B3: Rót sữa tạo hình.');
INSERT INTO CongThuc VALUES ('CT07', N'Công thức Trà Sữa Trân Châu', N'B1: Ủ 100ml trà đen. B2: Hòa 25g bột sữa và 20ml đường. B3: Thêm trân châu và đá.');
INSERT INTO CongThuc VALUES ('CT08', N'Công thức Ca Cao Nóng', N'B1: Hòa 10g bột ca cao với 30ml nước sôi. B2: Đánh nóng 120ml sữa tươi và sữa đặc. B3: Đổ chung khuấy đều.');
INSERT INTO CongThuc VALUES ('CT09', N'Công thức Nước Ép Chanh', N'B1: Vắt cốt 2 quả chanh (khoảng 20ml). B2: Hòa với 30ml nước đường và 50ml nước lọc. B3: Thêm đá.');


-- 13. Insert SanPham
INSERT INTO SanPham VALUES ('SP01', N'Cà phê đen đá', 25000, 'capheden.jpg', 'LSP01', 'CT01');
INSERT INTO SanPham VALUES ('SP02', N'Cà phê sữa đá', 29000, 'caphesua.jpg', 'LSP01', 'CT02');
INSERT INTO SanPham VALUES ('SP03', N'Bạc xỉu', 32000, 'bacxiu.jpg', 'LSP01', 'CT03');
INSERT INTO SanPham VALUES ('SP04', N'Matcha đá xay', 45000, 'matchadaxay.jpg', 'LSP05', 'CT04');
INSERT INTO SanPham VALUES ('SP05', N'Trà đào cam sả', 39000, 'tradaocamsa.jpg', 'LSP03', 'CT05');
INSERT INTO SanPham VALUES ('SP06', N'Café Latte', 40000, 'latte.jpg', 'LSP02', 'CT06');
INSERT INTO SanPham VALUES ('SP07', N'Trà sữa truyền thống', 35000, 'trasua.jpg', 'LSP04', 'CT07');
INSERT INTO SanPham VALUES ('SP08', N'Ca cao nóng', 35000, 'cacaonong.jpg', 'LSP08', 'CT08');
INSERT INTO SanPham VALUES ('SP09', N'Nước chanh đá', 25000, 'nuocchanh.jpg', 'LSP07', 'CT09');
INSERT INTO SanPham VALUES ('SP10', N'Bánh mì thịt', 35000, 'banhmi.jpg', 'LSP10', NULL);


-- 14. Insert ChiTietCongThuc
INSERT INTO ChiTietCongThuc VALUES ('CT01', 'NL01', 0.025);
INSERT INTO ChiTietCongThuc VALUES ('CT02', 'NL01', 0.025);
INSERT INTO ChiTietCongThuc VALUES ('CT02', 'NL02', 1.000);
INSERT INTO ChiTietCongThuc VALUES ('CT03', 'NL02', 1.000);
INSERT INTO ChiTietCongThuc VALUES ('CT03', 'NL03', 0.040);
INSERT INTO ChiTietCongThuc VALUES ('CT04', 'NL05', 0.005);
INSERT INTO ChiTietCongThuc VALUES ('CT04', 'NL03', 0.040);
INSERT INTO ChiTietCongThuc VALUES ('CT05', 'NL07', 0.010);
INSERT INTO ChiTietCongThuc VALUES ('CT08', 'NL10', 0.010);
INSERT INTO ChiTietCongThuc VALUES ('CT09', 'NL08', 0.100);

-- 15. Insert KhuyenMai
INSERT INTO KhuyenMai VALUES ('KM00', N'Không áp dụng', '2026-01-01', '2030-12-31', 0);
INSERT INTO KhuyenMai VALUES ('KM01', N'Khai trương chi nhánh mới giảm 20%', '2026-05-01', '2026-05-07', 20000);
INSERT INTO KhuyenMai VALUES ('KM02', N'Chào hè rực rỡ giảm 10k cho đơn từ 50k', '2026-06-01', '2026-06-30', 10000);
INSERT INTO KhuyenMai VALUES ('KM03', N'Quốc tế thiếu nhi đồng giá', '2026-06-01', '2026-06-02', 15000);
INSERT INTO KhuyenMai VALUES ('KM04', N'Tri ân khách hàng VIP', '2026-01-01', '2026-12-31', 50000);
INSERT INTO KhuyenMai VALUES ('KM05', N'Giảm giá mùa dịch Giáng Sinh', '2026-12-20', '2026-12-26', 25000);
INSERT INTO KhuyenMai VALUES ('KM06', N'Khuyến mãi Trung Thu', '2026-09-10', '2026-09-18', 30000);
INSERT INTO KhuyenMai VALUES ('KM07', N'Giảm giá ngày lễ Vu Lan', '2026-08-15', '2026-08-16', 15000);
INSERT INTO KhuyenMai VALUES ('KM08', N'Happy Hour khung giờ vàng 14h-16h', '2026-01-01', '2026-12-31', 12000);
INSERT INTO KhuyenMai VALUES ('KM09', N'Mã giảm giá từ App đối tác', '2026-03-01', '2026-07-01', 18000);

-- 16. Insert Ban
INSERT INTO Ban VALUES ('B01', 2, N'Trống');
INSERT INTO Ban VALUES ('B02', 4, N'Có khách');
INSERT INTO Ban VALUES ('B03', 4, N'Trống');
INSERT INTO Ban VALUES ('B04', 6, N'Đã đặt');
INSERT INTO Ban VALUES ('B05', 2, N'Có khách');
INSERT INTO Ban VALUES ('B06', 4, N'Trống');
INSERT INTO Ban VALUES ('B07', 8, N'Trống');
INSERT INTO Ban VALUES ('B08', 4, N'Có khách');
INSERT INTO Ban VALUES ('B09', 2, N'Trống');
INSERT INTO Ban VALUES ('B10', 6, N'Có khách');

-- 17. Insert HoaDonBan
INSERT INTO HoaDonBan VALUES ('HDB01', '28/5/2026', N'Đã thanh toán', 50000, 'NV05', 'KH01', 'KM00', 'B01');
INSERT INTO HoaDonBan VALUES ('HDB02', '28/5/2026', N'Chưa thanh toán', 58000, 'NV05', 'KH02', 'KM00', 'B02');
INSERT INTO HoaDonBan VALUES ('HDB03', '28/5/2026', N'Đã thanh toán', 64000, 'NV10', 'KH03', 'KM00', 'B05');
INSERT INTO HoaDonBan VALUES ('HDB04', '28/5/2026', N'Đã thanh toán', 35000, 'NV05', 'KH04', 'KM02', 'B08');
INSERT INTO HoaDonBan VALUES ('HDB05', '28/5/2026', N'Chưa thanh toán', 120000, 'NV10', 'KH05', 'KM00', 'B10');
INSERT INTO HoaDonBan VALUES ('HDB06', '28/5/2026', N'Đã thanh toán', 40000, 'NV05', 'KH06', 'KM00', 'B03');
INSERT INTO HoaDonBan VALUES ('HDB07', '28/5/2026', N'Đã thanh toán', 70000, 'NV05', 'KH07', 'KM00', 'B06');
INSERT INTO HoaDonBan VALUES ('HDB08', '28/5/2026', N'Đã thanh toán', 25000, 'NV10', 'KH08', 'KM00', 'B09');
INSERT INTO HoaDonBan VALUES ('HDB09', '28/5/2026', N'Đã thanh toán', 90000, 'NV05', 'KH09', 'KM00', 'B02');
INSERT INTO HoaDonBan VALUES ('HDB10', '28/5/2026', N'Đã thanh toán', 45000, 'NV10', 'KH10', 'KM00', 'B01');

-- 18. Insert ChiTietHoaDonBan
INSERT INTO ChiTietHoaDonBan VALUES ('HDB01', 'SP01', 2, 25000, 50000, N'Ít đá');
INSERT INTO ChiTietHoaDonBan VALUES ('HDB02', 'SP02', 2, 29000, 58000, N'Nhiều sữa');
INSERT INTO ChiTietHoaDonBan VALUES ('HDB03', 'SP03', 2, 32000, 64000, N'Không đường');
INSERT INTO ChiTietHoaDonBan VALUES ('HDB04', 'SP04', 1, 45000, 45000, N'Nhiều kem béo');
INSERT INTO ChiTietHoaDonBan VALUES ('HDB05', 'SP05', 3, 39000, 117000, N'Thêm đào miếng');
INSERT INTO ChiTietHoaDonBan VALUES ('HDB06', 'SP06', 1, 40000, 40000, N'Nóng');
INSERT INTO ChiTietHoaDonBan VALUES ('HDB07', 'SP07', 2, 35000, 70000, N'Trân châu đen');
INSERT INTO ChiTietHoaDonBan VALUES ('HDB08', 'SP09', 1, 25000, 25000, N'Mang về');
INSERT INTO ChiTietHoaDonBan VALUES ('HDB09', 'SP10', 2, 45000, 90000, N'Ăn tại chỗ');
INSERT INTO ChiTietHoaDonBan VALUES ('HDB10', 'SP08', 1, 35000, 35000, N'Nóng ít ngọt');

-- 19. Bảng QLTK (Tài Khoản Hệ Thống)
CREATE TABLE QLTK (
    TK VARCHAR(50) PRIMARY KEY,
    MK VARCHAR(50) NOT NULL,
    VT INT NOT NULL, -- 1: Admin, 0: Staff
    MaNV VARCHAR(10),
    FOREIGN KEY (MaNV) REFERENCES NhanVien(MaNhanVien) ON DELETE CASCADE
);

-- 20. Bảng QLBCC (Bảng Chấm Công)
CREATE TABLE QLBCC (
    MaBCC VARCHAR(10) PRIMARY KEY,
    Thang INT NOT NULL,
    Nam INT NOT NULL
);

-- 21. Bảng CTBCC (Chi Tiết Chấm Công)
CREATE TABLE CTBCC (
    MaNV VARCHAR(10),
    TenNV NVARCHAR(100),
    MaBCC VARCHAR(10),
    NgayCC VARCHAR(20),
    GioVao VARCHAR(20),
    GioRa VARCHAR(20),
    PRIMARY KEY (MaNV, MaBCC, NgayCC),
    FOREIGN KEY (MaNV) REFERENCES NhanVien(MaNhanVien) ON DELETE CASCADE,
    FOREIGN KEY (MaBCC) REFERENCES QLBCC(MaBCC) ON DELETE CASCADE
);

-- Seed QLTK
INSERT INTO QLTK VALUES ('admin', '1234567', 1, 'NV01');
INSERT INTO QLTK VALUES ('nv02', '1234567', 0, 'NV02');
INSERT INTO QLTK VALUES ('nv03', '1234567', 0, 'NV03');