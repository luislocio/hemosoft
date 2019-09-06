using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HemoSoft.Model
{
    [Table("Solicitacoes")]
    public class Solicitacao
    {
        [Key] public int IdSolicitacao { get; set; }
        public DateTime DataSolicitacao { get; set; }
        public Doacao Doacao { get; set; }
        public Solicitante Solicitante { get; set; }
    }
}