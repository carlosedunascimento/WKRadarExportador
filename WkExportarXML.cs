using System.Text;
using System.IO;
using WK;

namespace WKRadarExportador
{
    public class WkExportarXML {
        private string _diretorioOrigem;
        private string _diretorioDestino;

        public WkExportarXML(string DiretorioOrigem, string DiretorioDestino) {
            this._diretorioOrigem = DiretorioOrigem;
            this._diretorioDestino = DiretorioDestino;
        }

        public void Processar() {
            this.ProcessarDiretorio();
        }

        private void ProcessarDiretorio() {
            string diretorio = this._diretorioOrigem;
            string[] listaArquivos = Directory.GetFiles(diretorio, "*.xml", SearchOption.AllDirectories);
            foreach (string arquivo in listaArquivos)
                ProcessarArquivo(arquivo);
        }
        private void ProcessarArquivo(string arquivo)
        {
            string arquivoDestino = arquivo.Replace(this._diretorioOrigem, this._diretorioDestino);
            if (!Directory.Exists(Path.GetDirectoryName(arquivoDestino))) {
                Directory.CreateDirectory(Path.GetDirectoryName(arquivoDestino));
            }

            var xmlFileStream = XmlFileStream.Abre(arquivo, ModoAbrirArquivo.Leitura);
            int num = (int)xmlFileStream.Length;
            if (xmlFileStream.Encriptado)
            {
                num -= 2;
                xmlFileStream.Seek(2L, SeekOrigin.Begin);
            }

            byte[] buffer = new byte[num];
            xmlFileStream.Read(buffer, 0, num);
            string conteudo = this.Read(ref buffer);
            conteudo = conteudo.Replace("\"utf-7\"", "\"utf-8\"");
            File.WriteAllText(arquivoDestino, conteudo, Encoding.UTF8);
        }

        internal unsafe string Read(ref byte[] buffer)
        {
            if (buffer.Length <= 0)
            {
                return "";
            }
            else
            {
                fixed (byte* value = buffer)
                {
                    return new string((sbyte*)value, 0, buffer.Length);
                }
            }
        }
    }
}
