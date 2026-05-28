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
        string strcon = @"Data Source=LAPTOP-HT21K47P\PTG;Initial Catalog=QuanLyQuanCaFe;Integrated Security=True";
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
            loaddata();
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
