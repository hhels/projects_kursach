using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using fitnes.table;

namespace fitnes
{
    public partial class MainDirector : Form
    {
        public MainDirector()
        {
            InitializeComponent();
            label5.Text = DbConnector.ExecuteScalar($"SELECT COUNT(*) FROM Clients");
            label6.Text = DbConnector.ExecuteScalar($"SELECT COUNT(*) FROM Workers");
            var year = DateTime.Now.Year;
            var month = DateTime.Now.Month;
            //label7.Text = (Convert.ToInt32(DbConnector.ExecuteScalar($"SELECT COUNT(*) FROM Records WHERE Date >= {year}.{month}.01")) * 2000).ToString();
            label8.Text = (Convert.ToInt32(label6.Text) * 20000).ToString();
            dataGridView1.DataSource = DbConnector.ExecuteQuery($"SELECT * FROM Users");
            dataGridView3.DataSource = DbConnector.ExecuteQuery($"SELECT * FROM Clients");
            dataGridView4.DataSource = DbConnector.ExecuteQuery($"SELECT * FROM Workers");
            dataGridView5.DataSource = DbConnector.ExecuteQuery($"SELECT * FROM Time");
            dataGridView6.DataSource = DbConnector.ExecuteQuery($"SELECT * FROM Records");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new polzovateli().Show();
            //var n = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            //var nn = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            //var nnn = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            //var nnnn = dataGridView1.CurrentRow.Cells[3].Value.ToString();

            //MessageBox.Show(nn);
            //DbConnector.ExecuteQuery($"UPDATE Users SET FirstName = '{nn}' WHERE idUsers = {n} ");
            //dataGridView1.DataSource = DbConnector.ExecuteQuery($"SELECT * FROM Users");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new clienti().Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new rabotniki().Show();
        }


    }
}
