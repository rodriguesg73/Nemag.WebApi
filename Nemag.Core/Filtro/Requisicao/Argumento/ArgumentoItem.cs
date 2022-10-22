using System;

namespace Nemag.Core.Filtro.Requisicao.Argumento 
{ 
    public partial class ArgumentoItem : _BaseItem 
    { 
        public DateTime? DataInclusaoInicial { get; set; }

        public DateTime? DataInclusaoFinal { get; set; }

        public DateTime? DataAlteracaoInicial { get; set; }

        public DateTime? DataAlteracaoFinal { get; set; }

        public int? RegistroSituacaoId { get; set; }

        public int? RequisicaoId { get; set; }

        public string Nome { get; set; }

        public string Valor { get; set; } 
    } 
} 
