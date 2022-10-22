using System;
using System.Collections.Generic;

namespace Nemag.Core.Persistencia.Login.Atribuicao 
{ 
    public partial class AtribuicaoItem : _BaseItem, Interface.Login.Atribuicao.IAtribuicaoItem
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

        public List<Entidade.Login.Atribuicao.AtribuicaoItem> CarregarLista() 
        { 
            var sql = this.PrepararSelecaoSql(null, null, null, null, null, null); 

            return base.CarregarLista<Entidade.Login.Atribuicao.AtribuicaoItem>(_databaseItem, sql); 
        } 

        public List<Entidade.Login.Atribuicao.AtribuicaoItem> CarregarListaPorRegistroSituacaoId(int registroSituacaoId) 
        { 
            var sql = this.PrepararSelecaoSql(null, registroSituacaoId, null, null, null, null); 

            return base.CarregarLista<Entidade.Login.Atribuicao.AtribuicaoItem>(_databaseItem, sql); 
        } 

        public List<Entidade.Login.Atribuicao.AtribuicaoItem> CarregarListaPorRegistroLoginId(int registroLoginId) 
        { 
            var sql = this.PrepararSelecaoSql(null, null, registroLoginId, null, null, null); 

            return base.CarregarLista<Entidade.Login.Atribuicao.AtribuicaoItem>(_databaseItem, sql); 
        } 

        public List<Entidade.Login.Atribuicao.AtribuicaoItem> CarregarListaPorLoginGrupoId(int loginGrupoId) 
        { 
            var sql = this.PrepararSelecaoSql(null, null, null, loginGrupoId, null, null); 

            return base.CarregarLista<Entidade.Login.Atribuicao.AtribuicaoItem>(_databaseItem, sql); 
        } 

        public List<Entidade.Login.Atribuicao.AtribuicaoItem> CarregarListaPorLoginPerfilId(int loginPerfilId) 
        { 
            var sql = this.PrepararSelecaoSql(null, null, null, null, loginPerfilId, null); 

            return base.CarregarLista<Entidade.Login.Atribuicao.AtribuicaoItem>(_databaseItem, sql); 
        } 

        public List<Entidade.Login.Atribuicao.AtribuicaoItem> CarregarListaPorLoginId(int loginId) 
        { 
            var sql = this.PrepararSelecaoSql(null, null, null, null, null, loginId); 

            return base.CarregarLista<Entidade.Login.Atribuicao.AtribuicaoItem>(_databaseItem, sql); 
        } 

        public Entidade.Login.Atribuicao.AtribuicaoItem CarregarItem(int loginAtribuicaoId)
        {
            var sql = this.PrepararSelecaoSql(loginAtribuicaoId, null, null, null, null, null); 

            var retorno = base.CarregarItem<Entidade.Login.Atribuicao.AtribuicaoItem>(_databaseItem, sql); 

            return retorno; 
        }

        public Entidade.Login.Atribuicao.AtribuicaoItem InserirItem(Entidade.Login.Atribuicao.AtribuicaoItem atribuicaoItem)
        {
            var sql = this.PrepararInsercaoSql(atribuicaoItem); 

            sql += this.ObterUltimoItemInseridoSql();

            atribuicaoItem.Id = base.CarregarItem<Entidade.Login.Atribuicao.AtribuicaoItem>(_databaseItem, sql).Id;

            return atribuicaoItem;
        } 

        public Entidade.Login.Atribuicao.AtribuicaoItem AtualizarItem(Entidade.Login.Atribuicao.AtribuicaoItem atribuicaoItem)
        {
            var sql = this.PrepararAtualizacaoSql(atribuicaoItem); 

            sql += this.PrepararSelecaoSql(atribuicaoItem.Id, null, null, null, null, null);

            atribuicaoItem.DataAlteracao = base.CarregarItem<Entidade.Login.Atribuicao.AtribuicaoItem>(_databaseItem, sql).DataAlteracao;

            return atribuicaoItem;
        } 

