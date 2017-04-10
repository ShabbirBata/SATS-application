using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace SATSystem
{
    public partial class frmRFIDReader : Form
    {
        public uint uDev;
        public int size;
        public LibNFC libnfc = null;
        public frmRFIDReader()
        {
            InitializeComponent();
            uDev = 0;
            size = Marshal.SizeOf(typeof(nfc_iso14443a_info_t));
            libnfc = new LibNFC();
            nfc_device_desc_t[] devices = libnfc.list_devices(ref uDev);
            nfc_device_t con = libnfc.connect(devices[0]);
            libnfc.initiator_init();

            libnfc.configure(LibNFC.nfc_device_option_t.NDO_ACTIVATE_FIELD, false);
            libnfc.configure(LibNFC.nfc_device_option_t.NDO_INFINITE_SELECT, false);
            libnfc.configure(LibNFC.nfc_device_option_t.NDO_HANDLE_CRC, true);
            libnfc.configure(LibNFC.nfc_device_option_t.NDO_ACTIVATE_FIELD, true);
        }

        private void frmRFIDReader_Load(object sender, EventArgs e)
        {
            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += new DoWorkEventHandler(WorkThreadFunction);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);

            bw.RunWorkerAsync();

        }
        
        private void WorkThreadFunction(object sender, DoWorkEventArgs e)
        {
            nfc_target_t target = new nfc_target_t();
            while ((libnfc.initiator_select_passive_target(ref target)) == false) ;

            string sUID = "";
            for (int i = 0; i < target.nti.nai.szUidLen; i++)
            {
                sUID += Convert.ToString(target.nti.nai.abtUid[i], 16).PadLeft(2, '0');
            }

            SATSystem.getInstance().mRFIDTagID = sUID;
            SATSystem.getInstance().mExitCode = 1;
            System.Media.SystemSounds.Beep.Play();
            System.Threading.Thread.Sleep(1000);
        }

        frmLoading frmLoading1 = null;
        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            SATSystem.getInstance().mExitCode = 1;

            if (frmLoading1 == null)
            {
                frmLoading1 = new frmLoading();
                frmLoading1.strMessage = "Student RFID Card Identified";// +SATSystem.getInstance().mRFIDTagID;
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
            frmLoading1 = null;
            this.Close();
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            SATSystem.getInstance().mExitCode = 2;
            this.Close();
        }
    }
}
