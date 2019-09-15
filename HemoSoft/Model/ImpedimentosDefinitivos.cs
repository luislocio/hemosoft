using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HemoSoft.Model
{
    [Table("ImpedimentosDefinitivos")]
    public class ImpedimentosDefinitivos
    {
        [Key] public int IdImpedimentosDefinitivos { get; set; }
        // Implementar Opção "Antecedente de AVC" no cadastro de doações 
        public bool? AntecedenteAvc { get; set; }
        public bool? HepatiteB { get; set; }
        public bool? HepatiteC { get; set; }
        public bool? Hiv { get; set; }
        public Doacao Doacao { get; set; }

        public override string ToString()
        {
            return "Impedimentos Definitivos" +
                   "\nAntecedentes de AVC: " + AntecedenteAvc +
                   "\nHepatite B: " + HepatiteB +
                   "\nHepatite C: " + HepatiteC +
                   "\nHIV: " + Hiv;
        }
    }
}