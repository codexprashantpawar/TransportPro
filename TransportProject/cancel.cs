using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace TransportProject
{
    public partial class cancel : Form
    {
        private SqlConnection connection;
        private string connectionString = "Data Source=DESKTOP-7P1PBIT;Initial Catalog=tranport;Integrated Security=True"; // Update with your connection string

        public cancel()
        {
            InitializeComponent();
            LoadStudentNames();
        }

        private void LoadStudentNames()
        {
            using (connection = new SqlConnection(connectionString))
            {
                string query = "SELECT FullName FROM student";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                comboBox1.DataSource = dt;
                comboBox1.DisplayMember = "FullName";
                comboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox1.AutoCompleteSource = AutoCompleteSource.ListItems;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1)
            {
                string selectedStudent = comboBox1.Text;
                LoadStudentDetails(selectedStudent);
            }
        }

        private void LoadStudentDetails(string fullName)
        {
            using (connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM student WHERE FullName = @FullName";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@FullName", fullName);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    textBox1.Text = reader["FullName"].ToString();
                    comboBox2.Text = reader["FatherName"].ToString();
                    dateTimePicker1.Value = Convert.ToDateTime(reader["DOB"]);
                    textBox2.Text = reader["Address"].ToString();
                }
                connection.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string selectedStudent = comboBox1.Text;

            if (string.IsNullOrEmpty(selectedStudent))
            {
                MessageBox.Show("Please select a student to cancel admission.");
                return;
            }

            
            CancelAdmission(selectedStudent);
        }

        private void CancelAdmission(string fullName)
        {
            using (connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM student WHERE FullName = @FullName";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@FullName", fullName);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                connection.Close();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Admission cancelled successfully.");
                    LoadStudentNames(); 
                    ClearFields();
                }
                else
                {
                    MessageBox.Show("Failed to cancel admission.");
                }
            }
        }

        private void ClearFields()
        {
            textBox1.Clear();
            comboBox2.SelectedIndex = -1;
            textBox2.Clear();
            dateTimePicker1.Value = DateTime.Today;
        }

        
        
        

        private void cancel_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }
    }
}
