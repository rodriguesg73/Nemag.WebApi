using System;
using System.Collections.Generic;

namespace Nemag.Core.Persistencia.Requisicao 
{ 
    public partial class RequisicaoItem : _BaseItem, Interface.Requisicao.IRequisicaoItem
    { 
        #region Propriedades

        private Nemag.Database.DatabaseItem _databaseItem { get; set; }

        #endregion

        #region Construtores

        public RequisicaoItem() : this(new Nemag.Database.DatabaseItem())
        { }

        public RequisicaoItem(Nemag.Database.DatabaseItem databaseItem)
        {
	            _databaseItem = databaseItem;
        }

        #endregion

        #region Métodos Públicos 

        public List<Entidade.Requisicao.RequisicaoItem> CarregarLista() 
        { 
            var sql = this.PrepararSelecaoSql(null, null, null); 

            return base.CarregarLista<Entidade.Requisicao.RequisicaoItem>(_databaseItem, sql); 
        } 

        public List<Entidade.Requisicao.RequisicaoItem> CarregarListaPorRegistroSituacaoId(int registroSituacaoId) 
        { 
            var sql = this.PrepararSelecaoSql(null, registroSituacaoId, null); 

            return base.CarregarLista<Entidade.Requisicao.RequisicaoItem>(_databaseItem, sql); 
        } 

        public List<Entidade.Requisicao.RequisicaoItem> CarregarListaPorLoginAcessoId(int loginAcessoId) 
        { 
            var sql = this.PrepararSelecaoSql(null, null, loginAcessoId); 

            return base.CarregarLista<Entidade.Requisicao.RequisicaoItem>(_databaseItem, sql); 
        } 

        public Entidade.Requisicao.RequisicaoItem CarregarItem(int requisicaoId)
        {
            var sql = this.PrepararSelecaoSql(requisicaoId, null, null); 

            var retorno = base.CarregarItem<Entidade.Requisicao.RequisicaoItem>(_databaseItem, sql); 

            return retorno; 
        }

        public Entidade.Requisicao.RequisicaoItem InserirItem(Entidade.Requisicao.RequisicaoItem requisicaoItem)
        {
            var sql = this.PrepararInsercaoSql(requisicaoItem); 

            sql += this.ObterUltimoItemInseridoSql();

            requisicaoItem.Id = base.CarregarItem<Entidade.Requisicao.RequisicaoItem>(_databaseItem, sql).Id;

            return requisicaoItem;
        } 

        public Entidade.Requisicao.RequisicaoItem AtualizarItem(Entidade.Requisicao.RequisicaoItem requisicaoItem)
        {
            var sql = this.PrepararAtualizacaoSql(requisicaoItem); 

            sql += this.PrepararSelecaoSql(requisicaoItem.Id, null, null);

            requisicaoItem.DataAlteracao = base.CarregarItem<Entidade.Requisicao.RequisicaoItem>(_databaseItem, sql).DataAlteracao;

            return requisicaoItem;
        } 

        public Entidade.Requisicao.RequisicaoItem ExcluirItem(Entidade.Requisicao.RequisicaoItem requisicaoItem)
        {
            var sql = this.PrepararExclusaoSql(requisicaoItem); 

            return base.CarregarItem<Entidade.Requisicao.RequisicaoItem>(_databaseItem, sql); 
        } 

        public Entidade.Requisicao.RequisicaoItem InativarItem(Entidade.Requisicao.RequisicaoItem requisicaoItem)
        {
            var sql = this.PrepararInativacaoSql(requisicaoItem); 

            return base.CarregarItem<Entidade.Requisicao.RequisicaoItem>(_databaseItem, sql); 
        } 

        #endregion 

        #region Métodos Privados 

        private string PrepararSelecaoSql()
        { 
            var sql = ""; 

            sql += "SELECT \n";
            sql += "    A.REQUISICAO_ID,\n";
            sql += "    A.DATA_INCLUSAO,\n";
            sql += "    A.DATA_ALTERACAO,\n";
            sql += "    A.REGISTRO_SITUACAO_ID,\n";
            sql += "    A.LOGIN_ACESSO_ID,\n";
            sql += "    A.IP,\n";
            sql += "    A.URL_REFERENCIA,\n";
            sql += "    A.URL_ORIGEM,\n";
            sql += "    A.URL_DESTINO,\n";
            sql += "    A3.PESSOA_ID AS LOGIN_ACESSO_LOGIN_PESSOA_ID,\n";
            sql += "    A3.NOME AS LOGIN_ACESSO_LOGIN_NOME,\n";
            sql += "    A4.LOGIN_SITUACAO_ID AS LOGIN_ACESSO_LOGIN_SITUACAO_ID,\n";
            sql += "    A4.NOME AS LOGIN_ACESSO_LOGIN_SITUACAO_NOME,\n";
            sql += "    A2.LOGIN_ID AS LOGIN_ACESSO_LOGIN_ID,\n";
            sql += "    A2.USUARIO AS LOGIN_ACESSO_LOGIN_USUARIO,\n";
            sql += "    A2.SENHA AS LOGIN_ACESSO_LOGIN_SENHA,\n";
            sql += "    A2.NOME_EXIBICAO AS LOGIN_ACESSO_LOGIN_NOME_EXIBICAO,\n";
            sql += "    A1.LOGIN_ACESSO_ID AS LOGIN_ACESSO_ID,\n";
            sql += "    A1.TOKEN AS LOGIN_ACESSO_TOKEN,\n";
            sql += "    A1.IP AS LOGIN_ACESSO_IP,\n";
            sql += "    A1.DATA_VALIDADE AS LOGIN_ACESSO_DATA_VALIDADE\n";
            sql += "FROM \n";
            sql += "    REQUISICAO_TB A\n";
            sql += "    LEFT JOIN LOGIN_ACESSO_TB A1 ON A1.LOGIN_ACESSO_ID = A.LOGIN_ACESSO_ID\n";
            sql += "    LEFT JOIN LOGIN_TB A2 ON A2.LOGIN_ID = A1.LOGIN_ID\n";
            sql += "    LEFT JOIN PESSOA_TB A3 ON A3.PESSOA_ID = A2.PESSOA_ID\n";
            sql += "    LEFT JOIN LOGIN_SITUACAO_TB A4 ON A4.LOGIN_SITUACAO_ID = A2.LOGIN_SITUACAO_ID\n";

            return sql; 
        } 

