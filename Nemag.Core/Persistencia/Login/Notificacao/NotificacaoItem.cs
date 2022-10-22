using System;
using System.Collections.Generic;

namespace Nemag.Core.Persistencia.Login.Notificacao 
{ 
    public partial class NotificacaoItem : _BaseItem, Interface.Login.Notificacao.INotificacaoItem
    { 
        #region Propriedades

        private Nemag.Database.DatabaseItem _databaseItem { get; set; }

        #endregion

        #region Construtores

        public NotificacaoItem() : this(new Nemag.Database.DatabaseItem())
        { }

        public NotificacaoItem(Nemag.Database.DatabaseItem databaseItem)
        {
	            _databaseItem = databaseItem;
        }

        #endregion

        #region Métodos Públicos 

        public List<Entidade.Login.Notificacao.NotificacaoItem> CarregarLista() 
        { 
            var sql = this.PrepararSelecaoSql(null, null, null, null, null); 

            return base.CarregarLista<Entidade.Login.Notificacao.NotificacaoItem>(_databaseItem, sql); 
        } 

        public List<Entidade.Login.Notificacao.NotificacaoItem> CarregarListaPorRegistroSituacaoId(int registroSituacaoId) 
        { 
            var sql = this.PrepararSelecaoSql(null, registroSituacaoId, null, null, null); 

            return base.CarregarLista<Entidade.Login.Notificacao.NotificacaoItem>(_databaseItem, sql); 
        } 

        public List<Entidade.Login.Notificacao.NotificacaoItem> CarregarListaPorRegistroLoginId(int registroLoginId) 
        { 
            var sql = this.PrepararSelecaoSql(null, null, registroLoginId, null, null); 

            return base.CarregarLista<Entidade.Login.Notificacao.NotificacaoItem>(_databaseItem, sql); 
        } 

        public List<Entidade.Login.Notificacao.NotificacaoItem> CarregarListaPorLoginId(int loginId) 
        { 
            var sql = this.PrepararSelecaoSql(null, null, null, loginId, null); 

            return base.CarregarLista<Entidade.Login.Notificacao.NotificacaoItem>(_databaseItem, sql); 
        } 

        public List<Entidade.Login.Notificacao.NotificacaoItem> CarregarListaPorLoginNotificacaoTipoId(int loginNotificacaoTipoId) 
        { 
            var sql = this.PrepararSelecaoSql(null, null, null, null, loginNotificacaoTipoId); 

            return base.CarregarLista<Entidade.Login.Notificacao.NotificacaoItem>(_databaseItem, sql); 
        } 

        public Entidade.Login.Notificacao.NotificacaoItem CarregarItem(int loginNotificacaoId)
        {
            var sql = this.PrepararSelecaoSql(loginNotificacaoId, null, null, null, null); 

            var retorno = base.CarregarItem<Entidade.Login.Notificacao.NotificacaoItem>(_databaseItem, sql); 

            return retorno; 
        }

        public Entidade.Login.Notificacao.NotificacaoItem InserirItem(Entidade.Login.Notificacao.NotificacaoItem notificacaoItem)
        {
            var sql = this.PrepararInsercaoSql(notificacaoItem); 

            sql += this.ObterUltimoItemInseridoSql();

            notificacaoItem.Id = base.CarregarItem<Entidade.Login.Notificacao.NotificacaoItem>(_databaseItem, sql).Id;

            return notificacaoItem;
        } 

        public Entidade.Login.Notificacao.NotificacaoItem AtualizarItem(Entidade.Login.Notificacao.NotificacaoItem notificacaoItem)
        {
            var sql = this.PrepararAtualizacaoSql(notificacaoItem); 

            sql += this.PrepararSelecaoSql(notificacaoItem.Id, null, null, null, null);

            notificacaoItem.DataAlteracao = base.CarregarItem<Entidade.Login.Notificacao.NotificacaoItem>(_databaseItem, sql).DataAlteracao;

            return notificacaoItem;
        } 

        public Entidade.Login.Notificacao.NotificacaoItem ExcluirItem(Entidade.Login.Notificacao.NotificacaoItem notificacaoItem)
        {
            var sql = this.PrepararExclusaoSql(notificacaoItem); 

            return base.CarregarItem<Entidade.Login.Notificacao.NotificacaoItem>(_databaseItem, sql); 
        } 

