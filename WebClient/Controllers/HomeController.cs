using System;
using System.Web.Mvc;
using BitcoinLib.Responses;
using BitcoinLib.Services.Coins.Bitcoin;

namespace WebClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBitcoinService _bitcoinService;

        // GET: /Home/
        public HomeController()
        {
            _bitcoinService = new BitcoinService(useTestnet: true);
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
            DecodeRawTransactionResponse transaction = _bitcoinService.GetPublicTransaction(txId);
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
