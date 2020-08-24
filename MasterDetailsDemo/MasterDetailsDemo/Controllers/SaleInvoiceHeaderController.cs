using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using MasterDetailsDemo.DAL;
using MasterDetailsDemo.Models;
using MasterDetailsDemo.ViewModel;

namespace MasterDetailsDemo.Controllers {
    public class SaleInvoiceHeaderController : Controller
    {
        private MasterDetailDemoContext db = new MasterDetailDemoContext();

        // GET: SaleInvoiceHeader
        public ActionResult Index(){
            var saleInvoiceHeaders = db.SaleInvoiceHeaders.Include(s => s.Customer);
            return View(saleInvoiceHeaders.ToList());
        }

        // GET: SaleInvoiceHeader/Details/5
        public ActionResult Details(string id){
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SaleInvoiceHeader saleInvoiceHeader = db.SaleInvoiceHeaders.Find(id);
            if (saleInvoiceHeader == null)
            {
                return HttpNotFound();
            }
            return View(saleInvoiceHeader);
        }

        // GET: SaleInvoiceHeader/Create
        public ActionResult Create() {
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "Name");
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Create(SaleInvoiceHeaderVM saleInvoiceHeaderVM, string saleInvoiceItem) {
            if (ModelState.IsValid)  {
                SaleInvoiceHeader saleInvoiceHeader = new SaleInvoiceHeader {
                    SaleInvoiceHeaderId = Guid.NewGuid().ToString(),
                    Code = saleInvoiceHeaderVM.Code,
                    CustomerId = saleInvoiceHeaderVM.CustomerId,
                    TotalAmount = saleInvoiceHeaderVM.TotalAmount
                };
               
                List<SaleInvoiceItemVM> saleInvoiceItemVM = new JavaScriptSerializer().Deserialize<List<SaleInvoiceItemVM>>(saleInvoiceItem);

                List<SaleInvoiceItem> itemlist = new List<SaleInvoiceItem>();
                foreach (var saleItemVM in saleInvoiceItemVM)  {
                    SaleInvoiceItem siItem = new SaleInvoiceItem();
                    siItem.SaleInvoiceItemId = Guid.NewGuid().ToString();
                    siItem.SaleInvoiceHeaderId = saleInvoiceHeader.SaleInvoiceHeaderId;
                    siItem.ProductId = saleItemVM.ProductId;
                    siItem.Quantity = saleItemVM.Quantity;
                    siItem.Amount = saleItemVM.Amount;
                    itemlist.Add(siItem);
                }
                if(SavetoDatabase(saleInvoiceHeader, itemlist)){
                }             
                return Json("success");
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "Name", saleInvoiceHeaderVM.CustomerId);
            return Json("fail");
        }
        private bool SavetoDatabase(SaleInvoiceHeader saleInvoiceHeader, List<SaleInvoiceItem> itemlist){
            try{
                db.SaleInvoiceHeaders.Add(saleInvoiceHeader);
                db.SaleInvoiceItems.AddRange(itemlist);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex){
                return false;
            }
        }
        // GET: SaleInvoiceHeader/Edit/5
        public ActionResult Edit(string id){
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SaleInvoiceHeader saleInvoiceHeader = db.SaleInvoiceHeaders.Find(id);
            if (saleInvoiceHeader == null){
                return HttpNotFound();
            }
            SaleInvoiceHeaderVM saleHeaderVM = new SaleInvoiceHeaderVM();
            saleHeaderVM.SaleInvoiceHeaderId = saleInvoiceHeader.SaleInvoiceHeaderId;
            saleHeaderVM.Code = saleInvoiceHeader.Code;
            saleHeaderVM.CustomerId = saleInvoiceHeader.CustomerId;
            saleHeaderVM.TotalAmount = saleInvoiceHeader.TotalAmount;
            
            saleHeaderVM.saleInvoiceItemVMs = db.SaleInvoiceItems.Where(w => w.SaleInvoiceHeaderId == id).ToList()
                                                 .Select((s, index)=> new SaleInvoiceItemVM{
                                                     Index = index,
                                                     SaleInvoiceItemId = s.SaleInvoiceItemId,
                                                     ProductId = s.ProductId,
                                                     ProductName = s.Product.Name,
                                                     Price = s.Product.Price,
                                                     Quantity = s.Quantity,
                                                     Amount = s.Amount,
                                                  }).ToList();
            
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "Name",saleHeaderVM.CustomerId);
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "Name");
            return View(saleHeaderVM);
        }
        [HttpPost]
        public ActionResult Edit(SaleInvoiceHeaderVM saleInvoiceHeaderVM, string saleInvoiceItem){
            if (ModelState.IsValid){
                SaleInvoiceHeader saleInvoiceHeader = db.SaleInvoiceHeaders.Find(saleInvoiceHeaderVM.SaleInvoiceHeaderId);
                saleInvoiceHeader.SaleInvoiceHeaderId = saleInvoiceHeaderVM.SaleInvoiceHeaderId;
                saleInvoiceHeader.Code = saleInvoiceHeaderVM.Code;
                saleInvoiceHeader.CustomerId = saleInvoiceHeaderVM.CustomerId;
                saleInvoiceHeader.TotalAmount = saleInvoiceHeaderVM.TotalAmount;

                List<SaleInvoiceItem> saleInvoiceItems = db.SaleInvoiceItems.Where(w => w.SaleInvoiceHeaderId == saleInvoiceHeaderVM.SaleInvoiceHeaderId).ToList();
                foreach(var item in saleInvoiceItems){
                    db.SaleInvoiceItems.Remove(item);
                }
                List<SaleInvoiceItemVM> saleInvoiceItemVM = new JavaScriptSerializer().Deserialize<List<SaleInvoiceItemVM>>(saleInvoiceItem);

                List<SaleInvoiceItem> itemlist = new List<SaleInvoiceItem>();
                foreach (var saleItemVM in saleInvoiceItemVM){
                    SaleInvoiceItem siItem = new SaleInvoiceItem();
                    siItem.SaleInvoiceItemId = Guid.NewGuid().ToString();
                    siItem.SaleInvoiceHeaderId = saleInvoiceHeader.SaleInvoiceHeaderId;
                    siItem.ProductId = saleItemVM.ProductId;
                    siItem.Quantity = saleItemVM.Quantity;
                    siItem.Amount = saleItemVM.Amount;
                    itemlist.Add(siItem);
                }
                if (UpdateToDatabase(itemlist)){
                    return Json("success");
                }              
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "Name", saleInvoiceHeaderVM.CustomerId);
            return Json("fail");
        }

        private bool UpdateToDatabase(List<SaleInvoiceItem> itemlist){
            try{
                db.SaleInvoiceItems.AddRange(itemlist);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex){
                return false;
            }
        }
        
        // GET: SaleInvoiceHeader/Delete/5
        public ActionResult Delete(string id) {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SaleInvoiceHeader saleInvoiceHeader = db.SaleInvoiceHeaders.Find(id);
            if (saleInvoiceHeader == null) {
                return HttpNotFound();
            }
            return View(saleInvoiceHeader);
        }

        // POST: SaleInvoiceHeader/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id) {
            SaleInvoiceHeader saleInvoiceHeader = db.SaleInvoiceHeaders.Find(id);
            db.SaleInvoiceHeaders.Remove(saleInvoiceHeader);
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
    }
}
