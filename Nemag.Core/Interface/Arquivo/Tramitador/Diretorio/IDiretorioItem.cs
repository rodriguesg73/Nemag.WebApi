using System.Collections.Generic;

namespace Nemag.Core.Interface.Arquivo.Tramitador.Diretorio 
{ 
    public partial interface IDiretorioItem
    { 
        List<Entidade.Arquivo.Tramitador.Diretorio.DiretorioItem> CarregarLista(); 

        List<Entidade.Arquivo.Tramitador.Diretorio.DiretorioItem> CarregarListaPorRegistroLoginId(int registroLoginId); 

        List<Entidade.Arquivo.Tramitador.Diretorio.DiretorioItem> CarregarListaPorArquivoTramitadorId(int arquivoTramitadorId); 

        List<Entidade.Arquivo.Tramitador.Diretorio.DiretorioItem> CarregarListaPorArquivoTramitadorAcaoId(int arquivoTramitadorAcaoId); 

        Entidade.Arquivo.Tramitador.Diretorio.DiretorioItem CarregarItem(int arquivoTramitadorDiretorioId);

        Entidade.Arquivo.Tramitador.Diretorio.DiretorioItem InserirItem(Entidade.Arquivo.Tramitador.Diretorio.DiretorioItem diretorioItem); 

        Entidade.Arquivo.Tramitador.Diretorio.DiretorioItem AtualizarItem(Entidade.Arquivo.Tramitador.Diretorio.DiretorioItem diretorioItem); 

        Entidade.Arquivo.Tramitador.Diretorio.DiretorioItem ExcluirItem(Entidade.Arquivo.Tramitador.Diretorio.DiretorioItem diretorioItem); 

        Entidade.Arquivo.Tramitador.Diretorio.DiretorioItem InativarItem(Entidade.Arquivo.Tramitador.Diretorio.DiretorioItem diretorioItem); 
    } 
} 
