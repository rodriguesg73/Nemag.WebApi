using System;

namespace Nemag.Core.Filtro.Arquivo.Tramitador.Diretorio 
{ 
    public partial class DiretorioItem : _BaseItem 
    { 
        public DateTime? DataInclusaoInicial { get; set; }

        public DateTime? DataInclusaoFinal { get; set; }

        public DateTime? DataAlteracaoInicial { get; set; }

        public DateTime? DataAlteracaoFinal { get; set; }

        public int? RegistroSituacaoId { get; set; }

        public int? RegistroLoginId { get; set; }

        public int? ArquivoTramitadorId { get; set; }

        public int? ArquivoTramitadorAcaoId { get; set; }

        public string DiretorioUrl { get; set; } 
    } 
} 
