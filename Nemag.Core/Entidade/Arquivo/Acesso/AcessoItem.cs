using System;

namespace Nemag.Core.Entidade.Arquivo.Acesso 
{ 
    public partial class AcessoItem : _BaseItem 
    { 
        public int RegistroSituacaoId { get; set; } 

        public int ArquivoId { get; set; } 

        public int RegistroLoginId { get; set; } 

        public DateTime DataInclusao { get; set; } 

        public string Ip { get; set; } 

        public DateTime DataAlteracao { get; set; } 

        public string ArquivoNome { get; set; } 

        public string ArquivoDescricao { get; set; } 

        public string ArquivoDiretorioLocalUrl { get; set; } 

        public string ArquivoGuid { get; set; } 

        public string ArquivoChecksun { get; set; } 
    } 
} 
