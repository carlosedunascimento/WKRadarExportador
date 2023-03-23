// Decompiled with JetBrains decompiler
// Type: WK.ModoAbrirArquivo
// Assembly: WK.Core, Version=7.4.0.335, Culture=neutral, PublicKeyToken=df21a734793791be
// MVID: A4480DAE-9E75-4415-8D04-5140A1727FB2
// Assembly location: C:\Temp\Radar\x64\WK.Core.dll

using System;

namespace WK
{
  [Flags]
  public enum ModoAbrirArquivo
  {
    Leitura = 1,
    Gravacao = 2,
    PodeCriar = 4,
    LimpaConteudo = 8,
  }
}
