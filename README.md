# WKRadarExportador
Exportador da famigerada base de dados da WK Sistemas

Você que sofreu, assim como eu sofri, para exportar informações do WK Radar a partir de relatórios do sistema, porque a empresa não lhe ofereceu NENHUMA alternativa de acesso aos SEUS DADOS. Desenvolvi esta aplicação em C# que faz a leitura dos arquivos de dados XML em FileStream e salvam os mesmos em TextPlain com estrutura de XML, possibilitando a leitura e tratamento dos dados. 

Esta aplicação NÃO FAZ qualquer alteração nos arquivos de dados, ela apenas abre os arquivos em modo leitura, lê o seu conteúdo e salva em NOVOS arquivos em um diretório que você selecionou.

Preencha COM ATENÇÃO ao campo diretório de DESTINO, para evitar que ele sobreponha os arquivos originais.

Acho que nem preciso recomendar que faça um backup antes, ou realize o procedimento em outros diretórios e/ou outro computador.

Decidi fazer essa contribuição devido à uma necessidade antiga de programadores e clientes desta empresa que praticamente escolheu "reinventar a roda", tendo em vista a existência de várias opções servidores de bancos de dados pagos e gratuitos premiados e utilizados internacionalmente por pequenas à grandes empresas.

- Microsoft Visual Studio 2022
- Microsoft .NET Framework V4.8
