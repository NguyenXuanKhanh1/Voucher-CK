
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
            modelBuilder.UseSerialColumns();
        }

        public DbSet<BarCodeRedeems> BarCodeRedeem { get; set; }
    }
}
