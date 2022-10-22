using System;

namespace Nemag.Core.Filtro.Pessoa.Contato 
{ 
    public partial class ContatoItem : _BaseItem 
    { 
        public int? RegistroSituacaoId { get; set; }

        public int? PessoaContatoTipoId { get; set; }

        public int? PessoaId { get; set; }

        public int? RegistroLoginId { get; set; }

        public string Valor { get; set; }

        public DateTime? DataInclusaoInicial { get; set; }

        public DateTime? DataInclusaoFinal { get; set; }

        public DateTime? DataAlteracaoInicial { get; set; }

        public DateTime? DataAlteracaoFinal { get; set; } 
    } 
} 
