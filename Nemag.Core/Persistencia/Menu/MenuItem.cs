using System;
using System.Collections.Generic;

namespace Nemag.Core.Persistencia.Menu 
{ 
    public partial class MenuItem : _BaseItem, Interface.Menu.IMenuItem
    { 
        #region Propriedades

        private Nemag.Database.DatabaseItem _databaseItem { get; set; }

        #endregion

        #region Construtores

        public MenuItem() : this(new Nemag.Database.DatabaseItem())
        { }

        public MenuItem(Nemag.Database.DatabaseItem databaseItem)
        {
	            _databaseItem = databaseItem;
        }

        #endregion

        #region Métodos Públicos 

        public List<Entidade.Menu.MenuItem> CarregarLista() 
        { 
            var sql = this.PrepararSelecaoSql(null, null, null); 

            return base.CarregarLista<Entidade.Menu.MenuItem>(_databaseItem, sql); 
        } 

        public List<Entidade.Menu.MenuItem> CarregarListaPorRegistroSituacaoId(int registroSituacaoId) 
        { 
            var sql = this.PrepararSelecaoSql(null, registroSituacaoId, null); 

            return base.CarregarLista<Entidade.Menu.MenuItem>(_databaseItem, sql); 
        } 

        public List<Entidade.Menu.MenuItem> CarregarListaPorRegistroLoginId(int registroLoginId) 
        { 
            var sql = this.PrepararSelecaoSql(null, null, registroLoginId); 

            return base.CarregarLista<Entidade.Menu.MenuItem>(_databaseItem, sql); 
        } 

        public Entidade.Menu.MenuItem CarregarItem(int menuId)
        {
            var sql = this.PrepararSelecaoSql(menuId, null, null); 

            var retorno = base.CarregarItem<Entidade.Menu.MenuItem>(_databaseItem, sql); 

            return retorno; 
        }

        public Entidade.Menu.MenuItem InserirItem(Entidade.Menu.MenuItem menuItem)
        {
            var sql = this.PrepararInsercaoSql(menuItem); 

            sql += this.ObterUltimoItemInseridoSql();

            menuItem.Id = base.CarregarItem<Entidade.Menu.MenuItem>(_databaseItem, sql).Id;

            return menuItem;
        } 

        public Entidade.Menu.MenuItem AtualizarItem(Entidade.Menu.MenuItem menuItem)
        {
            var sql = this.PrepararAtualizacaoSql(menuItem); 

            sql += this.PrepararSelecaoSql(menuItem.Id, null, null);

            menuItem.DataAlteracao = base.CarregarItem<Entidade.Menu.MenuItem>(_databaseItem, sql).DataAlteracao;

            return menuItem;
        } 

        public Entidade.Menu.MenuItem ExcluirItem(Entidade.Menu.MenuItem menuItem)
        {
            var sql = this.PrepararExclusaoSql(menuItem); 

            return base.CarregarItem<Entidade.Menu.MenuItem>(_databaseItem, sql); 
        } 

        public Entidade.Menu.MenuItem InativarItem(Entidade.Menu.MenuItem menuItem)
        {
            var sql = this.PrepararInativacaoSql(menuItem); 

            return base.CarregarItem<Entidade.Menu.MenuItem>(_databaseItem, sql); 
        } 

        #endregion 

        #region Métodos Privados 

        private string PrepararSelecaoSql()
        { 
            var sql = ""; 

            sql += "SELECT \n";
            sql += "    A.MENU_ID,\n";
            sql += "    A.REGISTRO_SITUACAO_ID,\n";
            sql += "    A.REGISTRO_LOGIN_ID,\n";
            sql += "    A.MENU_SUPERIOR_ID,\n";
            sql += "    A.TITULO,\n";
            sql += "    A.DESCRICAO,\n";
            sql += "    A.WEB_URL,\n";
            sql += "    A.ICONE_CSS,\n";
            sql += "    A.DATA_INCLUSAO,\n";
            sql += "    A.DATA_ALTERACAO\n";
            sql += "FROM \n";
            sql += "    MENU_TB A\n";

            return sql; 
        } 

