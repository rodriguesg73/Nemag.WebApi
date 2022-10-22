using System;
using System.Collections.Generic;

namespace Nemag.Core.Persistencia.Produto 
{ 
    public partial class ProdutoItem : _BaseItem, Interface.Produto.IProdutoItem
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

        public List<Entidade.Produto.ProdutoItem> CarregarLista() 
        { 
            var sql = this.PrepararSelecaoSql(null, null, null, null); 

            return base.CarregarLista<Entidade.Produto.ProdutoItem>(_databaseItem, sql); 
        } 

        public List<Entidade.Produto.ProdutoItem> CarregarListaPorRegistroSituacaoId(int registroSituacaoId) 
        { 
            var sql = this.PrepararSelecaoSql(null, registroSituacaoId, null, null); 

            return base.CarregarLista<Entidade.Produto.ProdutoItem>(_databaseItem, sql); 
        } 

        public List<Entidade.Produto.ProdutoItem> CarregarListaPorRegistroLoginId(int registroLoginId) 
        { 
            var sql = this.PrepararSelecaoSql(null, null, registroLoginId, null); 

            return base.CarregarLista<Entidade.Produto.ProdutoItem>(_databaseItem, sql); 
        } 

        public List<Entidade.Produto.ProdutoItem> CarregarListaPorProdutoCategoriaId(int produtoCategoriaId) 
        { 
            var sql = this.PrepararSelecaoSql(null, null, null, produtoCategoriaId); 

            return base.CarregarLista<Entidade.Produto.ProdutoItem>(_databaseItem, sql); 
        } 

        public Entidade.Produto.ProdutoItem CarregarItem(int produtoId)
        {
            var sql = this.PrepararSelecaoSql(produtoId, null, null, null); 

            var retorno = base.CarregarItem<Entidade.Produto.ProdutoItem>(_databaseItem, sql); 

            return retorno; 
        }

        public Entidade.Produto.ProdutoItem InserirItem(Entidade.Produto.ProdutoItem produtoItem)
        {
            var sql = this.PrepararInsercaoSql(produtoItem); 

            sql += this.ObterUltimoItemInseridoSql();

            return base.CarregarItem<Entidade.Produto.ProdutoItem>(_databaseItem, sql); 
        } 

        public Entidade.Produto.ProdutoItem AtualizarItem(Entidade.Produto.ProdutoItem produtoItem)
        {
            var sql = this.PrepararAtualizacaoSql(produtoItem); 

            sql += this.PrepararSelecaoSql(produtoItem.Id, null, null, null);

            return base.CarregarItem<Entidade.Produto.ProdutoItem>(_databaseItem, sql); 
        } 

        public Entidade.Produto.ProdutoItem ExcluirItem(Entidade.Produto.ProdutoItem produtoItem)
        {
            var sql = this.PrepararExclusaoSql(produtoItem); 

            return base.CarregarItem<Entidade.Produto.ProdutoItem>(_databaseItem, sql); 
        } 

        public Entidade.Produto.ProdutoItem InativarItem(Entidade.Produto.ProdutoItem produtoItem)
        {
            var sql = this.PrepararInativacaoSql(produtoItem); 

            return base.CarregarItem<Entidade.Produto.ProdutoItem>(_databaseItem, sql); 
        } 

        #endregion 

        #region Métodos Privados 

        private string PrepararSelecaoSql()
        { 
            var sql = ""; 

            sql += "SELECT \n";
            sql += "    A.PRODUTO_ID,\n";
            sql += "    A.DATA_INCLUSAO,\n";
            sql += "    A.DATA_ALTERACAO,\n";
            sql += "    A.REGISTRO_SITUACAO_ID,\n";
            sql += "    A.REGISTRO_LOGIN_ID,\n";
            sql += "    A.NOME,\n";
            sql += "    A.DESCRICAO,\n";
            sql += "    A.VALOR,\n";
            sql += "    A.CODIGO,\n";
            sql += "    A.PRODUTO_CATEGORIA_ID,\n";
            sql += "    A1.PRODUTO_CATEGORIA_ID AS PRODUTO_CATEGORIA_ID,\n";
            sql += "    A1.NOME AS PRODUTO_CATEGORIA_NOME\n";
            sql += "FROM \n";
            sql += "    PRODUTO_TB A\n";
            sql += "    INNER JOIN PRODUTO_CATEGORIA_TB A1 ON A1.PRODUTO_CATEGORIA_ID = A.PRODUTO_CATEGORIA_ID\n";

            return sql; 
        } 

