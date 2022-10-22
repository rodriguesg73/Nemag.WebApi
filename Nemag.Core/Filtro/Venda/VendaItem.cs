using System;

namespace Nemag.Core.Filtro.Venda 
{ 
    public partial class VendaItem : _BaseItem 
    { 
        public DateTime? DataInclusaoInicial { get; set; }

        public DateTime? DataInclusaoFinal { get; set; }

        public DateTime? DataAlteracaoInicial { get; set; }

        public DateTime? DataAlteracaoFinal { get; set; }

        public int? RegistroSituacaoId { get; set; }

        public int? RegistroLoginId { get; set; }

        public int? ClienteId { get; set; }

        public decimal? ValorInicial { get; set; }

        public decimal? ValorFinal { get; set; } 
    } 
} 
