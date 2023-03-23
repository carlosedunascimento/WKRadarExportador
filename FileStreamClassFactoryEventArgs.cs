// Decompiled with JetBrains decompiler
// Type: WK.FileStreamClassFactoryEventArgs
// Assembly: WK.Core, Version=7.4.0.335, Culture=neutral, PublicKeyToken=df21a734793791be
// MVID: A4480DAE-9E75-4415-8D04-5140A1727FB2
// Assembly location: C:\Temp\Radar\x64\WK.Core.dll

using System;
using System.IO;

namespace WK
{
  public class FileStreamClassFactoryEventArgs : EventArgs
  {
    private string nomeArquivo;
    private FileMode fileMode;
    private FileAccess fileAccess;
    private FileShare fileShare;
    private bool arquivoExiste;

    public FileStreamClassFactoryEventArgs(
      string nomeArquivo,
      FileMode fileMode,
      FileAccess fileAccess,
      FileShare fileShare,
      bool arquivoExiste,
      VersaoArquivo versao)
    {
      this.nomeArquivo = nomeArquivo;
      this.fileMode = fileMode;
      this.fileAccess = fileAccess;
      this.fileShare = fileShare;
      this.arquivoExiste = arquivoExiste;
      this.Versao = versao;
    }

    public string NomeArquivo => this.nomeArquivo;

    public FileMode FileMode => this.fileMode;

    public FileShare FileShare => this.fileShare;

    public FileAccess FileAccess => this.fileAccess;

    public bool ArquivoExiste => this.arquivoExiste;

    internal void UpdateArquivoExiste(bool value) => this.arquivoExiste = value;

    internal void UpdateFileMode(FileMode value) => this.fileMode = value;

    public VersaoArquivo Versao { get; set; }
  }
}