        public Entidade.Login.Notificacao.NotificacaoItem InativarItem(Entidade.Login.Notificacao.NotificacaoItem notificacaoItem)
        {
            var sql = this.PrepararInativacaoSql(notificacaoItem); 

            return base.CarregarItem<Entidade.Login.Notificacao.NotificacaoItem>(_databaseItem, sql); 
        } 

        #endregion 

        #region Métodos Privados 

        private string PrepararSelecaoSql()
        { 
            var sql = ""; 

            sql += "SELECT \n";
            sql += "    A.LOGIN_NOTIFICACAO_ID,\n";
            sql += "    A.DATA_INCLUSAO,\n";
            sql += "    A.DATA_ALTERACAO,\n";
            sql += "    A.REGISTRO_SITUACAO_ID,\n";
            sql += "    A.REGISTRO_LOGIN_ID,\n";
            sql += "    A.LOGIN_ID,\n";
            sql += "    A.LOGIN_NOTIFICACAO_TIPO_ID,\n";
            sql += "    A.TITULO,\n";
            sql += "    A.CONTEUDO,\n";
            sql += "    A.LINK_URL,\n";
            sql += "    A3.PESSOA_ID AS LOGIN_PESSOA_ID,\n";
            sql += "    A3.NOME AS LOGIN_NOME,\n";
            sql += "    A4.LOGIN_SITUACAO_ID AS LOGIN_SITUACAO_ID,\n";
            sql += "    A4.NOME AS LOGIN_SITUACAO_NOME,\n";
            sql += "    A2.LOGIN_ID AS LOGIN_ID,\n";
            sql += "    A2.USUARIO AS LOGIN_USUARIO,\n";
            sql += "    A2.SENHA AS LOGIN_SENHA,\n";
            sql += "    A2.NOME_EXIBICAO AS LOGIN_NOME_EXIBICAO,\n";
            sql += "    A1.LOGIN_NOTIFICACAO_TIPO_ID AS LOGIN_NOTIFICACAO_TIPO_ID,\n";
            sql += "    A1.NOME AS LOGIN_NOTIFICACAO_TIPO_NOME,\n";
            sql += "    A1.ICONE_CSS AS LOGIN_NOTIFICACAO_TIPO_ICONE_CSS\n";
            sql += "FROM \n";
            sql += "    LOGIN_NOTIFICACAO_TB A\n";
            sql += "    INNER JOIN LOGIN_TB A2 ON A2.LOGIN_ID = A.LOGIN_ID\n";
            sql += "    INNER JOIN PESSOA_TB A3 ON A3.PESSOA_ID = A2.PESSOA_ID\n";
            sql += "    INNER JOIN LOGIN_SITUACAO_TB A4 ON A4.LOGIN_SITUACAO_ID = A2.LOGIN_SITUACAO_ID\n";
            sql += "    INNER JOIN LOGIN_NOTIFICACAO_TIPO_TB A1 ON A1.LOGIN_NOTIFICACAO_TIPO_ID = A.LOGIN_NOTIFICACAO_TIPO_ID\n";

            return sql; 
        } 

