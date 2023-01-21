using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RenielDavid.webdev.Infrastructure.Domain.Models;
using RenielDavid.webdev.Infrastructure.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace RenielDavid.webdev.Pages.Manage.Vids
{
    [Authorize(Roles = "admin")]
    public class Index : PageModel
    {
        private DefaultDbContext _context;
        private ILogger<Index> _logger;

        [BindProperty]
        public ViewModel View { get; set; }

        public Index(DefaultDbContext context, ILogger<Index> logger)
        {
            _logger = logger;
            _context = context;
            View = View ?? new ViewModel();
        }

        public void OnGet(int? pageIndex = 1, int? pageSize = 10, string? sortBy = "", SortOrder sortOrder = SortOrder.Ascending, string? keyword = "", Guid? Id = null)
        {
            var skip = (int)((pageIndex - 1) * pageSize);

            var query = _context.Videos
                                .Include(a => a.Service)
                                .AsQueryable();

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(a =>
                            a.Title != null && a.Title.ToLower().Contains(keyword.ToLower())
                        || a.Description != null && a.Description.ToLower().Contains(keyword.ToLower())

                );
            }

            if (Id != null)
            {
                query = query.Where(a => a.ServiceId == Id);
            }

            var totalRows = query.Count();

            if (!string.IsNullOrEmpty(sortBy))
            {
                if (sortBy.ToLower() == "title" && sortOrder == SortOrder.Ascending)
                {
                    query = query.OrderBy(a => a.Title);
                }
                else if (sortBy.ToLower() == "title" && sortOrder == SortOrder.Descending)
                {
                    query = query.OrderByDescending(a => a.Title);
                }
                else if (sortBy.ToLower() == "description" && sortOrder == SortOrder.Ascending)
                {
                    query = query.OrderBy(a => a.Description);
                }
                else if (sortBy.ToLower() == "description" && sortOrder == SortOrder.Descending)
                {
                    query = query.OrderByDescending(a => a.Description);
                }

            }

            var videos = query
                            .Skip(skip)
                            .Take((int)pageSize)
                            .ToList();

            if (Id != null)
            {
                View.Id = Id;
                View.StremingandServiceTitle = videos.FirstOrDefault()?.Service?.Title;
            }

            View.Videos = new Paged<Video>()
            {
                Items = videos,
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalRows = totalRows,
                SortBy = sortBy,
                SortOrder = sortOrder,
                Keyword = keyword,
            };
        }

        public JsonResult? OnGetRolesLookup(int pageIndex = 1, string? keyword = "", int pageSize = 10)
        {

            var query = _context.Videos.AsQueryable();

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(a =>
                            a.Title != null && a.Title.ToLower().StartsWith(keyword.ToLower())
                );
            }

            return new JsonResult(query?.Select(a => new LookupDto.Result()
            {
                Id = a.ServiceId.ToString(),
                Text = a.Title
            })
            .OrderBy(a => a.Text)
            .GetLookupPaged(pageIndex, pageSize));
        }

        public class ViewModel
        {
            public Paged<Video>? Videos { get; set; }
            public Guid? Id { get; set; }
            public string? StremingandServiceTitle { get; set; }
        }


    }
}