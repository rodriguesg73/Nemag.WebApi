using System;

namespace Nemag.Core.Entidade.Login.Situacao 
{ 
    public partial class SituacaoItem : _BaseItem 
    { 
        public DateTime DataInclusao { get; set; } 

        public DateTime DataAlteracao { get; set; } 

        public int RegistroSituacaoId { get; set; } 

        public string Nome { get; set; } 
    } 
} 
