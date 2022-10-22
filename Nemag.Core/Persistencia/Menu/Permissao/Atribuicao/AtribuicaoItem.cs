using System;
using System.Collections.Generic;

namespace Nemag.Core.Persistencia.Menu.Permissao.Atribuicao 
{ 
    public partial class AtribuicaoItem : _BaseItem, Interface.Menu.Permissao.Atribuicao.IAtribuicaoItem
    { 
        #region Propriedades

        private Nemag.Database.DatabaseItem _databaseItem { get; set; }

        #endregion

        #region Construtores

        public AtribuicaoItem() : this(new Nemag.Database.DatabaseItem())
        { }

        public AtribuicaoItem(Nemag.Database.DatabaseItem databaseItem)
        {
	            _databaseItem = databaseItem;
        }

        #endregion

        #region Métodos Públicos 

        public List<Entidade.Menu.Permissao.Atribuicao.AtribuicaoItem> CarregarLista() 
        { 
            var sql = this.PrepararSelecaoSql(null, null, null, null, null, null); 

            return base.CarregarLista<Entidade.Menu.Permissao.Atribuicao.AtribuicaoItem>(_databaseItem, sql); 
        } 

        public List<Entidade.Menu.Permissao.Atribuicao.AtribuicaoItem> CarregarListaPorRegistroLoginId(int registroLoginId) 
        { 
            var sql = this.PrepararSelecaoSql(null, registroLoginId, null, null, null, null); 

            return base.CarregarLista<Entidade.Menu.Permissao.Atribuicao.AtribuicaoItem>(_databaseItem, sql); 
        } 

        public List<Entidade.Menu.Permissao.Atribuicao.AtribuicaoItem> CarregarListaPorRegistroSituacaoId(int registroSituacaoId) 
        { 
            var sql = this.PrepararSelecaoSql(null, null, registroSituacaoId, null, null, null); 

            return base.CarregarLista<Entidade.Menu.Permissao.Atribuicao.AtribuicaoItem>(_databaseItem, sql); 
        } 

        public List<Entidade.Menu.Permissao.Atribuicao.AtribuicaoItem> CarregarListaPorMenuId(int menuId) 
        { 
            var sql = this.PrepararSelecaoSql(null, null, null, menuId, null, null); 

            return base.CarregarLista<Entidade.Menu.Permissao.Atribuicao.AtribuicaoItem>(_databaseItem, sql); 
        } 

        public List<Entidade.Menu.Permissao.Atribuicao.AtribuicaoItem> CarregarListaPorLoginGrupoId(int loginGrupoId) 
        { 
            var sql = this.PrepararSelecaoSql(null, null, null, null, loginGrupoId, null); 

            return base.CarregarLista<Entidade.Menu.Permissao.Atribuicao.AtribuicaoItem>(_databaseItem, sql); 
        } 

        public List<Entidade.Menu.Permissao.Atribuicao.AtribuicaoItem> CarregarListaPorLoginPerfilId(int loginPerfilId) 
        { 
            var sql = this.PrepararSelecaoSql(null, null, null, null, null, loginPerfilId); 

            return base.CarregarLista<Entidade.Menu.Permissao.Atribuicao.AtribuicaoItem>(_databaseItem, sql); 
        } 

        public Entidade.Menu.Permissao.Atribuicao.AtribuicaoItem CarregarItem(int menuPermissaoAtribuicaoId)
        {
            var sql = this.PrepararSelecaoSql(menuPermissaoAtribuicaoId, null, null, null, null, null); 

            var retorno = base.CarregarItem<Entidade.Menu.Permissao.Atribuicao.AtribuicaoItem>(_databaseItem, sql); 

            return retorno; 
        }

        public Entidade.Menu.Permissao.Atribuicao.AtribuicaoItem InserirItem(Entidade.Menu.Permissao.Atribuicao.AtribuicaoItem atribuicaoItem)
        {
            var sql = this.PrepararInsercaoSql(atribuicaoItem); 

            sql += this.ObterUltimoItemInseridoSql();

            atribuicaoItem.Id = base.CarregarItem<Entidade.Menu.Permissao.Atribuicao.AtribuicaoItem>(_databaseItem, sql).Id;

            return atribuicaoItem;
        } 

