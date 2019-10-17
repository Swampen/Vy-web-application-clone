
using System.Web.Mvc;
using log4net;
using WebApplication_Vy.Utils.Logging;

namespace WebApplication_Vy.Utils.Filters
{
    public class ControllerExeptionFilter : ActionFilterAttribute
    {
        private static readonly ILog Log = LogHelper.GetLogger(); 
        /// <summary>Called by the ASP.NET MVC framework before the action method executes.</summary>
        public virtual void OnActionExecuting(ActionExecutingContext filterContext)
        {
            
        }

        /// <summary>Called by the ASP.NET MVC framework after the action method executes.</summary>
        public virtual void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.Exception != null)
            {
                Log.Error(filterContext.Exception.Message);
            }
        }

        /// <summary>Called by the ASP.NET MVC framework before the action result executes.</summary>
        public virtual void OnResultExecuting(ResultExecutingContext filterContext)
        {
        }

        /// <summary>Called by the ASP.NET MVC framework after the action result executes.</summary>
        public virtual void OnResultExecuted(ResultExecutedContext filterContext)
        {
            if (filterContext.Exception != null)
            {
             Log.Error(filterContext.Exception.Message);   
            }
        }
        
    }
}