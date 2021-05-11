using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConFriend.Interfaces;
using ConFriend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ConFriend.Pages.Admin.FeatureTest
{
    public class IndexModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string FilterCriteria { get; set; }

        public List<Feature> Features { get; private set; }

        private readonly ICrudService<Feature> _featureService;

        public IndexModel(ICrudService<Feature> fService)
        {
            _featureService = fService;
            _featureService.Init(ModelTypes.Feature);
            Features = _featureService.GetAll();
        }
        public void OnGet()
        {
        }
    }
}
