using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SATSystem
{
    public partial class frmViewStudent : Form
    {
        public frmViewStudent()
        {
            InitializeComponent();
        }
        
        private void frmViewStudent_Load(object sender, EventArgs e)
        {

        }
        
        private void btnRFID_Click(object sender, EventArgs e)
        {
            SATSystem.getInstance().mRFIDTagID = "";
            SATSystem.getInstance().mExitCode = 0;
            SATSystem.getInstance().frmRFIDReader1.ShowDialog();

            string strRFID = SATSystem.getInstance().mRFIDTagID;            

            SATSystem.getInstance().mRFIDTagID = "";
            SATSystem.getInstance().mExitCode = 0;

            DBConnect DB = new DBConnect();
            string[] student1 = new string[16];
            student1 = DB.SelectStudentByRFID(strRFID);

            
            if (student1[3].Equals("Male"))
                rbMale.Checked = true;
            else if (student1[3].Equals("Female"))
                rbFemale.Checked = true;
            else
            {
                MessageBox.Show("No Student is associated with this RFID Card.", "Alert Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            tbRFID.Text = strRFID;
            tbRollNumber.Text = student1[0];
            tbName.Text = student1[1];
            tbAge.Text = student1[2];
            tbClass.Text = student1[4];
            tbDept.Text = student1[5];
            tbAcademicYear.Text = student1[6];
            tbFatherName.Text = student1[7];
            tbFatherContact.Text = student1[8];
            tbFatherOcc.Text = student1[9];
            tbMotherName.Text = student1[10];
            tbMotherContact.Text = student1[11];
            tbMotherOcc.Text = student1[12];
            tbAddress1.Text = student1[13];
            tbAddress2.Text = student1[14];
            tbCity.Text = student1[15];
            tbPupilID.Text = student1[16];
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string strRFID = tbRFID.Text;
            string strPupilID = tbPupilID.Text;
            if (strRFID.Equals(""))
            {
                MessageBox.Show("Kindly scan a RFID, to delete the Student.", "Alert Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (MessageBox.Show("Do you want to Delete the Student Information? ", "Alert Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                DBConnect DB = new DBConnect();
                if (DB.DeleteStudentByRFID(strRFID, strPupilID))
                {

                    tbRFID.Text = "";
                    tbRollNumber.Text = "";
                    tbName.Text = "";
                    tbAge.Text = "";
                    tbClass.Text = "";
                    tbDept.Text = "";
                    tbAcademicYear.Text = "";
                    tbFatherName.Text = "";
                    tbFatherContact.Text = "";
                    tbFatherOcc.Text = "";
                    tbMotherName.Text = "";
                    tbMotherContact.Text = "";
                    tbMotherOcc.Text = "";
                    tbAddress1.Text = "";
                    tbAddress2.Text = "";
                    tbCity.Text = "";
                    tbPupilID.Text = "";
                    MessageBox.Show("Student Information successfully deleted.", "Alert Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Error in deleting the Student Information.", "Alert Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
