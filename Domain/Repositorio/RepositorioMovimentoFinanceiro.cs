using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositorio
{
    public class RepositorioMovimentoFinanceiro : Domain.Repositorio.IRepositorioMovimentoFinanceiro
    {
        public void Inserir(MovimentoFinanceiro movimento)
        {
            using (Contexto db = new Contexto())
            {
                MovimentoFinanceiro mv = new MovimentoFinanceiro();


                db.MovimentoFinanceiro.Add(movimento);
                db.SaveChanges();
            }
        }

        public IQueryable<MovimentoFinanceiro> Filtar(Func<MovimentoFinanceiro,bool> func)
        {
            Contexto db = new Contexto();
            
            var retorno = db.MovimentoFinanceiro.Where<MovimentoFinanceiro>(func).AsQueryable<MovimentoFinanceiro>();

            return retorno;

        }

        public void Delete(Guid id)
        {
            using (Contexto db = new Contexto())
            {
                var item = (from q in db.MovimentoFinanceiro where q.Id == id select q).FirstOrDefault();

                if (item == null)
                    throw new Exception("Item não encontrado");


                db.MovimentoFinanceiro.Remove(item);
                db.SaveChanges();
            }
        }
    }
}
