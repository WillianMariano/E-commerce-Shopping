using Microsoft.EntityFrameworkCore;

namespace GeekShooping.CouponApi.Model.Context
{
    public class MySqlContext:DbContext
    {
        public MySqlContext()
        {
            
        }
        public MySqlContext(DbContextOptions<MySqlContext> options):base(options)
        {

        }

        public DbSet<Coupon> Coupons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Coupon>().HasData(new Coupon
            {
                Id = 1,
                CouponCode = "TESTE1",
                DiscountAmount = 10
            }); 
            modelBuilder.Entity<Coupon>().HasData(new Coupon
            {
                Id = 2,
                CouponCode = "TESTE3",
                DiscountAmount = 15
            });
        }
    }
}
