using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Drawing.Printing;

namespace TransportProject
{
    public partial class PrintForm : Form
    {
        public PrintForm()
        {
            InitializeComponent();
        }

        private void PrintForm_Load(object sender, EventArgs e)
        {

        }

        public void prinbus()
        {
            using (SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-7P1PBIT;Initial Catalog=tranport;Integrated Security=True"))
            {
                try
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand("select * from dbo.stopmaster;;", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable table = new DataTable();
                        da.Fill(table);
                        CrystalReport1 rpt = new CrystalReport1();
                        rpt.Database.Tables["Stop Name"].SetDataSource(table);
                       crystalReportViewer1.ReportSource = rpt;
                        this.crystalReportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
                        crystalReportViewer1.Visible = true;
                    }
                }
                catch
                { }
            }
        }

        private void crystalReportViewer1_Load(object sender, System.EventArgs e)
        {

        }

    }
}
