using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace TransportProject
{
    public partial class Stopmaster : Form
    {
        private string id;

        public Stopmaster()
        {
            InitializeComponent();
            BindCustomer();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=DESKTOP-7P1PBIT;Initial Catalog=tranport;Integrated Security=True";
            string stopName = textBox1.Text.Trim();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();

                    if (string.IsNullOrWhiteSpace(stopName))
                    {
                        MessageBox.Show("Please enter a stop name.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (button1.Text == "Save")
                    {
                        string query = "INSERT INTO stopmaster (StopName) VALUES (@StopName)";
                        SqlCommand cmd = new SqlCommand(query, con);
                        cmd.Parameters.AddWithValue("@StopName", stopName);

                        int result = cmd.ExecuteNonQuery();

                        if (result > 0)
                        {
                            MessageBox.Show("Stop Name saved successfully!");
                            ClearForm();
                            LoadStopMasterData();
                        }
                        else
                        {
                            MessageBox.Show("Error saving Stop Name.");
                        }
                    }
                    else if (button1.Text == "Update")
                    {
                        string query = "UPDATE stopmaster SET StopName = @StopName WHERE StopMasterId = @StopMasterId";
                        SqlCommand cmd = new SqlCommand(query, con);
                        cmd.Parameters.AddWithValue("@StopName", stopName);
                        cmd.Parameters.AddWithValue("@StopMasterId", id);

                        int result = cmd.ExecuteNonQuery();

                        if (result > 0)
                        {
                            MessageBox.Show("Stop Name updated successfully!");
                            ClearForm();
                            LoadStopMasterData();
                            button1.Text = "Save";
                        }
                        else
                        {
                            MessageBox.Show("Error updating Stop Name.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    con.Close();
                }
            }
        }

        private void LoadStopMasterData()
        {
            string connectionString = "Data Source=DESKTOP-7P1PBIT;Initial Catalog=tranport;Integrated Security=True";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();

                    string query = "SELECT StopMasterId, StopName FROM stopmaster";
                    SqlDataAdapter da = new SqlDataAdapter(query, con);
                    DataTable table = new DataTable();
                    da.Fill(table);
                    dataGridView1.DataSource = table;
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

        private void BindCustomer()
        {
            dataGridView1.Columns.Clear();

            DataGridViewButtonColumn editButtonColumn = new DataGridViewButtonColumn
            {
                Name = "Edit",
                HeaderText = "Edit",
                Text = "Edit",
                UseColumnTextForButtonValue = true
            };
            dataGridView1.Columns.Add(editButtonColumn);

            DataGridViewButtonColumn deleteButtonColumn = new DataGridViewButtonColumn
            {
                Name = "Delete",
                HeaderText = "Delete",
                Text = "Delete",
                UseColumnTextForButtonValue = true
            };
            dataGridView1.Columns.Add(deleteButtonColumn);

            LoadStopMasterData();
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == dataGridView1.Columns["Edit"].Index)
                {
                    id = dataGridView1.Rows[e.RowIndex].Cells["StopMasterId"].Value.ToString();
                    textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells["StopName"].Value.ToString();
                    button1.Text = "Update";
                }
                else if (e.ColumnIndex == dataGridView1.Columns["Delete"].Index)
                {
                    string stopMasterId = dataGridView1.Rows[e.RowIndex].Cells["StopMasterId"].Value.ToString();
                    DeleteData(stopMasterId);
                }
            }
        }

        private void DeleteData(string stopMasterId)
        {
            string connectionString = "Data Source=DESKTOP-7P1PBIT;Initial Catalog=tranport;Integrated Security=True";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string query = "DELETE FROM stopmaster WHERE StopMasterId = @StopMasterId";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@StopMasterId", int.Parse(stopMasterId));

                    int result = cmd.ExecuteNonQuery();

                    if (result > 0)
                    {
                        MessageBox.Show("Stop Name deleted successfully!");
                        LoadStopMasterData();
                    }
                    else
                    {
                        MessageBox.Show("Error deleting Stop Name.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ClearForm()
        {
            textBox1.Text = "";
        }

        private void Stopmaster_Load(object sender, EventArgs e)
        {
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.DefaultCellStyle.Font = new System.Drawing.Font("Arial", 12);
                column.HeaderCell.Style.Font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        private void btn_print_Click(object sender, EventArgs e)
        {
            PrintForm ob = new PrintForm();
            ob.prinbus();
            ob.Show();
        }
    }
}
