using System;

namespace Nemag.Core.Entidade.Arquivo 
{ 
    public partial class ArquivoItem : _BaseItem 
    { 
        public int RegistroSituacaoId { get; set; } 

        public int RegistroLoginId { get; set; } 

        public DateTime DataInclusao { get; set; } 

        public DateTime DataAlteracao { get; set; } 

        public string Nome { get; set; } 

        public string Descricao { get; set; } 

        public string DiretorioLocalUrl { get; set; } 

        public string Guid { get; set; } 

        public string Checksun { get; set; } 
    } 
} 
