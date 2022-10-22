using System;

namespace Nemag.Core.Entidade.Pessoa.Documento 
{ 
    public partial class DocumentoItem : _BaseItem 
    { 
        public DateTime DataInclusao { get; set; } 

        public DateTime DataAlteracao { get; set; } 

        public int RegistroSituacaoId { get; set; } 

        public int PessoaDocumentoTipoId { get; set; } 

        public string Valor { get; set; } 

        public string PessoaDocumentoTipoNome { get; set; } 
    } 
} 
