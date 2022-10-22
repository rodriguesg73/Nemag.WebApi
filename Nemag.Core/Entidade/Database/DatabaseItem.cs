using System;

namespace Nemag.Core.Entidade.Database 
{ 
    public partial class DatabaseItem : _BaseItem 
    { 
        public int DatabaseTipoId { get; set; } 

        public int RegistroSituacaoId { get; set; } 

        public int RegistroLoginId { get; set; } 

        public string Nome { get; set; } 

        public string Descricao { get; set; } 

        public string Ip { get; set; } 

        public int Porta { get; set; } 

        public string Usuario { get; set; } 

        public string Senha { get; set; } 

        public string DatabaseNome { get; set; } 

        public DateTime DataInclusao { get; set; } 

        public DateTime DataAlteracao { get; set; } 

        public string DatabaseTipoNome { get; set; } 
    } 
} 
