using System.Collections.Generic;

namespace Nemag.Core.Persistencia.Requisicao.Permissao.Atribuicao
{
    public partial class AtribuicaoItem
    { 
        #region Métodos Públicos 

        public List<Entidade.Requisicao.Permissao.Atribuicao.AtribuicaoItem> CarregarListaPorUrlDestino(string urlDestino)
        { 
            var sql = this.PrepararSelecaoPersonalizadoSql(urlDestino); 

            return base.CarregarLista<Entidade.Requisicao.Permissao.Atribuicao.AtribuicaoItem>(_databaseItem, sql); 
        } 

        #endregion 

        #region Métodos Privados 

        private string PrepararSelecaoPersonalizadoSql(string urlDestino)
		{ 
			var sql = "";

            if (!string.IsNullOrEmpty(urlDestino))
                sql += "A.URL_DESTINO = '" + urlDestino + "'\n";

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

        #endregion
    }
}