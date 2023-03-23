using System;
using System.Threading;
using System.IO;
using System.Security;

namespace WK
{
  public class WKFileStream : FileStream
  {
    protected VersaoArquivo Versao = VersaoArquivo.v2;
    private const int MAX_TENTATIVAS_ABRIR_ARQUIVO = 30;

    protected WKFileStream(
      string path,
      FileMode fileMode,
      FileAccess fileAccess,
      FileShare fileShare,
      VersaoArquivo versao = VersaoArquivo.v2)
      : base(path, fileMode, fileAccess, fileShare)
    {
      this.Versao = versao;
    }

    public static WKFileStream Abre(
      string nomeArquivo,
      ModoAbrirArquivo modo,
      VersaoArquivo versao = VersaoArquivo.v2)
    {
      return WKFileStream.Abre(nomeArquivo, modo, (FileStreamClassFactoryEventHandler) null, versao);
    }

    public static WKFileStream Abre(
      string nomeArquivo,
      ModoAbrirArquivo modo,
      FileStreamClassFactoryEventHandler criaHandler,
      VersaoArquivo versao = VersaoArquivo.v2)
    {
      FileMode fileMode = FileMode.Open;
      FileAccess fileAccess = FileAccess.ReadWrite;
      FileShare fileShare = modo == ModoAbrirArquivo.Leitura ? FileShare.Read : FileShare.None;
      bool podeCriar = (modo & ModoAbrirArquivo.PodeCriar) == ModoAbrirArquivo.PodeCriar;
      if (modo == ModoAbrirArquivo.Leitura)
        fileAccess = FileAccess.Read;
      if ((modo & ModoAbrirArquivo.LimpaConteudo) == ModoAbrirArquivo.LimpaConteudo)
        fileMode = FileMode.Truncate;
      return WKFileStream.InternalAbre(nomeArquivo, fileMode, fileAccess, fileShare, podeCriar, criaHandler, versao);
    }

    public static WKFileStream Abre(
      string nomeArquivo,
      FileMode fileMode,
      FileAccess fileAccess,
      FileShare fileShare)
    {
      return WKFileStream.InternalAbre(nomeArquivo, fileMode, fileAccess, fileShare, false, (FileStreamClassFactoryEventHandler) null);
    }

    private static WKFileStream InternalAbre(
      string nomeArquivo,
      FileMode fileMode,
      FileAccess fileAccess,
      FileShare fileShare,
      bool podeCriar,
      FileStreamClassFactoryEventHandler criaHandler,
      VersaoArquivo versao = VersaoArquivo.v2)
    {
      int num = 0;
      bool flag = false;
      FileStreamClassFactoryEventArgs e = new FileStreamClassFactoryEventArgs(nomeArquivo, fileMode, fileAccess, fileShare, true, versao);
      while (true)
      {
        try
        {
          if (criaHandler == null)
            return new WKFileStream(nomeArquivo, fileMode, fileAccess, fileShare, versao);
          if (flag || File.Exists(nomeArquivo))
          {
            e.UpdateArquivoExiste(!flag);
            e.UpdateFileMode(fileMode);
            return criaHandler((object) null, e);
          }
          if (!podeCriar)
            throw new System.Exception("O arquivo " + nomeArquivo + " não foi encontrado");
          fileMode = fileMode == FileMode.Truncate ? FileMode.Create : FileMode.OpenOrCreate;
          flag = true;
        }
        catch (FileNotFoundException ex)
        {
          if (!podeCriar)
            throw new System.Exception("O arquivo " + nomeArquivo + " não foi encontrado");
          fileMode = fileMode == FileMode.Truncate ? FileMode.Create : FileMode.OpenOrCreate;
          flag = true;
        }
        catch (SecurityException ex)
        {
          throw new System.Exception("Não possui permissão para abrir o arquivo " + nomeArquivo);
        }
        catch (DirectoryNotFoundException ex)
        {
          throw new System.Exception("Não existe o diretório do arquivo " + nomeArquivo);
        }
        catch (PathTooLongException ex)
        {
          throw new System.Exception("O nome do arquivo " + nomeArquivo + " é muito grande");
        }
        catch (IOException ex)
        {
          if (num > 30)
            throw new System.Exception("Erro ao abrir o arquivo " + nomeArquivo);
          Thread.Sleep(500);
        }
        catch (Exception ex)
        {
          throw new System.Exception("Erro ao abrir o arquivo " + nomeArquivo);
        }
        ++num;
      }
    }

