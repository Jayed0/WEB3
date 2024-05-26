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
    public partial class AdminStaffDeleteForm : Form
    {
        public AdminStaffDeleteForm()
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
            OracleCommand thisCommand = con.thisConnection.CreateCommand();
            thisCommand.CommandText = "SELECT * FROM Staff_Login_Personal_Info where Staff_id ='" + textBox1.Text + "' or Username ='" + textBox2.Text + "'";
            OracleDataReader thisReader = thisCommand.ExecuteReader();
            if (thisReader.HasRows)
            {
                thisCommand.CommandText = "Delete from Staff_Login_Personal_Info where Staff_id = '" + this.textBox1.Text + "' or Username = '" + this.textBox2.Text + "'";
                thisCommand.Connection = con.thisConnection;
                thisCommand.CommandType = CommandType.Text;

                try
                {
                    thisCommand.ExecuteNonQuery();
                    MessageBox.Show("Staff Account Deleted!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Staff Account Not Found!");
            }
            con.thisConnection.Close();
            AdminStaffDeleteForm f = new AdminStaffDeleteForm();
            f.Show();
            this.Hide();
        }

        private void AdminStaffDeleteForm_Load(object sender, EventArgs e)
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
