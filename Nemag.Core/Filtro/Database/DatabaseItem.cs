using System;

namespace Nemag.Core.Filtro.Database 
{ 
    public partial class DatabaseItem : _BaseItem 
    { 
        public int? DatabaseTipoId { get; set; }

        public int? RegistroSituacaoId { get; set; }

        public int? RegistroLoginId { get; set; }

        public string Nome { get; set; }

        public string Descricao { get; set; }

        public string Ip { get; set; }

        public int? PortaInicial { get; set; }

        public int? PortaFinal { get; set; }

        public string Usuario { get; set; }

        public string Senha { get; set; }

        public string DatabaseNome { get; set; }

        public DateTime? DataInclusaoInicial { get; set; }

        public DateTime? DataInclusaoFinal { get; set; }

        public DateTime? DataAlteracaoInicial { get; set; }

        public DateTime? DataAlteracaoFinal { get; set; } 
    } 
} 
