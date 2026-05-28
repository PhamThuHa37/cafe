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
    public partial class QLNCC : Form
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
            cmd.CommandText = "select MaNhaCungCap as MaNCC, TenNhaCungCap as TenNCC, '' as DiaChi, '' as SDT from NhaCungCap";
            adapter.SelectCommand = cmd;
            table.Clear();
            adapter.Fill(table);
            dataGridView1.DataSource = table;
            sqlcon.Close();
        }
        public QLNCC()
        {
            InitializeComponent();
            loaddata();
        }

        private void QLNCC_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            {
                try {
                    DBConnect.updateData("insert into NhaCungCap (MaNhaCungCap, TenNhaCungCap) values('" + txtmncc.Text + "',N'" + txttncc.Text + "')");
                    loaddata();
                }catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                DBConnect.updateData(" update NhaCungCap set TenNhaCungCap =N'" + txttncc.Text + "' where MaNhaCungCap = '" + txtmncc.Text + "'");
                loaddata();
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try {
                DBConnect.updateData("delete from NhaCungCap where MaNhaCungCap='" + txtmncc.Text + "'");
                loaddata();
            }catch(Exception ex)
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
                cmd = sqlcon.CreateCommand();
                cmd.CommandText = "select MaNhaCungCap as MaNCC, TenNhaCungCap as TenNCC, '' as DiaChi, '' as SDT from NhaCungCap where TenNhaCungCap like '%" + txttk.Text + "%'";
                adapter.SelectCommand = cmd;
                table.Clear();
                adapter.Fill(table);
                dataGridView1.DataSource = table;
                sqlcon.Close();
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            txtmncc.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtmncc.Enabled = false;
            txttncc.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtdc.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtsdt.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                sqlcon = new SqlConnection(strcon);
                sqlcon.Open();
                cmd = sqlcon.CreateCommand();
                cmd.CommandText = "select MaNhaCungCap as MaNCC, TenNhaCungCap as TenNCC, '' as DiaChi, '' as SDT from NhaCungCap";
                adapter.SelectCommand = cmd;
                table.Clear();
                adapter.Fill(table);
                dataGridView1.DataSource = table;
                sqlcon.Close();
                txtmncc.ResetText();
                txttncc.ResetText();
                txtsdt.ResetText();
                txtdc.ResetText();
                txttk.ResetText();
                txtmncc.Enabled = true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            }
    }
}
