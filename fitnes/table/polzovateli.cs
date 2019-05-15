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
    public partial class polzovateli : Form
    {
        public polzovateli()
        {
            InitializeComponent();
            comboBox1.DataSource = DbConnector.ExecuteQuery($"SELECT * FROM Users");
            comboBox1.DisplayMember = "idUsers";
            comboBox1.ValueMember = "idUsers";


            comboBox2.DataSource = DbConnector.ExecuteQuery($"SELECT * FROM Roles");
            comboBox2.DisplayMember = "Name";
            comboBox2.ValueMember = "idRoles";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var user = (comboBox1.SelectedItem as DataRowView).Row;
            textBox1.Text = user["FirstName"].ToString();
            textBox2.Text = user["MidName"].ToString();
            textBox3.Text = user["LastName"].ToString();
            textBox4.Text = user["Date"].ToString();
            textBox5.Text = user["Number"].ToString();
            comboBox2.SelectedValue = user["idRole"];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DbConnector.ExecuteQuery($"UPDATE Users SET FirstName = '{textBox1.Text}', MidName = '{textBox2.Text}', LastName = '{textBox3.Text}', Date = '{textBox4.Text}', Number = '{textBox5.Text}', idRole = '{comboBox2.SelectedValue}' WHERE idUsers = '{comboBox1.SelectedValue}' ");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DbConnector.ExecuteQuery($"DELETE FROM Users WHERE idUsers = '{comboBox1.SelectedValue}'");
        }
    }
}
