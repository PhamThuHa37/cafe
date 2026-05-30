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
    public partial class QLNhanVien : Form
    {
        // Chuỗi kết nối dùng chung cho toàn bộ Form, lấy từ DBConnect để quản lý tập trung
        private string strConnect = DBConnect.strcon;

        public QLNhanVien()
        {
            InitializeComponent();
            StyleForm(); // Áp dụng giao diện tone Nâu - Be sang trọng
            LoadData();
        }

        // Tự động thiết kế giao diện tone Nâu - Be ấm áp, hiện đại (Wow Aesthetic)
        private void StyleForm()
        {
            // Màu nền chính của Form (Màu Be sáng)
            this.BackColor = Color.FromArgb(245, 240, 235);
            this.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));

            // Tiêu đề lớn
            label6.ForeColor = Color.FromArgb(90, 65, 50); // Nâu đậm Espresso
            label6.Font = new Font("Segoe UI", 22F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            label6.Text = "QUẢN LÝ NHÂN VIÊN";

            // GroupBox thông tin (Dạng thẻ trắng nổi bật)
            groupBox1.BackColor = Color.White;
            groupBox1.ForeColor = Color.FromArgb(90, 65, 50);
            groupBox1.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));

            // Định dạng lại các điều khiển trong GroupBox
            foreach (Control ctrl in groupBox1.Controls)
            {
                if (ctrl is Label lbl)
                {
                    lbl.ForeColor = Color.FromArgb(90, 65, 50);
                    lbl.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
                }
                else if (ctrl is TextBox txt)
                {
                    txt.BackColor = Color.FromArgb(248, 246, 244);
                    txt.ForeColor = Color.FromArgb(60, 45, 35);
                    txt.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
                    txt.BorderStyle = BorderStyle.FixedSingle;
                }
                else if (ctrl is ComboBox cb)
                {
                    cb.BackColor = Color.FromArgb(248, 246, 244);
                    cb.ForeColor = Color.FromArgb(60, 45, 35);
                    cb.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
                }
                else if (ctrl is Button btn)
                {
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.BorderSize = 0;
                    btn.BackColor = Color.FromArgb(120, 90, 70); // Nâu ấm
                    btn.ForeColor = Color.White;
                    btn.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                    btn.Cursor = Cursors.Hand;
                }
            }

            // Định dạng các điều khiển ngoài GroupBox
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is Button btn)
                {
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.BorderSize = 0;
                    if (btn == button6) // Nút Thoát
                    {
                        btn.BackColor = Color.FromArgb(180, 75, 75); // Màu đỏ nhạt sang trọng
                    }
                    else
                    {
                        btn.BackColor = Color.FromArgb(120, 90, 70);
                    }
                    btn.ForeColor = Color.White;
                    btn.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                    btn.Cursor = Cursors.Hand;
                }
                else if (ctrl is TextBox txt)
                {
                    txt.BackColor = Color.FromArgb(248, 246, 244);
                    txt.ForeColor = Color.FromArgb(60, 45, 35);
                    txt.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
                    txt.BorderStyle = BorderStyle.FixedSingle;
                }
            }

            // Thiết kế bảng DataGridView (Màu nâu be đồng bộ)
            data.BackgroundColor = Color.White;
            data.BorderStyle = BorderStyle.None;
            data.RowHeadersVisible = false;
            data.AllowUserToAddRows = false;
            data.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            data.GridColor = Color.FromArgb(235, 230, 225);
            data.RowTemplate.Height = 32;

            // Hàng tiêu đề của DataGridView
            data.EnableHeadersVisualStyles = false;
            data.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(120, 90, 70);
            data.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            data.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            data.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            // Định dạng font chữ tế nhị và dòng xen kẽ
            data.DefaultCellStyle.BackColor = Color.White;
            data.DefaultCellStyle.ForeColor = Color.FromArgb(60, 45, 35);
            data.DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            data.DefaultCellStyle.SelectionBackColor = Color.FromArgb(185, 160, 140); // Be đậm khi click chọn
            data.DefaultCellStyle.SelectionForeColor = Color.White;

            data.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 246, 244);
            data.AlternatingRowsDefaultCellStyle.ForeColor = Color.FromArgb(60, 45, 35);
        }

        // Hàm đọc dữ liệu từ SQL Server trả về một DataTable
        private DataTable GetDataToTable(string sql)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(strConnect))
                {
                    using (SqlDataAdapter adapt = new SqlDataAdapter(sql, conn))
                    {
                        adapt.Fill(dt);
                    }
                }
                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi kết nối cơ sở dữ liệu: {ex.Message}", "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        private void LoadData()
        {
            // Chọn thêm cả nv.MaChucVu làm cột ẩn/phụ để ComboBox có thể SelectedValue chính xác
            string sql = "SELECT nv.MaNhanVien AS MaNV, nv.TenNhanVien AS TenNV, nv.DiaChi AS GT, nv.SoDienthoai AS SDT, cv.TenChucVu AS ChucVu, nv.MaChucVu AS MaCV " +
                         "FROM NhanVien nv LEFT JOIN ChucVu cv ON nv.MaChucVu = cv.MaChucVu";
            DataTable dt = GetDataToTable(sql);
            if (dt != null)
            {
                data.DataSource = dt;
                // Ẩn cột MaCV khỏi GridView để giao diện đẹp đẽ và sạch sẽ
                if (data.Columns["MaCV"] != null)
                {
                    data.Columns["MaCV"].Visible = false;
                }
            }
        }

        // Form Load: Nạp danh sách mã chức vụ vào ô Chọn
        private void QLNhanVien_Load(object sender, EventArgs e)
        {
            string sql = "SELECT MaChucVu, TenChucVu FROM ChucVu";
            DataTable dt = GetDataToTable(sql);
            if (dt != null)
            {
                textBox5.DataSource = dt;
                textBox5.DisplayMember = "TenChucVu"; // Hiển thị tên chức vụ thân thiện (ví dụ: Quản lý)
                textBox5.ValueMember = "MaChucVu";   // Giá trị ẩn bên dưới gửi vào database (ví dụ: CV01)
            }
        }

        // Nút Tìm kiếm
        private void button4_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTim.Text))
            {
                LoadData();
                return;
            }

            string safeSearch = txtTim.Text.Replace("'", "''");
            string sql = "SELECT nv.MaNhanVien AS MaNV, nv.TenNhanVien AS TenNV, nv.DiaChi AS GT, nv.SoDienthoai AS SDT, cv.TenChucVu AS ChucVu, nv.MaChucVu AS MaCV " +
                         "FROM NhanVien nv LEFT JOIN ChucVu cv ON nv.MaChucVu = cv.MaChucVu " +
                         $"WHERE nv.TenNhanVien LIKE N'%{safeSearch}%'";
            DataTable dt = GetDataToTable(sql);
            if (dt != null)
            {
                data.DataSource = dt;
                if (data.Columns["MaCV"] != null)
                {
                    data.Columns["MaCV"].Visible = false;
                }
            }
        }

        // Nút Thêm nhân viên
        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Mã và Tên nhân viên!");
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(strConnect))
                {
                    string sql = "INSERT INTO NhanVien (MaNhanVien, TenNhanVien, DiaChi, SoDienthoai, MaChucVu, MaBangLuong) VALUES (@ma, @ten, @gioitinh, @sdt, @mcv, 'BL04')";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        conn.Open();
                        cmd.Parameters.AddWithValue("@ma", textBox1.Text.Trim());
                        cmd.Parameters.AddWithValue("@ten", textBox2.Text.Trim());
                        cmd.Parameters.AddWithValue("@gioitinh", textBox3.Text.Trim());
                        cmd.Parameters.AddWithValue("@sdt", textBox4.Text.Trim());

                        // Lấy giá trị Mã chức vụ một cách an toàn tránh lỗi System.Data.DataRowView
                        string maCV = "";
                        if (textBox5.SelectedItem is DataRowView drv)
                        {
                            maCV = drv["MaChucVu"]?.ToString() ?? "";
                        }
                        else if (textBox5.SelectedValue != null)
                        {
                            maCV = textBox5.SelectedValue.ToString();
                        }
                        else
                        {
                            maCV = textBox5.Text.Trim();
                        }
                        cmd.Parameters.AddWithValue("@mcv", maCV);

                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Thêm nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
                Reset();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: Mã nhân viên đã tồn tại hoặc dữ liệu không hợp lệ! " + ex.Message);
            }
        }

        // Nút Sửa nhân viên
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(strConnect))
                {
                    string sql = "UPDATE NhanVien SET TenNhanVien=@ten, DiaChi=@gt, SoDienthoai=@sdt, MaChucVu=@mcv WHERE MaNhanVien=@ma";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        conn.Open();
                        cmd.Parameters.AddWithValue("@ma", textBox1.Text.Trim());
                        cmd.Parameters.AddWithValue("@ten", textBox2.Text.Trim());
                        cmd.Parameters.AddWithValue("@gt", textBox3.Text.Trim());
                        cmd.Parameters.AddWithValue("@sdt", textBox4.Text.Trim());

                        // Lấy giá trị Mã chức vụ một cách an toàn tránh lỗi System.Data.DataRowView
                        string maCV = "";
                        if (textBox5.SelectedItem is DataRowView drv)
                        {
                            maCV = drv["MaChucVu"]?.ToString() ?? "";
                        }
                        else if (textBox5.SelectedValue != null)
                        {
                            maCV = textBox5.SelectedValue.ToString();
                        }
                        else
                        {
                            maCV = textBox5.Text.Trim();
                        }
                        cmd.Parameters.AddWithValue("@mcv", maCV);

                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Đã sửa thông tin nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
                Reset();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật: " + ex.Message);
            }
        }

        // Nút Xóa nhân viên
        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa nhân viên này không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(strConnect))
                    {
                        string sql = "DELETE FROM NhanVien WHERE MaNhanVien=@manv";
                        using (SqlCommand cmd = new SqlCommand(sql, conn))
                        {
                            conn.Open();
                            cmd.Parameters.AddWithValue("@manv", textBox1.Text.Trim());
                            cmd.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show("Xóa thành công!");
                    LoadData();
                    Reset();
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 547) // Lỗi vi phạm Khóa Ngoại trong SQL Server
                    {
                        MessageBox.Show("Không thể xóa nhân viên này vì nhân viên đã có dữ liệu liên kết như phân ca, hóa đơn bán hàng hoặc hóa đơn nhập hàng trong hệ thống!", "Cảnh báo liên kết dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        MessageBox.Show("Lỗi cơ sở dữ liệu: " + ex.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Khi click vào dòng trên GridView hiển thị ngược lên các TextBox
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra tránh click vào dòng trống tiêu đề
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = data.Rows[e.RowIndex];

                textBox1.Enabled = false; // Khóa không cho sửa Mã khóa chính

                textBox1.Text = row.Cells["MaNV"].Value?.ToString() ?? "";
                textBox2.Text = row.Cells["TenNV"].Value?.ToString() ?? "";
                textBox3.Text = row.Cells["GT"].Value?.ToString() ?? "";
                textBox4.Text = row.Cells["SDT"].Value?.ToString() ?? "";

                // Gán trực tiếp Mã chức vụ vào SelectedValue để ComboBox tự động khớp chính xác
                if (row.Cells["MaCV"] != null && row.Cells["MaCV"].Value != null)
                {
                    textBox5.SelectedValue = row.Cells["MaCV"].Value.ToString();
                }
                else
                {
                    textBox5.Text = row.Cells["ChucVu"].Value?.ToString() ?? "";
                }
            }
        }

        // Nút Làm mới (Reset)
        private void btnLM_Click(object sender, EventArgs e)
        {
            Reset();
            LoadData();
        }

        private void Reset()
        {
            textBox1.Enabled = true;
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            if (textBox5.Items.Count > 0) textBox5.SelectedIndex = 0;
            txtTim.Text = "";
        }

        // Nút Quay lại hệ thống
        private void button6_Click(object sender, EventArgs e)
        {
            hethong ht = new hethong();
            ht.Show();
            this.Close();
        }

        // Bỏ trống nếu không sử dụng tránh lỗi phát sinh
        private void textBox5_SelectedIndexChanged(object sender, EventArgs e) { }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
    }
}