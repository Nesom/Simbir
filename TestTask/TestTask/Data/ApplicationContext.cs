using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestTask.Data
{
    public class ApplicationContext : DbContext, IDatabase
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base (options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Record> Records { get; set; }

        public void AddToDb(Dictionary<string, int> records)
        {
            foreach (var record in records)
            {
                Records.Add(new Record { Word = record.Key, Count = record.Value });
            }
            SaveChanges();
        }

        public List<Record> GetRecords()
        {
            return Records.ToList();
        }
    }
}
