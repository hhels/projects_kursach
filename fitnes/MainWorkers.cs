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
            var hours = new[] { "9-10", "10-11", "11-12", "13-14", "14-15", "15-16" };
            var hoursSelect = string.Join(",", hours.Select(x => $"T.`{x}`"));

            var id = CurrentUser.UserId;
            var workerid = DbConnector.ExecuteScalar($"SELECT idWorkers FROM Workers WHERE idUser = {id}");
            dataGridView1.DataSource = DbConnector.ExecuteQuery($"SELECT * FROM Records WHERE idWorker = {workerid} ");
            dataGridView1.DataSource = DbConnector.ExecuteQuery($@"SELECT  T.date, W.Position, U.LastName, {hoursSelect} FROM Records AS R
JOIN Time AS T ON T.idtime = R.time_idtime
JOIN Workers AS W ON W.idWorkers = R.idWorker
JOIN Users AS U ON U.idUsers = R.Clients_idUsers
WHERE W.idWorkers = {workerid}");
        }

        private void MainWorkers_Load(object sender, EventArgs e)
        {
            textBox1.Text = _workers;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
            new Start().Show();
        }
    }
}
