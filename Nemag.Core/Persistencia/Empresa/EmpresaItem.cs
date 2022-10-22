using System;
using System.Collections.Generic;

namespace Nemag.Core.Persistencia.Empresa 
{ 
    public partial class EmpresaItem : _BaseItem, Interface.Empresa.IEmpresaItem
    { 
        #region Propriedades

        private Nemag.Database.DatabaseItem _databaseItem { get; set; }

        #endregion

        #region Construtores

        public EmpresaItem() : this(new Nemag.Database.DatabaseItem())
        { }

        public EmpresaItem(Nemag.Database.DatabaseItem databaseItem)
        {
	            _databaseItem = databaseItem;
        }

        #endregion

        #region Métodos Públicos 

        public List<Entidade.Empresa.EmpresaItem> CarregarLista() 
        { 
            var sql = this.PrepararSelecaoSql(null, null, null, null, null); 

            return base.CarregarLista<Entidade.Empresa.EmpresaItem>(_databaseItem, sql); 
        } 

        public List<Entidade.Empresa.EmpresaItem> CarregarListaPorRegistroSituacaoId(int registroSituacaoId) 
        { 
            var sql = this.PrepararSelecaoSql(null, registroSituacaoId, null, null, null); 

            return base.CarregarLista<Entidade.Empresa.EmpresaItem>(_databaseItem, sql); 
        } 

        public List<Entidade.Empresa.EmpresaItem> CarregarListaPorRegistroLoginId(int registroLoginId) 
        { 
            var sql = this.PrepararSelecaoSql(null, null, registroLoginId, null, null); 

            return base.CarregarLista<Entidade.Empresa.EmpresaItem>(_databaseItem, sql); 
        } 

        public List<Entidade.Empresa.EmpresaItem> CarregarListaPorPessoaId(int pessoaId) 
        { 
            var sql = this.PrepararSelecaoSql(null, null, null, pessoaId, null); 

            return base.CarregarLista<Entidade.Empresa.EmpresaItem>(_databaseItem, sql); 
        } 

        public List<Entidade.Empresa.EmpresaItem> CarregarListaPorEmpresaCategoriaId(int empresaCategoriaId) 
        { 
            var sql = this.PrepararSelecaoSql(null, null, null, null, empresaCategoriaId); 

            return base.CarregarLista<Entidade.Empresa.EmpresaItem>(_databaseItem, sql); 
        } 

        public Entidade.Empresa.EmpresaItem CarregarItem(int empresaId)
        {
            var sql = this.PrepararSelecaoSql(empresaId, null, null, null, null); 

            var retorno = base.CarregarItem<Entidade.Empresa.EmpresaItem>(_databaseItem, sql); 

            return retorno; 
        }

        public Entidade.Empresa.EmpresaItem InserirItem(Entidade.Empresa.EmpresaItem empresaItem)
        {
            var sql = this.PrepararInsercaoSql(empresaItem); 

            sql += this.ObterUltimoItemInseridoSql();

            return base.CarregarItem<Entidade.Empresa.EmpresaItem>(_databaseItem, sql); 
        } 

        public Entidade.Empresa.EmpresaItem AtualizarItem(Entidade.Empresa.EmpresaItem empresaItem)
        {
            var sql = this.PrepararAtualizacaoSql(empresaItem); 

            sql += this.PrepararSelecaoSql(empresaItem.Id, null, null, null, null);

            return base.CarregarItem<Entidade.Empresa.EmpresaItem>(_databaseItem, sql); 
        } 

        public Entidade.Empresa.EmpresaItem ExcluirItem(Entidade.Empresa.EmpresaItem empresaItem)
        {
            var sql = this.PrepararExclusaoSql(empresaItem); 

            return base.CarregarItem<Entidade.Empresa.EmpresaItem>(_databaseItem, sql); 
        } 

        public Entidade.Empresa.EmpresaItem InativarItem(Entidade.Empresa.EmpresaItem empresaItem)
        {
            var sql = this.PrepararInativacaoSql(empresaItem); 

            return base.CarregarItem<Entidade.Empresa.EmpresaItem>(_databaseItem, sql); 
        } 

        #endregion 

        #region Métodos Privados 

        private string PrepararSelecaoSql()
        { 
            var sql = ""; 

            sql += "SELECT \n";
            sql += "    A.EMPRESA_ID,\n";
            sql += "    A.DATA_INCLUSAO,\n";
            sql += "    A.DATA_ALTERACAO,\n";
            sql += "    A.REGISTRO_SITUACAO_ID,\n";
            sql += "    A.REGISTRO_LOGIN_ID,\n";
            sql += "    A.PESSOA_ID,\n";
            sql += "    A.EMPRESA_CATEGORIA_ID,\n";
            sql += "    A.NOME_EXIBICAO,\n";
            sql += "    A1.PESSOA_ID AS PESSOA_ID,\n";
            sql += "    A1.NOME AS NOME,\n";
            sql += "    A2.EMPRESA_CATEGORIA_ID AS EMPRESA_CATEGORIA_ID,\n";
            sql += "    A2.NOME AS EMPRESA_CATEGORIA_NOME\n";
            sql += "FROM \n";
            sql += "    EMPRESA_TB A\n";
            sql += "    INNER JOIN PESSOA_TB A1 ON A1.PESSOA_ID = A.PESSOA_ID\n";
            sql += "    INNER JOIN EMPRESA_CATEGORIA_TB A2 ON A2.EMPRESA_CATEGORIA_ID = A.EMPRESA_CATEGORIA_ID\n";

            return sql; 
        } 

