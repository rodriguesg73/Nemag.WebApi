using System.Collections.Generic;

namespace Nemag.Ferramenta.Core.Entidade.Tabela
{
    public class TabelaItem : _BaseItem
    {
        public string Nome { get; set; }

        public List<Coluna.ColunaItem> ColunaLista { get; set; }

        public List<Relacionamento.RelacionamentoItem> RelacionamentoLista { get; set; }

        public int RelacionamentoNivel { get; set; }

        public int Indice { get; set; }
    }
}
