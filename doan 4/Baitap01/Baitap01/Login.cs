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
    public partial class Login : Form
    {
        SqlCommand command;
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable table = new DataTable();
        string strcon = @"Data Source=HUONGLT\SQLEXPRESS;Initial Catalog=QuanLyQuanCaFe;Integrated Security=True";
        SqlConnection sqlcon = null;
        public Login()
        {
            InitializeComponent();
            Baitap01.ThemeManager.ApplyTheme(this);
            
            Button btnDangKy = new Button();
            btnDangKy.Text = "Đăng ký";
            btnDangKy.Location = new Point(125, 158);
            btnDangKy.Size = new Size(100, 28);
            btnDangKy.Click += (sender, e) => {
                DangKy dk = new DangKy();
                dk.Show();
                this.Hide();
            };
            this.Controls.Add(btnDangKy);
        }
        public static string tk;
        public static string mk;
        string qr;
        public static int pq;
        public static string nv;
        private void getUSER()
        {
            sqlcon = new SqlConnection(strcon);
            sqlcon.Open();
            command = sqlcon.CreateCommand();
            command.CommandText = "select VT,MaNV from QLTK where TK='"+txttk.Text+"'";
            adapter.SelectCommand = command;
            table.Clear();
            adapter.Fill(table);
            pq = table.Rows[0].Field<int>(0);
            nv = table.Rows[0].Field<string>(1);
            sqlcon.Close();
        }
        private void btnlogin_Click(object sender, EventArgs e)
        {
            if (txttk.Text == "" && txtmk.Text == "")
                MessageBox.Show("Da nhap gi dau!");
            else if (txttk.Text == "")
                MessageBox.Show("Chua nhap tai khoan!");
            else if (txtmk.Text == "")
                MessageBox.Show("Chua nhap mat khau!");
            else
            {
                sqlcon = new SqlConnection(strcon);
                sqlcon.Open();
                qr = "select * from QLTK where TK='" + txttk.Text + "' and MK='" + txtmk.Text + "'";
                SqlCommand command = new SqlCommand(qr, sqlcon);
                SqlDataReader dr = command.ExecuteReader();
                if (dr.HasRows)
                {
                    getUSER();
                    hethong ht = new hethong();
                    ht.Show();
                    tk = txttk.Text;
                    mk = txtmk.Text;
                    this.Hide();
                }
                else
                    MessageBox.Show("Tai khoan hoac mat khau khong chinh xac!");
                sqlcon.Close();
            }
        }

        private void btnthoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
    }
}