        private string PrepararSelecaoSql(int? empresaId, int? registroSituacaoId, int? registroLoginId, int? pessoaId, int? empresaCategoriaId)
		{ 
			var sql = ""; 

			if (empresaId.HasValue)
				sql += "A.EMPRESA_ID = " + empresaId.Value + "\n";

			if (registroSituacaoId.HasValue)
				sql += "A.REGISTRO_SITUACAO_ID = " + registroSituacaoId.Value + "\n";

			if (registroLoginId.HasValue)
				sql += "A.REGISTRO_LOGIN_ID = " + registroLoginId.Value + "\n";

			if (pessoaId.HasValue)
				sql += "A.PESSOA_ID = " + pessoaId.Value + "\n";

			if (empresaCategoriaId.HasValue)
				sql += "A.EMPRESA_CATEGORIA_ID = " + empresaCategoriaId.Value + "\n";

			if (!empresaId.HasValue)
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

        private string PrepararInsercaoSql(Entidade.Empresa.EmpresaItem empresaItem) 
        { 
            var sql = string.Empty; 

            sql += "INSERT INTO EMPRESA_TB(\n";
			sql += "    REGISTRO_LOGIN_ID,\n";

			sql += "    PESSOA_ID,\n";

			sql += "    EMPRESA_CATEGORIA_ID,\n";

			sql += "    NOME_EXIBICAO,\n";

			sql = sql.Substring(0, sql.Length - 2) + "\n";

			sql += ") VALUES (\n";
			sql += "    " + empresaItem.RegistroLoginId.ToString() + ",\n";

			sql += "    " + empresaItem.PessoaId.ToString() + ",\n";

			sql += "    " + empresaItem.EmpresaCategoriaId.ToString() + ",\n";

			if (string.IsNullOrEmpty(empresaItem.NomeExibicao))
			    sql += "    NULL,\n";
			else
			    sql += "    '" + empresaItem.NomeExibicao.Replace("'", "''") + "',\n";

			sql = sql.Substring(0, sql.Length - 2) + "\n";

            sql += ");\n";

            return sql; 
        } 

        private string PrepararAtualizacaoSql(Entidade.Empresa.EmpresaItem empresaItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    EMPRESA_TB \n";
            sql += "SET\n";
			sql += "    DATA_ALTERACAO = CURRENT_TIMESTAMP,\n";

			sql += "    REGISTRO_LOGIN_ID = " + empresaItem.RegistroLoginId.ToString() + ",\n"; 

			sql += "    PESSOA_ID = " + empresaItem.PessoaId.ToString() + ",\n"; 

			sql += "    EMPRESA_CATEGORIA_ID = " + empresaItem.EmpresaCategoriaId.ToString() + ",\n"; 

			if (string.IsNullOrEmpty(empresaItem.NomeExibicao))
			    sql += "    NOME_EXIBICAO = NULL,\n";
			else
				sql += "    NOME_EXIBICAO = '" + empresaItem.NomeExibicao.Replace("'", "''") + "',\n";

			sql = sql.Substring(0, sql.Length - 2) + "\n";

            sql += "WHERE\n";
            sql += "    EMPRESA_ID = " + empresaItem.Id + "\n";
            return sql; 
        } 

        private string PrepararExclusaoSql(Entidade.Empresa.EmpresaItem empresaItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    EMPRESA_TB\n";
            sql += "SET\n";
            sql += "    REGISTRO_SITUACAO_ID = 3\n";
            sql += "WHERE\n";
            sql += "    EMPRESA_ID = " + empresaItem.Id + "\n";
            return sql; 
        } 

        private string PrepararInativacaoSql(Entidade.Empresa.EmpresaItem empresaItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    EMPRESA_TB\n";
            sql += "SET\n";
            sql += "    REGISTRO_SITUACAO_ID = 2\n";
            sql += "WHERE\n";
            sql += "    EMPRESA_ID = " + empresaItem.Id + "\n";
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
					sql += "    A.EMPRESA_ID = SCOPE_IDENTITY()\n";

					break;

				case Nemag.Database.Base.DATABASE_TIPO_ID.MYSQL:
					sql += "    A.EMPRESA_ID = LAST_INSERT_ID()\n";

					break;
			}

			return sql;
		}

		#endregion
    }
}
