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
    public partial class QLTK : Form
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
            cmd.CommandText = "select * from QLTK";
            adapter.SelectCommand = cmd;
            table.Clear();
            adapter.Fill(table);
            dataGridView1.DataSource = table;
            sqlcon.Close();
        }
        public QLTK()
        {
            InitializeComponent();
            loaddata();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            hethong ht = new hethong();
            ht.Show();
            this.Close();
        }
        
        int q;
        Boolean checktk(string tk)
        {
            sqlcon = new SqlConnection(strcon);
            sqlcon.Open();
            string qr = "select TK from QLTK where TK='" + tk + "'";
            SqlCommand command = new SqlCommand(qr, sqlcon);
            SqlDataReader dr = command.ExecuteReader();
            if (dr.HasRows)
            {
                sqlcon.Close();
                return true;
            }
            else
            {
                sqlcon.Close();
                return false;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (txttk.Text == "" || txtmk.Text == "" || cbmanv.Text == "")
                MessageBox.Show("Thông tin không được để trống!");
            else if(QAdmin.Checked == false&&QNV.Checked == false)
                MessageBox.Show("Chưa phân quyền cho tài khoản!");
            else if(txtmk.Text.Length<=6)
                MessageBox.Show("Mật khẩu phải lớn hơn 6 kí tự!");
            else
            {
                bool c = checktk(txttk.Text);
                if(c==true)
                    MessageBox.Show("Tài khoản đã tồn tại!");
                else
                {
                    if (QAdmin.Checked == true)
                        q = 1;
                    else if (QNV.Checked == true)
                        q = 0;
                    DBConnect.updateData("insert into QLTK values('" + txttk.Text + "','" + txtmk.Text + "','" + q + "','" + cbmanv.Text + "')");
                    loaddata();
                }
            }
        }

        private void btnsua_Click(object sender, EventArgs e)
        {
            if (txtmk.Text == "" || cbmanv.Text == "")
                MessageBox.Show("Thông tin không được để trống!");
            else if (txtmk.Text.Length <= 6)
                MessageBox.Show("Mật khẩu phải lớn hơn 6 kí tự!");
            else
            {
                if (QAdmin.Checked == true)
                    q = 1;
                else if (QNV.Checked == true)
                    q = 0;
                DBConnect.updateData("update QLTK set MK='" + txtmk.Text + "',VT='" + q + "',MaNV='" + cbmanv.Text + "' where TK='" + txttk.Text + "'");
                loaddata();
            }
        }
        int index;
        string kt;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            index = dataGridView1.CurrentRow.Index;
            txttk.Text = dataGridView1.Rows[index].Cells[0].Value.ToString();
            txttk.Enabled = false;
            txtmk.Text = dataGridView1.Rows[index].Cells[1].Value.ToString();
            kt = dataGridView1.Rows[index].Cells[2].Value.ToString();
            if (kt == "1")
                QAdmin.Checked = true;
            else
                QNV.Checked = true;
            cbmanv.Text = dataGridView1.Rows[index].Cells[3].Value.ToString();
        }

        private void btnrs_Click(object sender, EventArgs e)
        {
            reset();
            loaddata();
        }
        private void reset()
        {
            txttk.Enabled = true;
            txttk.Text = "";
            txtmk.Text = "";
            cbmanv.Text = "";
            QAdmin.Checked = false;
            QNV.Checked = false;
        }
        private void btnxoa_Click(object sender, EventArgs e)
        {
            DBConnect.updateData("delete from QLTK where TK='" + txttk.Text + "'");
            loaddata();
            reset();
        }

        private void btntim_Click(object sender, EventArgs e)
        {
            sqlcon = new SqlConnection(strcon);
            sqlcon.Open();
            cmd = sqlcon.CreateCommand();
            cmd.CommandText = "select * from QLTK where MaNV='"+txttim.Text+"' or TK like '%"+txttim.Text+"%'";
            adapter.SelectCommand = cmd;
            table.Clear();
            adapter.Fill(table);
            dataGridView1.DataSource = table;
            sqlcon.Close();
        }

        private void QLTK_Load(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(strcon))
                {
                    SqlDataAdapter da = new SqlDataAdapter("select MaNhanVien, TenNhanVien from NhanVien", conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    cbmanv.DataSource = dt;
                    cbmanv.DisplayMember = "MaNhanVien";
                    cbmanv.ValueMember = "MaNhanVien";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
