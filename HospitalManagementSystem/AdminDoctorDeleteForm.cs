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
    public partial class AdminDoctorDeleteForm : Form
    {
        public AdminDoctorDeleteForm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AdminMainMenu f = new AdminMainMenu();
            f.Show();
            this.Hide();
        }

        private void AdminDoctorDeleteForm_Load(object sender, EventArgs e)
        {
            connection CN = new connection();
            CN.thisConnection.Open();
            OracleCommand thisCommand = CN.thisConnection.CreateCommand();
            thisCommand.CommandText = "SELECT * FROM Doctor_Login_Personal_Info";
            OracleDataReader thisReader = thisCommand.ExecuteReader();
            while (thisReader.Read())
            {
                ListViewItem lsvItem = new ListViewItem();
                lsvItem.Text = thisReader["doctor_id"].ToString();
                lsvItem.SubItems.Add(thisReader["Username"].ToString());
                lsvItem.SubItems.Add(thisReader["Designation"].ToString());
                listView1.Items.Add(lsvItem);
            }
            CN.thisConnection.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            connection con = new connection();
            con.thisConnection.Open();
            OracleCommand thisCommand = con.thisConnection.CreateCommand();
            thisCommand.CommandText = "SELECT * FROM Doctor_Login_Personal_Info where Doctor_ID ='" + textBox1.Text + "' or Username ='" + textBox2.Text + "'";
            OracleDataReader thisReader = thisCommand.ExecuteReader();
            if (thisReader.HasRows)
            {
                thisCommand.CommandText = "Delete from Doctor_Login_Personal_Info where Doctor_ID = '" + this.textBox1.Text + "' or Username = '" + this.textBox2.Text + "'";
                thisCommand.Connection = con.thisConnection;
                thisCommand.CommandType = CommandType.Text;

                try
                {
                    thisCommand.ExecuteNonQuery();
                    MessageBox.Show("Doctor Account Deleted!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Doctor Account Not Found!");
            }
            con.thisConnection.Close();
            AdminDoctorDeleteForm f = new AdminDoctorDeleteForm();
            f.Show();
            this.Hide();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem item = listView1.SelectedItems[0];
                textBox1.Text = item.SubItems[0].Text;
                textBox2.Text = item.SubItems[1].Text;
            }
            else
            {
                textBox1.Text = string.Empty;
                textBox2.Text = string.Empty;
            }
        }
    }
}