        private string PrepararSelecaoSql(int? menuId, int? registroSituacaoId, int? registroLoginId)
		{ 
			var sql = ""; 

			if (menuId.HasValue)
				sql += "A.MENU_ID = " + menuId.Value + "\n";

			if (registroSituacaoId.HasValue)
				sql += "A.REGISTRO_SITUACAO_ID = " + registroSituacaoId.Value + "\n";

			if (registroLoginId.HasValue)
				sql += "A.REGISTRO_LOGIN_ID = " + registroLoginId.Value + "\n";

			if (!menuId.HasValue)
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

        private string PrepararInsercaoSql(Entidade.Menu.MenuItem menuItem) 
        { 
            var sql = string.Empty; 

            sql += "INSERT INTO MENU_TB(\n";
			sql += "    REGISTRO_LOGIN_ID,\n";

			sql += "    MENU_SUPERIOR_ID,\n";

			sql += "    TITULO,\n";

			sql += "    DESCRICAO,\n";

			sql += "    WEB_URL,\n";

			sql += "    ICONE_CSS,\n";

			sql = sql.Substring(0, sql.Length - 2) + "\n";

			sql += ") VALUES (\n";
			sql += "    " + menuItem.RegistroLoginId.ToString() + ",\n";

			sql += "    " + (menuItem.MenuSuperiorId > int.MinValue ? menuItem.MenuSuperiorId.ToString() : "NULL") + ",\n";

			    sql += "    '" + menuItem.Titulo.Replace("'", "''") + "',\n";

			if (string.IsNullOrEmpty(menuItem.Descricao))
			    sql += "    NULL,\n";
			else
			    sql += "    '" + menuItem.Descricao.Replace("'", "''") + "',\n";

			if (string.IsNullOrEmpty(menuItem.WebUrl))
			    sql += "    NULL,\n";
			else
			    sql += "    '" + menuItem.WebUrl.Replace("'", "''") + "',\n";

			if (string.IsNullOrEmpty(menuItem.IconeCss))
			    sql += "    NULL,\n";
			else
			    sql += "    '" + menuItem.IconeCss.Replace("'", "''") + "',\n";

			sql = sql.Substring(0, sql.Length - 2) + "\n";

            sql += ");\n";

            return sql; 
        } 

        private string PrepararAtualizacaoSql(Entidade.Menu.MenuItem menuItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    MENU_TB \n";
            sql += "SET\n";
			sql += "    REGISTRO_LOGIN_ID = " + menuItem.RegistroLoginId.ToString() + ",\n"; 

			sql += "    MENU_SUPERIOR_ID = " + (menuItem.MenuSuperiorId > int.MinValue ? menuItem.MenuSuperiorId.ToString() : "NULL") + ",\n"; 

			sql += "    TITULO = '" + menuItem.Titulo.Replace("'", "''") + "',\n";

			if (string.IsNullOrEmpty(menuItem.Descricao))
			    sql += "    DESCRICAO = NULL,\n";
			else
				sql += "    DESCRICAO = '" + menuItem.Descricao.Replace("'", "''") + "',\n";

			if (string.IsNullOrEmpty(menuItem.WebUrl))
			    sql += "    WEB_URL = NULL,\n";
			else
				sql += "    WEB_URL = '" + menuItem.WebUrl.Replace("'", "''") + "',\n";

			if (string.IsNullOrEmpty(menuItem.IconeCss))
			    sql += "    ICONE_CSS = NULL,\n";
			else
				sql += "    ICONE_CSS = '" + menuItem.IconeCss.Replace("'", "''") + "',\n";

			sql += "    DATA_ALTERACAO = CURRENT_TIMESTAMP,\n";

			sql = sql.Substring(0, sql.Length - 2) + "\n";

            sql += "WHERE\n";
            sql += "    MENU_ID = " + menuItem.Id + "\n";
            return sql; 
        } 

        private string PrepararExclusaoSql(Entidade.Menu.MenuItem menuItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    MENU_TB\n";
            sql += "SET\n";
            sql += "    REGISTRO_SITUACAO_ID = 3\n";
            sql += "WHERE\n";
            sql += "    MENU_ID = " + menuItem.Id + "\n";
            return sql; 
        } 

        private string PrepararInativacaoSql(Entidade.Menu.MenuItem menuItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    MENU_TB\n";
            sql += "SET\n";
            sql += "    REGISTRO_SITUACAO_ID = 2\n";
            sql += "WHERE\n";
            sql += "    MENU_ID = " + menuItem.Id + "\n";
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
					sql += "    A.MENU_ID = SCOPE_IDENTITY()\n";

					break;

				case Nemag.Database.Base.DATABASE_TIPO_ID.MYSQL:
					sql += "    A.MENU_ID = LAST_INSERT_ID()\n";

					break;
			}

			return sql;
		}

		#endregion
    }
}
