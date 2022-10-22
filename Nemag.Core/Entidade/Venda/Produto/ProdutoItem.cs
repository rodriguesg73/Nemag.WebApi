using System;

namespace Nemag.Core.Entidade.Venda.Produto 
{ 
    public partial class ProdutoItem : _BaseItem 
    { 
        public DateTime DataInclusao { get; set; } 

        public DateTime DataAlteracao { get; set; } 

        public int RegistroSituacaoId { get; set; } 

        public int RegistroLoginId { get; set; } 

        public int ProdutoId { get; set; } 

        public int VendaId { get; set; } 

        public int ProdutoCategoriaId { get; set; } 

        public string ProdutoCategoriaNome { get; set; } 

        public string ProdutoNome { get; set; } 

        public string ProdutoDescricao { get; set; } 

        public decimal ProdutoValor { get; set; } 

        public string ProdutoCodigo { get; set; } 

        public int VendaClientePessoaId { get; set; } 

        public string VendaClienteNome { get; set; } 

        public int VendaClienteId { get; set; } 

        public decimal VendaValor { get; set; } 
    } 
} 
