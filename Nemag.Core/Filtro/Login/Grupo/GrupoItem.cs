using System;

namespace Nemag.Core.Filtro.Login.Grupo 
{ 
    public partial class GrupoItem : _BaseItem 
    { 
        public int? RegistroSituacaoId { get; set; }

        public int? RegistroLoginId { get; set; }

        public string Nome { get; set; }

        public string Descricao { get; set; }

        public DateTime? DataInclusaoInicial { get; set; }

        public DateTime? DataInclusaoFinal { get; set; }

        public DateTime? DataAlteracaoInicial { get; set; }

        public DateTime? DataAlteracaoFinal { get; set; } 
    } 
} 
