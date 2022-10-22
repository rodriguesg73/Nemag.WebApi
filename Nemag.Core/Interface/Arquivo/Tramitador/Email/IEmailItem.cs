using System.Collections.Generic;

namespace Nemag.Core.Interface.Arquivo.Tramitador.Email 
{ 
    public partial interface IEmailItem
    { 
        List<Entidade.Arquivo.Tramitador.Email.EmailItem> CarregarLista(); 

        List<Entidade.Arquivo.Tramitador.Email.EmailItem> CarregarListaPorRegistroLoginId(int registroLoginId); 

        List<Entidade.Arquivo.Tramitador.Email.EmailItem> CarregarListaPorArquivoTramitadorId(int arquivoTramitadorId); 

        List<Entidade.Arquivo.Tramitador.Email.EmailItem> CarregarListaPorArquivoTramitadorAcaoId(int arquivoTramitadorAcaoId); 

        Entidade.Arquivo.Tramitador.Email.EmailItem CarregarItem(int arquivoTramitadorEmailId);

        Entidade.Arquivo.Tramitador.Email.EmailItem InserirItem(Entidade.Arquivo.Tramitador.Email.EmailItem emailItem); 

        Entidade.Arquivo.Tramitador.Email.EmailItem AtualizarItem(Entidade.Arquivo.Tramitador.Email.EmailItem emailItem); 

        Entidade.Arquivo.Tramitador.Email.EmailItem ExcluirItem(Entidade.Arquivo.Tramitador.Email.EmailItem emailItem); 

        Entidade.Arquivo.Tramitador.Email.EmailItem InativarItem(Entidade.Arquivo.Tramitador.Email.EmailItem emailItem); 
    } 
} 
