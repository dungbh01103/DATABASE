using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Iphone
{
    public partial class Login : Form
    {
        SqlConnection conn;
        public Login()
        {
            InitializeComponent();
            createConnection();
        }
        private void createConnection()
        {
            try
            {
                String stringConnection = "Server=DESKTOP-TMCDUUR\\LUCKDAT;Database=Iphone; Integrated Security = true";
                conn = new SqlConnection(stringConnection);
                MessageBox.Show(" Connection Successful");
            }
            catch (Exception ex)
            {
                MessageBox.Show(" Erorr createconnection " + ex.Message);
            }

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to exit ?", "Ok", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
            else
            {
                return;
            }
        }

        private void btnlogin_Click(object sender, EventArgs e)
        {
            try
            {
                string Username = txtusername.Text;
                string Password = txtpassword.Text;

                conn.Open();

                // Create SQL command
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                // Use parameters in SQL statement to avoid SQL Injection
                cmd.CommandText = "SELECT * FROM Employees WHERE Username = @Username AND Password = @Password";
                cmd.Parameters.Add("@Username", SqlDbType.VarChar).Value = Username;
                cmd.Parameters.Add("@Password", SqlDbType.VarChar).Value = Password;

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    MessageBox.Show("Login successful!");
                    Home HomeForm = new Home();
                    HomeForm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Incorrect username or password!");
                }

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred in btnLogin_Click: " + ex.Message);
            }

        }
    }
}
