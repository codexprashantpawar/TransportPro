using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Collections;
using System.Collections.Generic;
namespace TransportProject
{
    public partial class AccoutntSection : Form
    {
        string connectionString = "Data Source=DESKTOP-7P1PBIT;Initial Catalog=tranport;Integrated Security=True";

        public AccoutntSection()
        {
            InitializeComponent();
            LoadRouteNames();
            LoadStopNames(); 
            LoadStudentNames(); 
            LoadDropStopNames(); 
            BindCustomer();
            LoadData();
            LoadPick();
            LoadPick1();
            Loadstud();
            LoadClass();
            LoadBoard();
            routename();
           
            comboBox4.SelectedIndexChanged += comboBox4_SelectedIndexChanged;
            comboBox5.SelectedIndexChanged += comboBox5_SelectedIndexChanged;
            comboBox8.SelectedIndexChanged += comboBox8_SelectedIndexChanged;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string query = "INSERT INTO AccountSection (StudentYear, Board, Class, StudentName, PickupStop, DropStop, PickupTime, DropTime, RouteName, Amount, TotalAmount) VALUES (@StudentYear, @Board, @Class, @StudentName, @PickupStop, @DropStop, @PickupTime, @DropTime, @RouteName, @Amount, @TotalAmount)";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@StudentYear", datetimePacker1.Value);
                    cmd.Parameters.AddWithValue("@Board", comboBox1.Text);
                    cmd.Parameters.AddWithValue("@Class", comboBox9.Text);
                    cmd.Parameters.AddWithValue("@StudentName", comboBox3.Text);
                    cmd.Parameters.AddWithValue("@PickupStop", comboBox4.Text);
                    cmd.Parameters.AddWithValue("@DropStop", comboBox5.Text);
                    cmd.Parameters.AddWithValue("@PickupTime", comboBox6.Text);
                    cmd.Parameters.AddWithValue("@DropTime", comboBox7.Text);
                    cmd.Parameters.AddWithValue("@RouteName", comboBox8.Text);
                    cmd.Parameters.AddWithValue("@Amount", textBox1.Text);
                    cmd.Parameters.AddWithValue("@TotalAmount", textBox2.Text);

                    int result = cmd.ExecuteNonQuery();

                    if (result > 0)
                    {
                        MessageBox.Show("Account section details saved successfully!");
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show("Error saving account section details.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void LoadRouteNames()
        {
            comboBox8.Items.Clear();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string query = "SELECT RouteName FROM routemaster";
                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        comboBox8.Items.Add(reader["RouteName"].ToString());
                    }
                    reader.Close();
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
            comboBox4.Items.Clear();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string query = "SELECT StopName FROM stopmaster";
                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        comboBox4.Items.Add(reader["StopName"].ToString());
                    }
                    reader.Close();
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

        private void LoadStudentNames()
        {
            comboBox3.Items.Clear();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string query = "SELECT FullName FROM student";
                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        comboBox3.Items.Add(reader["FullName"].ToString());
                    }
                    reader.Close();
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

        private void LoadDropStopNames()
        {
            comboBox5.Items.Clear();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string query = "SELECT StopName FROM busstop";
                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        comboBox5.Items.Add(reader["StopName"].ToString());
                    }
                    reader.Close();
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

        private void comboBox8_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedRouteName = comboBox8.Text;
            if (!string.IsNullOrEmpty(selectedRouteName))
            {
                FetchAmount(selectedRouteName);
            }
        }

        private void FetchAmount(string routeName)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string queryBusStop = "SELECT Amount, StopName FROM busstop WHERE RouteName = @RouteName";
                    SqlCommand cmdBusStop = new SqlCommand(queryBusStop, con);
                    cmdBusStop.Parameters.AddWithValue("@RouteName", routeName);
                    SqlDataReader readerBusStop = cmdBusStop.ExecuteReader();

                    List<string> stopNames = new List<string>();
                    List<string> dropStops = new List<string>();

                    bool dataFound = false;

                    while (readerBusStop.Read())
                    {
                        string amount = readerBusStop["Amount"].ToString();
                        string stopName = readerBusStop["StopName"].ToString();
                        string dropStop = readerBusStop["StopName"].ToString();

                        textBox1.Text = amount;
                        textBox2.Text = amount;

                        if (!stopNames.Contains(stopName))
                        {
                            stopNames.Add(stopName);
                        }

                        if (!dropStops.Contains(dropStop))
                        {
                            dropStops.Add(dropStop);
                        }

                        dataFound = true;
                    }

                    if (dataFound)
                    {
                        comboBox4.DataSource = stopNames;
                        comboBox5.DataSource = dropStops;

                        if (comboBox4.Items.Count > 0)
                        {
                            comboBox4.SelectedIndex = 0;
                        }
                        if (comboBox5.Items.Count > 0)
                        {
                            comboBox5.SelectedIndex = 0;
                        }
                    }
                    else
                    {
                        textBox1.Text = "";
                        textBox2.Text = "";
                        comboBox4.DataSource = null;
                        comboBox5.DataSource = null;
                    }

                    readerBusStop.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while fetching data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void AccoutntSection_Load(object sender, EventArgs e)
        {
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.DefaultCellStyle.Font = new System.Drawing.Font("Arial", 12);
                column.HeaderCell.Style.Font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) { }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e) { }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e) { }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e) { }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e) { }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e) { }

        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e) { }

        private void datetimePacker1_ValueChanged(object sender, EventArgs e) { }

        private void textBox1_TextChanged(object sender, EventArgs e) { }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (dataGridView1.Columns[e.ColumnIndex].Name == "Edit")
                {
                    if (dataGridView1.Rows[e.RowIndex].Cells["RouteName"].Value != null &&
                        dataGridView1.Rows[e.RowIndex].Cells["DropStop"].Value != null &&
                        dataGridView1.Rows[e.RowIndex].Cells["TotalAmount"].Value != null)
                    {
                        string routeName = dataGridView1.Rows[e.RowIndex].Cells["RouteName"].Value.ToString();
                        string dropStop = dataGridView1.Rows[e.RowIndex].Cells["DropStop"].Value.ToString();
                        string totalAmount = dataGridView1.Rows[e.RowIndex].Cells["TotalAmount"].Value.ToString();

                        comboBox8.Text = routeName;
                        comboBox5.Text = dropStop;
                        textBox2.Text = totalAmount;
                    }
                }
                else if (dataGridView1.Columns[e.ColumnIndex].Name == "Delete")
                {
                    if (dataGridView1.Rows[e.RowIndex].Cells["RouteName"].Value != null &&
                        dataGridView1.Rows[e.RowIndex].Cells["DropStop"].Value != null &&
                        dataGridView1.Rows[e.RowIndex].Cells["TotalAmount"].Value != null)
                    {
                        string routeName = dataGridView1.Rows[e.RowIndex].Cells["RouteName"].Value.ToString();
                        string dropStop = dataGridView1.Rows[e.RowIndex].Cells["DropStop"].Value.ToString();
                        string totalAmount = dataGridView1.Rows[e.RowIndex].Cells["TotalAmount"].Value.ToString();

                        DeleteData(routeName, dropStop, totalAmount);
                    }
                }
            }
        }

        private void DeleteData(string routeName, string dropStop, string totalAmount)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string query = "DELETE FROM AccountSection WHERE RouteName=@RouteName AND DropStop=@DropStop AND TotalAmount=@TotalAmount";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@RouteName", routeName);
                        cmd.Parameters.AddWithValue("@DropStop", dropStop);
                        cmd.Parameters.AddWithValue("@TotalAmount", totalAmount);
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

        private void LoadData()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string query = "SELECT RouteName, DropStop, TotalAmount FROM AccountSection";
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

        private void BindCustomer()
        {
            DataGridViewButtonColumn editButtonColumn = new DataGridViewButtonColumn();
            editButtonColumn.Name = "Edit";
            editButtonColumn.Text = "Edit";
            editButtonColumn.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Add(editButtonColumn);

            DataGridViewButtonColumn deleteButtonColumn = new DataGridViewButtonColumn();
            deleteButtonColumn.Name = "Delete";
            deleteButtonColumn.Text = "Delete";
            deleteButtonColumn.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Add(deleteButtonColumn);

            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            comboBox1.Text = "";
            comboBox3.Text = "";
            comboBox4.Text = "";
            comboBox5.Text = "";
            comboBox6.Text = "";
            comboBox7.Text = "";
            comboBox8.Text = "";
            comboBox9.Text = "";
            textBox1.Text = "";
            textBox2.Text = "";
        }

        private void LoadPick()
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

                    comboBox4.DataSource = null;
                    comboBox4.Items.Clear();
                    comboBox4.DataSource = dt;
                    comboBox4.DisplayMember = "StopName";
                    comboBox4.ValueMember = "StopName";
                    comboBox4.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    comboBox4.AutoCompleteSource = AutoCompleteSource.ListItems;
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


        private void routename()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    string query = "SELECT RouteName FROM routemaster";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);


                    if (dt.Columns.Contains("RouteName"))
                    {
                        comboBox8.DataSource = dt;
                        comboBox8.DisplayMember = "RouteName";
                        comboBox8.ValueMember = "RouteName";
                        comboBox8.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        comboBox8.AutoCompleteSource = AutoCompleteSource.ListItems;
                    }
                    else
                    {
                        MessageBox.Show("DataTable does not contain 'StopName' column.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while loading data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void  LoadPick1()
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

                    comboBox5.DataSource = null;
                    comboBox5.Items.Clear();
                    comboBox5.DataSource = dt;
                    comboBox5.DisplayMember = "StopName";
                    comboBox5.ValueMember = "StopName";
                    comboBox5.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    comboBox5.AutoCompleteSource = AutoCompleteSource.ListItems;
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

        private void Loadstud()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    string query = "SELECT FullName FROM student";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);


                    if (dt.Columns.Contains("FullName"))
                    {
                        comboBox3.DataSource = dt;
                        comboBox3.DisplayMember = "FullName";
                        comboBox3.ValueMember = "FullName";
                        comboBox3.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        comboBox3.AutoCompleteSource = AutoCompleteSource.ListItems;
                    }
                    else
                    {
                        MessageBox.Show("DataTable does not contain 'StopName' column.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while loading data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }



        private void LoadClass()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    string query = "SELECT Class FROM student";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);


                    if (dt.Columns.Contains("Class"))
                    {
                        comboBox9.DataSource = dt;
                        comboBox9.DisplayMember = "Class";
                        comboBox9.ValueMember = "Class";
                        comboBox9.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        comboBox9.AutoCompleteSource = AutoCompleteSource.ListItems;
                    }
                    else
                    {
                        MessageBox.Show("DataTable does not contain 'StopName' column.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while loading data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void LoadBoard()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    string query = "SELECT Board FROM student";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);


                    if (dt.Columns.Contains("Board"))
                    {
                        comboBox1.DataSource = dt;
                        comboBox1.DisplayMember = "Board";
                        comboBox1.ValueMember = "Board";
                        comboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        comboBox1.AutoCompleteSource = AutoCompleteSource.ListItems;
                    }
                    else
                    {
                        MessageBox.Show("DataTable does not contain 'StopName' column.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while loading data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void comboBox9_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            comboBox9.Font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold);
        }

    }
}
