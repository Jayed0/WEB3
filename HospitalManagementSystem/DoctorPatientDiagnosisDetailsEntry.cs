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
    public partial class DoctorPatientDiagnosisDetailsEntry : Form
    {
        public DoctorPatientDiagnosisDetailsEntry()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DoctorMainMenu f = new DoctorMainMenu();
            f.Show();
            this.Hide();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            connection con = new connection();
            con.thisConnection.Open();

            OracleDataAdapter thisAdapter = new OracleDataAdapter("SELECT * FROM Patient_Diagnosis_Details", con.thisConnection);
            OracleCommandBuilder thisBuilder = new OracleCommandBuilder(thisAdapter);

            DataSet thisDataSet = new DataSet();
            thisAdapter.Fill(thisDataSet, "Patient_Diagnosis_Details");

            DataRow thisRow = thisDataSet.Tables["Patient_Diagnosis_Details"].NewRow();
            try
            {
                thisRow["Patient_ID"] = textBox7.Text;
                thisRow["Diagnosis_Details"] = textBox1.Text;
                thisRow["Diagnosis_Date"] = dateTimePicker1.Text;
                thisRow["Patient_Condition"] = textBox2.Text;
                thisRow["Doctor_ID"] = textBox4.Text;
                thisRow["Next_Appointment_Date"] = dateTimePicker2.Text;

                thisDataSet.Tables["Patient_Diagnosis_Details"].Rows.Add(thisRow);

                thisAdapter.Update(thisDataSet, "Patient_Diagnosis_Details");
                MessageBox.Show("Data Entry Successfully!");
            }
            catch (Exception ex)
            {
                DialogResult dialogResult = MessageBox.Show("Please fill up the form correctly.Do you want to return to form?", "Error", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    DoctorPatientDiagnosisDetailsEntry fr1 = new DoctorPatientDiagnosisDetailsEntry();
                    fr1.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Data Not Entry Successfully!");
                }
            }
            DoctorPatientDiagnosisDetailsEntry fr = new DoctorPatientDiagnosisDetailsEntry();
            fr.Show();
            this.Hide();
        }

        private void DoctorPatientDiagnosisDetailsEntry_Load(object sender, EventArgs e)
        {
            connection CN = new connection();
            CN.thisConnection.Open();
            OracleCommand thisCommand = CN.thisConnection.CreateCommand();
            thisCommand.CommandText = "SELECT * FROM Patient_Diagnosis_Details";
            OracleDataReader thisReader = thisCommand.ExecuteReader();
            while (thisReader.Read())
            {
                ListViewItem lsvItem = new ListViewItem();
                lsvItem.Text = thisReader["Patient_ID"].ToString();
                lsvItem.SubItems.Add(thisReader["Diagnosis_Details"].ToString());
                lsvItem.SubItems.Add(thisReader["Diagnosis_Date"].ToString());
                lsvItem.SubItems.Add(thisReader["Patient_Condition"].ToString());
                lsvItem.SubItems.Add(thisReader["Doctor_ID"].ToString());
                lsvItem.SubItems.Add(thisReader["Next_Appointment_Date"].ToString());
                listView1.Items.Add(lsvItem);
            }
            CN.thisConnection.Close();
        }
    }
}
