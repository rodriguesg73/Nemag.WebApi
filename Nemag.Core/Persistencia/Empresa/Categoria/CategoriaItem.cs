using System;
using System.Collections.Generic;

namespace Nemag.Core.Persistencia.Empresa.Categoria 
{ 
    public partial class CategoriaItem : _BaseItem, Interface.Empresa.Categoria.ICategoriaItem
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

        public List<Entidade.Empresa.Categoria.CategoriaItem> CarregarLista() 
        { 
            var sql = this.PrepararSelecaoSql(null, null, null); 

            return base.CarregarLista<Entidade.Empresa.Categoria.CategoriaItem>(_databaseItem, sql); 
        } 

        public List<Entidade.Empresa.Categoria.CategoriaItem> CarregarListaPorRegistroSituacaoId(int registroSituacaoId) 
        { 
            var sql = this.PrepararSelecaoSql(null, registroSituacaoId, null); 

            return base.CarregarLista<Entidade.Empresa.Categoria.CategoriaItem>(_databaseItem, sql); 
        } 

        public List<Entidade.Empresa.Categoria.CategoriaItem> CarregarListaPorRegistroLoginId(int registroLoginId) 
        { 
            var sql = this.PrepararSelecaoSql(null, null, registroLoginId); 

            return base.CarregarLista<Entidade.Empresa.Categoria.CategoriaItem>(_databaseItem, sql); 
        } 

        public Entidade.Empresa.Categoria.CategoriaItem CarregarItem(int empresaCategoriaId)
        {
            var sql = this.PrepararSelecaoSql(empresaCategoriaId, null, null); 

            var retorno = base.CarregarItem<Entidade.Empresa.Categoria.CategoriaItem>(_databaseItem, sql); 

            return retorno; 
        }

        public Entidade.Empresa.Categoria.CategoriaItem InserirItem(Entidade.Empresa.Categoria.CategoriaItem categoriaItem)
        {
            var sql = this.PrepararInsercaoSql(categoriaItem); 

            sql += this.ObterUltimoItemInseridoSql();

            return base.CarregarItem<Entidade.Empresa.Categoria.CategoriaItem>(_databaseItem, sql); 
        } 

        public Entidade.Empresa.Categoria.CategoriaItem AtualizarItem(Entidade.Empresa.Categoria.CategoriaItem categoriaItem)
        {
            var sql = this.PrepararAtualizacaoSql(categoriaItem); 

            sql += this.PrepararSelecaoSql(categoriaItem.Id, null, null);

            return base.CarregarItem<Entidade.Empresa.Categoria.CategoriaItem>(_databaseItem, sql); 
        } 

        public Entidade.Empresa.Categoria.CategoriaItem ExcluirItem(Entidade.Empresa.Categoria.CategoriaItem categoriaItem)
        {
            var sql = this.PrepararExclusaoSql(categoriaItem); 

            return base.CarregarItem<Entidade.Empresa.Categoria.CategoriaItem>(_databaseItem, sql); 
        } 

        public Entidade.Empresa.Categoria.CategoriaItem InativarItem(Entidade.Empresa.Categoria.CategoriaItem categoriaItem)
        {
            var sql = this.PrepararInativacaoSql(categoriaItem); 

            return base.CarregarItem<Entidade.Empresa.Categoria.CategoriaItem>(_databaseItem, sql); 
        } 

        #endregion 

        #region Métodos Privados 

        private string PrepararSelecaoSql()
        { 
            var sql = ""; 

            sql += "SELECT \n";
            sql += "    A.EMPRESA_CATEGORIA_ID,\n";
            sql += "    A.DATA_INCLUSAO,\n";
            sql += "    A.DATA_ALTERACAO,\n";
            sql += "    A.REGISTRO_SITUACAO_ID,\n";
            sql += "    A.REGISTRO_LOGIN_ID,\n";
            sql += "    A.NOME\n";
            sql += "FROM \n";
            sql += "    EMPRESA_CATEGORIA_TB A\n";

            return sql; 
        } 

        private string PrepararSelecaoSql(int? empresaCategoriaId, int? registroSituacaoId, int? registroLoginId)
		{ 
			var sql = ""; 

			if (empresaCategoriaId.HasValue)
				sql += "A.EMPRESA_CATEGORIA_ID = " + empresaCategoriaId.Value + "\n";

			if (registroSituacaoId.HasValue)
				sql += "A.REGISTRO_SITUACAO_ID = " + registroSituacaoId.Value + "\n";

			if (registroLoginId.HasValue)
				sql += "A.REGISTRO_LOGIN_ID = " + registroLoginId.Value + "\n";

			if (!empresaCategoriaId.HasValue)
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

        private string PrepararInsercaoSql(Entidade.Empresa.Categoria.CategoriaItem categoriaItem) 
        { 
            var sql = string.Empty; 

            sql += "INSERT INTO EMPRESA_CATEGORIA_TB(\n";
			sql += "    REGISTRO_LOGIN_ID,\n";

			sql += "    NOME,\n";

			sql = sql.Substring(0, sql.Length - 2) + "\n";

			sql += ") VALUES (\n";
			sql += "    " + categoriaItem.RegistroLoginId.ToString() + ",\n";

			    sql += "    '" + categoriaItem.Nome.Replace("'", "''") + "',\n";

			sql = sql.Substring(0, sql.Length - 2) + "\n";

            sql += ");\n";

            return sql; 
        } 

        private string PrepararAtualizacaoSql(Entidade.Empresa.Categoria.CategoriaItem categoriaItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    EMPRESA_CATEGORIA_TB \n";
            sql += "SET\n";
			sql += "    DATA_ALTERACAO = CURRENT_TIMESTAMP,\n";

			sql += "    REGISTRO_LOGIN_ID = " + categoriaItem.RegistroLoginId.ToString() + ",\n"; 

			sql += "    NOME = '" + categoriaItem.Nome.Replace("'", "''") + "',\n";

			sql = sql.Substring(0, sql.Length - 2) + "\n";

            sql += "WHERE\n";
            sql += "    EMPRESA_CATEGORIA_ID = " + categoriaItem.Id + "\n";
            return sql; 
        } 

        private string PrepararExclusaoSql(Entidade.Empresa.Categoria.CategoriaItem categoriaItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    EMPRESA_CATEGORIA_TB\n";
            sql += "SET\n";
            sql += "    REGISTRO_SITUACAO_ID = 3\n";
            sql += "WHERE\n";
            sql += "    EMPRESA_CATEGORIA_ID = " + categoriaItem.Id + "\n";
            return sql; 
        } 

        private string PrepararInativacaoSql(Entidade.Empresa.Categoria.CategoriaItem categoriaItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    EMPRESA_CATEGORIA_TB\n";
            sql += "SET\n";
            sql += "    REGISTRO_SITUACAO_ID = 2\n";
            sql += "WHERE\n";
            sql += "    EMPRESA_CATEGORIA_ID = " + categoriaItem.Id + "\n";
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
					sql += "    A.EMPRESA_CATEGORIA_ID = SCOPE_IDENTITY()\n";

					break;

				case Nemag.Database.Base.DATABASE_TIPO_ID.MYSQL:
					sql += "    A.EMPRESA_CATEGORIA_ID = LAST_INSERT_ID()\n";

					break;
			}

			return sql;
		}

		#endregion
    }
}
