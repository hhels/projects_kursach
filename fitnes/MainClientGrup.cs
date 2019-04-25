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

            var count1 = DbConnector.ExecuteQuery($"SELECT COUNT(10-11)  FROM Time AS T JOIN Users AS U ON T.Workers_idUser = U.idUsers WHERE T.date = '{label1.Text}' AND U.idUsers = {y} AND T.`10-11` = 1");
            var count2 = DbConnector.ExecuteQuery($"SELECT COUNT(11-12)  FROM Time AS T JOIN Users AS U ON T.Workers_idUser = U.idUsers WHERE T.date = '{label1.Text}' AND U.idUsers = {y} AND T.`11-12` = 1");
            var count3 = DbConnector.ExecuteQuery($"SELECT COUNT(13-14)  FROM Time AS T JOIN Users AS U ON T.Workers_idUser = U.idUsers WHERE T.date = '{label1.Text}' AND U.idUsers = {y} AND T.`13-14` = 1");
            var count4 = DbConnector.ExecuteQuery($"SELECT COUNT(14-15)  FROM Time AS T JOIN Users AS U ON T.Workers_idUser = U.idUsers WHERE T.date = '{label1.Text}' AND U.idUsers = {y} AND T.`14-15` = 1");
            var z1 = count1.AsEnumerable().FirstOrDefault()["Count(10-11)"].ToString();
            var z2 = count2.AsEnumerable().FirstOrDefault()["Count(11-12)"].ToString();
            var z3 = count3.AsEnumerable().FirstOrDefault()["Count(13-14)"].ToString();
            var z4 = count4.AsEnumerable().FirstOrDefault()["Count(14-15)"].ToString();

            if (Convert.ToInt32(z1) <= 0) { comboBox3.Items.Add ("10-11"); } else { comboBox3.DisplayMember = null; }
            if (Convert.ToInt32(z2) <= 0) { comboBox3.Items.Add("11-12"); } else { comboBox3.DisplayMember = null; }
            if (Convert.ToInt32(z3) <= 0) { comboBox3.Items.Add("13-14"); } else { comboBox3.DisplayMember = null; }
            if (Convert.ToInt32(z4) <= 0) { comboBox3.Items.Add("14-15"); } else { comboBox3.DisplayMember = null; }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            var selected = comboBox3.Text;
            var worker = ((DataRowView)comboBox2.SelectedItem).Row["idWorkers"];
            var user = ((DataRowView)comboBox2.SelectedItem).Row["idUsers"];
            var timeId = DbConnector.ExecuteInsert($"INSERT INTO Time (date, `{selected}`, Workers_idWorkers, Workers_idUser) VALUES ('{label1.Text}', 1, '{worker}', '{user}')");
            var clientId = DbConnector.ExecuteQuery($"SELECT * FROM Clients WHERE idUsers = {CurrentUser.UserId}").AsEnumerable()
                                      .FirstOrDefault()["idClient"];
            DbConnector.ExecuteInsert($"INSERT INTO Records (idWorker, time_idtime, Clients_idClient, Clients_idUsers) VALUES ('{worker}', {timeId}, {clientId}, {CurrentUser.UserId})");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Hide();
            //new MainClientMenu.Show();
        }
    }
}
