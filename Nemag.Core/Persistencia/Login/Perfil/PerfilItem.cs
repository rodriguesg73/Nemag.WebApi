using System;
using System.Collections.Generic;

namespace Nemag.Core.Persistencia.Login.Perfil 
{ 
    public partial class PerfilItem : _BaseItem, Interface.Login.Perfil.IPerfilItem
    { 
        #region Propriedades

        private Nemag.Database.DatabaseItem _databaseItem { get; set; }

        #endregion

        #region Construtores

        public PerfilItem() : this(new Nemag.Database.DatabaseItem())
        { }

        public PerfilItem(Nemag.Database.DatabaseItem databaseItem)
        {
	            _databaseItem = databaseItem;
        }

        #endregion

        #region Métodos Públicos 

        public List<Entidade.Login.Perfil.PerfilItem> CarregarLista() 
        { 
            var sql = this.PrepararSelecaoSql(null, null, null); 

            return base.CarregarLista<Entidade.Login.Perfil.PerfilItem>(_databaseItem, sql); 
        } 

        public List<Entidade.Login.Perfil.PerfilItem> CarregarListaPorRegistroSituacaoId(int registroSituacaoId) 
        { 
            var sql = this.PrepararSelecaoSql(null, registroSituacaoId, null); 

            return base.CarregarLista<Entidade.Login.Perfil.PerfilItem>(_databaseItem, sql); 
        } 

        public List<Entidade.Login.Perfil.PerfilItem> CarregarListaPorRegistroLoginId(int registroLoginId) 
        { 
            var sql = this.PrepararSelecaoSql(null, null, registroLoginId); 

            return base.CarregarLista<Entidade.Login.Perfil.PerfilItem>(_databaseItem, sql); 
        } 

        public Entidade.Login.Perfil.PerfilItem CarregarItem(int loginPerfilId)
        {
            var sql = this.PrepararSelecaoSql(loginPerfilId, null, null); 

            var retorno = base.CarregarItem<Entidade.Login.Perfil.PerfilItem>(_databaseItem, sql); 

            return retorno; 
        }

        public Entidade.Login.Perfil.PerfilItem InserirItem(Entidade.Login.Perfil.PerfilItem perfilItem)
        {
            var sql = this.PrepararInsercaoSql(perfilItem); 

            sql += this.ObterUltimoItemInseridoSql();

            perfilItem.Id = base.CarregarItem<Entidade.Login.Perfil.PerfilItem>(_databaseItem, sql).Id;

            return perfilItem;
        } 

        public Entidade.Login.Perfil.PerfilItem AtualizarItem(Entidade.Login.Perfil.PerfilItem perfilItem)
        {
            var sql = this.PrepararAtualizacaoSql(perfilItem); 

            sql += this.PrepararSelecaoSql(perfilItem.Id, null, null);

            perfilItem.DataAlteracao = base.CarregarItem<Entidade.Login.Perfil.PerfilItem>(_databaseItem, sql).DataAlteracao;

            return perfilItem;
        } 

        public Entidade.Login.Perfil.PerfilItem ExcluirItem(Entidade.Login.Perfil.PerfilItem perfilItem)
        {
            var sql = this.PrepararExclusaoSql(perfilItem); 

            return base.CarregarItem<Entidade.Login.Perfil.PerfilItem>(_databaseItem, sql); 
        } 

        public Entidade.Login.Perfil.PerfilItem InativarItem(Entidade.Login.Perfil.PerfilItem perfilItem)
        {
            var sql = this.PrepararInativacaoSql(perfilItem); 

            return base.CarregarItem<Entidade.Login.Perfil.PerfilItem>(_databaseItem, sql); 
        } 

        #endregion 

        #region Métodos Privados 

        private string PrepararSelecaoSql()
        { 
            var sql = ""; 

            sql += "SELECT \n";
            sql += "    A.LOGIN_PERFIL_ID,\n";
            sql += "    A.REGISTRO_SITUACAO_ID,\n";
            sql += "    A.REGISTRO_LOGIN_ID,\n";
            sql += "    A.NOME,\n";
            sql += "    A.DESCRICAO,\n";
            sql += "    A.DATA_INCLUSAO,\n";
            sql += "    A.DATA_ALTERACAO\n";
            sql += "FROM \n";
            sql += "    LOGIN_PERFIL_TB A\n";

            return sql; 
        } 

