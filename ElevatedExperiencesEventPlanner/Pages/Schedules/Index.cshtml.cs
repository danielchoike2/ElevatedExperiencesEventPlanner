using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ElevatedExperiencesEventPlanner.Data;
using ElevatedExperiencesEventPlanner.Models;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ElevatedExperiencesEventPlanner.Pages.Schedules
{
    public class IndexModel : PageModel
    {
        private readonly ElevatedExperiencesEventPlanner.Data.ElevatedExperiencesEventPlannerContext _context;

        public IndexModel(ElevatedExperiencesEventPlanner.Data.ElevatedExperiencesEventPlannerContext context)
        {
            _context = context;
        }

        public IList<Schedule> Schedule { get;set; } = default!;

        
        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }
        public SelectList? ServiceNames { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? ServicesName { get; set; }

        public string NameSort { get; set; }


        public async Task OnGetAsync(string sortOrder)
        {
            NameSort = sortOrder;
            IQueryable<Schedule> scheduleQuery = from s in _context.Schedule
                                                             select s;


            var schedules = from s in _context.Schedule
                         select s;
            if (!string.IsNullOrEmpty(SearchString))
            {
                schedules = schedules.Where(s => s.ServiceName.Contains(SearchString));
            }

            if (NameSort == "desc")
            {
                schedules = schedules.OrderByDescending(e => e.ScheduleDate);

            }
            else
            {
                schedules = schedules.OrderBy(offeredServices => offeredServices.ScheduleDate);
            }

            Schedule = await schedules.ToListAsync();
        }
    }
}
