using System;

namespace Nemag.Core.Entidade.Pessoa.Documento.Tipo 
{ 
    public partial class TipoItem : _BaseItem 
    { 
        public DateTime DataInclusao { get; set; } 

        public DateTime DataAlteracao { get; set; } 

        public int RegistroSituacaoId { get; set; } 

        public string Nome { get; set; } 
    } 
} 