        private string PrepararSelecaoSql(int? loginPerfilId, int? registroSituacaoId, int? registroLoginId)
		{ 
			var sql = ""; 

			if (loginPerfilId.HasValue)
				sql += "A.LOGIN_PERFIL_ID = " + loginPerfilId.Value + "\n";

			if (registroSituacaoId.HasValue)
				sql += "A.REGISTRO_SITUACAO_ID = " + registroSituacaoId.Value + "\n";

			if (registroLoginId.HasValue)
				sql += "A.REGISTRO_LOGIN_ID = " + registroLoginId.Value + "\n";

			if (!loginPerfilId.HasValue)
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

        private string PrepararInsercaoSql(Entidade.Login.Perfil.PerfilItem perfilItem) 
        { 
            var sql = string.Empty; 

            sql += "INSERT INTO LOGIN_PERFIL_TB(\n";
			sql += "    REGISTRO_LOGIN_ID,\n";

			sql += "    NOME,\n";

			sql += "    DESCRICAO,\n";

			sql = sql.Substring(0, sql.Length - 2) + "\n";

			sql += ") VALUES (\n";
			sql += "    " + perfilItem.RegistroLoginId.ToString() + ",\n";

			    sql += "    '" + perfilItem.Nome.Replace("'", "''") + "',\n";

			if (string.IsNullOrEmpty(perfilItem.Descricao))
			    sql += "    NULL,\n";
			else
			    sql += "    '" + perfilItem.Descricao.Replace("'", "''") + "',\n";

			sql = sql.Substring(0, sql.Length - 2) + "\n";

            sql += ");\n";

            return sql; 
        } 

        private string PrepararAtualizacaoSql(Entidade.Login.Perfil.PerfilItem perfilItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    LOGIN_PERFIL_TB \n";
            sql += "SET\n";
			sql += "    REGISTRO_LOGIN_ID = " + perfilItem.RegistroLoginId.ToString() + ",\n"; 

			sql += "    NOME = '" + perfilItem.Nome.Replace("'", "''") + "',\n";

			if (string.IsNullOrEmpty(perfilItem.Descricao))
			    sql += "    DESCRICAO = NULL,\n";
			else
				sql += "    DESCRICAO = '" + perfilItem.Descricao.Replace("'", "''") + "',\n";

			sql += "    DATA_ALTERACAO = CURRENT_TIMESTAMP,\n";

			sql = sql.Substring(0, sql.Length - 2) + "\n";

            sql += "WHERE\n";
            sql += "    LOGIN_PERFIL_ID = " + perfilItem.Id + "\n";
            return sql; 
        } 

        private string PrepararExclusaoSql(Entidade.Login.Perfil.PerfilItem perfilItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    LOGIN_PERFIL_TB\n";
            sql += "SET\n";
            sql += "    REGISTRO_SITUACAO_ID = 3\n";
            sql += "WHERE\n";
            sql += "    LOGIN_PERFIL_ID = " + perfilItem.Id + "\n";
            return sql; 
        } 

        private string PrepararInativacaoSql(Entidade.Login.Perfil.PerfilItem perfilItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    LOGIN_PERFIL_TB\n";
            sql += "SET\n";
            sql += "    REGISTRO_SITUACAO_ID = 2\n";
            sql += "WHERE\n";
            sql += "    LOGIN_PERFIL_ID = " + perfilItem.Id + "\n";
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
					sql += "    A.LOGIN_PERFIL_ID = SCOPE_IDENTITY()\n";

					break;

				case Nemag.Database.Base.DATABASE_TIPO_ID.MYSQL:
					sql += "    A.LOGIN_PERFIL_ID = LAST_INSERT_ID()\n";

					break;
			}

			return sql;
		}

		#endregion
    }
}
