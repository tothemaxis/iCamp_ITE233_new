using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace iCamp_ITE233_new
{
    public partial class Select_activity_form : Form
    {
        SqlCommand cmd;
        SqlConnection con;
        SqlDataReader reader;
        int id = 0;

        public Select_activity_form(string selected_combo)
        {
            InitializeComponent();
            //display day as today +1
            dateTimePicker1.Value = DateTime.Today.AddDays(1);
            //display bunk name in combo according to selected camp session from Homepage
            try
            {
                con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\MAX\Documents\Git\iCamp_ITE233_new\iCamp_ITE233_new\Database1.mdf;Integrated Security=True");
                con.Open();
                cmd = new SqlCommand("SELECT id FROM camp_session WHERE camp_session.name = '" + selected_combo.ToString() + "'", con);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    id = reader.GetInt32(0); //get id of camp_session
                }
                reader.Close();
                //select distinct so that no duplicated
                cmd = new SqlCommand("SELECT DISTINCT bunk FROM camper_info WHERE camper_info.sid = '" + id + "'", con);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    bunk_combo.Items.Add(reader.GetString(0));
                }
                reader.Close();
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

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void bunk_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            //show name of campers according to selected bunk name.
            if (bunk_combo.SelectedIndex != -1)
            {
                con.Open();
                name_combo.Items.Clear();
                String bunkString = bunk_combo.SelectedItem.ToString();
                cmd = new SqlCommand("SELECT name FROM camper_info WHERE camper_info.bunk = '" + bunkString + "'", con);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    name_combo.Items.Add(reader.GetString(0)); //get id of camp_session
                }
                reader.Close();
                con.Close();
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            listBox3.Items.Clear();
            listBox4.Items.Clear();
            listBox5.Items.Clear();
            //check if date exist in DB with rows
            con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\MAX\Documents\Git\iCamp_ITE233_new\iCamp_ITE233_new\Database1.mdf;Integrated Security=True");
            con.Open();
            SqlCommand checkDate = new SqlCommand("SELECT COUNT (*) FROM reg_activity WHERE reg_activity.selected_date = @selected_date", con);
            checkDate.Parameters.AddWithValue("@selected_date", dateTimePicker1.Value.Date);
            int dateExist = (int)checkDate.ExecuteScalar();
            if (dateExist > 0)
            {
                //if has data, read and display in listboxes related to selected date.
                cmd = new SqlCommand("SELECT act_name,subject_num FROM reg_activity WHERE selected_date = '" + dateTimePicker1.Value.Date + "'", con);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (reader.GetValue(1).Equals(1))
                    {
                        listBox1.Items.Add(reader.GetValue(0).ToString());
                    }
                    else if (reader.GetValue(1).Equals(2))
                    {
                        listBox2.Items.Add(reader.GetValue(0).ToString());
                    }
                    else if (reader.GetValue(1).Equals(3))
                    {
                        listBox3.Items.Add(reader.GetValue(0).ToString());
                    }
                    else if (reader.GetValue(1).Equals(4))
                    {
                        listBox4.Items.Add(reader.GetValue(0).ToString());
                    }
                    else if (reader.GetValue(1).Equals(5))
                    {
                        listBox5.Items.Add(reader.GetValue(0).ToString());
                    }
                    else
                        MessageBox.Show("error");
                }
                reader.Close();
                con.Close();
            }
            else
                MessageBox.Show("No activity has been created for the selected date. Please create one in Register Activity.");
            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //save button, save all selected acts+name+bunk+date to DB
            con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\MAX\Documents\Git\iCamp_ITE233_new\iCamp_ITE233_new\Database1.mdf;Integrated Security=True");
            con.Open();
            String bunkName = Convert.ToString(bunk_combo.SelectedItem);
            String name = Convert.ToString(name_combo.SelectedItem);
            String act1 = Convert.ToString(listBox1.SelectedItem);
            String act2 = Convert.ToString(listBox2.SelectedItem);
            String act3 = Convert.ToString(listBox3.SelectedItem);
            String act4 = Convert.ToString(listBox4.SelectedItem);
            String act5 = Convert.ToString(listBox5.SelectedItem);
            //check before add if data already existed in DB.
            SqlCommand checkCamper = new SqlCommand("SELECT COUNT (*) FROM select_activity WHERE select_activity.bunk_name = @bunk_name AND select_activity.camper_name" +
                "= @camper_name", con);
            checkCamper.Parameters.AddWithValue("@bunk_name", bunkName);
            checkCamper.Parameters.AddWithValue("@camper_name", name);
            int dataExist = (int)checkCamper.ExecuteScalar();
            //check both combo are selected and all acts are selected.
            if (bunk_combo.SelectedIndex > -1 & name_combo.SelectedIndex > -1 & listBox1.SelectedIndex > -1 & listBox2.SelectedIndex > -1
                & listBox3.SelectedIndex > -1 & listBox4.SelectedIndex > -1 & listBox5.SelectedIndex > -1)
            {
                if (dataExist > 0)
                {
                    //if data exist promt message box yes or no
                    DialogResult dialogResult = MessageBox.Show("The data already exist in Database. Override?", "Camper Data Found in System.", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        //yess to update that row in DB
                        cmd = new SqlCommand("UPDATE select_activity SET bunk_name=@bunk_name, camper_name=@camper_name, selected_date=@selected_date, " +
                            "selected_act1=@selected_act1, selected_act2=@selected_act2,selected_act3=@selected_act3,selected_act4=@selected_act4," +
                            "selected_act5=@selected_act5 WHERE bunk_name=@bunk_name AND camper_name=@camper_name", con);
                        cmd.Parameters.AddWithValue("@bunk_name", bunkName);
                        cmd.Parameters.AddWithValue("@camper_name", name);
                        cmd.Parameters.AddWithValue("@selected_date", dateTimePicker1.Value.Date);
                        cmd.Parameters.AddWithValue("@selected_act1", act1);
                        cmd.Parameters.AddWithValue("@selected_act2", act2);
                        cmd.Parameters.AddWithValue("@selected_act3", act3);
                        cmd.Parameters.AddWithValue("@selected_act4", act4);
                        cmd.Parameters.AddWithValue("@selected_act5", act5);
                        cmd.ExecuteNonQuery();
                        this.Close();
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        //cancel.
                    }
                }
                else //if no data exist, add new.
                    cmd = new SqlCommand("INSERT INTO select_activity (bunk_name,camper_name,selected_date,selected_act1,selected_act2,selected_act3,selected_act4" +
                            ",selected_act5) VALUES ('" + bunkName + "','" + name + "','" + dateTimePicker1.Value.Date + "','" + act1 + "','" + act2 + "','" + act3 + "'," +
                            "'" + act4 + "','" + act5 + "')", con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Data Saved Successfully.");
            }
            else
                MessageBox.Show("Please select Bunk Name and Camper Name.");
        }

        private void saveNext_btn_Click(object sender, EventArgs e)
        {
            button1_Click(sender, e);
            if (name_combo.SelectedIndex < name_combo.Items.Count - 1)
            {
                int k = name_combo.SelectedIndex;
                name_combo.SelectedIndex = k + 1;
            }
        }
    }
}
