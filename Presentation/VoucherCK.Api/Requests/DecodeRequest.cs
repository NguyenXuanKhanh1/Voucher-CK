using System.ComponentModel.DataAnnotations;

namespace VoucherCK.Api.Requests
{
    public class DecodeRequest
    {
        [Required]
        [MaxLength(16)]
        public string BarCode { get; set; }
    }
}
