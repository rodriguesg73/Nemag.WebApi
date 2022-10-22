namespace Nemag.Ferramenta.Core.Entidade.Tabela.Coluna
{
    public class ColunaItem : _BaseItem
    {
        public int Index { get; set; }

        public string NomeOriginal { get; set; }

        public string NomeExibicao { get; set; }

        public string Tipo { get; set; }

        public string TextoTamanho { get; set; }

        public int NumeroTamanho { get; set; }

        public int NumeroEscala { get; set; }

        public bool ChavePrimaria { get; set; }

        public bool ChaveEstrangeira { get; set; }

        public bool AutoIncremental { get; set; }

        public bool Nulavel { get; set; }

        public string ValorPadrao { get; set; }
    }
}
