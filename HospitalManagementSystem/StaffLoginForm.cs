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
    public partial class StaffLoginForm : Form
    {
        public StaffLoginForm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            f.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            connection con = new connection();

            con.thisConnection.Open();
            OracleCommand thisCommand = new OracleCommand();
            thisCommand.Connection = con.thisConnection;
            thisCommand.CommandText = "SELECT * FROM Staff_Login_Personal_Info WHERE Username='" + textBox1.Text + "' AND Password='" + textBox2.Text + "'";
            OracleDataReader thisReader = thisCommand.ExecuteReader();

            if (thisReader.Read())
            {
                StaffMainMenu f = new StaffMainMenu();
                f.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Username or Password Incorrect");
            }
            con.thisConnection.Close();
        }
    }
}
