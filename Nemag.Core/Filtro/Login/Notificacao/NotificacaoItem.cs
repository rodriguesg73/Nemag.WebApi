using System;

namespace Nemag.Core.Filtro.Login.Notificacao 
{ 
    public partial class NotificacaoItem : _BaseItem 
    { 
        public DateTime? DataInclusaoInicial { get; set; }

        public DateTime? DataInclusaoFinal { get; set; }

        public DateTime? DataAlteracaoInicial { get; set; }

        public DateTime? DataAlteracaoFinal { get; set; }

        public int? RegistroSituacaoId { get; set; }

        public int? RegistroLoginId { get; set; }

        public int? LoginId { get; set; }

        public int? LoginNotificacaoTipoId { get; set; }

        public string Titulo { get; set; }

        public string Conteudo { get; set; }

        public string LinkUrl { get; set; } 
    } 
} 