        public Entidade.Menu.Permissao.Atribuicao.AtribuicaoItem AtualizarItem(Entidade.Menu.Permissao.Atribuicao.AtribuicaoItem atribuicaoItem)
        {
            var sql = this.PrepararAtualizacaoSql(atribuicaoItem); 

            sql += this.PrepararSelecaoSql(atribuicaoItem.Id, null, null, null, null, null);

            atribuicaoItem.DataAlteracao = base.CarregarItem<Entidade.Menu.Permissao.Atribuicao.AtribuicaoItem>(_databaseItem, sql).DataAlteracao;

            return atribuicaoItem;
        } 

        public Entidade.Menu.Permissao.Atribuicao.AtribuicaoItem ExcluirItem(Entidade.Menu.Permissao.Atribuicao.AtribuicaoItem atribuicaoItem)
        {
            var sql = this.PrepararExclusaoSql(atribuicaoItem); 

            return base.CarregarItem<Entidade.Menu.Permissao.Atribuicao.AtribuicaoItem>(_databaseItem, sql); 
        } 

        public Entidade.Menu.Permissao.Atribuicao.AtribuicaoItem InativarItem(Entidade.Menu.Permissao.Atribuicao.AtribuicaoItem atribuicaoItem)
        {
            var sql = this.PrepararInativacaoSql(atribuicaoItem); 

            return base.CarregarItem<Entidade.Menu.Permissao.Atribuicao.AtribuicaoItem>(_databaseItem, sql); 
        } 

        #endregion 

        #region Métodos Privados 

        private string PrepararSelecaoSql()
        { 
            var sql = ""; 

            sql += "SELECT \n";
            sql += "    A.MENU_PERMISSAO_ATRIBUICAO_ID,\n";
            sql += "    A.REGISTRO_LOGIN_ID,\n";
            sql += "    A.REGISTRO_SITUACAO_ID,\n";
            sql += "    A.DATA_INCLUSAO,\n";
            sql += "    A.DATA_ALTERACAO,\n";
            sql += "    A.MENU_ID,\n";
            sql += "    A.LOGIN_GRUPO_ID,\n";
            sql += "    A.LOGIN_PERFIL_ID,\n";
            sql += "    A1.MENU_ID AS MENU_ID,\n";
            sql += "    A1.MENU_SUPERIOR_ID AS MENU_MENU_SUPERIOR_ID,\n";
            sql += "    A1.TITULO AS MENU_TITULO,\n";
            sql += "    A1.DESCRICAO AS MENU_DESCRICAO,\n";
            sql += "    A1.WEB_URL AS MENU_WEB_URL,\n";
            sql += "    A1.ICONE_CSS AS MENU_ICONE_CSS,\n";
            sql += "    A2.LOGIN_GRUPO_ID AS LOGIN_GRUPO_ID,\n";
            sql += "    A2.NOME AS LOGIN_GRUPO_NOME,\n";
            sql += "    A2.DESCRICAO AS LOGIN_GRUPO_DESCRICAO,\n";
            sql += "    A3.LOGIN_PERFIL_ID AS LOGIN_PERFIL_ID,\n";
            sql += "    A3.NOME AS LOGIN_PERFIL_NOME,\n";
            sql += "    A3.DESCRICAO AS LOGIN_PERFIL_DESCRICAO\n";
            sql += "FROM \n";
            sql += "    MENU_PERMISSAO_ATRIBUICAO_TB A\n";
            sql += "    INNER JOIN MENU_TB A1 ON A1.MENU_ID = A.MENU_ID\n";
            sql += "    INNER JOIN LOGIN_GRUPO_TB A2 ON A2.LOGIN_GRUPO_ID = A.LOGIN_GRUPO_ID\n";
            sql += "    INNER JOIN LOGIN_PERFIL_TB A3 ON A3.LOGIN_PERFIL_ID = A.LOGIN_PERFIL_ID\n";

            return sql; 
        } 

