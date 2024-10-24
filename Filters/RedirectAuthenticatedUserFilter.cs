using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace aspnet_mongo.Filters
{
    public class RedirectAuthenticatedUserFilter : IAsyncActionFilter
    {
        private readonly string _redirectController;

        private readonly string _redirectAction;

        public RedirectAuthenticatedUserFilter(string redirectController = "Home", string redirectAction = "Index")
        {
            _redirectController = redirectController;
            _redirectAction = redirectAction;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // verifica se o usuário está autenticado
            if(context.HttpContext.User.Identity.IsAuthenticated)
            {
                // redireciona para a página especificada
                context.Result = new RedirectToActionResult(_redirectAction, _redirectController, null);
                return;
            }

            // continua com a execução da ação se não estiver autenticado
            await next();
        }
    }
}
