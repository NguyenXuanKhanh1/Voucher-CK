using System.Text.Json.Serialization;

namespace VoucherCK.Application.DTOs
{
    public class VoucherResultDto
    {
        public string BarCode { get; set; }
        [JsonPropertyName("rtrStore")]
        public string StoreCode { get; set; }
        [JsonPropertyName("rtrPrizeCode")]
        public string PrizeCode { get; set; }
        [JsonPropertyName("rtrNPlay")]
        public int IsWarrior { get; set; }
        [JsonPropertyName("result")]
        public int Result { get; set; }
    }
}
