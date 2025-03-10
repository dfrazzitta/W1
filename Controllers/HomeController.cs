using System.Diagnostics;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using W1.Data;
using W1.Models;

/*
 * 
 *  just a test
    /.,Lpp99
    4454MyHOus3!23
    /.,Lpp99   4454MyH0us3!23 is the ftp password.
  

   ssh -p 443 -R0:localhost:5151 a.pinggy.io


   http://rnkfl-23-28-243-229.a.free.pinggy.link
   display:none;

   docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=tvxs721#3TTv" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2019-latest
 
   dotnet ef migrations add InitialCreate --context ApplicationDBContext --output-dir Migrations/ApplicationDBContextMigrations
   dotnet ef migrations add InitialCreate --context SchoolContext --output-dir Migrations/SchoolContextMigrations
   dotnet ef database update  --context 

   create new db
   delete all migration info -- controller/views (validation script in shared)/migrations
   change Member.cs
   dotnet ef migrations add InitialCreate --context PlacidDBContext --output-dir Migrations/PlacidDBContextMigrations
   dotnet ef database update  --context PlacidDBContext
   add controller
   
   sqlite> CREATE TABLE images(name TEXT, type TEXT, img BLOB);
   sqlite> INSERT INTO images(name,type,img)
   ...>   VALUES('icon','jpeg',readfile('icon.jpg'));

   await Task.WhenAll(eggsTask, baconTask, toastTask);
   // 5001

        Image img = Image.FromFile(fileName);
        ImageFormat format = img.RawFormat;
        Console.WriteLine("Image Type : "+format.ToString());
        Console.WriteLine("Image width : "+img.Width);
        Console.WriteLine("Image height : "+img.Height);
        Console.WriteLine("Image resolution : "+(img.VerticalResolution*img.HorizontalResolution));

        Console.WriteLine("Image Pixel depth : "+Image.GetPixelFormatSize(img.PixelFormat));
        Console.WriteLine("Image Creation Date : "+creation.ToString("yyyy-MM-dd"));
        Console.WriteLine("Image Creation Time : "+creation.ToString("hh:mm:ss"));
        Console.WriteLine("Image Modification Date : "+modify.ToString("yyyy-MM-dd"));
        Console.WriteLine("Image Modification Time : "+modify.ToString("hh:mm:ss"));



  function CallPodList(namespace, index)
        {
            jQuery.ajaxSetup({ async: false });
            $.ajax({
                type: "GET",
                url: '@Url.Action("GetPodsList")',
                data: { jsonInput: namespace },
                contentType: "application/json;charset=utf-8",
                //contentType: "application/x-www-form-urlencoded; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    //alert(response);
                    var tt = response.items;
                    if (tt.length == 0) {
                        alert("No Items returned !!!!")
                        hidespin();
                        jQuery.ajaxSetup({ async: true });
                        return false;
                    }
                    var listNames = response.items;

                    const container = document.getElementById('jsoneditor')

                    var modes = { mode: 'tree', modes: ['form', 'text', 'tree'] }; // OPTIONAL
                    var okcallback = function (jsonobj) { alert(JSON.stringify(tt)); }; // OPTIONAL
                    var cancelcallback = function () { }; // OPTIONAL
                    var errorcallback = function (e) { alert(e); }; // OPTIO
                    hidespin();
                    jsonDialog12(tt, modes, 'GetAllResources', okcallback, cancelcallback, errorcallback);
                },
                error: function (response) {
                    alert("error");
                    alert(response.responseText);
                }
            });
            jQuery.ajaxSetup({ async: true });
        }

        1024x682 300px inch  3.41x2.27
 */

