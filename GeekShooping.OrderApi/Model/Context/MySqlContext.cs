using Microsoft.EntityFrameworkCore;

namespace GeekShooping.OrderApi.Model.Context
{
    public class MySqlContext:DbContext
    {
        public MySqlContext()
        {
            
        }
        public MySqlContext(DbContextOptions<MySqlContext> options):base(options)
        {

        }
         
        public DbSet<OrderHeader> Headers { get; set; }
        public DbSet<OrderDetail> Details { get; set; }
    }
}
