using System.Web.Mvc;
using log4net;
using UTILS.Utils.Logging;

namespace UTILS.Utils.Filters
{
    public class ControllerExceptionFilter : ActionFilterAttribute
    {
        private static readonly ILog Log = LogHelper.GetLogger();
        
        /// <summary>Called by the ASP.NET MVC framework after the action method executes.</summary>
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.Exception != null) Log.Error(filterContext.Exception.Message);
        }

        /// <summary>Called by the ASP.NET MVC framework after the action result executes.</summary>
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            if (filterContext.Exception != null) Log.Error(filterContext.Exception.Message);
        }
    }
}