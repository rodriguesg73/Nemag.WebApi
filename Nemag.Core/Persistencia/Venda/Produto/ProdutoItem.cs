using System;
using System.Collections.Generic;

namespace Nemag.Core.Persistencia.Venda.Produto 
{ 
    public partial class ProdutoItem : _BaseItem, Interface.Venda.Produto.IProdutoItem
    { 
        #region Propriedades

        private Nemag.Database.DatabaseItem _databaseItem { get; set; }

        #endregion

        #region Construtores

        public ProdutoItem() : this(new Nemag.Database.DatabaseItem())
        { }

        public ProdutoItem(Nemag.Database.DatabaseItem databaseItem)
        {
	            _databaseItem = databaseItem;
        }

        #endregion

        #region Métodos Públicos 

        public List<Entidade.Venda.Produto.ProdutoItem> CarregarLista() 
        { 
            var sql = this.PrepararSelecaoSql(null, null, null, null, null); 

            return base.CarregarLista<Entidade.Venda.Produto.ProdutoItem>(_databaseItem, sql); 
        } 

        public List<Entidade.Venda.Produto.ProdutoItem> CarregarListaPorRegistroSituacaoId(int registroSituacaoId) 
        { 
            var sql = this.PrepararSelecaoSql(null, registroSituacaoId, null, null, null); 

            return base.CarregarLista<Entidade.Venda.Produto.ProdutoItem>(_databaseItem, sql); 
        } 

        public List<Entidade.Venda.Produto.ProdutoItem> CarregarListaPorRegistroLoginId(int registroLoginId) 
        { 
            var sql = this.PrepararSelecaoSql(null, null, registroLoginId, null, null); 

            return base.CarregarLista<Entidade.Venda.Produto.ProdutoItem>(_databaseItem, sql); 
        } 

        public List<Entidade.Venda.Produto.ProdutoItem> CarregarListaPorProdutoId(int produtoId) 
        { 
            var sql = this.PrepararSelecaoSql(null, null, null, produtoId, null); 

            return base.CarregarLista<Entidade.Venda.Produto.ProdutoItem>(_databaseItem, sql); 
        } 

        public List<Entidade.Venda.Produto.ProdutoItem> CarregarListaPorVendaId(int vendaId) 
        { 
            var sql = this.PrepararSelecaoSql(null, null, null, null, vendaId); 

            return base.CarregarLista<Entidade.Venda.Produto.ProdutoItem>(_databaseItem, sql); 
        } 

        public Entidade.Venda.Produto.ProdutoItem CarregarItem(int vendaProdutoId)
        {
            var sql = this.PrepararSelecaoSql(vendaProdutoId, null, null, null, null); 

            var retorno = base.CarregarItem<Entidade.Venda.Produto.ProdutoItem>(_databaseItem, sql); 

            return retorno; 
        }

        public Entidade.Venda.Produto.ProdutoItem InserirItem(Entidade.Venda.Produto.ProdutoItem produtoItem)
        {
            var sql = this.PrepararInsercaoSql(produtoItem); 

            sql += this.ObterUltimoItemInseridoSql();

            return base.CarregarItem<Entidade.Venda.Produto.ProdutoItem>(_databaseItem, sql); 
        } 

        public Entidade.Venda.Produto.ProdutoItem AtualizarItem(Entidade.Venda.Produto.ProdutoItem produtoItem)
        {
            var sql = this.PrepararAtualizacaoSql(produtoItem); 

            sql += this.PrepararSelecaoSql(produtoItem.Id, null, null, null, null);

            return base.CarregarItem<Entidade.Venda.Produto.ProdutoItem>(_databaseItem, sql); 
        } 

        public Entidade.Venda.Produto.ProdutoItem ExcluirItem(Entidade.Venda.Produto.ProdutoItem produtoItem)
        {
            var sql = this.PrepararExclusaoSql(produtoItem); 

            return base.CarregarItem<Entidade.Venda.Produto.ProdutoItem>(_databaseItem, sql); 
        } 

        public Entidade.Venda.Produto.ProdutoItem InativarItem(Entidade.Venda.Produto.ProdutoItem produtoItem)
        {
            var sql = this.PrepararInativacaoSql(produtoItem); 

            return base.CarregarItem<Entidade.Venda.Produto.ProdutoItem>(_databaseItem, sql); 
        } 

        #endregion 

        #region Métodos Privados 

        private string PrepararSelecaoSql()
        { 
            var sql = ""; 

            sql += "SELECT \n";
            sql += "    A.VENDA_PRODUTO_ID,\n";
            sql += "    A.DATA_INCLUSAO,\n";
            sql += "    A.DATA_ALTERACAO,\n";
            sql += "    A.REGISTRO_SITUACAO_ID,\n";
            sql += "    A.REGISTRO_LOGIN_ID,\n";
            sql += "    A.PRODUTO_ID,\n";
            sql += "    A.VENDA_ID,\n";
            sql += "    A4.PRODUTO_CATEGORIA_ID AS PRODUTO_CATEGORIA_ID,\n";
            sql += "    A4.NOME AS PRODUTO_CATEGORIA_NOME,\n";
            sql += "    A2.PRODUTO_ID AS PRODUTO_ID,\n";
            sql += "    A2.NOME AS PRODUTO_NOME,\n";
            sql += "    A2.DESCRICAO AS PRODUTO_DESCRICAO,\n";
            sql += "    A2.VALOR AS PRODUTO_VALOR,\n";
            sql += "    A2.CODIGO AS PRODUTO_CODIGO,\n";
            sql += "    A5.PESSOA_ID AS VENDA_CLIENTE_PESSOA_ID,\n";
            sql += "    A5.NOME AS VENDA_CLIENTE_NOME,\n";
            sql += "    A3.CLIENTE_ID AS VENDA_CLIENTE_ID,\n";
            sql += "    A1.VENDA_ID AS VENDA_ID,\n";
            sql += "    A1.VALOR AS VENDA_VALOR\n";
            sql += "FROM \n";
            sql += "    VENDA_PRODUTO_TB A\n";
            sql += "    INNER JOIN PRODUTO_TB A2 ON A2.PRODUTO_ID = A.PRODUTO_ID\n";
            sql += "    INNER JOIN PRODUTO_CATEGORIA_TB A4 ON A4.PRODUTO_CATEGORIA_ID = A2.PRODUTO_CATEGORIA_ID\n";
            sql += "    INNER JOIN VENDA_TB A1 ON A1.VENDA_ID = A.VENDA_ID\n";
            sql += "    INNER JOIN CLIENTE_TB A3 ON A3.CLIENTE_ID = A1.CLIENTE_ID\n";
            sql += "    INNER JOIN PESSOA_TB A5 ON A5.PESSOA_ID = A3.PESSOA_ID\n";

            return sql; 
        } 

