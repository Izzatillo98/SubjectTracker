using Microsoft.EntityFrameworkCore;
using STX.EFxceptions.SqlServer;
using SubjectTracker.Models;

namespace SubjectTracker.StorageBrokers
{
    public interface istorageBroker
    {
        Task<Subject> InsertSubjectAsync(Subject subject);
    }
    public class StorageBroker : EFxceptionsContext , istorageBroker
    {
        private IConfiguration configuration;
        public StorageBroker(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.Database.Migrate();       
        }
                  
        public DbSet<Subject> subject{ get; set; }

        public async Task<Subject> InsertSubjectAsync(Subject subject)
        {
           StorageBroker storageBroker = new StorageBroker(this.configuration);
            await storageBroker.subject.AddAsync(subject);
            await storageBroker.SaveChangesAsync();

            return subject;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connetionResult = this.configuration.GetConnectionString("DefaultConnection");            
            optionsBuilder.UseSqlServer(connetionResult); 
        }
    }
}
