using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Jobs_Done.Helpers
{
    public class Render
    {
        private static volatile Render INSTANCE;
        public static Render Instance
        {
            get
            {
                if (INSTANCE == null)
                {
                    INSTANCE = new Render();
                }
                return INSTANCE;
            }
        }

        public string RenderPartialToString(Controller controller, string viewName, object model)
        {
            controller.ViewData.Model = model;

            using (StringWriter sw = new StringWriter())
            {
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(controller.ControllerContext, viewName);
                ViewContext viewContext = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData, controller.TempData, sw);
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }
    }
}