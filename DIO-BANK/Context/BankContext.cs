using DIO_BANK.Models;
using Microsoft.EntityFrameworkCore;

namespace DIO_BANK.Context
{
    public class BankContext: DbContext
    {

        public BankContext(DbContextOptions<BankContext> options) : base(options)
        {
            Database.Migrate();
        }
        public DbSet<Conta> Contas { get; set; }

    }
}
