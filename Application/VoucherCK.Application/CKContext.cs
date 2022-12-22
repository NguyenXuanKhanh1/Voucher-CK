
using Microsoft.EntityFrameworkCore;

namespace VoucherCK.Application
{
    public class CKContext: DbContext
    {
        public CKContext(DbContextOptions<CKContext> options) :  base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BarCodeRedeems>().HasIndex(u => u.Voucher).IsUnique();
            modelBuilder.Entity<BarCodeRedeems>().HasIndex(u => u.Barcode).IsUnique();

            modelBuilder.UseSerialColumns();
        }

        public DbSet<BarCodeRedeems> BarCodeRedeem { get; set; }
    }
}
