using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Threading.Tasks;
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



        public bool checkfile(IFormFile file)
        {             // check the file 
            if (file != null && file.Length > 0)
            {
                // var fnext = Path.GetExtension(file.FileName);
                // string fname = LotNo + "." + fnext;
                //  verify the 
                return true;
            }
            return false;
        }



        /*
        [HttpPost]
        public async Task<IActionResult> VerifyUser(string email, string Code)
        {
           
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
   
            return RedirectToAction(nameof(Index), "home");
        }
        */


        /// <summary>
        /// //////////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>

        [HttpPost]
        public async Task<ActionResult> FileUpload1(IFormFile file, string TypeSell, string LotNo, string Price, string AgentLastName, string AgentFirstName, string Email,

            //public async Task<ActionResult> FileUpload1(IFormFile file, int LotNo, string Price, string AgentLastName, string AgentFirstName, string Email,
            string OfficePhone, string CellPhone, string AgentUrl)
        {

            #region junk
            // return StatusCode(400, "some text, json, etc.");
            //return BadRequest("Validation failed."); // 400 Bad Request

            // return Ok(new { message = "Data retrieved successfully!" });

            //if (String.Compare(_scopedService.GetPlacidUser().ToLower(), "placiduser@xyztt.com") != 0)
            //{
            //    return RedirectToAction(nameof(Index), "home");
            //} 
            // return RedirectToAction(nameof(Index), "home"); if (ModelState.IsValid)

            if (AgentUrl == null || AgentUrl.Length == 0)
            {
                AgentUrl = "noval";
            }
            #endregion

            // return StatusCode(404, "some text, json, etc.");


            var memberx = await _context.Members
               .FirstOrDefaultAsync(m => m.LotNo == LotNo);
            if (memberx != null)
            {
                return StatusCode(404, "some text, json, etc.");
            }
            else
            {
                if (ModelState.IsValid)
                {

                    String OfficePhone1 = OfficePhone.Insert(3, "-");
                    string OfficeFinal = OfficePhone1.Insert(7, "-");

                    String CellPhone1 = CellPhone.Insert(3, "-");
                    string CellPhoneFinal = CellPhone1.Insert(7, "-");
                    string currency = Price;

                    // try a no await 
                    await UploadFile(file, LotNo);
                    Member member0 = new Member();
                    member0.TypeSell = TypeSell;
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
                    member1.TypeSell = "";
                    member1.LotNo = "";
                    member1.Price = "";
                    member1.AgentLastName = "";
                    member1.AgentFirstName = "";
                    member1.Email = "";
                    member1.OfficePhone = "";
                    member1.CellPhone = "";
                    member1.ImageName = "";
                    return View(member1);
                }
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



        // GET: Members
        public async Task<IActionResult> Index()
        {
            //  if (String.Compare(_scopedService.GetPlacidUser().ToLower(), "placiduser@xyztt.com") != 0)
            //  {
            //      return RedirectToAction(nameof(Index), "home");
            //  }

            List<Member> members = await _context.Members.ToListAsync();

            return View(await _context.Members.ToListAsync());
        }

        // GET: Members/Details/5
        public async Task<IActionResult> Details(int? id)
        {

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
            return View();
        }

        // POST: Members/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,LotNo,Price,AgentLastName,AgentFirstName,Email,OfficePhone,CellPhone,AgentUrl,ImageName")] Member member)
        {
            //  return NotFound();

            // var memberx = await _context.Members
            //    .FirstOrDefaultAsync(m => m.LotNo == LotNo);
            //  if (memberx == null)
            //  {
            if (ModelState.IsValid)
            {
                _context.Add(member);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //    }

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

        public bool UpdatePrice(int? id, int? Price)
        {


            return true;
        }





        // POST: Members/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,TypeSell,LotNo,Price,AgentLastName,AgentFirstName,Email,OfficePhone,CellPhone,AgentUrl,ImageName")] Member member)
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
            var member = _context.Members.Find(id);

            try
            {

                if (member != null)
                {
                    var path = _appEnvironment.WebRootPath;

                    // Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "Upload"));
                    var imagepath = path + "\\" + member.ImageName;
                    if (System.IO.File.Exists(imagepath))
                    {
                        System.IO.File.Delete(imagepath);
                        _context.Members.Remove(member);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index), "Members");
                    }
                    else
                    {
                        _context.Members.Remove(member);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index), "Members");
                    }
                }
            }
            catch (Exception ex)
            {
                return NotFound();

            }
            /*
  
            if (member != null)
            {
                _context.Members.Remove(member);
            }
 
            await _context.SaveChangesAsync();
            */


            return RedirectToAction(nameof(Index), "Members");
        }

        private bool MemberExists(int id)
        {
            return _context.Members.Any(e => e.ID == id);
        }
    }
}




/*


<form method="post" enctype="multipart/form-data">
    <input type="file" asp-for="Upload" />
    <input type="submit" />
</form>


 public class UploadFileModel : PageModel
    {
        private IHostingEnvironment _environment;
        public UploadFileModel(IHostingEnvironment environment)
        {
            _environment = environment;
        }
        [BindProperty]
        public IFormFile Upload { get; set; }
        public async Task OnPostAsync()
        {
            var file = Path.Combine(_environment.ContentRootPath, "uploads", Upload.FileName);
            using (var fileStream = new FileStream(file, FileMode.Create))
            {
                await Upload.CopyToAsync(fileStream);
            }
        }
    }
*/