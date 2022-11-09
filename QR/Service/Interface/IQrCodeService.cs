using Newtonsoft.Json;
using QR.Helper;
using QR.Models;
using QR.Models.Dto;
using QR.Models.Response;

namespace QR.Service.Interface
{
    public interface IQrCodeService
    {
        VietQrReponse GenQrCode(GenQrDto genQr);
    }
}
