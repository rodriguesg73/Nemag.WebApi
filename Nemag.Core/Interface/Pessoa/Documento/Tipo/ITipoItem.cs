using System.Collections.Generic;

namespace Nemag.Core.Interface.Pessoa.Documento.Tipo 
{ 
    public partial interface ITipoItem
    { 
        List<Entidade.Pessoa.Documento.Tipo.TipoItem> CarregarLista(); 

        Entidade.Pessoa.Documento.Tipo.TipoItem CarregarItem(int pessoaDocumentoTipoId);

        Entidade.Pessoa.Documento.Tipo.TipoItem InserirItem(Entidade.Pessoa.Documento.Tipo.TipoItem tipoItem); 

        Entidade.Pessoa.Documento.Tipo.TipoItem AtualizarItem(Entidade.Pessoa.Documento.Tipo.TipoItem tipoItem); 

        Entidade.Pessoa.Documento.Tipo.TipoItem ExcluirItem(Entidade.Pessoa.Documento.Tipo.TipoItem tipoItem); 

        Entidade.Pessoa.Documento.Tipo.TipoItem InativarItem(Entidade.Pessoa.Documento.Tipo.TipoItem tipoItem); 
    } 
} 
