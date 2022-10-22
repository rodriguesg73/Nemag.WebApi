using System;

namespace Nemag.Core.Filtro.Produto 
{ 
    public partial class ProdutoItem : _BaseItem 
    { 
        public DateTime? DataInclusaoInicial { get; set; }

        public DateTime? DataInclusaoFinal { get; set; }

        public DateTime? DataAlteracaoInicial { get; set; }

        public DateTime? DataAlteracaoFinal { get; set; }

        public int? RegistroSituacaoId { get; set; }

        public int? RegistroLoginId { get; set; }

        public string Nome { get; set; }

        public string Descricao { get; set; }

        public decimal? ValorInicial { get; set; }

        public decimal? ValorFinal { get; set; }

        public string Codigo { get; set; }

        public int? ProdutoCategoriaId { get; set; } 
    } 
} 
