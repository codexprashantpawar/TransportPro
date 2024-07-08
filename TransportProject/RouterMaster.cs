using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace TransportProject
{
    public partial class RouterMaster : Form
    {
        private string id;
        string connectionString = "Data Source=DESKTOP-7P1PBIT;Initial Catalog=tranport;Integrated Security=True";

        public RouterMaster()
        {
            InitializeComponent();
            BindCustomer();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string routeName = textBox1.Text;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();

                    if (string.IsNullOrWhiteSpace(routeName))
                    {
                        MessageBox.Show("Please enter a route name.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (button1.Text == "Save")
                    {
                        string query = "INSERT INTO routemaster (RouteName) VALUES (@RouteName)";
                        SqlCommand cmd = new SqlCommand(query, con);
                        cmd.Parameters.AddWithValue("@RouteName", routeName);

                        int result = cmd.ExecuteNonQuery();

                        if (result > 0)
                        {
                            MessageBox.Show("Route Name saved successfully!");
                            ClearForm();
                            LoadRouteMasterData();
                        }
                        else
                        {
                            MessageBox.Show("Error saving Route Name.");
                        }
                    }
                    else if (button1.Text == "Update")
                    {
                        string query = "UPDATE routemaster SET RouteName = @RouteName WHERE RouterMasterId = @RouterMasterId";
                        SqlCommand cmd = new SqlCommand(query, con);
                        cmd.Parameters.AddWithValue("@RouteName", routeName);
                        cmd.Parameters.AddWithValue("@RouterMasterId", id);

                        int result = cmd.ExecuteNonQuery();

                        if (result > 0)
                        {
                            MessageBox.Show("Route Name updated successfully!");
                            ClearForm();
                            LoadRouteMasterData();
                            button1.Text = "Save";
                        }
                        else
                        {
                            MessageBox.Show("Error updating Route Name.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
                finally
                {
                    con.Close();
                }
            }
        }

        private void LoadRouteMasterData()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();

                    string query = "SELECT RouterMasterId, RouteName FROM routemaster";
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

            LoadRouteMasterData();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == dataGridView1.Columns["Edit"].Index)
                {
                    id = dataGridView1.Rows[e.RowIndex].Cells["RouterMasterId"].Value.ToString();
                    textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells["RouteName"].Value.ToString();
                    button1.Text = "Update";
                }
                else if (e.ColumnIndex == dataGridView1.Columns["Delete"].Index)
                {
                    string routerMasterId = dataGridView1.Rows[e.RowIndex].Cells["RouterMasterId"].Value.ToString();
                    DeleteData(routerMasterId);
                }
            }
        }

        private void DeleteData(string routerMasterId)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string query = "DELETE FROM routemaster WHERE RouterMasterId = @RouterMasterId";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@RouterMasterId", int.Parse(routerMasterId));

                    int result = cmd.ExecuteNonQuery();

                    if (result > 0)
                    {
                        MessageBox.Show("Route Name deleted successfully!");
                        LoadRouteMasterData();
                    }
                    else
                    {
                        MessageBox.Show("Error deleting Route Name.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
        }

        private void ClearForm()
        {
            textBox1.Text = "";
        }

        private void RouterMaster_Load(object sender, EventArgs e)
        {
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.DefaultCellStyle.Font = new System.Drawing.Font("Arial", 12);
                column.HeaderCell.Style.Font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold);
            }
        }
    }
}
