using System;

namespace Nemag.Core.Entidade.Login.Atribuicao 
{ 
    public partial class AtribuicaoItem : _BaseItem 
    { 
        public int RegistroSituacaoId { get; set; } 

        public int RegistroLoginId { get; set; } 

        public DateTime DataInclusao { get; set; } 

        public DateTime DataAlteracao { get; set; } 

        public int LoginGrupoId { get; set; } 

        public int LoginPerfilId { get; set; } 

        public int LoginId { get; set; } 

        public string LoginGrupoNome { get; set; } 

        public string LoginGrupoDescricao { get; set; } 

        public string LoginPerfilNome { get; set; } 

        public string LoginPerfilDescricao { get; set; } 

        public int LoginPessoaId { get; set; } 

        public string LoginNome { get; set; } 

        public int LoginSituacaoId { get; set; } 

        public string LoginSituacaoNome { get; set; } 

        public string LoginUsuario { get; set; } 

        public string LoginSenha { get; set; } 

        public string LoginNomeExibicao { get; set; } 
    } 
} 
