namespace Clinica_Medica
{
    partial class Form1
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.BtEntrar = new System.Windows.Forms.Button();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.User_Lg = new System.Windows.Forms.Label();
            this.Password_Lg = new System.Windows.Forms.Label();
            this.txtError = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // BtEntrar
            // 
            this.BtEntrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtEntrar.Location = new System.Drawing.Point(341, 35);
            this.BtEntrar.Name = "BtEntrar";
            this.BtEntrar.Size = new System.Drawing.Size(75, 20);
            this.BtEntrar.TabIndex = 2;
            this.BtEntrar.Text = "Entrar";
            this.BtEntrar.UseVisualStyleBackColor = true;
            this.BtEntrar.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(55, 35);
            this.txtPassword.MaxLength = 0;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(280, 20);
            this.txtPassword.TabIndex = 1;
            this.txtPassword.UseSystemPasswordChar = true;
            this.txtPassword.TextChanged += new System.EventHandler(this.txtPassword_TextChanged);
            // 
            // txtUsername
            // 
            this.txtUsername.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.RecentlyUsedList;
            this.txtUsername.Location = new System.Drawing.Point(55, 9);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(280, 20);
            this.txtUsername.TabIndex = 0;
            // 
            // User_Lg
            // 
            this.User_Lg.AutoSize = true;
            this.User_Lg.Location = new System.Drawing.Point(9, 12);
            this.User_Lg.Name = "User_Lg";
            this.User_Lg.Size = new System.Drawing.Size(46, 13);
            this.User_Lg.TabIndex = 4;
            this.User_Lg.Text = "Usuário:";
            // 
            // Password_Lg
            // 
            this.Password_Lg.AutoSize = true;
            this.Password_Lg.Location = new System.Drawing.Point(14, 38);
            this.Password_Lg.Name = "Password_Lg";
            this.Password_Lg.Size = new System.Drawing.Size(41, 13);
            this.Password_Lg.TabIndex = 5;
            this.Password_Lg.Text = "Senha:";
            // 
            // txtError
            // 
            this.txtError.AutoSize = true;
            this.txtError.ForeColor = System.Drawing.Color.Red;
            this.txtError.Location = new System.Drawing.Point(55, 58);
            this.txtError.Name = "txtError";
            this.txtError.Size = new System.Drawing.Size(0, 13);
            this.txtError.TabIndex = 6;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(428, 87);
            this.Controls.Add(this.txtError);
            this.Controls.Add(this.Password_Lg);
            this.Controls.Add(this.User_Lg);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.BtEntrar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "Clínica Boa Esperança - Acesso";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtEntrar;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label User_Lg;
        private System.Windows.Forms.Label Password_Lg;
        private System.Windows.Forms.Label txtError;
    }
}

