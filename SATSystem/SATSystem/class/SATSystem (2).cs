using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;

using System.Collections.Specialized;
using System.Net;
using System.IO;
using System.Threading;

using System.Web.Script.Serialization;

namespace SATSystem
{
    class SATSystem
    {
        
        public bool bValidAdmin { get; set; }
        public string mRFIDTagID { get; set; }
        public int mExitCode { get; set; }

        public static SATSystem instance = null;
        public frmRFIDReader frmRFIDReader1;

        public static SATSystem getInstance()
        {
            if (instance == null)
            {
                instance = new SATSystem();
            }
            return instance;
        }

        public SATSystem()
        {
            bValidAdmin = false;
        }

        public void Initate()
        {
            SATSystem.getInstance().frmRFIDReader1 = new frmRFIDReader();
        }

        public void StartAttendance()
        {   
            SATSystem.getInstance().mExitCode = 0;
            
            while (SATSystem.getInstance().mExitCode != 2)
            {
                SATSystem.getInstance().mRFIDTagID = "";
                SATSystem.getInstance().mExitCode = 0;
                
                do
                {
                    SATSystem.getInstance().frmRFIDReader1.ShowDialog();
                }
                while ((SATSystem.getInstance().mExitCode != 1) && (SATSystem.getInstance().mExitCode != 2));

                if (SATSystem.getInstance().mExitCode == 1)
                {
                    SATSystem.getInstance().mExitCode = 0;
                    
                    string strDate = DateTime.Now.ToString("ddMMyyyy");

                    DBConnect DB = new DBConnect();                    
                    string status = DB.InsertAttendance(strDate, SATSystem.getInstance().mRFIDTagID);
                    if (status.Equals("Error"))
                    {
                        MessageBox.Show("Invalid RFID Card", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else if (status.Equals("ErrorDuplicate"))
                    {
                        //MessageBox.Show("Invalid RFID Card", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //return;
                    }
                    else
                    {
                        //MessageBox.Show("Attendance Marked" + SATSystem.getInstance().mRFIDTagID, "SAT System", MessageBoxButtons.OK, MessageBoxIcon.Information);                        
                    }                    
                }
                else if (SATSystem.getInstance().mExitCode == 2)
                {
                    return;
                }
            }            
        }

        
    }
}
