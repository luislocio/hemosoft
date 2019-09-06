using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HemoSoft.Model
{
    public enum StatusDoacao
    {
        NaoDisponivel,
        Disponivel,
        AguardandoAtendimento,
        AguardandoResultados
    }

    [Table("Doacoes")]
    public class Doacao
    {
        [Key] public int IdDoacao { get; set; }
        public DateTime DataDoacao { get; set; }
        public StatusDoacao StatusDoacao { get; set; }
        public Doador Doador { get; set; }
        public Triador Triador { get; set; }
        [Required] public Solicitacao Solicitacao { get; set; }
        [Required] public TriagemClinica TriagemClinica { get; set; }
        [Required] public TriagemLaboratorial TriagemLaboratorial { get; set; }
        [Required] public ImpedimentosTemporarios ImpedimentosTemporarios { get; set; }
        [Required] public ImpedimentosDefinitivos ImpedimentosDefinitivos { get; set; }
    }
}