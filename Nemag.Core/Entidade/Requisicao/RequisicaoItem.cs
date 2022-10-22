using System;

namespace Nemag.Core.Entidade.Requisicao 
{ 
    public partial class RequisicaoItem : _BaseItem 
    { 
        public DateTime DataInclusao { get; set; } 

        public DateTime DataAlteracao { get; set; } 

        public int RegistroSituacaoId { get; set; } 

        public int LoginAcessoId { get; set; } 

        public string Ip { get; set; } 

        public string UrlReferencia { get; set; } 

        public string UrlOrigem { get; set; } 

        public string UrlDestino { get; set; } 

        public int LoginAcessoLoginPessoaId { get; set; } 

        public string LoginAcessoLoginNome { get; set; } 

        public int LoginAcessoLoginSituacaoId { get; set; } 

        public string LoginAcessoLoginSituacaoNome { get; set; } 

        public int LoginAcessoLoginId { get; set; } 

        public string LoginAcessoLoginUsuario { get; set; } 

        public string LoginAcessoLoginSenha { get; set; } 

        public string LoginAcessoLoginNomeExibicao { get; set; } 

        public string LoginAcessoToken { get; set; } 

        public string LoginAcessoIp { get; set; } 

        public DateTime LoginAcessoDataValidade { get; set; } 
    } 
} 
