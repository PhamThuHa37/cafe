namespace Baitap01
{
    partial class QuanLyKhuyenMai
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
            this.dgvKhuyenMai = new System.Windows.Forms.DataGridView();
            this.lblMaKM = new System.Windows.Forms.Label();
            this.txtMaKM = new System.Windows.Forms.TextBox();
            this.lblDK = new System.Windows.Forms.Label();
            this.txtDK = new System.Windows.Forms.TextBox();
            this.lblStart = new System.Windows.Forms.Label();
            this.dtpStart = new System.Windows.Forms.DateTimePicker();
            this.lblEnd = new System.Windows.Forms.Label();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.lblMucKM = new System.Windows.Forms.Label();
            this.numMucKM = new System.Windows.Forms.NumericUpDown();
            this.btnThem = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnLuu = new System.Windows.Forms.Button();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.btnQuayLai = new System.Windows.Forms.Button();
            
            ((System.ComponentModel.ISupportInitialize)(this.dgvKhuyenMai)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMucKM)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Size = new System.Drawing.Size(920, 45);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Text = "QUẢN LÝ CHƯƠNG TRÌNH KHUYẾN MẠI";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgvKhuyenMai
            // 
            this.dgvKhuyenMai.Location = new System.Drawing.Point(25, 90);
            this.dgvKhuyenMai.Size = new System.Drawing.Size(510, 440);
            this.dgvKhuyenMai.Name = "dgvKhuyenMai";
            this.dgvKhuyenMai.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvKhuyenMai_CellClick);
            // 
            // lblMaKM
            // 
            this.lblMaKM.Location = new System.Drawing.Point(570, 90);
            this.lblMaKM.Size = new System.Drawing.Size(360, 20);
            this.lblMaKM.Name = "lblMaKM";
            this.lblMaKM.Text = "Mã khuyến mại:";
            // 
            // txtMaKM
            // 
            this.txtMaKM.Location = new System.Drawing.Point(570, 112);
            this.txtMaKM.Size = new System.Drawing.Size(360, 25);
            this.txtMaKM.Name = "txtMaKM";
            // 
            // lblDK
            // 
            this.lblDK.Location = new System.Drawing.Point(570, 155);
            this.lblDK.Size = new System.Drawing.Size(360, 20);
            this.lblDK.Name = "lblDK";
            this.lblDK.Text = "Điều kiện áp dụng:";
            // 
            // txtDK
            // 
            this.txtDK.Location = new System.Drawing.Point(570, 177);
            this.txtDK.Size = new System.Drawing.Size(360, 25);
            this.txtDK.Name = "txtDK";
            // 
            // lblStart
            // 
            this.lblStart.Location = new System.Drawing.Point(570, 220);
            this.lblStart.Size = new System.Drawing.Size(170, 20);
            this.lblStart.Name = "lblStart";
            this.lblStart.Text = "Ngày bắt đầu:";
            // 
            // dtpStart
            // 
            this.dtpStart.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStart.Location = new System.Drawing.Point(570, 242);
            this.dtpStart.Size = new System.Drawing.Size(170, 25);
            this.dtpStart.Name = "dtpStart";
            // 
            // lblEnd
            // 
            this.lblEnd.Location = new System.Drawing.Point(760, 220);
            this.lblEnd.Size = new System.Drawing.Size(170, 20);
            this.lblEnd.Name = "lblEnd";
            this.lblEnd.Text = "Ngày kết thúc:";
            // 
            // dtpEnd
            // 
            this.dtpEnd.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEnd.Location = new System.Drawing.Point(760, 242);
            this.dtpEnd.Size = new System.Drawing.Size(170, 25);
            this.dtpEnd.Name = "dtpEnd";
            // 
            // lblMucKM
            // 
            this.lblMucKM.Location = new System.Drawing.Point(570, 285);
            this.lblMucKM.Size = new System.Drawing.Size(360, 20);
            this.lblMucKM.Name = "lblMucKM";
            this.lblMucKM.Text = "Mức khuyến mại (VNĐ):";
            // 
            // numMucKM
            // 
            this.numMucKM.Location = new System.Drawing.Point(570, 307);
            this.numMucKM.Size = new System.Drawing.Size(360, 25);
            this.numMucKM.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            this.numMucKM.Name = "numMucKM";
            // 
            // btnThem
            // 
            this.btnThem.Location = new System.Drawing.Point(570, 360);
            this.btnThem.Size = new System.Drawing.Size(105, 38);
            this.btnThem.Name = "btnThem";
            this.btnThem.Text = "Thêm";
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // btnSua
            // 
            this.btnSua.Location = new System.Drawing.Point(695, 360);
            this.btnSua.Size = new System.Drawing.Size(105, 38);
            this.btnSua.Name = "btnSua";
            this.btnSua.Text = "Sửa";
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.Location = new System.Drawing.Point(820, 360);
            this.btnXoa.Size = new System.Drawing.Size(110, 38);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Text = "Xóa";
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnLuu
            // 
            this.btnLuu.Location = new System.Drawing.Point(570, 420);
            this.btnLuu.Size = new System.Drawing.Size(170, 38);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Text = "Lưu";
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.Location = new System.Drawing.Point(760, 420);
            this.btnLamMoi.Size = new System.Drawing.Size(170, 38);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // btnQuayLai
            // 
            this.btnQuayLai.Location = new System.Drawing.Point(570, 480);
            this.btnQuayLai.Size = new System.Drawing.Size(360, 42);
            this.btnQuayLai.Name = "btnQuayLai";
            this.btnQuayLai.Text = "Quay Lại";
            this.btnQuayLai.Click += new System.EventHandler(this.btnQuayLai_Click);
            // 
            // QuanLyKhuyenMai
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(960, 560);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.dgvKhuyenMai);
            this.Controls.Add(this.lblMaKM);
            this.Controls.Add(this.txtMaKM);
            this.Controls.Add(this.lblDK);
            this.Controls.Add(this.txtDK);
            this.Controls.Add(this.lblStart);
            this.Controls.Add(this.dtpStart);
            this.Controls.Add(this.lblEnd);
            this.Controls.Add(this.dtpEnd);
            this.Controls.Add(this.lblMucKM);
            this.Controls.Add(this.numMucKM);
            this.Controls.Add(this.btnThem);
            this.Controls.Add(this.btnSua);
            this.Controls.Add(this.btnXoa);
            this.Controls.Add(this.btnLuu);
            this.Controls.Add(this.btnLamMoi);
            this.Controls.Add(this.btnQuayLai);
            this.Name = "QuanLyKhuyenMai";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản Lý Khuyến Mại - Coffee System";
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            ((System.ComponentModel.ISupportInitialize)(this.dgvKhuyenMai)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMucKM)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.DataGridView dgvKhuyenMai;
        private System.Windows.Forms.Label lblMaKM;
        private System.Windows.Forms.TextBox txtMaKM;
        private System.Windows.Forms.Label lblDK;
        private System.Windows.Forms.TextBox txtDK;
        private System.Windows.Forms.Label lblStart;
        private System.Windows.Forms.DateTimePicker dtpStart;
        private System.Windows.Forms.Label lblEnd;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.Label lblMucKM;
        private System.Windows.Forms.NumericUpDown numMucKM;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnLuu;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.Button btnQuayLai;
    }
}
