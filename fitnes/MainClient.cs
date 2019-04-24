using System;
using System.Data;
using System.Windows.Forms;

namespace fitnes
{
    public partial class MainClient : Form
    {
        public MainClient()
        {
            InitializeComponent();
            monthCalendar1.DateSelected += monthCalendar1_DateSelected;

            comboBox1.DataSource = DbConnector.ExecuteQuery("SELECT DISTINCT idWorkers, Position FROM Workers "); ;
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
            comboBox2.DataSource = DbConnector.ExecuteQuery($"SELECT FirstName, LastName, idUsers  FROM  Users AS U JOIN Workers AS W on U.idUsers = W.idUser WHERE W.Position = '{x}'");
            comboBox2.DisplayMember = "FirstName";
            //comboBox2.ValueMember = "idUsers";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var y = ((DataRowView)comboBox2.SelectedItem).Row["idUsers"];
            dataGridView1.DataSource = DbConnector.ExecuteQuery($"SELECT `9-10`, `15-16` FROM Time AS T JOIN Users AS U ON T.Workers_idUser = U.idUsers WHERE T.date = '{label1.Text}' AND U.idUsers = {y}");
        }
    }
}



