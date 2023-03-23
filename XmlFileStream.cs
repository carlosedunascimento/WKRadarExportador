// Decompiled with JetBrains decompiler
// Type: WK.Xml.XmlFileStream
// Assembly: WK.Core, Version=7.4.0.335, Culture=neutral, PublicKeyToken=df21a734793791be
// MVID: A4480DAE-9E75-4415-8D04-5140A1727FB2
// Assembly location: C:\Temp\Radar\x64\WK.Core.dll

using System;
using System.IO;

namespace WK
{
  public class XmlFileStream : WKFileStream
  {
    public const int BYTES_USED_BY_CHECKSUM = 2;
    public const int USE_CHECKSUM_MARK = 0;
    private static readonly byte[] ALPHABET_v1 = new byte[66]
    {
      (byte) 107,
      (byte) 106,
      (byte) 51,
      (byte) 56,
      (byte) 46,
      (byte) 43,
      (byte) 45,
      (byte) 48,
      (byte) 64,
      (byte) 51,
      (byte) 36,
      (byte) 37,
      (byte) 42,
      (byte) 44,
      (byte) 99,
      (byte) 42,
      (byte) 47,
      (byte) 45,
      (byte) 35,
      (byte) 36,
      (byte) 46,
      (byte) 46,
      (byte) 44,
      (byte) 47,
      (byte) 60,
      (byte) 62,
      (byte) 47,
      (byte) 59,
      (byte) 108,
      (byte) 74,
      (byte) 65,
      (byte) 122,
      (byte) 120,
      (byte) 116,
      (byte) 114,
      (byte) 111,
      (byte) 112,
      (byte) 91,
      (byte) 93,
      (byte) 92,
      (byte) 124,
      (byte) 97,
      (byte) 126,
      (byte) 96,
      (byte) 64,
      (byte) 51,
      (byte) 49,
      (byte) 50,
      (byte) 51,
      (byte) 105,
      (byte) 109,
      (byte) 97,
      (byte) 46,
      (byte) 45,
      (byte) 61,
      (byte) 51,
      (byte) 42,
      (byte) 38,
      (byte) 94,
      (byte) 37,
      (byte) 52,
      (byte) 51,
      (byte) 95,
      (byte) 45,
      (byte) 58,
      (byte) 65
    };
    private static readonly byte[] ALPHABET_v2 = new byte[66]
    {
      (byte) 68,
      (byte) 37,
      (byte) 76,
      (byte) 96,
      (byte) 41,
      (byte) 71,
      (byte) 108,
      (byte) 35,
      (byte) 72,
      (byte) 102,
      (byte) 126,
      (byte) 41,
      (byte) 63,
      (byte) 60,
      (byte) 107,
      (byte) 86,
      (byte) 78,
      (byte) 106,
      (byte) 40,
      (byte) 82,
      (byte) 108,
      (byte) 93,
      (byte) 35,
      (byte) 126,
      (byte) 111,
      (byte) 60,
      (byte) 81,
      (byte) 73,
      (byte) 100,
      (byte) 79,
      (byte) 50,
      (byte) 123,
      (byte) 100,
      (byte) 67,
      (byte) 101,
      (byte) 79,
      (byte) 99,
      (byte) 107,
      (byte) 60,
      (byte) 105,
      (byte) 116,
      (byte) 103,
      (byte) 117,
      (byte) 89,
      (byte) 105,
      (byte) 50,
      (byte) 116,
      (byte) 37,
      (byte) 119,
      (byte) 80,
      (byte) 37,
      (byte) 79,
      (byte) 50,
      (byte) 109,
      (byte) 73,
      (byte) 94,
      (byte) 111,
      (byte) 59,
      (byte) 72,
      (byte) 111,
      (byte) 96,
      (byte) 69,
      (byte) 36,
      (byte) 55,
      (byte) 74,
      (byte) 43
    };
    private bool encriptado;
    private bool wasWriting;
    private int readCheckSum;
    private int writeCheckSum;
    private int alphabetPos;

    public static XmlFileStream Abre(
      string path,
      ModoAbrirArquivo modo,
      VersaoArquivo versao = VersaoArquivo.v2)
    {
      XmlFileStream.XmlFileStreamClassFactory streamClassFactory = new XmlFileStream.XmlFileStreamClassFactory();
      return WKFileStream.Abre(path, modo, new FileStreamClassFactoryEventHandler(streamClassFactory.Cria), versao) as XmlFileStream;
    }

    internal XmlFileStream(
      string path,
      FileMode fileMode,
      FileAccess fileAccess,
      FileShare fileShare,
      VersaoArquivo versao = VersaoArquivo.v2)
      : base(path, fileMode, fileAccess, fileShare, versao)
    {
      if (fileAccess != FileAccess.Read && fileAccess != FileAccess.ReadWrite)
        return;
      this.InitReading();
    }

