using System;
using System.IO;
using System.Windows.Forms;

namespace WKRadarExportador
{
    public partial class Form1 : Form
    {
        private string ultimoDiretorio = string.Empty;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtDiretorioOrigem.Text)) {
                MessageBox.Show("Diretório de ORIGEM não informado", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (String.IsNullOrEmpty(txtDiretorioDestino.Text)) {
                MessageBox.Show("Diretório de DESTINO não informado", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtDiretorioOrigem.Text == txtDiretorioDestino.Text) {
                MessageBox.Show("Diretório de ORIGEM e DESTINO não podem ser iguais", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var exp = new WkExportarXML(txtDiretorioOrigem.Text, txtDiretorioDestino.Text);
            exp.Processar();

            MessageBox.Show("Finalizado", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void txtDiretorioOrigem_Click(object sender, EventArgs e)
        {
            this.ultimoDiretorio = (ultimoDiretorio == string.Empty ? Directory.GetCurrentDirectory() : ultimoDiretorio);
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                folderDialog.SelectedPath = this.ultimoDiretorio;
                if (folderDialog.ShowDialog() == DialogResult.OK) {
                    this.ultimoDiretorio = folderDialog.SelectedPath;
                    txtDiretorioOrigem.Text = folderDialog.SelectedPath;
                }
            }
        }
        private void txtDiretorioDestino_Click(object sender, EventArgs e)
        {
            this.ultimoDiretorio = (ultimoDiretorio == string.Empty ? Directory.GetCurrentDirectory() : ultimoDiretorio);
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                folderDialog.SelectedPath = this.ultimoDiretorio;
                if (folderDialog.ShowDialog() == DialogResult.OK) {
                    this.ultimoDiretorio = folderDialog.SelectedPath;
                    txtDiretorioDestino.Text = folderDialog.SelectedPath;
                }
            }
        }
    }
}
