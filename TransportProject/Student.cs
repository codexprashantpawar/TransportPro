using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace TransportProject
{
    public partial class Student : Form
    {
        public Student()
        {
            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(comboBox1.Text) ||
                string.IsNullOrWhiteSpace(textBox3.Text) ||
                string.IsNullOrWhiteSpace(comboBox1.Text))
            {
                MessageBox.Show("Please fill all the fields.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-7P1PBIT;Initial Catalog=tranport;Integrated Security=True");
            try
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand("insert into student (FullName,FatherName,Class,DOB,Address) values(@FullName,@FatherName,@Class,@DOB,@Address)", con))
                {
                    // cmd.Parameters.AddWithValue("@StudentId", int.Parse(textBox2.Text)); // StudentId is an identity column, do not insert it manually.
                    cmd.Parameters.AddWithValue("@FullName", textBox1.Text);
                    cmd.Parameters.AddWithValue("@FatherName", textBox3.Text);
                    cmd.Parameters.AddWithValue("@Class", comboBox1.Text);
                    cmd.Parameters.Add("@DOB", SqlDbType.Date).Value = dateTimePicker1.Value.Date; 
                    cmd.Parameters.AddWithValue("@Address", textBox4.Text);
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Data Saved Successfully");
            }
            catch (SqlException ex)
            {
                MessageBox.Show("An error occurred while saving data to the SQL Server: " + ex.Message, "SQL Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {


        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-7P1PBIT;Initial Catalog=tranport;Integrated Security=True"))
            {
                try
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM student", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable table = new DataTable();
                        da.Fill(table);
                        dataGridView1.DataSource = table;
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("An error occurred while retrieving data from the SQL Server: " + ex.Message, "SQL Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An unexpected error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Student_Load(object sender, EventArgs e)
        {
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.DefaultCellStyle.Font = new System.Drawing.Font("Arial", 12);
                column.HeaderCell.Style.Font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold);
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
            openFileDialog1.Title = "Select an Image";


            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                pictureBox1.ImageLocation = openFileDialog1.FileName;
            }
        }



    }
}
