using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

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

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) && string.IsNullOrWhiteSpace(textBox2.Text))
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
                    CurrentUser.Role = int.Parse(user["idRole"].ToString());
                    CurrentUser.Login = user["Login"].ToString();
                    CurrentUser.UserId = int.Parse(user["idUsers"].ToString());
                    if (CurrentUser.Role == 1)
                    {
                        new MainClientMenu().Show();
                    }
                    else if (CurrentUser.Role == 2)
                    {

                        new MainWorkers().Show();
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
