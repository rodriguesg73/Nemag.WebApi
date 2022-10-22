using System;
using System.Collections.Generic;

namespace Nemag.Core.Persistencia.Pessoa.Endereco 
{ 
    public partial class EnderecoItem : _BaseItem, Interface.Pessoa.Endereco.IEnderecoItem
    { 
        #region Propriedades

        private Nemag.Database.DatabaseItem _databaseItem { get; set; }

        #endregion

        #region Construtores

        public EnderecoItem() : this(new Nemag.Database.DatabaseItem())
        { }

        public EnderecoItem(Nemag.Database.DatabaseItem databaseItem)
        {
	            _databaseItem = databaseItem;
        }

        #endregion

        #region Métodos Públicos 

        public List<Entidade.Pessoa.Endereco.EnderecoItem> CarregarLista() 
        { 
            var sql = this.PrepararSelecaoSql(null, null, null, null, null); 

            return base.CarregarLista<Entidade.Pessoa.Endereco.EnderecoItem>(_databaseItem, sql); 
        } 

        public List<Entidade.Pessoa.Endereco.EnderecoItem> CarregarListaPorRegistroSituacaoId(int registroSituacaoId) 
        { 
            var sql = this.PrepararSelecaoSql(null, registroSituacaoId, null, null, null); 

            return base.CarregarLista<Entidade.Pessoa.Endereco.EnderecoItem>(_databaseItem, sql); 
        } 

        public List<Entidade.Pessoa.Endereco.EnderecoItem> CarregarListaPorRegistroLoginId(int registroLoginId) 
        { 
            var sql = this.PrepararSelecaoSql(null, null, registroLoginId, null, null); 

            return base.CarregarLista<Entidade.Pessoa.Endereco.EnderecoItem>(_databaseItem, sql); 
        } 

        public List<Entidade.Pessoa.Endereco.EnderecoItem> CarregarListaPorPessoaId(int pessoaId) 
        { 
            var sql = this.PrepararSelecaoSql(null, null, null, pessoaId, null); 

            return base.CarregarLista<Entidade.Pessoa.Endereco.EnderecoItem>(_databaseItem, sql); 
        } 

        public List<Entidade.Pessoa.Endereco.EnderecoItem> CarregarListaPorPessoaEnderecoTipoId(int pessoaEnderecoTipoId) 
        { 
            var sql = this.PrepararSelecaoSql(null, null, null, null, pessoaEnderecoTipoId); 

            return base.CarregarLista<Entidade.Pessoa.Endereco.EnderecoItem>(_databaseItem, sql); 
        } 

        public Entidade.Pessoa.Endereco.EnderecoItem CarregarItem(int pessoaEnderecoId)
        {
            var sql = this.PrepararSelecaoSql(pessoaEnderecoId, null, null, null, null); 

            var retorno = base.CarregarItem<Entidade.Pessoa.Endereco.EnderecoItem>(_databaseItem, sql); 

            return retorno; 
        }

        public Entidade.Pessoa.Endereco.EnderecoItem InserirItem(Entidade.Pessoa.Endereco.EnderecoItem enderecoItem)
        {
            var sql = this.PrepararInsercaoSql(enderecoItem); 

            sql += this.ObterUltimoItemInseridoSql();

            enderecoItem.Id = base.CarregarItem<Entidade.Pessoa.Endereco.EnderecoItem>(_databaseItem, sql).Id;

            return enderecoItem;
        } 

        public Entidade.Pessoa.Endereco.EnderecoItem AtualizarItem(Entidade.Pessoa.Endereco.EnderecoItem enderecoItem)
        {
            var sql = this.PrepararAtualizacaoSql(enderecoItem); 

            sql += this.PrepararSelecaoSql(enderecoItem.Id, null, null, null, null);

            enderecoItem.DataAlteracao = base.CarregarItem<Entidade.Pessoa.Endereco.EnderecoItem>(_databaseItem, sql).DataAlteracao;

            return enderecoItem;
        } 

        public Entidade.Pessoa.Endereco.EnderecoItem ExcluirItem(Entidade.Pessoa.Endereco.EnderecoItem enderecoItem)
        {
            var sql = this.PrepararExclusaoSql(enderecoItem); 

            return base.CarregarItem<Entidade.Pessoa.Endereco.EnderecoItem>(_databaseItem, sql); 
        } 

