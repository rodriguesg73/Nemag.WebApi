using System;
using System.Collections.Generic;

namespace Nemag.Core.Negocio.Empresa 
{ 
    public partial class EmpresaItem : Pessoa.PessoaItem
    { 
        #region Propriedades 

        private Interface.Empresa.IEmpresaItem _persistenciaEmpresaItem { get; set; } 

        #endregion 

        #region Construtores 

        public EmpresaItem() 
            : this(new Persistencia.Empresa.EmpresaItem()) 
        { } 

        public EmpresaItem(Interface.Empresa.IEmpresaItem persistenciaEmpresaItem) 
        { 
            this._persistenciaEmpresaItem = persistenciaEmpresaItem; 
        } 

        #endregion 

        #region Métodos Públicos 

        public new List<Entidade.Empresa.EmpresaItem> CarregarLista() 
        { 
            return _persistenciaEmpresaItem.CarregarLista(); 
        } 

        public List<Entidade.Empresa.EmpresaItem> CarregarListaPorRegistroLoginId(int registroLoginId) 
        { 
            return _persistenciaEmpresaItem.CarregarListaPorRegistroLoginId(registroLoginId); 
        } 

        public List<Entidade.Empresa.EmpresaItem> CarregarListaPorPessoaId(int pessoaId) 
        { 
            return _persistenciaEmpresaItem.CarregarListaPorPessoaId(pessoaId); 
        } 

        public List<Entidade.Empresa.EmpresaItem> CarregarListaPorEmpresaCategoriaId(int empresaCategoriaId) 
        { 
            return _persistenciaEmpresaItem.CarregarListaPorEmpresaCategoriaId(empresaCategoriaId); 
        } 

        public new Entidade.Empresa.EmpresaItem CarregarItem(int empresaId)
        {
            return _persistenciaEmpresaItem.CarregarItem(empresaId);
        }

        public Entidade.Empresa.EmpresaItem InserirItem(Entidade.Empresa.EmpresaItem empresaItem)
        {
            return _persistenciaEmpresaItem.InserirItem(empresaItem); 
        } 

        public Entidade.Empresa.EmpresaItem AtualizarItem(Entidade.Empresa.EmpresaItem empresaItem)
        {
            return _persistenciaEmpresaItem.AtualizarItem(empresaItem); 
        } 

        public Entidade.Empresa.EmpresaItem ExcluirItem(Entidade.Empresa.EmpresaItem empresaItem)
        {
            return _persistenciaEmpresaItem.ExcluirItem(empresaItem); 
        } 

        public Entidade.Empresa.EmpresaItem InativarItem(Entidade.Empresa.EmpresaItem empresaItem)
        {
            return _persistenciaEmpresaItem.InativarItem(empresaItem); 
        } 

        public Entidade.Empresa.EmpresaItem SalvarItem(Entidade.Empresa.EmpresaItem empresaItem)
        {
            var pessoaItem = empresaItem.Clone<Entidade.Pessoa.PessoaItem>();

            pessoaItem.Id = empresaItem.PessoaId;

            pessoaItem = base.SalvarItem(pessoaItem);

            empresaItem.PessoaId = pessoaItem.Id;

            if (empresaItem.Id.Equals(0))
                empresaItem = this.InserirItem(empresaItem);
            else
                empresaItem = this.AtualizarItem(empresaItem);

            return empresaItem;
        }

        #endregion 
    } 
} 
