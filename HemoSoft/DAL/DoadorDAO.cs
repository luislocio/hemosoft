using HemoSoft.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HemoSoft.DAL
{
    class DoadorDAO
    {
        private static Context ctx = SingletonContext.GetInstance();
        public static void CadastrarDoador(Doador d)
        {
            ctx.Doadores.Add(d);
            ctx.SaveChanges();
        }

        public static Doador BuscarDoadorPorNomeCompleto(Doador d)
        {
            return ctx.Doadores.FirstOrDefault
                (x => x.NomeCompleto.Equals(d.NomeCompleto));
        }


        public static List<Doador> BuscarDoadorPorParteNome(Doador d)
        {
            //Where: é método que retorna todas as
            //ocorrências em uma busca
            return ctx.Doadores.Where
                (x => x.NomeCompleto.Contains(d.NomeCompleto)).ToList();
        }
        

        public static Doador BuscarDoadorPorCpf(Doador d)
        {
            return ctx.Doadores.FirstOrDefault
                (x => x.Cpf.Equals(d.Cpf));
        }

        public static Doador BuscarDoadorPorEstadoCivil(Doador d)
        {
            return ctx.Doadores.FirstOrDefault
                (x => x.EstadoCivil.Equals(d.EstadoCivil));
        }


        public static Doador BuscarDoadorPorGenero(Doador d)
        {
            return ctx.Doadores.FirstOrDefault
                (x => x.Genero.Equals(d.Genero));
        }



    }
}

