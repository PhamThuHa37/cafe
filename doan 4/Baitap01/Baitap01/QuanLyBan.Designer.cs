namespace Baitap01
{
    partial class QuanLyBan
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.dgvBan = new System.Windows.Forms.DataGridView();
            this.lblMaBan = new System.Windows.Forms.Label();
            this.txtMaBan = new System.Windows.Forms.TextBox();
            this.lblSoLuong = new System.Windows.Forms.Label();
            this.numSoLuong = new System.Windows.Forms.NumericUpDown();
            this.lblTinhTrang = new System.Windows.Forms.Label();
            this.cboTinhTrang = new System.Windows.Forms.ComboBox();
            this.btnThem = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnLuu = new System.Windows.Forms.Button();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.btnQuayLai = new System.Windows.Forms.Button();
            
            ((System.ComponentModel.ISupportInitialize)(this.dgvBan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSoLuong)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Size = new System.Drawing.Size(860, 45);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Text = "QUẢN LÝ ĐẶT BÀN & TRẠNG THÁI";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgvBan
            // 
            this.dgvBan.Location = new System.Drawing.Point(25, 90);
            this.dgvBan.Size = new System.Drawing.Size(470, 405);
            this.dgvBan.Name = "dgvBan";
            this.dgvBan.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBan_CellClick);
            // 
            // lblMaBan
            // 
            this.lblMaBan.Location = new System.Drawing.Point(530, 95);
            this.lblMaBan.Size = new System.Drawing.Size(340, 20);
            this.lblMaBan.Name = "lblMaBan";
            this.lblMaBan.Text = "Mã bàn:";
            // 
            // txtMaBan
            // 
            this.txtMaBan.Location = new System.Drawing.Point(530, 120);
            this.txtMaBan.Size = new System.Drawing.Size(340, 25);
            this.txtMaBan.Name = "txtMaBan";
            // 
            // lblSoLuong
            // 
            this.lblSoLuong.Location = new System.Drawing.Point(530, 170);
            this.lblSoLuong.Size = new System.Drawing.Size(340, 20);
            this.lblSoLuong.Name = "lblSoLuong";
            this.lblSoLuong.Text = "Số lượng chỗ:";
            // 
            // numSoLuong
            // 
            this.numSoLuong.Location = new System.Drawing.Point(530, 195);
            this.numSoLuong.Size = new System.Drawing.Size(340, 25);
            this.numSoLuong.Minimum = 1;
            this.numSoLuong.Maximum = 100;
            this.numSoLuong.Value = 4;
            this.numSoLuong.Name = "numSoLuong";
            // 
            // lblTinhTrang
            // 
            this.lblTinhTrang.Location = new System.Drawing.Point(530, 245);
            this.lblTinhTrang.Size = new System.Drawing.Size(340, 20);
            this.lblTinhTrang.Name = "lblTinhTrang";
            this.lblTinhTrang.Text = "Trạng thái:";
            // 
            // cboTinhTrang
            // 
            this.cboTinhTrang.Location = new System.Drawing.Point(530, 270);
            this.cboTinhTrang.Size = new System.Drawing.Size(340, 25);
            this.cboTinhTrang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTinhTrang.Name = "cboTinhTrang";
            // 
            // btnThem
            // 
            this.btnThem.Location = new System.Drawing.Point(530, 330);
            this.btnThem.Size = new System.Drawing.Size(100, 38);
            this.btnThem.Name = "btnThem";
            this.btnThem.Text = "Thêm";
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // btnSua
            // 
            this.btnSua.Location = new System.Drawing.Point(650, 330);
            this.btnSua.Size = new System.Drawing.Size(100, 38);
            this.btnSua.Name = "btnSua";
            this.btnSua.Text = "Sửa";
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.Location = new System.Drawing.Point(770, 330);
            this.btnXoa.Size = new System.Drawing.Size(100, 38);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Text = "Xóa";
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnLuu
            // 
            this.btnLuu.Location = new System.Drawing.Point(530, 390);
            this.btnLuu.Size = new System.Drawing.Size(160, 38);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Text = "Lưu";
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.Location = new System.Drawing.Point(710, 390);
            this.btnLamMoi.Size = new System.Drawing.Size(160, 38);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // btnQuayLai
            // 
            this.btnQuayLai.Location = new System.Drawing.Point(530, 450);
            this.btnQuayLai.Size = new System.Drawing.Size(340, 42);
            this.btnQuayLai.Name = "btnQuayLai";
            this.btnQuayLai.Text = "Quay Lại";
            this.btnQuayLai.Click += new System.EventHandler(this.btnQuayLai_Click);
            // 
            // QuanLyBan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(920, 560);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.dgvBan);
            this.Controls.Add(this.lblMaBan);
            this.Controls.Add(this.txtMaBan);
            this.Controls.Add(this.lblSoLuong);
            this.Controls.Add(this.numSoLuong);
            this.Controls.Add(this.lblTinhTrang);
            this.Controls.Add(this.cboTinhTrang);
            this.Controls.Add(this.btnThem);
            this.Controls.Add(this.btnSua);
            this.Controls.Add(this.btnXoa);
            this.Controls.Add(this.btnLuu);
            this.Controls.Add(this.btnLamMoi);
            this.Controls.Add(this.btnQuayLai);
            this.Name = "QuanLyBan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản Lý Đặt Bàn - Coffee System";
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            ((System.ComponentModel.ISupportInitialize)(this.dgvBan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSoLuong)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.DataGridView dgvBan;
        private System.Windows.Forms.Label lblMaBan;
        private System.Windows.Forms.TextBox txtMaBan;
        private System.Windows.Forms.Label lblSoLuong;
        private System.Windows.Forms.NumericUpDown numSoLuong;
        private System.Windows.Forms.Label lblTinhTrang;
        private System.Windows.Forms.ComboBox cboTinhTrang;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnLuu;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.Button btnQuayLai;
    }
}
