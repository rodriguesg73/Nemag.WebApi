using System;

namespace Nemag.Core.Filtro.Empresa 
{ 
    public partial class EmpresaItem : _BaseItem 
    { 
        public DateTime? DataInclusaoInicial { get; set; }

        public DateTime? DataInclusaoFinal { get; set; }

        public DateTime? DataAlteracaoInicial { get; set; }

        public DateTime? DataAlteracaoFinal { get; set; }

        public int? RegistroSituacaoId { get; set; }

        public int? RegistroLoginId { get; set; }

        public int? PessoaId { get; set; }

        public int? EmpresaCategoriaId { get; set; }

        public string NomeExibicao { get; set; } 
    } 
} 
