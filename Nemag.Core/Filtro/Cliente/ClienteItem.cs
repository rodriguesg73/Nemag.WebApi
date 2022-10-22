using System;

namespace Nemag.Core.Filtro.Cliente 
{ 
    public partial class ClienteItem : _BaseItem 
    { 
        public DateTime? DataInclusaoInicial { get; set; }

        public DateTime? DataInclusaoFinal { get; set; }

        public DateTime? DataAlteracaoInicial { get; set; }

        public DateTime? DataAlteracaoFinal { get; set; }

        public int? RegistroSituacaoId { get; set; }

        public int? RegistroLoginId { get; set; }

        public int? PessoaId { get; set; }

        public string Nome { get; set; } 
    } 
} 
