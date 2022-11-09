using Newtonsoft.Json;
using QR.Helper;
using QR.Models.Dto;
using QR.Models.Response;
using QR.Service.Interface;

namespace QR.Service.Implement
{
    public class QrCodeService: IQrCodeService
    {
        public VietQrReponse GenQrCode(GenQrDto genQr)
        {
            var path = $"https://api.vietqr.io/v2/generate";
            var result = CustomHttpClient.Post<VietQrReponse>(path, tokenName: "", token: "", body: JsonConvert.SerializeObject(genQr));
            return result;
        }
    }
}
