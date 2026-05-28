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
    public partial class ChucVu : Form
    {
        public ChucVu()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {

            SqlConnection conn = new SqlConnection
         (@"Data Source=LAPTOP-HT21K47P\PTG;Initial Catalog=QuanLyQuanCaFe;Integrated Security=True");

            string sql = "SELECT MaChucVu AS MaCV, TenChucVu AS TenCV, 1.0 AS HSL FROM ChucVu";

            SqlDataAdapter adapt = new SqlDataAdapter(sql, conn);  // adapter : chuyển đổi
            DataSet ds = new DataSet();                 // bộ
            adapt.Fill(ds);                          // fill lấp đầy
            dataGridView1.DataSource = ds.Tables[0];

        }

        private void button5_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(@"Data Source=LAPTOP-HT21K47P\PTG;Initial Catalog=QuanLyQuanCaFe;Integrated Security=True");


            string sql = ("SELECT MaChucVu AS MaCV, TenChucVu AS TenCV, 1.0 AS HSL FROM ChucVu WHERE TenChucVu LIKE '%" + textBox4.Text + "%'");

            SqlDataAdapter adapt = new SqlDataAdapter(sql, conn);
            DataSet ds = new DataSet();
            adapt.Fill(ds, "xyz");
            dataGridView1.DataSource = ds.Tables["xyz"];
        }

        private void button6_Click(object sender, EventArgs e)
        {
            hethong ht = new hethong();
            ht.Show();
            this.Close();
        }
        private void Reset()
        {
            textBox1.Enabled = true;
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
           
        }
        private void button4_Click(object sender, EventArgs e)
        {

            Reset();
            LoadData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection
   (@"Data Source=LAPTOP-HT21K47P\PTG;Initial Catalog=QuanLyQuanCaFe;Integrated Security=True");
            SqlCommand cmd = new SqlCommand(" insert into ChucVu (MaChucVu, TenChucVu) values ( @mcv, @ten)", conn);
            conn.Open();
            cmd.Parameters.AddWithValue("@mcv", textBox1.Text);
            cmd.Parameters.AddWithValue("@ten", textBox2.Text);
            cmd.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("thêm  thành công");
            LoadData();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection
 (@"Data Source=LAPTOP-HT21K47P\PTG;Initial Catalog=QuanLyQuanCaFe;Integrated Security=True");

            conn.Open();
            SqlCommand cmd = new SqlCommand(" UPDATE ChucVu set TenChucVu=@ten WHERE MaChucVu=@mcv", conn);
            cmd.Parameters.AddWithValue("@ten", textBox2.Text);
            cmd.Parameters.AddWithValue("@mcv", textBox1.Text);
            cmd.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("đã sửa thành công");

            LoadData();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection
  (@"Data Source=LAPTOP-HT21K47P\PTG;Initial Catalog=QuanLyQuanCaFe;Integrated Security=True");



            SqlCommand cmd = new SqlCommand("DELETE FROM ChucVu WHERE MaChucVu=@manv", conn);
            conn.Open();
            cmd.Parameters.AddWithValue("@manv", textBox1.Text);
            cmd.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("xoá thành công ");
            LoadData();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = dataGridView1.CurrentCell.RowIndex;

            textBox1.Enabled = false;
            textBox1.Text = dataGridView1.Rows[r].Cells["MaCV"].Value.ToString();
            textBox2.Text = dataGridView1.Rows[r].Cells["TenCV"].Value.ToString();
            textBox3.Text = dataGridView1.Rows[r].Cells["HSL"].Value.ToString();
         
            
        }

        private void ChucVu_Load(object sender, EventArgs e)
        {

        }
    }
}
