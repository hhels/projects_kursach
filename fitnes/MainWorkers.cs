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
        public string _workers;

        public MainWorkers()
        {
            InitializeComponent();
            _workers = CurrentUser.Login;
        }

        private void MainWorkers_Load(object sender, EventArgs e)
        {
            textBox1.Text = _workers;
            dataGridView1.DataSource = DbConnector.ExecuteQuery($"  SELECT LastName, FirstName FROM Users WHERE idUsers IN  ( SELECT Clients_idUsers FROM Records WHERE idWorker = (SELECT idWorkers FROM Workers WHERE idUser = (SELECT idUsers FROM Users WHERE Login = '{textBox1.Text}')))");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
            new Start().Show();
        }
    }
}
