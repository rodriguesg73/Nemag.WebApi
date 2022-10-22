using System;

namespace Nemag.Core.Entidade.Requisicao.Permissao.Atribuicao 
{ 
    public partial class AtribuicaoItem : _BaseItem 
    { 
        public DateTime DataInclusao { get; set; } 

        public DateTime DataAlteracao { get; set; } 

        public int RegistroSituacaoId { get; set; } 

        public int RegistroLoginId { get; set; } 

        public string Ip { get; set; } 

        public string UrlOrigem { get; set; } 

        public string UrlDestino { get; set; } 

        public int LoginGrupoId { get; set; } 

        public int LoginPerfilId { get; set; } 

        public string LoginGrupoNome { get; set; } 

        public string LoginGrupoDescricao { get; set; } 

        public string LoginPerfilNome { get; set; } 

        public string LoginPerfilDescricao { get; set; } 
    } 
} 
