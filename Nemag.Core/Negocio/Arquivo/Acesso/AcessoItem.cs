using System;
using System.Collections.Generic;

namespace Nemag.Core.Negocio.Arquivo.Acesso 
{ 
    public partial class AcessoItem : _BaseItem
    { 
        #region Propriedades 

        private Interface.Arquivo.Acesso.IAcessoItem _persistenciaAcessoItem { get; set; } 

        #endregion 

        #region Construtores 

        public AcessoItem() 
            : this(new Persistencia.Arquivo.Acesso.AcessoItem()) 
        { } 

        public AcessoItem(Interface.Arquivo.Acesso.IAcessoItem persistenciaAcessoItem) 
        { 
            this._persistenciaAcessoItem = persistenciaAcessoItem; 
        } 

        #endregion 

        #region Métodos Públicos 

        public List<Entidade.Arquivo.Acesso.AcessoItem> CarregarLista() 
        { 
            return _persistenciaAcessoItem.CarregarLista(); 
        } 

        public List<Entidade.Arquivo.Acesso.AcessoItem> CarregarListaPorArquivoId(int arquivoId) 
        { 
            return _persistenciaAcessoItem.CarregarListaPorArquivoId(arquivoId); 
        } 

        public List<Entidade.Arquivo.Acesso.AcessoItem> CarregarListaPorRegistroLoginId(int registroLoginId) 
        { 
            return _persistenciaAcessoItem.CarregarListaPorRegistroLoginId(registroLoginId); 
        } 

        public Entidade.Arquivo.Acesso.AcessoItem CarregarItem(int arquivoAcessoId)
        {
            return _persistenciaAcessoItem.CarregarItem(arquivoAcessoId);
        }

        public Entidade.Arquivo.Acesso.AcessoItem InserirItem(Entidade.Arquivo.Acesso.AcessoItem acessoItem)
        {
            return _persistenciaAcessoItem.InserirItem(acessoItem); 
        } 

        public Entidade.Arquivo.Acesso.AcessoItem AtualizarItem(Entidade.Arquivo.Acesso.AcessoItem acessoItem)
        {
            return _persistenciaAcessoItem.AtualizarItem(acessoItem); 
        } 

        public Entidade.Arquivo.Acesso.AcessoItem ExcluirItem(Entidade.Arquivo.Acesso.AcessoItem acessoItem)
        {
            return _persistenciaAcessoItem.ExcluirItem(acessoItem); 
        } 

        public Entidade.Arquivo.Acesso.AcessoItem InativarItem(Entidade.Arquivo.Acesso.AcessoItem acessoItem)
        {
            return _persistenciaAcessoItem.InativarItem(acessoItem); 
        } 

        public Entidade.Arquivo.Acesso.AcessoItem SalvarItem(Entidade.Arquivo.Acesso.AcessoItem acessoItem)
        {
            if (acessoItem.Id.Equals(0))
                acessoItem = this.InserirItem(acessoItem);
            else
                acessoItem = this.AtualizarItem(acessoItem);

            return acessoItem;
        }

        #endregion 
    } 
} 
