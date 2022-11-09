using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace QR.Models
{
    public class QRCodeModel
    {

        [Display(Name = "Số tài khoản")]
        public string AccountNumber { get; set; }

        [Display(Name = "Tên chủ tài khoản")]
        public string AccountName { get; set; }

        [Display(Name = "Số tiền chuyển")]
        public decimal TransferAmount { get; set; }

        [Display(Name = "Nội dung chuyển khoản")]
        public string Content { get; set; }

    }
}