        private string PrepararSelecaoSql(int? menuPermissaoAtribuicaoId, int? registroLoginId, int? registroSituacaoId, int? menuId, int? loginGrupoId, int? loginPerfilId)
		{ 
			var sql = ""; 

			if (menuPermissaoAtribuicaoId.HasValue)
				sql += "A.MENU_PERMISSAO_ATRIBUICAO_ID = " + menuPermissaoAtribuicaoId.Value + "\n";

			if (registroLoginId.HasValue)
				sql += "A.REGISTRO_LOGIN_ID = " + registroLoginId.Value + "\n";

			if (registroSituacaoId.HasValue)
				sql += "A.REGISTRO_SITUACAO_ID = " + registroSituacaoId.Value + "\n";

			if (menuId.HasValue)
				sql += "A.MENU_ID = " + menuId.Value + "\n";

			if (loginGrupoId.HasValue)
				sql += "A.LOGIN_GRUPO_ID = " + loginGrupoId.Value + "\n";

			if (loginPerfilId.HasValue)
				sql += "A.LOGIN_PERFIL_ID = " + loginPerfilId.Value + "\n";

			if (!menuPermissaoAtribuicaoId.HasValue)
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

        private string PrepararInsercaoSql(Entidade.Menu.Permissao.Atribuicao.AtribuicaoItem atribuicaoItem) 
        { 
            var sql = string.Empty; 

            sql += "INSERT INTO MENU_PERMISSAO_ATRIBUICAO_TB(\n";
			sql += "    REGISTRO_LOGIN_ID,\n";

			sql += "    MENU_ID,\n";

			sql += "    LOGIN_GRUPO_ID,\n";

			sql += "    LOGIN_PERFIL_ID,\n";

			sql = sql.Substring(0, sql.Length - 2) + "\n";

			sql += ") VALUES (\n";
			sql += "    " + atribuicaoItem.RegistroLoginId.ToString() + ",\n";

			sql += "    " + atribuicaoItem.MenuId.ToString() + ",\n";

			sql += "    " + atribuicaoItem.LoginGrupoId.ToString() + ",\n";

			sql += "    " + atribuicaoItem.LoginPerfilId.ToString() + ",\n";

			sql = sql.Substring(0, sql.Length - 2) + "\n";

            sql += ");\n";

            return sql; 
        } 

        private string PrepararAtualizacaoSql(Entidade.Menu.Permissao.Atribuicao.AtribuicaoItem atribuicaoItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    MENU_PERMISSAO_ATRIBUICAO_TB \n";
            sql += "SET\n";
			sql += "    REGISTRO_LOGIN_ID = " + atribuicaoItem.RegistroLoginId.ToString() + ",\n"; 

			sql += "    DATA_ALTERACAO = CURRENT_TIMESTAMP,\n";

			sql += "    MENU_ID = " + atribuicaoItem.MenuId.ToString() + ",\n"; 

			sql += "    LOGIN_GRUPO_ID = " + atribuicaoItem.LoginGrupoId.ToString() + ",\n"; 

			sql += "    LOGIN_PERFIL_ID = " + atribuicaoItem.LoginPerfilId.ToString() + ",\n"; 

			sql = sql.Substring(0, sql.Length - 2) + "\n";

            sql += "WHERE\n";
            sql += "    MENU_PERMISSAO_ATRIBUICAO_ID = " + atribuicaoItem.Id + "\n";
            return sql; 
        } 

        private string PrepararExclusaoSql(Entidade.Menu.Permissao.Atribuicao.AtribuicaoItem atribuicaoItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    MENU_PERMISSAO_ATRIBUICAO_TB\n";
            sql += "SET\n";
            sql += "    REGISTRO_SITUACAO_ID = 3\n";
            sql += "WHERE\n";
            sql += "    MENU_PERMISSAO_ATRIBUICAO_ID = " + atribuicaoItem.Id + "\n";
            return sql; 
        } 

        private string PrepararInativacaoSql(Entidade.Menu.Permissao.Atribuicao.AtribuicaoItem atribuicaoItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    MENU_PERMISSAO_ATRIBUICAO_TB\n";
            sql += "SET\n";
            sql += "    REGISTRO_SITUACAO_ID = 2\n";
            sql += "WHERE\n";
            sql += "    MENU_PERMISSAO_ATRIBUICAO_ID = " + atribuicaoItem.Id + "\n";
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
					sql += "    A.MENU_PERMISSAO_ATRIBUICAO_ID = SCOPE_IDENTITY()\n";

					break;

				case Nemag.Database.Base.DATABASE_TIPO_ID.MYSQL:
					sql += "    A.MENU_PERMISSAO_ATRIBUICAO_ID = LAST_INSERT_ID()\n";

					break;
			}

			return sql;
		}

		#endregion
    }
}
