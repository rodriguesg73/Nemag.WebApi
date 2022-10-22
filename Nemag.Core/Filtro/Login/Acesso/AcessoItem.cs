using System;

namespace Nemag.Core.Filtro.Login.Acesso 
{ 
    public partial class AcessoItem : _BaseItem 
    { 
        public int? RegistroSituacaoId { get; set; }

        public int? RegistroLoginId { get; set; }

        public string Token { get; set; }

        public string Ip { get; set; }

        public DateTime? DataInclusaoInicial { get; set; }

        public DateTime? DataInclusaoFinal { get; set; }

        public DateTime? DataValidadeInicial { get; set; }

        public DateTime? DataValidadeFinal { get; set; }

        public DateTime? DataAlteracaoInicial { get; set; }

        public DateTime? DataAlteracaoFinal { get; set; }

        public int? LoginId { get; set; } 
    } 
} 
