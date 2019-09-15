using HemoSoft.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HemoSoft.DAL
{
    class SolicitacaoDAO
    {

        private static Context ctx = SingletonContext.GetInstance();
        public static void CadastrarSolicitacao(Solicitacao so)
        {
            ctx.Solicitacoes.Add(so);
            ctx.SaveChanges();
        }
    }
}
