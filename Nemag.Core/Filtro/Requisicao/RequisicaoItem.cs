using System;

namespace Nemag.Core.Filtro.Requisicao 
{ 
    public partial class RequisicaoItem : _BaseItem 
    { 
        public DateTime? DataInclusaoInicial { get; set; }

        public DateTime? DataInclusaoFinal { get; set; }

        public DateTime? DataAlteracaoInicial { get; set; }

        public DateTime? DataAlteracaoFinal { get; set; }

        public int? RegistroSituacaoId { get; set; }

        public int? LoginAcessoId { get; set; }

        public string Ip { get; set; }

        public string UrlReferencia { get; set; }

        public string UrlOrigem { get; set; }

        public string UrlDestino { get; set; } 
    } 
} 
