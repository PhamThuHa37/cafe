using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Baitap01
{
    public partial class QuanLyBan : Form
    {
        private string strConnect = DBConnect.strcon;

        public QuanLyBan()
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
            dgvBan.BackgroundColor = Color.White;
            dgvBan.BorderStyle = BorderStyle.None;
            dgvBan.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 245, 242);
            dgvBan.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvBan.RowHeadersVisible = false;
            dgvBan.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBan.AllowUserToAddRows = false;
            dgvBan.AllowUserToDeleteRows = false;
            dgvBan.ReadOnly = true;
            dgvBan.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvBan.RowTemplate.Height = 34;
            dgvBan.GridColor = Color.FromArgb(230, 220, 210);
            dgvBan.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(120, 90, 70);
            dgvBan.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvBan.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            dgvBan.EnableHeadersVisualStyles = false;

            // Labels
            lblMaBan.ForeColor = brownLabel;
            lblMaBan.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblSoLuong.ForeColor = brownLabel;
            lblSoLuong.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTinhTrang.ForeColor = brownLabel;
            lblTinhTrang.Font = new Font("Segoe UI", 9F, FontStyle.Bold);

            // Inputs
            txtMaBan.BackColor = creamyInput;
            txtMaBan.BorderStyle = BorderStyle.FixedSingle;
            txtMaBan.ForeColor = Color.FromArgb(60, 45, 35);

            numSoLuong.BackColor = creamyInput;
            numSoLuong.BorderStyle = BorderStyle.FixedSingle;
            numSoLuong.ForeColor = Color.FromArgb(60, 45, 35);

            cboTinhTrang.BackColor = creamyInput;
            cboTinhTrang.ForeColor = Color.FromArgb(60, 45, 35);
            cboTinhTrang.Items.AddRange(new object[] { "Trống", "Đã đặt trước", "Có khách" });
            cboTinhTrang.SelectedIndex = 0;

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

            btnLamMoi.FlatStyle = FlatStyle.Flat;
            btnLamMoi.FlatAppearance.BorderSize = 0;
            btnLamMoi.BackColor = Color.FromArgb(160, 150, 140); // Greyish brown neutral
            btnLamMoi.ForeColor = Color.White;
            btnLamMoi.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnLamMoi.Cursor = Cursors.Hand;

            btnLuu.FlatStyle = FlatStyle.Flat;
            btnLuu.FlatAppearance.BorderSize = 0;
            btnLuu.BackColor = Color.FromArgb(90, 135, 100); // Sage green accent
            btnLuu.ForeColor = Color.White;
            btnLuu.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnLuu.Cursor = Cursors.Hand;

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
                    string sql = "SELECT MaBan AS [MaBan], SoLuong AS [SoLuong], TinhTrang AS [TinhTrang] FROM Ban";
                    using (SqlDataAdapter adapt = new SqlDataAdapter(sql, conn))
                    {
                        DataTable dt = new DataTable();
                        adapt.Fill(dt);
                        dgvBan.DataSource = dt;
                    }
                }
                
                // Set custom header text for professional look
                if (dgvBan.Columns.Count >= 3)
                {
                    dgvBan.Columns[0].HeaderText = "Mã Bàn";
                    dgvBan.Columns[1].HeaderText = "Số Chỗ";
                    dgvBan.Columns[2].HeaderText = "Trạng Thái";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi nạp dữ liệu danh sách bàn: " + ex.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ResetInputs()
        {
            txtMaBan.Text = "";
            txtMaBan.Enabled = true;
            numSoLuong.Value = 4;
            cboTinhTrang.SelectedIndex = 0;
        }

        private void dgvBan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvBan.Rows[e.RowIndex];
                txtMaBan.Text = row.Cells["MaBan"].Value?.ToString() ?? "";
                txtMaBan.Enabled = false; // Disable ID modification when selected
                
                if (row.Cells["SoLuong"].Value != DBNull.Value)
                {
                    numSoLuong.Value = Convert.ToDecimal(row.Cells["SoLuong"].Value);
                }
                
                string tt = row.Cells["TinhTrang"].Value?.ToString() ?? "Trống";
                
                // Align UI status list with database values
                if (tt == "Đã đặt" || tt == "Đã đặt trước")
                {
                    cboTinhTrang.SelectedItem = "Đã đặt trước";
                }
                else if (tt == "Có khách")
                {
                    cboTinhTrang.SelectedItem = "Có khách";
                }
                else
                {
                    cboTinhTrang.SelectedItem = "Trống";
                }
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string ma = txtMaBan.Text.Trim();
            int sl = (int)numSoLuong.Value;
            string tt = cboTinhTrang.SelectedItem?.ToString() ?? "Trống";

            if (string.IsNullOrEmpty(ma))
            {
                MessageBox.Show("Vui lòng nhập mã bàn (Ví dụ: B11)!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(strConnect))
                {
                    conn.Open();
                    // Duplication verification
                    string checkSql = "SELECT COUNT(*) FROM Ban WHERE MaBan = @ma";
                    using (SqlCommand checkCmd = new SqlCommand(checkSql, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@ma", ma);
                        int exists = (int)checkCmd.ExecuteScalar();
                        if (exists > 0)
                        {
                            MessageBox.Show("Mã bàn này đã tồn tại! Vui lòng chọn mã khác.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    string insertSql = "INSERT INTO Ban (MaBan, SoLuong, TinhTrang) VALUES (@ma, @sl, @tt)";
                    using (SqlCommand cmd = new SqlCommand(insertSql, conn))
                    {
                        cmd.Parameters.AddWithValue("@ma", ma);
                        cmd.Parameters.AddWithValue("@sl", sl);
                        cmd.Parameters.AddWithValue("@tt", tt);
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Thêm bàn mới thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ResetInputs();
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thêm bàn: " + ex.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string ma = txtMaBan.Text.Trim();
            int sl = (int)numSoLuong.Value;
            string tt = cboTinhTrang.SelectedItem?.ToString() ?? "Trống";

            if (string.IsNullOrEmpty(ma))
            {
                MessageBox.Show("Vui lòng chọn bàn cần sửa đổi thông tin trong danh sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(strConnect))
                {
                    conn.Open();
                    string updateSql = "UPDATE Ban SET SoLuong = @sl, TinhTrang = @tt WHERE MaBan = @ma";
                    using (SqlCommand cmd = new SqlCommand(updateSql, conn))
                    {
                        cmd.Parameters.AddWithValue("@ma", ma);
                        cmd.Parameters.AddWithValue("@sl", sl);
                        cmd.Parameters.AddWithValue("@tt", tt);
                        int updated = cmd.ExecuteNonQuery();
                        
                        if (updated > 0)
                        {
                            MessageBox.Show("Cập nhật thông tin bàn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy thông tin bàn cần cập nhật!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                ResetInputs();
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi sửa thông tin bàn: " + ex.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string ma = txtMaBan.Text.Trim();
            if (string.IsNullOrEmpty(ma))
            {
                MessageBox.Show("Vui lòng chọn bàn muốn xóa từ danh sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show($"Bạn có chắc chắn muốn xóa bàn {ma} khỏi hệ thống?", "Xác nhận xóa bàn", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(strConnect))
                    {
                        conn.Open();
                        string deleteSql = "DELETE FROM Ban WHERE MaBan = @ma";
                        using (SqlCommand cmd = new SqlCommand(deleteSql, conn))
                        {
                            cmd.Parameters.AddWithValue("@ma", ma);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show("Xóa bàn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ResetInputs();
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không thể xóa bàn do bàn này đang được liên kết trong lịch sử hóa đơn bán hàng.\nChi tiết lỗi: " + ex.Message, "Lỗi Liên Kết Dữ Liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string ma = txtMaBan.Text.Trim();
            int sl = (int)numSoLuong.Value;
            string tt = cboTinhTrang.SelectedItem?.ToString() ?? "Trống";

            if (string.IsNullOrEmpty(ma))
            {
                MessageBox.Show("Vui lòng nhập mã bàn hoặc chọn một bàn từ danh sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(strConnect))
                {
                    conn.Open();
                    if (txtMaBan.Enabled) // Add new mode
                    {
                        string checkSql = "SELECT COUNT(*) FROM Ban WHERE MaBan = @ma";
                        using (SqlCommand checkCmd = new SqlCommand(checkSql, conn))
                        {
                            checkCmd.Parameters.AddWithValue("@ma", ma);
                            int exists = (int)checkCmd.ExecuteScalar();
                            if (exists > 0)
                            {
                                MessageBox.Show("Mã bàn này đã tồn tại! Vui lòng chọn mã khác.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }

                        string insertSql = "INSERT INTO Ban (MaBan, SoLuong, TinhTrang) VALUES (@ma, @sl, @tt)";
                        using (SqlCommand cmd = new SqlCommand(insertSql, conn))
                        {
                            cmd.Parameters.AddWithValue("@ma", ma);
                            cmd.Parameters.AddWithValue("@sl", sl);
                            cmd.Parameters.AddWithValue("@tt", tt);
                            cmd.ExecuteNonQuery();
                        }
                        MessageBox.Show("Lưu bàn mới thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else // Edit mode
                    {
                        string updateSql = "UPDATE Ban SET SoLuong = @sl, TinhTrang = @tt WHERE MaBan = @ma";
                        using (SqlCommand cmd = new SqlCommand(updateSql, conn))
                        {
                            cmd.Parameters.AddWithValue("@ma", ma);
                            cmd.Parameters.AddWithValue("@sl", sl);
                            cmd.Parameters.AddWithValue("@tt", tt);
                            cmd.ExecuteNonQuery();
                        }
                        MessageBox.Show("Lưu thông tin cập nhật bàn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                ResetInputs();
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lưu dữ liệu bàn: " + ex.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
