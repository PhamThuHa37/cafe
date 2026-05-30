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
    public partial class ChamCong : Form
    {
        string strcon = DBConnect.strcon;
        SqlConnection sqlcon = null;
        SqlCommand cmd;
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable table = new DataTable();
        public ChamCong()
        {
            InitializeComponent();
            loaddata();
        }
        void loaddata()
        {
            sqlcon = new SqlConnection(strcon);
            sqlcon.Open();
            cmd = sqlcon.CreateCommand();
            cmd.CommandText = "select * from QLBCC";
            adapter.SelectCommand = cmd;
            table.Clear();
            adapter.Fill(table);
            dataGridView2.DataSource = table;
            sqlcon.Close();
        }
        int q = Login.pq;
        private void btntim_Click(object sender, EventArgs e)
        {
            sqlcon = new SqlConnection(strcon);
            sqlcon.Open();
            cmd = sqlcon.CreateCommand();
            cmd.CommandText = "select * from QLBCC where Thang='"+cbthang.SelectedItem+"' and Nam='"+txtnam.Text+"'";
            adapter.SelectCommand = cmd;
            table.Clear();
            adapter.Fill(table);
            dataGridView2.DataSource = table;
            sqlcon.Close();
        }
        private int taomabcc()
        {
            sqlcon = new SqlConnection(strcon);
            sqlcon.Open();
            cmd = sqlcon.CreateCommand();
            cmd.CommandText = "select * from QLBCC";
            adapter.SelectCommand = cmd;
            table.Clear();
            adapter.Fill(table);
            int dem = table.Rows.Count;
            sqlcon.Close();
            return dem + 1;
        }
        int mabcc;
        string qr;
        private void btntao_Click(object sender, EventArgs e)
        {
            mabcc = taomabcc();
            DateTime time = DateTime.Now;
            sqlcon = new SqlConnection(strcon);
            sqlcon.Open();
            qr = "select * from QLBCC where Thang='"+ time.Month + "' and Nam='"+ time.Year + "'";
            SqlCommand command = new SqlCommand(qr, sqlcon);
            SqlDataReader dr = command.ExecuteReader();
            if (dr.HasRows)
            {
                MessageBox.Show("Tháng này đã tạo bảng chấm công!");
                sqlcon.Close();
            }
            else
            {
                sqlcon.Close();
                DBConnect.updateData("insert into QLBCC values('B0" + mabcc + "','" + time.Month + "','" + time.Year + "')");
                //sqlcon.Open();
                //qr = "insert into QLBCC values('B0" + mabcc + "','" + time.Month + "','" + time.Year + "')";
                //command = new SqlCommand(qr, sqlcon);
                //command.ExecuteNonQuery();
                loaddata();
                //sqlcon.Close();
            }
        }

        private void btnquaylai_Click(object sender, EventArgs e)
        {
            hethong ht = new hethong();
            ht.Show();
            this.Close();
        }

        private void btnrs_Click(object sender, EventArgs e)
        {
            loaddata();
        }

        private void btnctcc_Click(object sender, EventArgs e)
        {
            ChiTietCC c = new ChiTietCC();
            c.Show();
        }
        public static string ma;
        //public static string thang;
        //public static string nam;
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = dataGridView2.CurrentRow.Index;
            ma = dataGridView2.Rows[i].Cells[0].Value.ToString();
            //thang= dataGridView2.Rows[i].Cells[1].Value.ToString();
            //nam= dataGridView2.Rows[i].Cells[2].Value.ToString();
        }

        private void btntl_Click(object sender, EventArgs e)
        {
            TinhLuong tl = new TinhLuong();
            tl.Show();
        }

        private void ChamCong_Load(object sender, EventArgs e)
        {

        }
    }
}
