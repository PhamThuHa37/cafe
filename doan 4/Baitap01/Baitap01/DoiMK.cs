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
    public partial class DoiMK : Form
    {
        string strcon = DBConnect.strcon;
        SqlConnection sqlcon = null;
        public DoiMK()
        {
            InitializeComponent();
        }

        private void btnhuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        string tk = Login.tk;
        private void btndmk_Click(object sender, EventArgs e)
        {
            if (txtmkc.Text == "" || txtmkm.Text == "" || txtxn.Text == "")
                MessageBox.Show("Thông tin không được để trống!");
            else if(txtmkc.Text!=Login.mk)
                MessageBox.Show("Mật khẩu không chính xác, yêu cầu nhập lại!");
            else if (txtmkm.Text.Length <= 6)
                MessageBox.Show("Mật khẩu phải lớn hơn 6 kí tự!");
            else if(txtxn.Text!=txtmkm.Text)
                MessageBox.Show("Xác nhận mật khẩu không khớp, yêu cầu nhập lại!");
            else
            {
                sqlcon = new SqlConnection(strcon);
                sqlcon.Open();
                string dmk = "update QLTK set MK='" + txtmkm.Text + "' where TK='" + tk + "'";
                SqlCommand cmddmk = new SqlCommand(dmk, sqlcon);
                cmddmk.ExecuteNonQuery();
                sqlcon.Close();
                MessageBox.Show("Đổi mật khẩu thành công!");
                Login.mk = txtmkm.Text;
                this.Close();
            }
        }

        private void DoiMK_Load(object sender, EventArgs e)
        {

        }
    }
}
