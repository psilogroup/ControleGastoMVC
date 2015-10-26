using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ControleGastos.Controllers
{
    public class GastoController : Controller
    {
     

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Create(FormCollection collection)
        {
            try
            {
                
                Domain.Dominio.DominioMovimentoFinanceiro dominio = new Domain.Dominio.DominioMovimentoFinanceiro();
                Domain.Repositorio.MovimentoFinanceiro movimento = new Domain.Repositorio.MovimentoFinanceiro();

                //De -> Para das Propriedades
                movimento.Descricao = collection["Descricao"];
                movimento.Categoria = collection["Categoria"];
                movimento.Valor = Convert.ToDecimal(collection["Valor"]);
                movimento.DataMovimentacao = Convert.ToDateTime(collection["DataMovimentacao"]);

                //Grava o Movimento
                Domain.Repositorio.MovimentoFinanceiro retorno = dominio.NovoGasto(movimento);

                //Retorno em Json
                return Json(new { Status = "OK", Data = retorno });
            }
            catch
            {
                return Json(new { Status = "NOK"});
            }
        }

        [HttpGet]
        public ActionResult Analitico()
        {
            return View();
        }

        [HttpGet]
        public JsonResult BuscarDadosOitoUltimos()
        {
            try
            {
                Domain.Dominio.DominioMovimentoFinanceiro dominio = new Domain.Dominio.DominioMovimentoFinanceiro();

                var lista = dominio.GastosUltimoMes().Take(8);

                //A um problema com a formatação do JSON para o tipo de Dados DateTime
                //Basicamente só preciso disto para fazer uma conversão de dados

                var listaConvertida = (from q in lista
                                       select new
                                       {
                                           Categoria = q.Categoria,
                                           DataMovimentacao = q.DataMovimentacao.ToString("dd/MM/yyyy"),
                                           Descricao = q.Descricao,
                                           Id = q.Id,
                                           Valor = q.Valor.ToString("N2")
                                       }).ToList();


                return Json(new { Status = "OK", Data = listaConvertida }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { Status = "NOK", JsonRequestBehavior.AllowGet });
            }
        }

        [HttpGet]
        public JsonResult BuscarDadosAnaliticos()
        {
            try
            {
                Domain.Dominio.DominioMovimentoFinanceiro dominio = new Domain.Dominio.DominioMovimentoFinanceiro();

                var lista = dominio.GastosUltimoMes().OrderBy(x => x.Valor);

                //A um problema com a formatação do JSON para o tipo de Dados DateTime
                //Basicamente só preciso disto para fazer uma conversão de dados

                var listaConvertida = (from q in lista
                                       select new
                                           {
                                               Categoria = q.Categoria,
                                               DataMovimentacao = q.DataMovimentacao.ToString("dd/MM/yyyy"),
                                               Descricao = q.Descricao,
                                               Id = q.Id,
                                               Valor = q.Valor.ToString("N2")
                                           }).ToList();


                return Json(new { Status = "OK", Data = listaConvertida }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { Status = "NOK",JsonRequestBehavior.AllowGet});
            }
        }

        
        public ActionResult Graficos()
        {
            return View();
        }

        [HttpGet]
        public JsonResult BuscarDadosConsolidados()
        {
            try
            {
                Domain.Dominio.DominioMovimentoFinanceiro dominio = new Domain.Dominio.DominioMovimentoFinanceiro();

                List<Domain.Repositorio.MovimentoFinanceiro> lista = dominio.GastosPorCategoriaUltimoMes();

                

                return Json(new { Status = "OK", Data = lista }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { Status = "NOK", JsonRequestBehavior.AllowGet });
            }
        }

        [HttpPost]
        public ActionResult RemoverItem(Guid id)
        {
            try
            {
                Domain.Dominio.DominioMovimentoFinanceiro dominio = new Domain.Dominio.DominioMovimentoFinanceiro();

                dominio.RemoverGasto(id);
                return Json(new { Status = "OK"}, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { Status = "NOK", JsonRequestBehavior.AllowGet });
            }
        }
        
    }
}
