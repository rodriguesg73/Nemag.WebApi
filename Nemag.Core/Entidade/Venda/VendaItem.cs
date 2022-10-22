using System;

namespace Nemag.Core.Entidade.Venda 
{ 
    public partial class VendaItem : _BaseItem 
    { 
        public DateTime DataInclusao { get; set; } 

        public DateTime DataAlteracao { get; set; } 

        public int RegistroSituacaoId { get; set; } 

        public int RegistroLoginId { get; set; } 

        public int ClienteId { get; set; } 

        public decimal Valor { get; set; } 

        public int ClientePessoaId { get; set; } 

        public string ClienteNome { get; set; } 
    } 
} 
