using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace iCamp_ite233
{
    public partial class home_form : Form
    {

        public home_form()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            register_form f1 = new register_form();
            f1.ShowDialog();
        }

        private void new_btn_Click(object sender, EventArgs e)
        {
            new_session_form newName = new new_session_form();
            newName.ShowDialog();
        }
    }
}
