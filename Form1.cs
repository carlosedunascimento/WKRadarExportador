using System;
using System.IO;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WKRadarExportador
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();  
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string diretorioOrigem = "C:/Temp/TRANSPORTES/";
            string diretorioDestino = "C:/Temp/WKRADAR_SAIDA/";

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
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                folderDialog.SelectedPath = Directory.GetCurrentDirectory();
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    txtDiretorioOrigem.Text = folderDialog.SelectedPath;
                }
            }
        }
        private void txtDiretorioDestino_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                folderDialog.SelectedPath = Directory.GetCurrentDirectory();
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    txtDiretorioDestino.Text = folderDialog.SelectedPath;
                }
            }
        }
    }
}
