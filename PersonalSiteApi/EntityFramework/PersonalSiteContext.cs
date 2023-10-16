using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PersonalSiteApi.EntityFramework.Classes;

namespace PersonalSiteApi.EntityFramework
{
    public class PersonalSiteContext : DbContext
    {
        public DbSet<LanguageDB> Languages { get; set; }
        public DbSet<PageDB> Pages { get; set; }
        public DbSet<PageContentDB> PageContent { get; set; }
        public DbSet<PageDetailDB> PageDetails { get; set; }
        public DbSet<ProjectDB> Projects { get; set; }
        public DbSet<ProjectContentDB> ProjectContent { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
            options.UseSqlServer(configuration.GetValue<string>("ConnectionString"));
        }
    }
}
