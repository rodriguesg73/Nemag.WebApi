using System;

namespace Nemag.Core.Filtro.Arquivo.Tramitador 
{ 
    public partial class TramitadorItem : _BaseItem 
    { 
        public DateTime? DataInclusaoInicial { get; set; }

        public DateTime? DataInclusaoFinal { get; set; }

        public DateTime? DataAlteracaoInicial { get; set; }

        public DateTime? DataAlteracaoFinal { get; set; }

        public int? RegistroSituacaoId { get; set; }

        public int? RegistroLoginId { get; set; }

        public string Nome { get; set; }

        public string Descricao { get; set; } 
    } 
} 
