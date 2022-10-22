using System;

namespace Nemag.Core.Filtro.Arquivo 
{ 
    public partial class ArquivoItem : _BaseItem 
    { 
        public int? RegistroSituacaoId { get; set; }

        public int? RegistroLoginId { get; set; }

        public DateTime? DataInclusaoInicial { get; set; }

        public DateTime? DataInclusaoFinal { get; set; }

        public DateTime? DataAlteracaoInicial { get; set; }

        public DateTime? DataAlteracaoFinal { get; set; }

        public string Nome { get; set; }

        public string Descricao { get; set; }

        public string DiretorioLocalUrl { get; set; }

        public string Guid { get; set; }

        public string Checksun { get; set; } 
    } 
} 
