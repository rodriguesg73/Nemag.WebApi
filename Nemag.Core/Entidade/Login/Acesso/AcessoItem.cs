using System;

namespace Nemag.Core.Entidade.Login.Acesso 
{ 
    public partial class AcessoItem : _BaseItem 
    { 
        public int RegistroSituacaoId { get; set; } 

        public int RegistroLoginId { get; set; } 

        public string Token { get; set; } 

        public string Ip { get; set; } 

        public DateTime DataInclusao { get; set; } 

        public DateTime DataValidade { get; set; } 

        public DateTime DataAlteracao { get; set; } 

        public int LoginId { get; set; } 

        public int LoginPessoaId { get; set; } 

        public string LoginNome { get; set; } 

        public int LoginSituacaoId { get; set; } 

        public string LoginSituacaoNome { get; set; } 

        public string LoginUsuario { get; set; } 

        public string LoginSenha { get; set; } 

        public string LoginNomeExibicao { get; set; } 
    } 
} 
