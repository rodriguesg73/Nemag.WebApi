using System;

namespace Nemag.Core.Filtro.Registro.Situacao 
{ 
    public partial class SituacaoItem : _BaseItem 
    { 
        public DateTime? DataInclusaoInicial { get; set; }

        public DateTime? DataInclusaoFinal { get; set; }

        public DateTime? DataAlteracaoInicial { get; set; }

        public DateTime? DataAlteracaoFinal { get; set; }

        public string Nome { get; set; } 
    } 
} 
