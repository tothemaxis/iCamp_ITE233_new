using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace iCamp_ITE233_new
{
    public partial class Home_form : Form
    {
        public Home_form()
        {
            InitializeComponent();
        }

        private void new_btn_Click(object sender, EventArgs e)
        {
            New_session_form newName = new New_session_form();
            newName.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Register_form f1 = new Register_form();
            f1.ShowDialog();
        }
    }
}
