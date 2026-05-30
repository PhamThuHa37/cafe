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
    public partial class QLSP : Form
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
            cmd.CommandText = "select MaSanPham AS MaSP, TenSanPham AS TenSP, MaLoaiSanPham AS MaLSP, MaCongThuc AS MaNCC, GiaBan AS Gia from SanPham";
            adapter.SelectCommand = cmd;
            table.Clear();
            adapter.Fill(table);
            dataGridView1.DataSource = table;
            sqlcon.Close();
        }
        public QLSP()
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

            // Labels
            label1.ForeColor = brownLabel;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label2.ForeColor = brownLabel;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label3.ForeColor = brownLabel;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label4.ForeColor = brownLabel;
            label4.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label5.ForeColor = brownLabel;
            label5.Font = new Font("Segoe UI", 9F, FontStyle.Bold);

            // GroupBox
            groupBox1.BackColor = beigeBg;
            groupBox1.ForeColor = darkEspresso;
            groupBox1.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);

            // TextBoxes / ComboBoxes
            txtmsp.BackColor = creamyInput;
            txtmsp.BorderStyle = BorderStyle.FixedSingle;
            txtmsp.ForeColor = Color.FromArgb(60, 45, 35);
            
            txttsp.BackColor = creamyInput;
            txttsp.BorderStyle = BorderStyle.FixedSingle;
            txttsp.ForeColor = Color.FromArgb(60, 45, 35);
            
            txtlsp.BackColor = creamyInput;
            txtlsp.ForeColor = Color.FromArgb(60, 45, 35);
            
            txtmncc.BackColor = creamyInput;
            txtmncc.ForeColor = Color.FromArgb(60, 45, 35);
            
            txtg.BackColor = creamyInput;
            txtg.BorderStyle = BorderStyle.FixedSingle;
            txtg.ForeColor = Color.FromArgb(60, 45, 35);
            
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

            // btnthoat: Thoát (Quay lại)
            btnthoat.FlatStyle = FlatStyle.Flat;
            btnthoat.FlatAppearance.BorderSize = 0;
            btnthoat.BackColor = Color.FromArgb(185, 175, 165);
            btnthoat.ForeColor = Color.White;
            btnthoat.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnthoat.Cursor = Cursors.Hand;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void QLLSP_Load(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(strcon))
                {
                    SqlDataAdapter da1 = new SqlDataAdapter("select MaLoaiSanPham, TenLoaiSanPham from LoaiSanPham", conn);
                    DataTable dt1 = new DataTable();
                    da1.Fill(dt1);
                    txtlsp.DataSource = dt1;
                    txtlsp.DisplayMember = "TenLoaiSanPham";
                    txtlsp.ValueMember = "MaLoaiSanPham";

                    SqlDataAdapter da2 = new SqlDataAdapter("select MaCongThuc, TenCongThuc from CongThuc", conn);
                    DataTable dt2 = new DataTable();
                    da2.Fill(dt2);
                    txtmncc.DataSource = dt2;
                    txtmncc.DisplayMember = "TenCongThuc";
                    txtmncc.ValueMember = "MaCongThuc";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
                try
                {
                    sqlcon = new SqlConnection(strcon);
                    sqlcon.Open();
                    string qr = "insert into SanPham (MaSanPham, TenSanPham, MaLoaiSanPham, MaCongThuc, GiaBan, HinhAnh) values('" + txtmsp.Text + "',N'" + txttsp.Text + "','" + txtlsp.SelectedValue.ToString() + "','" + txtmncc.SelectedValue.ToString() + "'," + txtg.Text + ",'')";
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

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(strcon))
                {
                    conn.Open();
                    string safeTim = txttk.Text.Trim();
                    string sql = "select sp.MaSanPham AS MaSP, sp.TenSanPham AS TenSP, sp.MaLoaiSanPham AS MaLSP, sp.MaCongThuc AS MaNCC, sp.GiaBan AS Gia " +
                                 "from SanPham sp left join LoaiSanPham lsp on sp.MaLoaiSanPham = lsp.MaLoaiSanPham " +
                                 "where sp.MaSanPham = @tim or sp.TenSanPham like @timLike or lsp.TenLoaiSanPham like @timLike";
                    using (SqlCommand command = new SqlCommand(sql, conn))
                    {
                        command.Parameters.AddWithValue("@tim", safeTim);
                        command.Parameters.AddWithValue("@timLike", "%" + safeTim + "%");
                        using (SqlDataAdapter adapt = new SqlDataAdapter(command))
                        {
                            DataTable dt = new DataTable();
                            adapt.Fill(dt);
                            dataGridView1.DataSource = dt;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm sản phẩm: " + ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                sqlcon = new SqlConnection(strcon);
                sqlcon.Open();
                string sua = " update SanPham set TenSanPham =N'" + txttsp.Text + "',MaLoaiSanPham='" + txtlsp.SelectedValue.ToString() + "',MaCongThuc='" + txtmncc.SelectedValue.ToString() + "',GiaBan=" + txtg.Text + " where MaSanPham = '" + txtmsp.Text + "'";
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

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                sqlcon = new SqlConnection(strcon);
                sqlcon.Open();
                string xoa = "delete from SanPham where MaSanPham='" + txtmsp.Text + "'";
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
                txtmncc.ResetText();
                txtlsp.ResetText();
                txttsp.ResetText();
                txtg.ResetText();
                txtmsp.ResetText();
                txttk.ResetText();
                txtmsp.Enabled = true;
                sqlcon = new SqlConnection(strcon);
                sqlcon.Open();
                cmd = sqlcon.CreateCommand();
                cmd.CommandText = "select MaSanPham AS MaSP, TenSanPham AS TenSP, MaLoaiSanPham AS MaLSP, MaCongThuc AS MaNCC, GiaBan AS Gia from SanPham";
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

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            txtmsp.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtmsp.Enabled = false;
            txttsp.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtlsp.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtmncc.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            txtg.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
        }

        private void btnthoat_Click(object sender, EventArgs e)
        {
            hethong ht = new hethong();
            ht.Show();
            this.Close();
        }
    }
}
