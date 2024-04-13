using Entities.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

public class DbContextCaisse : IDbContextCaisse, IDisposable
{
    public DbContextCaisse(IOptions<DbContextSettings> settings)
    {
        var option =new DbContextOptionsBuilder<CaisseDbContext>().UseNpgsql(settings.Value.DbConnectionString,
        npgsqlOptionsAction:s=>{
            s.EnableRetryOnFailure(maxRetryCount:10,maxRetryDelay:TimeSpan.FromSeconds(30),errorCodesToAdd:null);
        }
        ).EnableSensitiveDataLogging().Options;
        DbContext=new CaisseDbContext(option);
        DbContext.ChangeTracker.QueryTrackingBehavior=QueryTrackingBehavior.NoTracking;

        
    }
    public CaisseDbContext DbContext {get; private set;}

    public void Dispose()=>DbContext?.Dispose();
    ~DbContextCaisse()=>Dispose();
}