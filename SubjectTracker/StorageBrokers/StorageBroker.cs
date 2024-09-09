using Microsoft.EntityFrameworkCore;
using STX.EFxceptions.SqlServer;

namespace SubjectTracker.StorageBrokers
{
    public interface IStorageBroker
    {

    }
    public class StorageBroker : EFxceptionsContext , IStorageBroker
    {
        private IConfiguration configuration;
        //public StorageBroker(IConfiguration configuration)
        //{
        //    this.configuration = configuration;
        //}
        public StorageBroker() 
        {
            this.Database.Migrate();       
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connetionResult = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SubjectTrackerCoreApiDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

                //this.configuration.GetConnectionString("DefaultConnection");

            optionsBuilder.UseSqlServer(connetionResult); 
        }
    }
}
