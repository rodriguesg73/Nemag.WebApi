using System;
using System.Collections.Generic;

namespace Nemag.Core.Persistencia.Menu.Permissao.Login 
{ 
    public partial class LoginItem : _BaseItem, Interface.Menu.Permissao.Login.ILoginItem
    { 
        #region Propriedades

        private Nemag.Database.DatabaseItem _databaseItem { get; set; }

        #endregion

        #region Construtores

        public LoginItem() : this(new Nemag.Database.DatabaseItem())
        { }

        public LoginItem(Nemag.Database.DatabaseItem databaseItem)
        {
	            _databaseItem = databaseItem;
        }

        #endregion

        #region Métodos Públicos 

        public List<Entidade.Menu.Permissao.Login.LoginItem> CarregarLista() 
        { 
            var sql = this.PrepararSelecaoSql(null, null, null, null, null); 

            return base.CarregarLista<Entidade.Menu.Permissao.Login.LoginItem>(_databaseItem, sql); 
        } 

        public List<Entidade.Menu.Permissao.Login.LoginItem> CarregarListaPorRegistroLoginId(int registroLoginId) 
        { 
            var sql = this.PrepararSelecaoSql(null, registroLoginId, null, null, null); 

            return base.CarregarLista<Entidade.Menu.Permissao.Login.LoginItem>(_databaseItem, sql); 
        } 

        public List<Entidade.Menu.Permissao.Login.LoginItem> CarregarListaPorRegistroSituacaoId(int registroSituacaoId) 
        { 
            var sql = this.PrepararSelecaoSql(null, null, registroSituacaoId, null, null); 

            return base.CarregarLista<Entidade.Menu.Permissao.Login.LoginItem>(_databaseItem, sql); 
        } 

        public List<Entidade.Menu.Permissao.Login.LoginItem> CarregarListaPorMenuId(int menuId) 
        { 
            var sql = this.PrepararSelecaoSql(null, null, null, menuId, null); 

            return base.CarregarLista<Entidade.Menu.Permissao.Login.LoginItem>(_databaseItem, sql); 
        } 

        public List<Entidade.Menu.Permissao.Login.LoginItem> CarregarListaPorLoginId(int loginId) 
        { 
            var sql = this.PrepararSelecaoSql(null, null, null, null, loginId); 

            return base.CarregarLista<Entidade.Menu.Permissao.Login.LoginItem>(_databaseItem, sql); 
        } 

        public Entidade.Menu.Permissao.Login.LoginItem CarregarItem(int menuPermissaoLoginId)
        {
            var sql = this.PrepararSelecaoSql(menuPermissaoLoginId, null, null, null, null); 

            var retorno = base.CarregarItem<Entidade.Menu.Permissao.Login.LoginItem>(_databaseItem, sql); 

            return retorno; 
        }

        public Entidade.Menu.Permissao.Login.LoginItem InserirItem(Entidade.Menu.Permissao.Login.LoginItem loginItem)
        {
            var sql = this.PrepararInsercaoSql(loginItem); 

            sql += this.ObterUltimoItemInseridoSql();

            loginItem.Id = base.CarregarItem<Entidade.Menu.Permissao.Login.LoginItem>(_databaseItem, sql).Id;

            return loginItem;
        } 

        public Entidade.Menu.Permissao.Login.LoginItem AtualizarItem(Entidade.Menu.Permissao.Login.LoginItem loginItem)
        {
            var sql = this.PrepararAtualizacaoSql(loginItem); 

            sql += this.PrepararSelecaoSql(loginItem.Id, null, null, null, null);

            loginItem.DataAlteracao = base.CarregarItem<Entidade.Menu.Permissao.Login.LoginItem>(_databaseItem, sql).DataAlteracao;

            return loginItem;
        } 

        public Entidade.Menu.Permissao.Login.LoginItem ExcluirItem(Entidade.Menu.Permissao.Login.LoginItem loginItem)
        {
            var sql = this.PrepararExclusaoSql(loginItem); 

            return base.CarregarItem<Entidade.Menu.Permissao.Login.LoginItem>(_databaseItem, sql); 
        } 

        public Entidade.Menu.Permissao.Login.LoginItem InativarItem(Entidade.Menu.Permissao.Login.LoginItem loginItem)
        {
            var sql = this.PrepararInativacaoSql(loginItem); 

            return base.CarregarItem<Entidade.Menu.Permissao.Login.LoginItem>(_databaseItem, sql); 
        } 

        #endregion 

        #region Métodos Privados 