    public override int ReadByte()
    {
      try
      {
        return base.ReadByte();
      }
      catch (IOException ex)
      {
        throw new System.Exception("Falha ao ler um byte do arquivo " + this.Name);
      }
    }

    public override int Read(byte[] array, int offset, int count)
    {
      try
      {
        return base.Read(array, offset, count);
      }
      catch (IOException ex)
      {
        throw new System.Exception("Falha ao ler bytes do arquivo " + this.Name);
      }
    }

    public override void Write(byte[] array, int offset, int count)
    {
      try
      {
        base.Write(array, offset, count);
      }
      catch (IOException ex)
      {
        throw new System.Exception("Ocorreu um erro ao escrever no arquivo " + this.Name);
      }
    }

    public override void WriteByte(byte value)
    {
      try
      {
        base.WriteByte(value);
      }
      catch (IOException ex)
      {
        throw new System.Exception("Falha ao escrever no arquivo " + this.Name);
      }
    }

    public override void SetLength(long value)
    {
      try
      {
        base.SetLength(value);
      }
      catch (IOException ex)
      {
        throw new System.Exception("Falha ao alterar o tamanho do arquivo " + this.Name);
      }
    }

    public override void Close()
    {
      try
      {
        base.Close();
      }
      catch (IOException ex)
      {
        throw new System.Exception("Falha ao fechar o arquivo " + this.Name);
      }
    }

    public override long Length
    {
      get
      {
        try
        {
          return base.Length;
        }
        catch (IOException ex)
        {
          throw new System.Exception("Ocorreu um erro ao pegar o tamanho do arquivo " + this.Name);
        }
      }
    }

    public override long Seek(long offset, SeekOrigin origin)
    {
      try
      {
        return base.Seek(offset, origin);
      }
      catch (IOException ex)
      {
        throw new System.Exception("Erro ao alterar a posição de leitura/gravação do arquivo " + this.Name);
      }
    }

    public override IAsyncResult BeginRead(
      byte[] array,
      int offset,
      int numBytes,
      AsyncCallback userCallback,
      object stateObject)
    {
      try
      {
        return base.BeginRead(array, offset, numBytes, userCallback, stateObject);
      }
      catch (IOException ex)
      {
        throw new System.Exception("Erro ao iniciar a leitura assíncrona do arquivo " + this.Name);
      }
    }

    public override int EndRead(IAsyncResult asyncResult)
    {
      try
      {
        return base.EndRead(asyncResult);
      }
      catch (IOException ex)
      {
        throw new System.Exception("Erro ao terminar a leitura assíncrona do arquivo " + this.Name);
      }
    }

    public override IAsyncResult BeginWrite(
      byte[] array,
      int offset,
      int numBytes,
      AsyncCallback userCallback,
      object stateObject)
    {
      try
      {
        return base.BeginWrite(array, offset, numBytes, userCallback, stateObject);
      }
      catch (IOException ex)
      {
        throw new System.Exception("Erro ao iniciar a gravação assíncrona do arquivo " + this.Name);
      }
    }

    public override void EndWrite(IAsyncResult asyncResult)
    {
      try
      {
        base.EndWrite(asyncResult);
      }
      catch (IOException ex)
      {
        throw new System.Exception("Erro ao terminar a gravação assíncrona do arquivo " + this.Name);
      }
    }

    public override void Lock(long position, long length)
    {
      try
      {
        base.Lock(position, length);
      }
      catch (IOException ex)
      {
        throw new System.Exception("Erro ao proteger um bloco do arquivo " + this.Name);
      }
    }

    public override void Unlock(long position, long length)
    {
      try
      {
        base.Unlock(position, length);
      }
      catch (IOException ex)
      {
        throw new System.Exception("Erro ao liberar um bloco do arquivo " + this.Name);
      }
    }

    public override void Flush()
    {
      try
      {
        base.Flush();
      }
      catch (IOException ex)
      {
        throw new System.Exception("Erro ao gravar os dados em buffer no arquivo " + this.Name);
      }
    }

    public void Limpa()
    {
      try
      {
        this.Seek(0L, SeekOrigin.Begin);
        this.SetLength(0L);
      }
      catch (IOException ex)
      {
        throw new System.Exception("Erro ao limpar os dados em buffer no arquivo " + this.Name);
      }
    }
  }
}
