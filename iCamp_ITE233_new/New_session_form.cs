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
    public partial class New_session_form : Form
    {
        SqlCommand cmd;
        SqlConnection con;
        

        public New_session_form()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.TextLength > 0)
            {
                con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\MAX\Documents\Git\iCamp_ITE233_new\iCamp_ITE233_new\Database1.mdf;Integrated Security=True");
                con.Open();
                String trim = textBox1.Text.Trim();
                cmd = new SqlCommand("INSERT INTO camp_session (name,start_date,end_date) VALUES ('" + trim + "','" + startdate_pick.Value.Date + "','" + enddate_pick.Value.Date + "')", con);
                cmd.ExecuteNonQuery();
                Home_form backhome = new Home_form();
                backhome.Show();
                this.Close();
            }
        }
    }
}
