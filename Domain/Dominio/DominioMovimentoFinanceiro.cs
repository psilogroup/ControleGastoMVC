using Domain.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dominio
{
    public class DominioMovimentoFinanceiro
    {
        private IRepositorioMovimentoFinanceiro repositorio { get; set; }

        public DominioMovimentoFinanceiro()
        {
            //Poderiamos muito bem ter usado injeção de dependencia
            //mas por ser um teste simples não vejo a necessidade
            repositorio = new Repositorio.RepositorioMovimentoFinanceiro();
        }

        /// <summary>
        /// Insere um novo Movimento Financeiro
        /// </summary>
        /// <param name="movimento">Movimento</param>
        public MovimentoFinanceiro NovoGasto(MovimentoFinanceiro movimento)
        {
            if (movimento == null)
                throw new Exception("Movimento inválido");

            if (movimento.DataMovimentacao == DateTime.MinValue || movimento.DataMovimentacao == DateTime.MaxValue)
                throw new Exception("A Data do movimento não é válida");

            if (movimento.Valor == 0)
                throw new Exception("O Valor não pode ser zero");


         
            movimento.DataCadastro = DateTime.Now;
            movimento.Id = Guid.NewGuid();

            repositorio.Inserir(movimento);
            return movimento;
            
        }

        /// <summary>
        /// Lista todos os movimentos do ultimo mês
        /// </summary>
        /// <returns>Lista com Movimentos</returns>
        public List<MovimentoFinanceiro> GastosUltimoMes()
        {
            DateTime dataInicial = DateTime.Now.AddMonths(-1);
            DateTime dataFianl = DateTime.Now;

            return repositorio.Filtar(x => x.DataMovimentacao >= dataInicial && x.DataMovimentacao <= dataFianl).ToList();
        }

        /// <summary>
        /// Retorna Lista de Movimentos agrupado por Categoria
        /// Os Valores são a soma do total da categoria
        /// </summary>
        /// <returns></returns>
        public List<MovimentoFinanceiro> GastosPorCategoriaUltimoMes()
        {
            DateTime dataInicial = DateTime.Now.AddMonths(-1);
            DateTime dataFianl = DateTime.Now;

            var list = repositorio.Filtar(x => x.DataMovimentacao >= dataInicial && x.DataMovimentacao <= dataFianl).
                GroupBy(y => y.Categoria).Select(
                 k => new MovimentoFinanceiro { 
                     Categoria = k.First().Categoria, 
                     Valor = 
                     k.Sum(c => c.Valor) 
                 }).ToList();

            return list;
           
        }

        public void RemoverGasto(Guid id)
        {
            using (Contexto db = new Contexto())
            {
                repositorio.Delete(id);
            }
        }
    }
}
