using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;

namespace iCamp_ITE233_new
{
    public partial class Home_form : Form
    {
        SqlCommand cmd;
        SqlConnection con;
        SqlDataReader reader;

        public Home_form()
        {
            InitializeComponent();
            //display day as today +1
            dateTimePicker1.Value = DateTime.Today.AddDays(1);
            try
            {
                con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\MAX\Documents\Git\iCamp_ITE233_new\iCamp_ITE233_new\Database1.mdf;Integrated Security=True");
                con.Open();
                cmd = new SqlCommand("SELECT name FROM camp_session", con);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    combo_sessionName.Items.Add(reader.GetString(0)); // get the information as a string at the column index 0
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error: " + ex.ToString());
            }
            finally // what to do after try/catch is done
            {
                if (con != null)
                {
                    con.Close();
                }
            }
            combo_sessionName.SelectedIndex = combo_sessionName.Items.Count - 1;
        }

        private void new_btn_Click(object sender, EventArgs e)
        {
            this.Hide();
            New_session_form newName = new New_session_form();
            newName.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //show register form
            var selected_combo = combo_sessionName.SelectedItem.ToString();
            Register_form regisForm = new Register_form(selected_combo);
            regisForm.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var selected_combo = combo_sessionName.SelectedItem.ToString();
            Select_activity_form chooseForm = new Select_activity_form(selected_combo);
            chooseForm.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Register_activity_form regisAform = new Register_activity_form();
            regisAform.ShowDialog();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }
    }
}
