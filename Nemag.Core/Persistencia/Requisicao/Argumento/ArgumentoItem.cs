using System;
using System.Collections.Generic;

namespace Nemag.Core.Persistencia.Requisicao.Argumento 
{ 
    public partial class ArgumentoItem : _BaseItem, Interface.Requisicao.Argumento.IArgumentoItem
    { 
        #region Propriedades

        private Nemag.Database.DatabaseItem _databaseItem { get; set; }

        #endregion

        #region Construtores

        public ArgumentoItem() : this(new Nemag.Database.DatabaseItem())
        { }

        public ArgumentoItem(Nemag.Database.DatabaseItem databaseItem)
        {
	            _databaseItem = databaseItem;
        }

        #endregion

        #region Métodos Públicos 

        public List<Entidade.Requisicao.Argumento.ArgumentoItem> CarregarLista() 
        { 
            var sql = this.PrepararSelecaoSql(null, null, null); 

            return base.CarregarLista<Entidade.Requisicao.Argumento.ArgumentoItem>(_databaseItem, sql); 
        } 

        public List<Entidade.Requisicao.Argumento.ArgumentoItem> CarregarListaPorRegistroSituacaoId(int registroSituacaoId) 
        { 
            var sql = this.PrepararSelecaoSql(null, registroSituacaoId, null); 

            return base.CarregarLista<Entidade.Requisicao.Argumento.ArgumentoItem>(_databaseItem, sql); 
        } 

        public List<Entidade.Requisicao.Argumento.ArgumentoItem> CarregarListaPorRequisicaoId(int requisicaoId) 
        { 
            var sql = this.PrepararSelecaoSql(null, null, requisicaoId); 

            return base.CarregarLista<Entidade.Requisicao.Argumento.ArgumentoItem>(_databaseItem, sql); 
        } 

        public Entidade.Requisicao.Argumento.ArgumentoItem CarregarItem(int requisicaoArgumentoId)
        {
            var sql = this.PrepararSelecaoSql(requisicaoArgumentoId, null, null); 

            var retorno = base.CarregarItem<Entidade.Requisicao.Argumento.ArgumentoItem>(_databaseItem, sql); 

            return retorno; 
        }

        public Entidade.Requisicao.Argumento.ArgumentoItem InserirItem(Entidade.Requisicao.Argumento.ArgumentoItem argumentoItem)
        {
            var sql = this.PrepararInsercaoSql(argumentoItem); 

            sql += this.ObterUltimoItemInseridoSql();

            argumentoItem.Id = base.CarregarItem<Entidade.Requisicao.Argumento.ArgumentoItem>(_databaseItem, sql).Id;

            return argumentoItem;
        } 

        public Entidade.Requisicao.Argumento.ArgumentoItem AtualizarItem(Entidade.Requisicao.Argumento.ArgumentoItem argumentoItem)
        {
            var sql = this.PrepararAtualizacaoSql(argumentoItem); 

            sql += this.PrepararSelecaoSql(argumentoItem.Id, null, null);

            argumentoItem.DataAlteracao = base.CarregarItem<Entidade.Requisicao.Argumento.ArgumentoItem>(_databaseItem, sql).DataAlteracao;

            return argumentoItem;
        } 

        public Entidade.Requisicao.Argumento.ArgumentoItem ExcluirItem(Entidade.Requisicao.Argumento.ArgumentoItem argumentoItem)
        {
            var sql = this.PrepararExclusaoSql(argumentoItem); 

            return base.CarregarItem<Entidade.Requisicao.Argumento.ArgumentoItem>(_databaseItem, sql); 
        } 

        public Entidade.Requisicao.Argumento.ArgumentoItem InativarItem(Entidade.Requisicao.Argumento.ArgumentoItem argumentoItem)
        {
            var sql = this.PrepararInativacaoSql(argumentoItem); 

            return base.CarregarItem<Entidade.Requisicao.Argumento.ArgumentoItem>(_databaseItem, sql); 
        } 

        #endregion 

        #region Métodos Privados 

