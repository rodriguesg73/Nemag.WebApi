using System;

namespace Nemag.Core.Entidade.Empresa 
{ 
    public partial class EmpresaItem : _BaseItem 
    { 
        public DateTime DataInclusao { get; set; } 

        public DateTime DataAlteracao { get; set; } 

        public int RegistroSituacaoId { get; set; } 

        public int RegistroLoginId { get; set; } 

        public int PessoaId { get; set; } 

        public int EmpresaCategoriaId { get; set; } 

        public string NomeExibicao { get; set; } 

        public string Nome { get; set; } 

        public string EmpresaCategoriaNome { get; set; } 
    } 
} 
