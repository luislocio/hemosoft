using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HemoSoft.Model
{
    public enum Gravidez
    {
        Nenhuma,
        PartoNormal,
        Cesarea
    }

    [Table("ImpedimentosTemporarios")]
    public class ImpedimentosTemporarios
    {

        [Key] public int IdImpedimentosTemporarios { get; set; }
        public bool? BebidaAlcoolica { get; set; }
        public int? BebidaAlcoolicaUltimaVez { get; set; }
        public Gravidez Gravidez { get; set; }
        public int? GravidezUltimaVez { get; set; }
        public bool? Gripe { get; set; }
        public int? GripeUltimaVez { get; set; }
        public bool? Tatuagem { get; set; }
        public int? TatuagemUltimaVez { get; set; }
        public Doacao Doacao { get; set; }

        public override string ToString()
        {
            return "Impedimentos Temporarios" +
                   "\nBebida Alcoolica: " + BebidaAlcoolica +
                   "\nGravidez: " + Gravidez +
                   "\nGripe: " + Gripe +
                   "\nTatuagem: " + Tatuagem;
        }
    }
}