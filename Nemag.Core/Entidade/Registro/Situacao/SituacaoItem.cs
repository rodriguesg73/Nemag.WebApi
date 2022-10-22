using System;

namespace Nemag.Core.Entidade.Registro.Situacao 
{ 
    public partial class SituacaoItem : _BaseItem 
    { 
        public DateTime DataInclusao { get; set; } 

        public DateTime DataAlteracao { get; set; } 

        public string Nome { get; set; } 
    } 
} 
