using System.Collections.Generic;
using Nemag.Ferramenta.Core.Entidade.Tabela;
using Nemag.Ferramenta.Core.Entidade.Tabela.Coluna;

namespace Nemag.Ferramenta.Core.Interface.Codigo
{
    public interface ICodigoItem
    {
        List<TabelaItem> ObterTabelaLista();

        TabelaItem ObterTabelaItem(string tabelaNome);

        List<ColunaItem> ObterTabelaColunaLista(string tabelaNome);

        void AuditarTabelaItem(TabelaItem tabelaItem);

        string MontarClasseEntidadeItem(string namespaceClasse, TabelaItem tabelaItem);

        string MontarClasseFiltroItem(string namespaceClasse, TabelaItem tabelaItem);

        string MontarClasseInterfaceItem(string namespaceClasse, TabelaItem tabelaItem);

        string MontarClasseNegocioItem(string namespaceClasse, TabelaItem tabelaItem);

        string MontarClassePersistenciaItem(string namespaceClasse, string namespaceDatabase, TabelaItem tabelaItem);

        string MontarSqlCreateTable(TabelaItem tabelaItem, List<TabelaItem> tabelaLista);

        string ObterRegistroInicialLista(List<TabelaItem> tabelaLista);

        int IdentificarTabelaRelacionamentoNivel(TabelaItem tabelaItem, List<TabelaItem> tabelaLista);
    }
}