namespace W1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _appEnvironment;

        private readonly PlacidDBContext _context;


        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment appEnvironment,
            PlacidDBContext context)
        {
            _logger = logger;
            _appEnvironment = appEnvironment;
            _context = context;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ParkPlan()
        {
            return View();
        }



        public IActionResult Location()
        {
            return View();
        }

        public IActionResult Rules()
        {
            return View();
        }

        public IActionResult ResidentOwner()
        {
            return View();
        }


        /*
        public IActionResult Homes()
        {
            return View();
        }
        */



        public IActionResult Homes()
        {
            List<Member> member = _context.Members.ToList();

            int ct = member.Count();
            //string newHomes = "<div class=\"row\">\r\n <div class=\"col-sm-1\">\r\n </div>\r\n<div class=\"col-sm-10\" width:100%;>\r\n <div class=\"d-flex justify-content-center\">\r\n <table align=\"center\" mx-auto border=\"1\" cellpadding=\"4\" cellspacing=\"0\" width=\"100%\">\r\n <tbody>\r\n <tr>\r\n <td colspan=\"3\" align=\"center\" class=\"BG_Light_Blue\"> <a name=\"L88\" id=\"L88\"></a> Lot # 5 </td>\r\n </tr>\r\n <tr>\r\n <td colspan=\"5\">\r\n <div align=\"center\"> <img src=\"6.jpg\" class=\"img-fluid\" alt=\"Lot # 5\" name=\"Main_05\" id=\"Main_05\" height=\"auto\" width=\"auto\">  \r\n <a id=\"abcd0\"  class=\"example-image-link\" href=\"/Images/platmap.jpg\" width='388px' height='339px' data-lightbox=\"example-1\" data-toggle=\"tooltip\" data-placement=\"top\" title=\"Image Expanded\">\r\n Lot Location\r\n </a>\r\n </div>\r\n </td>\r\n </tr>\r\n\r\n <tr>\r\n <td colspan=\"4\">\r\n <div align=\"center\"> <strong>Property Specifics:</strong> </div>\r\n <table border=\"1\" cellpadding=\"1\" cellspacing=\"0\" width=\"100%\">\r\n  <tbody>\r\n <tr>\r\n <td width=\"20%\"> <strong>Price: </strong> </td>\r\n <td width=\"80%\"> $110,000.00 </td>\r\n  </tr>\r\n <tr>\r\n <td valign=\"top\"> <strong>Contact:</strong> </td>\r\n <td> Kathryn Valentine   <br /> Ph: 407-674-0220 <br />Ph: 407-479-8789 <br /><a href=\"LINKTOWEB\" target=\"_blank\">Web Listing</a></td>\r\n </tr>\r\n </table>\r\n </td>\r\n </tr>\r\n </tbody>\r\n </table>\r\n </div>\r\n </div>\r\n <div class=\"col-sm-1\">\r\n </div>\r\n </div>\r\n <br />\r\n <br />";


            string newHomes = "<div class=\"row\">\r\n <div class=\"col-sm-1\">\r\n </div>\r\n<div class=\"col-sm-10\" width:100%;>\r\n <div class=\"d-flex justify-content-center\">\r\n <table align=\"center\" mx-auto border=\"1\" cellpadding=\"4\" cellspacing=\"0\" width=\"100%\">\r\n <tbody>\r\n <tr>\r\n <td colspan=\"3\" align=\"center\" class=\"BG_Light_Blue\"> <a name=\"L88\" id=\"L88\"></a> Lot # 5 </td>\r\n </tr>\r\n <tr>\r\n <td colspan=\"5\">\r\n <div align=\"center\"> <img src=\"6.jpg\" class=\"img-fluid\" alt=\"Lot # 5\" name=\"Main_05\" id=\"Main_05\" height=\"auto\" width=\"auto\">  \r\n <a id=\"abcd0\"  class=\"example-image-link\" href=\"/Images/platmap.jpg\" width='388px' height='339px' data-lightbox=\"example-1\" data-toggle=\"tooltip\" data-placement=\"top\" title=\"Image Expanded\">\r\n Lot Location\r\n </a>\r\n </div>\r\n </td>\r\n </tr>\r\n\r\n <tr>\r\n <td colspan=\"4\">\r\n <div align=\"center\"> <strong>Property Specifics:</strong> </div align=\"center\">\r\n <table align=\"center\" border=\"3\" cellpadding=\"1\" cellspacing=\"0\" width=\"35%\">\r\n  <tbody>\r\n <tr>\r\n <td width=\"20%\"> <strong>Price: </strong> </td>\r\n <td width=\"80%\"> $110,000.00 </td>\r\n  </tr>\r\n <tr>\r\n <td valign=\"top\"> <strong>Contact:</strong> </td>\r\n <td> Kathryn Valentine   <br /> Ph: 407-674-0220  <br />Ph: 407-479-8789 <br /><a href=\"LINKTOWEB\" target=\"_blank\">Agent Web Listing</a></td>\r\n </tr>\r\n </table>\r\n </td>\r\n </tr>\r\n </tbody>\r\n </table>\r\n </div>\r\n </div>\r\n <div class=\"col-sm-1\">\r\n </div>\r\n </div>\r\n <br />\r\n <br />";
 
            List<DynamicHomes> dh = new List<DynamicHomes>();

            foreach (Member m in member)
            {
                DynamicHomes dh1 = new DynamicHomes();

                StringBuilder sb1 = new StringBuilder();
                sb1.Append(newHomes);
                string bb = "/" + m.LotNo.ToString() + ".jpg";
                sb1.Replace("6.jpg", bb);
                sb1.Replace("Lot # 5", "Lot # " + m.LotNo.ToString());

                sb1.Replace("407-674-0220", m.CellPhone.ToString() + " " + "Cell");
                sb1.Replace("407-479-8789", m.OfficePhone.ToString() + " " + "Office");

                sb1.Replace("/Images/platmap.jpg", "/Images/platmap" + m.LotNo.ToString() + ".jpg");

                sb1.Replace("Kathryn Valentine", m.AgentFirstName.Trim() + " " + m.AgentLastName.Trim());
                //sb.Replace("Kathryn", m.FirstName);

                sb1.Replace("mailto:lakeplacidpark@gmail.com", "mailto:" + m.Email);

                sb1.Replace("LINKTOWEB", m.AgentUrl);
                sb1.Replace("$110,000.00", m.Price.Trim());
                string ll1 = sb1.ToString();

                dh1.homelist = ll1; //.Add(ll);
                dh.Add(dh1);
            }

            return View(dh);
        }
 
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
