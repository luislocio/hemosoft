namespace HemoSoft.Model
{
    public enum TipoUsuario
    {
        Triador,
        Solicitante
    }

    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string NomeDeUsuario { get; set; }
        public TipoUsuario TipoUsuario { get; set; }
    }
}