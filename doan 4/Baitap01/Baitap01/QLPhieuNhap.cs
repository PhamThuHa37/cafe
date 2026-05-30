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
    public partial class QLPhieuNhap : Form
    {
         string strcon = DBConnect.strcon;
        SqlConnection sqlcon = null;
        SqlCommand cmd;
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable table = new DataTable();
        //string setvt;
        void loaddata()
        {
            sqlcon = new SqlConnection(strcon);
            sqlcon.Open();
            cmd = sqlcon.CreateCommand();
            cmd.CommandText = "select MaHoaDonNhap as MaPN, '2026-05-28' as NgayNhap, MaNhanVien as MaNV, TongTienNhap as TongTien from HoaDonNhap";
            adapter.SelectCommand = cmd;
            table.Clear();
            adapter.Fill(table);
            dataGridView1.DataSource = table;
            sqlcon.Close();
        }
        public QLPhieuNhap()
        {
            InitializeComponent();
            loaddata();
            ngay();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                DBConnect.updateData("insert into HoaDonNhap (MaHoaDonNhap, TongTienNhap, MaNhanVien, MaNhaCungCap) values('" + mpn.Text + "', " + (string.IsNullOrEmpty(tt.Text) ? "0" : tt.Text) + ", '" + mnv.SelectedValue.ToString() + "', 'NCC01')");                
                loaddata();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void ngay()
        {
            DateTime dt = DateTime.Now;
            nn.Text = dt.Day + "/" + dt.Month + "/" + dt.Year;
            nn.Enabled = false;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                DBConnect.updateData(" update HoaDonNhap set MaNhanVien='" + mnv.SelectedValue.ToString() + "',TongTienNhap=" + (string.IsNullOrEmpty(tt.Text) ? "0" : tt.Text) + " where MaHoaDonNhap='" + mpn.Text + "'");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                DBConnect.updateData("delete from HoaDonNhap where MaHoaDonNhap='" + mpn.Text + "'");
               
                loaddata();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            sqlcon = new SqlConnection(strcon);
            sqlcon.Open();
            cmd = sqlcon.CreateCommand();
            cmd.CommandText = "select MaHoaDonNhap as MaPN, '2026-05-28' as NgayNhap, MaNhanVien as MaNV, TongTienNhap as TongTien from HoaDonNhap where MaHoaDonNhap ='" + timkiem.Text + "'";
            adapter.SelectCommand = cmd;
            table.Clear();
            adapter.Fill(table);
            dataGridView1.DataSource = table;
            sqlcon.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            mpn.ResetText();
            mpn.Enabled = true;
            ngay();
            mnv.ResetText();
            tt.ResetText();

            sqlcon = new SqlConnection(strcon);
            sqlcon.Open();
            cmd = sqlcon.CreateCommand();
            cmd.CommandText = "select MaHoaDonNhap as MaPN, '2026-05-28' as NgayNhap, MaNhanVien as MaNV, TongTienNhap as TongTien from HoaDonNhap" ;
            adapter.SelectCommand = cmd;
            table.Clear();
            adapter.Fill(table);
            dataGridView1.DataSource = table;
            sqlcon.Close();
        }
        public static string mapn;
        private void dataGridView1_Click(object sender, EventArgs e)
        {
            mpn.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            mapn = mpn.Text;
            mpn.Enabled = false;
            nn.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            mnv.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            tt.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            hethong ht = new hethong();
            ht.Show();
            this.Close();
        }

        private void btnchitiet_Click(object sender, EventArgs e)
        {
            ChiTietPN ct = new ChiTietPN();
            ct.Show();
        }

        private void QLPhieuNhap_Load(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(strcon))
                {
                    SqlDataAdapter da = new SqlDataAdapter("select MaNhanVien, TenNhanVien from NhanVien", conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    mnv.DataSource = dt;
                    mnv.DisplayMember = "MaNhanVien";
                    mnv.ValueMember = "MaNhanVien";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
