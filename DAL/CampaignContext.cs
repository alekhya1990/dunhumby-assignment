using ProductCampaign.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ProductCampaign.DAL
{
    public class CampaignContext : DbContext
    {
        public CampaignContext()
            : base("CampaignContext")
        {
        }
        
        public DbSet<Campaign> Campaigns { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}