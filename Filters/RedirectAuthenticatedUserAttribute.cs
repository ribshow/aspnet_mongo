using Microsoft.AspNetCore.Mvc;

namespace aspnet_mongo.Filters
{
    public class RedirectAuthenticatedUserAttribute : TypeFilterAttribute
    {
        public RedirectAuthenticatedUserAttribute(string redirectController = "Home", string redirectAction = "Index") : base(typeof(RedirectAuthenticatedUserFilter))
        {
            Arguments = new object[] { redirectController, redirectAction };
        }
    }
}
