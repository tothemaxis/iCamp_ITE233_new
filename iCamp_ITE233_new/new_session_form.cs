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
    public partial class new_session_form : Form
    {
        public new_session_form()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {   
            if (textBox1.TextLength > 0)
            {
                this.Close();
            }
        }
    }
}
