using System;
using System.Web.Mvc;
using BitcoinLib.Responses;
using BitcoinLib.Services;

namespace WebClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBitcoinService _bitcoinService;

        // GET: /Home/
        public HomeController()
        {
            _bitcoinService = new BitcoinService();
        }

        public ActionResult Index()
        {
            return View("Index");
        }

        [HttpGet]
        public Decimal GetBalanceInWallet()
        {
            return _bitcoinService.GetBalance();
        }

        [HttpGet]
        public JsonResult GetInformationAboutTransaction(String txId)
        {
            DecodeRawTransactionResponse transaction = _bitcoinService.GetTransaction(txId);
            return Json(transaction, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetBlockInfo(String blockhashId)
        {
            GetBlockResponse transaction = _bitcoinService.GetBlock(blockhashId);
            return Json(transaction, JsonRequestBehavior.AllowGet);
        }
    }
}
