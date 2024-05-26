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
    public partial class AdminStaffEntryForm : Form
    {
        public AdminStaffEntryForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            connection con = new connection();
            con.thisConnection.Open();

            OracleDataAdapter thisAdapter = new OracleDataAdapter("SELECT * FROM Staff_Login_Personal_Info", con.thisConnection);
            OracleCommandBuilder thisBuilder = new OracleCommandBuilder(thisAdapter);
            DataSet thisDataSet = new DataSet();
            thisAdapter.Fill(thisDataSet, "Staff_Login_Personal_Info");
            DataRow thisRow = thisDataSet.Tables["Staff_Login_Personal_Info"].NewRow();
            
            try
            {
                thisRow["staff_id"] = textBox7.Text;
                thisRow["NAME"] = textBox1.Text;
                thisRow["DEPARTMENT"] = textBox2.Text;
                thisRow["GENDER"] = comboBox1.Text;
                thisRow["ADDRESS"] = textBox4.Text;
                thisRow["CONTACT_NO"] = textBox5.Text;

                thisRow["Username"] = textBox3.Text;
                thisRow["Password"] = textBox6.Text;

                thisDataSet.Tables["Staff_Login_Personal_Info"].Rows.Add(thisRow);
                thisAdapter.Update(thisDataSet, "Staff_Login_Personal_Info");;
                MessageBox.Show("New Staff Account Created");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }          

            AdminStaffEntryForm f = new AdminStaffEntryForm();
            f.Show();
            this.Hide();
        }

        private void StaffEntryForm_Load(object sender, EventArgs e)
        {
            connection CN = new connection();
            CN.thisConnection.Open();
            OracleCommand thisCommand = CN.thisConnection.CreateCommand();
            thisCommand.CommandText = "SELECT * FROM Staff_Login_Personal_Info";
            OracleDataReader thisReader = thisCommand.ExecuteReader();
            while (thisReader.Read())
            {
                ListViewItem lsvItem = new ListViewItem();
                lsvItem.Text = thisReader["staff_id"].ToString();
                lsvItem.SubItems.Add(thisReader["Username"].ToString());
                lsvItem.SubItems.Add(thisReader["Department"].ToString());
                listView1.Items.Add(lsvItem);
            }
            CN.thisConnection.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AdminMainMenu f = new AdminMainMenu();
            f.Show();
            this.Hide();
        }
    }
}