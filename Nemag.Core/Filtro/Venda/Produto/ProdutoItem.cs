using System;

namespace Nemag.Core.Filtro.Venda.Produto 
{ 
    public partial class ProdutoItem : _BaseItem 
    { 
        public DateTime? DataInclusaoInicial { get; set; }

        public DateTime? DataInclusaoFinal { get; set; }

        public DateTime? DataAlteracaoInicial { get; set; }

        public DateTime? DataAlteracaoFinal { get; set; }

        public int? RegistroSituacaoId { get; set; }

        public int? RegistroLoginId { get; set; }

        public int? ProdutoId { get; set; }

        public int? VendaId { get; set; } 
    } 
} 
