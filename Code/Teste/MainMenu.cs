using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Teste
{
    public partial class MenuWindow : Form
    {
        public MenuWindow()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Are you sure you want to exit ?","Exit", MessageBoxButtons.YesNo);

            if (res == DialogResult.Yes)
            {
                Application.Exit();
            }

            
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            About h = new About();
            h.Location = this.Location;
            h.ShowDialog();
        }


        private void btnChooseDomain_Click(object sender, EventArgs e)
        {
            ChooseDomain cd = new ChooseDomain();
            cd.Location = this.Location;
            this.Visible = false;
            cd.ShowDialog();
            this.Close();
        }

       
    }
}
