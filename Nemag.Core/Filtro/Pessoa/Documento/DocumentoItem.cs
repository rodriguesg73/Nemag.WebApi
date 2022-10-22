using System;

namespace Nemag.Core.Filtro.Pessoa.Documento 
{ 
    public partial class DocumentoItem : _BaseItem 
    { 
        public DateTime? DataInclusaoInicial { get; set; }

        public DateTime? DataInclusaoFinal { get; set; }

        public DateTime? DataAlteracaoInicial { get; set; }

        public DateTime? DataAlteracaoFinal { get; set; }

        public int? RegistroSituacaoId { get; set; }

        public int? PessoaDocumentoTipoId { get; set; }

        public string Valor { get; set; } 
    } 
} 
