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
    public partial class QuanLyKH : Form
    {
        public QuanLyKH()
        {
            InitializeComponent();
            LoadData();
          
        }

        private void LoadData()
        {

            SqlConnection conn = new SqlConnection
         (@"Data Source=LAPTOP-HT21K47P\PTG;Initial Catalog=QuanLyQuanCaFe;Integrated Security=True");

            string sql = "select MaKhachHang as MaKH, TenKhachHang as TenKH, '' as GT, SoDienThoai as SDT, cast(DiemTichLuy as nvarchar(50)) as DC from KhachHang";

            SqlDataAdapter adapt = new SqlDataAdapter(sql, conn);  // adapter : chuyển đổi
            DataSet ds = new DataSet();                 // bộ
            adapt.Fill(ds);                          // fill lấp đầy
          dataGridView1.DataSource = ds.Tables[0];

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection
          (@"Data Source=LAPTOP-HT21K47P\PTG;Initial Catalog=QuanLyQuanCaFe;Integrated Security=True");
                SqlCommand cmd = new SqlCommand(" insert into KhachHang (MaKhachHang, TenKhachHang, SoDienThoai, DiemTichLuy) values ( @ma, @ten, @sdt, @diem)", conn);
                conn.Open();
                cmd.Parameters.AddWithValue("@ma", textBox1.Text);
                cmd.Parameters.AddWithValue("@ten", textBox2.Text);
                cmd.Parameters.AddWithValue("@sdt", textBox3.Text);
                int diem = 0;
                int.TryParse(textBox4.Text, out diem);
                cmd.Parameters.AddWithValue("@diem", diem);
                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("thêm khách hàng thành công");
                LoadData();
                Reset();
            }catch(Exception ex)
            {
                MessageBox.Show("Mã khách hàng đã tồn tại!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection
     (@"Data Source=LAPTOP-HT21K47P\PTG;Initial Catalog=QuanLyQuanCaFe;Integrated Security=True");

            conn.Open();
            SqlCommand cmd = new SqlCommand(" UPDATE KhachHang set TenKhachHang=@ten, SoDienThoai=@sdt, DiemTichLuy=@diem WHERE MaKhachHang=@ma", conn);
            cmd.Parameters.AddWithValue("@ma", textBox1.Text);
            cmd.Parameters.AddWithValue("@ten", textBox2.Text);
            cmd.Parameters.AddWithValue("@sdt", textBox3.Text);
            int diem = 0;
            int.TryParse(textBox4.Text, out diem);
            cmd.Parameters.AddWithValue("@diem", diem);
            cmd.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("đã sửa thông tin khách hàng thành công");
            LoadData();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection
   (@"Data Source=LAPTOP-HT21K47P\PTG;Initial Catalog=QuanLyQuanCaFe;Integrated Security=True");

            string sql = ("select MaKhachHang as MaKH, TenKhachHang as TenKH, '' as GT, SoDienThoai as SDT, cast(DiemTichLuy as nvarchar(50)) as DC from KhachHang WHERE TenKhachHang LIKE '%" + textBox5.Text + "%'");

            SqlDataAdapter adapt = new SqlDataAdapter(sql, conn);
            DataSet ds = new DataSet();
            adapt.Fill(ds, "xyz");
                       dataGridView1.DataSource = ds.Tables["xyz"];

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            int r = dataGridView1.CurrentCell.RowIndex;
            textBox1.Text = dataGridView1.Rows[r].Cells["MaKH"].Value.ToString();
            textBox2.Text = dataGridView1.Rows[r].Cells["TenKH"].Value.ToString();
            textBox9.Text = dataGridView1.Rows[r].Cells["GT"].Value.ToString();
            textBox3.Text = dataGridView1.Rows[r].Cells["SDT"].Value.ToString();
            textBox4.Text = dataGridView1.Rows[r].Cells["DC"].Value.ToString();
            textBox1.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection
     (@"Data Source=LAPTOP-HT21K47P\PTG;Initial Catalog=QuanLyQuanCaFe;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("DELETE FROM KhachHang WHERE MaKhachHang=@manv", conn);
            conn.Open();
            cmd.Parameters.AddWithValue("@manv", textBox1.Text);
            cmd.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("xoá thành công ");
            LoadData();
            Reset();
        }

        private void Reset()
        {

            textBox1.Text = "";
            textBox2.Text ="";
            textBox9.Text = "";
            textBox3.Text ="";
            textBox4.Text = "";
            textBox1.Enabled = true;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Reset();
            textBox5.Text = "";
            LoadData();
        }


        private void button5_Click(object sender, EventArgs e)
        {
            hethong ht = new hethong();
            ht.Show();
            this.Close();
        }

        private void QuanLyKH_Load(object sender, EventArgs e)
        {

        }
    }
}
