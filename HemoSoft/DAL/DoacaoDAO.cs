using HemoSoft.Model;
using System.Collections.Generic;
using System.Data.Entity;
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

        public static Doacao BuscarDoacaoPorId(Doacao d)
        {
            //Where: é método que retorna todas as
            //ocorrências em uma busca
            return ctx.Doacoes
                .Include("Doador")
                .Include("TriagemClinica")
                .Include("TriagemLaboratorial")
                .Include("ImpedimentosTemporarios")
                .Include("ImpedimentosDefinitivos")
                .FirstOrDefault
                (x => x.IdDoacao.Equals(d.IdDoacao));
        }

        public static List<Doacao> BuscarDoacaoPorStatus(Doacao d)
        {
            //Where: é método que retorna todas as
            //ocorrências em uma busca
            return ctx.Doacoes
                .Include("TriagemClinica")
                .Include("TriagemLaboratorial")
                .Include("ImpedimentosTemporarios")
                .Include("ImpedimentosDefinitivos")
                .Where
                (x => x.StatusDoacao == d.StatusDoacao).ToList();
        }

        public static void AlterarDoacao(Doacao d)
        {
            ctx.Entry(d).State = EntityState.Modified;
            ctx.SaveChanges();
        }
    }
}