        public Entidade.Pessoa.Endereco.EnderecoItem InativarItem(Entidade.Pessoa.Endereco.EnderecoItem enderecoItem)
        {
            var sql = this.PrepararInativacaoSql(enderecoItem); 

            return base.CarregarItem<Entidade.Pessoa.Endereco.EnderecoItem>(_databaseItem, sql); 
        } 

        #endregion 

        #region Métodos Privados 

        private string PrepararSelecaoSql()
        { 
            var sql = ""; 

            sql += "SELECT \n";
            sql += "    A.PESSOA_ENDERECO_ID,\n";
            sql += "    A.DATA_INCLUSAO,\n";
            sql += "    A.DATA_ALTERACAO,\n";
            sql += "    A.REGISTRO_SITUACAO_ID,\n";
            sql += "    A.REGISTRO_LOGIN_ID,\n";
            sql += "    A.PESSOA_ID,\n";
            sql += "    A.PESSOA_ENDERECO_TIPO_ID,\n";
            sql += "    A.LOGRADOURO,\n";
            sql += "    A.NUMERO,\n";
            sql += "    A.COMPLEMENTO,\n";
            sql += "    A.BAIRRO_NOME,\n";
            sql += "    A.CIDADE_NOME,\n";
            sql += "    A.ESTADO_SIGLA,\n";
            sql += "    A.CEP,\n";
            sql += "    A1.PESSOA_ID AS PESSOA_ID,\n";
            sql += "    A1.NOME AS NOME,\n";
            sql += "    A2.PESSOA_ENDERECO_TIPO_ID AS PESSOA_ENDERECO_TIPO_ID,\n";
            sql += "    A2.NOME AS PESSOA_ENDERECO_TIPO_NOME\n";
            sql += "FROM \n";
            sql += "    PESSOA_ENDERECO_TB A\n";
            sql += "    INNER JOIN PESSOA_TB A1 ON A1.PESSOA_ID = A.PESSOA_ID\n";
            sql += "    INNER JOIN PESSOA_ENDERECO_TIPO_TB A2 ON A2.PESSOA_ENDERECO_TIPO_ID = A.PESSOA_ENDERECO_TIPO_ID\n";

            return sql; 
        } 

