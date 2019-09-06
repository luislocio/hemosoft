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
        [Key] public int idDoacao { get; set; }
        public DateTime dataDoacao { get; set; }
        public StatusDoacao StatusDoacao { get; set; }
    }
}