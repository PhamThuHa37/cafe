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
        string strcon = DBConnect.strcon;
        private LinkLabel lnkRegister;

        public Login()
        {
            InitializeComponent();
            StyleForm(); // Áp dụng giao diện Coffee-shop Nâu Be sang trọng
        }

        public static string tk;
        public static string mk;
        public static int pq;
        public static string nv;

        // Định dạng giao diện tone Nâu - Be và thêm chức năng Đăng ký
        private void StyleForm()
        {
            this.BackColor = Color.FromArgb(245, 240, 235); // Be sáng
            this.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            this.Size = new Size(380, 270);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // Title "ĐĂNG NHẬP"
            label3.Text = "ĐĂNG NHẬP";
            label3.ForeColor = Color.FromArgb(90, 65, 50); // Nâu Espresso
            label3.Font = new Font("Segoe UI", 20F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            label3.Location = new Point(0, 15);
            label3.Size = new Size(360, 45);
            label3.TextAlign = ContentAlignment.MiddleCenter;

            // Labels
            label1.ForeColor = Color.FromArgb(100, 75, 60);
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label2.ForeColor = Color.FromArgb(100, 75, 60);
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold);

            // TextBoxes
            txttk.BackColor = Color.FromArgb(252, 250, 248);
            txttk.BorderStyle = BorderStyle.FixedSingle;
            txttk.ForeColor = Color.FromArgb(60, 45, 35);
            txtmk.BackColor = Color.FromArgb(252, 250, 248);
            txtmk.BorderStyle = BorderStyle.FixedSingle;
            txtmk.ForeColor = Color.FromArgb(60, 45, 35);

            // Login Button
            btnlogin.FlatStyle = FlatStyle.Flat;
            btnlogin.FlatAppearance.BorderSize = 0;
            btnlogin.BackColor = Color.FromArgb(120, 90, 70); // Nâu ấm
            btnlogin.ForeColor = Color.White;
            btnlogin.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnlogin.Cursor = Cursors.Hand;

            // Exit Button
            btnthoat.FlatStyle = FlatStyle.Flat;
            btnthoat.FlatAppearance.BorderSize = 0;
            btnthoat.BackColor = Color.FromArgb(185, 175, 165); // Be xám đậm
            btnthoat.ForeColor = Color.White;
            btnthoat.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnthoat.Cursor = Cursors.Hand;

            // Programmatically add Register LinkLabel
            lnkRegister = new LinkLabel();
            lnkRegister.Text = "Chưa có tài khoản? Đăng ký ngay";
            lnkRegister.Location = new Point(0, 195);
            lnkRegister.Size = new Size(360, 25);
            lnkRegister.TextAlign = ContentAlignment.MiddleCenter;
            lnkRegister.LinkColor = Color.FromArgb(120, 90, 70);
            lnkRegister.ActiveLinkColor = Color.FromArgb(90, 65, 50);
            lnkRegister.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            lnkRegister.Cursor = Cursors.Hand;
            lnkRegister.Click += new EventHandler(lnkRegister_Click);
            this.Controls.Add(lnkRegister);
        }

        private void lnkRegister_Click(object sender, EventArgs e)
        {
            RegisterForm reg = new RegisterForm();
            reg.ShowDialog();
        }

        private void getUSER(string username)
        {
            try
            {
                using (SqlConnection sqlcon = new SqlConnection(strcon))
                {
                    string sql = "select VT, MaNV from QLTK where TK=@tk";
                    using (SqlCommand command = new SqlCommand(sql, sqlcon))
                    {
                        command.Parameters.AddWithValue("@tk", username);
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            DataTable table = new DataTable();
                            adapter.Fill(table);
                            if (table.Rows.Count > 0)
                            {
                                pq = Convert.ToInt32(table.Rows[0][0]);
                                nv = table.Rows[0][1]?.ToString() ?? "";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lấy thông tin phiên: " + ex.Message);
            }
        }

        private void btnlogin_Click(object sender, EventArgs e)
        {
            string username = txttk.Text.Trim();
            string password = txtmk.Text;

            if (string.IsNullOrEmpty(username) && string.IsNullOrEmpty(password))
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin đăng nhập!");
            else if (string.IsNullOrEmpty(username))
                MessageBox.Show("Chưa nhập tài khoản!");
            else if (string.IsNullOrEmpty(password))
                MessageBox.Show("Chưa nhập mật khẩu!");
            else
            {
                try
                {
                    using (SqlConnection sqlcon = new SqlConnection(strcon))
                    {
                        sqlcon.Open();
                        string qr = "select count(*) from QLTK where TK=@tk and MK=@mk";
                        using (SqlCommand command = new SqlCommand(qr, sqlcon))
                        {
                            command.Parameters.AddWithValue("@tk", username);
                            command.Parameters.AddWithValue("@mk", password);
                            int count = (int)command.ExecuteScalar();

                            if (count > 0)
                            {
                                getUSER(username);
                                tk = username;
                                mk = password;

                                hethong ht = new hethong();
                                ht.Show();
                                this.Hide();
                            }
                            else
                            {
                                MessageBox.Show("Tài khoản hoặc mật khẩu không chính xác!", "Đăng nhập thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + ex.Message, "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
