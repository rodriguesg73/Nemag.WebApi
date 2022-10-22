using System;
using System.Collections.Generic;

namespace Nemag.Core.Persistencia.Login.Notificacao.Tipo 
{ 
    public partial class TipoItem : _BaseItem, Interface.Login.Notificacao.Tipo.ITipoItem
    { 
        #region Propriedades

        private Nemag.Database.DatabaseItem _databaseItem { get; set; }

        #endregion

        #region Construtores

        public TipoItem() : this(new Nemag.Database.DatabaseItem())
        { }

        public TipoItem(Nemag.Database.DatabaseItem databaseItem)
        {
	            _databaseItem = databaseItem;
        }

        #endregion

        #region Métodos Públicos 

        public List<Entidade.Login.Notificacao.Tipo.TipoItem> CarregarLista() 
        { 
            var sql = this.PrepararSelecaoSql(null, null, null); 

            return base.CarregarLista<Entidade.Login.Notificacao.Tipo.TipoItem>(_databaseItem, sql); 
        } 

        public List<Entidade.Login.Notificacao.Tipo.TipoItem> CarregarListaPorRegistroSituacaoId(int registroSituacaoId) 
        { 
            var sql = this.PrepararSelecaoSql(null, registroSituacaoId, null); 

            return base.CarregarLista<Entidade.Login.Notificacao.Tipo.TipoItem>(_databaseItem, sql); 
        } 

        public List<Entidade.Login.Notificacao.Tipo.TipoItem> CarregarListaPorRegistroLoginId(int registroLoginId) 
        { 
            var sql = this.PrepararSelecaoSql(null, null, registroLoginId); 

            return base.CarregarLista<Entidade.Login.Notificacao.Tipo.TipoItem>(_databaseItem, sql); 
        } 

        public Entidade.Login.Notificacao.Tipo.TipoItem CarregarItem(int loginNotificacaoTipoId)
        {
            var sql = this.PrepararSelecaoSql(loginNotificacaoTipoId, null, null); 

            var retorno = base.CarregarItem<Entidade.Login.Notificacao.Tipo.TipoItem>(_databaseItem, sql); 

            return retorno; 
        }

        public Entidade.Login.Notificacao.Tipo.TipoItem InserirItem(Entidade.Login.Notificacao.Tipo.TipoItem tipoItem)
        {
            var sql = this.PrepararInsercaoSql(tipoItem); 

            sql += this.ObterUltimoItemInseridoSql();

            tipoItem.Id = base.CarregarItem<Entidade.Login.Notificacao.Tipo.TipoItem>(_databaseItem, sql).Id;

            return tipoItem;
        } 

        public Entidade.Login.Notificacao.Tipo.TipoItem AtualizarItem(Entidade.Login.Notificacao.Tipo.TipoItem tipoItem)
        {
            var sql = this.PrepararAtualizacaoSql(tipoItem); 

            sql += this.PrepararSelecaoSql(tipoItem.Id, null, null);

            tipoItem.DataAlteracao = base.CarregarItem<Entidade.Login.Notificacao.Tipo.TipoItem>(_databaseItem, sql).DataAlteracao;

            return tipoItem;
        } 

        public Entidade.Login.Notificacao.Tipo.TipoItem ExcluirItem(Entidade.Login.Notificacao.Tipo.TipoItem tipoItem)
        {
            var sql = this.PrepararExclusaoSql(tipoItem); 

            return base.CarregarItem<Entidade.Login.Notificacao.Tipo.TipoItem>(_databaseItem, sql); 
        } 

        public Entidade.Login.Notificacao.Tipo.TipoItem InativarItem(Entidade.Login.Notificacao.Tipo.TipoItem tipoItem)
        {
            var sql = this.PrepararInativacaoSql(tipoItem); 

            return base.CarregarItem<Entidade.Login.Notificacao.Tipo.TipoItem>(_databaseItem, sql); 
        } 

        #endregion 

        #region Métodos Privados 

        private string PrepararSelecaoSql()
        { 
            var sql = ""; 

            sql += "SELECT \n";
            sql += "    A.LOGIN_NOTIFICACAO_TIPO_ID,\n";
            sql += "    A.DATA_INCLUSAO,\n";
            sql += "    A.DATA_ALTERACAO,\n";
            sql += "    A.REGISTRO_SITUACAO_ID,\n";
            sql += "    A.REGISTRO_LOGIN_ID,\n";
            sql += "    A.NOME,\n";
            sql += "    A.ICONE_CSS\n";
            sql += "FROM \n";
            sql += "    LOGIN_NOTIFICACAO_TIPO_TB A\n";

            return sql; 
        } 

