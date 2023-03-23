namespace WKRadarExportador
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
            this.button1 = new System.Windows.Forms.Button();
            this.txtDiretorioOrigem = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDiretorioDestino = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(23, 154);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Iniciar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtDiretorioOrigem
            // 
            this.txtDiretorioOrigem.Location = new System.Drawing.Point(23, 41);
            this.txtDiretorioOrigem.Name = "txtDiretorioOrigem";
            this.txtDiretorioOrigem.ReadOnly = true;
            this.txtDiretorioOrigem.Size = new System.Drawing.Size(548, 22);
            this.txtDiretorioOrigem.TabIndex = 1;
            this.txtDiretorioOrigem.Click += new System.EventHandler(this.txtDiretorioOrigem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(189, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Informe o diretório de ORIGEM";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(196, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Informe o diretório de DESTINO";
            // 
            // txtDiretorioDestino
            // 
            this.txtDiretorioDestino.Location = new System.Drawing.Point(23, 107);
            this.txtDiretorioDestino.Name = "txtDiretorioDestino";
            this.txtDiretorioDestino.ReadOnly = true;
            this.txtDiretorioDestino.Size = new System.Drawing.Size(548, 22);
            this.txtDiretorioDestino.TabIndex = 3;
            this.txtDiretorioDestino.Click += new System.EventHandler(this.txtDiretorioDestino_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(590, 204);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtDiretorioDestino);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtDiretorioOrigem);
            this.Controls.Add(this.button1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WKRadarExportador";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtDiretorioOrigem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDiretorioDestino;
    }
}

