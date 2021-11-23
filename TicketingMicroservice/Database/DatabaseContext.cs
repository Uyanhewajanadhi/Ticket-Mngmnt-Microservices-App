using Microsoft.EntityFrameworkCore;

namespace TicketingMicroservice.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }
        public DbSet<Ticketing> Ticketings { get; set; }
    }
}

