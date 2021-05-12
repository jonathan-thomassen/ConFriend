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
    public class DeleteFeatureModel : PageModel
    {
        [BindProperty] public Feature Feature { get; set; }
        private ICrudService<Feature> featureService;
        public DeleteFeatureModel(ICrudService<Feature> fService)
        {
            featureService = fService;
            featureService.Init(ModelTypes.Feature);
        }
        public async Task OnGetAsync(int fId)
        {
            Feature = await featureService.GetFromId(fId);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            await featureService.Delete(Feature.FeatureId);
            return RedirectToPage("Index");
        }
    }
}
