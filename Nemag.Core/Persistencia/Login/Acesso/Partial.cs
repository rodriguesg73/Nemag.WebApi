using System;

namespace Nemag.Core.Persistencia.Login.Acesso
{
    public partial class AcessoItem
    {
        #region Métodos Públicos

        public Entidade.Login.Acesso.AcessoItem CarregarItemPorToken(string token)
        {
            var sql = PrepararSelecaoPersonalizadoSql(token, null, string.Empty, DateTime.MinValue, DateTime.MinValue);

            var retorno = CarregarItem<Entidade.Login.Acesso.AcessoItem>(_databaseItem, sql);

            return retorno;
        }

        public Entidade.Login.Acesso.AcessoItem CarregarItemValidoPorRegistroLoginId(int loginId, string ip)
        {
            var sql = PrepararSelecaoPersonalizadoSql(null, loginId, ip, DateTime.MinValue, DateTime.Now);

            var retorno = CarregarItem<Entidade.Login.Acesso.AcessoItem>(_databaseItem, sql);

            return retorno;
        }

        #endregion

        #region Métodos Privados

        private string PrepararSelecaoPersonalizadoSql(string token, int? registroLoginId, string ip, DateTime dataInclusao, DateTime dataValidade)
        {
            var sql = string.Empty;

            if (!string.IsNullOrEmpty(token))
                sql += "A.TOKEN = '" + token + "'\n";

            if (registroLoginId.HasValue)
                sql += "A.REGISTRO_LOGIN_ID = " + registroLoginId.Value + "\n";

            if (!string.IsNullOrEmpty(ip))
                sql += "A.IP = '" + ip + "'\n";

            if (dataInclusao > DateTime.MinValue)
                sql += "A.DATA_INCLUSAO = '" + string.Format("{0:dd-MM-yyyy HH:mm:ss}", dataInclusao) + "'\n";

            if (dataValidade > DateTime.MinValue)
                sql += "A.DATA_VALIDADE = '" + string.Format("{0:dd-MM-yyyy HH:mm:ss}", dataValidade) + "'\n";

            sql += "A.REGISTRO_SITUACAO_ID <> 3\n";

            if (!string.IsNullOrEmpty(sql))
            {
                sql = sql[0..^1];

                sql = sql.Replace("\n", "\nAND ");

                sql = "WHERE\n\t" + sql;
            }

            sql = PrepararSelecaoSql() + " " + sql;

            return sql;
        }

        #endregion
    }
}