        public Entidade.Login.Atribuicao.AtribuicaoItem ExcluirItem(Entidade.Login.Atribuicao.AtribuicaoItem atribuicaoItem)
        {
            var sql = this.PrepararExclusaoSql(atribuicaoItem); 

            return base.CarregarItem<Entidade.Login.Atribuicao.AtribuicaoItem>(_databaseItem, sql); 
        } 

        public Entidade.Login.Atribuicao.AtribuicaoItem InativarItem(Entidade.Login.Atribuicao.AtribuicaoItem atribuicaoItem)
        {
            var sql = this.PrepararInativacaoSql(atribuicaoItem); 

            return base.CarregarItem<Entidade.Login.Atribuicao.AtribuicaoItem>(_databaseItem, sql); 
        } 

        #endregion 

        #region Métodos Privados 

        private string PrepararSelecaoSql()
        { 
            var sql = ""; 

            sql += "SELECT \n";
            sql += "    A.LOGIN_ATRIBUICAO_ID,\n";
            sql += "    A.REGISTRO_SITUACAO_ID,\n";
            sql += "    A.REGISTRO_LOGIN_ID,\n";
            sql += "    A.DATA_INCLUSAO,\n";
            sql += "    A.DATA_ALTERACAO,\n";
            sql += "    A.LOGIN_GRUPO_ID,\n";
            sql += "    A.LOGIN_PERFIL_ID,\n";
            sql += "    A.LOGIN_ID,\n";
            sql += "    A1.LOGIN_GRUPO_ID AS LOGIN_GRUPO_ID,\n";
            sql += "    A1.NOME AS LOGIN_GRUPO_NOME,\n";
            sql += "    A1.DESCRICAO AS LOGIN_GRUPO_DESCRICAO,\n";
            sql += "    A2.LOGIN_PERFIL_ID AS LOGIN_PERFIL_ID,\n";
            sql += "    A2.NOME AS LOGIN_PERFIL_NOME,\n";
            sql += "    A2.DESCRICAO AS LOGIN_PERFIL_DESCRICAO,\n";
            sql += "    A4.PESSOA_ID AS LOGIN_PESSOA_ID,\n";
            sql += "    A4.NOME AS LOGIN_NOME,\n";
            sql += "    A5.LOGIN_SITUACAO_ID AS LOGIN_SITUACAO_ID,\n";
            sql += "    A5.NOME AS LOGIN_SITUACAO_NOME,\n";
            sql += "    A3.LOGIN_ID AS LOGIN_ID,\n";
            sql += "    A3.USUARIO AS LOGIN_USUARIO,\n";
            sql += "    A3.SENHA AS LOGIN_SENHA,\n";
            sql += "    A3.NOME_EXIBICAO AS LOGIN_NOME_EXIBICAO\n";
            sql += "FROM \n";
            sql += "    LOGIN_ATRIBUICAO_TB A\n";
            sql += "    INNER JOIN LOGIN_GRUPO_TB A1 ON A1.LOGIN_GRUPO_ID = A.LOGIN_GRUPO_ID\n";
            sql += "    INNER JOIN LOGIN_PERFIL_TB A2 ON A2.LOGIN_PERFIL_ID = A.LOGIN_PERFIL_ID\n";
            sql += "    INNER JOIN LOGIN_TB A3 ON A3.LOGIN_ID = A.LOGIN_ID\n";
            sql += "    INNER JOIN PESSOA_TB A4 ON A4.PESSOA_ID = A3.PESSOA_ID\n";
            sql += "    INNER JOIN LOGIN_SITUACAO_TB A5 ON A5.LOGIN_SITUACAO_ID = A3.LOGIN_SITUACAO_ID\n";

            return sql; 
        } 

