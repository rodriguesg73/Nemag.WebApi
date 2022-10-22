using System;

namespace Nemag.Core.Entidade.Pais 
{ 
    public partial class PaisItem : _BaseItem 
    { 
        public DateTime DataInclusao { get; set; } 

        public DateTime DataAlteracao { get; set; } 

        public int RegistroSituacaoId { get; set; } 

        public int RegistroLoginId { get; set; } 

        public string Nome { get; set; } 

        public string Codigo { get; set; } 

        public int PaisContinenteId { get; set; } 

        public string PaisContinenteNome { get; set; } 
    } 
} 
