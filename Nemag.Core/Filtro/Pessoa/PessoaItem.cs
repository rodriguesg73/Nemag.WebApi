using System;

namespace Nemag.Core.Filtro.Pessoa 
{ 
    public partial class PessoaItem : _BaseItem 
    { 
        public int? RegistroSituacaoId { get; set; }

        public string Nome { get; set; }

        public DateTime? DataInclusaoInicial { get; set; }

        public DateTime? DataInclusaoFinal { get; set; }

        public DateTime? DataAlteracaoInicial { get; set; }

        public DateTime? DataAlteracaoFinal { get; set; } 
    } 
} 
