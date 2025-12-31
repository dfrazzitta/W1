using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer.Server;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Processing;
using System.Diagnostics;

//using System.Drawing;
//using System.Drawing.Drawing2D;
//using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using W1.Data;
using W1.Models;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace W1.Controllers
{
    [Authorize]
    public class MembersController : Controller
    {
        private readonly PlacidDBContext _context;
        private readonly ApplicationDbContext _appcontext;
        //private readonly IScopedService _scopedService;


        private readonly IWebHostEnvironment _appEnvironment;
        private readonly ILogger<MembersController> _logger;


        public MembersController(ILogger<MembersController> logger, ApplicationDbContext appcontext, PlacidDBContext context, IWebHostEnvironment appEnvironment) //, IScopedService scopedService)
        {
            _context = context;
            _appEnvironment = appEnvironment;
            //  _scopedService = scopedService;
            _appcontext = appcontext;
            _logger = logger;
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

        /// <summary>
        /// //////////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>

        [HttpPost]
        public async Task<ActionResult> FileUpload1(IFormFile file, string TypeSell, string LotNo, string Price, string AgentLastName, string AgentFirstName, string Email,

            //public async Task<ActionResult> FileUpload1(IFormFile file, int LotNo, string Price, string AgentLastName, string AgentFirstName, string Email,
            string OfficePhone, string CellPhone, string AgentUrl)
        {
            // check for a empty value on lot and filename extension 
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
            bool areEqual = string.Equals(Path.GetFileNameWithoutExtension(file.FileName), LotNo.ToString(), StringComparison.OrdinalIgnoreCase);
            if (areEqual == false)
            {
                return StatusCode(598, "some text, json, etc.");
            }


            var memberx = await _context.Members
           .FirstOrDefaultAsync(m => m.LotNo == LotNo);
            if (memberx != null)
            {
                return StatusCode(599, "some text, json, etc.");
            }
            else
            {
                if (ModelState.IsValid)
                {

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
                    member0.OfficePhone = OfficePhone;
                    member0.CellPhone = CellPhone;
                    member0.ImageName = file.FileName;
                    member0.AgentUrl = AgentUrl;

                    _context.Add(member0);
                    await _context.SaveChangesAsync();
                    // return RedirectToAction(nameof(Index));
                    return StatusCode(200, "Process completed.");
                }
                else
                {
                    return StatusCode(597, "some text, json, etc.");
                    /*
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
                    */
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
                    var tt = path + "/" + filename;
                    using (var filestream = new FileStream(path + "/" + filename, FileMode.Create))  //        
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
            //  System.IO.File.Create(Path.Combine(_appEnvironment.WebRootPath, "HomeIndex.txt"));
            //   List<Member> members = await _context.Members.ToListAsync();
            //   System.IO.File.Create(Path.Combine(_appEnvironment.WebRootPath, "HomeIndex1.txt"));
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

            if (member != null)
            {
                // _context.Members.Remove(member);

                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index), "Members");


        }

        // GET: Members/Create
        public IActionResult Create()
        {
            return View();
        }

        public string FirstLetterToUpper(string str)
        {
            if (str == null)
                return null;

            if (str.Length > 1)
                return char.ToUpper(str[0]) + str.Substring(1);

            return str.ToUpper();
        }



        // POST: Members/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,LotNo,Price,AgentLastName,AgentFirstName,Email,OfficePhone,CellPhone,AgentUrl,ImageName")] Member member)
        {
            //  return NotFound();


            if (ModelState.IsValid)
            {
                string fn = member.AgentFirstName;
                string fn1 = FirstLetterToUpper(fn);
                member.AgentFirstName = fn1;

                _context.Add(member);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }


            return View(member);
        }

        // GET: Members/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {


            if (id == null)
            {
                return NotFound();
            }

            var member = await _context.Members.FindAsync(id);
            if (member == null)
            {
                return NotFound();
            }

            string op = member.OfficePhone.Replace("-", "");
            string cp = member.CellPhone.Replace("-", "");


            member.OfficePhone = op;
            member.CellPhone = cp;

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

            if (id != member.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // check all fields phone etc

                    _context.Update(member);
                    await _context.SaveChangesAsync();
                    // RedirectPermanent("http://localhost:5000/members");
                    return RedirectToAction(nameof(Index), "Members");
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

            }
            else
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


            // _context.Members.Remove(member);
            // await _context.SaveChangesAsync();
            //  return RedirectToAction(nameof(Index), "Members");

            return View(member);
        }


        bool ChangeLinuxFilePermissions(string file)
        {


            using (Process proc = new Process())
            {
                proc.StartInfo.FileName = "rm ";
                //  proc.StartInfo.Arguments = $"\{file}\"";
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.StartInfo.RedirectStandardError = true;
                proc.StartInfo.CreateNoWindow = true;

                proc.Start();
                proc.WaitForExit();

                if (proc.ExitCode == 0)
                {
                    _logger.LogInformation("file deleted {Data}", file);
                    //Console.WriteLine($"Successfully changed permissions for '{filePath}' to '{permissions}'.");
                    return true;
                }
                else
                {
                    _logger.LogInformation("file deleted  error {Data}", file);

                    string error = proc.StandardError.ReadToEnd();
                    // Console.WriteLine($"Error changing permissions for '{filePath}': {error}");
                    return false;
                }
            }

            //return true;
        }

        /*
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        */

        public IActionResult Error(int statusCode)
        {
            if (statusCode == 404)
            {
                return View("Error");
            }
            return View("Error");
        }


        // POST: Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var member = _context.Members.Find(id);

            try
            {
                var path1 = _appEnvironment.WebRootPath;
                var linuxPath1 = path1 + "//" + member.ImageName;
                _logger.LogInformation("linux Processing data: {Data}", linuxPath1);

                if (member != null)
                {
                    var path = _appEnvironment.WebRootPath;

                    // Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "Upload"));
                    var imagepath = path + "\\" + member.ImageName;
                    _logger.LogInformation("Processing data: {Data}", imagepath);
                    var linuxPath = path + "/" + member.ImageName;

                    if (System.IO.File.Exists(imagepath))
                    {
                        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                        {

                            string newPath = imagepath.Replace("\\", "/");
                            _logger.LogInformation("Processing data linuxPath: {Data}", path);
                            _logger.LogInformation("Processing data linuxPath: {Data}", linuxPath);

                            // change file attributes on Linux
                            // ChangeLinuxFilePermissions(linuxPath);
                            // System.IO.File.Delete(linuxPath);
                        }
                        else
                        {
                            // System.IO.File.Delete(imagepath);
                        }


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


            return RedirectToAction(nameof(Index), "Members");
        }

        private bool MemberExists(int id)
        {
            return _context.Members.Any(e => e.ID == id);
        }

        [HttpPost]
        public bool unsetforsale()
        {
            // Fix for CS0119 and CS0642:
            // Correctly open the file for appending and ensure the StreamWriter is properly used.
            string logFilePath = Path.Combine(_appEnvironment.WebRootPath, "HomeVisible.txt");
            System.IO.File.Delete(logFilePath);
            return true;
        }

        [HttpPost]
        public bool setforsale()
        {
            string logFilePath = Path.Combine(_appEnvironment.WebRootPath, "HomeVisible.txt");
            System.IO.File.Create(logFilePath);
            return true;
        }


        [HttpPost]
        public ActionResult FileUpload10(IFormFile file)
        {
            if (file == null)
            {
                return RedirectToAction("Index"); // return RedirectToAction(nameof(Index));
            }

            var extension = Path.GetExtension(file.FileName);
            bool areEqualjpg = System.String.Equals(extension, ".jpg", StringComparison.OrdinalIgnoreCase);
            bool areEqualjpeg = System.String.Equals(extension, ".jpeg", StringComparison.OrdinalIgnoreCase);

            if (areEqualjpg == false)
            {
                return RedirectToAction("Index");
            }


            UploadFile0(file, "1_b.jpg");
            return RedirectToAction("Index"); //  return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public ActionResult FileUpload20(IFormFile file)
        {
            if (file == null)
            {
                return RedirectToAction("Index"); // return RedirectToAction(nameof(Index));
            }

            var extension = Path.GetExtension(file.FileName);
            bool areEqualjpg = System.String.Equals(extension, ".jpg", StringComparison.OrdinalIgnoreCase);
            bool areEqualjpeg = System.String.Equals(extension, ".jpeg", StringComparison.OrdinalIgnoreCase);

            if (areEqualjpg == false)
            {
                return RedirectToAction("Index");
            }


            UploadFile0(file, "6_b.jpg");
            return RedirectToAction("Index"); // return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public ActionResult FileUpload30(IFormFile file)
        {
            if (file == null)
            {
                return RedirectToAction("Index"); // return RedirectToAction(nameof(Index));
            }

            var extension = Path.GetExtension(file.FileName);
            bool areEqualjpg = System.String.Equals(extension, ".jpg", StringComparison.OrdinalIgnoreCase);
            bool areEqualjpeg = System.String.Equals(extension, ".jpeg", StringComparison.OrdinalIgnoreCase);

            if (areEqualjpg == false)
            {
                return RedirectToAction("Index");
            }


            UploadFile0(file, "3_b.jpg");
            return RedirectToAction("Index"); //  return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public ActionResult FileUpload40(IFormFile file)
        {
            if (file == null)
            {
                return RedirectToAction("Index"); // return RedirectToAction(nameof(Index));
            }

            var extension = Path.GetExtension(file.FileName);
            bool areEqualjpg = System.String.Equals(extension, ".jpg", StringComparison.OrdinalIgnoreCase);
            bool areEqualjpeg = System.String.Equals(extension, ".jpeg", StringComparison.OrdinalIgnoreCase);

            if (areEqualjpg == false)
            {
                return RedirectToAction("Index");
            }


            UploadFile0(file, "4_b.jpg");
            return RedirectToAction("Index"); // return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public ActionResult FileUpload50(IFormFile file)
        {
            if (file == null)
            {
                return RedirectToAction("Index"); //  return RedirectToAction(nameof(Index));
            }

            var extension = Path.GetExtension(file.FileName);
            bool areEqualjpg = System.String.Equals(extension, ".jpg", StringComparison.OrdinalIgnoreCase);
            bool areEqualjpeg = System.String.Equals(extension, ".jpeg", StringComparison.OrdinalIgnoreCase);

            if (areEqualjpg == false)
            {
                return RedirectToAction("Index");
            }

            UploadFile0(file, "8_b.jpg");
            return RedirectToAction("Index");

        }




        bool SaveToWindows(byte[] bytes, string itemNo)
        {


            using (var ms = new MemoryStream())
            {
                var path = _appEnvironment.WebRootPath;
                byte[] imageBytes = ms.ToArray();

                using (SixLabors.ImageSharp.Image image = SixLabors.ImageSharp.Image.Load(imageBytes))
                {
                    // Define the resize options, e.g., maintaining aspect ratio and using high quality resampling
                    var resizeOptions = new ResizeOptions
                    {
                        Size = new SixLabors.ImageSharp.Size(700, 200),
                        Mode = ResizeMode.Max
                    };

                    image.Mutate(x => x.Resize(resizeOptions));


                    var jp = new SixLabors.ImageSharp.Formats.Jpeg.JpegEncoder
                    {
                        Quality = 70 // Set quality as needed
                    };

                    // var path = _appEnvironment.WebRootPath;
                    image.Save(path + "\\" + "Images" + "\\" + "SlideShow" + "\\" + itemNo, jp);
                }
                return true;


                // MemoryStream ms = new MemoryStream(bytes);
                // System.Drawing.Image returnImage = System.Drawing.Image.FromStream(ms);
                // System.Drawing.Image NewImage = ResizeImage(returnImage, 700, 200);

                //  path = _appEnvironment.WebRootPath;

                // NewImage.Save(path + "\\" + "Images" + "\\" + "SlideShow" + "\\" + "1_b.jpg", ImageFormat.Jpeg);
                // zzz
                //NewImage.Save(path + "\\" + "Images" + "\\" + "SlideShow" + "\\" + itemNo, ImageFormat.Jpeg);

            }
        }


        bool SaveToLinux(IFormFile file, string itemNo)
        {

            if (file != null && file.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    byte[] imageBytes = ms.ToArray();

                    using (SixLabors.ImageSharp.Image image = SixLabors.ImageSharp.Image.Load(imageBytes))
                    {
                        // Define the resize options, e.g., maintaining aspect ratio and using high quality resampling
                        // var resizeOptions = new ResizeOptions
                        // {
                        //     Size = new SixLabors.ImageSharp.Size(700, 200);
                        //  };

                        var options = new ResizeOptions
                        {
                            Size = new Size(700, 200),
                            Mode = ResizeMode.Min, // Resizes to fit within the specified bounds while maintaining aspect ratio
                            Sampler = KnownResamplers.Lanczos3 // Specifies the resampling algorithm
                        };

                        // image.Mutate(x => x.Rotate(-90));
                        image.Mutate(x => x.Resize(700, 200));



                        //  image.Mutate(x => x.Resize(options(700, 0)); //);

                        var jp = new SixLabors.ImageSharp.Formats.Jpeg.JpegEncoder
                        {
                            Quality = 70 // Set quality as needed
                        };

                        var path = _appEnvironment.WebRootPath;
                        image.Save(path + "\\" + "Images" + "\\" + "SlideShow" + "\\" + itemNo, jp);
                    }

                }
            }
            return true;
        }



        public bool UploadFile0(IFormFile file, string itemNo)
        {
            string path = "";
            bool iscopied = false;


            if (file.Length > 0)
            {
                using var fileStream = file.OpenReadStream();
                byte[] bytes = new byte[file.Length];
                fileStream?.Read(bytes, 0, (int)file.Length);

                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    // SaveToWindows(bytes, itemNo);
                    SaveToLinux(file, itemNo);
                }


                if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    SaveToLinux(file, itemNo);
                }

                // iscopied = true;
            }

            return true;
        }



    }

}






