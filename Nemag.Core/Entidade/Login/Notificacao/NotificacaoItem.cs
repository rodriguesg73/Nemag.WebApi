using System;

namespace Nemag.Core.Entidade.Login.Notificacao 
{ 
    public partial class NotificacaoItem : _BaseItem 
    { 
        public DateTime DataInclusao { get; set; } 

        public DateTime DataAlteracao { get; set; } 

        public int RegistroSituacaoId { get; set; } 

        public int RegistroLoginId { get; set; } 

        public int LoginId { get; set; } 

        public int LoginNotificacaoTipoId { get; set; } 

        public string Titulo { get; set; } 

        public string Conteudo { get; set; } 

        public string LinkUrl { get; set; } 

        public int LoginPessoaId { get; set; } 

        public string LoginNome { get; set; } 

        public int LoginSituacaoId { get; set; } 

        public string LoginSituacaoNome { get; set; } 

        public string LoginUsuario { get; set; } 

        public string LoginSenha { get; set; } 

        public string LoginNomeExibicao { get; set; } 

        public string LoginNotificacaoTipoNome { get; set; } 

        public string LoginNotificacaoTipoIconeCss { get; set; } 
    } 
} 
