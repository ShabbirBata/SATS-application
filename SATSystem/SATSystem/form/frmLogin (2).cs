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
    public partial class frmLogin : Form
    {
        frmLoading frmLoading1;
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            String strAdminID = tbAdminID.Text;
            String strPassword = tbPassword.Text;

            strAdminID.Trim();
            strPassword.Trim();

            if (strAdminID.Equals(""))
            {
                MessageBox.Show("Kindly provide Admin ID.", "Alert Message", MessageBoxButtons.OK, MessageBoxIcon.Error);               
                return;
            }
            if (strPassword.Equals(""))
            {
                MessageBox.Show("Kindly provide Password for validation.", "Alert Message", MessageBoxButtons.OK, MessageBoxIcon.Error);               
                return;
            }

            try
            {
                DBConnect DB = new DBConnect();
                if (DB.OpenConnection())
                {
                    DB.CloseConnection();
                    if (!DB.ValidateAdmin(strAdminID, strPassword))
                    {
                        MessageBox.Show("Invalid Userid OR Password", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        return;
                    }
                }
                else
                {
                    DB.CloseConnection();
                    return;
                }
            }catch(Exception e1)
            {
                return;
            }
            
            SATSystem.getInstance().bValidAdmin = true;
            SATSystem.getInstance().Initate();

            if (frmLoading1 == null)
            {
                frmLoading1 = new frmLoading();
                frmLoading1.strMessage = "Validating Admin Credential...";
                frmLoading1.FormBorderStyle = FormBorderStyle.None;
                frmLoading1.FormClosed += frmLoading1_FormClosed;
                frmLoading1.Show();
            }
            else
            {
                frmLoading1.Activate();
            }            
        }              

        void frmLoading1_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
            //frmLoading1 = null;
            //if (frmLandingScreen1 == null)
            //{
            //    frmLandingScreen1 = new frmLandingScreen();
            //    frmLandingScreen1.FormClosed += frmLandingScreen1_FormClosed;
            //    frmLandingScreen1.Show();                
            //}
            //else
            //{
            //    frmLandingScreen1.Activate();
            //}            
        }
        //void frmLandingScreen1_FormClosed(object sender, FormClosedEventArgs e)
        //{
        //    frmLandingScreen1 = null;
        //}

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            tbAdminID.Text = "Gaurav";
            tbPassword.Text = "welcome123";
            pb1.Image = global::SATSystem.Properties.Resources.star1;
            pb2.Image = global::SATSystem.Properties.Resources.star1;
            pb3.Image = global::SATSystem.Properties.Resources.star1;
            pb4.Image = global::SATSystem.Properties.Resources.star1;
            pb5.Image = global::SATSystem.Properties.Resources.star1;
            try
            {
                DBConnect DB = new DBConnect();
                string strRating = DB.GetRating();
                switch (strRating)
                {
                    case "0":
                        pb1.Image = global::SATSystem.Properties.Resources.star1;
                        pb2.Image = global::SATSystem.Properties.Resources.star1;
                        pb3.Image = global::SATSystem.Properties.Resources.star1;
                        pb4.Image = global::SATSystem.Properties.Resources.star1;
                        pb5.Image = global::SATSystem.Properties.Resources.star1;
                        break;
                    case "1":
                        pb1.Image = global::SATSystem.Properties.Resources.star;
                        pb2.Image = global::SATSystem.Properties.Resources.star1;
                        pb3.Image = global::SATSystem.Properties.Resources.star1;
                        pb4.Image = global::SATSystem.Properties.Resources.star1;
                        pb5.Image = global::SATSystem.Properties.Resources.star1;
                        break;
                    case "2":
                        pb1.Image = global::SATSystem.Properties.Resources.star;
                        pb2.Image = global::SATSystem.Properties.Resources.star;
                        pb3.Image = global::SATSystem.Properties.Resources.star1;
                        pb4.Image = global::SATSystem.Properties.Resources.star1;
                        pb5.Image = global::SATSystem.Properties.Resources.star1;
                        break;
                    case "3":
                        pb1.Image = global::SATSystem.Properties.Resources.star;
                        pb2.Image = global::SATSystem.Properties.Resources.star;
                        pb3.Image = global::SATSystem.Properties.Resources.star;
                        pb4.Image = global::SATSystem.Properties.Resources.star1;
                        pb5.Image = global::SATSystem.Properties.Resources.star1;
                        break;
                    case "4":
                        pb1.Image = global::SATSystem.Properties.Resources.star;
                        pb2.Image = global::SATSystem.Properties.Resources.star;
                        pb3.Image = global::SATSystem.Properties.Resources.star;
                        pb4.Image = global::SATSystem.Properties.Resources.star;
                        pb5.Image = global::SATSystem.Properties.Resources.star1;
                        break;
                    case "5":
                        pb1.Image = global::SATSystem.Properties.Resources.star;
                        pb2.Image = global::SATSystem.Properties.Resources.star;
                        pb3.Image = global::SATSystem.Properties.Resources.star;
                        pb4.Image = global::SATSystem.Properties.Resources.star;
                        pb5.Image = global::SATSystem.Properties.Resources.star;
                        break;

                }
            }
            catch (Exception e1)
            {
                return;
            }

        }

        private void pb1_Click(object sender, EventArgs e)
        {
            pb1.Image = global::SATSystem.Properties.Resources.star;
            pb2.Image = global::SATSystem.Properties.Resources.star1;
            pb3.Image = global::SATSystem.Properties.Resources.star1;
            pb4.Image = global::SATSystem.Properties.Resources.star1;
            pb5.Image = global::SATSystem.Properties.Resources.star1;
            DBConnect DB = new DBConnect();
            DB.UpdateRating(1);
            MessageBox.Show("Thanks for rating us :)", "SAT System", MessageBoxButtons.OK, MessageBoxIcon.Information);            
        }

        private void pb2_Click(object sender, EventArgs e)
        {
            pb1.Image = global::SATSystem.Properties.Resources.star;
            pb2.Image = global::SATSystem.Properties.Resources.star;
            pb3.Image = global::SATSystem.Properties.Resources.star1;
            pb4.Image = global::SATSystem.Properties.Resources.star1;
            pb5.Image = global::SATSystem.Properties.Resources.star1;
            DBConnect DB = new DBConnect();
            DB.UpdateRating(2);

            MessageBox.Show("Thanks for rating us :)", "SAT System", MessageBoxButtons.OK, MessageBoxIcon.Information); 
        }

        private void pb3_Click(object sender, EventArgs e)
        {
            pb1.Image = global::SATSystem.Properties.Resources.star;
            pb2.Image = global::SATSystem.Properties.Resources.star;
            pb3.Image = global::SATSystem.Properties.Resources.star;
            pb4.Image = global::SATSystem.Properties.Resources.star1;
            pb5.Image = global::SATSystem.Properties.Resources.star1;
            DBConnect DB = new DBConnect();
            DB.UpdateRating(3);
            MessageBox.Show("Thanks for rating us :)", "SAT System", MessageBoxButtons.OK, MessageBoxIcon.Information); 
        }

        private void pb4_Click(object sender, EventArgs e)
        {
            pb1.Image = global::SATSystem.Properties.Resources.star;
            pb2.Image = global::SATSystem.Properties.Resources.star;
            pb3.Image = global::SATSystem.Properties.Resources.star;
            pb4.Image = global::SATSystem.Properties.Resources.star;
            pb5.Image = global::SATSystem.Properties.Resources.star1;
            DBConnect DB = new DBConnect();
            DB.UpdateRating(4);
            MessageBox.Show("Thanks for rating us :)", "SAT System", MessageBoxButtons.OK, MessageBoxIcon.Information); 
        }

        private void pb5_Click(object sender, EventArgs e)
        {
            pb1.Image = global::SATSystem.Properties.Resources.star;
            pb2.Image = global::SATSystem.Properties.Resources.star;
            pb3.Image = global::SATSystem.Properties.Resources.star;
            pb4.Image = global::SATSystem.Properties.Resources.star;
            pb5.Image = global::SATSystem.Properties.Resources.star;
            DBConnect DB = new DBConnect();
            DB.UpdateRating(5);
            MessageBox.Show("Thanks for rating us :)", "SAT System", MessageBoxButtons.OK, MessageBoxIcon.Information); 
        }

        private void tbAdminID_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
