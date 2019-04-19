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
    public partial class MainWorkers : Form
    {
        //public string Txt
        //{
        //    get { return textBox1.Text; }
        //    set { textBox1.Text = value; }
        //}
        public string _workers;

        public MainWorkers(string workers)
        {
            InitializeComponent();
            _workers = workers;
        }

        private void MainWorkers_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = DbConnector.ExecuteQuery("SELECT * FROM Records").AsEnumerable();
            textBox1.Text = _workers;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
            new Start().Show();
        }
    }
}
