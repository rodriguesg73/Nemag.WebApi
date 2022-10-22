using System;

namespace Nemag.Core.Entidade.Arquivo.Tramitador.Diretorio 
{ 
    public partial class DiretorioItem : _BaseItem 
    { 
        public DateTime DataInclusao { get; set; } 

        public DateTime DataAlteracao { get; set; } 

        public int RegistroSituacaoId { get; set; } 

        public int RegistroLoginId { get; set; } 

        public int ArquivoTramitadorId { get; set; } 

        public int ArquivoTramitadorAcaoId { get; set; } 

        public string DiretorioUrl { get; set; } 

        public string ArquivoTramitadorNome { get; set; } 

        public string ArquivoTramitadorDescricao { get; set; } 

        public string ArquivoTramitadorAcaoNome { get; set; } 
    } 
} 
