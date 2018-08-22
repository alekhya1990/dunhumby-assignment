namespace ProductCampaign.Migrations
{
    using ProductCampaign.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ProductCampaign.DAL.CampaignContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ProductCampaign.DAL.CampaignContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            var product = new List<Product>
            {
                new Product{ID = 1, ProductName = "Coca Cola"},
                new Product{ID = 2, ProductName = "Nike"},
            
            };
            product.ForEach(s => context.Products.Add(s));
            context.SaveChanges();

            var campaigns = new List<Campaign>
            {
                new Campaign{CampaignName="Coca Cola Campaign",ProductID=1,StartDate=DateTime.Parse("2018-08-15"), EndDate=DateTime.Parse("2018-12-31")},
                new Campaign{CampaignName="Coca Christmas Campaign",ProductID=1,StartDate=DateTime.Parse("2018-12-01"), EndDate=DateTime.Parse("2018-12-31")},
                new Campaign{CampaignName="Nike New Year Campaign",ProductID=2,StartDate=DateTime.Parse("2018-12-01"), EndDate=DateTime.Parse("2018-12-31")},
                new Campaign{CampaignName="Nike Christmas Campaign",ProductID=2,StartDate=DateTime.Parse("2018-12-01"), EndDate=DateTime.Parse("2018-12-31")}

            };

            campaigns.ForEach(s => context.Campaigns.Add(s));
            context.Campaigns.AddOrUpdate(p => p.CampaignID);
            context.SaveChanges();
        }
    }
}
