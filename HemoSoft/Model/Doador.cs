using HemoSoft.Model.Enum;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HemoSoft.Model
{
    [Table("Doadores")]
    public class Doador
    {
        [Key] public int IdDoador { get; set; }
        public string Cpf { get; set; }
        public EstadoCivil EstadoCivil { get; set; }
        public string NomeCompleto { get; set; }
        public Genero Genero { get; set; }
        public List<Doacao> Doacoes { get; set; }
        public FatorRh? FatorRh { get; set; }
        public TipoSanguineo? TipoSanguineo { get; set; }

        public override string ToString()
        {
            return "Nome Completo: " + NomeCompleto +
                   " | CPF: " + Cpf +
                   " | EstadoCivil: " + EstadoCivil +
                   " | Genero: " + Genero;
        }
    }
}