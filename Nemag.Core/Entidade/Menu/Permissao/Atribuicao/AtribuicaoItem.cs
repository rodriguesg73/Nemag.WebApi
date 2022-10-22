using System;

namespace Nemag.Core.Entidade.Menu.Permissao.Atribuicao 
{ 
    public partial class AtribuicaoItem : _BaseItem 
    { 
        public int RegistroLoginId { get; set; } 

        public int RegistroSituacaoId { get; set; } 

        public DateTime DataInclusao { get; set; } 

        public DateTime DataAlteracao { get; set; } 

        public int MenuId { get; set; } 

        public int LoginGrupoId { get; set; } 

        public int LoginPerfilId { get; set; } 

        public int MenuMenuSuperiorId { get; set; } 

        public string MenuTitulo { get; set; } 

        public string MenuDescricao { get; set; } 

        public string MenuWebUrl { get; set; } 

        public string MenuIconeCss { get; set; } 

        public string LoginGrupoNome { get; set; } 

        public string LoginGrupoDescricao { get; set; } 

        public string LoginPerfilNome { get; set; } 

        public string LoginPerfilDescricao { get; set; } 
    } 
} 