        private string PrepararSelecaoSql(int? loginNotificacaoId, int? registroSituacaoId, int? registroLoginId, int? loginId, int? loginNotificacaoTipoId)
		{ 
			var sql = ""; 

			if (loginNotificacaoId.HasValue)
				sql += "A.LOGIN_NOTIFICACAO_ID = " + loginNotificacaoId.Value + "\n";

			if (registroSituacaoId.HasValue)
				sql += "A.REGISTRO_SITUACAO_ID = " + registroSituacaoId.Value + "\n";

			if (registroLoginId.HasValue)
				sql += "A.REGISTRO_LOGIN_ID = " + registroLoginId.Value + "\n";

			if (loginId.HasValue)
				sql += "A.LOGIN_ID = " + loginId.Value + "\n";

			if (loginNotificacaoTipoId.HasValue)
				sql += "A.LOGIN_NOTIFICACAO_TIPO_ID = " + loginNotificacaoTipoId.Value + "\n";

			if (!loginNotificacaoId.HasValue)
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

        private string PrepararInsercaoSql(Entidade.Login.Notificacao.NotificacaoItem notificacaoItem) 
        { 
            var sql = string.Empty; 

            sql += "INSERT INTO LOGIN_NOTIFICACAO_TB(\n";
			sql += "    REGISTRO_LOGIN_ID,\n";

			sql += "    LOGIN_ID,\n";

			sql += "    LOGIN_NOTIFICACAO_TIPO_ID,\n";

			sql += "    TITULO,\n";

			sql += "    CONTEUDO,\n";

			sql += "    LINK_URL,\n";

			sql = sql.Substring(0, sql.Length - 2) + "\n";

			sql += ") VALUES (\n";
			sql += "    " + notificacaoItem.RegistroLoginId.ToString() + ",\n";

			sql += "    " + notificacaoItem.LoginId.ToString() + ",\n";

			sql += "    " + notificacaoItem.LoginNotificacaoTipoId.ToString() + ",\n";

			    sql += "    '" + notificacaoItem.Titulo.Replace("'", "''") + "',\n";

			if (string.IsNullOrEmpty(notificacaoItem.Conteudo))
			    sql += "    NULL,\n";
			else
			    sql += "    '" + notificacaoItem.Conteudo.Replace("'", "''") + "',\n";

			if (string.IsNullOrEmpty(notificacaoItem.LinkUrl))
			    sql += "    NULL,\n";
			else
			    sql += "    '" + notificacaoItem.LinkUrl.Replace("'", "''") + "',\n";

			sql = sql.Substring(0, sql.Length - 2) + "\n";

            sql += ");\n";

            return sql; 
        } 

        private string PrepararAtualizacaoSql(Entidade.Login.Notificacao.NotificacaoItem notificacaoItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    LOGIN_NOTIFICACAO_TB \n";
            sql += "SET\n";
			sql += "    DATA_ALTERACAO = CURRENT_TIMESTAMP,\n";

			sql += "    REGISTRO_LOGIN_ID = " + notificacaoItem.RegistroLoginId.ToString() + ",\n"; 

			sql += "    LOGIN_ID = " + notificacaoItem.LoginId.ToString() + ",\n"; 

			sql += "    LOGIN_NOTIFICACAO_TIPO_ID = " + notificacaoItem.LoginNotificacaoTipoId.ToString() + ",\n"; 

			sql += "    TITULO = '" + notificacaoItem.Titulo.Replace("'", "''") + "',\n";

			if (string.IsNullOrEmpty(notificacaoItem.Conteudo))
			    sql += "    CONTEUDO = NULL,\n";
			else
				sql += "    CONTEUDO = '" + notificacaoItem.Conteudo.Replace("'", "''") + "',\n";

			if (string.IsNullOrEmpty(notificacaoItem.LinkUrl))
			    sql += "    LINK_URL = NULL,\n";
			else
				sql += "    LINK_URL = '" + notificacaoItem.LinkUrl.Replace("'", "''") + "',\n";

			sql = sql.Substring(0, sql.Length - 2) + "\n";

            sql += "WHERE\n";
            sql += "    LOGIN_NOTIFICACAO_ID = " + notificacaoItem.Id + "\n";
            return sql; 
        } 

        private string PrepararExclusaoSql(Entidade.Login.Notificacao.NotificacaoItem notificacaoItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    LOGIN_NOTIFICACAO_TB\n";
            sql += "SET\n";
            sql += "    REGISTRO_SITUACAO_ID = 3\n";
            sql += "WHERE\n";
            sql += "    LOGIN_NOTIFICACAO_ID = " + notificacaoItem.Id + "\n";
            return sql; 
        } 

        private string PrepararInativacaoSql(Entidade.Login.Notificacao.NotificacaoItem notificacaoItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    LOGIN_NOTIFICACAO_TB\n";
            sql += "SET\n";
            sql += "    REGISTRO_SITUACAO_ID = 2\n";
            sql += "WHERE\n";
            sql += "    LOGIN_NOTIFICACAO_ID = " + notificacaoItem.Id + "\n";
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
					sql += "    A.LOGIN_NOTIFICACAO_ID = SCOPE_IDENTITY()\n";

					break;

				case Nemag.Database.Base.DATABASE_TIPO_ID.MYSQL:
					sql += "    A.LOGIN_NOTIFICACAO_ID = LAST_INSERT_ID()\n";

					break;
			}

			return sql;
		}

		#endregion
    }
}
