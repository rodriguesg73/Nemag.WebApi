using System;

namespace Nemag.Core.Entidade.Login 
{ 
    public partial class LoginItem : _BaseItem 
    { 
        public int RegistroSituacaoId { get; set; } 

        public int PessoaId { get; set; } 

        public string Usuario { get; set; } 

        public string Senha { get; set; } 

        public DateTime DataInclusao { get; set; } 

        public DateTime DataAlteracao { get; set; } 

        public int LoginSituacaoId { get; set; } 

        public string NomeExibicao { get; set; } 

        public string Nome { get; set; } 

        public string LoginSituacaoNome { get; set; } 
    } 
} 