        private string PrepararSelecaoSql(int? pessoaEnderecoId, int? registroSituacaoId, int? registroLoginId, int? pessoaId, int? pessoaEnderecoTipoId)
		{ 
			var sql = ""; 

			if (pessoaEnderecoId.HasValue)
				sql += "A.PESSOA_ENDERECO_ID = " + pessoaEnderecoId.Value + "\n";

			if (registroSituacaoId.HasValue)
				sql += "A.REGISTRO_SITUACAO_ID = " + registroSituacaoId.Value + "\n";

			if (registroLoginId.HasValue)
				sql += "A.REGISTRO_LOGIN_ID = " + registroLoginId.Value + "\n";

			if (pessoaId.HasValue)
				sql += "A.PESSOA_ID = " + pessoaId.Value + "\n";

			if (pessoaEnderecoTipoId.HasValue)
				sql += "A.PESSOA_ENDERECO_TIPO_ID = " + pessoaEnderecoTipoId.Value + "\n";

			if (!pessoaEnderecoId.HasValue)
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

        private string PrepararInsercaoSql(Entidade.Pessoa.Endereco.EnderecoItem enderecoItem) 
        { 
            var sql = string.Empty; 

            sql += "INSERT INTO PESSOA_ENDERECO_TB(\n";
			sql += "    REGISTRO_LOGIN_ID,\n";

			sql += "    PESSOA_ID,\n";

			sql += "    PESSOA_ENDERECO_TIPO_ID,\n";

			sql += "    LOGRADOURO,\n";

			sql += "    NUMERO,\n";

			sql += "    COMPLEMENTO,\n";

			sql += "    BAIRRO_NOME,\n";

			sql += "    CIDADE_NOME,\n";

			sql += "    ESTADO_SIGLA,\n";

			sql += "    CEP,\n";

			sql = sql.Substring(0, sql.Length - 2) + "\n";

			sql += ") VALUES (\n";
			sql += "    " + enderecoItem.RegistroLoginId.ToString() + ",\n";

			sql += "    " + enderecoItem.PessoaId.ToString() + ",\n";

			sql += "    " + enderecoItem.PessoaEnderecoTipoId.ToString() + ",\n";

			    sql += "    '" + enderecoItem.Logradouro.Replace("'", "''") + "',\n";

			if (string.IsNullOrEmpty(enderecoItem.Numero))
			    sql += "    NULL,\n";
			else
			    sql += "    '" + enderecoItem.Numero.Replace("'", "''") + "',\n";

			if (string.IsNullOrEmpty(enderecoItem.Complemento))
			    sql += "    NULL,\n";
			else
			    sql += "    '" + enderecoItem.Complemento.Replace("'", "''") + "',\n";

			if (string.IsNullOrEmpty(enderecoItem.BairroNome))
			    sql += "    NULL,\n";
			else
			    sql += "    '" + enderecoItem.BairroNome.Replace("'", "''") + "',\n";

			    sql += "    '" + enderecoItem.CidadeNome.Replace("'", "''") + "',\n";

			    sql += "    '" + enderecoItem.EstadoSigla.Replace("'", "''") + "',\n";

			    sql += "    '" + enderecoItem.Cep.Replace("'", "''") + "',\n";

			sql = sql.Substring(0, sql.Length - 2) + "\n";

            sql += ");\n";

            return sql; 
        } 

        private string PrepararAtualizacaoSql(Entidade.Pessoa.Endereco.EnderecoItem enderecoItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    PESSOA_ENDERECO_TB \n";
            sql += "SET\n";
			sql += "    DATA_ALTERACAO = CURRENT_TIMESTAMP,\n";

			sql += "    REGISTRO_LOGIN_ID = " + enderecoItem.RegistroLoginId.ToString() + ",\n"; 

			sql += "    PESSOA_ID = " + enderecoItem.PessoaId.ToString() + ",\n"; 

			sql += "    PESSOA_ENDERECO_TIPO_ID = " + enderecoItem.PessoaEnderecoTipoId.ToString() + ",\n"; 

			sql += "    LOGRADOURO = '" + enderecoItem.Logradouro.Replace("'", "''") + "',\n";

			if (string.IsNullOrEmpty(enderecoItem.Numero))
			    sql += "    NUMERO = NULL,\n";
			else
				sql += "    NUMERO = '" + enderecoItem.Numero.Replace("'", "''") + "',\n";

			if (string.IsNullOrEmpty(enderecoItem.Complemento))
			    sql += "    COMPLEMENTO = NULL,\n";
			else
				sql += "    COMPLEMENTO = '" + enderecoItem.Complemento.Replace("'", "''") + "',\n";

			if (string.IsNullOrEmpty(enderecoItem.BairroNome))
			    sql += "    BAIRRO_NOME = NULL,\n";
			else
				sql += "    BAIRRO_NOME = '" + enderecoItem.BairroNome.Replace("'", "''") + "',\n";

			sql += "    CIDADE_NOME = '" + enderecoItem.CidadeNome.Replace("'", "''") + "',\n";

			sql += "    ESTADO_SIGLA = '" + enderecoItem.EstadoSigla.Replace("'", "''") + "',\n";

			sql += "    CEP = '" + enderecoItem.Cep.Replace("'", "''") + "',\n";

			sql = sql.Substring(0, sql.Length - 2) + "\n";

            sql += "WHERE\n";
            sql += "    PESSOA_ENDERECO_ID = " + enderecoItem.Id + "\n";
            return sql; 
        } 

        private string PrepararExclusaoSql(Entidade.Pessoa.Endereco.EnderecoItem enderecoItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    PESSOA_ENDERECO_TB\n";
            sql += "SET\n";
            sql += "    REGISTRO_SITUACAO_ID = 3\n";
            sql += "WHERE\n";
            sql += "    PESSOA_ENDERECO_ID = " + enderecoItem.Id + "\n";
            return sql; 
        } 

        private string PrepararInativacaoSql(Entidade.Pessoa.Endereco.EnderecoItem enderecoItem) 
        { 
            var sql = string.Empty; 

            sql += "UPDATE \n";
            sql += "    PESSOA_ENDERECO_TB\n";
            sql += "SET\n";
            sql += "    REGISTRO_SITUACAO_ID = 2\n";
            sql += "WHERE\n";
            sql += "    PESSOA_ENDERECO_ID = " + enderecoItem.Id + "\n";
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
					sql += "    A.PESSOA_ENDERECO_ID = SCOPE_IDENTITY()\n";

					break;

				case Nemag.Database.Base.DATABASE_TIPO_ID.MYSQL:
					sql += "    A.PESSOA_ENDERECO_ID = LAST_INSERT_ID()\n";

					break;
			}

			return sql;
		}

		#endregion
    }
}
