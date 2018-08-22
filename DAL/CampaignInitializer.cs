using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using ProductCampaign.Models;

namespace ProductCampaign.DAL
{
    public class CampaignInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<CampaignContext>
    {
        protected override void Seed(CampaignContext context)
        {
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
            context.SaveChanges();

        }

    }
}