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
    public partial class CTHD : Form
    {
        string strcon = @"Data Source=LAPTOP-HT21K47P\PTG;Initial Catalog=QuanLyQuanCaFe;Integrated Security=True";
        SqlConnection sqlcon = null;
        SqlCommand cmd;
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable table = new DataTable();
        public CTHD()
        {
            InitializeComponent();
            loaddata();
        }
        string mahd = QLHD.laymahd;
        void loaddata()
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
            
        }

        private void btnquaylai_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CTHD_Load(object sender, EventArgs e)
        {

        }
    }
}
