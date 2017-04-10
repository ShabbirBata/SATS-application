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
    public partial class frmLandingScreen : Form
    {
        frmRegisterStudent frmRegisterStudent1 = null;
        frmAbout frmAbout1 = null;
        frmReport frmReport1 = null;
        public frmLandingScreen()
        {
            InitializeComponent();
        }

        private void frmLandingScreen_Load(object sender, EventArgs e)
        {
            this.toolStripStatusLabel2.Text = "      " + System.DateTime.Now.ToLongDateString() + "    " + System.DateTime.Now.ToLongTimeString() + "      ";
            statusMsg.Refresh();

            DisplayButtons(true);
        }

        public void DisplayButtons(bool bState)
        {
            btnRegisterStudent.Visible = bState;
            btnRecordAttendance.Visible = bState;
            btnReport.Visible = bState;
            btnAbout.Visible = bState;
            btnExit.Visible = bState;
            btnView.Visible = bState;

            this.menuStrip1.Visible = bState;
                        
            if (bState)
            {
                this.BackgroundImage = global::SATSystem.Properties.Resources.bcknd;                    
            }
            else
            {
                this.BackgroundImage = global::SATSystem.Properties.Resources.bcknd1;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.toolStripStatusLabel2.Text = "      " + System.DateTime.Now.ToLongDateString() + "    " + System.DateTime.Now.ToLongTimeString() + "      ";
            statusMsg.Refresh();
        }

        
        
        /// <summary>
        /// Register Student
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRegisterStudent_Click(object sender, EventArgs e)
        {
            if (frmRegisterStudent1 == null)
            {
                frmRegisterStudent1 = new frmRegisterStudent();
                //frmRegisterStudent1.MdiParent = this;
                frmRegisterStudent1.FormBorderStyle = FormBorderStyle.None;
                frmRegisterStudent1.FormClosed += frmRegisterStudent1_FormClosed;
                this.DisplayButtons(false);
                frmRegisterStudent1.ShowDialog();
            }
            else
            {
                frmRegisterStudent1.Activate();
            }
        }
        private void registerStudentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (frmRegisterStudent1 == null)
            {
                frmRegisterStudent1 = new frmRegisterStudent();
                //frmRegisterStudent1.MdiParent = this;
                frmRegisterStudent1.FormBorderStyle = FormBorderStyle.None;
                frmRegisterStudent1.FormClosed += frmRegisterStudent1_FormClosed;
                this.DisplayButtons(false);
                frmRegisterStudent1.ShowDialog();
            }
            else
            {
                frmRegisterStudent1.Activate();
            }   
        }

        void frmRegisterStudent1_FormClosed(object sender, FormClosedEventArgs e)
        {
            frmRegisterStudent1 = null;
            this.DisplayButtons(true);
        }


        /// <summary>
        /// record Attendance
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRecordAttendance_Click(object sender, EventArgs e)
        {
            //string strDate = DateTime.Now.ToString("ddMMyyyy");
            //MessageBox.Show("strDate : " + strDate, "SAT System", MessageBoxButtons.OK, MessageBoxIcon.Information);
            SATSystem.getInstance().StartAttendance();
        }

        private void recordAttendanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SATSystem.getInstance().StartAttendance();
        }


        /// <summary>
        /// Report
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void btnReport_Click(object sender, EventArgs e)
        {
            if (frmReport1 == null)
            {
                frmReport1 = new frmReport();
                frmReport1.FormBorderStyle = FormBorderStyle.None;
                frmReport1.FormClosed += frmReport1_FormClosed;
                this.DisplayButtons(false);
                frmReport1.ShowDialog();
            }
            else
            {
                frmReport1.Activate();
            } 
        }

        private void reportByTodayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (frmReport1 == null)
            {
                frmReport1 = new frmReport();
                frmReport1.FormBorderStyle = FormBorderStyle.None;
                frmReport1.FormClosed += frmReport1_FormClosed;
                this.DisplayButtons(false);
                frmReport1.ShowDialog();
            }
            else
            {
                frmReport1.Activate();
            } 
        }  

        void frmReport1_FormClosed(object sender, FormClosedEventArgs e)
        {
            frmReport1 = null;
            this.DisplayButtons(true);
        }

        /// <summary>
        /// About SAT
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        
        private void btnAbout_Click(object sender, EventArgs e)
        {
            if (frmAbout1 == null)
            {
                frmAbout1 = new frmAbout();
                //frmAbout1.MdiParent = this;
                frmAbout1.FormBorderStyle = FormBorderStyle.None;
                frmAbout1.FormClosed += frmAbout1_FormClosed;
                this.DisplayButtons(false);
                frmAbout1.ShowDialog();
            }
            else
            {
                frmAbout1.Activate();
            } 
        }
        private void aboutSATToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (frmAbout1 == null)
            {
                frmAbout1 = new frmAbout();
                //frmAbout1.MdiParent = this;
                frmAbout1.FormBorderStyle = FormBorderStyle.None;
                frmAbout1.FormClosed += frmAbout1_FormClosed;
                this.DisplayButtons(false);
                frmAbout1.ShowDialog();
            }
            else
            {
                frmAbout1.Activate();
            }
        }
        void frmAbout1_FormClosed(object sender, FormClosedEventArgs e)
        {
            frmAbout1 = null;
            this.DisplayButtons(true);
        }


        /// <summary>
        /// Exit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to quit SAT System? ", "Alert Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to quit SAT System? ", "Alert Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        frmViewStudent frmViewStudent1;

        private void btnView_Click(object sender, EventArgs e)
        {
            if (frmViewStudent1 == null)
            {
                frmViewStudent1 = new frmViewStudent();                
                frmViewStudent1.FormBorderStyle = FormBorderStyle.None;
                frmViewStudent1.FormClosed += frmViewStudent1_FormClosed;
                this.DisplayButtons(false);
                frmViewStudent1.ShowDialog();
            }
            else
            {
                frmViewStudent1.Activate();
            }
        }

        void frmViewStudent1_FormClosed(object sender, FormClosedEventArgs e)
        {
            frmViewStudent1 = null;
            this.DisplayButtons(true);
        }

        private void viewStudentInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (frmViewStudent1 == null)
            {
                frmViewStudent1 = new frmViewStudent();
                frmViewStudent1.FormBorderStyle = FormBorderStyle.None;
                frmViewStudent1.FormClosed += frmViewStudent1_FormClosed;
                this.DisplayButtons(false);
                frmViewStudent1.ShowDialog();
            }
            else
            {
                frmViewStudent1.Activate();
            }
        }

              
    }
}
