namespace QR.Models.Response
{
    public class VietQrReponse
    {
        public string code { get; set; }
        public string desc { get; set; }
        public Data data { get; set; }
    }
    public class Data
    {
        public int acqId { get; set; }
        public string accountName { get; set; }
        public string qrCode { get; set; }
        public string qrDataURL { get; set; }
    }
}
