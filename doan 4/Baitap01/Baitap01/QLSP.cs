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
        string strcon = @"Data Source=LAPTOP-HT21K47P\PTG;Initial Catalog=QuanLyQuanCaFe;Integrated Security=True";
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
            loaddata();
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
            sqlcon = new SqlConnection(strcon);
            sqlcon.Open();
            cmd = sqlcon.CreateCommand();
            cmd.CommandText = "select MaSanPham AS MaSP, TenSanPham AS TenSP, MaLoaiSanPham AS MaLSP, MaCongThuc AS MaNCC, GiaBan AS Gia from SanPham where MaSanPham= '" + txttk.Text + "' or TenSanPham like '%" + txttk.Text + "%'";
            adapter.SelectCommand = cmd;
            table.Clear();
            adapter.Fill(table);
            dataGridView1.DataSource = table;
            sqlcon.Close();
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
