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
    public partial class LapHD : Form
    {
        string strcon = @"Data Source=LAPTOP-HT21K47P\PTG;Initial Catalog=QuanLyQuanCaFe;Integrated Security=True";
        SqlConnection sqlcon = null;
        SqlCommand cmd;
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable table = new DataTable();
        public LapHD()
        {
            InitializeComponent();
            DateTime dt = DateTime.Now;
            txtngay.Text = dt.Day + "/" + dt.Month + "/" + dt.Year;
            txtmahd.Text = mahd;
        }
        string mahd = QLHD.laymahd;
        private void LoadData()
        {
            sqlcon = new SqlConnection(strcon);
            sqlcon.Open();
            cmd = sqlcon.CreateCommand();
            cmd.CommandText = "select ct.MaSanPham as MaSP, sp.TenSanPham as TenSP, ct.SoLuong, ct.ThanhTien as DonGia from ChiTietHoaDonBan ct join SanPham sp on ct.MaSanPham = sp.MaSanPham where ct.MaHoaDonBan = '" + mahd + "'";
            adapter.SelectCommand = cmd;
            table.Clear();
            adapter.Fill(table);
            dataGridView1.DataSource = table;
            sqlcon.Close();
            double tt = 0;
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                tt = tt + Convert.ToDouble(dataGridView1.Rows[i].Cells[3].Value);
            }
            txttt.Text = tt.ToString();
        }

        private void btnthoat_Click(object sender, EventArgs e)
        {
            DBConnect.updateData("delete from ChiTietHoaDonBan where MaHoaDonBan='" + txtmahd.Text + "'");
            
            DBConnect.updateData("delete from HoaDonBan where MaHoaDonBan='" + txtmahd.Text + "'");
            
            this.Close();
        }


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = dataGridView1.CurrentCell.RowIndex;
            cbmsp.Text = dataGridView1.Rows[r].Cells["TenSP"].Value.ToString();
            cbmsp.Enabled = false;
            txtsl.Text = dataGridView1.Rows[r].Cells["SoLuong"].Value.ToString();
            txtdg.Text = dataGridView1.Rows[r].Cells["DonGia"].Value.ToString();
        }
        string tensp;
        float gia;
        void getTen()
        {
            sqlcon = new SqlConnection(strcon);
            sqlcon.Open();
            cmd = sqlcon.CreateCommand();
            cmd.CommandText = "select TenSanPham from SanPham where MaSanPham='" + cbmsp.SelectedValue.ToString() + "'";
            adapter.SelectCommand = cmd;
            table.Clear();
            adapter.Fill(table);
            tensp = table.Rows[0].Field<string>(0);
            sqlcon.Close();
        }
        void tinh()
        {
            sqlcon = new SqlConnection(strcon);
            sqlcon.Open();
            SqlCommand command = sqlcon.CreateCommand();
            command.CommandText = "select TenSanPham, GiaBan from SanPham where MaSanPham='" + cbmsp.SelectedValue.ToString() + "'";
            adapter.SelectCommand = command;
            table.Clear();
            adapter.Fill(table);
            tensp = table.Rows[0].Field<string>(0);
            gia = Convert.ToSingle(table.Rows[0][1]);
            sqlcon.Close();
        }
        void tinhtien()
        {
            sqlcon = new SqlConnection(strcon);
            DataTable tb = new DataTable();
            sqlcon.Open();
            cmd = sqlcon.CreateCommand();
            cmd.CommandText = "select sum(ThanhTien) as TongTien from ChiTietHoaDonBan where MaHoaDonBan='" + mahd + "'";
            adapter.SelectCommand = cmd;
            tb.Clear();
            adapter.Fill(tb);
            sqlcon.Close();
            double tt = 0;
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                tt = tt + Convert.ToDouble(dataGridView1.Rows[i].Cells[0].Value);

            }
            txttt.Text = tt.ToString();
        }
        private void btnthem_Click(object sender, EventArgs e)
        {
            if (cbmsp.Text == "")
                MessageBox.Show("Chưa chọn sản phẩm cần thêm!");
            else if (txtsl.Text == "")
                MessageBox.Show("Chưa có số lượng!");
            else if (Convert.ToInt32(txtsl.Text) <= 0)
                MessageBox.Show("Số lượng phải lớn hơn 0!");
            else
            {
                bool c = checksp(cbmsp.SelectedValue.ToString());
                if (c == true)
                    MessageBox.Show("Sản phẩm này đã được thêm rồi!");
                else
                {
                    DBConnect.updateData("insert into ChiTietHoaDonBan (MaHoaDonBan, MaSanPham, SoLuong, DonGia, ThanhTien, GhiChu) values ('" + mahd + "','" + cbmsp.SelectedValue.ToString() + "', " + txtsl.Text + ", (select GiaBan from SanPham where MaSanPham='" + cbmsp.SelectedValue.ToString() + "'), (" + txtsl.Text + " * (select GiaBan from SanPham where MaSanPham='" + cbmsp.SelectedValue.ToString() + "')), N'')");
                    LoadData();
                    Reset();
                }
            }
        }
        Boolean checksp(string sp)
        {
            sqlcon = new SqlConnection(strcon);
            sqlcon.Open();
            string qr = "select MaSanPham from ChiTietHoaDonBan where MaHoaDonBan='" + txtmahd.Text + "' and MaSanPham='" + sp + "'";
            SqlCommand command = new SqlCommand(qr, sqlcon);
            SqlDataReader dr = command.ExecuteReader();
            if (dr.HasRows)
            {
                sqlcon.Close();
                return true;
            }
            else
            {
                sqlcon.Close();
                return false;
            }
        }
        private void btnsua_Click(object sender, EventArgs e)
        {
            if (cbmsp.Text == "")
                MessageBox.Show("Chưa chọn sản phẩm cần sửa!");
            else if(txtsl.Text == "")
                MessageBox.Show("Chưa có số lượng!");
            else if (Convert.ToInt32(txtsl.Text) <= 0)
                MessageBox.Show("Số lượng phải lớn hơn 0!");
            else
            {
                DBConnect.updateData("update ChiTietHoaDonBan set SoLuong=" + txtsl.Text + ", ThanhTien=(" + txtsl.Text + " * (select GiaBan from SanPham where MaSanPham='" + cbmsp.SelectedValue.ToString() + "')) where MaHoaDonBan='" + txtmahd.Text + "' and MaSanPham='" + cbmsp.SelectedValue.ToString() + "'");
                LoadData();
                Reset();
            }
        }

        private void btnxoa_Click(object sender, EventArgs e)
        {
            if (cbmsp.Text == "")
                MessageBox.Show("Chưa chọn sản phẩm cần xoá!");
            else
            {
                DBConnect.updateData("delete from ChiTietHoaDonBan where MaHoaDonBan='" + txtmahd.Text + "' and MaSanPham='" + cbmsp.SelectedValue.ToString() + "'");
                LoadData();
                Reset();
            }
        }
        private void Reset()
        {
            cbmsp.Enabled = true;
            cbmsp.Text = "";
            txtsl.Text = "";
            txtdg.Text = "";
        }
        private void btnLM_Click(object sender, EventArgs e)
        {
            Reset();
            LoadData();
        }

        private void btnlaphd_Click(object sender, EventArgs e)
        {
            DBConnect.updateData("update HoaDonBan set MaKhachHang='" + cbmakh.SelectedValue.ToString() + "', TrangThai=N'Đã thanh toán', TongTienBan=" + (string.IsNullOrEmpty(txttt.Text) ? "0" : txttt.Text) + " where MaHoaDonBan='" + txtmahd.Text + "'");
            this.Close();
        }

        private void LapHD_Load(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(strcon))
                {
                    SqlDataAdapter da1 = new SqlDataAdapter("select MaKhachHang, TenKhachHang from KhachHang", conn);
                    DataTable dt1 = new DataTable();
                    da1.Fill(dt1);
                    cbmakh.DataSource = dt1;
                    cbmakh.DisplayMember = "TenKhachHang";
                    cbmakh.ValueMember = "MaKhachHang";

                    SqlDataAdapter da2 = new SqlDataAdapter("select MaSanPham, TenSanPham from SanPham", conn);
                    DataTable dt2 = new DataTable();
                    da2.Fill(dt2);
                    cbmsp.DataSource = dt2;
                    cbmsp.DisplayMember = "TenSanPham";
                    cbmsp.ValueMember = "MaSanPham";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
