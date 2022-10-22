using System;
using System.Collections.Generic;

namespace Nemag.Core.Negocio.Pessoa.Contato.Tipo 
{ 
    public partial class TipoItem : _BaseItem
    { 
        #region Propriedades 

        private Interface.Pessoa.Contato.Tipo.ITipoItem _persistenciaTipoItem { get; set; } 

        #endregion 

        #region Construtores 

        public TipoItem() 
            : this(new Persistencia.Pessoa.Contato.Tipo.TipoItem()) 
        { } 

        public TipoItem(Interface.Pessoa.Contato.Tipo.ITipoItem persistenciaTipoItem) 
        { 
            this._persistenciaTipoItem = persistenciaTipoItem; 
        } 

        #endregion 

        #region Métodos Públicos 

        public List<Entidade.Pessoa.Contato.Tipo.TipoItem> CarregarLista() 
        { 
            return _persistenciaTipoItem.CarregarLista(); 
        } 

        public List<Entidade.Pessoa.Contato.Tipo.TipoItem> CarregarListaPorRegistroLoginId(int registroLoginId) 
        { 
            return _persistenciaTipoItem.CarregarListaPorRegistroLoginId(registroLoginId); 
        } 

        public Entidade.Pessoa.Contato.Tipo.TipoItem CarregarItem(int pessoaContatoTipoId)
        {
            return _persistenciaTipoItem.CarregarItem(pessoaContatoTipoId);
        }

        public Entidade.Pessoa.Contato.Tipo.TipoItem InserirItem(Entidade.Pessoa.Contato.Tipo.TipoItem tipoItem)
        {
            return _persistenciaTipoItem.InserirItem(tipoItem); 
        } 

        public Entidade.Pessoa.Contato.Tipo.TipoItem AtualizarItem(Entidade.Pessoa.Contato.Tipo.TipoItem tipoItem)
        {
            return _persistenciaTipoItem.AtualizarItem(tipoItem); 
        } 

        public Entidade.Pessoa.Contato.Tipo.TipoItem ExcluirItem(Entidade.Pessoa.Contato.Tipo.TipoItem tipoItem)
        {
            return _persistenciaTipoItem.ExcluirItem(tipoItem); 
        } 

        public Entidade.Pessoa.Contato.Tipo.TipoItem InativarItem(Entidade.Pessoa.Contato.Tipo.TipoItem tipoItem)
        {
            return _persistenciaTipoItem.InativarItem(tipoItem); 
        } 

        public Entidade.Pessoa.Contato.Tipo.TipoItem SalvarItem(Entidade.Pessoa.Contato.Tipo.TipoItem tipoItem)
        {
            if (tipoItem.Id.Equals(0))
                tipoItem = this.InserirItem(tipoItem);
            else
                tipoItem = this.AtualizarItem(tipoItem);

            return tipoItem;
        }

        #endregion 
    } 
} 
