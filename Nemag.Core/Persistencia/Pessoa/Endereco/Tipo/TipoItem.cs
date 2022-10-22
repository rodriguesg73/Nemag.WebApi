using System;
using System.Collections.Generic;

namespace Nemag.Core.Persistencia.Pessoa.Endereco.Tipo 
{ 
    public partial class TipoItem : _BaseItem, Interface.Pessoa.Endereco.Tipo.ITipoItem
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

        public List<Entidade.Pessoa.Endereco.Tipo.TipoItem> CarregarLista() 
        { 
            var sql = this.PrepararSelecaoSql(null, null); 

            return base.CarregarLista<Entidade.Pessoa.Endereco.Tipo.TipoItem>(_databaseItem, sql); 
        } 

        public List<Entidade.Pessoa.Endereco.Tipo.TipoItem> CarregarListaPorRegistroSituacaoId(int registroSituacaoId) 
        { 
            var sql = this.PrepararSelecaoSql(null, registroSituacaoId); 

            return base.CarregarLista<Entidade.Pessoa.Endereco.Tipo.TipoItem>(_databaseItem, sql); 
        } 

        public Entidade.Pessoa.Endereco.Tipo.TipoItem CarregarItem(int pessoaEnderecoTipoId)
        {
            var sql = this.PrepararSelecaoSql(pessoaEnderecoTipoId, null); 

            var retorno = base.CarregarItem<Entidade.Pessoa.Endereco.Tipo.TipoItem>(_databaseItem, sql); 

            return retorno; 
        }

        public Entidade.Pessoa.Endereco.Tipo.TipoItem InserirItem(Entidade.Pessoa.Endereco.Tipo.TipoItem tipoItem)
        {
            var sql = this.PrepararInsercaoSql(tipoItem); 

            sql += this.ObterUltimoItemInseridoSql();

            tipoItem.Id = base.CarregarItem<Entidade.Pessoa.Endereco.Tipo.TipoItem>(_databaseItem, sql).Id;

            return tipoItem;
        } 

        public Entidade.Pessoa.Endereco.Tipo.TipoItem AtualizarItem(Entidade.Pessoa.Endereco.Tipo.TipoItem tipoItem)
        {
            var sql = this.PrepararAtualizacaoSql(tipoItem); 

            sql += this.PrepararSelecaoSql(tipoItem.Id, null);

            tipoItem.DataAlteracao = base.CarregarItem<Entidade.Pessoa.Endereco.Tipo.TipoItem>(_databaseItem, sql).DataAlteracao;

            return tipoItem;
        } 

        public Entidade.Pessoa.Endereco.Tipo.TipoItem ExcluirItem(Entidade.Pessoa.Endereco.Tipo.TipoItem tipoItem)
        {
            var sql = this.PrepararExclusaoSql(tipoItem); 

            return base.CarregarItem<Entidade.Pessoa.Endereco.Tipo.TipoItem>(_databaseItem, sql); 
        } 

        public Entidade.Pessoa.Endereco.Tipo.TipoItem InativarItem(Entidade.Pessoa.Endereco.Tipo.TipoItem tipoItem)
        {
            var sql = this.PrepararInativacaoSql(tipoItem); 

            return base.CarregarItem<Entidade.Pessoa.Endereco.Tipo.TipoItem>(_databaseItem, sql); 
        } 

        #endregion 

        #region Métodos Privados 

        private string PrepararSelecaoSql()
        { 
            var sql = ""; 

            sql += "SELECT \n";
            sql += "    A.PESSOA_ENDERECO_TIPO_ID,\n";
            sql += "    A.DATA_INCLUSAO,\n";
            sql += "    A.DATA_ALTERACAO,\n";
            sql += "    A.REGISTRO_SITUACAO_ID,\n";
            sql += "    A.NOME\n";
            sql += "FROM \n";
            sql += "    PESSOA_ENDERECO_TIPO_TB A\n";

            return sql; 
        } 

        private string PrepararSelecaoSql(int? pessoaEnderecoTipoId, int? registroSituacaoId)
		{ 
			var sql = ""; 

			if (pessoaEnderecoTipoId.HasValue)
				sql += "A.PESSOA_ENDERECO_TIPO_ID = " + pessoaEnderecoTipoId.Value + "\n";

			if (registroSituacaoId.HasValue)
				sql += "A.REGISTRO_SITUACAO_ID = " + registroSituacaoId.Value + "\n";

			if (!pessoaEnderecoTipoId.HasValue)
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

        private string PrepararInsercaoSql(Entidade.Pessoa.Endereco.Tipo.TipoItem tipoItem) 
        { 
            var sql = string.Empty; 

            sql += "INSERT INTO PESSOA_ENDERECO_TIPO_TB(\n";
			sql += "    NOME,\n";

			sql = sql.Substring(0, sql.Length - 2) + "\n";

			sql += ") VALUES (\n";
			    sql += "    '" + tipoItem.Nome.Replace("'", "''") + "',\n";

			sql = sql.Substring(0, sql.Length - 2) + "\n";

            sql += ");\n";

            return sql; 
        } 

        private string PrepararAtualizacaoSql(Entidade.Pessoa.Endereco.Tipo.TipoItem tipoItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    PESSOA_ENDERECO_TIPO_TB \n";
            sql += "SET\n";
			sql += "    DATA_ALTERACAO = CURRENT_TIMESTAMP,\n";

			sql += "    NOME = '" + tipoItem.Nome.Replace("'", "''") + "',\n";

			sql = sql.Substring(0, sql.Length - 2) + "\n";

            sql += "WHERE\n";
            sql += "    PESSOA_ENDERECO_TIPO_ID = " + tipoItem.Id + "\n";
            return sql; 
        } 

        private string PrepararExclusaoSql(Entidade.Pessoa.Endereco.Tipo.TipoItem tipoItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    PESSOA_ENDERECO_TIPO_TB\n";
            sql += "SET\n";
            sql += "    REGISTRO_SITUACAO_ID = 3\n";
            sql += "WHERE\n";
            sql += "    PESSOA_ENDERECO_TIPO_ID = " + tipoItem.Id + "\n";
            return sql; 
        } 

        private string PrepararInativacaoSql(Entidade.Pessoa.Endereco.Tipo.TipoItem tipoItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    PESSOA_ENDERECO_TIPO_TB\n";
            sql += "SET\n";
            sql += "    REGISTRO_SITUACAO_ID = 2\n";
            sql += "WHERE\n";
            sql += "    PESSOA_ENDERECO_TIPO_ID = " + tipoItem.Id + "\n";
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
					sql += "    A.PESSOA_ENDERECO_TIPO_ID = SCOPE_IDENTITY()\n";

					break;

				case Nemag.Database.Base.DATABASE_TIPO_ID.MYSQL:
					sql += "    A.PESSOA_ENDERECO_TIPO_ID = LAST_INSERT_ID()\n";

					break;
			}

			return sql;
		}

		#endregion
    }
}
