using System.Collections.Generic;

namespace Nemag.Core.Interface.Empresa 
{ 
    public partial interface IEmpresaItem
    { 
        List<Entidade.Empresa.EmpresaItem> CarregarLista(); 

        List<Entidade.Empresa.EmpresaItem> CarregarListaPorRegistroLoginId(int registroLoginId); 

        List<Entidade.Empresa.EmpresaItem> CarregarListaPorPessoaId(int pessoaId); 

        List<Entidade.Empresa.EmpresaItem> CarregarListaPorEmpresaCategoriaId(int empresaCategoriaId); 

        Entidade.Empresa.EmpresaItem CarregarItem(int empresaId);

        Entidade.Empresa.EmpresaItem InserirItem(Entidade.Empresa.EmpresaItem empresaItem); 

        Entidade.Empresa.EmpresaItem AtualizarItem(Entidade.Empresa.EmpresaItem empresaItem); 

        Entidade.Empresa.EmpresaItem ExcluirItem(Entidade.Empresa.EmpresaItem empresaItem); 

        Entidade.Empresa.EmpresaItem InativarItem(Entidade.Empresa.EmpresaItem empresaItem); 
    } 
} 
