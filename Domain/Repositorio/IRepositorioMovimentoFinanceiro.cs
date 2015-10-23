using System;
using System.Collections.Generic;
using System.Linq;
namespace Domain.Repositorio
{
    public interface IRepositorioMovimentoFinanceiro
    {
        void Inserir(MovimentoFinanceiro movimento);
        IQueryable<MovimentoFinanceiro> Filtar(Func<MovimentoFinanceiro, bool> func);
        void Delete(Guid id);
    }
}
