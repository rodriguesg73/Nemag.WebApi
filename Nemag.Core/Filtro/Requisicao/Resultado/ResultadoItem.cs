using System;

namespace Nemag.Core.Filtro.Requisicao.Resultado 
{ 
    public partial class ResultadoItem : _BaseItem 
    { 
        public DateTime? DataInclusaoInicial { get; set; }

        public DateTime? DataInclusaoFinal { get; set; }

        public DateTime? DataAlteracaoInicial { get; set; }

        public DateTime? DataAlteracaoFinal { get; set; }

        public int? RegistroSituacaoId { get; set; }

        public int? RequisicaoId { get; set; }

        public string Conteudo { get; set; }

        public string Tipo { get; set; }

        public int? CodigoInicial { get; set; }

        public int? CodigoFinal { get; set; } 
    } 
} 
