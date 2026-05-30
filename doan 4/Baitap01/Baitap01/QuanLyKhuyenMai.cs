using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Baitap01
{
    public partial class QuanLyKhuyenMai : Form
    {
        private string strConnect = DBConnect.strcon;

        public QuanLyKhuyenMai()
        {
            InitializeComponent();
            ApplyAesthetic();
            LoadData();
        }

        private void ApplyAesthetic()
        {
            // Cohesive modern Coffee Brand theme (Beige, Warm Espresso, Soft cream)
            Color beigeBg = Color.FromArgb(245, 240, 235);
            Color darkEspresso = Color.FromArgb(90, 65, 50);
            Color brownLabel = Color.FromArgb(100, 75, 60);
            Color creamyInput = Color.FromArgb(252, 250, 248);

            this.BackColor = beigeBg;
            this.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));

            lblTitle.ForeColor = darkEspresso;
            lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTitle.BackColor = Color.FromArgb(235, 227, 218);

            // Grid styling
            dgvKhuyenMai.BackgroundColor = Color.White;
            dgvKhuyenMai.BorderStyle = BorderStyle.None;
            dgvKhuyenMai.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 245, 242);
            dgvKhuyenMai.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvKhuyenMai.RowHeadersVisible = false;
            dgvKhuyenMai.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvKhuyenMai.AllowUserToAddRows = false;
            dgvKhuyenMai.AllowUserToDeleteRows = false;
            dgvKhuyenMai.ReadOnly = true;
            dgvKhuyenMai.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvKhuyenMai.RowTemplate.Height = 34;
            dgvKhuyenMai.GridColor = Color.FromArgb(230, 220, 210);
            dgvKhuyenMai.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(120, 90, 70);
            dgvKhuyenMai.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvKhuyenMai.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            dgvKhuyenMai.EnableHeadersVisualStyles = false;

            // Labels
            lblMaKM.ForeColor = brownLabel;
            lblMaKM.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblDK.ForeColor = brownLabel;
            lblDK.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblStart.ForeColor = brownLabel;
            lblStart.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblEnd.ForeColor = brownLabel;
            lblEnd.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblMucKM.ForeColor = brownLabel;
            lblMucKM.Font = new Font("Segoe UI", 9F, FontStyle.Bold);

            // Inputs
            txtMaKM.BackColor = creamyInput;
            txtMaKM.BorderStyle = BorderStyle.FixedSingle;
            txtMaKM.ForeColor = Color.FromArgb(60, 45, 35);

            txtDK.BackColor = creamyInput;
            txtDK.BorderStyle = BorderStyle.FixedSingle;
            txtDK.ForeColor = Color.FromArgb(60, 45, 35);

            numMucKM.BackColor = creamyInput;
            numMucKM.BorderStyle = BorderStyle.FixedSingle;
            numMucKM.ForeColor = Color.FromArgb(60, 45, 35);

            dtpStart.CalendarMonthBackground = creamyInput;
            dtpEnd.CalendarMonthBackground = creamyInput;

            // Buttons
            btnThem.FlatStyle = FlatStyle.Flat;
            btnThem.FlatAppearance.BorderSize = 0;
            btnThem.BackColor = Color.FromArgb(120, 90, 70); // Rich espresso brown
            btnThem.ForeColor = Color.White;
            btnThem.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnThem.Cursor = Cursors.Hand;

            btnSua.FlatStyle = FlatStyle.Flat;
            btnSua.FlatAppearance.BorderSize = 0;
            btnSua.BackColor = Color.FromArgb(185, 135, 95); // Warm wood amber
            btnSua.ForeColor = Color.White;
            btnSua.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnSua.Cursor = Cursors.Hand;

            btnXoa.FlatStyle = FlatStyle.Flat;
            btnXoa.FlatAppearance.BorderSize = 0;
            btnXoa.BackColor = Color.FromArgb(180, 80, 70); // Red-Espresso accent
            btnXoa.ForeColor = Color.White;
            btnXoa.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnXoa.Cursor = Cursors.Hand;

            btnLuu.FlatStyle = FlatStyle.Flat;
            btnLuu.FlatAppearance.BorderSize = 0;
            btnLuu.BackColor = Color.FromArgb(90, 135, 100); // Sage green accent
            btnLuu.ForeColor = Color.White;
            btnLuu.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnLuu.Cursor = Cursors.Hand;

            btnLamMoi.FlatStyle = FlatStyle.Flat;
            btnLamMoi.FlatAppearance.BorderSize = 0;
            btnLamMoi.BackColor = Color.FromArgb(160, 150, 140); // Greyish brown neutral
            btnLamMoi.ForeColor = Color.White;
            btnLamMoi.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnLamMoi.Cursor = Cursors.Hand;

            btnQuayLai.FlatStyle = FlatStyle.Flat;
            btnQuayLai.FlatAppearance.BorderSize = 0;
            btnQuayLai.BackColor = Color.FromArgb(185, 175, 165); // Soft neutral back button
            btnQuayLai.ForeColor = Color.White;
            btnQuayLai.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnQuayLai.Cursor = Cursors.Hand;
        }

        private void LoadData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(strConnect))
                {
                    string sql = "SELECT MaKhuyenMai AS [MaKM], DieuKienApDung AS [DieuKien], ThoiGianBatDau AS [NgayBatDau], ThoiGianKetThuc AS [NgayKetThuc], MucKhuyenMai AS [MucKM] FROM KhuyenMai";
                    using (SqlDataAdapter adapt = new SqlDataAdapter(sql, conn))
                    {
                        DataTable dt = new DataTable();
                        adapt.Fill(dt);
                        dgvKhuyenMai.DataSource = dt;
                    }
                }

                // Set headers
                if (dgvKhuyenMai.Columns.Count >= 5)
                {
                    dgvKhuyenMai.Columns[0].HeaderText = "Mã KM";
                    dgvKhuyenMai.Columns[1].HeaderText = "Điều Kiện";
                    dgvKhuyenMai.Columns[2].HeaderText = "Bắt Đầu";
                    dgvKhuyenMai.Columns[3].HeaderText = "Kết Thúc";
                    dgvKhuyenMai.Columns[4].HeaderText = "Mức KM";
                    
                    // Format decimal column to currency representation
                    dgvKhuyenMai.Columns[4].DefaultCellStyle.Format = "#,##0 VNĐ";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách khuyến mại: " + ex.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ResetInputs()
        {
            txtMaKM.Text = "";
            txtMaKM.Enabled = true;
            txtDK.Text = "";
            dtpStart.Value = DateTime.Now;
            dtpEnd.Value = DateTime.Now.AddDays(7);
            numMucKM.Value = 0;
        }

        private void dgvKhuyenMai_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvKhuyenMai.Rows[e.RowIndex];
                txtMaKM.Text = row.Cells["MaKM"].Value?.ToString() ?? "";
                txtMaKM.Enabled = false; // Disable PK textbox during edits

                txtDK.Text = row.Cells["DieuKien"].Value?.ToString() ?? "";

                if (row.Cells["NgayBatDau"].Value != DBNull.Value)
                    dtpStart.Value = Convert.ToDateTime(row.Cells["NgayBatDau"].Value);
                
                if (row.Cells["NgayKetThuc"].Value != DBNull.Value)
                    dtpEnd.Value = Convert.ToDateTime(row.Cells["NgayKetThuc"].Value);

                if (row.Cells["MucKM"].Value != DBNull.Value)
                    numMucKM.Value = Convert.ToDecimal(row.Cells["MucKM"].Value);
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            ResetInputs();
            txtMaKM.Focus();
            MessageBox.Show("Mời bạn nhập mã và thông tin khuyến mại mới, sau đó nhấn 'Lưu' để hoàn tất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaKM.Text))
            {
                MessageBox.Show("Vui lòng chọn khuyến mại cần sửa trong bảng trước!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            txtMaKM.Enabled = false;
            txtDK.Focus();
            MessageBox.Show("Mời bạn sửa đổi thông tin cần thiết, sau đó nhấn 'Lưu' để hoàn tất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string ma = txtMaKM.Text.Trim();
            if (string.IsNullOrEmpty(ma))
            {
                MessageBox.Show("Vui lòng chọn chương trình khuyến mại muốn xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (ma == "KM00")
            {
                MessageBox.Show("Không thể xóa mã khuyến mại mặc định 'KM00'!", "Lỗi bảo mật", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            DialogResult confirm = MessageBox.Show($"Bạn có chắc chắn muốn xóa chương trình khuyến mại {ma}?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(strConnect))
                    {
                        conn.Open();
                        string sql = "DELETE FROM KhuyenMai WHERE MaKhuyenMai = @ma";
                        using (SqlCommand cmd = new SqlCommand(sql, conn))
                        {
                            cmd.Parameters.AddWithValue("@ma", ma);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show("Xóa khuyến mại thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ResetInputs();
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không thể xóa khuyến mại này do đã được áp dụng trong các hóa đơn bán hàng cũ.\nChi tiết: " + ex.Message, "Lỗi liên kết dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string ma = txtMaKM.Text.Trim();
            string dk = txtDK.Text.Trim();
            DateTime start = dtpStart.Value;
            DateTime end = dtpEnd.Value;
            decimal muc = numMucKM.Value;

            if (string.IsNullOrEmpty(ma))
            {
                MessageBox.Show("Vui lòng nhập mã khuyến mại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (start > end)
            {
                MessageBox.Show("Ngày bắt đầu không thể lớn hơn ngày kết thúc!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(strConnect))
                {
                    conn.Open();
                    if (txtMaKM.Enabled) // Add mode
                    {
                        // Check duplicates
                        string checkSql = "SELECT COUNT(*) FROM KhuyenMai WHERE MaKhuyenMai = @ma";
                        using (SqlCommand checkCmd = new SqlCommand(checkSql, conn))
                        {
                            checkCmd.Parameters.AddWithValue("@ma", ma);
                            int exists = (int)checkCmd.ExecuteScalar();
                            if (exists > 0)
                            {
                                MessageBox.Show("Mã khuyến mại này đã tồn tại! Vui lòng chọn mã khác.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }

                        // Insert
                        string sql = "INSERT INTO KhuyenMai (MaKhuyenMai, DieuKienApDung, ThoiGianBatDau, ThoiGianKetThuc, MucKhuyenMai) VALUES (@ma, @dk, @start, @end, @muc)";
                        using (SqlCommand cmd = new SqlCommand(sql, conn))
                        {
                            cmd.Parameters.AddWithValue("@ma", ma);
                            cmd.Parameters.AddWithValue("@dk", dk);
                            cmd.Parameters.AddWithValue("@start", start);
                            cmd.Parameters.AddWithValue("@end", end);
                            cmd.Parameters.AddWithValue("@muc", muc);
                            cmd.ExecuteNonQuery();
                        }
                        MessageBox.Show("Lưu khuyến mại mới thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else // Edit mode
                    {
                        // Update
                        string sql = "UPDATE KhuyenMai SET DieuKienApDung = @dk, ThoiGianBatDau = @start, ThoiGianKetThuc = @end, MucKhuyenMai = @muc WHERE MaKhuyenMai = @ma";
                        using (SqlCommand cmd = new SqlCommand(sql, conn))
                        {
                            cmd.Parameters.AddWithValue("@ma", ma);
                            cmd.Parameters.AddWithValue("@dk", dk);
                            cmd.Parameters.AddWithValue("@start", start);
                            cmd.Parameters.AddWithValue("@end", end);
                            cmd.Parameters.AddWithValue("@muc", muc);
                            cmd.ExecuteNonQuery();
                        }
                        MessageBox.Show("Lưu cập nhật khuyến mại thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                ResetInputs();
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lưu khuyến mại: " + ex.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            ResetInputs();
            LoadData();
        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            hethong ht = new hethong();
            ht.Show();
            this.Close();
        }
    }
}
