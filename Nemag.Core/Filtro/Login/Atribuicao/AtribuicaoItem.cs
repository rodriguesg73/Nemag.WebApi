using System;

namespace Nemag.Core.Filtro.Login.Atribuicao 
{ 
    public partial class AtribuicaoItem : _BaseItem 
    { 
        public int? RegistroSituacaoId { get; set; }

        public int? RegistroLoginId { get; set; }

        public DateTime? DataInclusaoInicial { get; set; }

        public DateTime? DataInclusaoFinal { get; set; }

        public DateTime? DataAlteracaoInicial { get; set; }

        public DateTime? DataAlteracaoFinal { get; set; }

        public int? LoginGrupoId { get; set; }

        public int? LoginPerfilId { get; set; }

        public int? LoginId { get; set; } 
    } 
} 
