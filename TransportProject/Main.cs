using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TransportProject
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void routeMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RouterMaster routermaster = new RouterMaster();
            routermaster.Show();

        }

        private void stopMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stopmaster st = new Stopmaster();
            st.Show();
        }

        private void busStopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BustStop bus = new BustStop();
            bus.Show();
        }

        private void assignTransportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AccoutntSection ac = new AccoutntSection();
            ac.Show();
        }

        private void newAdmissionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Student st = new Student();
            st.Show();
        }

        private void cancelAdmissionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cancel can = new cancel();
            can.Show();
        }

        private void LogOut_Click(object sender, EventArgs e)
        {
            
            var result = MessageBox.Show("Are you sure you want to log out?", "Confirm Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    
                    this.Close();

                   
                    Form1 loginForm = new Form1();
                    loginForm.Show();
                }
                catch (Exception ex)
                {
                    
                    MessageBox.Show("An error occurred while logging out: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void Main_Load(object sender, EventArgs e)
        {

        }
    }
}
