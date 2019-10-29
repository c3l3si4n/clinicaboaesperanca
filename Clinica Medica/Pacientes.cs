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
    public partial class Pacientes : Form
    {
        public Pacientes()
        {
            InitializeComponent();
            
        }

        private void Pacientes_Load(object sender, EventArgs e)
        {
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.Aquamarine;
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.Red;
            dataGridView1.ColumnHeadersHeight = 30;


        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

     

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 obj = new Form1();
            string query = $"INSERT INTO paciente(nome,rg,telefone,plano_saude,observacao,data_nascimento) VALUES (@nome,@rg,@telefone,@plano_saude,@observacao,@data_nascimento);";
            string escapedPhone = new String(txtFone.Text.Where(Char.IsDigit).ToArray());

            MySqlParameter pamUser = new MySqlParameter("nome", txtNome.Text);
            MySqlParameter pamRg = new MySqlParameter("rg", txtRg.Text);
            MySqlParameter pamFone = new MySqlParameter("telefone", escapedPhone);
            MySqlParameter pamPlano = new MySqlParameter("plano_saude", txtPlano.Text);
            MySqlParameter pamObservacao = new MySqlParameter("observacao", rtxtDescription.Text);
            MySqlParameter pamDtNasc = new MySqlParameter("data_nascimento", dateBirth.Value.Date);





            List<MySqlParameter> sqlParameters = new List<MySqlParameter> { pamUser, pamRg, pamFone, pamPlano, pamObservacao, pamDtNasc };


            obj.executarQuery(query, sqlParameters);

            MessageBox.Show("Paciente " + txtNome.Text + " inserido!");
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 obj = new Form1();
            string query = "select nome AS 'Nome',rg AS 'RG',telefone AS 'Telefone',plano_saude AS 'Plano de Saude',observacao AS 'Observacao',data_nascimento AS 'Data de Nascimento' from paciente where nome like @nome; ";

            string builtName = '%' + textBox1.Text + '%';
            MySqlParameter pamNome = new MySqlParameter("nome", builtName);
            
            List<MySqlParameter> sqlParameters = new List<MySqlParameter> { pamNome };


            DataTable dt =  obj.executeSelect(query, sqlParameters); ;
            dataGridView1.DataSource = dt;


            
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabControl tab = sender as TabControl;
            switch (tab.SelectedIndex)
            {
                case 0:
                    this.AcceptButton = button1;
                    break;
                case 1:
                    this.AcceptButton = button2;
                    break;
            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
    }
}
