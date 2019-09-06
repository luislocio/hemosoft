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
        public Solicitacao Solicitacao { get; set; }
        public TriagemClinica TriagemClinica { get; set; }
        public TriagemLaboratorial TriagemLaboratorial { get; set; }
        public ImpedimentosTemporarios ImpedimentosTemporarios { get; set; }
        public ImpedimentosDefinitivos ImpedimentosDefinitivos { get; set; }
    }
}