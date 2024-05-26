using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HospitalManagementSystem
{
    public partial class DoctorMainMenu : Form
    {
        public DoctorMainMenu()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            f.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DoctorViewPatientForm f = new DoctorViewPatientForm();
            f.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DoctorPatientViewAppointment f = new DoctorPatientViewAppointment();
            f.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DoctorPatientDiagnosisDetailsEntry f = new DoctorPatientDiagnosisDetailsEntry();
            f.Show();
            this.Hide();
        }
    }
}