        private string PrepararSelecaoSql(int? produtoId, int? registroSituacaoId, int? registroLoginId, int? produtoCategoriaId)
		{ 
			var sql = ""; 

			if (produtoId.HasValue)
				sql += "A.PRODUTO_ID = " + produtoId.Value + "\n";

			if (registroSituacaoId.HasValue)
				sql += "A.REGISTRO_SITUACAO_ID = " + registroSituacaoId.Value + "\n";

			if (registroLoginId.HasValue)
				sql += "A.REGISTRO_LOGIN_ID = " + registroLoginId.Value + "\n";

			if (produtoCategoriaId.HasValue)
				sql += "A.PRODUTO_CATEGORIA_ID = " + produtoCategoriaId.Value + "\n";

			if (!produtoId.HasValue)
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

        private string PrepararInsercaoSql(Entidade.Produto.ProdutoItem produtoItem) 
        { 
            var sql = string.Empty; 

            sql += "INSERT INTO PRODUTO_TB(\n";
			sql += "    REGISTRO_LOGIN_ID,\n";

			sql += "    NOME,\n";

			sql += "    DESCRICAO,\n";

			sql += "    VALOR,\n";

			sql += "    CODIGO,\n";

			sql += "    PRODUTO_CATEGORIA_ID,\n";

			sql = sql.Substring(0, sql.Length - 2) + "\n";

			sql += ") VALUES (\n";
			sql += "    " + produtoItem.RegistroLoginId.ToString() + ",\n";

			if (string.IsNullOrEmpty(produtoItem.Nome))
			    sql += "    NULL,\n";
			else
			    sql += "    '" + produtoItem.Nome.Replace("'", "''") + "',\n";

			if (string.IsNullOrEmpty(produtoItem.Descricao))
			    sql += "    NULL,\n";
			else
			    sql += "    '" + produtoItem.Descricao.Replace("'", "''") + "',\n";

			sql += "    " + (produtoItem.Valor > decimal.MinValue ? produtoItem.Valor.ToString().Replace(",", ".") : "NULL") + ",\n";

			if (string.IsNullOrEmpty(produtoItem.Codigo))
			    sql += "    NULL,\n";
			else
			    sql += "    '" + produtoItem.Codigo.Replace("'", "''") + "',\n";

			sql += "    " + produtoItem.ProdutoCategoriaId.ToString() + ",\n";

			sql = sql.Substring(0, sql.Length - 2) + "\n";

            sql += ");\n";

            return sql; 
        } 

        private string PrepararAtualizacaoSql(Entidade.Produto.ProdutoItem produtoItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    PRODUTO_TB \n";
            sql += "SET\n";
			sql += "    DATA_ALTERACAO = CURRENT_TIMESTAMP,\n";

			sql += "    REGISTRO_LOGIN_ID = " + produtoItem.RegistroLoginId.ToString() + ",\n"; 

			if (string.IsNullOrEmpty(produtoItem.Nome))
			    sql += "    NOME = NULL,\n";
			else
				sql += "    NOME = '" + produtoItem.Nome.Replace("'", "''") + "',\n";

			if (string.IsNullOrEmpty(produtoItem.Descricao))
			    sql += "    DESCRICAO = NULL,\n";
			else
				sql += "    DESCRICAO = '" + produtoItem.Descricao.Replace("'", "''") + "',\n";

			sql += "    VALOR = " + (produtoItem.Valor > decimal.MinValue ? produtoItem.Valor.ToString().Replace(",", ".") : "NULL") + ",\n"; 

			if (string.IsNullOrEmpty(produtoItem.Codigo))
			    sql += "    CODIGO = NULL,\n";
			else
				sql += "    CODIGO = '" + produtoItem.Codigo.Replace("'", "''") + "',\n";

			sql += "    PRODUTO_CATEGORIA_ID = " + produtoItem.ProdutoCategoriaId.ToString() + ",\n"; 

			sql = sql.Substring(0, sql.Length - 2) + "\n";

            sql += "WHERE\n";
            sql += "    PRODUTO_ID = " + produtoItem.Id + "\n";
            return sql; 
        } 

        private string PrepararExclusaoSql(Entidade.Produto.ProdutoItem produtoItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    PRODUTO_TB\n";
            sql += "SET\n";
            sql += "    REGISTRO_SITUACAO_ID = 3\n";
            sql += "WHERE\n";
            sql += "    PRODUTO_ID = " + produtoItem.Id + "\n";
            return sql; 
        } 

        private string PrepararInativacaoSql(Entidade.Produto.ProdutoItem produtoItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    PRODUTO_TB\n";
            sql += "SET\n";
            sql += "    REGISTRO_SITUACAO_ID = 2\n";
            sql += "WHERE\n";
            sql += "    PRODUTO_ID = " + produtoItem.Id + "\n";
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
					sql += "    A.PRODUTO_ID = SCOPE_IDENTITY()\n";

					break;

				case Nemag.Database.Base.DATABASE_TIPO_ID.MYSQL:
					sql += "    A.PRODUTO_ID = LAST_INSERT_ID()\n";

					break;
			}

			return sql;
		}

		#endregion
    }
}
