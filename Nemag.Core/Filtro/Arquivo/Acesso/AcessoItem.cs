using System;

namespace Nemag.Core.Filtro.Arquivo.Acesso 
{ 
    public partial class AcessoItem : _BaseItem 
    { 
        public int? RegistroSituacaoId { get; set; }

        public int? ArquivoId { get; set; }

        public int? RegistroLoginId { get; set; }

        public DateTime? DataInclusaoInicial { get; set; }

        public DateTime? DataInclusaoFinal { get; set; }

        public string Ip { get; set; }

        public DateTime? DataAlteracaoInicial { get; set; }

        public DateTime? DataAlteracaoFinal { get; set; } 
    } 
} 
