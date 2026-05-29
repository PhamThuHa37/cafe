using System;
using System.Drawing;
using System.Windows.Forms;

namespace Baitap01
{
    public static class ThemeManager
    {
        public static void ApplyTheme(Form form)
        {
            // Background color for Form (Light Beige)
            form.BackColor = Color.FromArgb(245, 235, 220);

            ApplyThemeToControls(form.Controls);
        }

        private static void ApplyThemeToControls(Control.ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                if (control is Button)
                {
                    Button btn = (Button)control;
                    btn.BackColor = Color.FromArgb(139, 69, 19); // SaddleBrown
                    btn.ForeColor = Color.White;
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.BorderSize = 0;
                    btn.Font = new Font(btn.Font, FontStyle.Bold);
                }
                else if (control is Label)
                {
                    Label lbl = (Label)control;
                    lbl.ForeColor = Color.FromArgb(92, 64, 51); // Dark Brown
                    lbl.BackColor = Color.Transparent;
                    lbl.Font = new Font(lbl.Font, FontStyle.Bold);
                }
                else if (control is DataGridView)
                {
                    DataGridView dgv = (DataGridView)control;
                    dgv.BackgroundColor = Color.White;
                    dgv.DefaultCellStyle.BackColor = Color.FromArgb(255, 250, 240); // FloralWhite
                    dgv.DefaultCellStyle.ForeColor = Color.FromArgb(92, 64, 51);
                    dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(210, 180, 140); // Tan
                    dgv.DefaultCellStyle.SelectionForeColor = Color.Black;
                    dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(139, 69, 19);
                    dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                    dgv.EnableHeadersVisualStyles = false;
                }
                else if (control is MenuStrip)
                {
                    MenuStrip ms = (MenuStrip)control;
                    ms.BackColor = Color.FromArgb(139, 69, 19);
                    ms.ForeColor = Color.White;
                }
                else if (control is Panel || control is GroupBox)
                {
                    // Recursively apply to children
                    ApplyThemeToControls(control.Controls);
                    if (control is GroupBox)
                    {
                        control.ForeColor = Color.FromArgb(92, 64, 51); // Dark Brown
                    }
                }
                else
                {
                    // For other controls like Panel, TabControl, etc., we can recurse
                    if (control.HasChildren)
                    {
                        ApplyThemeToControls(control.Controls);
                    }
                }
            }
        }
    }
}
