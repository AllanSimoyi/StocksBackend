using Microsoft.EntityFrameworkCore;

class StockDb : DbContext
{
  public StockDb(DbContextOptions<StockDb> options)
      : base(options) { }

  public DbSet<Stock> Stocks => Set<Stock>();
}