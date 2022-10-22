using System;

namespace Nemag.Core.Entidade.Login.Notificacao.Tipo 
{ 
    public partial class TipoItem : _BaseItem 
    { 
        public DateTime DataInclusao { get; set; } 

        public DateTime DataAlteracao { get; set; } 

        public int RegistroSituacaoId { get; set; } 

        public int RegistroLoginId { get; set; } 

        public string Nome { get; set; } 

        public string IconeCss { get; set; } 
    } 
} 
