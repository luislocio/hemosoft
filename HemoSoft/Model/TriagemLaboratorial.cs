using HemoSoft.Model.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HemoSoft.Model
{
    [Table("TriagensLaboratoriais")]
    public class TriagemLaboratorial
    {
        [Key] public int IdTriagemLaboratorial { get; set; }
        public bool? HepatiteB { get; set; }
        public bool? HepatiteC { get; set; }
        public bool? Hiv { get; set; }
        public StatusTriagem? StatusTriagem { get; set; }
        public Doacao Doacao { get; set; }

        public override string ToString()
        {
            return "Triagem Laboratorial #" + IdTriagemLaboratorial +
                   "\nHepatite B: " + HepatiteB +
                   "\nHepatite C: " + HepatiteC +
                   "\nHiv: " + Hiv +
                   "\nStatus: " + StatusTriagem;
        }
    }
}