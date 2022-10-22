using System;
using System.Collections.Generic;

namespace Nemag.Core.Persistencia.Pessoa.Contato.Tipo 
{ 
    public partial class TipoItem : _BaseItem, Interface.Pessoa.Contato.Tipo.ITipoItem
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

        public List<Entidade.Pessoa.Contato.Tipo.TipoItem> CarregarLista() 
        { 
            var sql = this.PrepararSelecaoSql(null, null, null); 

            return base.CarregarLista<Entidade.Pessoa.Contato.Tipo.TipoItem>(_databaseItem, sql); 
        } 

        public List<Entidade.Pessoa.Contato.Tipo.TipoItem> CarregarListaPorRegistroSituacaoId(int registroSituacaoId) 
        { 
            var sql = this.PrepararSelecaoSql(null, registroSituacaoId, null); 

            return base.CarregarLista<Entidade.Pessoa.Contato.Tipo.TipoItem>(_databaseItem, sql); 
        } 

        public List<Entidade.Pessoa.Contato.Tipo.TipoItem> CarregarListaPorRegistroLoginId(int registroLoginId) 
        { 
            var sql = this.PrepararSelecaoSql(null, null, registroLoginId); 

            return base.CarregarLista<Entidade.Pessoa.Contato.Tipo.TipoItem>(_databaseItem, sql); 
        } 

        public Entidade.Pessoa.Contato.Tipo.TipoItem CarregarItem(int pessoaContatoTipoId)
        {
            var sql = this.PrepararSelecaoSql(pessoaContatoTipoId, null, null); 

            var retorno = base.CarregarItem<Entidade.Pessoa.Contato.Tipo.TipoItem>(_databaseItem, sql); 

            return retorno; 
        }

        public Entidade.Pessoa.Contato.Tipo.TipoItem InserirItem(Entidade.Pessoa.Contato.Tipo.TipoItem tipoItem)
        {
            var sql = this.PrepararInsercaoSql(tipoItem); 

            sql += this.ObterUltimoItemInseridoSql();

            tipoItem.Id = base.CarregarItem<Entidade.Pessoa.Contato.Tipo.TipoItem>(_databaseItem, sql).Id;

            return tipoItem;
        } 

        public Entidade.Pessoa.Contato.Tipo.TipoItem AtualizarItem(Entidade.Pessoa.Contato.Tipo.TipoItem tipoItem)
        {
            var sql = this.PrepararAtualizacaoSql(tipoItem); 

            sql += this.PrepararSelecaoSql(tipoItem.Id, null, null);

            tipoItem.DataAlteracao = base.CarregarItem<Entidade.Pessoa.Contato.Tipo.TipoItem>(_databaseItem, sql).DataAlteracao;

            return tipoItem;
        } 

        public Entidade.Pessoa.Contato.Tipo.TipoItem ExcluirItem(Entidade.Pessoa.Contato.Tipo.TipoItem tipoItem)
        {
            var sql = this.PrepararExclusaoSql(tipoItem); 

            return base.CarregarItem<Entidade.Pessoa.Contato.Tipo.TipoItem>(_databaseItem, sql); 
        } 

        public Entidade.Pessoa.Contato.Tipo.TipoItem InativarItem(Entidade.Pessoa.Contato.Tipo.TipoItem tipoItem)
        {
            var sql = this.PrepararInativacaoSql(tipoItem); 

            return base.CarregarItem<Entidade.Pessoa.Contato.Tipo.TipoItem>(_databaseItem, sql); 
        } 

        #endregion 

        #region Métodos Privados 

        private string PrepararSelecaoSql()
        { 
            var sql = ""; 

            sql += "SELECT \n";
            sql += "    A.PESSOA_CONTATO_TIPO_ID,\n";
            sql += "    A.REGISTRO_SITUACAO_ID,\n";
            sql += "    A.REGISTRO_LOGIN_ID,\n";
            sql += "    A.NOME,\n";
            sql += "    A.DESCRICAO,\n";
            sql += "    A.DATA_INCLUSAO,\n";
            sql += "    A.DATA_ALTERACAO\n";
            sql += "FROM \n";
            sql += "    PESSOA_CONTATO_TIPO_TB A\n";

            return sql; 
        } 

