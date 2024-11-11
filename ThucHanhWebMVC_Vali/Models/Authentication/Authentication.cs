using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc;

namespace ThucHanhWebMVC_Vali.Models.Authentication
{
    public class Authentication : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // Kiểm tra nếu chưa có tên người dùng trong session
            if (context.HttpContext.Session.GetString("UserName") == null)
            {
                // Nếu chưa có, chuyển hướng đến trang login
                context.Result = new RedirectToRouteResult(
                    new RouteValueDictionary
                    {
                        { "Controller", "Access" },
                        { "Action", "Login" }
                    });
            }
        }
    }
}
