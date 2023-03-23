using System;

namespace WK
{
  [Flags]
  public enum VersaoArquivo
  {
    v1 = 1,
    v2 = 2,
    Todos = v2 | v1,
  }
}
