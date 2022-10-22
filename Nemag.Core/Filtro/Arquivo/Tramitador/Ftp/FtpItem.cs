using System;

namespace Nemag.Core.Filtro.Arquivo.Tramitador.Ftp 
{ 
    public partial class FtpItem : _BaseItem 
    { 
        public DateTime? DataInclusaoInicial { get; set; }

        public DateTime? DataInclusaoFinal { get; set; }

        public DateTime? DataAlteracaoInicial { get; set; }

        public DateTime? DataAlteracaoFinal { get; set; }

        public int? RegistroSituacaoId { get; set; }

        public int? RegistroLoginId { get; set; }

        public int? ArquivoTramitadorId { get; set; }

        public int? ArquivoTramitadorAcaoId { get; set; }

        public string Servidor { get; set; }

        public int? PortaInicial { get; set; }

        public int? PortaFinal { get; set; }

        public string Usuario { get; set; }

        public string Senha { get; set; }

        public string DiretorioUrl { get; set; } 
    } 
} 