        private string PrepararSelecaoSql(int? loginAtribuicaoId, int? registroSituacaoId, int? registroLoginId, int? loginGrupoId, int? loginPerfilId, int? loginId)
		{ 
			var sql = ""; 

			if (loginAtribuicaoId.HasValue)
				sql += "A.LOGIN_ATRIBUICAO_ID = " + loginAtribuicaoId.Value + "\n";

			if (registroSituacaoId.HasValue)
				sql += "A.REGISTRO_SITUACAO_ID = " + registroSituacaoId.Value + "\n";

			if (registroLoginId.HasValue)
				sql += "A.REGISTRO_LOGIN_ID = " + registroLoginId.Value + "\n";

			if (loginGrupoId.HasValue)
				sql += "A.LOGIN_GRUPO_ID = " + loginGrupoId.Value + "\n";

			if (loginPerfilId.HasValue)
				sql += "A.LOGIN_PERFIL_ID = " + loginPerfilId.Value + "\n";

			if (loginId.HasValue)
				sql += "A.LOGIN_ID = " + loginId.Value + "\n";

			if (!loginAtribuicaoId.HasValue)
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

        private string PrepararInsercaoSql(Entidade.Login.Atribuicao.AtribuicaoItem atribuicaoItem) 
        { 
            var sql = string.Empty; 

            sql += "INSERT INTO LOGIN_ATRIBUICAO_TB(\n";
			sql += "    REGISTRO_LOGIN_ID,\n";

			sql += "    LOGIN_GRUPO_ID,\n";

			sql += "    LOGIN_PERFIL_ID,\n";

			sql += "    LOGIN_ID,\n";

			sql = sql.Substring(0, sql.Length - 2) + "\n";

			sql += ") VALUES (\n";
			sql += "    " + atribuicaoItem.RegistroLoginId.ToString() + ",\n";

			sql += "    " + atribuicaoItem.LoginGrupoId.ToString() + ",\n";

			sql += "    " + atribuicaoItem.LoginPerfilId.ToString() + ",\n";

			sql += "    " + atribuicaoItem.LoginId.ToString() + ",\n";

			sql = sql.Substring(0, sql.Length - 2) + "\n";

            sql += ");\n";

            return sql; 
        } 

        private string PrepararAtualizacaoSql(Entidade.Login.Atribuicao.AtribuicaoItem atribuicaoItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    LOGIN_ATRIBUICAO_TB \n";
            sql += "SET\n";
			sql += "    REGISTRO_LOGIN_ID = " + atribuicaoItem.RegistroLoginId.ToString() + ",\n"; 

			sql += "    DATA_ALTERACAO = CURRENT_TIMESTAMP,\n";

			sql += "    LOGIN_GRUPO_ID = " + atribuicaoItem.LoginGrupoId.ToString() + ",\n"; 

			sql += "    LOGIN_PERFIL_ID = " + atribuicaoItem.LoginPerfilId.ToString() + ",\n"; 

			sql += "    LOGIN_ID = " + atribuicaoItem.LoginId.ToString() + ",\n"; 

			sql = sql.Substring(0, sql.Length - 2) + "\n";

            sql += "WHERE\n";
            sql += "    LOGIN_ATRIBUICAO_ID = " + atribuicaoItem.Id + "\n";
            return sql; 
        } 

        private string PrepararExclusaoSql(Entidade.Login.Atribuicao.AtribuicaoItem atribuicaoItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    LOGIN_ATRIBUICAO_TB\n";
            sql += "SET\n";
            sql += "    REGISTRO_SITUACAO_ID = 3\n";
            sql += "WHERE\n";
            sql += "    LOGIN_ATRIBUICAO_ID = " + atribuicaoItem.Id + "\n";
            return sql; 
        } 

        private string PrepararInativacaoSql(Entidade.Login.Atribuicao.AtribuicaoItem atribuicaoItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    LOGIN_ATRIBUICAO_TB\n";
            sql += "SET\n";
            sql += "    REGISTRO_SITUACAO_ID = 2\n";
            sql += "WHERE\n";
            sql += "    LOGIN_ATRIBUICAO_ID = " + atribuicaoItem.Id + "\n";
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
					sql += "    A.LOGIN_ATRIBUICAO_ID = SCOPE_IDENTITY()\n";

					break;

				case Nemag.Database.Base.DATABASE_TIPO_ID.MYSQL:
					sql += "    A.LOGIN_ATRIBUICAO_ID = LAST_INSERT_ID()\n";

					break;
			}

			return sql;
		}

		#endregion
    }
}
