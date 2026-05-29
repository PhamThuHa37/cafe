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
    public partial class ChiTietPN : Form
    {
        string strcon = @"Data Source=HUONGLT\SQLEXPRESS;Initial Catalog=QuanLyQuanCaFe;Integrated Security=True";
        SqlConnection sqlcon = null;
        SqlCommand cmd;
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable table = new DataTable();
        string mapn = QLPhieuNhap.mapn;
        //string setvt;
        void loaddata()
        {
            sqlcon = new SqlConnection(strcon);
            sqlcon.Open();
            cmd = sqlcon.CreateCommand();
            cmd.CommandText = "select ctdn.MaNguyenLieu as MaSP, nl.TenNguyenLieu as TenSP, hdn.MaNhaCungCap as MaNCC, ncc.TenNhaCungCap as TenNCC, ctdn.SoLuong, ctdn.ThanhTien as DonGia from ChiTietHoaDonNhap ctdn join NguyenLieu nl on ctdn.MaNguyenLieu = nl.MaNguyenLieu join HoaDonNhap hdn on ctdn.MaHoaDonNhap = hdn.MaHoaDonNhap join NhaCungCap ncc on hdn.MaNhaCungCap = ncc.MaNhaCungCap where ctdn.MaHoaDonNhap = '" + mapn + "'";
            adapter.SelectCommand = cmd;
            table.Clear();
            adapter.Fill(table);
            dataGridView1.DataSource = table;
            sqlcon.Close();
            tt = 0;
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                tt = tt + Convert.ToDouble(dataGridView1.Rows[i].Cells[5].Value);
            }
        }
        double tt;
        
        void uptt()
        {
            try
            {
                DBConnect.updateData(" update HoaDonNhap set TongTienNhap=" + tt + " where MaHoaDonNhap='" + mpn.Text + "'");
                loaddata();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public ChiTietPN()
        {
            InitializeComponent();
            Baitap01.ThemeManager.ApplyTheme(this);
            loaddata();
            mpn.Text = mapn;
            mpn.Enabled = false;
        }
        double tinh()
        {
            SqlCommand cmd;
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();
            sqlcon = new SqlConnection(strcon);
            sqlcon.Open();
            cmd = sqlcon.CreateCommand();
            cmd.CommandText = "select (GiaNhap*" + sl.Text + ") as TTien from NguyenLieu where MaNguyenLieu='" + msp.SelectedValue.ToString() + "'";
            adapter.SelectCommand = cmd;
            table.Clear();
            adapter.Fill(table);
            double t = Convert.ToDouble(table.Rows[0][0]);
            return t;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                double t = tinh();
                DBConnect.updateData("insert into ChiTietHoaDonNhap (MaHoaDonNhap, MaNguyenLieu, SoLuong, DonGia, ThanhTien) values('" + mpn.Text + "','" + msp.SelectedValue.ToString() + "', " + sl.Text + ", (select GiaNhap from NguyenLieu where MaNguyenLieu='" + msp.SelectedValue.ToString() + "'), " + t + ")");
                loaddata();
                uptt();
                dg.Text = t.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                double t = tinh();
                DBConnect.updateData(" update ChiTietHoaDonNhap set SoLuong=" + sl.Text + ", DonGia=(select GiaNhap from NguyenLieu where MaNguyenLieu='" + msp.SelectedValue.ToString() + "'), ThanhTien=" + t + " where MaHoaDonNhap='" + mpn.Text + "' and MaNguyenLieu='" + msp.SelectedValue.ToString() + "'");
                loaddata();
                uptt();
                dg.Text = t.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            msp.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            msp.Enabled = false;
            mncc.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            sl.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            dg.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                DBConnect.updateData("delete from ChiTietHoaDonNhap where MaHoaDonNhap='" + mpn.Text + "' and MaNguyenLieu='" + msp.SelectedValue.ToString() + "'");
                loaddata();
                uptt();
                rst();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void rst()
        {
            msp.ResetText();
            msp.Enabled = true;
            sl.ResetText();
            mncc.ResetText();
            dg.ResetText();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            rst();
            loaddata();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            sqlcon = new SqlConnection(strcon);
            sqlcon.Open();
            cmd = sqlcon.CreateCommand();
            cmd.CommandText = "select ctdn.MaNguyenLieu as MaSP, nl.TenNguyenLieu as TenSP, hdn.MaNhaCungCap as MaNCC, ncc.TenNhaCungCap as TenNCC, ctdn.SoLuong, ctdn.ThanhTien as DonGia from ChiTietHoaDonNhap ctdn join NguyenLieu nl on ctdn.MaNguyenLieu = nl.MaNguyenLieu join HoaDonNhap hdn on ctdn.MaHoaDonNhap = hdn.MaHoaDonNhap join NhaCungCap ncc on hdn.MaNhaCungCap = ncc.MaNhaCungCap where ctdn.MaHoaDonNhap = '" + mapn + "' and ctdn.MaNguyenLieu='" + timkiem.Text + "'";
            adapter.SelectCommand = cmd;
            table.Clear();
            adapter.Fill(table);
            dataGridView1.DataSource = table;
            sqlcon.Close();
        }

        private void ChiTietPN_Load(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(strcon))
                {
                    SqlDataAdapter da1 = new SqlDataAdapter("select MaNguyenLieu, TenNguyenLieu from NguyenLieu", conn);
                    DataTable dt1 = new DataTable();
                    da1.Fill(dt1);
                    msp.DataSource = dt1;
                    msp.DisplayMember = "TenNguyenLieu";
                    msp.ValueMember = "MaNguyenLieu";

                    SqlDataAdapter da2 = new SqlDataAdapter("select MaNhaCungCap, TenNhaCungCap from NhaCungCap", conn);
                    DataTable dt2 = new DataTable();
                    da2.Fill(dt2);
                    mncc.DataSource = dt2;
                    mncc.DisplayMember = "TenNhaCungCap";
                    mncc.ValueMember = "MaNhaCungCap";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
