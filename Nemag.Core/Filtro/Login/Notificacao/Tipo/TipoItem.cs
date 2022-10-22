using System;

namespace Nemag.Core.Filtro.Login.Notificacao.Tipo 
{ 
    public partial class TipoItem : _BaseItem 
    { 
        public DateTime? DataInclusaoInicial { get; set; }

        public DateTime? DataInclusaoFinal { get; set; }

        public DateTime? DataAlteracaoInicial { get; set; }

        public DateTime? DataAlteracaoFinal { get; set; }

        public int? RegistroSituacaoId { get; set; }

        public int? RegistroLoginId { get; set; }

        public string Nome { get; set; }

        public string IconeCss { get; set; } 
    } 
} 
