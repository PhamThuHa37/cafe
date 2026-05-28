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
        string strcon = @"Data Source=LAPTOP-HT21K47P\PTG;Initial Catalog=QuanLyQuanCaFe;Integrated Security=True";
        SqlConnection sqlcon = null;
        SqlCommand cmd;
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable table = new DataTable();
        public ChiTietCC()
        {
            InitializeComponent();
            loaddata();
            DateTime tg = DateTime.Now;
            int t = tg.Month;
            int n = tg.Year;
            getThang();
            if (t != thang && n != nam || t != thang && n == nam)
            {
                btncc.Enabled = false;
                btnkt.Enabled = false;
            }
        }
        int thang;
        int nam;
        private void getThang()
        {
            sqlcon = new SqlConnection(strcon);
            sqlcon.Open();
            cmd = sqlcon.CreateCommand();
            cmd.CommandText = "select Thang,Nam from QLBCC where MaBCC='" + mabcc + "'";
            adapter.SelectCommand = cmd;
            DataTable tabt = new DataTable();
            tabt.Clear();
            adapter.Fill(tabt);
            thang = tabt.Rows[0].Field<int>(0);
            nam = tabt.Rows[0].Field<int>(1);
            sqlcon.Close();
        }
        string manv = Login.nv;
        string mabcc = ChamCong.ma;
        void loaddata()
        {
            sqlcon = new SqlConnection(strcon);
            sqlcon.Open();
            cmd = sqlcon.CreateCommand();
            cmd.CommandText = "select MaNV,TenNV,NgayCC,GioVao,GioRa from CTBCC where MaNV='" + manv + "' and MaBCC='" + mabcc + "'";
            adapter.SelectCommand = cmd;
            table.Clear();
            adapter.Fill(table);
            dataGridView1.DataSource = table;
            sqlcon.Close();
        }

        private void btnthoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private string getTen()
        {
            sqlcon = new SqlConnection(strcon);
            sqlcon.Open();
            cmd = sqlcon.CreateCommand();
            cmd.CommandText = "select TenNhanVien from NhanVien where MaNhanVien='" + manv + "'";
            adapter.SelectCommand = cmd;
            DataTable tab = new DataTable();
            tab.Clear();
            adapter.Fill(tab);
            string tennv = tab.Rows[0].Field<string>(0);
            sqlcon.Close();
            return tennv;
        }
            
        private int kiemtra(string ncc)
        {
            int kt = 0;
            sqlcon = new SqlConnection(strcon);
            sqlcon.Open();
            cmd = sqlcon.CreateCommand();
            cmd.CommandText = "select GioRa from CTBCC where NgayCC = '" + ncc + "' and MaNV = '" + manv + "'";
            adapter.SelectCommand = cmd;
            DataTable tab = new DataTable();
            tab.Clear();
            adapter.Fill(tab);
            string gr = tab.Rows[0].Field<string>(0);
            if (gr == "")
                kt = 1;
            sqlcon.Close();
            return kt;
        }

        private void btnkt_Click(object sender, EventArgs e)
        {
            DateTime time = DateTime.Now;
            string ncc = time.Day + "/" + time.Month + "/" + time.Year;
            string gkt = time.Hour + ":" + time.Minute + "." + time.Second;
            sqlcon = new SqlConnection(strcon);
            sqlcon.Open();
            cmd = sqlcon.CreateCommand();
            cmd.CommandText = "select * from CTBCC where NgayCC = '" + ncc + "' and MaNV = '" + manv + "'";
            adapter.SelectCommand = cmd;
            DataTable tab = new DataTable();
            tab.Clear();
            adapter.Fill(tab);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                string gr = tab.Rows[0].Field<string>(0);

                int k = kiemtra(ncc);
                if (k == 1)
                {
                    sqlcon = new SqlConnection(strcon);
                    sqlcon.Open();
                    string sua = "update CTBCC set GioRa='" + gkt + "' where NgayCC='" + ncc + "' and MaNV = '" + manv + "'";
                    SqlCommand cmdsua = new SqlCommand(sua, sqlcon);
                    cmdsua.ExecuteNonQuery();
                    loaddata();
                    sqlcon.Close();
                    
                }
                else
                {
                    MessageBox.Show("Hôm nay kết thúc chấm công rồi!");
                }
            }
            else
            {
                MessageBox.Show("Hôm nay chưa chấm công!");
            }
            sqlcon.Close();
        }
        string qr;
        private void btncc_Click(object sender, EventArgs e)
        {
            string tennv = getTen();
            DateTime time = DateTime.Now;
            string ncc = time.Day + "/" + time.Month + "/" + time.Year;
            string gcc = time.Hour + ":" + time.Minute + "." + time.Second;
            sqlcon = new SqlConnection(strcon);
            sqlcon.Open();
            qr = "select MaNV,TenNV,NgayCC,GioVao,GioRa from CTBCC where NgayCC='" + ncc + "' and MaNV='" + manv + "'";
            SqlCommand command = new SqlCommand(qr, sqlcon);
            SqlDataReader dr = command.ExecuteReader();
            if (dr.HasRows)
            {
                MessageBox.Show("Hôm nay đã chấm công rồi!");
                sqlcon.Close();
            }
            else
            {
                sqlcon.Close();
                sqlcon.Open();
                qr = "insert into CTBCC values('" + manv + "','" + tennv + "','" + mabcc + "','" + ncc + "','" + gcc + "','')";
                command = new SqlCommand(qr, sqlcon);
                command.ExecuteNonQuery();
                loaddata();
                sqlcon.Close();
            }
        }

        private void ChiTietCC_Load(object sender, EventArgs e)
        {

        }
    }
}
