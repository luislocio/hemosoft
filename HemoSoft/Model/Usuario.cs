using HemoSoft.Model.Enum;

namespace HemoSoft.Model
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string NomeDeUsuario { get; set; }
        public TipoUsuario TipoUsuario { get; set; }
    }
}