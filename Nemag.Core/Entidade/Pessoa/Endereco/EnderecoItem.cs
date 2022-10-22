using System;

namespace Nemag.Core.Entidade.Pessoa.Endereco 
{ 
    public partial class EnderecoItem : _BaseItem 
    { 
        public DateTime DataInclusao { get; set; } 

        public DateTime DataAlteracao { get; set; } 

        public int RegistroSituacaoId { get; set; } 

        public int RegistroLoginId { get; set; } 

        public int PessoaId { get; set; } 

        public int PessoaEnderecoTipoId { get; set; } 

        public string Logradouro { get; set; } 

        public string Numero { get; set; } 

        public string Complemento { get; set; } 

        public string BairroNome { get; set; } 

        public string CidadeNome { get; set; } 

        public string EstadoSigla { get; set; } 

        public string Cep { get; set; } 

        public string Nome { get; set; } 

        public string PessoaEnderecoTipoNome { get; set; } 
    } 
} 
