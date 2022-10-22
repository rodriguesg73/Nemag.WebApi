using System;

namespace Nemag.Core.Entidade.Menu 
{ 
    public partial class MenuItem : _BaseItem 
    { 
        public int RegistroSituacaoId { get; set; } 

        public int RegistroLoginId { get; set; } 

        public int MenuSuperiorId { get; set; } 

        public string Titulo { get; set; } 

        public string Descricao { get; set; } 

        public string WebUrl { get; set; } 

        public string IconeCss { get; set; } 

        public DateTime DataInclusao { get; set; } 

        public DateTime DataAlteracao { get; set; } 
    } 
} 
