using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using W1.Data;
using W1.Models;

namespace W1.Controllers
{
    [Authorize]
    public class MembersController : Controller
    {
        private readonly PlacidDBContext _context;
        private readonly ApplicationDbContext _appcontext;
        private readonly IScopedService _scopedService;


        private readonly IWebHostEnvironment _appEnvironment;

        public MembersController(ApplicationDbContext appcontext, PlacidDBContext context, IWebHostEnvironment appEnvironment, IScopedService scopedService)
        {
            _context = context;
            _appEnvironment = appEnvironment;
            _scopedService = scopedService;
            _appcontext = appcontext;
        }

        [HttpPost]
        public async Task<IActionResult> VerifyUser(string email, string Code)
        {

            //_transientService.SetPlacidUser(email);
            //_transientService.SetPlacid(true);
            //PlacidSingleton.Instance.SetPlacid(false);

            //PlacidSingleton.Instance.SetPlacidUser(email);

             
            if (String.Compare("placiduser@xyztt.com", email) == 0 && String.Compare("23456", Code) == 0)
            {
                _scopedService.SetPlacidUser(email);
                _scopedService.SetPlacid(true);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                PlacidSingleton.Instance.SetPlacid(false);
                PlacidSingleton.Instance.SetPlacidUser("");
                return RedirectToAction(nameof(Index), "home");
            }

            /*
            if (String.Compare(PlacidSingleton.Instance.GetPlacidUser().ToLower(), "placiduser@xyztt.com") == 0)
            {
                PlacidSingleton.Instance.SetPlacid(true);
                PlacidSingleton.Instance.SetPlacidUser(email);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                PlacidSingleton.Instance.SetPlacid(false);
                PlacidSingleton.Instance.SetPlacidUser("");
                return RedirectToAction(nameof(Index), "home");
            }   
            */
            return RedirectToAction(nameof(Index), "home");
        }

 
        /// <summary>
        /// //////////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>

        [HttpPost]
        public async Task<ActionResult> FileUpload1(IFormFile file, string LotNo, string Price, string AgentLastName, string AgentFirstName, string Email,

            //public async Task<ActionResult> FileUpload1(IFormFile file, int LotNo, string Price, string AgentLastName, string AgentFirstName, string Email,
            string OfficePhone, string CellPhone, string AgentUrl)
        {

            string finalLot = null;

             
            if (String.Compare(_scopedService.GetPlacidUser().ToLower(), "placiduser@xyztt.com") != 0)
            {
                return RedirectToAction(nameof(Index), "home");
            } 


            String OfficePhone1 = OfficePhone.Insert(3, "-");
            string OfficeFinal = OfficePhone1.Insert(7, "-");

            String CellPhone1 = CellPhone.Insert(3, "-");
            string CellPhoneFinal = CellPhone1.Insert(7, "-");


            string currency = "$" + " " + Price + ".00";

            if (ModelState.IsValid)
            {
                string finalLot1 = null;

                /*
                if (LotNo > 0 || LotNo <= 9)
                {
                    finalLot1 = "0" + LotNo.ToString();
                }
                else
                {
                    finalLot1 = LotNo.ToString();
                }  */
                await UploadFile(file, LotNo);
                Member member0 = new Member();
                member0.LotNo = LotNo;
                member0.Price = currency;
                member0.AgentLastName = AgentLastName;
                member0.AgentFirstName = AgentFirstName;
                member0.Email = Email;
                member0.OfficePhone = OfficeFinal;
                member0.CellPhone = CellPhoneFinal;
                member0.ImageName = file.FileName;
                member0.AgentUrl = AgentUrl;

                _context.Add(member0);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                Member member1 = new Member();
                member1.LotNo = LotNo;
                member1.Price = Price;
                member1.AgentLastName = AgentLastName;
                member1.AgentFirstName = AgentFirstName;
                member1.Email = Email;
                member1.OfficePhone = OfficePhone;
                member1.CellPhone = CellPhone;
                member1.ImageName = file.FileName;
                return View(member1);
            }
 
        }

