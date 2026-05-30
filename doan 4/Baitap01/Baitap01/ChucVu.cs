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
        private string strConnect = DBConnect.strcon;

        public ChucVu()
        {
            InitializeComponent();
            StyleForm();
            LoadData();
        }

        private void StyleForm()
        {
            Color beigeBg = Color.FromArgb(245, 240, 235);
            Color darkEspresso = Color.FromArgb(90, 65, 50);
            Color brownLabel = Color.FromArgb(100, 75, 60);
            Color creamyInput = Color.FromArgb(252, 250, 248);

            this.BackColor = beigeBg;
            this.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));

            // Form Title
            label6.ForeColor = darkEspresso;
            label6.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            label6.Text = "QUẢN LÝ CHỨC VỤ";

            // Labels
            label1.ForeColor = brownLabel;
            label1.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            label2.ForeColor = brownLabel;
            label2.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            label3.ForeColor = brownLabel;
            label3.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);

            // GroupBox
            groupBox1.BackColor = beigeBg;
            groupBox1.ForeColor = darkEspresso;
            groupBox1.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);

            // TextBoxes
            textBox1.BackColor = creamyInput;
            textBox1.BorderStyle = BorderStyle.FixedSingle;
            textBox1.ForeColor = Color.FromArgb(60, 45, 35);
            textBox2.BackColor = creamyInput;
            textBox2.BorderStyle = BorderStyle.FixedSingle;
            textBox2.ForeColor = Color.FromArgb(60, 45, 35);
            textBox3.BackColor = creamyInput;
            textBox3.BorderStyle = BorderStyle.FixedSingle;
            textBox3.ForeColor = Color.FromArgb(60, 45, 35);
            textBox4.BackColor = creamyInput;
            textBox4.BorderStyle = BorderStyle.FixedSingle;
            textBox4.ForeColor = Color.FromArgb(60, 45, 35);

            // GridView
            dataGridView1.BackgroundColor = Color.White;
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 245, 242);
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.GridColor = Color.FromArgb(230, 220, 210);
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(120, 90, 70);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.RowTemplate.Height = 32;

            // Buttons styling
            // button1: Thêm
            button1.FlatStyle = FlatStyle.Flat;
            button1.FlatAppearance.BorderSize = 0;
            button1.BackColor = Color.FromArgb(120, 90, 70);
            button1.ForeColor = Color.White;
            button1.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            button1.Cursor = Cursors.Hand;

            // button2: Sửa
            button2.FlatStyle = FlatStyle.Flat;
            button2.FlatAppearance.BorderSize = 0;
            button2.BackColor = Color.FromArgb(185, 135, 95);
            button2.ForeColor = Color.White;
            button2.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            button2.Cursor = Cursors.Hand;

            // button3: Xóa
            button3.FlatStyle = FlatStyle.Flat;
            button3.FlatAppearance.BorderSize = 0;
            button3.BackColor = Color.FromArgb(180, 80, 70);
            button3.ForeColor = Color.White;
            button3.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            button3.Cursor = Cursors.Hand;

            // button4: Reset (Làm mới)
            button4.FlatStyle = FlatStyle.Flat;
            button4.FlatAppearance.BorderSize = 0;
            button4.BackColor = Color.FromArgb(160, 150, 140);
            button4.ForeColor = Color.White;
            button4.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            button4.Cursor = Cursors.Hand;

            // button5: Tìm kiếm
            button5.FlatStyle = FlatStyle.Flat;
            button5.FlatAppearance.BorderSize = 0;
            button5.BackColor = Color.FromArgb(120, 90, 70);
            button5.ForeColor = Color.White;
            button5.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            button5.Cursor = Cursors.Hand;

            // button6: Thoát (Quay lại)
            button6.FlatStyle = FlatStyle.Flat;
            button6.FlatAppearance.BorderSize = 0;
            button6.BackColor = Color.FromArgb(185, 175, 165);
            button6.ForeColor = Color.White;
            button6.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            button6.Cursor = Cursors.Hand;
        }

        private void LoadData()
        {

            SqlConnection conn = new SqlConnection(strConnect);

            string sql = "SELECT MaChucVu AS MaCV, TenChucVu AS TenCV, 1.0 AS HSL FROM ChucVu";

            SqlDataAdapter adapt = new SqlDataAdapter(sql, conn);  // adapter : chuyển đổi
            DataSet ds = new DataSet();                 // bộ
            adapt.Fill(ds);                          // fill lấp đầy
            dataGridView1.DataSource = ds.Tables[0];

        }

        private void button5_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(strConnect);


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
            SqlConnection conn = new SqlConnection(strConnect);
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
            SqlConnection conn = new SqlConnection(strConnect);

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
            SqlConnection conn = new SqlConnection(strConnect);



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
