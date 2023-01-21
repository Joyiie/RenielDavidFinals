using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RenielDavid.webdev.Infrastructure.Domain.Models;
using RenielDavid.webdev.Infrastructure.Domain;
using System.Data;

namespace RenielDavid.webdev.Pages.Manage.Vids
{
    public class Update : PageModel
    {
        private ILogger<Index> _logger;
        private DefaultDbContext _context;

        [BindProperty]
        public ViewModel View { get; set; }

        public Update(DefaultDbContext context, ILogger<Index> logger)
        {
            _logger = logger;
            _context = context;
            View = View ?? new ViewModel();
        }

        public IActionResult OnGet(Guid? id = null)
        {
            if (id == null)
            {
                return NotFound();
            }

            var video = _context?.Videos?.Where(a => a.ServiceId == id)
                                   .Select(a => new ViewModel()
                                   {
                                       Title = a.Title,
                                       Description = a.Description,
                                       DateOfPublish = a.DateOfPublish,
                                       Type = a.Type
                                   }).FirstOrDefault();

            if (video == null)
            {
                return NotFound();
            }

            View = video;
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

            var existingRole = _context?.Roles?.FirstOrDefault(a =>
                    a.RoleId != View.ServiceId &&
                    a.Name.ToLower() == View.Title.ToLower()
            );

            if (existingRole != null)
            {
                ModelState.AddModelError("", "Role is already existing.");
                return Page();
            }

            var video = _context?.Videos?.FirstOrDefault(a => a.VideoId == View.VideoId);

            if (video != null)
            {
               
                video.Title = View.Title;
                video.Description = View.Description;
                video.DateOfPublish = View.DateOfPublish;


                _context?.Videos?.Update(video);
                _context?.SaveChanges();

                return RedirectPermanent("~/manage/vids");
            }

            return Page();

        }

        public class ViewModel : Video
        {

        }
    }
}