        private string PrepararSelecaoSql(int? vendaProdutoId, int? registroSituacaoId, int? registroLoginId, int? produtoId, int? vendaId)
		{ 
			var sql = ""; 

			if (vendaProdutoId.HasValue)
				sql += "A.VENDA_PRODUTO_ID = " + vendaProdutoId.Value + "\n";

			if (registroSituacaoId.HasValue)
				sql += "A.REGISTRO_SITUACAO_ID = " + registroSituacaoId.Value + "\n";

			if (registroLoginId.HasValue)
				sql += "A.REGISTRO_LOGIN_ID = " + registroLoginId.Value + "\n";

			if (produtoId.HasValue)
				sql += "A.PRODUTO_ID = " + produtoId.Value + "\n";

			if (vendaId.HasValue)
				sql += "A.VENDA_ID = " + vendaId.Value + "\n";

			if (!vendaProdutoId.HasValue)
				sql += "A.REGISTRO_SITUACAO_ID <> 3\n";

            if (!string.IsNullOrEmpty(sql))
            {
                sql = sql.Substring(0, sql.Length - 1);

                sql = sql.Replace("\n", "\nAND "); 

                sql = "WHERE\n\t" + sql; 
            } 

            sql = this.PrepararSelecaoSql() + " " + sql;

            return sql; 
        } 

        private string PrepararInsercaoSql(Entidade.Venda.Produto.ProdutoItem produtoItem) 
        { 
            var sql = string.Empty; 

            sql += "INSERT INTO VENDA_PRODUTO_TB(\n";
			sql += "    REGISTRO_LOGIN_ID,\n";

			sql += "    PRODUTO_ID,\n";

			sql += "    VENDA_ID,\n";

			sql = sql.Substring(0, sql.Length - 2) + "\n";

			sql += ") VALUES (\n";
			sql += "    " + produtoItem.RegistroLoginId.ToString() + ",\n";

			sql += "    " + produtoItem.ProdutoId.ToString() + ",\n";

			sql += "    " + produtoItem.VendaId.ToString() + ",\n";

			sql = sql.Substring(0, sql.Length - 2) + "\n";

            sql += ");\n";

            return sql; 
        } 

        private string PrepararAtualizacaoSql(Entidade.Venda.Produto.ProdutoItem produtoItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    VENDA_PRODUTO_TB \n";
            sql += "SET\n";
			sql += "    DATA_ALTERACAO = CURRENT_TIMESTAMP,\n";

			sql += "    REGISTRO_LOGIN_ID = " + produtoItem.RegistroLoginId.ToString() + ",\n"; 

			sql += "    PRODUTO_ID = " + produtoItem.ProdutoId.ToString() + ",\n"; 

			sql += "    VENDA_ID = " + produtoItem.VendaId.ToString() + ",\n"; 

			sql = sql.Substring(0, sql.Length - 2) + "\n";

            sql += "WHERE\n";
            sql += "    VENDA_PRODUTO_ID = " + produtoItem.Id + "\n";
            return sql; 
        } 

        private string PrepararExclusaoSql(Entidade.Venda.Produto.ProdutoItem produtoItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    VENDA_PRODUTO_TB\n";
            sql += "SET\n";
            sql += "    REGISTRO_SITUACAO_ID = 3\n";
            sql += "WHERE\n";
            sql += "    VENDA_PRODUTO_ID = " + produtoItem.Id + "\n";
            return sql; 
        } 

        private string PrepararInativacaoSql(Entidade.Venda.Produto.ProdutoItem produtoItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    VENDA_PRODUTO_TB\n";
            sql += "SET\n";
            sql += "    REGISTRO_SITUACAO_ID = 2\n";
            sql += "WHERE\n";
            sql += "    VENDA_PRODUTO_ID = " + produtoItem.Id + "\n";
            return sql; 
        } 

        #endregion 

		#region Métodos Específicos do Banco

		private string ObterUltimoItemInseridoSql()
		{
			var sql = this.PrepararSelecaoSql();

			sql += "WHERE \n";

			var databaseItem = new Nemag.Database.DatabaseItem();

			switch (databaseItem.DatabaseTipoId)
			{
				case Nemag.Database.Base.DATABASE_TIPO_ID.MSSQL:
					sql += "    A.VENDA_PRODUTO_ID = SCOPE_IDENTITY()\n";

					break;

				case Nemag.Database.Base.DATABASE_TIPO_ID.MYSQL:
					sql += "    A.VENDA_PRODUTO_ID = LAST_INSERT_ID()\n";

					break;
			}

			return sql;
		}

		#endregion
    }
}
