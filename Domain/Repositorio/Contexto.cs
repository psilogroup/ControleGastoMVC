using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositorio
{
    public class Contexto : DbContext 
    {
        public DbSet<MovimentoFinanceiro> MovimentoFinanceiro { get; set; }
    }
}
