namespace Clinica_Medica
{
    partial class PaginaInicial
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PaginaInicial));
            this.Bt_Pacientes = new System.Windows.Forms.Button();
            this.BtMed = new System.Windows.Forms.Button();
            this.btnRegistrar = new WindowsFormsAero.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.button1 = new WindowsFormsAero.Button();
            this.SuspendLayout();
            // 
            // Bt_Pacientes
            // 
            this.Bt_Pacientes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Bt_Pacientes.Location = new System.Drawing.Point(12, 12);
            this.Bt_Pacientes.Name = "Bt_Pacientes";
            this.Bt_Pacientes.Size = new System.Drawing.Size(147, 56);
            this.Bt_Pacientes.TabIndex = 0;
            this.Bt_Pacientes.Text = "Pacientes";
            this.Bt_Pacientes.UseVisualStyleBackColor = true;
            this.Bt_Pacientes.Click += new System.EventHandler(this.Bt_Pacientes_Click);
            // 
            // BtMed
            // 
            this.BtMed.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtMed.Location = new System.Drawing.Point(165, 12);
            this.BtMed.Name = "BtMed";
            this.BtMed.Size = new System.Drawing.Size(147, 56);
            this.BtMed.TabIndex = 1;
            this.BtMed.Text = "Médicos";
            this.BtMed.UseVisualStyleBackColor = true;
            this.BtMed.Click += new System.EventHandler(this.BtMed_Click);
            // 
            // btnRegistrar
            // 
            this.btnRegistrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegistrar.Location = new System.Drawing.Point(211, 80);
            this.btnRegistrar.Name = "btnRegistrar";
            this.btnRegistrar.Size = new System.Drawing.Size(101, 29);
            this.btnRegistrar.TabIndex = 4;
            this.btnRegistrar.Text = "Registrar Usuário";
            this.btnRegistrar.UseVisualStyleBackColor = true;
            this.btnRegistrar.Click += new System.EventHandler(this.btnRegistrar_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ForeColor = System.Drawing.Color.Maroon;
            this.button1.Location = new System.Drawing.Point(12, 83);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Sair";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // PaginaInicial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(318, 113);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnRegistrar);
            this.Controls.Add(this.BtMed);
            this.Controls.Add(this.Bt_Pacientes);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PaginaInicial";
            this.Text = "Clínica Boa Esperança";
            this.Load += new System.EventHandler(this.PaginaInicial_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.PaginaInicial_Paint);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Bt_Pacientes;
        private System.Windows.Forms.Button BtMed;
        private WindowsFormsAero.Button btnRegistrar;
        private System.Windows.Forms.Timer timer1;
        private WindowsFormsAero.Button button1;
    }
}