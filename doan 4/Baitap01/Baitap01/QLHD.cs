using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Baitap01
{
    public partial class QLHD : Form
    {
        string strcon = DBConnect.strcon;
        SqlConnection sqlcon = null;
        SqlCommand cmd;
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable table = new DataTable();

        // Custom status tracking controls
        private GroupBox grpStatus;
        private Label lblSelected;
        private Label lblStatus;
        private Button btnBrewing;
        private Button btnComplete;
        private Button btnCancel;
        private Button btnRefund;

        public QLHD()
        {
            InitializeComponent();
            StyleForm(); // Áp dụng giao diện Nâu - Be và bộ điều khiển trạng thái
            LoadData();
        }

        private void StyleForm()
        {
            // Thiết lập kích thước lớn hơn để có chỗ cho thanh trạng thái
            this.Size = new Size(1010, 500);
            this.BackColor = Color.FromArgb(245, 240, 235);
            this.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));

            label1.ForeColor = Color.FromArgb(90, 65, 50);
            label1.Font = new Font("Segoe UI", 20F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            label1.Text = "QUẢN LÝ ĐƠN HÀNG & HÓA ĐƠN";
            label1.Location = new Point(16, 15);
            label1.Size = new Size(960, 45);
            label1.TextAlign = ContentAlignment.MiddleCenter;

            // Form buttons
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is Button btn)
                {
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.BorderSize = 0;
                    if (btn == btnquaylai)
                        btn.BackColor = Color.FromArgb(180, 75, 75);
                    else
                        btn.BackColor = Color.FromArgb(120, 90, 70);
                    btn.ForeColor = Color.White;
                    btn.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                    btn.Cursor = Cursors.Hand;
                }
                else if (ctrl is TextBox txt)
                {
                    txt.BackColor = Color.FromArgb(248, 246, 244);
                    txt.BorderStyle = BorderStyle.FixedSingle;
                    txt.ForeColor = Color.FromArgb(60, 45, 35);
                }
            }

            // Reposition controls
            dataGridView2.Location = new Point(16, 120);
            dataGridView2.Size = new Size(680, 280);
            dataGridView2.BackgroundColor = Color.White;
            dataGridView2.BorderStyle = BorderStyle.None;
            dataGridView2.RowHeadersVisible = false;
            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView2.GridColor = Color.FromArgb(235, 230, 225);
            dataGridView2.RowTemplate.Height = 32;

            dataGridView2.EnableHeadersVisualStyles = false;
            dataGridView2.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(120, 90, 70);
            dataGridView2.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView2.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);

            dataGridView2.DefaultCellStyle.BackColor = Color.White;
            dataGridView2.DefaultCellStyle.ForeColor = Color.FromArgb(60, 45, 35);
            dataGridView2.DefaultCellStyle.SelectionBackColor = Color.FromArgb(185, 160, 140);
            dataGridView2.DefaultCellStyle.SelectionForeColor = Color.White;

            dataGridView2.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 246, 244);

            // Programmatically inject status GroupBox
            grpStatus = new GroupBox();
            grpStatus.Text = "Cập nhật Trạng thái Đơn";
            grpStatus.Location = new Point(710, 115);
            grpStatus.Size = new Size(260, 285);
            grpStatus.BackColor = Color.White;
            grpStatus.ForeColor = Color.FromArgb(90, 65, 50);
            grpStatus.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);

            lblSelected = new Label();
            lblSelected.Text = "Đơn chọn: Chưa chọn";
            lblSelected.Location = new Point(15, 30);
            lblSelected.Size = new Size(230, 20);
            lblSelected.ForeColor = Color.FromArgb(90, 65, 50);
            lblSelected.Font = new Font("Segoe UI", 9F, FontStyle.Bold);

            lblStatus = new Label();
            lblStatus.Text = "Trạng thái: N/A";
            lblStatus.Location = new Point(15, 55);
            lblStatus.Size = new Size(230, 20);
            lblStatus.ForeColor = Color.FromArgb(120, 90, 70);
            lblStatus.Font = new Font("Segoe UI", 9F, FontStyle.Bold);

            // Setup buttons
            btnBrewing = CreateCustomButton("Bắt đầu Pha Chế", Color.FromArgb(140, 110, 90), 85);
            btnComplete = CreateCustomButton("Hoàn thành Món", Color.FromArgb(90, 120, 90), 125);
            btnCancel = CreateCustomButton("Hủy đơn hàng", Color.FromArgb(180, 100, 100), 165);
            btnRefund = CreateCustomButton("Hoàn trả / Hoàn tiền", Color.FromArgb(160, 120, 80), 205);

            btnBrewing.Click += (s, e) => UpdateStatus("Đang pha chế");
            btnComplete.Click += (s, e) => UpdateStatus("Đã hoàn thành");
            btnCancel.Click += (s, e) => UpdateStatus("Đã hủy");
            btnRefund.Click += (s, e) => {
                if (string.IsNullOrEmpty(laymahd))
                {
                    MessageBox.Show("Vui lòng chọn một đơn hàng!");
                    return;
                }
                if (MessageBox.Show("Bạn có muốn thực hiện hoàn trả món/hoàn tiền cho đơn " + laymahd + " không?", "Hoàn trả món", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    UpdateStatus("Đã hủy");
                    MessageBox.Show("Đã hoàn tất thủ tục hoàn trả món và hoàn tiền cho khách hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            };

            grpStatus.Controls.Add(lblSelected);
            grpStatus.Controls.Add(lblStatus);
            grpStatus.Controls.Add(btnBrewing);
            grpStatus.Controls.Add(btnComplete);
            grpStatus.Controls.Add(btnCancel);
            grpStatus.Controls.Add(btnRefund);

            this.Controls.Add(grpStatus);

            // Role perms inside QLHD
            if (Login.pq == 2) // Pha chế chỉ cần pha chế, hoàn thành, hủy
            {
                btnRefund.Visible = false;
                btnlhd.Visible = false; // Ẩn nút tạo mới hóa đơn
            }
            else if (Login.pq == 4) // Kho không dùng
            {
                grpStatus.Enabled = false;
            }
        }

        private Button CreateCustomButton(string text, Color bg, int y)
        {
            Button btn = new Button();
            btn.Text = text;
            btn.Location = new Point(15, y);
            btn.Size = new Size(230, 32);
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = bg;
            btn.ForeColor = Color.White;
            btn.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btn.Cursor = Cursors.Hand;
            return btn;
        }

        private void UpdateStatus(string trangThai)
        {
            if (string.IsNullOrEmpty(laymahd))
            {
                MessageBox.Show("Vui lòng chọn một đơn hàng để cập nhật trạng thái!");
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(strcon))
                {
                    conn.Open();
                    string sql = "UPDATE HoaDonBan SET TrangThai = @tt WHERE MaHoaDonBan = @mahd";
                    using (SqlCommand command = new SqlCommand(sql, conn))
                    {
                        command.Parameters.AddWithValue("@tt", trangThai);
                        command.Parameters.AddWithValue("@mahd", laymahd);
                        command.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Cập nhật trạng thái đơn hàng thành công!");
                LoadData();
                lblStatus.Text = "Trạng thái: " + trangThai;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi cập nhật trạng thái: " + ex.Message);
            }
        }

        private void LoadData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(strcon))
                {
                    string sql = "select MaHoaDonBan as MaHD, MaKhachHang as MaKH, NgayLap, TongTienBan as ThanhTien, TrangThai from HoaDonBan";
                    using (SqlDataAdapter adapt = new SqlDataAdapter(sql, conn))
                    {
                        DataTable dt = new DataTable();
                        adapt.Fill(dt);
                        dataGridView2.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi nạp danh sách hóa đơn: " + ex.Message);
            }
        }

        int taomahd()
        {
            int ma = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(strcon))
                {
                    conn.Open();
                    string sql = "select count(*) from HoaDonBan";
                    using (SqlCommand command = new SqlCommand(sql, conn))
                    {
                        ma = (int)command.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tạo mã: " + ex.Message);
            }
            return ma + 1;
        }

        private void btnlhd_Click(object sender, EventArgs e)
        {
            laymahd = "HD" + taomahd();
            try
            {
                using (SqlConnection conn = new SqlConnection(strcon))
                {
                    conn.Open();
                    string qr = "insert into HoaDonBan(MaHoaDonBan, TrangThai, TongTienBan, MaNhanVien, MaKhachHang, MaKhuyenMai, MaBan, NgayLap) " +
                                 "values (@mahd, N'Chờ xử lý', 0, @manv, 'KH01', 'KM00', 'B01', @ngay)";
                    using (SqlCommand command = new SqlCommand(qr, conn))
                    {
                        command.Parameters.AddWithValue("@mahd", laymahd);
                        command.Parameters.AddWithValue("@manv", Login.nv ?? "NV01");
                        command.Parameters.AddWithValue("@ngay", DateTime.Now.ToString("dd/MM/yyyy"));
                        command.ExecuteNonQuery();
                    }
                }
                LoadData();
                LapHD lhd = new LapHD();
                lhd.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tạo hóa đơn mới: " + ex.Message);
            }
        }

        private void btnquaylai_Click(object sender, EventArgs e)
        {
            hethong ht = new hethong();
            ht.Show();
            this.Close();
        }

        private void btnLM_Click(object sender, EventArgs e)
        {
            txttim.Text = "";
            LoadData();
        }

        private void btnxemchitiet_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(laymahd))
            {
                MessageBox.Show("Vui lòng chọn hóa đơn cần xem!");
                return;
            }
            CTHD ct = new CTHD();
            ct.Show();
        }

        public static string laymahd;

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView2.CurrentRow != null)
            {
                laymahd = dataGridView2.CurrentRow.Cells["MaHD"].Value?.ToString() ?? "";
                lblSelected.Text = "Đơn chọn: " + laymahd;
                
                string trangThai = dataGridView2.CurrentRow.Cells["TrangThai"].Value?.ToString() ?? "N/A";
                lblStatus.Text = "Trạng thái: " + trangThai;
            }
        }

        private void btntim_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(strcon))
                {
                    string sql = "select MaHoaDonBan as MaHD, MaKhachHang as MaKH, NgayLap, TongTienBan as ThanhTien, TrangThai from HoaDonBan where MaHoaDonBan=@tim or MaKhachHang=@tim";
                    using (SqlCommand command = new SqlCommand(sql, conn))
                    {
                        command.Parameters.AddWithValue("@tim", txttim.Text.Trim());
                        using (SqlDataAdapter adapt = new SqlDataAdapter(command))
                        {
                            DataTable dt = new DataTable();
                            adapt.Fill(dt);
                            dataGridView2.DataSource = dt;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm hóa đơn: " + ex.Message);
            }
        }

        private void QLHD_Load(object sender, EventArgs e)
        {

        }
    }
}