        private string PrepararSelecaoSql()
        { 
            var sql = ""; 

            sql += "SELECT \n";
            sql += "    A.REQUISICAO_ARGUMENTO_ID,\n";
            sql += "    A.DATA_INCLUSAO,\n";
            sql += "    A.DATA_ALTERACAO,\n";
            sql += "    A.REGISTRO_SITUACAO_ID,\n";
            sql += "    A.REQUISICAO_ID,\n";
            sql += "    A.NOME,\n";
            sql += "    A.VALOR,\n";
            sql += "    A4.PESSOA_ID AS REQUISICAO_LOGIN_ACESSO_LOGIN_PESSOA_ID,\n";
            sql += "    A4.NOME AS REQUISICAO_LOGIN_ACESSO_LOGIN_NOME,\n";
            sql += "    A5.LOGIN_SITUACAO_ID AS REQUISICAO_LOGIN_ACESSO_LOGIN_SITUACAO_ID,\n";
            sql += "    A5.NOME AS REQUISICAO_LOGIN_ACESSO_LOGIN_SITUACAO_NOME,\n";
            sql += "    A3.LOGIN_ID AS REQUISICAO_LOGIN_ACESSO_LOGIN_ID,\n";
            sql += "    A3.USUARIO AS REQUISICAO_LOGIN_ACESSO_LOGIN_USUARIO,\n";
            sql += "    A3.SENHA AS REQUISICAO_LOGIN_ACESSO_LOGIN_SENHA,\n";
            sql += "    A3.NOME_EXIBICAO AS REQUISICAO_LOGIN_ACESSO_LOGIN_NOME_EXIBICAO,\n";
            sql += "    A2.LOGIN_ACESSO_ID AS REQUISICAO_LOGIN_ACESSO_ID,\n";
            sql += "    A2.TOKEN AS REQUISICAO_LOGIN_ACESSO_TOKEN,\n";
            sql += "    A2.IP AS REQUISICAO_LOGIN_ACESSO_IP,\n";
            sql += "    A2.DATA_VALIDADE AS REQUISICAO_LOGIN_ACESSO_DATA_VALIDADE,\n";
            sql += "    A1.REQUISICAO_ID AS REQUISICAO_ID,\n";
            sql += "    A1.IP AS REQUISICAO_IP,\n";
            sql += "    A1.URL_REFERENCIA AS REQUISICAO_URL_REFERENCIA,\n";
            sql += "    A1.URL_ORIGEM AS REQUISICAO_URL_ORIGEM,\n";
            sql += "    A1.URL_DESTINO AS REQUISICAO_URL_DESTINO\n";
            sql += "FROM \n";
            sql += "    REQUISICAO_ARGUMENTO_TB A\n";
            sql += "    INNER JOIN REQUISICAO_TB A1 ON A1.REQUISICAO_ID = A.REQUISICAO_ID\n";
            sql += "    LEFT JOIN LOGIN_ACESSO_TB A2 ON A2.LOGIN_ACESSO_ID = A1.LOGIN_ACESSO_ID\n";
            sql += "    LEFT JOIN LOGIN_TB A3 ON A3.LOGIN_ID = A2.LOGIN_ID\n";
            sql += "    LEFT JOIN PESSOA_TB A4 ON A4.PESSOA_ID = A3.PESSOA_ID\n";
            sql += "    LEFT JOIN LOGIN_SITUACAO_TB A5 ON A5.LOGIN_SITUACAO_ID = A3.LOGIN_SITUACAO_ID\n";

            return sql; 
        } 

        private string PrepararSelecaoSql(int? requisicaoArgumentoId, int? registroSituacaoId, int? requisicaoId)
		{ 
			var sql = ""; 

			if (requisicaoArgumentoId.HasValue)
				sql += "A.REQUISICAO_ARGUMENTO_ID = " + requisicaoArgumentoId.Value + "\n";

			if (registroSituacaoId.HasValue)
				sql += "A.REGISTRO_SITUACAO_ID = " + registroSituacaoId.Value + "\n";

			if (requisicaoId.HasValue)
				sql += "A.REQUISICAO_ID = " + requisicaoId.Value + "\n";

			if (!requisicaoArgumentoId.HasValue)
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

        private string PrepararInsercaoSql(Entidade.Requisicao.Argumento.ArgumentoItem argumentoItem) 
        { 
            var sql = string.Empty; 

            sql += "INSERT INTO REQUISICAO_ARGUMENTO_TB(\n";
			sql += "    REQUISICAO_ID,\n";

			sql += "    NOME,\n";

			sql += "    VALOR,\n";

			sql = sql.Substring(0, sql.Length - 2) + "\n";

			sql += ") VALUES (\n";
			sql += "    " + argumentoItem.RequisicaoId.ToString() + ",\n";

			    sql += "    '" + argumentoItem.Nome.Replace("'", "''") + "',\n";

			if (string.IsNullOrEmpty(argumentoItem.Valor))
			    sql += "    NULL,\n";
			else
			    sql += "    '" + argumentoItem.Valor.Replace("'", "''") + "',\n";

			sql = sql.Substring(0, sql.Length - 2) + "\n";

            sql += ");\n";

            return sql; 
        } 

        private string PrepararAtualizacaoSql(Entidade.Requisicao.Argumento.ArgumentoItem argumentoItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    REQUISICAO_ARGUMENTO_TB \n";
            sql += "SET\n";
			sql += "    DATA_ALTERACAO = CURRENT_TIMESTAMP,\n";

			sql += "    REQUISICAO_ID = " + argumentoItem.RequisicaoId.ToString() + ",\n"; 

			sql += "    NOME = '" + argumentoItem.Nome.Replace("'", "''") + "',\n";

			if (string.IsNullOrEmpty(argumentoItem.Valor))
			    sql += "    VALOR = NULL,\n";
			else
				sql += "    VALOR = '" + argumentoItem.Valor.Replace("'", "''") + "',\n";

			sql = sql.Substring(0, sql.Length - 2) + "\n";

            sql += "WHERE\n";
            sql += "    REQUISICAO_ARGUMENTO_ID = " + argumentoItem.Id + "\n";
            return sql; 
        } 

        private string PrepararExclusaoSql(Entidade.Requisicao.Argumento.ArgumentoItem argumentoItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    REQUISICAO_ARGUMENTO_TB\n";
            sql += "SET\n";
            sql += "    REGISTRO_SITUACAO_ID = 3\n";
            sql += "WHERE\n";
            sql += "    REQUISICAO_ARGUMENTO_ID = " + argumentoItem.Id + "\n";
            return sql; 
        } 

        private string PrepararInativacaoSql(Entidade.Requisicao.Argumento.ArgumentoItem argumentoItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    REQUISICAO_ARGUMENTO_TB\n";
            sql += "SET\n";
            sql += "    REGISTRO_SITUACAO_ID = 2\n";
            sql += "WHERE\n";
            sql += "    REQUISICAO_ARGUMENTO_ID = " + argumentoItem.Id + "\n";
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
					sql += "    A.REQUISICAO_ARGUMENTO_ID = SCOPE_IDENTITY()\n";

					break;

				case Nemag.Database.Base.DATABASE_TIPO_ID.MYSQL:
					sql += "    A.REQUISICAO_ARGUMENTO_ID = LAST_INSERT_ID()\n";

					break;
			}

			return sql;
		}

		#endregion
    }
}