        public async Task<bool> UploadFile(IFormFile file, string lotNo)
        {
            string path = "";
            bool iscopied = false;

 

            try
            {
                if (file.Length > 0)
                {
                    // var fnext = Path.GetExtension(file.FileName);
                    // string fname = LotNo + "." + fnext;
                    //  verify the 
                    string filename = lotNo + Path.GetExtension(file.FileName);
                    path = _appEnvironment.WebRootPath;
                    // Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "Upload"));
                    var tt = path + "\\" + filename;
                    using (var filestream = new FileStream(path + "\\" + filename, FileMode.Create))  //        //Path.Combine(path, filename), FileMode.Create))
                    {
                        await file.CopyToAsync(filestream);
                    }
                    iscopied = true;
                }
                else
                {
                    iscopied = false;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return iscopied;
        }


        /// <summary>
        /// /////////////////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>
        







        // GET: Members
        public async Task<IActionResult> Index()
        {
          //  if (String.Compare(_scopedService.GetPlacidUser().ToLower(), "placiduser@xyztt.com") != 0)
          //  {
          //      return RedirectToAction(nameof(Index), "home");
          //  }



            return View(await _context.Members.ToListAsync());
        }

        // GET: Members/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            /*
           if (String.Compare(PlacidSingleton.Instance.GetPlacidUser().ToLower(), "placiduser@xyztt.com") != 0)
           {
               return RedirectToAction(nameof(Index), "home");
           }*/


            if (id == null)
            {
                return NotFound();
            }

            var member = await _context.Members
                .FirstOrDefaultAsync(m => m.ID == id);
            if (member == null)
            {
                return NotFound();
            }
            //PlacidSingleton.Instance.SetPlacid(false);
            return View(member);
        }

        // GET: Members/Create
        public IActionResult Create()
        {
            /*
            if (String.Compare(PlacidSingleton.Instance.GetPlacidUser().ToLower(), "placiduser@xyztt.com") != 0)
            {
                return RedirectToAction(nameof(Index), "home");
            }*/
            return View();
        }

        // POST: Members/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,LotNo,Price,AgentLastName,AgentFirstName,Email,OfficePhone,CellPhone,AgentUrl,ImageName")] Member member)
        {
            /*
            if (String.Compare(PlacidSingleton.Instance.GetPlacidUser().ToLower(), "placiduser@xyztt.com") != 0)
            {
                return RedirectToAction(nameof(Index), "home");
            }*/

            if (ModelState.IsValid)
            {
                _context.Add(member);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(member);
        }

        // GET: Members/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            /*
           if (String.Compare(PlacidSingleton.Instance.GetPlacidUser().ToLower(), "placiduser@xyztt.com") != 0)
           {
               return RedirectToAction(nameof(Index), "home");
           }*/

            if (id == null)
            {
                return NotFound();
            }

            var member = await _context.Members.FindAsync(id);
            if (member == null)
            {
                return NotFound();
            }
           // PlacidSingleton.Instance.SetPlacid(false);
            return View(member);
        }

        // POST: Members/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,LotNo,Price,AgentLastName,AgentFirstName,Email,OfficePhone,CellPhone,AgentUrl,ImageName")] Member member)
        {
            /*
           if (String.Compare(PlacidSingleton.Instance.GetPlacidUser().ToLower(), "placiduser@xyztt.com") != 0)
           {
               return RedirectToAction(nameof(Index), "home");
           }*/

            if (id != member.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(member);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MemberExists(member.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(member);
        }

        // GET: Members/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            /*
            if (String.Compare(PlacidSingleton.Instance.GetPlacidUser().ToLower(), "placiduser@xyztt.com") != 0)
            {
                return RedirectToAction(nameof(Index), "home");
            }*/


            if (id == null)
            {
                return NotFound();
            }

            var member = await _context.Members
                .FirstOrDefaultAsync(m => m.ID == id);
            if (member == null)
            {
                return NotFound();
            }
           // PlacidSingleton.Instance.SetPlacid(false);
            return View(member);
        }

        // POST: Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            /*
            if (String.Compare(PlacidSingleton.Instance.GetPlacidUser().ToLower(), "placiduser@xyztt.com") != 0)
            {
                return RedirectToAction(nameof(Index), "home");
            }*/


            var member = await _context.Members.FindAsync(id);
            if (member != null)
            {
                _context.Members.Remove(member);
            }

            await _context.SaveChangesAsync();

            //PlacidSingleton.Instance.SetPlacid(false);

            return RedirectToAction(nameof(Index));
        }

        private bool MemberExists(int id)
        {
            return _context.Members.Any(e => e.ID == id);
        }
    }
}
