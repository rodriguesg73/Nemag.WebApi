using System;

namespace Nemag.Core.Entidade.Cliente 
{ 
    public partial class ClienteItem : _BaseItem 
    { 
        public DateTime DataInclusao { get; set; } 

        public DateTime DataAlteracao { get; set; } 

        public int RegistroSituacaoId { get; set; } 

        public int RegistroLoginId { get; set; } 

        public int PessoaId { get; set; } 

        public string Nome { get; set; } 
    } 
} 
