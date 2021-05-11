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
    public class CreateFeatureModel : PageModel
    {
        [BindProperty] public Feature Feature { get; set; }
        private ICrudService<Feature> featureService;
        public CreateFeatureModel(ICrudService<Feature> fService)
        {
            featureService = fService;
            featureService.Init(ModelTypes.Feature);
            Feature = new Feature();
        }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            featureService.Create(Feature);
            return RedirectToPage("Index");
        }
    }
}
