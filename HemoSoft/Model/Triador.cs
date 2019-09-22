using HemoSoft.Model.Enum;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HemoSoft.Model
{
    [Table("Triadores")]
    public class Triador
    {
        [Key] public int IdTriador { get; set; }
        public string Matricula { get; set; }
        public string NomeCompleto { get; set; }
        public string Senha { get; set; }
        public StatusUsuario StatusUsuario { get; set; }
        public List<Doacao> Doacoes { get; set; }

        public override string ToString()
        {
            return "Matricula: " + Matricula +
                   " | Nome Completo: " + NomeCompleto +
                   " | Status: " + StatusUsuario;
        }
    }
}