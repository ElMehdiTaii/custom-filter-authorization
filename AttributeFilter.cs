using System;
using System.Web.Mvc;
namespace Application.Attributes
{
public class SessionManager : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            bool skipAuthorization = filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true)
                         || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true);

            bool checkAuthorization = filterContext.ActionDescriptor.IsDefined(typeof(CheckAuthorization), true)
                         || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(CheckAuthorization), true);

            string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;

            if (checkAuthorization && !SessionHelper.IsExisting("token"))
            {
                filterContext.Result = new JsonResult() { Data = "/home/index" };
            }

            if (!skipAuthorization)
            {
                if (!SessionHelper.IsSessionIdExist() && controllerName.ToUpper() != "HOME")
                {
                    filterContext.Result = new RedirectResult("~/Home/Index");
                }
                else if (SessionHelper.IsSessionIdExist() && controllerName.ToUpper() == "HOME")
                {
                    filterContext.Result = new RedirectResult("~/application/Index");
                }
            }
        }
    }
    public class CheckAuthorization : Attribute
    {

    }
    }
