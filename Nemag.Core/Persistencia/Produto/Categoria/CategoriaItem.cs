using System;
using System.Collections.Generic;

namespace Nemag.Core.Persistencia.Produto.Categoria 
{ 
    public partial class CategoriaItem : _BaseItem, Interface.Produto.Categoria.ICategoriaItem
    { 
        #region Propriedades

        private Nemag.Database.DatabaseItem _databaseItem { get; set; }

        #endregion

        #region Construtores

        public CategoriaItem() : this(new Nemag.Database.DatabaseItem())
        { }

        public CategoriaItem(Nemag.Database.DatabaseItem databaseItem)
        {
	            _databaseItem = databaseItem;
        }

        #endregion

        #region Métodos Públicos 

        public List<Entidade.Produto.Categoria.CategoriaItem> CarregarLista() 
        { 
            var sql = this.PrepararSelecaoSql(null, null, null); 

            return base.CarregarLista<Entidade.Produto.Categoria.CategoriaItem>(_databaseItem, sql); 
        } 

        public List<Entidade.Produto.Categoria.CategoriaItem> CarregarListaPorRegistroSituacaoId(int registroSituacaoId) 
        { 
            var sql = this.PrepararSelecaoSql(null, registroSituacaoId, null); 

            return base.CarregarLista<Entidade.Produto.Categoria.CategoriaItem>(_databaseItem, sql); 
        } 

        public List<Entidade.Produto.Categoria.CategoriaItem> CarregarListaPorRegistroLoginId(int registroLoginId) 
        { 
            var sql = this.PrepararSelecaoSql(null, null, registroLoginId); 

            return base.CarregarLista<Entidade.Produto.Categoria.CategoriaItem>(_databaseItem, sql); 
        } 

        public Entidade.Produto.Categoria.CategoriaItem CarregarItem(int produtoCategoriaId)
        {
            var sql = this.PrepararSelecaoSql(produtoCategoriaId, null, null); 

            var retorno = base.CarregarItem<Entidade.Produto.Categoria.CategoriaItem>(_databaseItem, sql); 

            return retorno; 
        }

        public Entidade.Produto.Categoria.CategoriaItem InserirItem(Entidade.Produto.Categoria.CategoriaItem categoriaItem)
        {
            var sql = this.PrepararInsercaoSql(categoriaItem); 

            sql += this.ObterUltimoItemInseridoSql();

            return base.CarregarItem<Entidade.Produto.Categoria.CategoriaItem>(_databaseItem, sql); 
        } 

        public Entidade.Produto.Categoria.CategoriaItem AtualizarItem(Entidade.Produto.Categoria.CategoriaItem categoriaItem)
        {
            var sql = this.PrepararAtualizacaoSql(categoriaItem); 

            sql += this.PrepararSelecaoSql(categoriaItem.Id, null, null);

            return base.CarregarItem<Entidade.Produto.Categoria.CategoriaItem>(_databaseItem, sql); 
        } 

        public Entidade.Produto.Categoria.CategoriaItem ExcluirItem(Entidade.Produto.Categoria.CategoriaItem categoriaItem)
        {
            var sql = this.PrepararExclusaoSql(categoriaItem); 

            return base.CarregarItem<Entidade.Produto.Categoria.CategoriaItem>(_databaseItem, sql); 
        } 

        public Entidade.Produto.Categoria.CategoriaItem InativarItem(Entidade.Produto.Categoria.CategoriaItem categoriaItem)
        {
            var sql = this.PrepararInativacaoSql(categoriaItem); 

            return base.CarregarItem<Entidade.Produto.Categoria.CategoriaItem>(_databaseItem, sql); 
        } 

        #endregion 

        #region Métodos Privados 

        private string PrepararSelecaoSql()
        { 
            var sql = ""; 

            sql += "SELECT \n";
            sql += "    A.PRODUTO_CATEGORIA_ID,\n";
            sql += "    A.DATA_INCLUSAO,\n";
            sql += "    A.DATA_ALTERACAO,\n";
            sql += "    A.REGISTRO_SITUACAO_ID,\n";
            sql += "    A.REGISTRO_LOGIN_ID,\n";
            sql += "    A.NOME\n";
            sql += "FROM \n";
            sql += "    PRODUTO_CATEGORIA_TB A\n";

            return sql; 
        } 

        private string PrepararSelecaoSql(int? produtoCategoriaId, int? registroSituacaoId, int? registroLoginId)
		{ 
			var sql = ""; 

			if (produtoCategoriaId.HasValue)
				sql += "A.PRODUTO_CATEGORIA_ID = " + produtoCategoriaId.Value + "\n";

			if (registroSituacaoId.HasValue)
				sql += "A.REGISTRO_SITUACAO_ID = " + registroSituacaoId.Value + "\n";

			if (registroLoginId.HasValue)
				sql += "A.REGISTRO_LOGIN_ID = " + registroLoginId.Value + "\n";

			if (!produtoCategoriaId.HasValue)
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

        private string PrepararInsercaoSql(Entidade.Produto.Categoria.CategoriaItem categoriaItem) 
        { 
            var sql = string.Empty; 

            sql += "INSERT INTO PRODUTO_CATEGORIA_TB(\n";
			sql += "    REGISTRO_LOGIN_ID,\n";

			sql += "    NOME,\n";

			sql = sql.Substring(0, sql.Length - 2) + "\n";

			sql += ") VALUES (\n";
			sql += "    " + categoriaItem.RegistroLoginId.ToString() + ",\n";

			    sql += "    '" + categoriaItem.Nome.Replace("'", "''").ToUpper() + "',\n";

			sql = sql.Substring(0, sql.Length - 2) + "\n";

            sql += ");\n";

            return sql; 
        } 

        private string PrepararAtualizacaoSql(Entidade.Produto.Categoria.CategoriaItem categoriaItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    PRODUTO_CATEGORIA_TB \n";
            sql += "SET\n";
			sql += "    DATA_ALTERACAO = CURRENT_TIMESTAMP,\n";

			sql += "    REGISTRO_LOGIN_ID = " + categoriaItem.RegistroLoginId.ToString() + ",\n"; 

			sql += "    NOME = '" + categoriaItem.Nome.Replace("'", "''") + "',\n";

			sql = sql.Substring(0, sql.Length - 2) + "\n";

            sql += "WHERE\n";
            sql += "    PRODUTO_CATEGORIA_ID = " + categoriaItem.Id + "\n";
            return sql; 
        } 

        private string PrepararExclusaoSql(Entidade.Produto.Categoria.CategoriaItem categoriaItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    PRODUTO_CATEGORIA_TB\n";
            sql += "SET\n";
            sql += "    REGISTRO_SITUACAO_ID = 3\n";
            sql += "WHERE\n";
            sql += "    PRODUTO_CATEGORIA_ID = " + categoriaItem.Id + "\n";
            return sql; 
        } 

        private string PrepararInativacaoSql(Entidade.Produto.Categoria.CategoriaItem categoriaItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    PRODUTO_CATEGORIA_TB\n";
            sql += "SET\n";
            sql += "    REGISTRO_SITUACAO_ID = 2\n";
            sql += "WHERE\n";
            sql += "    PRODUTO_CATEGORIA_ID = " + categoriaItem.Id + "\n";
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
					sql += "    A.PRODUTO_CATEGORIA_ID = SCOPE_IDENTITY()\n";

					break;

				case Nemag.Database.Base.DATABASE_TIPO_ID.MYSQL:
					sql += "    A.PRODUTO_CATEGORIA_ID = LAST_INSERT_ID()\n";

					break;
			}

			return sql;
		}

		#endregion
    }
}
