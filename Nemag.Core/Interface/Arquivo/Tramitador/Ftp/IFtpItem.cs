using System.Collections.Generic;

namespace Nemag.Core.Interface.Arquivo.Tramitador.Ftp 
{ 
    public partial interface IFtpItem
    { 
        List<Entidade.Arquivo.Tramitador.Ftp.FtpItem> CarregarLista(); 

        List<Entidade.Arquivo.Tramitador.Ftp.FtpItem> CarregarListaPorRegistroLoginId(int registroLoginId); 

        List<Entidade.Arquivo.Tramitador.Ftp.FtpItem> CarregarListaPorArquivoTramitadorId(int arquivoTramitadorId); 

        List<Entidade.Arquivo.Tramitador.Ftp.FtpItem> CarregarListaPorArquivoTramitadorAcaoId(int arquivoTramitadorAcaoId); 

        Entidade.Arquivo.Tramitador.Ftp.FtpItem CarregarItem(int arquivoTramitadorFtpId);

        Entidade.Arquivo.Tramitador.Ftp.FtpItem InserirItem(Entidade.Arquivo.Tramitador.Ftp.FtpItem ftpItem); 

        Entidade.Arquivo.Tramitador.Ftp.FtpItem AtualizarItem(Entidade.Arquivo.Tramitador.Ftp.FtpItem ftpItem); 

        Entidade.Arquivo.Tramitador.Ftp.FtpItem ExcluirItem(Entidade.Arquivo.Tramitador.Ftp.FtpItem ftpItem); 

        Entidade.Arquivo.Tramitador.Ftp.FtpItem InativarItem(Entidade.Arquivo.Tramitador.Ftp.FtpItem ftpItem); 
    } 
} 
