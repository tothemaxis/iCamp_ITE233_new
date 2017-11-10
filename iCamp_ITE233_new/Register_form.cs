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
    public partial class Register_form : Form
    {
        SqlCommand cmd;
        SqlConnection con;
        SqlDataReader reader;
        int id = 0;

        public Register_form(string selected_combo)
        {
            InitializeComponent();

            con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\MAX\Documents\Git\iCamp_ITE233_new\iCamp_ITE233_new\Database1.mdf;Integrated Security=True");
            con.Open();
            cmd = new SqlCommand("SELECT name FROM camp_session WHERE camp_session.name = '" + selected_combo + "'", con);
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Cname_label.Text = reader.GetString(0); 
            }
            reader.Close();
        }

        private void save_btn_Click(object sender, EventArgs e)
        {
            cmd = new SqlCommand("SELECT id FROM camp_session WHERE camp_session.name = '" + Cname_label.Text + "'", con);
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
               id = reader.GetInt32(0);
            }
            reader.Close();
            if (name_txt.TextLength != 0 & bunk_txt.TextLength != 0 & age_txt.TextLength != 0
                & startDate_txt.TextLength != 0 & endDate_txt.TextLength != 0 & p1Name_txt.TextLength != 0
                & p1Phone_txt.TextLength != 0 & p2Name_txt.TextLength != 0 & p2Phone_txt.TextLength != 0)
            {
                //promt yes/no dialog.
                DialogResult dialogResult = MessageBox.Show("Are You Sure to Save?", "Saving Camper Info", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    //yess and save to DB and close.(too lazy for parameters)
                    cmd = new SqlCommand("INSERT INTO camper_info (name,prefered_name,bunk,age,nationality,restriction,start_date,end_date," +
                        "medication,transportation,p1name,p1phone,p1email,p2name,p2phone,p2email,sid) VALUES ('" + name_txt.Text + "','" + prename_txt.Text + "','" + bunk_txt.Text + "'," +
                        "'" + age_txt.Text + "','" + national_txt.Text + "','" + restrict_txt.Text + "','" + startDate_txt.Text + "','" + endDate_txt.Text + "','" + medicate_txt.Text + "','" + transport_txt.Text + "'," +
                        "'" + p1Name_txt.Text + "','" + p1Phone_txt.Text + "','" + p1Mail_txt.Text + "','" + p2Name_txt.Text + "','" + p2Phone_txt.Text + "','" + p2Mail_txt.Text + "','" + id + "')", con);
                    cmd.ExecuteNonQuery();
                    this.Close();
                }
                else if (dialogResult == DialogResult.No)
                {
                    //cancel.
                }
            }
            else
                MessageBox.Show("Please fill in all required(*) infomation.");
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
