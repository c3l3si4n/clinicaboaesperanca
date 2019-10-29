using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Reflection;
namespace Clinica_Medica
{
    public partial class Medicos : Form
    {
        public Medicos()
        {
            InitializeComponent();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog1.FileName;
                Image imagem = Image.FromFile(filePath);
                pictureBox1.Image = imagem;
            }

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void Medicos_Load(object sender, EventArgs e)
        {
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.CheckPathExists = true;
            openFileDialog1.DefaultExt = "jpg";
            buscar();

            

        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, pictureBox1.ClientRectangle, Color.Black, ButtonBorderStyle.Solid);
        }
        public byte[] ImageToByteArray(Image imageIn)
        {
            var ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            return ms.ToArray();

        }
        private void button2_Click(object sender, EventArgs e)
        {

            Form1 obj = new Form1();

            string qs = "SELECT * FROM medicos WHERE rg=@rg;";

            MySqlParameter pam2 = new MySqlParameter("rg", textBox3.Text);
            List<MySqlParameter> sqlParameters = new List<MySqlParameter> { pam2 };
            DataTable rgTable = obj.executeSelect(qs, sqlParameters);
            if (rgTable.Rows.Count > 0)
            {
                MessageBox.Show("Um doutor com esse RG já existe!");
                return;
            }


            string query = $"INSERT INTO medicos(nome,especialidade,rg,telefone,crm,foto) VALUES (@nome,@especialidade,@rg,@telefone,@crm,@foto);";
            string encodedImage = "";

            


            if (pictureBox1.Image == null)
            {
                Assembly asm = Assembly.GetExecutingAssembly();
                Stream strm = asm.GetManifestResourceStream("Clinica_Medica.unknownPicture.png");
                Bitmap bmp = new Bitmap(strm);

                encodedImage = System.Convert.ToBase64String(ImageToByteArray((Image) bmp));
            }
            else
            {
                encodedImage = System.Convert.ToBase64String(ImageToByteArray(pictureBox1.Image));

            }

            MySqlParameter pamNome = new MySqlParameter("nome", textBox1.Text);
            MySqlParameter pamEspecialidade= new MySqlParameter("especialidade", textBox2.Text);
            MySqlParameter pamRg = new MySqlParameter("rg", textBox3.Text);
            MySqlParameter pamTelefone = new MySqlParameter("telefone", textBox4.Text);
            MySqlParameter pamCrm = new MySqlParameter("crm", textBox5.Text);
            MySqlParameter pamFoto = new MySqlParameter("foto", encodedImage);





            sqlParameters = new List<MySqlParameter> { pamNome, pamEspecialidade, pamRg, pamTelefone, pamCrm, pamFoto };


            obj.executarQuery(query, sqlParameters);

            MessageBox.Show("Médico " + textBox1.Text + " inserido!");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form1 obj = new Form1();

            string qs = "SELECT nome FROM medicos WHERE Nome LIKE @busca OR Especialidade LIKE @busca OR   rg LIKE @busca OR telefone LIKE @busca OR crm LIKE @busca;";

            MySqlParameter pam2 = new MySqlParameter("busca", '%' + textBox6.Text + '%' );
            List<MySqlParameter> sqlParameters = new List<MySqlParameter> { pam2 };
            DataTable searchTable  = obj.executeSelect(qs, sqlParameters);

            listView1.Clear();

            foreach (DataRow row in searchTable.Rows)
            {
                ListViewItem item = new ListViewItem(row[0].ToString());
                for (int i = 1; i < searchTable.Columns.Count; i++)
                {
                    item.SubItems.Add(row[i].ToString());
                }
                listView1.Items.Add(item);
            }
        }
        int selectedDoctor;

