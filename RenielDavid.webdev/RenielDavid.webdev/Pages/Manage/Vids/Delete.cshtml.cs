using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RenielDavid.webdev.Infrastructure.Domain.Models;
using RenielDavid.webdev.Infrastructure.Domain;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace RenielDavid.webdev.Pages.Manage.Vids
{
    [Authorize(Roles = "admin")]
    public class Delete : PageModel
    {
        private ILogger<Index> _logger;
        private DefaultDbContext _context;

        [BindProperty]
        public ViewModel View { get; set; }

        public Delete(DefaultDbContext context, ILogger<Index> logger)
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
            if (View.VideoId == null)
            {
                return NotFound();
            }

            var video = _context?.Videos?.FirstOrDefault(a => a.VideoId == View.VideoId);

            if (video != null)
            {
                _context?.Videos?.Remove(video);
                _context?.SaveChanges();

                return RedirectPermanent("~/manage/video");
            }

            return NotFound();

        }

        public class ViewModel : Video
        {

        }
    }
}