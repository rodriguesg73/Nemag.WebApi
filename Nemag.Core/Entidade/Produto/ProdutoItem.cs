using System;

namespace Nemag.Core.Entidade.Produto 
{ 
    public partial class ProdutoItem : _BaseItem 
    { 
        public DateTime DataInclusao { get; set; } 

        public DateTime DataAlteracao { get; set; } 

        public int RegistroSituacaoId { get; set; } 

        public int RegistroLoginId { get; set; } 

        public string Nome { get; set; } 

        public string Descricao { get; set; } 

        public decimal Valor { get; set; } 

        public string Codigo { get; set; } 

        public int ProdutoCategoriaId { get; set; } 

        public string ProdutoCategoriaNome { get; set; } 
    } 
} 
