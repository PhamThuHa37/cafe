using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Baitap01
{
    public partial class ChamCong : Form
    {
        string strcon = @"Data Source=HUONGLT\SQLEXPRESS;Initial Catalog=QuanLyQuanCaFe;Integrated Security=True";
        DateTimePicker dtpNgay;
        DataGridView dgvChamCong;
        public static string ma; // Dummy for ChiTietCC compilation

        public ChamCong()
        {
            InitializeComponent();
            Baitap01.ThemeManager.ApplyTheme(this);
            
            // Hide old UI
            foreach(Control c in this.Controls) {
                c.Visible = false;
            }
            
            // Build new UI
            Label lblTitle = new Label { Text = "QUẢN LÝ CHẤM CÔNG", Font = new Font("Microsoft Sans Serif", 20), AutoSize = true, Location = new Point(20, 20) };
            this.Controls.Add(lblTitle);
            
            dtpNgay = new DateTimePicker { Location = new Point(20, 70), Format = DateTimePickerFormat.Short, Width = 120 };
            this.Controls.Add(dtpNgay);
            
            Button btnLoad = new Button { Text = "Tải / Tạo ca làm", Location = new Point(150, 70), Width = 120 };
            btnLoad.Click += BtnLoad_Click;
            this.Controls.Add(btnLoad);
            
            Button btnSave = new Button { Text = "Lưu Chấm Công", Location = new Point(280, 70), Width = 120 };
            btnSave.Click += BtnSave_Click;
            this.Controls.Add(btnSave);
            
            Button btnBack = new Button { Text = "Quay Lại", Location = new Point(410, 70), Width = 100 };
            btnBack.Click += (s, e) => { hethong ht = new hethong(); ht.Show(); this.Close(); };
            this.Controls.Add(btnBack);
            
            dgvChamCong = new DataGridView { Location = new Point(20, 110), Width = 760, Height = 350, AllowUserToAddRows = false, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill };
            this.Controls.Add(dgvChamCong);
            
            this.Size = new Size(820, 520);
            
            if (Login.pq != 1)
            {
                btnSave.Enabled = false;
                btnLoad.Enabled = false;
                MessageBox.Show("Chỉ Admin mới có quyền sửa đổi bảng chấm công!");
            }
            
            this.Load += (s, e) => BtnLoad_Click(null, null);
        }

        private void BtnLoad_Click(object sender, EventArgs e)
        {
            string ngay = dtpNgay.Value.ToString("yyyy-MM-dd");
            using (SqlConnection conn = new SqlConnection(strcon))
            {
                conn.Open();
                
                // 1. Get employees assigned to shifts from PhanCa
                string sqlPhanCa = "SELECT p.MaNhanVien, p.MaCa FROM PhanCa p";
                SqlCommand cmdPhanCa = new SqlCommand(sqlPhanCa, conn);
                SqlDataReader dr = cmdPhanCa.ExecuteReader();
                DataTable dtPhanCa = new DataTable();
                dtPhanCa.Load(dr);
                
                // 2. Insert into DiemDanh if not exists
                foreach (DataRow row in dtPhanCa.Rows)
                {
                    string maNV = row["MaNhanVien"].ToString();
                    string maCa = row["MaCa"].ToString();
                    
                    string checkSql = "SELECT 1 FROM DiemDanh WHERE Ngay = @ngay AND MaNhanVien = @maNV AND MaCa = @maCa";
                    SqlCommand cmdCheck = new SqlCommand(checkSql, conn);
                    cmdCheck.Parameters.AddWithValue("@ngay", ngay);
                    cmdCheck.Parameters.AddWithValue("@maNV", maNV);
                    cmdCheck.Parameters.AddWithValue("@maCa", maCa);
                    
                    object exists = cmdCheck.ExecuteScalar();
                    if (exists == null)
                    {
                        string insertSql = "INSERT INTO DiemDanh (Ngay, MaNhanVien, MaCa, TrangThai) VALUES (@ngay, @maNV, @maCa, N'Vắng mặt')";
                        SqlCommand cmdInsert = new SqlCommand(insertSql, conn);
                        cmdInsert.Parameters.AddWithValue("@ngay", ngay);
                        cmdInsert.Parameters.AddWithValue("@maNV", maNV);
                        cmdInsert.Parameters.AddWithValue("@maCa", maCa);
                        cmdInsert.ExecuteNonQuery();
                    }
                }
                
                // 3. Load DiemDanh to DataGridView
                string sqlLoad = @"
                    SELECT d.ID, d.MaNhanVien, n.TenNhanVien, d.MaCa, d.TrangThai, d.GioVao, d.GioRa, d.GhiChu 
                    FROM DiemDanh d 
                    JOIN NhanVien n ON d.MaNhanVien = n.MaNhanVien 
                    WHERE d.Ngay = @ngay";
                SqlCommand cmdLoad = new SqlCommand(sqlLoad, conn);
                cmdLoad.Parameters.AddWithValue("@ngay", ngay);
                
                SqlDataAdapter adapter = new SqlDataAdapter(cmdLoad);
                DataTable dtDiemDanh = new DataTable();
                adapter.Fill(dtDiemDanh);
                
                dgvChamCong.DataSource = dtDiemDanh;
                
                // Format DGV
                dgvChamCong.Columns["ID"].Visible = false;
                dgvChamCong.Columns["MaNhanVien"].ReadOnly = true;
                dgvChamCong.Columns["TenNhanVien"].ReadOnly = true;
                dgvChamCong.Columns["MaCa"].ReadOnly = true;
                
                // Change TrangThai to ComboBox column if not already
                if (dgvChamCong.Columns["TrangThai"] is DataGridViewTextBoxColumn)
                {
                    int idx = dgvChamCong.Columns["TrangThai"].Index;
                    dgvChamCong.Columns.RemoveAt(idx);
                    
                    DataGridViewComboBoxColumn cbCol = new DataGridViewComboBoxColumn();
                    cbCol.Name = "TrangThai";
                    cbCol.HeaderText = "Trạng Thái";
                    cbCol.DataPropertyName = "TrangThai";
                    cbCol.Items.AddRange("Có mặt", "Vắng mặt", "Đi trễ", "Về sớm", "Nghỉ phép");
                    dgvChamCong.Columns.Insert(idx, cbCol);
                }
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (Login.pq != 1) return;
            
            try
            {
                using (SqlConnection conn = new SqlConnection(strcon))
                {
                    conn.Open();
                    foreach (DataGridViewRow row in dgvChamCong.Rows)
                    {
                        if (row.IsNewRow) continue;
                        
                        int id = Convert.ToInt32(row.Cells["ID"].Value);
                        string trangThai = row.Cells["TrangThai"].Value?.ToString() ?? "";
                        
                        // Parse Times
                        object gioVaoObj = DBNull.Value;
                        if (TimeSpan.TryParse(row.Cells["GioVao"].Value?.ToString(), out TimeSpan gv))
                            gioVaoObj = gv;
                            
                        object gioRaObj = DBNull.Value;
                        if (TimeSpan.TryParse(row.Cells["GioRa"].Value?.ToString(), out TimeSpan gr))
                            gioRaObj = gr;
                            
                        string ghiChu = row.Cells["GhiChu"].Value?.ToString() ?? "";
                        
                        string updateSql = "UPDATE DiemDanh SET TrangThai = @tt, GioVao = @gv, GioRa = @gr, GhiChu = @gc WHERE ID = @id";
                        SqlCommand cmd = new SqlCommand(updateSql, conn);
                        cmd.Parameters.AddWithValue("@tt", trangThai);
                        cmd.Parameters.AddWithValue("@gv", gioVaoObj);
                        cmd.Parameters.AddWithValue("@gr", gioRaObj);
                        cmd.Parameters.AddWithValue("@gc", ghiChu);
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Lưu chấm công thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu: " + ex.Message);
            }
        }
        
        // Remove old unused handlers
        private void btntim_Click(object sender, EventArgs e) { }
        private void btntao_Click(object sender, EventArgs e) { }
        private void btnquaylai_Click(object sender, EventArgs e) { }
        private void btnrs_Click(object sender, EventArgs e) { }
        private void btnctcc_Click(object sender, EventArgs e) { }
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e) { }
        private void btntl_Click(object sender, EventArgs e) { }
        private void ChamCong_Load(object sender, EventArgs e) { }
    }
}
