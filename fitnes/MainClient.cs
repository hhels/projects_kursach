using System;
using System.Data;
using System.Windows.Forms;

namespace fitnes
{
    public partial class MainClient : Form
    {
        private DataTable _chto;
        private DataTable _kto;

        public MainClient()
        {
            InitializeComponent();
            monthCalendar1.DateSelected += monthCalendar1_DateSelected;

            _chto = DbConnector.ExecuteQuery("SELECT DISTINCT idWorkers, Position FROM Workers ");
            comboBox1.DataSource = _chto;
            comboBox1.DisplayMember = "Position";
            comboBox1.ValueMember = "Position";
        }
        void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            label1.Text = e.Start.ToString("yyyy.MM.dd");
            // или так - аналогичный код
            //label1.Text = String.Format("Вы выбрали: {0}", monthCalendar1.SelectionStart.ToLongDateString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
            new Aboniment().Show();
        }


        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e) 
        {
            e.ToString();
            //string op = comboBox1.SelectedIndex.ToString();
            //MessageBox.Show(op);
            //var pos = ((ComboBox)sender).SelectedText;
            //MessageBox.Show(pos);
            //_kto = DbConnector.ExecuteQuery($"SELECT FirstName, LastName FROM  Users AS U JOIN Workers AS W on U.idUsers = W.idUser WHERE W.Position = '{comboBox1.SelectedText}'");
            //comboBox2.DataSource = _kto;
            //comboBox2.DisplayMember = "FirstName";
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            var pos = ((ComboBox)sender).Text;
            var x = comboBox1.Text;
            _kto = DbConnector.ExecuteQuery($"SELECT FirstName, LastName FROM  Users AS U JOIN Workers AS W on U.idUsers = W.idUser WHERE W.Position = '{x}'");
            comboBox2.DataSource = _kto;
            comboBox2.DisplayMember = "FirstName";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var y = comboBox2.Text;
            dataGridView1.DataSource =  DbConnector.ExecuteQuery($"SELECT * FROM Time AS T, Users AS U WHERE T.date = '{label1.Text}' AND U.FirstName = '{y}'");
        }
    }
}



