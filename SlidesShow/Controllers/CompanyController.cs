using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SlidesShow.Models;
using SixLabors.ImageSharp;


namespace SlidesShow.Controllers
{
    public class CompanyController : Controller
    {
        private readonly SlidesDbContext _context;
        private readonly IMapper _map;

        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment;

        public CompanyController(SlidesDbContext context, IMapper map, Microsoft.AspNetCore.Hosting.IHostingEnvironment Environment)
        {
            _environment = Environment;
            _map = map;
            _context = context;
        }
        public IActionResult Index()
        {
            //IEnumerable<CompanySlide> images = _context.CompanySlides.ToList();

            //IEnumerable<createslide> lists = _map.Map<IEnumerable<createslide>>(images);

            return View();
        }

        public IActionResult Selected(IFormCollection formCollection)
        {
            string Company = formCollection["Company"];

            string Club = formCollection["Club"];

            IEnumerable<CompanySlide> images = _context.CompanySlides.Where(s=>s.PropertyId == int.Parse(Club)).OrderByDescending(o=>o.Id).ToList();

            IEnumerable<createslide> lists = _map.Map<IEnumerable<createslide>>(images);

            Addnew newList = new Addnew();
            newList.AllSelected = lists;
            newList.clubId = int.Parse(Club);

            ViewData["ClubName"] = _context.Companies.FirstOrDefault(c=>c.Id == newList.clubId).Name;

            ViewData["propId"] = newList.clubId;

            return View(newList);
        }
        public IActionResult BootstrapSlide(int id)
        {
            var imageDetails = _context.CompanySlides.Where(i => i.PropertyId == id).OrderByDescending(o=>o.Id).ToList();

            var requiredDetails = _map.Map<List<Preview>>(imageDetails);

            return View(requiredDetails);
        }

        public IActionResult PartialViewCreate()
        {
            return PartialView("_Create", new createslide());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(createslide inputs)
        {
            string uniqueImageName = null;
            int width = 0;
            int height = 0;
            if (inputs.Image != null && inputs.Image.Length > 0)
            {
                string uploadfolder = Path.Combine(_environment.WebRootPath, "Images");
                uniqueImageName = Guid.NewGuid().ToString() + "_" + inputs.Image.FileName;

                var extension = Path.GetExtension(uniqueImageName);

                if (extension.ToLower().Equals(".png") || extension.ToLower().Equals(".jpg")
                    || extension.ToLower().Equals(".jpeg"))
                {

                    string filepath = Path.Combine(uploadfolder, uniqueImageName);
                    inputs.Image.CopyTo(new FileStream(filepath, FileMode.Create));
                }
                else
                {
                    ViewBag.Image = "Please Upload .png ,.jpg,.jpeg formats only!!";
                }
            }
            using (var image = Image.Load(inputs.Image.OpenReadStream()))
            {
                width = image.Width;
                height = image.Height;

            }


            CompanySlide companySlide = new CompanySlide
            {
                PropertyId = inputs.PropertyId,
                ImagePath = uniqueImageName,
                Name = inputs.Image.FileName,
                Width = width,
                Height = height,
                FileSize = inputs.Image.Length,
                BackgroundColor = inputs.BackgroundColor,
                Duration=inputs.Duration,
                Footer=inputs.Footer,   
            };

            _context.CompanySlides.Add(companySlide);
            _context.SaveChanges();

            IEnumerable<CompanySlide> images = _context.CompanySlides.Where(s => s.PropertyId == inputs.PropertyId).OrderByDescending(o => o.Id).ToList();

            IEnumerable<createslide> lists = _map.Map<IEnumerable<createslide>>(images);

            Addnew newList = new Addnew();
            newList.AllSelected = lists;
            newList.clubId = inputs.PropertyId;

            ViewData["ClubName"] = _context.Companies.FirstOrDefault(c => c.Id == newList.clubId).Name;

            ViewData["propId"] = newList.clubId;

            return View("Selected", newList);

        }

        public IActionResult Delete(int id)
        {
            try
            {
                var slide =  _context.CompanySlides.Find(id);
                if (slide != null)
                {
                    _context.CompanySlides.Remove(slide);
                    _context.SaveChanges();
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return NotFound();
            }
        }

        [HttpPost]
        public JsonResult DropDownList(IFormCollection options)
        {
            string option = options["CompanyId"];
            var SubList = _context.Companies.Where(s => s.CompanyId == int.Parse(option)).ToList();

            if (option == "3")
            {
                SubList = _context.Companies.Where(s=>s.IsParent==false).ToList();
            }
            

            return Json(SubList);
        }


        public IActionResult ShowSlideShow(int id)
        {
            var ImageDetails = _context.CompanySlides.Where(i => i.PropertyId == id).OrderByDescending(o=>o.Id).ToList();

            ListPreview listPreview = new ListPreview();
            listPreview.PreviewList = _map.Map<List<Preview>>(ImageDetails);

            string lisOfImages = "";
            foreach (var p in ImageDetails)
            {
                lisOfImages = lisOfImages + "," + p.ImagePath;
            }
            @ViewBag.images = lisOfImages.Substring(1, lisOfImages.Length - 1);

            string lisOfDuration = "";
            foreach (var d in ImageDetails)
            {
                lisOfDuration = lisOfDuration + "," + d.Duration;
            }
            @ViewBag.duration = lisOfDuration.Substring(1, lisOfDuration.Length - 1);

            return View("Previews");
        }


    }
}
