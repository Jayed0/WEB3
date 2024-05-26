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
    public partial class AdminDoctorEntryForm : Form
    {
        public AdminDoctorEntryForm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AdminMainMenu f = new AdminMainMenu();
            f.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            connection con = new connection();
            con.thisConnection.Open();

            OracleDataAdapter thisAdapter = new OracleDataAdapter("SELECT * FROM Doctor_Login_Personal_Info", con.thisConnection);
            OracleCommandBuilder thisBuilder = new OracleCommandBuilder(thisAdapter);
            DataSet thisDataSet = new DataSet();
            thisAdapter.Fill(thisDataSet, "Doctor_Login_Personal_Info");
            DataRow thisRow = thisDataSet.Tables["Doctor_Login_Personal_Info"].NewRow();

            try
            {
                thisRow["Doctor_ID"] = textBox7.Text;
                thisRow["FIRSTNAME"] = textBox1.Text;
                thisRow["LASTNAME"] = textBox8.Text;
                thisRow["GENDER"] = comboBox1.Text;
                thisRow["ADDRESS"] = textBox4.Text;
                thisRow["DESIGNATION"] = textBox5.Text;
                
                thisRow["Username"] = textBox3.Text;
                thisRow["Password"] = textBox6.Text;

                thisDataSet.Tables["Doctor_Login_Personal_Info"].Rows.Add(thisRow);
                thisAdapter.Update(thisDataSet, "Doctor_Login_Personal_Info"); ;
                MessageBox.Show("New Doctor Account Created");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            AdminDoctorEntryForm f = new AdminDoctorEntryForm();
            f.Show();
            this.Hide();
        }

        private void AdminDoctorEntryForm_Load(object sender, EventArgs e)
        {
            connection CN = new connection();
            CN.thisConnection.Open();
            OracleCommand thisCommand = CN.thisConnection.CreateCommand();
            thisCommand.CommandText = "SELECT * FROM Doctor_Login_Personal_Info";
            OracleDataReader thisReader = thisCommand.ExecuteReader();
            while (thisReader.Read())
            {
                ListViewItem lsvItem = new ListViewItem();
                lsvItem.Text = thisReader["Doctor_ID"].ToString();
                lsvItem.SubItems.Add(thisReader["Username"].ToString());
                lsvItem.SubItems.Add(thisReader["Designation"].ToString());
                listView1.Items.Add(lsvItem);
            }
            CN.thisConnection.Close();
        }
    }
}
