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
    public partial class Register_activity_form : Form
    {
        SqlCommand cmd;
        SqlConnection con;
        SqlDataReader reader;
        DateTime selectedDate;
        /*note for future:
            show message if selected date out of range for that camp session*/

        public Register_activity_form()
        {
            InitializeComponent();
            //display day as today +1
            dateTimePicker1.Value = DateTime.Today.AddDays(1);
            selectedDate = dateTimePicker1.Value;
            //combo_subject list
            subject_combo.Items.Add("-SELECT-");
            subject_combo.Items.Add("Subject 1");
            subject_combo.Items.Add("Subject 2");
            subject_combo.Items.Add("Subject 3");
            subject_combo.Items.Add("Subject 4");
            subject_combo.Items.Add("Subject 5");
            subject_combo.SelectedIndex = 0;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            String trim = addActivity_txt.Text.Trim();
            //add activity btn
            if (!listBox1.Text.Contains(trim) & !listBox2.Text.Contains(trim) & !listBox3.Text.Contains(trim)
                & !listBox4.Text.Contains(trim) & !listBox5.Text.Contains(trim))
            {
                if (addActivity_txt.TextLength > 0 & subject_combo.SelectedIndex == 1)
                {
                    cmd = new SqlCommand("INSERT INTO reg_activity (act_name, selected_date,subject_num) VALUES ('" + trim + "','" + dateTimePicker1.Value.Date + "',1)", con);
                    cmd.ExecuteNonQuery();
                    listBox1.Items.Add(trim);
                }
                else if (addActivity_txt.TextLength > 0 & subject_combo.SelectedIndex == 2)
                {
                    cmd = new SqlCommand("INSERT INTO reg_activity (act_name, selected_date,subject_num) VALUES ('" + trim + "','" + dateTimePicker1.Value.Date + "',2)", con);
                    cmd.ExecuteNonQuery();
                    listBox2.Items.Add(trim);
                }
                else if (addActivity_txt.TextLength > 0 & subject_combo.SelectedIndex == 3)
                {
                    cmd = new SqlCommand("INSERT INTO reg_activity (act_name, selected_date,subject_num) VALUES ('" + trim + "','" + dateTimePicker1.Value.Date + "',3)", con);
                    cmd.ExecuteNonQuery();
                    listBox3.Items.Add(trim);
                }
                else if (addActivity_txt.TextLength > 0 & subject_combo.SelectedIndex == 4)
                {
                    cmd = new SqlCommand("INSERT INTO reg_activity (act_name, selected_date,subject_num) VALUES ('" + trim + "','" + dateTimePicker1.Value.Date + "',4)", con);
                    cmd.ExecuteNonQuery();
                    listBox4.Items.Add(trim);
                }
                else if (addActivity_txt.TextLength > 0 & subject_combo.SelectedIndex == 5)
                {
                    cmd = new SqlCommand("INSERT INTO reg_activity (act_name, selected_date,subject_num) VALUES ('" + trim + "','" + dateTimePicker1.Value.Date + "',5)", con);
                    cmd.ExecuteNonQuery();
                    listBox5.Items.Add(trim);
                }
                else
                    MessageBox.Show("Please select subject to add to.");
            }
            else
                MessageBox.Show("Already exist in the list.");
        }

        private void delS1_btn_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1 & listBox1.Items.Count > 0)
            {
                cmd = new SqlCommand("DELETE FROM reg_activity WHERE  reg_activity.act_name = '" + listBox1.SelectedItem + "' AND reg_activity.subject_num = 1", con);
                cmd.ExecuteNonQuery();
                listBox1.Items.Remove(listBox1.SelectedItem);
            }
            else
                MessageBox.Show("Please select activity to delete from list 1");
        }

        private void delS2_btn_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex != -1 & listBox2.Items.Count > 0)
            {
                cmd = new SqlCommand("DELETE FROM reg_activity WHERE  reg_activity.act_name = '" + listBox2.SelectedItem + "' AND reg_activity.subject_num = 2", con);
                cmd.ExecuteNonQuery();
                listBox2.Items.Remove(listBox2.SelectedItem);
            }
            else
                MessageBox.Show("Please select activity to delete from list 2");
        }

        private void delS3_btn_Click(object sender, EventArgs e)
        {
            if (listBox3.SelectedIndex != -1 & listBox3.Items.Count > 0)
            {
                cmd = new SqlCommand("DELETE FROM reg_activity WHERE  reg_activity.act_name = '" + listBox3.SelectedItem + "' AND reg_activity.subject_num = 3", con);
                cmd.ExecuteNonQuery();
                listBox3.Items.Remove(listBox3.SelectedItem);
            }
            else
                MessageBox.Show("Please select activity to delete from list 3");
        }

        private void delS4_btn_Click(object sender, EventArgs e)
        {
            if (listBox4.SelectedIndex != -1 & listBox4.Items.Count > 0)
            {
                cmd = new SqlCommand("DELETE FROM reg_activity WHERE  reg_activity.act_name = '" + listBox4.SelectedItem + "' AND reg_activity.subject_num = 4", con);
                cmd.ExecuteNonQuery();
                listBox4.Items.Remove(listBox4.SelectedItem);
            }
            else
                MessageBox.Show("Please select activity to delete from list 4");
        }

        private void delS5_btn_Click(object sender, EventArgs e)
        {
            if (listBox5.SelectedIndex != -1 & listBox5.Items.Count > 0)
            {
                cmd = new SqlCommand("DELETE FROM reg_activity WHERE  reg_activity.act_name = '" + listBox5.SelectedItem + "' AND reg_activity.subject_num = 5", con);
                cmd.ExecuteNonQuery();
                listBox5.Items.Remove(listBox5.SelectedItem);
            }
            else
                MessageBox.Show("Please select activity to delete from list 5");
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            //since set defaut display as tomorrow = value changed. so
            //clear lists first.
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            listBox3.Items.Clear();
            listBox4.Items.Clear();
            listBox5.Items.Clear();
            //default activities array
            String[] list_act = { "Tennis", "Basketball", "Gaga Ball", "Volleyball", "Football", "Cat-Walk", "Arts & Crafts", "Performing Arts", "Drumming", "Hula Hoop"
            , "Football Game", "Water Park", "Horse Riding", "Boxing", "Treasure Hunt"};
            con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\MAX\Documents\Git\iCamp_ITE233_new\iCamp_ITE233_new\Database1.mdf;Integrated Security=True");
            con.Open();
            //check if date exist in DB with rows
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
            }
            else
                //add default activities if no data exist in selected date.(i know it looks silly)
                for (int i = 0; i < list_act.Length; i++)
                {
                    if (i >= 0 & i < 3)
                    {
                        cmd = new SqlCommand("INSERT INTO reg_activity (act_name, selected_date,subject_num) VALUES ('" + list_act[i] + "','" + dateTimePicker1.Value.Date + "','1')", con);
                        listBox1.Items.Add(list_act[i]);
                    }
                    else if (i >= 3 & i < 6)
                    {
                        cmd = new SqlCommand("INSERT INTO reg_activity (act_name, selected_date,subject_num) VALUES ('" + list_act[i] + "','" + dateTimePicker1.Value.Date + "','2')", con);
                        listBox2.Items.Add(list_act[i]);
                    }
                    else if (i >= 6 & i < 9)
                    {
                        cmd = new SqlCommand("INSERT INTO reg_activity (act_name, selected_date,subject_num) VALUES ('" + list_act[i] + "','" + dateTimePicker1.Value.Date + "','3')", con);
                        listBox3.Items.Add(list_act[i]);
                    }
                    else if (i >= 9 & i < 12)
                    {
                        cmd = new SqlCommand("INSERT INTO reg_activity (act_name, selected_date,subject_num) VALUES ('" + list_act[i] + "','" + dateTimePicker1.Value.Date + "','4')", con);
                        listBox4.Items.Add(list_act[i]);
                    }
                    else if (i >= 12 & i < 15)
                    {
                        cmd = new SqlCommand("INSERT INTO reg_activity (act_name, selected_date,subject_num) VALUES ('" + list_act[i] + "','" + dateTimePicker1.Value.Date + "','5')", con);
                        listBox5.Items.Add(list_act[i]);
                    }
                    cmd.ExecuteNonQuery();
                }
        }
    }
}
