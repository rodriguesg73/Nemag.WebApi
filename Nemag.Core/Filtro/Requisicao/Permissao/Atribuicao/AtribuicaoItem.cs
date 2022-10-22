using System;

namespace Nemag.Core.Filtro.Requisicao.Permissao.Atribuicao 
{ 
    public partial class AtribuicaoItem : _BaseItem 
    { 
        public DateTime? DataInclusaoInicial { get; set; }

        public DateTime? DataInclusaoFinal { get; set; }

        public DateTime? DataAlteracaoInicial { get; set; }

        public DateTime? DataAlteracaoFinal { get; set; }

        public int? RegistroSituacaoId { get; set; }

        public int? RegistroLoginId { get; set; }

        public string Ip { get; set; }

        public string UrlOrigem { get; set; }

        public string UrlDestino { get; set; }

        public int? LoginGrupoId { get; set; }

        public int? LoginPerfilId { get; set; } 
    } 
} 
