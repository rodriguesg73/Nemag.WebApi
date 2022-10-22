using System.Collections.Generic;

namespace Nemag.Core.Persistencia.Login
{
    public partial class LoginItem
    {
        #region Métodos Públicos

        public Entidade.Login.LoginItem CarregarItemPorUsuario(string loginUsuario)
        {
            var databaseItem = new Nemag.Database.DatabaseItem();

            var sql = PrepararSelecaoPersonalizadoSql(loginUsuario, null, null);

            return CarregarItem<Entidade.Login.LoginItem>(databaseItem, sql);
        }

        public List<Entidade.Login.LoginItem> CarregarListaPorAtribuicaoItem(int loginGrupoId, int loginPerfilId)
        {
            var databaseItem = new Nemag.Database.DatabaseItem();

            var sql = PrepararSelecaoPersonalizadoSql(string.Empty, loginGrupoId, loginPerfilId);

            return CarregarLista<Entidade.Login.LoginItem>(databaseItem, sql);
        }

        #endregion

        #region Métodos Privados

        private string PrepararSelecaoPersonalizadoSql(string usuario, int? loginGrupoId, int? loginPerfilId)
        {
            var sql = string.Empty;

            if (!string.IsNullOrEmpty(usuario))
                sql += "A.USUARIO = '" + usuario.Replace("'", "''") + "'\n";

            if (loginGrupoId.HasValue && loginPerfilId.HasValue)
            {
                sql += "C.LOGIN_GRUPO_ID = " + loginGrupoId + "\n";

                sql += "C.LOGIN_PERFIL_ID = '" + loginPerfilId + "'\n";

                sql += "A.LOGIN_SITUACAO_ID = 1\n";

                sql += "A.REGISTRO_SITUACAO_ID <> 3\n";
            }

            sql = sql.Substring(0, sql.Length - 1);

            sql = sql.Replace("\n", "\nAND ");

            if (loginGrupoId.HasValue && loginPerfilId.HasValue)
                sql = "INNER JOIN LOGIN_ATRIBUICAO_TB C ON C.LOGIN_ID = A.LOGIN_ID\n\t" + sql;

            sql = "WHERE\n\t" + sql;

            sql = this.PrepararSelecaoSql() + " " + sql;

            return sql;
        }

        #endregion
    }
}
