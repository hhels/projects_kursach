using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace fitnes
{
    public partial class MainClient : Form
    {
        public MainClient()
        {
            InitializeComponent();
            monthCalendar1.DateSelected += monthCalendar1_DateSelected;
            monthCalendar1.MinDate = DateTime.Now;

            comboBox1.DataSource = DbConnector.ExecuteQuery("SELECT DISTINCT idWorkers, Position FROM Workers ");
            comboBox1.DisplayMember = "Position";
            comboBox1.ValueMember = "idWorkers";
        }

        void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            label1.Text = e.Start.ToString("yyyy.MM.dd");
            // или так - аналогичный код
            //label1.Text = String.Format("Вы выбрали: {0}", monthCalendar1.SelectionStart.ToLongDateString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new Aboniment().ShowDialog();
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            var x = comboBox1.Text;
            comboBox2.DataSource = DbConnector.ExecuteQuery($"SELECT FirstName, LastName, idUsers, W.idWorkers AS idWorkers  FROM  Users AS U JOIN Workers AS W on U.idUsers = W.idUser WHERE W.Position = '{x}'");
            comboBox2.DisplayMember = "FirstName";
            //comboBox2.ValueMember = "idUsers";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var y = ((DataRowView)comboBox2.SelectedItem).Row["idUsers"];
            dataGridView1.DataSource = DbConnector.ExecuteQuery($"SELECT `9-10`, `15-16` FROM Time AS T JOIN Users AS U ON T.Workers_idUser = U.idUsers WHERE T.date = '{label1.Text}' AND U.idUsers = {y}");
            var count9 = DbConnector.ExecuteQuery($"SELECT COUNT(*) FROM Time AS T JOIN Users AS U ON T.Workers_idUser = U.idUsers WHERE T.date = '{label1.Text}' AND U.idUsers = {y} AND T.`9-10` = 3");
            var count15 = DbConnector.ExecuteQuery($"SELECT COUNT(*) FROM Time AS T JOIN Users AS U ON T.Workers_idUser = U.idUsers WHERE T.date = '{label1.Text}' AND U.idUsers = {y} AND T.`15-16` = 3");

            label7.Text = count9.AsEnumerable().FirstOrDefault()["Count(*)"].ToString();
            label9.Text = count15.AsEnumerable().FirstOrDefault()["Count(*)"].ToString();

            if (Convert.ToInt32(label7.Text) >= 20)
            {
                radioButton1.Visible = false;
            }
            else { radioButton1.Visible = false; }

            if (Convert.ToInt32(label9.Text) >= 20)
            {
                radioButton2.Visible = false;
            }
            else { radioButton2.Visible = false; }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show(radioButton1.Checked.ToString());
            var selected = radioButton1.Checked ? "`9-10`" : "`15-16`";
            var worker = ((DataRowView)comboBox2.SelectedItem).Row["idWorkers"];
            var user = ((DataRowView)comboBox2.SelectedItem).Row["idUsers"];
            var timeId = DbConnector.ExecuteUpdate($"INSERT INTO Time (date, {selected}, Workers_idWorkers, Workers_idUser) VALUES ('{label1.Text}', 3, '{worker}', '{user}')");
            var clientId = DbConnector.ExecuteQuery($"SELECT * FROM Clients WHERE idUsers = {CurrentUser.UserId}").AsEnumerable()
                                      .FirstOrDefault()["idClient"];
            DbConnector.ExecuteUpdate($"INSERT INTO Records (idWorker, time_idtime, Clients_idClient, Clients_idUsers) VALUES ('{worker}', {timeId}, {clientId}, {CurrentUser.UserId})");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Hide();
            new MainClientMenu().Show();
        }
    }
}



