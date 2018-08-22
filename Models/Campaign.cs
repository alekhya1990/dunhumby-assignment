using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProductCampaign.Models;
using System.ComponentModel.DataAnnotations;

namespace ProductCampaign.Models
{
    public class Campaign
    {
        public int CampaignID { get; set; }
        public string CampaignName { get; set; }
        public int ProductID { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }

        public virtual Product Product { get; set; }

    }
}