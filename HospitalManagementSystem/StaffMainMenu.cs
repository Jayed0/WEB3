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
    public partial class StaffMainMenu : Form
    {
        public StaffMainMenu()
        {
            InitializeComponent();
        }

        private void StaffMainMenu_Load(object sender, EventArgs e)
        {
            connection con = new connection();
            con.thisConnection.Open();
            OracleCommand thisCommand = con.thisConnection.CreateCommand();
            thisCommand.CommandText = "SELECT * FROM Doctor_Login_Personal_Info";
            OracleDataReader thisReader = thisCommand.ExecuteReader();
            while (thisReader.Read())
            {
                ListViewItem lsvItem = new ListViewItem();
                lsvItem.Text = thisReader["Doctor_ID"].ToString();
                lsvItem.SubItems.Add(thisReader["FIRSTNAME"].ToString()); 
                lsvItem.SubItems.Add(thisReader["LASTNAME"].ToString());
                lsvItem.SubItems.Add(thisReader["Designation"].ToString());
                listView2.Items.Add(lsvItem);
            }
            con.thisConnection.Close();
            connection con1 = new connection();
            con1.thisConnection.Open();
            OracleCommand thisCommand1 = con1.thisConnection.CreateCommand();
            thisCommand1.CommandText = "SELECT * FROM Patient_Info";
            OracleDataReader thisReader1 = thisCommand1.ExecuteReader();
            while (thisReader1.Read())
            {
                ListViewItem lsvItem1 = new ListViewItem();
                lsvItem1.Text = thisReader1["Patient_ID"].ToString();
                lsvItem1.SubItems.Add(thisReader1["FIRSTNAME"].ToString());
                lsvItem1.SubItems.Add(thisReader1["LASTNAME"].ToString());
                lsvItem1.SubItems.Add(thisReader1["Doctor_ID"].ToString());
                listView1.Items.Add(lsvItem1);
            }
            con1.thisConnection.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            f.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            connection con = new connection();
            con.thisConnection.Open();

            OracleDataAdapter thisAdapter = new OracleDataAdapter("SELECT * FROM Patient_Info", con.thisConnection);
            OracleCommandBuilder thisBuilder = new OracleCommandBuilder(thisAdapter);
            DataSet thisDataSet = new DataSet();
            thisAdapter.Fill(thisDataSet, "Patient_Info");
            DataRow thisRow = thisDataSet.Tables["Patient_Info"].NewRow();
            try
            {
                thisRow["Patient_ID"] = textBox7.Text;
                thisRow["FIRSTNAME"] = textBox1.Text;
                thisRow["LASTNAME"] = textBox2.Text;
                thisRow["GENDER"] = comboBox1.Text;
                thisRow["Age"] = textBox8.Text;
                thisRow["ADDRESS"] = textBox4.Text;
                thisRow["CONTACT_NO"] = textBox5.Text;
                thisRow["Doctor_ID"] = textBox3.Text;
                thisRow["APPOINTDATE"] = dateTimePicker1.Text;

                thisDataSet.Tables["Patient_Info"].Rows.Add(thisRow);
                thisAdapter.Update(thisDataSet, "Patient_Info"); ;
                MessageBox.Show("New Patient Inserted");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            StaffMainMenu f = new StaffMainMenu();
            f.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            connection c = new connection();
            c.thisConnection.Open();
            OracleCommand thisCommand = c.thisConnection.CreateCommand();
            thisCommand.CommandText = "SELECT * FROM Patient_Info where Patient_ID ='" + textBox6.Text + "'";
            OracleDataReader thisReader = thisCommand.ExecuteReader();
            DialogResult dialogResult = MessageBox.Show("Are you sure to delete?", "Confirm", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                if (thisReader.HasRows)
                {
                    thisCommand.CommandText = "Delete from Patient_Info where Patient_ID = '" + textBox6.Text + "'";
                    thisCommand.Connection = c.thisConnection;
                    thisCommand.CommandType = CommandType.Text;

                    try
                    {
                        thisCommand.ExecuteNonQuery();
                        MessageBox.Show("Patient Deleted!");

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    listView1.Refresh();
                }
            }
            else
            {
                MessageBox.Show("Patient Not Deleted!");
            }
            c.thisConnection.Close();

            StaffMainMenu f = new StaffMainMenu();
            f.Show();
            this.Hide();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            {
                if (listView1.SelectedItems.Count > 0)
                {
                    ListViewItem item = listView1.SelectedItems[0];
                    textBox6.Text = item.SubItems[0].Text;
                    textBox9.Text = item.SubItems[1].Text;
                    textBox10.Text = item.SubItems[2].Text;
                }
                else
                {
                    textBox6.Text = string.Empty;
                    textBox9.Text = string.Empty;
                    textBox10.Text = string.Empty;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            connection con = new connection();
            con.thisConnection.Open();
            OracleCommand thisCommand = con.thisConnection.CreateCommand();
            thisCommand.CommandText = "SELECT * FROM Patient_Info where Patient_ID = '" + textBox11.Text + "'";
            OracleDataReader thisReader = thisCommand.ExecuteReader();
            if (thisReader.Read())
            {
                label19.Text = (thisReader["Patient_ID"].ToString());
                label23.Text = (thisReader["APPOINTDATE"].ToString());
                label24.Text = (thisReader["Doctor_ID"].ToString());
                MessageBox.Show("Patient Found!");
            }
            else
            {
                MessageBox.Show("Patient Not Found!");
            }
            con.thisConnection.Close();
        }
    }
}