    private void InitReading()
    {
      try
      {
        this.readCheckSum = 0;
        this.alphabetPos = 0;
        if (this.Length <= 0L)
          return;
        if (base.ReadByte() == 0)
        {
          this.Encriptado = true;
          this.readCheckSum = base.ReadByte();
          byte[] array = new byte[this.Length - 2L];
          base.Read(array, 0, array.Length);
          int num = 0;
          for (int index = 0; index < array.Length; ++index)
            num += (int) array[index];
          if (num % (int) byte.MaxValue != this.readCheckSum)
          {
            this.Seek(2L, SeekOrigin.Begin);
            throw new System.Exception("O arquivo " + this.Name + " está corrompido.");
          }
        }
        this.Seek(0L, SeekOrigin.Begin);
      }
      catch (IOException ex)
      {
        throw new System.Exception("Falha ao ler o arquivo " + this.Name);
      }
    }

    private byte Cript(byte value)
    {
      if (this.Versao == VersaoArquivo.v1)
      {
        if (this.alphabetPos >= XmlFileStream.ALPHABET_v1.Length)
          this.alphabetPos = 0;
        return (byte) ((int) value ^ (int) XmlFileStream.ALPHABET_v1[this.alphabetPos++]);
      }
      if (this.alphabetPos >= XmlFileStream.ALPHABET_v2.Length)
        this.alphabetPos = 0;
      return (byte) ((int) value ^ (int) XmlFileStream.ALPHABET_v2[this.alphabetPos++]);
    }

    public override int ReadByte()
    {
      try
      {
        int num = base.ReadByte();
        if (this.Encriptado)
          num = (int) this.Cript((byte) num);
        return num;
      }
      catch (IOException ex)
      {
        throw new System.Exception("Falha ao ler um byte do arquivo " + this.Name);
      }
    }

    internal int PureRead(byte[] array, int offset, int count)
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

    public override int Read(byte[] array, int offset, int count)
    {
      try
      {
        int num = base.Read(array, offset, count);
        if (this.Encriptado)
        {
          for (int index = 0; index < num; ++index)
            array[index] = this.Cript(array[index]);
        }
        return num;
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
        if (this.Encriptado)
        {
          int index = offset;
          for (; count > 0; --count)
          {
            this.WriteByte(array[index]);
            ++index;
          }
        }
        else
        {
          for (; count > 0; --count)
          {
            base.WriteByte(XmlFileStream.SafeXmlByte(array[offset]));
            ++offset;
          }
        }
      }
      catch (IOException ex)
      {
        throw new System.Exception("Ocorreu um erro ao escrever no arquivo " + this.Name);
      }
    }

    public static byte SafeXmlByte(byte b)
    {
      switch (b)
      {
        case 19:
          b = (byte) 45;
          break;
        case 25:
          b = (byte) 39;
          break;
        case 28:
        case 29:
          b = (byte) 34;
          break;
      }
      return b;
    }

    public override void WriteByte(byte value)
    {
      try
      {
        if (!this.wasWriting)
          this.InitWriting();
        if (this.Encriptado)
        {
          value = this.Cript(XmlFileStream.SafeXmlByte(value));
          this.writeCheckSum += (int) value;
          base.WriteByte(value);
        }
        else
          base.WriteByte(XmlFileStream.SafeXmlByte(value));
      }
      catch (IOException ex)
      {
        throw new System.Exception("Falha ao escrever no arquivo " + this.Name);
      }
    }

    public bool Encriptado
    {
      get => this.encriptado;
      set
      {
        if (this.encriptado == value)
          return;
        if (this.wasWriting)
          throw new System.Exception(string.Format("Tentativa de criptografar o arquivo {0} após início da gravação"));
        this.encriptado = value;
      }
    }

    private void InitWriting()
    {
      try
      {
        this.writeCheckSum = 0;
        this.alphabetPos = 0;
        this.wasWriting = true;
        if (!this.Encriptado)
          return;
        if (this.Position != 0L)
          throw new System.Exception(string.Format("Posição do arquivo {0} não está no início", (object) this.Name));
        base.WriteByte((byte) 0);
        base.WriteByte(byte.MaxValue);
      }
      catch (IOException ex)
      {
        throw new System.Exception("Falha ao gravar no arquivo " + this.Name);
      }
    }

    public override void SetLength(long value)
    {
      try
      {
        if (value == 0L)
          this.alphabetPos = 0;
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
        if (!this.Encriptado || !this.wasWriting)
          return;
        this.Seek(0L, SeekOrigin.Begin);
        if (base.ReadByte() != 0)
          throw new System.Exception(string.Format("Tentativa de gravar check sum em arquivo não criptografado {0}", (object) this.Name));
        base.WriteByte((byte) (this.writeCheckSum % (int) byte.MaxValue));
      }
      catch (IOException ex)
      {
        throw new System.Exception("Falha ao fechar o arquivo " + this.Name);
      }
      finally
      {
        base.Close();
      }
    }

    private class XmlFileStreamClassFactory
    {
      internal XmlFileStreamClassFactory()
      {
      }

      internal WKFileStream Cria(object sender, FileStreamClassFactoryEventArgs e) => (WKFileStream) new XmlFileStream(e.NomeArquivo, e.FileMode, e.FileAccess, e.FileShare, e.Versao);
    }
  }
}
