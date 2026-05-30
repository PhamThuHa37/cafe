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
    public partial class QLLSP : Form
    {
        string strcon = DBConnect.strcon;
        SqlConnection sqlcon = null;
        SqlCommand cmd;
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable table = new DataTable();
        void loaddata()
        {
            sqlcon = new SqlConnection(strcon);
            sqlcon.Open();
            cmd = sqlcon.CreateCommand();
            cmd.CommandText = "select MaLoaiSanPham as MaLSP, TenLoaiSanPham as TenLSP from LoaiSanPham";
            adapter.SelectCommand = cmd;
            table.Clear();
            adapter.Fill(table);
            dataGridView1.DataSource = table;
            sqlcon.Close();
        }
        public QLLSP()
        {
            InitializeComponent();
            StyleForm();
            loaddata();
            if (Login.pq != 1)
            {
                button2.Enabled = false; // Thêm
                button3.Enabled = false; // Sửa
                button4.Enabled = false; // Xóa
            }
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
            label1.ForeColor = darkEspresso;
            label1.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            label1.Text = "QUẢN LÝ LOẠI SẢN PHẨM";

            // Labels
            label3.ForeColor = brownLabel;
            label3.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            label2.ForeColor = brownLabel;
            label2.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);

            // TextBoxes
            txtmlsp.BackColor = creamyInput;
            txtmlsp.BorderStyle = BorderStyle.FixedSingle;
            txtmlsp.ForeColor = Color.FromArgb(60, 45, 35);
            
            txtlsp.BackColor = creamyInput;
            txtlsp.BorderStyle = BorderStyle.FixedSingle;
            txtlsp.ForeColor = Color.FromArgb(60, 45, 35);
            
            txttk.BackColor = creamyInput;
            txttk.BorderStyle = BorderStyle.FixedSingle;
            txttk.ForeColor = Color.FromArgb(60, 45, 35);

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
            // button2: Thêm
            button2.FlatStyle = FlatStyle.Flat;
            button2.FlatAppearance.BorderSize = 0;
            button2.BackColor = Color.FromArgb(120, 90, 70);
            button2.ForeColor = Color.White;
            button2.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            button2.Cursor = Cursors.Hand;

            // button3: Sửa
            button3.FlatStyle = FlatStyle.Flat;
            button3.FlatAppearance.BorderSize = 0;
            button3.BackColor = Color.FromArgb(185, 135, 95);
            button3.ForeColor = Color.White;
            button3.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            button3.Cursor = Cursors.Hand;

            // button4: Xóa
            button4.FlatStyle = FlatStyle.Flat;
            button4.FlatAppearance.BorderSize = 0;
            button4.BackColor = Color.FromArgb(180, 80, 70);
            button4.ForeColor = Color.White;
            button4.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            button4.Cursor = Cursors.Hand;

            // button1: Reset (Làm mới)
            button1.FlatStyle = FlatStyle.Flat;
            button1.FlatAppearance.BorderSize = 0;
            button1.BackColor = Color.FromArgb(160, 150, 140);
            button1.ForeColor = Color.White;
            button1.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            button1.Cursor = Cursors.Hand;

            // button6: Tìm kiếm
            button6.FlatStyle = FlatStyle.Flat;
            button6.FlatAppearance.BorderSize = 0;
            button6.BackColor = Color.FromArgb(120, 90, 70);
            button6.ForeColor = Color.White;
            button6.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            button6.Cursor = Cursors.Hand;

            // button5: Thoát (Quay lại)
            button5.FlatStyle = FlatStyle.Flat;
            button5.FlatAppearance.BorderSize = 0;
            button5.BackColor = Color.FromArgb(185, 175, 165);
            button5.ForeColor = Color.White;
            button5.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            button5.Cursor = Cursors.Hand;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                sqlcon = new SqlConnection(strcon);
                sqlcon.Open();
                string qr = "insert into LoaiSanPham (MaLoaiSanPham, TenLoaiSanPham) values('" + txtmlsp.Text + "',N'" + txtlsp.Text + "')";
                SqlCommand qrcmd = new SqlCommand(qr, sqlcon);
                qrcmd.ExecuteNonQuery();
                loaddata();
                sqlcon.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            hethong ht = new hethong();
            ht.Show();
            this.Close();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                sqlcon = new SqlConnection(strcon);
                sqlcon.Open();
                string sua = " update LoaiSanPham set TenLoaiSanPham =N'" + txtlsp.Text + "' where MaLoaiSanPham='" + txtmlsp.Text + "'";
                SqlCommand cmdsua = new SqlCommand(sua, sqlcon);
                cmdsua.ExecuteNonQuery();
                loaddata();
                sqlcon.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            txtmlsp.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtlsp.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtmlsp.Enabled = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                sqlcon = new SqlConnection(strcon);
                sqlcon.Open();
                string xoa = "delete from LoaiSanPham where MaLoaiSanPham='" + txtmlsp.Text + "'";
                SqlCommand cmdxoa = new SqlCommand(xoa, sqlcon);
                cmdxoa.ExecuteNonQuery();
                loaddata();
                sqlcon.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                txtlsp.ResetText();
                txtmlsp.Enabled = true;
                txtmlsp.ResetText();
                txttk.ResetText();
                sqlcon = new SqlConnection(strcon);
                sqlcon.Open();
                cmd = sqlcon.CreateCommand();
                cmd.CommandText = "select MaLoaiSanPham as MaLSP, TenLoaiSanPham as TenLSP from LoaiSanPham";
                adapter.SelectCommand = cmd;
                table.Clear();
                adapter.Fill(table);
                dataGridView1.DataSource = table;
                sqlcon.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                sqlcon = new SqlConnection(strcon);
                sqlcon.Open();
                cmd = sqlcon.CreateCommand();
                cmd.CommandText = "select MaLoaiSanPham as MaLSP, TenLoaiSanPham as TenLSP from LoaiSanPham where TenLoaiSanPham like '%"+txttk.Text+"%'";
                adapter.SelectCommand = cmd;
                table.Clear();
                adapter.Fill(table);
                dataGridView1.DataSource = table;
                sqlcon.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void QLLSP_Load(object sender, EventArgs e)
        {

        }
    }
}
