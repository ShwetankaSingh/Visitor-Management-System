using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using VisitorManagementSystemMVC.Models;

namespace VisitorManagementSystemMVC.Controllers
{
   
    public class VisitorController : Controller
    {
       
        private readonly ApplicationDbContext dbContext;
        private readonly IWebHostEnvironment webHostEnvironment;
        //private readonly INotyfService _notfy;

        public Visitor Visitor { get; private set; }

        public VisitorController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            dbContext = context;
            webHostEnvironment = hostEnvironment;
           
        }

      
       
        [Authorize(Roles ="User")]
        public async Task<IActionResult> Index(string sortOrder,DateTime? SearchStart,DateTime? SearchEnd)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["StartDate"] = SearchStart;
            ViewData["EndDate"] = SearchEnd;

            var visitors = from v in dbContext.Visitors
                           select v;
            if(SearchStart.HasValue && SearchEnd.HasValue)
            {
                visitors = visitors.Where(v => (v.VisitedDate >= SearchStart && v.VisitedDate <= SearchEnd));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    visitors = visitors.OrderByDescending(v => v.Name);
                    break;
                case "Date":
                    visitors = visitors.OrderBy(v => v.VisitedDate);
                    break;
                case "date_desc":
                    visitors = visitors.OrderByDescending(v => v.VisitedDate);
                    break;
                default:
                    visitors = visitors.OrderBy(v => v.Name);
                    break;
            }
            //returns new query i.e returned entities will not be cached 
            return View(await visitors.AsNoTracking().ToListAsync());
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Approval()
        {
            var visitor =dbContext.Visitors.Where(v => v.Approval == false && v.Rejected == false);

            return View(await visitor.ToListAsync());
        }


        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Approved()
        {
            var visitor = dbContext.Visitors.Where(v => v.Approval == true);

            return View(await visitor.ToListAsync());
        }


        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Rejected()
        {
            var visitor = dbContext.Visitors.Where(v => v.Rejected == true);

            return View(await visitor.ToListAsync());
        }


        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetApprove(int? id)
        {
            var visitor = await dbContext.Visitors.FindAsync(id);
            visitor.Approval = true;
            dbContext.Update(visitor);
            await dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Approval));
        }


        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetReject(int? id)
        {
            var visitor = await dbContext.Visitors.FindAsync(id);
            visitor.Rejected = true;
            dbContext.Update(visitor);
            await dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Approval));
        }

        [Authorize(Roles = "User")]
        public IActionResult New()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> New(VisitorViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = UploadedFile(model);

                Visitor visi = new Visitor
                {
                    Name = model.Name,
                    PhoneNumber = model.PhoneNumber,
                    Email = model.Email,
                    VisitedDate = model.VisitedDate,
                    Approval = model.Approval,
                    ProfilePicture = uniqueFileName,
                };
                dbContext.Add(visi);
                await dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [Authorize(Roles = "Admin,User")]
        private string UploadedFile(VisitorViewModel model)
        {
            string uniqueFileName = null;

            if (model.ProfileImage != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ProfileImage.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.ProfileImage.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

        [AllowAnonymous]
        public IActionResult Privacy()
        {
            return View();
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Authorize(Roles = "Admin,User")]
        public IActionResult Details(int? id)
        {
           // throw new Exception("Exception in Details view");
            Visitor = new Visitor();


            Visitor = dbContext.Visitors.FirstOrDefault(u => u.Id == id);
            if (Visitor == null)
            {
                Response.StatusCode = 404;
                return View("VisitorNotFound",id.Value);
            }
            return View(Visitor);
        }

        [Authorize(Roles = "User")]
        public async Task<IActionResult> Edit(int id)
        {
            var visitor = await dbContext.Visitors.FindAsync(id);
            var visitorViewModel = new VisitorViewModel()
            {
                Id = visitor.Id,
                Name = visitor.Name,
                PhoneNumber = visitor.PhoneNumber,
                Email = visitor.Email,
                VisitedDate = visitor.VisitedDate,
                ProfilePicture = visitor.ProfilePicture
            };

            if (visitor == null)
            {
                return NotFound();
            }
            return View(visitorViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Edit(int id, VisitorViewModel model)
        {
            if (ModelState.IsValid)
            {
                var visitor = await dbContext.Visitors.FindAsync(model.Id);
                visitor.Name = model.Name;
                visitor.PhoneNumber = model.PhoneNumber;
                visitor.Email = model.Email;
              

                visitor.VisitedDate = model.VisitedDate;

                if (model.ProfileImage != null)
                {
                    if (model.ProfilePicture != null)
                    {
                        string filePath = Path.Combine(webHostEnvironment.WebRootPath, "Images", model.ProfilePicture);
                        System.IO.File.Delete(filePath);
                    }

                    visitor.ProfilePicture = UploadedFile(model);
                }
                dbContext.Update(visitor);
                await dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        /*
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var visitor = await dbContext.Visitors.FindAsync(id);
            var CurrentImage = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", visitor.ProfilePicture);
            dbContext.Visitors.Remove(visitor);
            if (await dbContext.SaveChangesAsync() > 0)
            {
                if (System.IO.File.Exists(CurrentImage))
                {
                    System.IO.File.Delete(CurrentImage);
                }
            }
            return RedirectToAction(nameof(Index));
        } */

        [Authorize(Roles = "User")]
        public async Task<IActionResult> Delete(int? id)
        {
            var visitor = await dbContext.Visitors.FindAsync(id);
            var CurrentImage = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", visitor.ProfilePicture);
            dbContext.Visitors.Remove(visitor);
            if (await dbContext.SaveChangesAsync() > 0)
            {
                if (System.IO.File.Exists(CurrentImage))
                {
                    System.IO.File.Delete(CurrentImage);
                }
            }
            return RedirectToAction(nameof(Index));
            //return RedirectToPage("Index");
        }

    }
}
