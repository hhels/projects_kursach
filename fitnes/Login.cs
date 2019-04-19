using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace fitnes
{
    public partial class Login : Form
    {
        private DataTable _users;
        private int _loginCount;
        private const int MaximumLogin = 20;



        public Login()
        {
            InitializeComponent();
            _users = DbConnector.ExecuteQuery("SELECT * FROM Users");
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
            new Start().Show();
        }

        //private void Login_Load(object sender, EventArgs e)
        //{
        //    _users = DbConnector.ExecuteQuery("SELECT * FROM Users");
        //}
        
        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text)&&string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Заполнены не все поля");
            }
            else
            {
                _loginCount += 1;
                var login = textBox1.Text;
                var pass = textBox2.Text;

                var user = _users.AsEnumerable().FirstOrDefault(x => x["Login"].ToString() == login &&
                                                                     x["Password"].ToString() == pass);

                if (user == null)
                {
                    MessageBox.Show("Неправильный логин или пароль");
                    if (_loginCount == MaximumLogin)
                    {
                        MessageBox.Show("Превышено максимальное количество попыток авторизации. Вы забанены!!!!");
                        button1.Hide();
                    }
                    return;
                }
                else
                {
                    Hide();
                    var role = int.Parse(user["idRole"].ToString());
                    var workers = user["Login"].ToString();
                    if (role == 1)
                    {
                        new MainClientMenu().Show();
                    }
                    else if (role == 2)
                    {

                       new MainWorkers(workers).Show();
                    }
                    else
                    {
                        new MainDirector().Show();
                    }
                }
            }
        }
    }
}
