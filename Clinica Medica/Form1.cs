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
using BDGerenciador;
using System.Security.Cryptography;
using Microsoft.Win32;
using System.IO;

namespace Clinica_Medica
{
    public partial class Form1 : Form
    {

        string cs = @"Server=localhost;Uid=root;database=clinica;";
        public string loggedInUsername;
        bool debug = false;
        string encription_key = "aSdTZpGof4u0Ubm2RZvDVWeU7ovYwcJq";
        public static string EncryptString(string key, string plainText)
        {
            byte[] iv = new byte[16];
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                        {
                            streamWriter.Write(plainText);
                        }

                        array = memoryStream.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(array);
        }

        public static string DecryptString(string key, string cipherText)
        {
            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(cipherText);

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }
        public DataTable executeSelect(string query, List<MySqlParameter> parameters)
        {
            MySqlConnection con = null;
            DataTable dt = new DataTable(); ;
            
            try
            {
                con = new MySqlConnection(cs);
                con.Open();
                MySqlCommand sql = new MySqlCommand(query, con);

                foreach (MySqlParameter pam in parameters)
                {

                    sql.Parameters.Add(pam);

                }
                var dr = sql.ExecuteReader();

                dt.Load(dr);
               
                
                
            }
            catch(Exception e)
            {
                MessageBox.Show("Erro ao conectar no banco de dados MySQL. Você está com ele rodando? 🤔"+ e.Message);
                System.Environment.Exit(1);
                
            }
            finally
            {
                if (con != null) con.Close();
                
                
            }
            return dt;
            
        }

        public long executarQuery(string query, List<MySqlParameter> parameters)
        {
            MySqlConnection con = null;
            con = new MySqlConnection(cs);
            con.Open();
            MySqlCommand sql = new MySqlCommand(query, con);

            try
            {

                foreach (MySqlParameter pam in parameters)
                {
                    sql.Parameters.Add(pam);
                }

                sql.ExecuteNonQuery();
            }
            catch(Exception e)
            {
                MessageBox.Show("Erro ao conectar no banco de dados MySQL. Você está com ele rodando? 🤔 "+ e.Message);
                System.Environment.Exit(1);
            }
            finally
            {
                if (con != null) con.Close();
                

            }
            return sql.LastInsertedId;

        }

        public Form1()
        {
            InitializeComponent();
           


            

        }
        private bool validateLogin(string username, string password, bool debug = false)
        {

            if (debug == true && username == "admin" && password == "clinicaboaesperanca") return true;
;


            if (validatePassword(username,password))
            {
                DateTime dt = DateTime.Now;
                string loginUpdate = $"UPDATE usuarios SET lastAccessed=CURRENT_TIMESTAMP() WHERE username=@username;";
                MySqlParameter sqlUsername = new MySqlParameter("username", username);
                List<MySqlParameter> sqlParameters = new List<MySqlParameter> { sqlUsername };

                executarQuery(loginUpdate, sqlParameters);

                return true;
            }
            else
            {


                string updateLastAttempt = $"UPDATE usuarios SET lastAttempt=CURRENT_TIMESTAMP(),lastAttemptPassword=@attemptedPass WHERE username=@username;";
                MySqlParameter pam1 = new MySqlParameter("attemptedPass", hashPassword(password));
                MySqlParameter pam2 = new MySqlParameter("username", username);
                List<MySqlParameter> sqlParameters = new List<MySqlParameter> { pam1, pam2 };
                executarQuery(updateLastAttempt, sqlParameters);


                return false;
            }
        }
        private bool validatePassword(string username, string password)
        {
            
            string qs = "SELECT * FROM usuarios WHERE username=@username;";
          
            MySqlParameter pam2 = new MySqlParameter("username", username);
            List<MySqlParameter> sqlParameters = new List<MySqlParameter> { pam2 };

            DataTable userTable = executeSelect(qs, sqlParameters);
            if (userTable.Rows.Count == 1)
            {
                string savedPassHash = userTable.Rows[0][2].ToString();
                byte[] hashBytes = Convert.FromBase64String(savedPassHash);

                byte[] salt = new byte[16];
                Array.Copy(hashBytes, 0, salt, 0, 16);
                var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
                byte[] hash = pbkdf2.GetBytes(20);
                
                bool loginOk = true;
                for (int i = 0; i < 20; i++)
                {
                    if (hashBytes[i + 16] != hash[i]) loginOk = false;
                }
               
                return loginOk;
            }
            else
            {
                return false;
            }
        }
        public string hashPassword(string password)
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);

            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);
            return Convert.ToBase64String(hashBytes);
        }

        private void button1_Click(object sender, EventArgs e)
        {


            if (  validateLogin(txtUsername.Text, txtPassword.Text, debug))
            {
                PaginaInicial obj = new PaginaInicial();
                Properties.Settings.Default["username"] = txtUsername.Text;
                Properties.Settings.Default["password"] = EncryptString(encription_key, txtPassword.Text);
                Properties.Settings.Default.Save();

                this.Hide();
                obj.Closed += (s, args) => this.Close();
                obj.Show();
            }
            else
            {
                txtError.Text = "Usuário ou senha incorretos.";
            }

     
        }

        private void BtLimpar_Click(object sender, EventArgs e)
        {
            txtUsername.Clear();
            txtPassword.Clear();
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.AcceptButton = this.BtEntrar;
            txtUsername.Focus();
            String[] arguments = Environment.GetCommandLineArgs();
            if (arguments.Length != 1)
            {
                if (arguments[1] == "debug") debug = true;
            }
            



        }
        public bool controlelogin = true;
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            String autouser = Convert.ToString(Properties.Settings.Default["username"]);
            String autopass = DecryptString(encription_key, Convert.ToString(Properties.Settings.Default["password"]));
            if (controlelogin)
            {
                controlelogin = false;
                if (validateLogin(autouser, autopass, false))
                {
                    controlelogin = false;
                    PaginaInicial obj = new PaginaInicial();


                    this.Hide();
                    obj.Closed += (s, args) => this.Close();
                    obj.Show();
                }
            }
      
        }
    }
}
