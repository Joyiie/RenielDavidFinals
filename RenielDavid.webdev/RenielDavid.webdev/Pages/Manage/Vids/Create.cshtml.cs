using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RenielDavid.webdev.Infrastructure.Domain.Models;
using RenielDavid.webdev.Infrastructure.Domain;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace RenielDavid.webdev.Pages.Manage.Vids
{
    [Authorize(Roles = "admin")]
    public class Create : PageModel
    {
        private ILogger<Index> _logger;
        private DefaultDbContext _context;

        [BindProperty]
        public ViewModel View { get; set; }

        public Create(DefaultDbContext context, ILogger<Index> logger)
        {
            _logger = logger;
            _context = context;
            View = View ?? new ViewModel();
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public IActionResult OnPost()
        {
            if (string.IsNullOrEmpty(View.Title))
            {
                ModelState.AddModelError("", "Role name cannot be blank.");
                return Page();
            }

            if (string.IsNullOrEmpty(View.Description))
            {
                ModelState.AddModelError("", "Description cannot be blank.");
                return Page();
            }
            if (DateTime.MinValue >= View.DateOfPublish)
            {
                ModelState.AddModelError("", "date name cannot be blank.");
                return Page();
            }
           

            var existingVideo = _context?.Videos.FirstOrDefault(a => a.Title.ToLower() == View.Title.ToLower());
            if (existingVideo != null)
            {
                ModelState.AddModelError("", "Role is already existing.");
                return Page();
            }

           Video video = new Video()
            {
                VideoId = Guid.NewGuid(),
                Title= View.Title,
                Description = View.Description,
                DateOfPublish = View.DateOfPublish
               
            };

            _context?.Videos?.Add(video);
            _context?.SaveChanges();

            return RedirectPermanent("~/manage/roles");
        }

        public class ViewModel : Video
        {

        }
    }
}
