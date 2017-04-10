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
    public partial class frmRegisterStudent : Form
    {
        public frmRegisterStudent()
        {
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            string strRollNum = tbRollNumber.Text;
            strRollNum.Trim();
            if (strRollNum.Equals(""))
            {
                MessageBox.Show("Kindly provide Pupil Roll Number.", "Alert Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string strName = tbName.Text;
            strName.Trim();
            if (strName.Equals(""))
            {
                MessageBox.Show("Kindly provide Pupil Name.", "Alert Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string strAge = tbAge.Text;
            strAge.Trim();
            if (strAge.Equals(""))
            {
                MessageBox.Show("Kindly provide Pupil Age.", "Alert Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string strClass = tbClass.Text;
            strClass.Trim();
            if (strClass.Equals(""))
            {
                MessageBox.Show("Kindly provide Class information.", "Alert Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string strDept = tbDept.Text;
            strDept.Trim();
            if (strDept.Equals(""))
            {
                MessageBox.Show("Kindly provide Department.", "Alert Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string strFatherName = tbFatherName.Text;
            strFatherName.Trim();
            if (strRollNum.Equals(""))
            {
                MessageBox.Show("Kindly provide Father Name.", "Alert Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string strFatherOcc = tbFatherOcc.Text;
            strFatherOcc.Trim();
            if (strFatherOcc.Equals(""))
            {
                MessageBox.Show("Kindly provide Father Occupation.", "Alert Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string strFatherContact = tbFatherContact.Text;
            strFatherContact.Trim();
            if (strFatherContact.Equals(""))
            {
                MessageBox.Show("Kindly provide Father Contact Number", "Alert Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string strMotherName = tbMotherName.Text;
            strMotherName.Trim();
            if (strMotherName.Equals(""))
            {
                MessageBox.Show("Kindly provide Mother Name.", "Alert Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string strMotherOcc = tbMotherOcc.Text;
            strMotherOcc.Trim();
            if (strMotherOcc.Equals(""))
            {
                MessageBox.Show("Kindly provide Mother Occupation", "Alert Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string strMotherContact = tbMotherContact.Text;
            strMotherName.Trim();
            if (strRollNum.Equals(""))
            {
                MessageBox.Show("Kindly provide Mother Contact Number", "Alert Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string strAddress1 = tbAddress1.Text;
            strAddress1.Trim();
            if (strAddress1.Equals(""))
            {
                MessageBox.Show("Kindly provide Address1", "Alert Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string strAddress2 = tbAddress2.Text;
            strAddress2.Trim();
            if (strAddress2.Equals(""))
            {
                MessageBox.Show("Kindly provide Address2", "Alert Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string strCity = tbCity.Text;
            strCity.Trim();
            if (strCity.Equals(""))
            {
                MessageBox.Show("Kindly provide City.", "Alert Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string strAcademicyear = tbAcademicYear.Text;
            strAcademicyear.Trim();
            if (strAcademicyear.Equals(""))
            {
                MessageBox.Show("Kindly provide the Academic Year of Admission.", "Alert Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string strRFID = tbRFID.Text;
            strRFID.Trim();
            if (strRFID.Equals(""))
            {
                MessageBox.Show("Kindly Assign a RFID.", "Alert Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
                       

            string strGender = "";
            if (rbMale.Checked == true)
            {
                strGender = "Male";
            }
            else
            {
                strGender = "Female";
            }

            DBConnect DB = new DBConnect();
            if (DB.OpenConnection())
            {
                DB.CloseConnection();
                if (!DB.InsertStudent(strRollNum, strName, strAge, strGender, strClass, strDept, strAcademicyear,
                                    strFatherName, strFatherOcc, strFatherContact,
                                    strMotherName, strMotherOcc, strMotherContact,
                                    strAddress1, strAddress2, strCity, strRFID))
                {
                    MessageBox.Show("Error in New Student Registration", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    MessageBox.Show("New Student Registration Successfull", "SAT System", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tbRollNumber.Text = "";
                    tbName.Text = "";
                    tbAge.Text = "";
                    tbClass.Text = "";
                    tbAcademicYear.Text = "";
                    tbDept.Text = "";
                    tbFatherName.Text = "";
                    tbFatherOcc.Text = "";
                    tbFatherContact.Text = "";
                    tbMotherName.Text = "";
                    tbMotherOcc.Text = "";
                    tbMotherContact.Text = "";
                    tbAddress1.Text = "";
                    tbAddress2.Text = "";
                    tbCity.Text = "";
                    tbRFID.Text = "";
                    rbFemale.Checked = false;
                    rbMale.Checked = true;
                    return;
                }
            }
            else
            {
                DB.CloseConnection();
                return;
            } 

            
        }

        private void frmRegisterStudent_Load(object sender, EventArgs e)
        {
            tbRollNumber.Text = "";
            tbName.Text = "";
            tbAge.Text = "";
            tbClass.Text = "";
            tbAcademicYear.Text = "";
            tbDept.Text = "";
            tbFatherName.Text = "";
            tbFatherOcc.Text = "";
            tbFatherContact.Text = "";
            tbMotherName.Text = "";
            tbMotherOcc.Text = "";
            tbMotherContact.Text = "";            
            tbAddress1.Text = "";
            tbAddress2.Text = "";            
            tbCity.Text = "";
            tbRFID.Text = "";
            rbFemale.Checked = false;
            rbMale.Checked = true;

            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRFID_Click(object sender, EventArgs e)
        {
            SATSystem.getInstance().mRFIDTagID = "";
            SATSystem.getInstance().mExitCode = 0;
            
            SATSystem.getInstance().frmRFIDReader1.ShowDialog();
            tbRFID.Text = SATSystem.getInstance().mRFIDTagID;

            SATSystem.getInstance().mRFIDTagID = "";
            SATSystem.getInstance().mExitCode = 0;
        }
    }
}
