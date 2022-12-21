using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoucherCK.Application
{
    [Table("BarCodeRedeems")]
    public class BarCodeRedeems
    {
        public static object Lock = new object();
        public BarCodeRedeems(string id, string barcode, string voucher, DateTime redeemAt)
        {
            Id = id;
            Barcode = barcode;
            Voucher = voucher;
            RedeemAt = redeemAt;
        }

        [Key]
        [Column("id")]
        public string Id { get; set; }
        [Column("Barcode")]
        public string Barcode { get; set; }
        [Column("Voucher")]
        public string Voucher { get; set; }
        [Column("RedeemAt")]
        public DateTime RedeemAt { get; set; }

    }
}
