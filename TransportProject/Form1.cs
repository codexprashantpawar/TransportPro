using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace TransportProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            textBox2.PasswordChar = '*';
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=DESKTOP-7P1PBIT;Initial Catalog=tranport;Integrated Security=True";
            string username = textBox1.Text;
            string password = textBox2.Text;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    string query = "SELECT COUNT(1) FROM userrT WHERE name=@username AND password=@password";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);

                    con.Open();
                    int count = Convert.ToInt32(cmd.ExecuteScalar());

                    if (count == 1)
                    {
                        MessageBox.Show("Login Successful!");
                        Main main = new Main();
                        main.Show();
                        this.Hide();  // Hide the login form
                    }
                    else
                    {
                        MessageBox.Show("Login Failed! Please check your username and password.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Initialization code, if needed
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=DESKTOP-7P1PBIT;Initial Catalog=tranport;Integrated Security=True";
            string newUsername = textBox1.Text;
            string newPassword = textBox2.Text;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    string checkQuery = "SELECT COUNT(1) FROM userrT WHERE name = @username";
                    SqlCommand checkCmd = new SqlCommand(checkQuery, con);
                    checkCmd.Parameters.AddWithValue("@username", newUsername);

                    con.Open();
                    int userExists = Convert.ToInt32(checkCmd.ExecuteScalar());

                    if (userExists > 0)
                    {
                        MessageBox.Show("User already exists. Please choose a different username.");
                    }
                    else
                    {
                        string insertQuery = "INSERT INTO userrT (name, password) VALUES (@username, @password)";
                        SqlCommand insertCmd = new SqlCommand(insertQuery, con);
                        insertCmd.Parameters.AddWithValue("@username", newUsername);
                        insertCmd.Parameters.AddWithValue("@password", newPassword);

                        int result = insertCmd.ExecuteNonQuery();

                        if (result > 0)
                        {
                            MessageBox.Show("New user registered successfully!");
                            textBox1.Text = "";
                            textBox2.Text = "";
                        }
                        else
                        {
                            MessageBox.Show("User registration failed. Please try again.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
        }
    }
}
