using Jobs_Done.Models;
using Jobs_Done.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Jobs_Done.Helpers;

namespace Jobs_Done.Controllers
{
    public class HomeController : Controller
    {
        private DBContext context = DBContext.Instance;
        private VMFactory VMFactory = VMFactory.Instance;
        private Render render = Render.Instance;

        public ActionResult Index()
        {
            List<Case> cases = null;

            if (TempData["SearchResult"] != null)
            {
                cases = TempData["SearchResult"] as List<Case>;
            }
            else
            {
                cases = context.CaseFactory.GetAll();
            }


            return View(cases);
        }

        #region Case

        // id er Case.ID
        public ActionResult ShowCase(int id = 0)
        {
            ViewBag.Tasks = context.TaskFactory.GetAll();
            CaseVM @case = VMFactory.CreateCaseVM(id);
            return View(@case);
        }

        [HttpPost]
        public ActionResult SearchCases(string searchQuery)
        {
            List<Case> searchResult = context.CaseFactory.SearchBy(searchQuery, "Title", "Remarks");

            TempData["SearchResult"] = searchResult;

            return RedirectToAction("Index");
        }

        public ActionResult AddCase()
        {
            var result = new { html = render.RenderPartialToString(this, "AddCase", null) };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddCase(Case @case)
        {
            context.CaseFactory.Insert(@case);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult EditCase(Case @case)
        {
            context.CaseFactory.Update(@case);
            return RedirectToAction("Index");
        }

        public ActionResult DeleteCase(int id = 0)
        {
            context.RelationFactory.DeleteBy("CaseID", id);
            context.CaseFactory.Delete(id);

            return RedirectToAction("Index");
        }

        #endregion

        #region Task

        // id = Case.ID
        public ActionResult AddTask(int id = 0)
        {
            ViewBag.CaseID = id;

            var result = new { html = render.RenderPartialToString(this, "AddTask", context.TaskFactory.GetAll()) };

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddTask(Relation relation, string newTask = null)
        {
            // Hvis IKKE newTask er tom eller null
            if (!string.IsNullOrEmpty(newTask))
            {
                Task task = new Task()
                {
                    Title = newTask
                };
                relation.TaskID = context.TaskFactory.Insert(task).ID;
            }

            context.RelationFactory.Insert(relation);
            return Redirect("/Home/ShowCase/" + relation.CaseID);
        }



        #endregion

        #region Relation

        [HttpPost]
        public ActionResult EditRelation(Relation relation)
        {
            context.RelationFactory.Update(relation);
            return Redirect("/Home/ShowCase/" + relation.CaseID);
        }

        public ActionResult DeleteRelation(int id = 0)
        {
            Relation relation = context.RelationFactory.Get(id);
            context.RelationFactory.Delete(id);
            return Redirect("/Home/ShowCase/" + relation.CaseID);
        }
        #endregion
    }
}