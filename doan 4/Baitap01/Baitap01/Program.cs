using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Baitap01
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Bypass Login screen and automatically assign full Admin privileges
            Login.pq = 1;       // Admin role (1)
            Login.nv = "NV01";  // Default Admin Employee ID (MaNV)
            Login.tk = "admin"; // Default username
            Login.mk = "1234567"; // Default password

            Application.Run(new hethong());
        }
    }
}
