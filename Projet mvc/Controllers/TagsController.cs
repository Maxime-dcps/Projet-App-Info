using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projet_mvc.Core.Domain;
using Projet_mvc.Core.Repository;
using Projet_mvc.Models;

namespace Projet_mvc.Controllers
{
    public class TagsController : Controller
    {
        private readonly ITagRepository _tagRepository;

        public TagsController (ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Policy = "CreateTagsPolicy")]
        public async Task<IActionResult> Create(TagViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Listings");
            }

            var tag = new Tags
            {
                Label = model.Label
            };

            await _tagRepository.CreateTagsAsync(tag);
            return RedirectToAction("Index", "Listings");
        }

    }
}
