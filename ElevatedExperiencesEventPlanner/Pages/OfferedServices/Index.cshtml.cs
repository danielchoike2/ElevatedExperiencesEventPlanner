using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ElevatedExperiencesEventPlanner.Data;
using ElevatedExperiencesEventPlanner.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;

namespace ElevatedExperiencesEventPlanner.Pages.OfferedServices
{
    public class IndexModel : PageModel
    {
        private readonly ElevatedExperiencesEventPlanner.Data.ElevatedExperiencesEventPlannerContext _context;

        public IndexModel(ElevatedExperiencesEventPlanner.Data.ElevatedExperiencesEventPlannerContext context)
        {
            _context = context;
        }

        public IList<OfferedService> OfferedService { get;set; } = default!;
        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }
        public SelectList? ServiceNames { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? ServiceName { get; set; }
        
        public string NameSort { get; set; }
        public async Task OnGetAsync(string sortOrder)
        {
            NameSort = sortOrder;
            IQueryable<OfferedService> offeredServiceQuery = from s in _context.OfferedService
                                                             select s;

            var offeredServices = from s in _context.OfferedService
                         select s;
            
            if (!string.IsNullOrEmpty(SearchString))
            {
                offeredServices = offeredServices.Where(s => s.Name.Contains(SearchString));
            }
            if (NameSort == "desc")
            {
                offeredServices = offeredServices.OrderByDescending(e => e.Name);

            }
            else
            {
                offeredServices = offeredServices.OrderBy(offeredServices => offeredServices.Name);
            }

            OfferedService = await offeredServices.ToListAsync();
        }
    }
}