        private string PrepararSelecaoSql(int? pessoaContatoTipoId, int? registroSituacaoId, int? registroLoginId)
		{ 
			var sql = ""; 

			if (pessoaContatoTipoId.HasValue)
				sql += "A.PESSOA_CONTATO_TIPO_ID = " + pessoaContatoTipoId.Value + "\n";

			if (registroSituacaoId.HasValue)
				sql += "A.REGISTRO_SITUACAO_ID = " + registroSituacaoId.Value + "\n";

			if (registroLoginId.HasValue)
				sql += "A.REGISTRO_LOGIN_ID = " + registroLoginId.Value + "\n";

			if (!pessoaContatoTipoId.HasValue)
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

        private string PrepararInsercaoSql(Entidade.Pessoa.Contato.Tipo.TipoItem tipoItem) 
        { 
            var sql = string.Empty; 

            sql += "INSERT INTO PESSOA_CONTATO_TIPO_TB(\n";
			sql += "    REGISTRO_LOGIN_ID,\n";

			sql += "    NOME,\n";

			sql += "    DESCRICAO,\n";

			sql = sql.Substring(0, sql.Length - 2) + "\n";

			sql += ") VALUES (\n";
			sql += "    " + tipoItem.RegistroLoginId.ToString() + ",\n";

			    sql += "    '" + tipoItem.Nome.Replace("'", "''") + "',\n";

			if (string.IsNullOrEmpty(tipoItem.Descricao))
			    sql += "    NULL,\n";
			else
			    sql += "    '" + tipoItem.Descricao.Replace("'", "''") + "',\n";

			sql = sql.Substring(0, sql.Length - 2) + "\n";

            sql += ");\n";

            return sql; 
        } 

        private string PrepararAtualizacaoSql(Entidade.Pessoa.Contato.Tipo.TipoItem tipoItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    PESSOA_CONTATO_TIPO_TB \n";
            sql += "SET\n";
			sql += "    REGISTRO_LOGIN_ID = " + tipoItem.RegistroLoginId.ToString() + ",\n"; 

			sql += "    NOME = '" + tipoItem.Nome.Replace("'", "''") + "',\n";

			if (string.IsNullOrEmpty(tipoItem.Descricao))
			    sql += "    DESCRICAO = NULL,\n";
			else
				sql += "    DESCRICAO = '" + tipoItem.Descricao.Replace("'", "''") + "',\n";

			sql += "    DATA_ALTERACAO = CURRENT_TIMESTAMP,\n";

			sql = sql.Substring(0, sql.Length - 2) + "\n";

            sql += "WHERE\n";
            sql += "    PESSOA_CONTATO_TIPO_ID = " + tipoItem.Id + "\n";
            return sql; 
        } 

        private string PrepararExclusaoSql(Entidade.Pessoa.Contato.Tipo.TipoItem tipoItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    PESSOA_CONTATO_TIPO_TB\n";
            sql += "SET\n";
            sql += "    REGISTRO_SITUACAO_ID = 3\n";
            sql += "WHERE\n";
            sql += "    PESSOA_CONTATO_TIPO_ID = " + tipoItem.Id + "\n";
            return sql; 
        } 

        private string PrepararInativacaoSql(Entidade.Pessoa.Contato.Tipo.TipoItem tipoItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    PESSOA_CONTATO_TIPO_TB\n";
            sql += "SET\n";
            sql += "    REGISTRO_SITUACAO_ID = 2\n";
            sql += "WHERE\n";
            sql += "    PESSOA_CONTATO_TIPO_ID = " + tipoItem.Id + "\n";
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
					sql += "    A.PESSOA_CONTATO_TIPO_ID = SCOPE_IDENTITY()\n";

					break;

				case Nemag.Database.Base.DATABASE_TIPO_ID.MYSQL:
					sql += "    A.PESSOA_CONTATO_TIPO_ID = LAST_INSERT_ID()\n";

					break;
			}

			return sql;
		}

		#endregion
    }
}
