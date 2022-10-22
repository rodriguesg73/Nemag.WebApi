using System;
using System.Collections.Generic;

namespace Nemag.Core.Negocio.Arquivo 
{ 
    public partial class ArquivoItem : _BaseItem
    { 
        #region Propriedades 

        private Interface.Arquivo.IArquivoItem _persistenciaArquivoItem { get; set; } 

        #endregion 

        #region Construtores 

        public ArquivoItem() 
            : this(new Persistencia.Arquivo.ArquivoItem()) 
        { } 

        public ArquivoItem(Interface.Arquivo.IArquivoItem persistenciaArquivoItem) 
        { 
            this._persistenciaArquivoItem = persistenciaArquivoItem; 
        } 

        #endregion 

        #region Métodos Públicos 

        public List<Entidade.Arquivo.ArquivoItem> CarregarLista() 
        { 
            return _persistenciaArquivoItem.CarregarLista(); 
        } 

        public List<Entidade.Arquivo.ArquivoItem> CarregarListaPorRegistroLoginId(int registroLoginId) 
        { 
            return _persistenciaArquivoItem.CarregarListaPorRegistroLoginId(registroLoginId); 
        } 

        public Entidade.Arquivo.ArquivoItem CarregarItem(int arquivoId)
        {
            return _persistenciaArquivoItem.CarregarItem(arquivoId);
        }

        public Entidade.Arquivo.ArquivoItem InserirItem(Entidade.Arquivo.ArquivoItem arquivoItem)
        {
            return _persistenciaArquivoItem.InserirItem(arquivoItem); 
        } 

        public Entidade.Arquivo.ArquivoItem AtualizarItem(Entidade.Arquivo.ArquivoItem arquivoItem)
        {
            return _persistenciaArquivoItem.AtualizarItem(arquivoItem); 
        } 

        public Entidade.Arquivo.ArquivoItem ExcluirItem(Entidade.Arquivo.ArquivoItem arquivoItem)
        {
            return _persistenciaArquivoItem.ExcluirItem(arquivoItem); 
        } 

        public Entidade.Arquivo.ArquivoItem InativarItem(Entidade.Arquivo.ArquivoItem arquivoItem)
        {
            return _persistenciaArquivoItem.InativarItem(arquivoItem); 
        } 

        public Entidade.Arquivo.ArquivoItem SalvarItem(Entidade.Arquivo.ArquivoItem arquivoItem)
        {
            if (arquivoItem.Id.Equals(0))
                arquivoItem = this.InserirItem(arquivoItem);
            else
                arquivoItem = this.AtualizarItem(arquivoItem);

            return arquivoItem;
        }

        #endregion 
    } 
} 
