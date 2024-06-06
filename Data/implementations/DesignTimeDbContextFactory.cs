using System.Diagnostics;
using Data.Implementations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

[DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<DBContext>
{
    public DBContext CreateDbContext(string[] args)
    {
      
        var optionsBuilder = new DbContextOptionsBuilder<DBContext>();
        optionsBuilder.UseSqlServer("Data Source=tcp:emotionsandcareserver.database.windows.net,1433;Initial Catalog=emotionscaredb;Persist Security Info=False;User ID=ArmandoAl;Password=UntilYourLastBreath_;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;");

        return new DBContext(optionsBuilder.Options);
    }

    private string GetDebuggerDisplay()
    {
        return ToString() ?? base.ToString() ?? GetType().ToString();
    }
}
