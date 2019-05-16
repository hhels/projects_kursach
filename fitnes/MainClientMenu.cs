using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace fitnes
{
    public partial class MainClientMenu : Form
    {
        public MainClientMenu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
            new MainClient().Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
            new MainClientGrup().Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Hide();
            new MainRecords().Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Start().Show();
        }
    }
}
