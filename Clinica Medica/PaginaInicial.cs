using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clinica_Medica
{
    public partial class PaginaInicial : Form
    {
        public PaginaInicial()
        {
            InitializeComponent();
        }

        private void Bt_Pacientes_Click(object sender, EventArgs e)
        {
            Pacientes obj = new Pacientes();
            obj.Show();
        }

        private void BtMed_Click(object sender, EventArgs e)
        {
            Medicos obj = new Medicos();
            obj.Show();
        }

        private void Bt_Horários_Click(object sender, EventArgs e)
        {
            Horarios obj = new Horarios();
            obj.Show();
        }

        private void Bt_Consultas_Click(object sender, EventArgs e)
        {
            Consultas obj = new Consultas();
            obj.Show();
        }

        private void PaginaInicial_Load(object sender, EventArgs e)
        {

            timer1.Start();
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            registrationForm  obj = new registrationForm();
            obj.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Form1 obj = new Form1();



        }

        private void PaginaInicial_Paint(object sender, PaintEventArgs e)
        {
        
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Reset();
            this.Close();
        }
    }
}
