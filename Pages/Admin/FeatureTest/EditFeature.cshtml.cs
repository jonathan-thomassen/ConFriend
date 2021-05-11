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
    public class EditFeatureModel : PageModel
    {
        [BindProperty] public Feature Feature { get; set; }
        private ICrudService<Feature> featureService;
        public EditFeatureModel(ICrudService<Feature> fService)
        {
            featureService = fService;
            featureService.Init(ModelTypes.Feature);
        }
        public void OnGet(int fId)
        {
            Feature = featureService.GetFromId(fId);
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            featureService.Update(Feature);
            return RedirectToPage("Index");
        }
    }
}
