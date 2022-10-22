using System;

namespace Nemag.Core.Entidade.Pais.Continente 
{ 
    public partial class ContinenteItem : _BaseItem 
    { 
        public DateTime DataInclusao { get; set; } 

        public DateTime DataAlteracao { get; set; } 

        public int RegistroSituacaoId { get; set; } 

        public int RegistroLoginId { get; set; } 

        public string Nome { get; set; } 
    } 
} 
