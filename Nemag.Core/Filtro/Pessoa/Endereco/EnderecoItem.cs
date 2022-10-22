using System;

namespace Nemag.Core.Filtro.Pessoa.Endereco 
{ 
    public partial class EnderecoItem : _BaseItem 
    { 
        public DateTime? DataInclusaoInicial { get; set; }

        public DateTime? DataInclusaoFinal { get; set; }

        public DateTime? DataAlteracaoInicial { get; set; }

        public DateTime? DataAlteracaoFinal { get; set; }

        public int? RegistroSituacaoId { get; set; }

        public int? RegistroLoginId { get; set; }

        public int? PessoaId { get; set; }

        public int? PessoaEnderecoTipoId { get; set; }

        public string Logradouro { get; set; }

        public string Numero { get; set; }

        public string Complemento { get; set; }

        public string BairroNome { get; set; }

        public string CidadeNome { get; set; }

        public string EstadoSigla { get; set; }

        public string Cep { get; set; } 
    } 
} 
