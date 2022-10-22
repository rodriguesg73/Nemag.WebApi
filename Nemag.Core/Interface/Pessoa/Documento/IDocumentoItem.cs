using System.Collections.Generic;

namespace Nemag.Core.Interface.Pessoa.Documento 
{ 
    public partial interface IDocumentoItem
    { 
        List<Entidade.Pessoa.Documento.DocumentoItem> CarregarLista(); 

        List<Entidade.Pessoa.Documento.DocumentoItem> CarregarListaPorPessoaDocumentoTipoId(int pessoaDocumentoTipoId); 

        Entidade.Pessoa.Documento.DocumentoItem CarregarItem(int pessoaDocumentoId);

        Entidade.Pessoa.Documento.DocumentoItem InserirItem(Entidade.Pessoa.Documento.DocumentoItem documentoItem); 

        Entidade.Pessoa.Documento.DocumentoItem AtualizarItem(Entidade.Pessoa.Documento.DocumentoItem documentoItem); 

        Entidade.Pessoa.Documento.DocumentoItem ExcluirItem(Entidade.Pessoa.Documento.DocumentoItem documentoItem); 

        Entidade.Pessoa.Documento.DocumentoItem InativarItem(Entidade.Pessoa.Documento.DocumentoItem documentoItem); 
    } 
} 
