using System.Collections.Generic;

namespace Nemag.Core.Persistencia.Menu
{
    public partial class MenuItem : _BaseItem, Interface.Menu.IMenuItem
    {
        #region Métodos Públicos 

        public List<Entidade.Menu.MenuItem> CarregarListaPorLoginId(int loginId)
        {
            var sql = this.PrepararSelecaoPersonalizadoSql(loginId);

            return base.CarregarLista<Entidade.Menu.MenuItem>(_databaseItem, sql);
        }

        #endregion

        #region Métodos Privados 

        private string PrepararSelecaoPersonalizadoSql(int? loginId)
        {
            var sql = "";

            if (loginId.HasValue)
                sql += "C.LOGIN_ID = " + loginId.Value + "\n";

            sql += "A.REGISTRO_SITUACAO_ID <> 3\n";

            if (!string.IsNullOrEmpty(sql))
            {
                sql = sql.Substring(0, sql.Length - 1);

                sql = sql.Replace("\n", "\nAND ");

                sql = "WHERE\n\t" + sql;
            }

            var sqlFinal = PrepararSelecaoSql();

            if (loginId.HasValue)
            {
                sqlFinal = "SELECT DISTINCT" + sqlFinal.Substring("SELECT".Length, sqlFinal.Length - "SELECT".Length - 1);

                sqlFinal += "    INNER JOIN MENU_PERMISSAO_ATRIBUICAO_TB B ON B.MENU_ID = A.MENU_ID AND B.REGISTRO_SITUACAO_ID = 1\n";
                sqlFinal += "    INNER JOIN LOGIN_ATRIBUICAO_TB C ON C.LOGIN_GRUPO_ID = B.LOGIN_GRUPO_ID AND C.LOGIN_PERFIL_ID = B.LOGIN_PERFIL_ID AND C.REGISTRO_SITUACAO_ID = 1\n";
            }

            sqlFinal += " " + sql;

            return sqlFinal;
        }

        #endregion
    }
}
