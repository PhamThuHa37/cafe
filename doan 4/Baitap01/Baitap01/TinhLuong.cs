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
    public partial class TinhLuong : Form
    {
        string strcon = @"Data Source=HUONGLT\SQLEXPRESS;Initial Catalog=QuanLyQuanCaFe;Integrated Security=True";
        SqlConnection sqlcon = null;
        SqlCommand cmd;
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable table = new DataTable();
        public TinhLuong()
        {
            InitializeComponent();
            Baitap01.ThemeManager.ApplyTheme(this);
            laynv();

        }
        private void laynv()
        {
            
            sqlcon = new SqlConnection(strcon);
            sqlcon.Open();
            cmd = sqlcon.CreateCommand();
            cmd.CommandText = "select * from NhanVien";
            adapter.SelectCommand = cmd;
            table.Clear();
            adapter.Fill(table);
            comboBox1.DataSource = table;
            comboBox1.DisplayMember = "MaNhanVien";
            comboBox1.ValueMember = "MaNhanVien";
            sqlcon.Close();
        }
        void loaddata()
        {
            string bcc = laybcc();
            sqlcon = new SqlConnection(strcon);
            sqlcon.Open();
            cmd = sqlcon.CreateCommand();
            cmd.CommandText = "select Count(CTBCC.NgayCC) as SoCong,Count(CTBCC.NgayCC)*300000 as Luong from CTBCC where MaBCC = '" + bcc + "' and MaNV='" + manv + "'";
            adapter.SelectCommand = cmd;
            DataTable tab = new DataTable();
            tab.Clear();
            adapter.Fill(tab);
            dataGridView1.DataSource = tab;
            sqlcon.Close();
        }
        
        string manv;
        string mabcc;
        private string laybcc()
        {
            sqlcon = new SqlConnection(strcon);
            sqlcon.Open();
            cmd = sqlcon.CreateCommand();
            cmd.CommandText = "select MaBCC from QLBCC where Thang='" + cbthang.SelectedItem + "' and Nam='"+txtnam.Text+"'";
            adapter.SelectCommand = cmd;
            DataTable tab = new DataTable();
            tab.Clear();
            adapter.Fill(tab);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                mabcc = tab.Rows[0].Field<string>(0);
            }
            sqlcon.Close();
            return mabcc;
        }
        private void btnthoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            manv = cb.SelectedValue.ToString();
            // getTen();
            //getluong();
            loaddata();
        }

        private void TinhLuong_Load(object sender, EventArgs e)
        {

        }
    }
}
