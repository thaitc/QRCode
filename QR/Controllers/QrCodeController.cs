using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QR.Models.Dto;
using QR.Models;
using QR.Service.Interface;

namespace QR.Controllers
{
    public class QrCodeController : Controller
    {
        private readonly IQrCodeService _qrCodeInterface;

        public QrCodeController(IQrCodeService qrCodeInterface)
        {
            _qrCodeInterface = qrCodeInterface;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult CreateQRCode()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateQRCode(QRCodeModel qRCode)
        {
            var genQr = new GenQrDto();
            genQr.accountNo = qRCode.AccountNumber;
            genQr.accountName = qRCode.AccountName;
            genQr.acqId = "970423";
            genQr.addInfo = "Xin chao VN";
            genQr.amount = qRCode.TransferAmount.ToString();
            genQr.template = "compact";
            var genVietQr = _qrCodeInterface.GenQrCode(genQr);
            if (genVietQr == null)
            {
                return View();
            }
            string base64 = genVietQr.data.qrDataURL;
            ViewBag.QrCodeUri = base64;
            return View();
        }
    }
}
