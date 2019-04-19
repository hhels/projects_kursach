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
    public partial class Registration : Form
    {
        private DataTable _users;

        public Registration()
        {
            InitializeComponent();
            _users = DbConnector.ExecuteQuery("SELECT Login FROM Users");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
            new Start().Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            var login = _users.AsEnumerable().FirstOrDefault(x => x["Login"].ToString() == textBox6.Text);
            if (login == null)
            {
                if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text) ||
                               string.IsNullOrWhiteSpace(textBox3.Text) || string.IsNullOrWhiteSpace(textBox4.Text) || string.IsNullOrWhiteSpace(textBox5.Text) || string.IsNullOrWhiteSpace(textBox6.Text) || string.IsNullOrWhiteSpace(textBox7.Text))
                {
                    MessageBox.Show("Заполнены не все поля");
                    return;
                }
                else
                {
                    var ins = $"INSERT INTO Users(FirstName, MidName, LastName, Date, Number, Login, Password, idRole) VALUES('{textBox1.Text}', '{textBox2.Text}', '{textBox3.Text}', '{textBox4.Text}', '{textBox5.Text}','{textBox6.Text}', {textBox7.Text}, 1)";
                    DbConnector.ExecuteQuery(ins);
                    MessageBox.Show("Пользователь добавлен");
                    Hide();
                    new MainClient().Show();
                }
            }
            else
            {
                MessageBox.Show("Логин уже используется");
            }
           
        }
    }
}
