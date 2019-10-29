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
namespace Clinica_Medica
{
    public partial class Form2 : Form
    {
        public int consultasSelectedDoctorId;
        public Form2()
        {
            InitializeComponent();
        }

        private void SplitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        int selectedPacientId = 0;
        private void Button1_Click(object sender, EventArgs e)
        {
            Form1 obj = new Form1();
            if (listView1.SelectedItems.Count == 0) return;
            String selectedPacientName = listView1.SelectedItems[0].Text;
            String queryPacient = "SELECT codigo FROM paciente WHERE nome = @name";
            MySqlParameter pam1 = new MySqlParameter("name", selectedPacientName);
            List<MySqlParameter> sqlParameters = new List<MySqlParameter> { pam1 };
            DataTable dtPacientes = obj.executeSelect(queryPacient, sqlParameters);
            selectedPacientId = Convert.ToInt32(dtPacientes.Rows[0][0].ToString());

            DateTime selectedDate = dateTimePicker1.Value + dateTimePicker2.Value.TimeOfDay;



            String query = "INSERT INTO HORARIOS(horario,codigo_medico,codigo_paciente) VALUES(@date,@codMedic,@codPacient);";
            MySqlParameter pam2 = new MySqlParameter("date", selectedDate);
            MySqlParameter pam3 = new MySqlParameter("codMedic", consultasSelectedDoctorId);
            MySqlParameter pam4 = new MySqlParameter("codPacient", selectedPacientId );



            sqlParameters = new List<MySqlParameter> { pam2, pam3, pam4 };
            long insertTimeId = obj.executarQuery(query, sqlParameters);

            query = "INSERT INTO CONSULTAS(CODIGO_HORARIO,CODIGO_PACIENTE,CODIGO_MEDICO,PRONTUARIO,EXAMES,RECEITAS) VALUES(@codTime, @codPacient, @codMedic,@prontuario,@exames,@receitas);";
            MySqlParameter pam5 = new MySqlParameter("codTime", insertTimeId);
            MySqlParameter pam6 = new MySqlParameter("prontuario", richTextBox1.Text);
            MySqlParameter pam7 = new MySqlParameter("receitas", richTextBox3.Text);
            MySqlParameter pam8 = new MySqlParameter("exames", richTextBox2.Text);
            sqlParameters = new List<MySqlParameter> {  pam3, pam4, pam5,pam6,pam7,pam8 };
            long insertedConsultId = obj.executarQuery(query, sqlParameters);

            MessageBox.Show("Consulta Cadastrada!");


        }

        private void buscaPacientes()
        {
            Form1 obj = new Form1();
            String query = "SELECT codigo,nome FROM paciente;";
            

            List<MySqlParameter> sqlParameters = new List<MySqlParameter> {};


            DataTable dtPacientes = obj.executeSelect(query, sqlParameters);
            listView1.Clear();

            foreach (DataRow row in dtPacientes.Rows)
            {
                ListViewItem item = new ListViewItem(row[1].ToString());
                for (int i = 1; i < dtPacientes.Columns.Count; i++)
                {
                    item.SubItems.Add(row[i].ToString());
                }
                listView1.Items.Add(item);
            }
        }
        private void ListView1_SelectedIndexChanged(object sender, EventArgs e)
        {

            
        }

        private void Form2_Load(object sender, EventArgs e)
        {

            buscaPacientes();
           if (listView1.Items.Count > 0)
{
    listView1.Items[0].Selected = true;
    listView1.Select();
}
        }

        private void listView2_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
          
        }

        private void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if ((sender as ListView).FocusedItem != null)
            {
                (sender as ListView).FocusedItem.Selected = true;
            }
            Form1 obj = new Form1();
            String queryPacient = "SELECT * FROM paciente WHERE nome = @cod;";
            if (listView1.SelectedItems.Count == 0) return;
            MySqlParameter pam1 = new MySqlParameter("cod", listView1.SelectedItems[0].Text);
            List<MySqlParameter> sqlParameters = new List<MySqlParameter> { pam1 };
            DataTable dtPacientes = obj.executeSelect(queryPacient, sqlParameters);

             queryPacient = "SELECT * FROM consultas WHERE codigo_paciente = @cod;";
             pam1 = new MySqlParameter("cod", dtPacientes.Rows[0][0].ToString());
            sqlParameters = new List<MySqlParameter> { pam1 };
             dtPacientes = obj.executeSelect(queryPacient, sqlParameters);
            listView2.Clear();
            foreach (DataRow row in dtPacientes.Rows)
            {
                ListViewItem item = new ListViewItem(row[0].ToString());
                for (int i = 1; i < dtPacientes.Columns.Count; i++)
                {
                    item.SubItems.Add(row[i].ToString());
                }
                listView2.Items.Add(item);
            }
        }

        private void ListView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Form1 obj = new Form1();
            String queryPacient = "SELECT * FROM consultas WHERE codigo = @cod;";
            if (listView2.SelectedItems.Count == 0) return;
            MySqlParameter pam1 = new MySqlParameter("cod", listView2.SelectedItems[0].Text);
            List<MySqlParameter> sqlParameters = new List<MySqlParameter> { pam1 };
            DataTable dtPacientes = obj.executeSelect(queryPacient, sqlParameters);
            if (dtPacientes.Rows.Count != 0)
            {
                textBox1.Text = dtPacientes.Rows[0][4].ToString() ;
                textBox2.Text = dtPacientes.Rows[0][5].ToString();
                textBox3.Text = dtPacientes.Rows[0][6].ToString();
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Form1 obj = new Form1();
            String queryPacient = "UPDATE consultas SET prontuario=@prontuario,exames=@exames,receitas=@receitas WHERE codigo=@cod;"; 
            if (listView2.SelectedItems.Count == 0) return;
            MySqlParameter pam1 = new MySqlParameter("cod", listView2.SelectedItems[0].Text);
            MySqlParameter pam2 = new MySqlParameter("prontuario", textBox1.Text);
            MySqlParameter pam3 = new MySqlParameter("exames", textBox2.Text);
            MySqlParameter pam4 = new MySqlParameter("receitas", textBox3.Text);
            List<MySqlParameter> sqlParameters = new List<MySqlParameter> { pam1, pam2,pam3,pam4 };
            obj.executarQuery(queryPacient, sqlParameters);
            
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Form1 obj = new Form1();
            String queryPacient = "DELETE from consultas WHERE codigo=@cod;";
            if (listView2.SelectedItems.Count == 0) return;
            MySqlParameter pam1 = new MySqlParameter("cod", listView2.SelectedItems[0].Text);

            List<MySqlParameter> sqlParameters = new List<MySqlParameter> { pam1 };
            obj.executarQuery(queryPacient, sqlParameters);
            
        }
    }
}
