namespace Nemag.Ferramenta.Core.Entidade.Tabela.Relacionamento
{
    public class RelacionamentoItem : _BaseItem
    {
        public string Nome { get; set; }

        public string TabelaNome { get; set; }

        public string ColunaNome { get; set; }

        public string TabelaReferenciaNome { get; set; }

        public string ColunaReferenciaNome { get; set; }
    }
}
