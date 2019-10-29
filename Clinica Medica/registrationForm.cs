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
    public partial class registrationForm : Form
    {
        public registrationForm()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text != textBox1.Text)
            {
                button1.Enabled = false;
                label4.Text = "Senha e confirmação não conferem!";
            }
            else
            {
                button1.Enabled = true;
                label4.Text = "";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 obj = new Form1();
            if (string.IsNullOrWhiteSpace(txtUsuario.Text) || string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text))
            {
                label4.Text = "Você precisa preencher todos os campos!";
                return;
            }

        
            string hashedPassword = obj.hashPassword(textBox1.Text);

            string query = $"INSERT INTO usuarios(username,password) VALUES (@username,@password);";

            MySqlParameter pam3 = new MySqlParameter("username", txtUsuario.Text);
            MySqlParameter pam4 = new MySqlParameter("password", hashedPassword);

            List<MySqlParameter> sqlParameters2 = new List<MySqlParameter> { pam3, pam4 };
            obj.executarQuery(query, sqlParameters2);

            MessageBox.Show($"Usuário {txtUsuario.Text} registrado!");

            registrationForm registrationForm = this;
            registrationForm.Dispose(false);
        
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox2_TextChanged(sender, e);
        }
    }
}
