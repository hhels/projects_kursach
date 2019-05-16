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
    public partial class MainRecords : Form
    {
        public MainRecords()
        {
            InitializeComponent();

            UpdateDatagrid();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var recordsId = comboBox1.Text;
            var timeId = DbConnector.ExecuteScalar($"SELECT time_idtime FROM Records WHERE idRecords = {recordsId}");

            DbConnector.ExecuteUpdate($"DELETE FROM Records WHERE idRecords = {recordsId}");
            DbConnector.ExecuteUpdate($"DELETE FROM Time WHERE idTime = {timeId}");
            MessageBox.Show("ДАНННЫЕ УДАЛЕНЫ!!!!!!!!!");

            UpdateDatagrid();
        }

        public void UpdateDatagrid()
        {
            var hours = new[] { "9-10", "10-11", "11-12", "13-14", "14-15", "15-16" };
            var hoursSelect = string.Join(",", hours.Select(x => $"T.`{x}`"));

            var id = CurrentUser.UserId;
            dataGridView1.DataSource = DbConnector.ExecuteQuery($@"SELECT R.idRecords, T.date, W.Position, U.LastName, {hoursSelect} FROM Records AS R
JOIN Time AS T ON T.idtime = R.time_idtime
JOIN Workers AS W ON W.idWorkers = R.idWorker
JOIN Users AS U ON U.idUsers = W.idUser
WHERE R.Clients_idUsers = {id}");

            comboBox1.DataSource = DbConnector.ExecuteQuery($@"SELECT R.idRecords AS idRecord FROM Records AS R
JOIN Time AS T ON T.idtime = R.time_idtime
JOIN Workers AS W ON W.idWorkers = R.idWorker
JOIN Users AS U ON U.idUsers = W.idUser
WHERE R.Clients_idUsers = {id}");
            comboBox1.DisplayMember = "idRecord";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            new MainClientMenu().Show();
        }
    }
}
