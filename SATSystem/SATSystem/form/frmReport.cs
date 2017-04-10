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
    public partial class frmReport : Form
    {
        public frmReport()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string strDate = dateTimePicker1.Value.ToString("ddMMyyyy");
            //Remove existing rows 
            int nTotalRow = dgAttedance.Rows.Count;
            for (int nRow = nTotalRow - 1; nRow >= 0; nRow--)
            {
                dgAttedance.Rows.RemoveAt(nRow);
            }

            List<string>[] listAllStudents;
            List<string>[] list;
            DBConnect DB = new DBConnect();
            list = DB.SelectStudentByDate(strDate);
            listAllStudents = DB.SelectAllStudents();

            int nRows = listAllStudents[0].Count;
            int nPresent = list[0].Count;
            for (int i = 0; i < listAllStudents[0].Count; i++)
            {
                int number = dgAttedance.Rows.Add();
                dgAttedance.Rows[number].Cells[0].Value = listAllStudents[0][i];
                dgAttedance.Rows[number].Cells[1].Value = listAllStudents[1][i];
                dgAttedance.Rows[number].Cells[2].Value = listAllStudents[2][i];
                dgAttedance.Rows[number].Cells[3].Value = listAllStudents[3][i];
                dgAttedance.Rows[number].Cells[4].Value = listAllStudents[4][i];
                for (int j = 0; j < nPresent; j++)
                {
                    if (listAllStudents[0][i].Equals(list[0][j]))
                    {
                        dgAttedance.Rows[number].Cells[4].Value = list[4][j];                        
                    }                    
                }
            }
        }

        private void frmReport_Load(object sender, EventArgs e)
        {

        }
    }
}
