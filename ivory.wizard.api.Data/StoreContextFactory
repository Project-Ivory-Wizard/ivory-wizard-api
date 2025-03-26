using Microsoft.EntityFrameworkCore;
using Microsoft EntityFrameworkCore.Design;

namespace ivory.wizard.api.Data;

public class StoreContextFactory : IDesignTimeDbContextFactory<StoreContext>
{
  public StoreContext CreateDbContext(string[] args)
    {
      var optionsBuilder = new DbContextOptionsBuilder<StoreContext>();

      optionsBuilder.UseSqlite("Data Source=../Registration.sqlite");

      return new StoreContext(optionsBuilder.Options);
    }
}
