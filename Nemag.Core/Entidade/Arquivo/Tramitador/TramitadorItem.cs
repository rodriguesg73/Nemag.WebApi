using System;

namespace Nemag.Core.Entidade.Arquivo.Tramitador 
{ 
    public partial class TramitadorItem : _BaseItem 
    { 
        public DateTime DataInclusao { get; set; } 

        public DateTime DataAlteracao { get; set; } 

        public int RegistroSituacaoId { get; set; } 

        public int RegistroLoginId { get; set; } 

        public string Nome { get; set; } 

        public string Descricao { get; set; } 
    } 
} 
