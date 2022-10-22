using System;

namespace Nemag.Core.Filtro.Menu.Permissao.Login 
{ 
    public partial class LoginItem : _BaseItem 
    { 
        public int? RegistroLoginId { get; set; }

        public int? RegistroSituacaoId { get; set; }

        public DateTime? DataInclusaoInicial { get; set; }

        public DateTime? DataInclusaoFinal { get; set; }

        public DateTime? DataAlteracaoInicial { get; set; }

        public DateTime? DataAlteracaoFinal { get; set; }

        public int? MenuId { get; set; }

        public int? LoginId { get; set; } 
    } 
} 
