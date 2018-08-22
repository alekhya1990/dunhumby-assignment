using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProductCampaign.Models;
using ProductCampaign.DAL;
using PagedList;

namespace ProductCampaign.Controllers
{
    public class CampaignController : Controller
    {
        private CampaignContext db = new CampaignContext();

        // GET: /Campaign/
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            db = new CampaignContext();

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var campaigns = from c in db.Campaigns
                            select c;

            if (!String.IsNullOrEmpty(searchString))
            {
                if (searchString == "0")
                    campaigns = campaigns.Where(s => s.StartDate > DateTime.Now || s.EndDate < DateTime.Now);
                else if (searchString == "1")
                    campaigns = campaigns.Where(s => s.StartDate <= DateTime.Now && s.EndDate >= DateTime.Now);
            }

            switch (sortOrder)
            {
                case "name_desc":
                    campaigns = campaigns.OrderByDescending(s => s.CampaignName);
                    break;
                case "Date":
                    campaigns = campaigns.OrderBy(s => s.StartDate);
                    break;
                case "date_desc":
                    campaigns = campaigns.OrderByDescending(s => s.StartDate);
                    break;
                default:
                    campaigns = campaigns.OrderBy(s => s.CampaignName);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(campaigns.ToPagedList(pageNumber, pageSize));

        }

        // GET: /Campaign/Create
        public ActionResult Create()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            var productList = db.Products.ToList();

            ViewData["ProductList"] = ProductList();


            return View();
        }

        // POST: /Campaign/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CampaignID,CampaignName,ProductID,StartDate,EndDate")] Campaign campaign)
        {
            try
            {
                ViewData["ProductList"] = ProductList();

                if (ModelState.IsValid)
                {
                    campaign.Product = db.Products.Where(p => p.ID == campaign.ProductID).FirstOrDefault();
                    db.Campaigns.Add(campaign);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes");
            }

            return View(campaign);
        }

        // GET: /Campaign/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewData["ProductList"] = ProductList();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Campaign campaign = db.Campaigns.Find(id);
            if (campaign == null)
            {
                return HttpNotFound();
            }
            return View(campaign);
        }

        // POST: /Campaign/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CampaignID,CampaignName,ProductID,StartDate,EndDate")] Campaign campaign)
        {
            ViewData["ProductList"] = ProductList();
            if (ModelState.IsValid)
            {
                db.Entry(campaign).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(campaign);
        }

        // GET: /Campaign/Delete/5
        public ActionResult Delete(int? id)
        {
            ViewData["ProductList"] = ProductList();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Campaign campaign = db.Campaigns.Find(id);
            if (campaign == null)
            {
                return HttpNotFound();
            }
            return View(campaign);
        }

        // POST: /Campaign/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ViewData["ProductList"] = ProductList();
            Campaign campaign = db.Campaigns.Find(id);
            db.Campaigns.Remove(campaign);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public List<SelectListItem> ProductList()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            var productList = db.Products.ToList();

            foreach (var product in productList)
            {
                SelectListItem item = new SelectListItem();
                item.Text = product.ProductName;
                item.Value = product.ID.ToString();

                list.Add(item);
            }

            return list;

        }
    }
}
