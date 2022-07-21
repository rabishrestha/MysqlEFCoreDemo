using Microsoft.EntityFrameworkCore;

namespace MysqlEFCoreDemo.Data
{
    public class MyDbContext: DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options): base(options)
        {

        }
        public DbSet<PersonModel> Person { get; set; }
        public DbSet<ContactModel> Contact { get; set; }
    }
}
