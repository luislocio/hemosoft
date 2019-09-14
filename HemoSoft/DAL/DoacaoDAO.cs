using HemoSoft.Model;
using System.Collections.Generic;
using System.Linq;

namespace HemoSoft.DAL
{
    class DoacaoDAO
    {

        private static Context ctx = SingletonContext.GetInstance();
        public static void CadastrarDoacao(Doacao d)
        {
            ctx.Doacoes.Add(d);
            ctx.SaveChanges();
        }

        public static List<Doacao> BuscarDoacaoPorDoador(Doador d)
        {
            //Where: é método que retorna todas as
            //ocorrências em uma busca
            return ctx.Doacoes.Where
                (x => x.Doador.IdDoador.Equals(d.IdDoador)).ToList();
        }
    }
}
