using System;

namespace Nemag.Core.Entidade.Requisicao.Resultado 
{ 
    public partial class ResultadoItem : _BaseItem 
    { 
        public DateTime DataInclusao { get; set; } 

        public DateTime DataAlteracao { get; set; } 

        public int RegistroSituacaoId { get; set; } 

        public int RequisicaoId { get; set; } 

        public string Conteudo { get; set; } 

        public string Tipo { get; set; } 

        public int Codigo { get; set; } 

        public int RequisicaoLoginAcessoLoginPessoaId { get; set; } 

        public string RequisicaoLoginAcessoLoginNome { get; set; } 

        public int RequisicaoLoginAcessoLoginSituacaoId { get; set; } 

        public string RequisicaoLoginAcessoLoginSituacaoNome { get; set; } 

        public int RequisicaoLoginAcessoLoginId { get; set; } 

        public string RequisicaoLoginAcessoLoginUsuario { get; set; } 

        public string RequisicaoLoginAcessoLoginSenha { get; set; } 

        public string RequisicaoLoginAcessoLoginNomeExibicao { get; set; } 

        public int RequisicaoLoginAcessoId { get; set; } 

        public string RequisicaoLoginAcessoToken { get; set; } 

        public string RequisicaoLoginAcessoIp { get; set; } 

        public DateTime RequisicaoLoginAcessoDataValidade { get; set; } 

        public string RequisicaoIp { get; set; } 

        public string RequisicaoUrlReferencia { get; set; } 

        public string RequisicaoUrlOrigem { get; set; } 

        public string RequisicaoUrlDestino { get; set; } 
    } 
} 
