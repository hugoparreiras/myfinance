using Microsoft.AspNetCore.Mvc;
using myfinance_web_netcore.Models;
using myfinance_web_netcore.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace myfinance_web_netcore.Controllers
{
    [Route("[controller]")]
    public class TransacaoController : Controller
    {
        private readonly ILogger<TransacaoController> _logger;
        private readonly ITransacaoService _service;
        private readonly IPlanoContaService _accountPlanService;

        public TransacaoController(ILogger<TransacaoController> logger,
        ITransacaoService service, IPlanoContaService accountPlanService)
        {
            _logger = logger;
            _service = service;
            _accountPlanService = accountPlanService;
        }

        [HttpGet]
        [Route("Index")]
        public IActionResult Index()
        {
            ViewBag.ListTransaction = _service.GetAll();
            return View();
        }

        [HttpGet]
        [Route("Register")]
        [Route("Register/{id}")]
        public IActionResult Register(int? id)
        {
            var model = new TransacaoModel();

            if (id != null)
            {
                model = _service.Get((int)id);
            } else {
                model.DateTransaction = DateTime.Now;
            }

            var listAccountPlan = _accountPlanService.GetAll();
            model.AccountPlans = new SelectList(listAccountPlan, "Id", "Description");            
            
            return View(model);
        }

        [HttpPost]
        [Route("Register")]
        [Route("Register/{id}")]
        public IActionResult Register(TransacaoModel model)
        {
            _service.Save(model);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            _service.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("Relatorio")]
        public IActionResult Relatorio()
        {
            RelatorioTransacaoModel model = new RelatorioTransacaoModel();
            model.StartDate = DateTime.Now;
            model.EndDate = DateTime.Now;
            return View(model);
        }

        [HttpPost]
        [Route("Relatorio")]
        public IActionResult Relatorio(RelatorioTransacaoModel model)
        {
            if (model.StartDate != null || model.EndDate != null)
            {
                model = _service.GetAllByPeriod(model.StartDate, model.EndDate);
            }
            ViewBag.ReceitasBag = model.CountIncomeTransactions.ToString();
            ViewBag.DespesasBag = model.CountExpensesTransactions.ToString();
            return View(model);
        }
    }
}