using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HemoSoft.Model
{
    public enum FatorRh
    {
        Negativo,
        Positivo
    }

    public enum TipoSanguineo
    {
        A,
        B,
        AB,
        O,
        Indefinido
    }

    [Table("TriagensLaboratoriais")]
    public class TriagemLaboratorial
    {
        [Key] public int IdTriagemLaboratorial { get; set; }
        public FatorRh FatorRh { get; set; }
        public bool HepatiteB { get; set; }
        public bool HepatiteC { get; set; }
        public bool Hiv { get; set; }
        public StatusTriagem StatusTriagem { get; set; }
        public TipoSanguineo TipoSanguineo { get; set; }
        public Doacao Doacao { get; set; }

        public override string ToString()
        {
            return "Triagem Laboratorial #" + IdTriagemLaboratorial +
                   "\nTipo Sanguineo: " + TipoSanguineo + " " + FatorRh +
                   "\nHepatite B: " + HepatiteB +
                   "\nHepatite C: " + HepatiteC +
                   "\nHiv: " + Hiv +
                   "\nStatus: " + StatusTriagem;
        }
    }
}