using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace SATSystem
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
            
            
            //SATSystem satSystem = new SATSystem();
            //satSystem.Start();

            Application.Run(new frmSplashScreen());
            Application.Run(new frmLogin());
            if (SATSystem.getInstance().bValidAdmin)
            {
                Application.Run(new frmLandingScreen());
            }
        }
    }
}
