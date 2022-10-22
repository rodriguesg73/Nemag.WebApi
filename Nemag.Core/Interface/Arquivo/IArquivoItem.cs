using System.Collections.Generic;

namespace Nemag.Core.Interface.Arquivo 
{ 
    public partial interface IArquivoItem
    { 
        List<Entidade.Arquivo.ArquivoItem> CarregarLista(); 

        List<Entidade.Arquivo.ArquivoItem> CarregarListaPorRegistroLoginId(int registroLoginId); 

        Entidade.Arquivo.ArquivoItem CarregarItem(int arquivoId);

        Entidade.Arquivo.ArquivoItem InserirItem(Entidade.Arquivo.ArquivoItem arquivoItem); 

        Entidade.Arquivo.ArquivoItem AtualizarItem(Entidade.Arquivo.ArquivoItem arquivoItem); 

        Entidade.Arquivo.ArquivoItem ExcluirItem(Entidade.Arquivo.ArquivoItem arquivoItem); 

        Entidade.Arquivo.ArquivoItem InativarItem(Entidade.Arquivo.ArquivoItem arquivoItem); 
    } 
} 