        private string PrepararSelecaoSql(int? requisicaoId, int? registroSituacaoId, int? loginAcessoId)
		{ 
			var sql = ""; 

			if (requisicaoId.HasValue)
				sql += "A.REQUISICAO_ID = " + requisicaoId.Value + "\n";

			if (registroSituacaoId.HasValue)
				sql += "A.REGISTRO_SITUACAO_ID = " + registroSituacaoId.Value + "\n";

			if (loginAcessoId.HasValue)
				sql += "A.LOGIN_ACESSO_ID = " + loginAcessoId.Value + "\n";

			if (!requisicaoId.HasValue)
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

        private string PrepararInsercaoSql(Entidade.Requisicao.RequisicaoItem requisicaoItem) 
        { 
            var sql = string.Empty; 

            sql += "INSERT INTO REQUISICAO_TB(\n";
			sql += "    LOGIN_ACESSO_ID,\n";

			sql += "    IP,\n";

			sql += "    URL_REFERENCIA,\n";

			sql += "    URL_ORIGEM,\n";

			sql += "    URL_DESTINO,\n";

			sql = sql.Substring(0, sql.Length - 2) + "\n";

			sql += ") VALUES (\n";
			sql += "    " + (!requisicaoItem.LoginAcessoId.Equals(0) ? requisicaoItem.LoginAcessoId.ToString() : "NULL") + ",\n";

			    sql += "    '" + requisicaoItem.Ip.Replace("'", "''") + "',\n";

			if (string.IsNullOrEmpty(requisicaoItem.UrlReferencia))
			    sql += "    NULL,\n";
			else
			    sql += "    '" + requisicaoItem.UrlReferencia.Replace("'", "''") + "',\n";

			if (string.IsNullOrEmpty(requisicaoItem.UrlOrigem))
			    sql += "    NULL,\n";
			else
			    sql += "    '" + requisicaoItem.UrlOrigem.Replace("'", "''") + "',\n";

			    sql += "    '" + requisicaoItem.UrlDestino.Replace("'", "''") + "',\n";

			sql = sql.Substring(0, sql.Length - 2) + "\n";

            sql += ");\n";

            return sql; 
        } 

        private string PrepararAtualizacaoSql(Entidade.Requisicao.RequisicaoItem requisicaoItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    REQUISICAO_TB \n";
            sql += "SET\n";
			sql += "    DATA_ALTERACAO = CURRENT_TIMESTAMP,\n";

			sql += "    LOGIN_ACESSO_ID = " + (!requisicaoItem.LoginAcessoId.Equals(0) ? requisicaoItem.LoginAcessoId.ToString() : "NULL") + ",\n"; 

			sql += "    IP = '" + requisicaoItem.Ip.Replace("'", "''") + "',\n";

			if (string.IsNullOrEmpty(requisicaoItem.UrlReferencia))
			    sql += "    URL_REFERENCIA = NULL,\n";
			else
				sql += "    URL_REFERENCIA = '" + requisicaoItem.UrlReferencia.Replace("'", "''") + "',\n";

			if (string.IsNullOrEmpty(requisicaoItem.UrlOrigem))
			    sql += "    URL_ORIGEM = NULL,\n";
			else
				sql += "    URL_ORIGEM = '" + requisicaoItem.UrlOrigem.Replace("'", "''") + "',\n";

			sql += "    URL_DESTINO = '" + requisicaoItem.UrlDestino.Replace("'", "''") + "',\n";

			sql = sql.Substring(0, sql.Length - 2) + "\n";

            sql += "WHERE\n";
            sql += "    REQUISICAO_ID = " + requisicaoItem.Id + "\n";
            return sql; 
        } 

        private string PrepararExclusaoSql(Entidade.Requisicao.RequisicaoItem requisicaoItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    REQUISICAO_TB\n";
            sql += "SET\n";
            sql += "    REGISTRO_SITUACAO_ID = 3\n";
            sql += "WHERE\n";
            sql += "    REQUISICAO_ID = " + requisicaoItem.Id + "\n";
            return sql; 
        } 

        private string PrepararInativacaoSql(Entidade.Requisicao.RequisicaoItem requisicaoItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    REQUISICAO_TB\n";
            sql += "SET\n";
            sql += "    REGISTRO_SITUACAO_ID = 2\n";
            sql += "WHERE\n";
            sql += "    REQUISICAO_ID = " + requisicaoItem.Id + "\n";
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
					sql += "    A.REQUISICAO_ID = SCOPE_IDENTITY()\n";

					break;

				case Nemag.Database.Base.DATABASE_TIPO_ID.MYSQL:
					sql += "    A.REQUISICAO_ID = LAST_INSERT_ID()\n";

					break;
			}

			return sql;
		}

		#endregion
    }
}
