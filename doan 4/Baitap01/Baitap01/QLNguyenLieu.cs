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
    public partial class QLNguyenLieu : Form
    {
        string strcon = @"Data Source=HUONGLT\SQLEXPRESS;Initial Catalog=QuanLyQuanCaFe;Integrated Security=True";
        SqlConnection sqlcon = null;
        SqlCommand cmd;
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable table = new DataTable();
        void loaddata()
        {
            sqlcon = new SqlConnection(strcon);
            sqlcon.Open();
            cmd = sqlcon.CreateCommand();
            cmd.CommandText = "select MaNguyenLieu as MaLSP, TenNguyenLieu as TenLSP from NguyenLieu";
            adapter.SelectCommand = cmd;
            table.Clear();
            adapter.Fill(table);
            dataGridView1.DataSource = table;
            sqlcon.Close();
        }
        public QLNguyenLieu()
        {
            InitializeComponent();
            Baitap01.ThemeManager.ApplyTheme(this);
            loaddata();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                sqlcon = new SqlConnection(strcon);
                sqlcon.Open();
                string qr = "insert into NguyenLieu (MaNguyenLieu, TenNguyenLieu) values('" + txtmnl.Text + "',N'" + txttnl.Text + "')";
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
                string sua = " update NguyenLieu set TenNguyenLieu =N'" + txttnl.Text + "' where MaNguyenLieu='" + txtmnl.Text + "'";
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
            txtmnl.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txttnl.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtmnl.Enabled = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                sqlcon = new SqlConnection(strcon);
                sqlcon.Open();
                string xoa = "delete from NguyenLieu where MaNguyenLieu='" + txtmnl.Text + "'";
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
                txttnl.ResetText();
                txtmnl.Enabled = true;
                txtmnl.ResetText();
                txttk.ResetText();
                sqlcon = new SqlConnection(strcon);
                sqlcon.Open();
                cmd = sqlcon.CreateCommand();
                cmd.CommandText = "select MaNguyenLieu as MaLSP, TenNguyenLieu as TenLSP from NguyenLieu";
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
                cmd.CommandText = "select MaNguyenLieu as MaLSP, TenNguyenLieu as TenLSP from NguyenLieu where TenNguyenLieu like '%"+txttk.Text+"%'";
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

        private void QLNguyenLieu_Load(object sender, EventArgs e)
        {

        }
    }
}
