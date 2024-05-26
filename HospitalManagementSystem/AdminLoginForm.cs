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
    public partial class AdminLoginForm : Form
    {
        public AdminLoginForm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            f.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            connection CN = new connection();
            CN.thisConnection.Open();
            OracleCommand thisCommand = new OracleCommand();
            thisCommand.Connection = CN.thisConnection;
            thisCommand.CommandText = "SELECT * FROM Admin_Login WHERE Username='" + textBox1.Text + "' AND Password='" + textBox2.Text + "'";
            OracleDataReader thisReader = thisCommand.ExecuteReader();

            if (thisReader.Read())
            {
                AdminMainMenu f = new AdminMainMenu();
                f.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Username or Password Incorrect");
            }
            CN.thisConnection.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
        }
    }
 }