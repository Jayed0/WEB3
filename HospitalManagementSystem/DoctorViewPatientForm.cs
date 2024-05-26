using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OracleClient;

namespace HospitalManagementSystem
{
    public partial class DoctorViewPatientForm : Form
    {
        public DoctorViewPatientForm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DoctorMainMenu f = new DoctorMainMenu();
            f.Show();
            this.Hide();
        }

        private void DoctorViewPatientForm_Load(object sender, EventArgs e)
        {
            connection con = new connection();
            con.thisConnection.Open();
            OracleCommand thisCommand = con.thisConnection.CreateCommand();
            thisCommand.CommandText = "SELECT * FROM Patient_Info";
            OracleDataReader thisReader = thisCommand.ExecuteReader();
            while (thisReader.Read())
            {
                ListViewItem lsvItem = new ListViewItem();
                lsvItem.Text = thisReader["Patient_ID"].ToString();
                lsvItem.SubItems.Add(thisReader["FIRSTNAME"].ToString());
                lsvItem.SubItems.Add(thisReader["Doctor_ID"].ToString());
                listView1.Items.Add(lsvItem);
            }
            con.thisConnection.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            connection con = new connection();
            con.thisConnection.Open();
            OracleCommand thisCommand = con.thisConnection.CreateCommand();
            thisCommand.CommandText = "SELECT * FROM Patient_Info where Patient_ID ='" + textBox1.Text + "'";
            OracleDataReader thisReader = thisCommand.ExecuteReader();
            if (thisReader.HasRows)
            {
                listView1.Items.Clear();
                while (thisReader.Read())
                {
                    ListViewItem lsvItem = new ListViewItem();
                    lsvItem.Text = thisReader["Patient_ID"].ToString();
                    lsvItem.SubItems.Add(thisReader["FIRSTNAME"].ToString());
                    lsvItem.SubItems.Add(thisReader["Doctor_ID"].ToString());
                    listView1.Items.Add(lsvItem);
                    MessageBox.Show("Patient Found!");
                }
            }
            else
            {
                MessageBox.Show("Patient Not Found!");
            }
            con.thisConnection.Close();
        }
    }
}
