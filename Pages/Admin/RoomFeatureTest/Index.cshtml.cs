using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConFriend.Interfaces;
using ConFriend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ConFriend.Pages.Admin.RoomFeatureTest
{
    public class IndexModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string FilterCriteria { get; set; }

        public List<RoomFeature> RoomFeatures { get; private set; }

        private readonly ICrudService<RoomFeature> _roomFeatureService;

        public IndexModel(ICrudService<RoomFeature> rfService)
        {
            _roomFeatureService = rfService;
            _roomFeatureService.Init_Composite(ModelTypes.Feature, ModelTypes.Room, ModelTypes.RoomFeature);
            RoomFeatures = _roomFeatureService.GetAll();
        }
        public void OnGet()
        {
        }
    }
}