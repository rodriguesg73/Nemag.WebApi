using System.Collections.Generic;

namespace Nemag.Core.Persistencia.Pessoa.Contato
{
    public partial class ContatoItem : _BaseItem, Interface.Pessoa.Contato.IContatoItem
    {
        public List<Core.Entidade.Pessoa.Contato.ContatoItem> CarregarListaPorLoginId(int loginId)
        {
            var databaseItem = new Nemag.Database.DatabaseItem();

            var sql = PrepararSelecaContatoSql(loginId);

            return CarregarLista<Entidade.Pessoa.Contato.ContatoItem>(databaseItem, sql);
        }

        public Core.Entidade.Pessoa.Contato.ContatoItem CarregarItemPorLoginId(int loginId)
        {
            var databaseItem = new Nemag.Database.DatabaseItem();

            var sql = PrepararSelecaContatoSql(loginId);

            return CarregarItem<Entidade.Pessoa.Contato.ContatoItem>(databaseItem, sql);
        }

        private string PrepararSelecaContatoSql(int loginId)
        {
            var sql = "";

            sql += "B.LOGIN_ID = " + loginId + "\n";

            sql += "A.REGISTRO_SITUACAO_ID <> 3\n";

            sql = sql[0..^1];

            sql = sql.Replace("\n", "\nAND ");

            sql = "WHERE\n\t" + sql;

            sql = "INNER JOIN LOGIN_TB B ON B.PESSOA_ID = A.PESSOA_ID\n\t" + sql;

            sql = PrepararSelecaoSql() + " " + sql;

            return sql;
        }
    }
}