        private string PrepararSelecaoSql(int? loginNotificacaoTipoId, int? registroSituacaoId, int? registroLoginId)
		{ 
			var sql = ""; 

			if (loginNotificacaoTipoId.HasValue)
				sql += "A.LOGIN_NOTIFICACAO_TIPO_ID = " + loginNotificacaoTipoId.Value + "\n";

			if (registroSituacaoId.HasValue)
				sql += "A.REGISTRO_SITUACAO_ID = " + registroSituacaoId.Value + "\n";

			if (registroLoginId.HasValue)
				sql += "A.REGISTRO_LOGIN_ID = " + registroLoginId.Value + "\n";

			if (!loginNotificacaoTipoId.HasValue)
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

        private string PrepararInsercaoSql(Entidade.Login.Notificacao.Tipo.TipoItem tipoItem) 
        { 
            var sql = string.Empty; 

            sql += "INSERT INTO LOGIN_NOTIFICACAO_TIPO_TB(\n";
			sql += "    REGISTRO_LOGIN_ID,\n";

			sql += "    NOME,\n";

			sql += "    ICONE_CSS,\n";

			sql = sql.Substring(0, sql.Length - 2) + "\n";

			sql += ") VALUES (\n";
			sql += "    " + tipoItem.RegistroLoginId.ToString() + ",\n";

			    sql += "    '" + tipoItem.Nome.Replace("'", "''") + "',\n";

			if (string.IsNullOrEmpty(tipoItem.IconeCss))
			    sql += "    NULL,\n";
			else
			    sql += "    '" + tipoItem.IconeCss.Replace("'", "''") + "',\n";

			sql = sql.Substring(0, sql.Length - 2) + "\n";

            sql += ");\n";

            return sql; 
        } 

        private string PrepararAtualizacaoSql(Entidade.Login.Notificacao.Tipo.TipoItem tipoItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    LOGIN_NOTIFICACAO_TIPO_TB \n";
            sql += "SET\n";
			sql += "    DATA_ALTERACAO = CURRENT_TIMESTAMP,\n";

			sql += "    REGISTRO_LOGIN_ID = " + tipoItem.RegistroLoginId.ToString() + ",\n"; 

			sql += "    NOME = '" + tipoItem.Nome.Replace("'", "''") + "',\n";

			if (string.IsNullOrEmpty(tipoItem.IconeCss))
			    sql += "    ICONE_CSS = NULL,\n";
			else
				sql += "    ICONE_CSS = '" + tipoItem.IconeCss.Replace("'", "''") + "',\n";

			sql = sql.Substring(0, sql.Length - 2) + "\n";

            sql += "WHERE\n";
            sql += "    LOGIN_NOTIFICACAO_TIPO_ID = " + tipoItem.Id + "\n";
            return sql; 
        } 

        private string PrepararExclusaoSql(Entidade.Login.Notificacao.Tipo.TipoItem tipoItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    LOGIN_NOTIFICACAO_TIPO_TB\n";
            sql += "SET\n";
            sql += "    REGISTRO_SITUACAO_ID = 3\n";
            sql += "WHERE\n";
            sql += "    LOGIN_NOTIFICACAO_TIPO_ID = " + tipoItem.Id + "\n";
            return sql; 
        } 

        private string PrepararInativacaoSql(Entidade.Login.Notificacao.Tipo.TipoItem tipoItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    LOGIN_NOTIFICACAO_TIPO_TB\n";
            sql += "SET\n";
            sql += "    REGISTRO_SITUACAO_ID = 2\n";
            sql += "WHERE\n";
            sql += "    LOGIN_NOTIFICACAO_TIPO_ID = " + tipoItem.Id + "\n";
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
					sql += "    A.LOGIN_NOTIFICACAO_TIPO_ID = SCOPE_IDENTITY()\n";

					break;

				case Nemag.Database.Base.DATABASE_TIPO_ID.MYSQL:
					sql += "    A.LOGIN_NOTIFICACAO_TIPO_ID = LAST_INSERT_ID()\n";

					break;
			}

			return sql;
		}

		#endregion
    }
}
