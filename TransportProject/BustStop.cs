using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace TransportProject
{
    public partial class BustStop : Form
    {
        private string connectionString = "Data Source=DESKTOP-7P1PBIT;Initial Catalog=tranport;Integrated Security=True";
        private string hiddenId = string.Empty;

        public BustStop()
        {
            InitializeComponent();
            LoadData();
            LoadRouteNames();
            LoadStopNames();
            BindCustomer();
        }

        private void LoadData()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string query = "SELECT RouteId, RouteName, StopName, Amount FROM busstop";
                    SqlDataAdapter da = new SqlDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("An SQL error occurred: " + ex.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
        }

        private void LoadRouteNames()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string query = "SELECT RouteName FROM routemaster";
                    SqlDataAdapter da = new SqlDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    comboBox1.DataSource = null; 
                    comboBox1.Items.Clear(); 
                    comboBox1.DataSource = dt;
                    comboBox1.DisplayMember = "RouteName";
                    comboBox1.ValueMember = "RouteName";
                    comboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    comboBox1.AutoCompleteSource = AutoCompleteSource.ListItems;
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("An SQL error occurred: " + ex.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
        }

        private void LoadStopNames()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string query = "SELECT StopName FROM stopmaster";
                    SqlDataAdapter da = new SqlDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    comboBox2.DataSource = null; 
                    comboBox2.Items.Clear(); 
                    comboBox2.DataSource = dt;
                    comboBox2.DisplayMember = "StopName";
                    comboBox2.ValueMember = "StopName";
                    comboBox2.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    comboBox2.AutoCompleteSource = AutoCompleteSource.ListItems;
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("An SQL error occurred: " + ex.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
        }

        private void BindCustomer()
        {
            DataGridViewButtonColumn editButtonColumn = new DataGridViewButtonColumn
            {
                Name = "Edit",
                Text = "Edit",
                UseColumnTextForButtonValue = true
            };
            dataGridView1.Columns.Add(editButtonColumn);

            DataGridViewButtonColumn deleteButtonColumn = new DataGridViewButtonColumn
            {
                Name = "Delete",
                Text = "Delete",
                UseColumnTextForButtonValue = true
            };
            dataGridView1.Columns.Add(deleteButtonColumn);

            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == dataGridView1.Columns["Edit"].Index)
                {
                    if (dataGridView1.Rows[e.RowIndex].Cells["RouteId"].Value != null)
                    {
                        hiddenId = dataGridView1.Rows[e.RowIndex].Cells["RouteId"].Value.ToString();
                        comboBox1.Text = dataGridView1.Rows[e.RowIndex].Cells["RouteName"].Value.ToString();
                        comboBox2.Text = dataGridView1.Rows[e.RowIndex].Cells["StopName"].Value.ToString();
                        textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells["Amount"].Value.ToString();
                        save.Text = "Update";
                    }
                }
                else if (e.ColumnIndex == dataGridView1.Columns["Delete"].Index)
                {
                    if (dataGridView1.Rows[e.RowIndex].Cells["RouteId"].Value != null)
                    {
                        string id = dataGridView1.Rows[e.RowIndex].Cells["RouteId"].Value.ToString();
                        DeleteData(id);
                    }
                }
            }
        }

        private void DeleteData(string id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("DELETE FROM busstop WHERE RouteId=@RouteId", con))
                    {
                        cmd.Parameters.AddWithValue("@RouteId", int.Parse(id));
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Record Deleted Successfully", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("An error occurred while deleting data from the SQL Server: " + ex.Message, "SQL Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An unexpected error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void save_Click(object sender, EventArgs e)
        {
            string routeName = comboBox1.Text;
            string stopName = comboBox2.Text;
            string amount = textBox1.Text;

            if (string.IsNullOrWhiteSpace(routeName) || string.IsNullOrWhiteSpace(stopName) || string.IsNullOrWhiteSpace(amount))
            {
                MessageBox.Show("Please fill in all fields.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();

                    if (save.Text == "Update")
                    {
                        string updateQuery = "UPDATE busstop SET RouteName = @RouteName, StopName = @StopName, Amount = @Amount WHERE RouteId = @RouteId";
                        SqlCommand cmd = new SqlCommand(updateQuery, con);
                        cmd.Parameters.AddWithValue("@RouteName", routeName);
                        cmd.Parameters.AddWithValue("@StopName", stopName);
                        cmd.Parameters.AddWithValue("@Amount", amount);
                        cmd.Parameters.AddWithValue("@RouteId", int.Parse(hiddenId));

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Data updated successfully!");
                            LoadData();
                            save.Text = "Save";
                        }
                        else
                        {
                            MessageBox.Show("Failed to update data.");
                        }
                    }
                    else
                    {
                        string checkQuery = "SELECT COUNT(*) FROM busstop WHERE RouteName = @RouteName AND StopName = @StopName";
                        SqlCommand checkCmd = new SqlCommand(checkQuery, con);
                        checkCmd.Parameters.AddWithValue("@RouteName", routeName);
                        checkCmd.Parameters.AddWithValue("@StopName", stopName);

                        int count = (int)checkCmd.ExecuteScalar();

                        if (count > 0)
                        {
                            MessageBox.Show("Record already exists.");
                            return;
                        }

                        string insertQuery = "INSERT INTO busstop (RouteName, StopName, Amount) VALUES (@RouteName, @StopName, @Amount); SELECT SCOPE_IDENTITY();";
                        SqlCommand cmd = new SqlCommand(insertQuery, con);
                        cmd.Parameters.AddWithValue("@RouteName", routeName);
                        cmd.Parameters.AddWithValue("@StopName", stopName);
                        cmd.Parameters.AddWithValue("@Amount", amount);

                        int newRouteId = Convert.ToInt32(cmd.ExecuteScalar());

                        if (newRouteId > 0)
                        {
                            MessageBox.Show("Data inserted successfully!");
                            LoadData();

                            if (!comboBox1.Items.Contains(routeName))
                                comboBox1.Items.Add(routeName);

                            if (!comboBox2.Items.Contains(stopName))
                                comboBox2.Items.Add(stopName);

                            comboBox1.Text = routeName;
                            comboBox2.Text = stopName;
                        }
                        else
                        {
                            MessageBox.Show("Failed to insert data.");
                        }
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("An SQL error occurred: " + ex.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
        }

        private void BustStop_Load(object sender, EventArgs e)
        {
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.DefaultCellStyle.Font = new System.Drawing.Font("Arial", 12);
                column.HeaderCell.Style.Font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
    }
}
