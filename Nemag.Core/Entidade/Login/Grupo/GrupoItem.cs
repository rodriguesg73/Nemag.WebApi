using System;

namespace Nemag.Core.Entidade.Login.Grupo 
{ 
    public partial class GrupoItem : _BaseItem 
    { 
        public int RegistroSituacaoId { get; set; } 

        public int RegistroLoginId { get; set; } 

        public string Nome { get; set; } 

        public string Descricao { get; set; } 

        public DateTime DataInclusao { get; set; } 

        public DateTime DataAlteracao { get; set; } 
    } 
} 
