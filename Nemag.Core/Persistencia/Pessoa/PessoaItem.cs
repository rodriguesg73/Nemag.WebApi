using System;
using System.Collections.Generic;

namespace Nemag.Core.Persistencia.Pessoa 
{ 
    public partial class PessoaItem : _BaseItem, Interface.Pessoa.IPessoaItem
    { 
        #region Propriedades

        private Nemag.Database.DatabaseItem _databaseItem { get; set; }

        #endregion

        #region Construtores

        public PessoaItem() : this(new Nemag.Database.DatabaseItem())
        { }

        public PessoaItem(Nemag.Database.DatabaseItem databaseItem)
        {
	            _databaseItem = databaseItem;
        }

        #endregion

        #region Métodos Públicos 

        public List<Entidade.Pessoa.PessoaItem> CarregarLista() 
        { 
            var sql = this.PrepararSelecaoSql(null, null); 

            return base.CarregarLista<Entidade.Pessoa.PessoaItem>(_databaseItem, sql); 
        } 

        public List<Entidade.Pessoa.PessoaItem> CarregarListaPorRegistroSituacaoId(int registroSituacaoId) 
        { 
            var sql = this.PrepararSelecaoSql(null, registroSituacaoId); 

            return base.CarregarLista<Entidade.Pessoa.PessoaItem>(_databaseItem, sql); 
        } 

        public Entidade.Pessoa.PessoaItem CarregarItem(int pessoaId)
        {
            var sql = this.PrepararSelecaoSql(pessoaId, null); 

            var retorno = base.CarregarItem<Entidade.Pessoa.PessoaItem>(_databaseItem, sql); 

            return retorno; 
        }

        public Entidade.Pessoa.PessoaItem InserirItem(Entidade.Pessoa.PessoaItem pessoaItem)
        {
            var sql = this.PrepararInsercaoSql(pessoaItem); 

            sql += this.ObterUltimoItemInseridoSql();

            pessoaItem.Id = base.CarregarItem<Entidade.Pessoa.PessoaItem>(_databaseItem, sql).Id;

            return pessoaItem;
        } 

        public Entidade.Pessoa.PessoaItem AtualizarItem(Entidade.Pessoa.PessoaItem pessoaItem)
        {
            var sql = this.PrepararAtualizacaoSql(pessoaItem); 

            sql += this.PrepararSelecaoSql(pessoaItem.Id, null);

            pessoaItem.DataAlteracao = base.CarregarItem<Entidade.Pessoa.PessoaItem>(_databaseItem, sql).DataAlteracao;

            return pessoaItem;
        } 

        public Entidade.Pessoa.PessoaItem ExcluirItem(Entidade.Pessoa.PessoaItem pessoaItem)
        {
            var sql = this.PrepararExclusaoSql(pessoaItem); 

            return base.CarregarItem<Entidade.Pessoa.PessoaItem>(_databaseItem, sql); 
        } 

        public Entidade.Pessoa.PessoaItem InativarItem(Entidade.Pessoa.PessoaItem pessoaItem)
        {
            var sql = this.PrepararInativacaoSql(pessoaItem); 

            return base.CarregarItem<Entidade.Pessoa.PessoaItem>(_databaseItem, sql); 
        } 

        #endregion 

        #region Métodos Privados 

        private string PrepararSelecaoSql()
        { 
            var sql = ""; 

            sql += "SELECT \n";
            sql += "    A.PESSOA_ID,\n";
            sql += "    A.REGISTRO_SITUACAO_ID,\n";
            sql += "    A.NOME,\n";
            sql += "    A.DATA_INCLUSAO,\n";
            sql += "    A.DATA_ALTERACAO\n";
            sql += "FROM \n";
            sql += "    PESSOA_TB A\n";

            return sql; 
        } 

        private string PrepararSelecaoSql(int? pessoaId, int? registroSituacaoId)
		{ 
			var sql = ""; 

			if (pessoaId.HasValue)
				sql += "A.PESSOA_ID = " + pessoaId.Value + "\n";

			if (registroSituacaoId.HasValue)
				sql += "A.REGISTRO_SITUACAO_ID = " + registroSituacaoId.Value + "\n";

			if (!pessoaId.HasValue)
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

        private string PrepararInsercaoSql(Entidade.Pessoa.PessoaItem pessoaItem) 
        { 
            var sql = string.Empty; 

            sql += "INSERT INTO PESSOA_TB(\n";
			sql += "    NOME,\n";

			sql = sql.Substring(0, sql.Length - 2) + "\n";

			sql += ") VALUES (\n";
			    sql += "    '" + pessoaItem.Nome.Replace("'", "''") + "',\n";

			sql = sql.Substring(0, sql.Length - 2) + "\n";

            sql += ");\n";

            return sql; 
        } 

        private string PrepararAtualizacaoSql(Entidade.Pessoa.PessoaItem pessoaItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    PESSOA_TB \n";
            sql += "SET\n";
			sql += "    NOME = '" + pessoaItem.Nome.Replace("'", "''") + "',\n";

			sql += "    DATA_ALTERACAO = CURRENT_TIMESTAMP,\n";

			sql = sql.Substring(0, sql.Length - 2) + "\n";

            sql += "WHERE\n";
            sql += "    PESSOA_ID = " + pessoaItem.Id + "\n";
            return sql; 
        } 

        private string PrepararExclusaoSql(Entidade.Pessoa.PessoaItem pessoaItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    PESSOA_TB\n";
            sql += "SET\n";
            sql += "    REGISTRO_SITUACAO_ID = 3\n";
            sql += "WHERE\n";
            sql += "    PESSOA_ID = " + pessoaItem.Id + "\n";
            return sql; 
        } 

        private string PrepararInativacaoSql(Entidade.Pessoa.PessoaItem pessoaItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    PESSOA_TB\n";
            sql += "SET\n";
            sql += "    REGISTRO_SITUACAO_ID = 2\n";
            sql += "WHERE\n";
            sql += "    PESSOA_ID = " + pessoaItem.Id + "\n";
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
					sql += "    A.PESSOA_ID = SCOPE_IDENTITY()\n";

					break;

				case Nemag.Database.Base.DATABASE_TIPO_ID.MYSQL:
					sql += "    A.PESSOA_ID = LAST_INSERT_ID()\n";

					break;
			}

			return sql;
		}

		#endregion
    }
}
