using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConFriend.Interfaces;
using ConFriend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ConFriend.Pages.Admin.FloorTest
{
    public class IndexModel : PageModel
    {
        [BindProperty(SupportsGet = true)] public string FilterCriteria { get; set; }
        public List<Floor> Floors { get; private set; }

        private ICrudService<Floor> floorService;

        public IndexModel(ICrudService<Floor> fService)
        {
            floorService = fService;
            floorService.Init(ModelTypes.Floor);
        }
        public async Task OnGetAsync()
        {
            Floors = await floorService.GetAll();
        }
    }
}
