using System;

namespace Nemag.Core.Entidade.Menu.Permissao.Login 
{ 
    public partial class LoginItem : _BaseItem 
    { 
        public int RegistroLoginId { get; set; } 

        public int RegistroSituacaoId { get; set; } 

        public DateTime DataInclusao { get; set; } 

        public DateTime DataAlteracao { get; set; } 

        public int MenuId { get; set; } 

        public int LoginId { get; set; } 

        public int MenuMenuSuperiorId { get; set; } 

        public string MenuTitulo { get; set; } 

        public string MenuDescricao { get; set; } 

        public string MenuWebUrl { get; set; } 

        public string MenuIconeCss { get; set; } 

        public int LoginPessoaId { get; set; } 

        public string LoginNome { get; set; } 

        public int LoginSituacaoId { get; set; } 

        public string LoginSituacaoNome { get; set; } 

        public string LoginUsuario { get; set; } 

        public string LoginSenha { get; set; } 

        public string LoginNomeExibicao { get; set; } 
    } 
} 
