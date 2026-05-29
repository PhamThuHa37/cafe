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
    public partial class DangKy : Form
    {
        SqlCommand command;
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable table = new DataTable();
        string strcon = @"Data Source=HUONGLT\SQLEXPRESS;Initial Catalog=QuanLyQuanCaFe;Integrated Security=True";
        SqlConnection sqlcon = null;
        public DangKy()
        {
            InitializeComponent();
            Baitap01.ThemeManager.ApplyTheme(this);
            label3.Text = "Đăng Ký";
            btnlogin.Text = "Đăng Ký";
            
            lblVaiTro = new Label();
            lblVaiTro.Text = "Vai Trò";
            lblVaiTro.Location = new Point(12, 115); // Adjust Y
            lblVaiTro.AutoSize = true;
            this.Controls.Add(lblVaiTro);
            
            cbVaiTro = new ComboBox();
            cbVaiTro.Location = new Point(83, 115); // Adjust Y
            cbVaiTro.Size = new Size(163, 21);
            cbVaiTro.Items.Add("Admin");
            cbVaiTro.Items.Add("Phục vụ");
            cbVaiTro.Items.Add("Thu ngân");
            cbVaiTro.Items.Add("Nhân viên Kho");
            cbVaiTro.SelectedIndex = 1;
            this.Controls.Add(cbVaiTro);
            
            // Push existing buttons down
            btnlogin.Top += 30;
            btnthoat.Top += 30;
            this.Height += 30;
        }
        
        ComboBox cbVaiTro;
        Label lblVaiTro;

        private int GetVaiTroId()
        {
            if (cbVaiTro.SelectedIndex == 0) return 1;
            if (cbVaiTro.SelectedIndex == 1) return 2;
            if (cbVaiTro.SelectedIndex == 2) return 3;
            if (cbVaiTro.SelectedIndex == 3) return 4;
            return 2;
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
                
                // Check if username exists
                string checkQr = "select * from QLTK where TK='" + txttk.Text + "'";
                SqlCommand checkCmd = new SqlCommand(checkQr, sqlcon);
                SqlDataReader dr = checkCmd.ExecuteReader();
                if (dr.HasRows)
                {
                    MessageBox.Show("Tài khoản đã tồn tại, vui lòng chọn tên khác!");
                    dr.Close();
                    sqlcon.Close();
                    return;
                }
                dr.Close();
                
                // Insert new user
                int roleId = GetVaiTroId();
                string insertQr = "insert into QLTK (TK, MK, VT) values ('" + txttk.Text + "', '" + txtmk.Text + "', " + roleId + ")";
                SqlCommand insertCmd = new SqlCommand(insertQr, sqlcon);
                insertCmd.ExecuteNonQuery();
                
                MessageBox.Show("Đăng ký thành công!");
                
                Login f = new Login();
                f.Show();
                this.Hide();
                
                sqlcon.Close();
            }
        }

        private void btnthoat_Click(object sender, EventArgs e)
        {
            Login f = new Login();
            f.Show();
            this.Hide();
        }

        private void DangKy_Load(object sender, EventArgs e)
        {

        }
    }
}
