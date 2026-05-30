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
    public partial class ChiTietCC : Form
    {
        private string strcon = DBConnect.strcon;
        private string manv = Login.nv;
        private string mabcc = ChamCong.ma;
        private int thang;
        private int nam;

        public ChiTietCC()
        {
            InitializeComponent();
            loaddata();
            
            DateTime tg = DateTime.Now;
            int t = tg.Month;
            int n = tg.Year;
            
            getThang();
            
            if ((t != thang && n != nam) || (t != thang && n == nam))
            {
                btncc.Enabled = false;
                btnkt.Enabled = false;
            }
        }

        private void getThang()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(strcon))
                {
                    string sql = "SELECT Thang, Nam FROM QLBCC WHERE MaBCC = @mabcc";
                    using (SqlCommand command = new SqlCommand(sql, conn))
                    {
                        command.Parameters.AddWithValue("@mabcc", mabcc ?? "");
                        conn.Open();
                        using (SqlDataReader dr = command.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                thang = Convert.ToInt32(dr["Thang"]);
                                nam = Convert.ToInt32(dr["Nam"]);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lấy thông tin tháng chấm công: " + ex.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void loaddata()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(strcon))
                {
                    string sql = "SELECT MaNV, TenNV, NgayCC, GioVao, GioRa FROM CTBCC WHERE MaNV = @manv AND MaBCC = @mabcc";
                    using (SqlDataAdapter da = new SqlDataAdapter(sql, conn))
                    {
                        da.SelectCommand.Parameters.AddWithValue("@manv", manv ?? "");
                        da.SelectCommand.Parameters.AddWithValue("@mabcc", mabcc ?? "");
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dataGridView1.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi nạp dữ liệu: " + ex.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnthoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private string getTen()
        {
            string tennv = "";
            try
            {
                using (SqlConnection conn = new SqlConnection(strcon))
                {
                    string sql = "SELECT TenNhanVien FROM NhanVien WHERE MaNhanVien = @manv";
                    using (SqlCommand command = new SqlCommand(sql, conn))
                    {
                        command.Parameters.AddWithValue("@manv", manv ?? "");
                        conn.Open();
                        object res = command.ExecuteScalar();
                        if (res != null)
                        {
                            tennv = res.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lấy tên nhân viên: " + ex.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return tennv;
        }

        // Nút Kết thúc Chấm công (Check-out)
        private void btnkt_Click(object sender, EventArgs e)
        {
            DateTime time = DateTime.Now;
            string ncc = time.Day + "/" + time.Month + "/" + time.Year;
            string gkt = time.Hour + ":" + time.Minute + "." + time.Second;

            try
            {
                using (SqlConnection conn = new SqlConnection(strcon))
                {
                    string sqlCheck = "SELECT GioRa FROM CTBCC WHERE NgayCC = @ngay AND MaNV = @manv";
                    using (SqlCommand cmdCheck = new SqlCommand(sqlCheck, conn))
                    {
                        cmdCheck.Parameters.AddWithValue("@ngay", ncc);
                        cmdCheck.Parameters.AddWithValue("@manv", manv ?? "");

                        conn.Open();
                        using (SqlDataReader dr = cmdCheck.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                string gioRa = dr["GioRa"]?.ToString() ?? "";
                                dr.Close(); // Đóng reader trước khi thực hiện update

                                if (string.IsNullOrEmpty(gioRa))
                                {
                                    string sqlUpdate = "UPDATE CTBCC SET GioRa = @giora WHERE NgayCC = @ngay AND MaNV = @manv";
                                    using (SqlCommand cmdUpdate = new SqlCommand(sqlUpdate, conn))
                                    {
                                        cmdUpdate.Parameters.AddWithValue("@giora", gkt);
                                        cmdUpdate.Parameters.AddWithValue("@ngay", ncc);
                                        cmdUpdate.Parameters.AddWithValue("@manv", manv ?? "");
                                        cmdUpdate.ExecuteNonQuery();
                                    }
                                    MessageBox.Show("Chấm công ra thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    loaddata();
                                }
                                else
                                {
                                    MessageBox.Show("Hôm nay kết thúc chấm công rồi!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Hôm nay chưa chấm công vào!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi chấm công ra: " + ex.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Nút Chấm công Vào (Check-in)
        private void btncc_Click(object sender, EventArgs e)
        {
            string tennv = getTen();
            DateTime time = DateTime.Now;
            string ncc = time.Day + "/" + time.Month + "/" + time.Year;
            string gcc = time.Hour + ":" + time.Minute + "." + time.Second;

            try
            {
                using (SqlConnection conn = new SqlConnection(strcon))
                {
                    string sqlCheck = "SELECT COUNT(*) FROM CTBCC WHERE NgayCC = @ngay AND MaNV = @manv";
                    using (SqlCommand cmdCheck = new SqlCommand(sqlCheck, conn))
                    {
                        cmdCheck.Parameters.AddWithValue("@ngay", ncc);
                        cmdCheck.Parameters.AddWithValue("@manv", manv ?? "");

                        conn.Open();
                        int count = (int)cmdCheck.ExecuteScalar();
                        if (count > 0)
                        {
                            MessageBox.Show("Hôm nay đã chấm công rồi!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            string sqlInsert = "INSERT INTO CTBCC (MaNV, TenNV, MaBCC, NgayCC, GioVao, GioRa) VALUES (@manv, @tennv, @mabcc, @ngay, @giovao, '')";
                            using (SqlCommand cmdInsert = new SqlCommand(sqlInsert, conn))
                            {
                                cmdInsert.Parameters.AddWithValue("@manv", manv ?? "");
                                cmdInsert.Parameters.AddWithValue("@tennv", tennv);
                                cmdInsert.Parameters.AddWithValue("@mabcc", mabcc ?? "");
                                cmdInsert.Parameters.AddWithValue("@ngay", ncc);
                                cmdInsert.Parameters.AddWithValue("@giovao", gcc);
                                cmdInsert.ExecuteNonQuery();
                            }
                            MessageBox.Show("Chấm công vào thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            loaddata();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi chấm công vào: " + ex.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ChiTietCC_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
