using System.Web.Mvc;
using HoS_AP.BLL.Models;
using HoS_AP.BLL.ServiceInterfaces;

namespace HoS_AP.Web.Controllers
{
    [Authorize, RoutePrefix("characters")]
    public class CharacterController : Controller
    {
        private readonly ICharacterPresentationService characterPresentationService;
        private readonly ICharacterOperationService characterOperationService;

        public CharacterController(ICharacterPresentationService characterPresentationService, 
            ICharacterOperationService characterOperationService)
        {
            this.characterPresentationService = characterPresentationService;
            this.characterOperationService = characterOperationService;
        }

        [Route]
        public ActionResult Index()
        {
            return View(characterPresentationService.List());
        }

        [Route("add")]
        public ActionResult Add()
        {
            return View(new CharacterEditModel {Active = true });
        }

        [Route("add"), HttpPost]
        public ActionResult Add(CharacterEditModel model)
        {
            return Edit(model);
        }

        [Route("{name}/edit")]
        public ActionResult Edit(string name)
        {
            var model = characterPresentationService.Load(name);
            if (model == null)
            {
                return new HttpNotFoundResult();
            }

            return View(model);
        }

        [Route("{name}/edit"), HttpPost]
        public ActionResult Edit(CharacterEditModel model)
        {
            var operationResult = characterOperationService.Save(model);
            if (operationResult.IsValid)
            {
                return RedirectToAction("Index");
            }

            operationResult.ToModelErrors(ModelState);
            return View(model);
        }

        [Route("{name}/delete"), HttpPost]
        public ActionResult Delete(string name)
        {
            characterOperationService.Delete(name);
            return RedirectToAction("Index");
        }

        [Route("{name}/recover"), HttpPost]
        public ActionResult Recover(string name)
        {
            characterOperationService.Recover(name);
            return RedirectToAction("Index");
        }
    }
}