        private void buscar()
        {
            Form1 obj = new Form1();
            String selected;
            if (listView1.SelectedItems.Count > 0) { selected = listView1.SelectedItems[0].Text; }
            else return;
            string qs = "SELECT * FROM medicos WHERE nome=@nome order by nome;";

            MySqlParameter pam2 = new MySqlParameter("nome", selected);

            List<MySqlParameter> sqlParameters = new List<MySqlParameter> { pam2 };
            DataTable detalhesTable = obj.executeSelect(qs, sqlParameters);
            if (detalhesTable == null) return;
            if (detalhesTable.Rows.Count <= 0) return;
                DataRow informacoesMedico = detalhesTable.Rows[0];
            //            string encodedImage = System.Convert.ToBase64String(ImageToByteArray(pictureBox1.Image));
            string base64image = informacoesMedico["foto"].ToString();


            Image profilePicture = (Bitmap)((new ImageConverter()).ConvertFrom(System.Convert.FromBase64String(base64image)));

            
            pictureBox2.Image = profilePicture;
            selectedDoctor = Convert.ToInt32(informacoesMedico["codigo"].ToString());

            label6.Text = informacoesMedico["nome"].ToString();
            label7.Text =   informacoesMedico["especialidade"].ToString();
            label8.Text = "RG: " +  informacoesMedico["rg"].ToString();
            label9.Text = "Telefone: " +  informacoesMedico["telefone"].ToString();
            label10.Text = "CRM: " + informacoesMedico["crm"].ToString();

            textBox7.Text = informacoesMedico["nome"].ToString();
            textBox8.Text = informacoesMedico["especialidade"].ToString();
            textBox9.Text = informacoesMedico["rg"].ToString();
            textBox10.Text = informacoesMedico["telefone"].ToString();
            textBox11.Text = informacoesMedico["crm"].ToString();
            label18.Text = "Imagem da DB";
            button8.Visible = true;
            button4.Visible = true;
            
        }
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            buscar();
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {
            buscar();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            buscar();
        }

        private void button4_Click(object sender, EventArgs e)
        {

            Form1 obj = new Form1();
            String selected;
            if (listView1.SelectedItems.Count > 0) { selected = listView1.SelectedItems[0].Text; }
            else return;
            string qs = "DELETE FROM medicos WHERE nome=@nome;";

            MySqlParameter pam2 = new MySqlParameter("nome", selected);
                
            List<MySqlParameter> sqlParameters = new List<MySqlParameter> { pam2 };
            obj.executarQuery(qs, sqlParameters);

            MessageBox.Show("Médico Deletado!");
            buscar();
        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form1 obj = new Form1();
            String selected;
            if (listView1.SelectedItems.Count > 0) { selected = listView1.SelectedItems[0].Text; }
            else return;

            string qs = "UPDATE medicos SET nome=@newName,especialidade=@newSkill,rg=@newRg,telefone=@newPhone,crm=@newCrm,foto=@newPhoto WHERE nome=@name;";

            string encodedImage;
            if (openFileDialog2.FileName != "openFileDialog2")
            {
                encodedImage = System.Convert.ToBase64String(ImageToByteArray(Image.FromFile(openFileDialog2.FileName)));
            }
            else
            {
                encodedImage = System.Convert.ToBase64String(ImageToByteArray(pictureBox2.Image));
            }
            


            MySqlParameter pam1 = new MySqlParameter("name", selected);
            MySqlParameter pam2 = new MySqlParameter("newName", textBox7.Text);
            MySqlParameter pam3 = new MySqlParameter("newSkill", textBox8.Text);
            MySqlParameter pam4 = new MySqlParameter("newRg", textBox9.Text);
            MySqlParameter pam5 = new MySqlParameter("newPhone", textBox10.Text);
            MySqlParameter pam6 = new MySqlParameter("newCrm", textBox11.Text);
            MySqlParameter pam7 = new MySqlParameter("newPhoto", encodedImage);
 


            /* 
              label6.Text = informacoesMedico["nome"].ToString();
            label7.Text =   informacoesMedico["especialidade"].ToString();
            label8.Text = "RG: " +  informacoesMedico["rg"].ToString();
            label9.Text = "Telefone: " +  informacoesMedico["telefone"].ToString();
            label10.Text = "CRM: " + informacoesMedico["crm"].ToString();

            textBox7.Text = informacoesMedico["nome"].ToString();
            textBox8.Text = informacoesMedico["especialidade"].ToString();
            textBox9.Text = informacoesMedico["rg"].ToString();
            textBox10.Text = informacoesMedico["telefone"].ToString();
            textBox11.Text = informacoesMedico["crm"].ToString();
             */
            List<MySqlParameter> sqlParameters = new List<MySqlParameter> { pam1,pam2,pam3,pam4,pam5,pam6,pam7 };
            obj.executarQuery(qs, sqlParameters);

            MessageBox.Show("Médico Modificado!");
            buscar();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (openFileDialog2.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog2.FileName;

            }

        }

        private void Button8_Click(object sender, EventArgs e)
        {
            Form2 obj = new Form2();
            obj.consultasSelectedDoctorId = selectedDoctor;
            this.Hide();
            obj.Closed += (s, args) => this.Show();
            obj.Show();

        }
    }
}
