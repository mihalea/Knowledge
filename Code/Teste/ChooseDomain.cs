using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Teste
{
    public partial class ChooseDomain : Form
    {
        public ChooseDomain()
        {
            InitializeComponent();
        }

        private void ChooseDomain_Closing(object sender, EventArgs e)
        {
            //Application.Exit() ;
        }

        private void ChooseDomain_Load(object sender, EventArgs e)
        {
            StreamReader input = new StreamReader("intrebari/domains.dat");

            string str = input.ReadLine();

            int domains = Convert.ToInt32(str);
            lbxDomains.Items.Clear();

            for (int i = 0; i < domains; i++)
            {
                str = input.ReadLine();
                lbxDomains.Items.Add(str);

            }

        }

        private void lbxDomains_SelectedIndexChanged(object sender, EventArgs e)
        {
            //label1.Text = lbxDomains.SelectedIndex.ToString();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (lbxDomains.SelectedIndex == -1)
            {
                MessageBox.Show("Please select an Item first!");
                return;
            }


            TestingChamber tc = new TestingChamber(lbxDomains.SelectedIndex);
            tc.Location = this.Location;
            this.Visible = false;
            tc.ShowDialog();
            this.Close();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            MenuWindow mw = new MenuWindow();
            mw.Location = this.Location;
            this.Visible = false;
            mw.ShowDialog();
            this.Close();
        }
    }
}
