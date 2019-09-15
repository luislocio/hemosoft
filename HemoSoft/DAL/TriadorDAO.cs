using HemoSoft.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HemoSoft.DAL
{
    class TriadorDAO
    {
        private static Context ctx = SingletonContext.GetInstance();
        public static void CadastrarTriador(Triador t)
        {
            ctx.Triadores.Add(t);
            ctx.SaveChanges();
        }

        public static Triador BuscarTriadorPorMatricula(Triador t)
        {
            return ctx.Triadores.FirstOrDefault
                (x => x.Matricula.Equals(t.Matricula));
        }

        public static Triador BuscarTriadorPorId(Triador t)
        {
            return ctx.Triadores.FirstOrDefault
                (x => x.IdTriador.Equals(t.IdTriador));
        }
    }
}