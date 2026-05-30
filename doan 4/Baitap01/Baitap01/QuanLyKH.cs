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
    public partial class QuanLyKH : Form
    {
        private string strConnect = DBConnect.strcon;

        public QuanLyKH()
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
            label6.Text = "QUẢN LÝ KHÁCH HÀNG";

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
                    if (btn == button5) // Nút Thoát
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
            dataGridView1.BackgroundColor = Color.White;
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.GridColor = Color.FromArgb(235, 230, 225);
            dataGridView1.RowTemplate.Height = 32;

            // Hàng tiêu đề của DataGridView
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(120, 90, 70);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            // Định dạng font chữ tế nhị và dòng xen kẽ
            dataGridView1.DefaultCellStyle.BackColor = Color.White;
            dataGridView1.DefaultCellStyle.ForeColor = Color.FromArgb(60, 45, 35);
            dataGridView1.DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.FromArgb(185, 160, 140); // Be đậm khi click chọn
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.White;

            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 246, 244);
            dataGridView1.AlternatingRowsDefaultCellStyle.ForeColor = Color.FromArgb(60, 45, 35);
        }

        private void LoadData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(strConnect))
                {
                    string sql = "SELECT MaKhachHang AS MaKH, TenKhachHang AS TenKH, '' AS GT, SoDienThoai AS SDT, CAST(DiemTichLuy AS nvarchar(50)) AS DC FROM KhachHang";
                    using (SqlDataAdapter adapt = new SqlDataAdapter(sql, conn))
                    {
                        DataTable dt = new DataTable();
                        adapt.Fill(dt);
                        dataGridView1.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi nạp dữ liệu khách hàng: " + ex.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Nút Thêm khách hàng
        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Mã và Tên khách hàng!");
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(strConnect))
                {
                    string sql = "INSERT INTO KhachHang (MaKhachHang, TenKhachHang, SoDienThoai, DiemTichLuy) VALUES (@ma, @ten, @sdt, @diem)";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        conn.Open();
                        cmd.Parameters.AddWithValue("@ma", textBox1.Text.Trim());
                        cmd.Parameters.AddWithValue("@ten", textBox2.Text.Trim());
                        cmd.Parameters.AddWithValue("@sdt", textBox3.Text.Trim());

                        int diem = 0;
                        int.TryParse(textBox4.Text, out diem);
                        cmd.Parameters.AddWithValue("@diem", diem);

                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Thêm khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
                Reset();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: Mã khách hàng đã tồn tại hoặc dữ liệu không hợp lệ! " + ex.Message);
            }
        }

        // Nút Sửa khách hàng
        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Vui lòng chọn khách hàng cần sửa!");
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(strConnect))
                {
                    string sql = "UPDATE KhachHang SET TenKhachHang=@ten, SoDienThoai=@sdt, DiemTichLuy=@diem WHERE MaKhachHang=@ma";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        conn.Open();
                        cmd.Parameters.AddWithValue("@ma", textBox1.Text.Trim());
                        cmd.Parameters.AddWithValue("@ten", textBox2.Text.Trim());
                        cmd.Parameters.AddWithValue("@sdt", textBox3.Text.Trim());

                        int diem = 0;
                        int.TryParse(textBox4.Text, out diem);
                        cmd.Parameters.AddWithValue("@diem", diem);

                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Đã sửa thông tin khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
                Reset();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi cập nhật: " + ex.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Nút Tìm kiếm khách hàng
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(strConnect))
                {
                    string sql = "SELECT MaKhachHang AS MaKH, TenKhachHang AS TenKH, '' AS GT, SoDienThoai AS SDT, CAST(DiemTichLuy AS nvarchar(50)) AS DC " +
                                 "FROM KhachHang WHERE TenKhachHang LIKE @search";
                    using (SqlDataAdapter adapt = new SqlDataAdapter(sql, conn))
                    {
                        adapt.SelectCommand.Parameters.AddWithValue("@search", "%" + textBox5.Text.Trim() + "%");
                        DataTable dt = new DataTable();
                        adapt.Fill(dt);
                        dataGridView1.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm: " + ex.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Click dòng trên DataGridView nạp dữ liệu lên TextBoxes
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                textBox1.Text = row.Cells["MaKH"].Value?.ToString() ?? "";
                textBox2.Text = row.Cells["TenKH"].Value?.ToString() ?? "";
                textBox9.Text = row.Cells["GT"].Value?.ToString() ?? "";
                textBox3.Text = row.Cells["SDT"].Value?.ToString() ?? "";
                textBox4.Text = row.Cells["DC"].Value?.ToString() ?? "";
                
                textBox1.Enabled = false; // Khóa trường Mã khách hàng chính
            }
        }

        // Nút Xóa khách hàng
        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Vui lòng chọn khách hàng cần xóa!");
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa khách hàng này không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(strConnect))
                    {
                        string sql = "DELETE FROM KhachHang WHERE MaKhachHang=@ma";
                        using (SqlCommand cmd = new SqlCommand(sql, conn))
                        {
                            conn.Open();
                            cmd.Parameters.AddWithValue("@ma", textBox1.Text.Trim());
                            cmd.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show("Xóa khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                    Reset();
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 547) // Lỗi khóa ngoại
                    {
                        MessageBox.Show("Không thể xóa khách hàng này vì khách hàng đã có lịch sử hóa đơn mua hàng trong hệ thống!", "Cảnh báo ràng buộc", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        MessageBox.Show("Lỗi cơ sở dữ liệu: " + ex.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xóa khách hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Reset()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox9.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox1.Enabled = true;
        }

        // Nút Reload danh sách
        private void button6_Click(object sender, EventArgs e)
        {
            Reset();
            textBox5.Text = "";
            LoadData();
        }

        // Nút Quay lại hệ thống chính
        private void button5_Click(object sender, EventArgs e)
        {
            hethong ht = new hethong();
            ht.Show();
            this.Close();
        }

        private void QuanLyKH_Load(object sender, EventArgs e)
        {

        }
    }
}
