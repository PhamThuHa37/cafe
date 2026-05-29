using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Baitap01
{
    public partial class hethong : Form
    {
        public hethong()
        {
            InitializeComponent();
            Baitap01.ThemeManager.ApplyTheme(this);
            if (q == 1)
            {
                // Admin: Full access
            }
            else if (q == 2)
            {
                // Phuc vu: Tao don hang, xem trang thai, menu do uong
                itemQLTK.Enabled = false;
                itemQLNS.Enabled = false; // Nhan Su
                itemNV.Enabled = false;
                itemCV.Enabled = false;
                itemQLMH.Enabled = false; // Mat Hang (San pham / LSP)
                // itemSP.Enabled = false; // Phuc vu can xem san pham
                // itemLSP.Enabled = false;
                itemCC.Enabled = false; // Cham cong
                quảnLýNhậpHàngToolStripMenuItem.Enabled = false;
                itemNCC.Enabled = false;
                itemPN.Enabled = false;
                itemthongke.Enabled = false;
            }
            else if (q == 3)
            {
                // Thu ngan: Thanh toan, Menu do uong, Phieu thu chi
                itemQLTK.Enabled = false;
                itemQLNS.Enabled = false;
                itemNV.Enabled = false;
                itemCV.Enabled = false;
                itemQLMH.Enabled = false;
                itemCC.Enabled = false;
                quảnLýNhậpHàngToolStripMenuItem.Enabled = false;
                itemNCC.Enabled = false;
                itemPN.Enabled = false;
                itemthongke.Enabled = false;
            }
            else if (q == 4)
            {
                // Kho: Quan ly nguyen lieu, phieu nhap, ton kho
                itemQLTK.Enabled = false;
                itemQLNS.Enabled = false;
                itemNV.Enabled = false;
                itemCV.Enabled = false;
                itemCC.Enabled = false;
                quảnLýHoáĐơnToolStripMenuItem.Enabled = false; // Khong ban hang
                itemKH.Enabled = false;
                itemthongke.Enabled = false;
            }
        }
        int q = Login.pq;
        string nv = Login.nv;
        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Login dn = new Login();
            dn.Show();
            this.Close();
        }

        private void quảnLýHoáĐơnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QLHD hd = new QLHD();
            hd.Show();
            this.Hide();
        }

        private void itemCC_Click(object sender, EventArgs e)
        {
            ChamCong cc = new ChamCong();
            cc.Show();
            this.Hide();
        }

        private void itemQLTK_Click(object sender, EventArgs e)
        {
            QLTK tk = new QLTK();
            tk.Show();
            this.Hide();
        }

        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void đổiMậtKhẩuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoiMK dmk = new DoiMK();
            dmk.Show();
        }

        private void itemSP_Click(object sender, EventArgs e)
        {
            QLSP sp = new QLSP();
            sp.Show();
            this.Hide();
        }

        private void itemLSP_Click(object sender, EventArgs e)
        {
            QLLSP lsp = new QLLSP();
            lsp.Show();
            this.Hide();
        }

        private void itemKH_Click(object sender, EventArgs e)
        {
            QuanLyKH kh = new QuanLyKH();
            kh.Show();
            this.Hide();
        }

        private void itemNV_Click(object sender, EventArgs e)
        {
            QLNhanVien nv = new QLNhanVien();
            nv.Show();
            this.Hide();
        }

        private void itemCV_Click(object sender, EventArgs e)
        {
            ChucVu cv = new ChucVu();
            cv.Show();
            this.Hide();
        }

        private void itemNCC_Click(object sender, EventArgs e)
        {
            QLNCC ncc = new QLNCC();
            ncc.Show();
            this.Hide();
        }

        private void itemPN_Click(object sender, EventArgs e)
        {
            QLPhieuNhap pn = new QLPhieuNhap();
            pn.Show();
            this.Hide();
        }

        private void itemthongke_Click(object sender, EventArgs e)
        {
            ThongKe tk = new ThongKe();
            tk.Show();
            this.Hide();
        }
    }
}
