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
    public partial class ThongKe : Form
    {
        string strcon = @"Data Source=LAPTOP-HT21K47P\PTG;Initial Catalog=QuanLyQuanCaFe;Integrated Security=True";
        SqlConnection sqlcon = null;
        public ThongKe()
        {
            InitializeComponent();
            txtnamt.Enabled = false;
            cbthang.Enabled = false;
            txtnamq.Enabled = false;
            cbquy.Enabled = false;
            txtnamn.Enabled = false;
        }
        void loaddata(string sql)
        {
            SqlCommand cmd;
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();
            sqlcon = new SqlConnection(strcon);
            sqlcon.Open();
            cmd = sqlcon.CreateCommand();
            cmd.CommandText = sql;
            adapter.SelectCommand = cmd;
            table.Clear();
            adapter.Fill(table);
            dataGridView1.DataSource = table;
            sqlcon.Close();
            double tt = 0;
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                tt = tt + Convert.ToDouble(dataGridView1.Rows[i].Cells[2].Value);
            }
            txttt.Text = tt.ToString();
        }
        int ktrb=0;
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked.Equals(true))
            {
                txtnamt.Enabled = true;
                cbthang.Enabled = true;
                ktrb = 1;
            }
            else
            {
                txtnamt.Enabled = false;
                cbthang.Enabled = false;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked.Equals(true))
            {
                txtnamq.Enabled = true;
                cbquy.Enabled = true;
                ktrb = 2;
            }
            else
            {
                txtnamq.Enabled = false;
                cbquy.Enabled = false;
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked.Equals(true))
            {
                txtnamn.Enabled = true;
                ktrb = 3;
            }
            else
            {
                txtnamn.Enabled = false;
            }
        }

        private void btntk_Click(object sender, EventArgs e)
        {
            if (kt==0)
            {
                if (ktrb==1)
                {
                    loaddata("select ctdn.MaNguyenLieu as 'Mã sản phẩm', sum(ctdn.SoLuong) as 'Số lượng', sum(ctdn.ThanhTien) as 'Đơn giá' from ChiTietHoaDonNhap ctdn join NguyenLieu nl on ctdn.MaNguyenLieu = nl.MaNguyenLieu join HoaDonNhap hdn on ctdn.MaHoaDonNhap = hdn.MaHoaDonNhap where hdn.NgayNhap like '%/" + cbthang.Text + "/" + txtnamt.Text + "%' group by ctdn.MaNguyenLieu");
                }
                else if (ktrb==2)
                {
                    if (cbquy.SelectedIndex.Equals(0))
                    {
                        loaddata("select ctdn.MaNguyenLieu as 'Mã sản phẩm', sum(ctdn.SoLuong) as 'Số lượng', sum(ctdn.ThanhTien) as 'Đơn giá' from ChiTietHoaDonNhap ctdn join NguyenLieu nl on ctdn.MaNguyenLieu = nl.MaNguyenLieu join HoaDonNhap hdn on ctdn.MaHoaDonNhap = hdn.MaHoaDonNhap where (hdn.NgayNhap like '%/1/" + txtnamq.Text + "%' or hdn.NgayNhap like '%/2/" + txtnamq.Text + "%' or hdn.NgayNhap like '%/3/" + txtnamq.Text + "%') group by ctdn.MaNguyenLieu");
                    }
                    else if (cbquy.SelectedIndex.Equals(1))
                    {
                        loaddata("select ctdn.MaNguyenLieu as 'Mã sản phẩm', sum(ctdn.SoLuong) as 'Số lượng', sum(ctdn.ThanhTien) as 'Đơn giá' from ChiTietHoaDonNhap ctdn join NguyenLieu nl on ctdn.MaNguyenLieu = nl.MaNguyenLieu join HoaDonNhap hdn on ctdn.MaHoaDonNhap = hdn.MaHoaDonNhap where (hdn.NgayNhap like '%/4/" + txtnamq.Text + "%' or hdn.NgayNhap like '%/5/" + txtnamq.Text + "%' or hdn.NgayNhap like '%/6/" + txtnamq.Text + "%') group by ctdn.MaNguyenLieu");
                    }
                    else if (cbquy.SelectedIndex.Equals(2))
                    {
                        loaddata("select ctdn.MaNguyenLieu as 'Mã sản phẩm', sum(ctdn.SoLuong) as 'Số lượng', sum(ctdn.ThanhTien) as 'Đơn giá' from ChiTietHoaDonNhap ctdn join NguyenLieu nl on ctdn.MaNguyenLieu = nl.MaNguyenLieu join HoaDonNhap hdn on ctdn.MaHoaDonNhap = hdn.MaHoaDonNhap where (hdn.NgayNhap like '%/7/" + txtnamq.Text + "%' or hdn.NgayNhap like '%/8/" + txtnamq.Text + "%' or hdn.NgayNhap like '%/9/" + txtnamq.Text + "%') group by ctdn.MaNguyenLieu");
                    }
                    else if (cbquy.SelectedIndex.Equals(3))
                    {
                        loaddata("select ctdn.MaNguyenLieu as 'Mã sản phẩm', sum(ctdn.SoLuong) as 'Số lượng', sum(ctdn.ThanhTien) as 'Đơn giá' from ChiTietHoaDonNhap ctdn join NguyenLieu nl on ctdn.MaNguyenLieu = nl.MaNguyenLieu join HoaDonNhap hdn on ctdn.MaHoaDonNhap = hdn.MaHoaDonNhap where (hdn.NgayNhap like '%/10/" + txtnamq.Text + "%' or hdn.NgayNhap like '%/11/" + txtnamq.Text + "%' or hdn.NgayNhap like '%/12/" + txtnamq.Text + "%') group by ctdn.MaNguyenLieu");
                    }
                }
                else if (ktrb==3)
                {
                    loaddata("select ctdn.MaNguyenLieu as 'Mã sản phẩm', sum(ctdn.SoLuong) as 'Số lượng', sum(ctdn.ThanhTien) as 'Đơn giá' from ChiTietHoaDonNhap ctdn join NguyenLieu nl on ctdn.MaNguyenLieu = nl.MaNguyenLieu join HoaDonNhap hdn on ctdn.MaHoaDonNhap = hdn.MaHoaDonNhap where hdn.NgayNhap like '%" + txtnamn.Text + "%' group by ctdn.MaNguyenLieu");
                }
                else
                {
                    MessageBox.Show("Yêu cầu chọn kiểu thống kê!");
                }
            }
            else if (kt==1)
            {
                if (ktrb == 1)
                {
                    loaddata("select ct.MaSanPham as 'Mã sản phẩm', sum(ct.SoLuong) as 'Số lượng', sum(ct.ThanhTien) as 'Đơn giá' from ChiTietHoaDonBan ct join SanPham sp on ct.MaSanPham = sp.MaSanPham join HoaDonBan hdb on ct.MaHoaDonBan = hdb.MaHoaDonBan where hdb.NgayLap like '%/" + cbthang.Text + "/" + txtnamt.Text + "%' group by ct.MaSanPham");
                }
                else if (ktrb == 2)
                {
                    if (cbquy.SelectedIndex.Equals(0))
                    {
                        loaddata("select ct.MaSanPham as 'Mã sản phẩm', sum(ct.SoLuong) as 'Số lượng', sum(ct.ThanhTien) as 'Đơn giá' from ChiTietHoaDonBan ct join SanPham sp on ct.MaSanPham = sp.MaSanPham join HoaDonBan hdb on ct.MaHoaDonBan = hdb.MaHoaDonBan where (hdb.NgayLap like '%1/" + txtnamq.Text + "%' or hdb.NgayLap like '%2/" + txtnamq.Text + "%' or hdb.NgayLap like '%3/" + txtnamq.Text + "%') group by ct.MaSanPham");
                    }
                    else if (cbquy.SelectedIndex.Equals(1))
                    {
                        loaddata("select ct.MaSanPham as 'Mã sản phẩm', sum(ct.SoLuong) as 'Số lượng', sum(ct.ThanhTien) as 'Đơn giá' from ChiTietHoaDonBan ct join SanPham sp on ct.MaSanPham = sp.MaSanPham join HoaDonBan hdb on ct.MaHoaDonBan = hdb.MaHoaDonBan where (hdb.NgayLap like '%4/" + txtnamq.Text + "%' or hdb.NgayLap like '%5/" + txtnamq.Text + "%' or hdb.NgayLap like '%6/" + txtnamq.Text + "%') group by ct.MaSanPham");
                    }
                    else if (cbquy.SelectedIndex.Equals(2))
                    {
                        loaddata("select ct.MaSanPham as 'Mã sản phẩm', sum(ct.SoLuong) as 'Số lượng', sum(ct.ThanhTien) as 'Đơn giá' from ChiTietHoaDonBan ct join SanPham sp on ct.MaSanPham = sp.MaSanPham join HoaDonBan hdb on ct.MaHoaDonBan = hdb.MaHoaDonBan where (hdb.NgayLap like '%7/" + txtnamq.Text + "%' or hdb.NgayLap like '%8/" + txtnamq.Text + "%' or hdb.NgayLap like '%9/" + txtnamq.Text + "%') group by ct.MaSanPham");
                    }
                    else if (cbquy.SelectedIndex.Equals(3))
                    {
                        loaddata("select ct.MaSanPham as 'Mã sản phẩm', sum(ct.SoLuong) as 'Số lượng', sum(ct.ThanhTien) as 'Đơn giá' from ChiTietHoaDonBan ct join SanPham sp on ct.MaSanPham = sp.MaSanPham join HoaDonBan hdb on ct.MaHoaDonBan = hdb.MaHoaDonBan where (hdb.NgayLap like '%10/" + txtnamq.Text + "%' or hdb.NgayLap like '%11/" + txtnamq.Text + "%' or hdb.NgayLap like '%12/" + txtnamq.Text + "%') group by ct.MaSanPham");
                    }
                }
                else if (ktrb == 3)
                {
                    loaddata("select ct.MaSanPham as 'Mã sản phẩm', sum(ct.SoLuong) as 'Số lượng', sum(ct.ThanhTien) as 'Đơn giá' from ChiTietHoaDonBan ct join SanPham sp on ct.MaSanPham = sp.MaSanPham join HoaDonBan hdb on ct.MaHoaDonBan = hdb.MaHoaDonBan where hdb.NgayLap like '%/" + txtnamn.Text + "%' group by ct.MaSanPham");
                }
                else
                {
                    MessageBox.Show("Yêu cầu chọn kiểu thống kê!");
                }
            }
            else
            {
                MessageBox.Show("Yêu cầu chọn loại thống kê!");
            }
        }

        private void btntim_Click(object sender, EventArgs e)
        {
            if (kt == 0)
            {
                if (ktrb == 1)
                {
                    loaddata("select ctdn.MaNguyenLieu as 'Mã sản phẩm', sum(ctdn.SoLuong) as 'Số lượng', sum(ctdn.ThanhTien) as 'Đơn giá' from ChiTietHoaDonNhap ctdn join NguyenLieu nl on ctdn.MaNguyenLieu = nl.MaNguyenLieu join HoaDonNhap hdn on ctdn.MaHoaDonNhap = hdn.MaHoaDonNhap where hdn.NgayNhap like '%/" + cbthang.Text + "/" + txtnamt.Text + "%' and ctdn.MaNguyenLieu ='" + cbtimkiem.Text + "' group by ctdn.MaNguyenLieu");
                }
                else if (ktrb == 2)
                {
                    if (cbquy.SelectedIndex.Equals(0))
                    {
                        loaddata("select ctdn.MaNguyenLieu as 'Mã sản phẩm', sum(ctdn.SoLuong) as 'Số lượng', sum(ctdn.ThanhTien) as 'Đơn giá' from ChiTietHoaDonNhap ctdn join NguyenLieu nl on ctdn.MaNguyenLieu = nl.MaNguyenLieu join HoaDonNhap hdn on ctdn.MaHoaDonNhap = hdn.MaHoaDonNhap where (hdn.NgayNhap like '%/1/" + txtnamq.Text + "%' or hdn.NgayNhap like '%/2/" + txtnamq.Text + "%' or hdn.NgayNhap like '%/3/" + txtnamq.Text + "%') and ctdn.MaNguyenLieu ='" + cbtimkiem.Text + "' group by ctdn.MaNguyenLieu");
                    }
                    else if (cbquy.SelectedIndex.Equals(1))
                    {
                        loaddata("select ctdn.MaNguyenLieu as 'Mã sản phẩm', sum(ctdn.SoLuong) as 'Số lượng', sum(ctdn.ThanhTien) as 'Đơn giá' from ChiTietHoaDonNhap ctdn join NguyenLieu nl on ctdn.MaNguyenLieu = nl.MaNguyenLieu join HoaDonNhap hdn on ctdn.MaHoaDonNhap = hdn.MaHoaDonNhap where (hdn.NgayNhap like '%/4/" + txtnamq.Text + "%' or hdn.NgayNhap like '%/5/" + txtnamq.Text + "%' or hdn.NgayNhap like '%/6/" + txtnamq.Text + "%') and ctdn.MaNguyenLieu ='" + cbtimkiem.Text + "' group by ctdn.MaNguyenLieu");
                    }
                    else if (cbquy.SelectedIndex.Equals(2))
                    {
                        loaddata("select ctdn.MaNguyenLieu as 'Mã sản phẩm', sum(ctdn.SoLuong) as 'Số lượng', sum(ctdn.ThanhTien) as 'Đơn giá' from ChiTietHoaDonNhap ctdn join NguyenLieu nl on ctdn.MaNguyenLieu = nl.MaNguyenLieu join HoaDonNhap hdn on ctdn.MaHoaDonNhap = hdn.MaHoaDonNhap where (hdn.NgayNhap like '%/7/" + txtnamq.Text + "%' or hdn.NgayNhap like '%/8/" + txtnamq.Text + "%' or hdn.NgayNhap like '%/9/" + txtnamq.Text + "%') and ctdn.MaNguyenLieu ='" + cbtimkiem.Text + "' group by ctdn.MaNguyenLieu");
                    }
                    else if (cbquy.SelectedIndex.Equals(3))
                    {
                        loaddata("select ctdn.MaNguyenLieu as 'Mã sản phẩm', sum(ctdn.SoLuong) as 'Số lượng', sum(ctdn.ThanhTien) as 'Đơn giá' from ChiTietHoaDonNhap ctdn join NguyenLieu nl on ctdn.MaNguyenLieu = nl.MaNguyenLieu join HoaDonNhap hdn on ctdn.MaHoaDonNhap = hdn.MaHoaDonNhap where (hdn.NgayNhap like '%/10/" + txtnamq.Text + "%' or hdn.NgayNhap like '%/11/" + txtnamq.Text + "%' or hdn.NgayNhap like '%/12/" + txtnamq.Text + "%') and ctdn.MaNguyenLieu ='" + cbtimkiem.Text + "' group by ctdn.MaNguyenLieu");
                    }
                }
                else if (ktrb == 3)
                {
                    loaddata("select ctdn.MaNguyenLieu as 'Mã sản phẩm', sum(ctdn.SoLuong) as 'Số lượng', sum(ctdn.ThanhTien) as 'Đơn giá' from ChiTietHoaDonNhap ctdn join NguyenLieu nl on ctdn.MaNguyenLieu = nl.MaNguyenLieu join HoaDonNhap hdn on ctdn.MaHoaDonNhap = hdn.MaHoaDonNhap where hdn.NgayNhap like '%" + txtnamn.Text + "%' and ctdn.MaNguyenLieu ='" + cbtimkiem.Text + "' group by ctdn.MaNguyenLieu");
                }
                else
                {
                    MessageBox.Show("Yêu cầu chọn kiểu thống kê!");
                }
            }
            else if (kt == 1)
            {
                if (ktrb == 1)
                {
                    loaddata("select ct.MaSanPham as 'Mã sản phẩm', sum(ct.SoLuong) as 'Số lượng', sum(ct.ThanhTien) as 'Đơn giá' from ChiTietHoaDonBan ct join SanPham sp on ct.MaSanPham = sp.MaSanPham join HoaDonBan hdb on ct.MaHoaDonBan = hdb.MaHoaDonBan where hdb.NgayLap like '%/" + cbthang.Text + "/" + txtnamt.Text + "%' and ct.MaSanPham ='" + cbtimkiem.Text + "' group by ct.MaSanPham");
                }
                else if (ktrb == 2)
                {
                    if (cbquy.SelectedIndex.Equals(0))
                    {
                        loaddata("select ct.MaSanPham as 'Mã sản phẩm', sum(ct.SoLuong) as 'Số lượng', sum(ct.ThanhTien) as 'Đơn giá' from ChiTietHoaDonBan ct join SanPham sp on ct.MaSanPham = sp.MaSanPham join HoaDonBan hdb on ct.MaHoaDonBan = hdb.MaHoaDonBan where (hdb.NgayLap like '%1/" + txtnamq.Text + "%' or hdb.NgayLap like '%2/" + txtnamq.Text + "%' or hdb.NgayLap like '%3/" + txtnamq.Text + "%') and ct.MaSanPham ='" + cbtimkiem.Text + "' group by ct.MaSanPham");
                    }
                    else if (cbquy.SelectedIndex.Equals(1))
                    {
                        loaddata("select ct.MaSanPham as 'Mã sản phẩm', sum(ct.SoLuong) as 'Số lượng', sum(ct.ThanhTien) as 'Đơn giá' from ChiTietHoaDonBan ct join SanPham sp on ct.MaSanPham = sp.MaSanPham join HoaDonBan hdb on ct.MaHoaDonBan = hdb.MaHoaDonBan where (hdb.NgayLap like '%4/" + txtnamq.Text + "%' or hdb.NgayLap like '%5/" + txtnamq.Text + "%' or hdb.NgayLap like '%6/" + txtnamq.Text + "%') and ct.MaSanPham ='" + cbtimkiem.Text + "' group by ct.MaSanPham");
                    }
                    else if (cbquy.SelectedIndex.Equals(2))
                    {
                        loaddata("select ct.MaSanPham as 'Mã sản phẩm', sum(ct.SoLuong) as 'Số lượng', sum(ct.ThanhTien) as 'Đơn giá' from ChiTietHoaDonBan ct join SanPham sp on ct.MaSanPham = sp.MaSanPham join HoaDonBan hdb on ct.MaHoaDonBan = hdb.MaHoaDonBan where (hdb.NgayLap like '%7/" + txtnamq.Text + "%' or hdb.NgayLap like '%8/" + txtnamq.Text + "%' or hdb.NgayLap like '%9/" + txtnamq.Text + "%') and ct.MaSanPham ='" + cbtimkiem.Text + "' group by ct.MaSanPham");
                    }
                    else if (cbquy.SelectedIndex.Equals(3))
                    {
                        loaddata("select ct.MaSanPham as 'Mã sản phẩm', sum(ct.SoLuong) as 'Số lượng', sum(ct.ThanhTien) as 'Đơn giá' from ChiTietHoaDonBan ct join SanPham sp on ct.MaSanPham = sp.MaSanPham join HoaDonBan hdb on ct.MaHoaDonBan = hdb.MaHoaDonBan where (hdb.NgayLap like '%10/" + txtnamq.Text + "%' or hdb.NgayLap like '%11/" + txtnamq.Text + "%' or hdb.NgayLap like '%12/" + txtnamq.Text + "%') and ct.MaSanPham ='" + cbtimkiem.Text + "' group by ct.MaSanPham");
                    }
                }
                else if (ktrb == 3)
                {
                    loaddata("select ct.MaSanPham as 'Mã sản phẩm', sum(ct.SoLuong) as 'Số lượng', sum(ct.ThanhTien) as 'Đơn giá' from ChiTietHoaDonBan ct join SanPham sp on ct.MaSanPham = sp.MaSanPham join HoaDonBan hdb on ct.MaHoaDonBan = hdb.MaHoaDonBan where hdb.NgayLap like '%/" + txtnamn.Text + "%' and ct.MaSanPham ='" + cbtimkiem.Text + "' group by ct.MaSanPham");
                }
                else
                {
                    MessageBox.Show("Yêu cầu chọn kiểu thống kê!");
                }
            }
            else
            {
                MessageBox.Show("Yêu cầu chọn loại thống kê!");
            }
        }
        int kt = 2;
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex.Equals(0))
            {
                kt = 0;
            }
            else if (comboBox1.SelectedIndex.Equals(1))
            {
                kt = 1;
            }
        }

        private void ThongKe_Load(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(strcon))
                {
                    // For searching, we load product IDs from SanPham
                    SqlDataAdapter da = new SqlDataAdapter("select MaSanPham from SanPham", conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    cbtimkiem.DataSource = dt;
                    cbtimkiem.DisplayMember = "MaSanPham";
                    cbtimkiem.ValueMember = "MaSanPham";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnthoat_Click(object sender, EventArgs e)
        {
            hethong ht = new hethong();
            ht.Show();
            this.Close();
        }
    }
}
