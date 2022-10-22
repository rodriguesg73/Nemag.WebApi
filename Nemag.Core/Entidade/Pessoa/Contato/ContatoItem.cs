using System;

namespace Nemag.Core.Entidade.Pessoa.Contato 
{ 
    public partial class ContatoItem : _BaseItem 
    { 
        public int RegistroSituacaoId { get; set; } 

        public int PessoaContatoTipoId { get; set; } 

        public int PessoaId { get; set; } 

        public int RegistroLoginId { get; set; } 

        public string Valor { get; set; } 

        public DateTime DataInclusao { get; set; } 

        public DateTime DataAlteracao { get; set; } 

        public string PessoaContatoTipoNome { get; set; } 

        public string PessoaContatoTipoDescricao { get; set; } 

        public string Nome { get; set; } 
    } 
} 
