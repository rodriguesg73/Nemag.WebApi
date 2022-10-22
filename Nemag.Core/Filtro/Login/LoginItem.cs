using System;

namespace Nemag.Core.Filtro.Login 
{ 
    public partial class LoginItem : _BaseItem 
    { 
        public int? RegistroSituacaoId { get; set; }

        public int? PessoaId { get; set; }

        public string Usuario { get; set; }

        public string Senha { get; set; }

        public DateTime? DataInclusaoInicial { get; set; }

        public DateTime? DataInclusaoFinal { get; set; }

        public DateTime? DataAlteracaoInicial { get; set; }

        public DateTime? DataAlteracaoFinal { get; set; }

        public int? LoginSituacaoId { get; set; }

        public string NomeExibicao { get; set; } 
    } 
} 
