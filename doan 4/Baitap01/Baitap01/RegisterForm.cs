using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Baitap01
{
    public class RegisterForm : Form
    {
        private TextBox txtUsername;
        private TextBox txtPassword;
        private ComboBox cbRole;
        private ComboBox cbNhanVien;
        private Button btnRegister;
        private Button btnCancel;
        private Label lblTitle;
        private Label lblUsername;
        private Label lblPassword;
        private Label lblRole;
        private Label lblNhanVien;

        private string strConnect = DBConnect.strcon;

        public RegisterForm()
        {
            InitializeComponent();
            ApplyAesthetic();
            LoadNhanVien();
            LoadRoles();
        }

        private void InitializeComponent()
        {
            this.lblTitle = new Label();
            this.lblUsername = new Label();
            this.txtUsername = new TextBox();
            this.lblPassword = new Label();
            this.txtPassword = new TextBox();
            this.lblRole = new Label();
            this.cbRole = new ComboBox();
            this.lblNhanVien = new Label();
            this.cbNhanVien = new ComboBox();
            this.btnRegister = new Button();
            this.btnCancel = new Button();

            this.SuspendLayout();

            // Form settings
            this.Text = "Đăng ký tài khoản hệ thống";
            this.Size = new Size(420, 480);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // Title
            this.lblTitle.Text = "ĐĂNG KÝ TÀI KHOẢN";
            this.lblTitle.Location = new Point(20, 25);
            this.lblTitle.Size = new Size(360, 45);
            this.lblTitle.TextAlign = ContentAlignment.MiddleCenter;

            // Username Label & TextBox
            this.lblUsername.Text = "Tên tài khoản:";
            this.lblUsername.Location = new Point(40, 90);
            this.lblUsername.Size = new Size(120, 20);

            this.txtUsername.Location = new Point(40, 115);
            this.txtUsername.Size = new Size(320, 25);

            // Password Label & TextBox
            this.lblPassword.Text = "Mật khẩu:";
            this.lblPassword.Location = new Point(40, 155);
            this.lblPassword.Size = new Size(120, 20);

            this.txtPassword.Location = new Point(40, 180);
            this.txtPassword.Size = new Size(320, 25);
            this.txtPassword.PasswordChar = '*';

            // Role Label & ComboBox
            this.lblRole.Text = "Vai trò người dùng:";
            this.lblRole.Location = new Point(40, 220);
            this.lblRole.Size = new Size(150, 20);

            this.cbRole.Location = new Point(40, 245);
            this.cbRole.Size = new Size(320, 25);
            this.cbRole.DropDownStyle = ComboBoxStyle.DropDownList;

            // NhanVien Label & ComboBox
            this.lblNhanVien.Text = "Liên kết nhân viên:";
            this.lblNhanVien.Location = new Point(40, 285);
            this.lblNhanVien.Size = new Size(150, 20);

            this.cbNhanVien.Location = new Point(40, 310);
            this.cbNhanVien.Size = new Size(320, 25);
            this.cbNhanVien.DropDownStyle = ComboBoxStyle.DropDownList;

            // Register Button
            this.btnRegister.Text = "Đăng Ký";
            this.btnRegister.Location = new Point(40, 370);
            this.btnRegister.Size = new Size(145, 38);
            this.btnRegister.Click += new EventHandler(this.btnRegister_Click);

            // Cancel Button
            this.btnCancel.Text = "Quay Lại";
            this.btnCancel.Location = new Point(215, 370);
            this.btnCancel.Size = new Size(145, 38);
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);

            // Add Controls
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblUsername);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.lblRole);
            this.Controls.Add(this.cbRole);
            this.Controls.Add(this.lblNhanVien);
            this.Controls.Add(this.cbNhanVien);
            this.Controls.Add(this.btnRegister);
            this.Controls.Add(this.btnCancel);

            this.ResumeLayout(false);
        }

        private void ApplyAesthetic()
        {
            // Tông màu Nâu - Be ấm áp, sang trọng
            this.BackColor = Color.FromArgb(245, 240, 235);
            this.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));

            lblTitle.ForeColor = Color.FromArgb(90, 65, 50); // Nâu Espresso đậm
            lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));

            Color brownLabel = Color.FromArgb(100, 75, 60);
            lblUsername.ForeColor = brownLabel;
            lblUsername.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblPassword.ForeColor = brownLabel;
            lblPassword.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblRole.ForeColor = brownLabel;
            lblRole.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblNhanVien.ForeColor = brownLabel;
            lblNhanVien.Font = new Font("Segoe UI", 9F, FontStyle.Bold);

            Color txtBg = Color.FromArgb(252, 250, 248);
            Color txtBorder = Color.FromArgb(185, 160, 140);

            txtUsername.BackColor = txtBg;
            txtUsername.BorderStyle = BorderStyle.FixedSingle;
            txtPassword.BackColor = txtBg;
            txtPassword.BorderStyle = BorderStyle.FixedSingle;

            cbRole.BackColor = txtBg;
            cbNhanVien.BackColor = txtBg;

            // Register Button styling
            btnRegister.FlatStyle = FlatStyle.Flat;
            btnRegister.FlatAppearance.BorderSize = 0;
            btnRegister.BackColor = Color.FromArgb(120, 90, 70); // Nâu đậm ấm áp
            btnRegister.ForeColor = Color.White;
            btnRegister.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnRegister.Cursor = Cursors.Hand;

            // Cancel Button styling
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.BackColor = Color.FromArgb(185, 175, 165); // Be xám đậm
            btnCancel.ForeColor = Color.White;
            btnCancel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnCancel.Cursor = Cursors.Hand;
        }

        private void LoadNhanVien()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(strConnect))
                {
                    string sql = "SELECT MaNhanVien, TenNhanVien + ' (' + MaNhanVien + ')' AS DisplayText FROM NhanVien";
                    using (SqlDataAdapter adapt = new SqlDataAdapter(sql, conn))
                    {
                        DataTable dt = new DataTable();
                        adapt.Fill(dt);
                        cbNhanVien.DataSource = dt;
                        cbNhanVien.DisplayMember = "DisplayText";
                        cbNhanVien.ValueMember = "MaNhanVien";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lấy danh sách nhân viên: " + ex.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadRoles()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Text", typeof(string));
            dt.Columns.Add("Value", typeof(int));

            dt.Rows.Add("Quản trị viên (Admin)", 1);
            dt.Rows.Add("Nhân viên Pha chế (Barista)", 2);
            dt.Rows.Add("Nhân viên Thu ngân (Cashier)", 3);
            dt.Rows.Add("Nhân viên Kho (Warehouse)", 4);

            cbRole.DataSource = dt;
            cbRole.DisplayMember = "Text";
            cbRole.ValueMember = "Value";
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string tk = txtUsername.Text.Trim();
            string mk = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(tk) || string.IsNullOrEmpty(mk))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ tên tài khoản và mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cbRole.SelectedValue == null || cbNhanVien.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn đầy đủ vai trò và nhân viên liên kết!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int vt = (int)cbRole.SelectedValue;
            string maNV = cbNhanVien.SelectedValue.ToString();

            try
            {
                using (SqlConnection conn = new SqlConnection(strConnect))
                {
                    conn.Open();
                    // Kiểm tra xem tên tài khoản đã tồn tại chưa
                    string checkSql = "SELECT COUNT(*) FROM QLTK WHERE TK = @tk";
                    using (SqlCommand checkCmd = new SqlCommand(checkSql, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@tk", tk);
                        int exists = (int)checkCmd.ExecuteScalar();
                        if (exists > 0)
                        {
                            MessageBox.Show("Tên tài khoản này đã tồn tại! Vui lòng chọn tên khác.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    // Thêm tài khoản mới
                    string insertSql = "INSERT INTO QLTK (TK, MK, VT, MaNV) VALUES (@tk, @mk, @vt, @manv)";
                    using (SqlCommand insertCmd = new SqlCommand(insertSql, conn))
                    {
                        insertCmd.Parameters.AddWithValue("@tk", tk);
                        insertCmd.Parameters.AddWithValue("@mk", mk);
                        insertCmd.Parameters.AddWithValue("@vt", vt);
                        insertCmd.Parameters.AddWithValue("@manv", maNV);
                        insertCmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Đăng ký tài khoản hệ thống thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi đăng ký tài khoản: " + ex.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
