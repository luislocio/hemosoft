using HemoSoft.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HemoSoft.DAL
{
    class SolicitanteDAO
    {
        private static Context ctx = SingletonContext.GetInstance();
        public static void CadastrarSolicitante(Solicitante s)
        {
            ctx.Solicitantes.Add(s);
            ctx.SaveChanges();
        }

        public static Solicitante BuscarSolicitantePorCnpj(Solicitante s)
        {
            return ctx.Solicitantes.FirstOrDefault
                (x => x.Cnpj.Equals(s.Cnpj));
        }

        public static Solicitante BuscarSolicitantePorRazaoSocial(Solicitante s)
        {
            return ctx.Solicitantes.FirstOrDefault
                (x => x.RazaoSocial.Equals(s.RazaoSocial));
        }


        public static List<Solicitante> BuscarProdutosPorParteNome(Solicitante s)
        {
            //Where: é método que retorna todas as ocorrências em uma busca
            return ctx.Solicitantes.Where
                (x => x.RazaoSocial.Contains(s.RazaoSocial)).ToList();
        }

    }
}

