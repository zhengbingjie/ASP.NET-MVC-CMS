/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System.Web.Mvc;
using Easy.CMS.Section.Models;
using Easy.CMS.Section.Service;
using Easy.Constant;
using Easy.Web.Attribute;
using Easy.Web.Authorize;

namespace Easy.CMS.Section.Controllers
{
    [PopUp, DefaultAuthorize]
    public class SectionContentImageController : Controller
    {
        private readonly ISectionContentProviderService _sectionContentProviderService;

        public SectionContentImageController(ISectionContentProviderService sectionContentProviderService)
        {
            _sectionContentProviderService = sectionContentProviderService;
        }

        public ActionResult Create(int sectionGroupId, string sectionWidgetId)
        {
            return View("Form", new SectionContentImage
            {
                SectionGroupId = sectionGroupId,
                SectionWidgetId = sectionWidgetId,
                ActionType = ActionType.Create
            });
        }
        
        public ActionResult Edit(int Id)
        {
            var content = _sectionContentProviderService.Get(Id);
            content.ActionType = ActionType.Update;
            return View("Form", content);
        }
        [HttpPost]
        public ActionResult Save(SectionContentImage content)
        {
            if (!ModelState.IsValid)
            {
                return View("Form", content);
            }
            if (content.ActionType == ActionType.Create)
            {
                _sectionContentProviderService.Add(content);
            }
            else
            {
                _sectionContentProviderService.Update(content);
            }
            ViewBag.Close = true;
            return View("Form", content);
        }

        public JsonResult Delete(int Id)
        {
            _sectionContentProviderService.Delete(Id);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}
