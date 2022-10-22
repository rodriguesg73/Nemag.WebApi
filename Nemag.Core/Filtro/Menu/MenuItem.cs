using System;

namespace Nemag.Core.Filtro.Menu 
{ 
    public partial class MenuItem : _BaseItem 
    { 
        public int? RegistroSituacaoId { get; set; }

        public int? RegistroLoginId { get; set; }

        public int? MenuSuperiorIdInicial { get; set; }

        public int? MenuSuperiorIdFinal { get; set; }

        public string Titulo { get; set; }

        public string Descricao { get; set; }

        public string WebUrl { get; set; }

        public string IconeCss { get; set; }

        public DateTime? DataInclusaoInicial { get; set; }

        public DateTime? DataInclusaoFinal { get; set; }

        public DateTime? DataAlteracaoInicial { get; set; }

        public DateTime? DataAlteracaoFinal { get; set; } 
    } 
} 