        private string PrepararSelecaoSql()
        { 
            var sql = ""; 

            sql += "SELECT \n";
            sql += "    A.MENU_PERMISSAO_LOGIN_ID,\n";
            sql += "    A.REGISTRO_LOGIN_ID,\n";
            sql += "    A.REGISTRO_SITUACAO_ID,\n";
            sql += "    A.DATA_INCLUSAO,\n";
            sql += "    A.DATA_ALTERACAO,\n";
            sql += "    A.MENU_ID,\n";
            sql += "    A.LOGIN_ID,\n";
            sql += "    A1.MENU_ID AS MENU_ID,\n";
            sql += "    A1.MENU_SUPERIOR_ID AS MENU_MENU_SUPERIOR_ID,\n";
            sql += "    A1.TITULO AS MENU_TITULO,\n";
            sql += "    A1.DESCRICAO AS MENU_DESCRICAO,\n";
            sql += "    A1.WEB_URL AS MENU_WEB_URL,\n";
            sql += "    A1.ICONE_CSS AS MENU_ICONE_CSS,\n";
            sql += "    A3.PESSOA_ID AS LOGIN_PESSOA_ID,\n";
            sql += "    A3.NOME AS LOGIN_NOME,\n";
            sql += "    A4.LOGIN_SITUACAO_ID AS LOGIN_SITUACAO_ID,\n";
            sql += "    A4.NOME AS LOGIN_SITUACAO_NOME,\n";
            sql += "    A2.LOGIN_ID AS LOGIN_ID,\n";
            sql += "    A2.USUARIO AS LOGIN_USUARIO,\n";
            sql += "    A2.SENHA AS LOGIN_SENHA,\n";
            sql += "    A2.NOME_EXIBICAO AS LOGIN_NOME_EXIBICAO\n";
            sql += "FROM \n";
            sql += "    MENU_PERMISSAO_LOGIN_TB A\n";
            sql += "    INNER JOIN MENU_TB A1 ON A1.MENU_ID = A.MENU_ID\n";
            sql += "    INNER JOIN LOGIN_TB A2 ON A2.LOGIN_ID = A.LOGIN_ID\n";
            sql += "    INNER JOIN PESSOA_TB A3 ON A3.PESSOA_ID = A2.PESSOA_ID\n";
            sql += "    INNER JOIN LOGIN_SITUACAO_TB A4 ON A4.LOGIN_SITUACAO_ID = A2.LOGIN_SITUACAO_ID\n";

            return sql; 
        } 

        private string PrepararSelecaoSql(int? menuPermissaoLoginId, int? registroLoginId, int? registroSituacaoId, int? menuId, int? loginId)
		{ 
			var sql = ""; 

			if (menuPermissaoLoginId.HasValue)
				sql += "A.MENU_PERMISSAO_LOGIN_ID = " + menuPermissaoLoginId.Value + "\n";

			if (registroLoginId.HasValue)
				sql += "A.REGISTRO_LOGIN_ID = " + registroLoginId.Value + "\n";

			if (registroSituacaoId.HasValue)
				sql += "A.REGISTRO_SITUACAO_ID = " + registroSituacaoId.Value + "\n";

			if (menuId.HasValue)
				sql += "A.MENU_ID = " + menuId.Value + "\n";

			if (loginId.HasValue)
				sql += "A.LOGIN_ID = " + loginId.Value + "\n";

			if (!menuPermissaoLoginId.HasValue)
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

        private string PrepararInsercaoSql(Entidade.Menu.Permissao.Login.LoginItem loginItem) 
        { 
            var sql = string.Empty; 

            sql += "INSERT INTO MENU_PERMISSAO_LOGIN_TB(\n";
			sql += "    REGISTRO_LOGIN_ID,\n";

			sql += "    MENU_ID,\n";

			sql += "    LOGIN_ID,\n";

			sql = sql.Substring(0, sql.Length - 2) + "\n";

			sql += ") VALUES (\n";
			sql += "    " + loginItem.RegistroLoginId.ToString() + ",\n";

			sql += "    " + loginItem.MenuId.ToString() + ",\n";

			sql += "    " + loginItem.LoginId.ToString() + ",\n";

			sql = sql.Substring(0, sql.Length - 2) + "\n";

            sql += ");\n";

            return sql; 
        } 

        private string PrepararAtualizacaoSql(Entidade.Menu.Permissao.Login.LoginItem loginItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    MENU_PERMISSAO_LOGIN_TB \n";
            sql += "SET\n";
			sql += "    REGISTRO_LOGIN_ID = " + loginItem.RegistroLoginId.ToString() + ",\n"; 

			sql += "    DATA_ALTERACAO = CURRENT_TIMESTAMP,\n";

			sql += "    MENU_ID = " + loginItem.MenuId.ToString() + ",\n"; 

			sql += "    LOGIN_ID = " + loginItem.LoginId.ToString() + ",\n"; 

			sql = sql.Substring(0, sql.Length - 2) + "\n";

            sql += "WHERE\n";
            sql += "    MENU_PERMISSAO_LOGIN_ID = " + loginItem.Id + "\n";
            return sql; 
        } 

        private string PrepararExclusaoSql(Entidade.Menu.Permissao.Login.LoginItem loginItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    MENU_PERMISSAO_LOGIN_TB\n";
            sql += "SET\n";
            sql += "    REGISTRO_SITUACAO_ID = 3\n";
            sql += "WHERE\n";
            sql += "    MENU_PERMISSAO_LOGIN_ID = " + loginItem.Id + "\n";
            return sql; 
        } 

        private string PrepararInativacaoSql(Entidade.Menu.Permissao.Login.LoginItem loginItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    MENU_PERMISSAO_LOGIN_TB\n";
            sql += "SET\n";
            sql += "    REGISTRO_SITUACAO_ID = 2\n";
            sql += "WHERE\n";
            sql += "    MENU_PERMISSAO_LOGIN_ID = " + loginItem.Id + "\n";
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
					sql += "    A.MENU_PERMISSAO_LOGIN_ID = SCOPE_IDENTITY()\n";

					break;

				case Nemag.Database.Base.DATABASE_TIPO_ID.MYSQL:
					sql += "    A.MENU_PERMISSAO_LOGIN_ID = LAST_INSERT_ID()\n";

					break;
			}

			return sql;
		}

		#endregion
    }
}
