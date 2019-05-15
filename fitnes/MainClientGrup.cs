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
    public partial class MainClientGrup : Form
    {
        public MainClientGrup()
        {
            InitializeComponent();
            monthCalendar1.DateSelected += monthCalendar1_DateSelected;
            monthCalendar1.MinDate = DateTime.Now;

            comboBox1.DataSource = DbConnector.ExecuteQuery($"SELECT DISTINCT idWorkers, Position FROM Workers");
            comboBox1.DisplayMember = "Position";
            comboBox1.ValueMember = "idWorkers";
        }

        private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            label1.Text = e.Start.ToString("yyyy.MM.dd");
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            var x = comboBox1.Text;
            comboBox2.DataSource = DbConnector.ExecuteQuery($"SELECT FirstName, LastName, idUsers, W.idWorkers AS idWorkers  FROM  Users AS U JOIN Workers AS W on U.idUsers = W.idUser WHERE W.Position = '{x}'");
            comboBox2.DisplayMember = "LastName";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            comboBox3.Items.Clear();
            comboBox3.SelectedIndex = -1;
            var y = ((DataRowView)comboBox2.SelectedItem).Row["idUsers"];

            dataGridView1.DataSource = DbConnector.ExecuteQuery($"SELECT `10-11`,`11-12`,`13-14`,`14-15`  FROM Time AS T JOIN Users AS U ON T.Workers_idUser = U.idUsers WHERE T.date = '{label1.Text}' AND U.idUsers = {y}");

            var count1 = DbConnector.ExecuteScalar($"SELECT COUNT(10-11)  FROM Time AS T JOIN Users AS U ON T.Workers_idUser = U.idUsers WHERE T.date = '{label1.Text}' AND U.idUsers = {y} AND T.`10-11` = 1");
            var count2 = DbConnector.ExecuteScalar($"SELECT COUNT(11-12)  FROM Time AS T JOIN Users AS U ON T.Workers_idUser = U.idUsers WHERE T.date = '{label1.Text}' AND U.idUsers = {y} AND T.`11-12` = 1");
            var count3 = DbConnector.ExecuteScalar($"SELECT COUNT(13-14)  FROM Time AS T JOIN Users AS U ON T.Workers_idUser = U.idUsers WHERE T.date = '{label1.Text}' AND U.idUsers = {y} AND T.`13-14` = 1");
            var count4 = DbConnector.ExecuteScalar($"SELECT COUNT(14-15)  FROM Time AS T JOIN Users AS U ON T.Workers_idUser = U.idUsers WHERE T.date = '{label1.Text}' AND U.idUsers = {y} AND T.`14-15` = 1");

            if (Convert.ToInt32(count1) <= 0)
                comboBox3.Items.Add ("10-11");
            if (Convert.ToInt32(count2) <= 0)
                comboBox3.Items.Add("11-12");
            if (Convert.ToInt32(count3) <= 0)
                comboBox3.Items.Add("13-14");
            if (Convert.ToInt32(count4) <= 0)
                comboBox3.Items.Add("14-15"); 

        }

        private void button2_Click(object sender, EventArgs e)
        {
            var selected = comboBox3.Text;
            var worker = ((DataRowView)comboBox2.SelectedItem).Row["idWorkers"];
            var user = ((DataRowView)comboBox2.SelectedItem).Row["idUsers"];
            var timeId = DbConnector.ExecuteUpdate($"INSERT INTO Time (date, `{selected}`, Workers_idWorkers, Workers_idUser) VALUES ('{label1.Text}', 1, '{worker}', '{user}')");
            var clientId = DbConnector.ExecuteScalar($"SELECT idClient FROM Clients WHERE idUsers = {CurrentUser.UserId}");
            DbConnector.ExecuteUpdate($"INSERT INTO Records (idWorker, time_idtime, Clients_idClient, Clients_idUsers) VALUES ('{worker}', {timeId}, {clientId}, {CurrentUser.UserId})");
            MessageBox.Show("Вы записасались");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Hide();
            new MainClientMenu().Show();
        }
    }
}
