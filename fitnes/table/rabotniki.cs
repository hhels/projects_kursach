using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace fitnes.table
{
    public partial class rabotniki : Form
    {
        public rabotniki()
        {
            InitializeComponent();
            comboBox1.DataSource = DbConnector.ExecuteQuery($"SELECT * FROM Workers");
            comboBox1.DisplayMember = "idWorkers";
            comboBox1.ValueMember = "idWorkers";

            comboBox2.DataSource = DbConnector.ExecuteQuery($"SELECT * FROM Users");
            comboBox2.DisplayMember = "LastName";
            comboBox2.ValueMember = "idUsers";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var work = (comboBox1.SelectedItem as DataRowView).Row;
            textBox1.Text = work["Position"].ToString();
            textBox2.Text = work["Description"].ToString();
            textBox3.Text = work["Exp"].ToString();
            comboBox2.SelectedValue = work["idUser"].ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DbConnector.ExecuteQuery($"UPDATE Workers SET Position = '{textBox1.Text}', Description = '{textBox2.Text}', Exp = '{textBox3.Text}', idUser = '{comboBox2.SelectedValue}' WHERE idWorkers = '{comboBox1.SelectedValue}'");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DbConnector.ExecuteQuery($"DELETE FROM Workers WHERE idWorkers = '{comboBox1.SelectedValue}'");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DbConnector.ExecuteQuery($"INSERT INTO Workers(Position, Description, Exp, idUser) VALUES('{textBox1.Text}', '{textBox2.Text}', '{textBox3.Text}', '{comboBox2.SelectedValue}') ");
        }
    }
}
