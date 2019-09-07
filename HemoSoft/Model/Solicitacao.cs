using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HemoSoft.Model
{
    [Table("Solicitacoes")]
    public class Solicitacao
    {
        [Key] public int IdSolicitacao { get; set; }
        public DateTime DataSolicitacao { get; set; }
        public List<Doacao> Doacao { get; set; }
        public Solicitante Solicitante { get; set; }
    }
}