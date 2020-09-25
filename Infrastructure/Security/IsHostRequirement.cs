using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Persistence;

namespace Infrastructure.Security
{
    public class IsHostRequirement : IAuthorizationRequirement { }

    public class IsHostRequirementHandler : AuthorizationHandler<IsHostRequirement>
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public IsHostRequirementHandler(IHttpContextAccessor httpContextAccessor, 
            DataContext context)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IsHostRequirement requirement)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (context.Resource is AuthorizationFilterContext authContext)
            {
                var currentUserName = _httpContextAccessor.HttpContext.User?.Claims?.SingleOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

                var activityId = Guid.Parse(authContext.RouteData.Values["id"].ToString());
                
                var activity = _context.Activities.FindAsync(activityId).Result;

                var host = activity.UserActivities.FirstOrDefault(x => x.IsHost);

                if (host?.AppUser?.UserName == currentUserName)
                    context.Succeed(requirement);
            } else {
                context.Fail();
            }
            
            return Task.CompletedTask;
        //    var currentUserName = _httpContextAccessor.HttpContext.User?.Claims?.SingleOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

        //     var activityId = Guid.Parse(_httpContextAccessor.HttpContext.Request.RouteValues.SingleOrDefault(x => x.Key == "id").Value.ToString());

        //     var activity = _context.Activities.FindAsync(activityId).Result;

        //     var host = activity.UserActivities.FirstOrDefault(x => x.IsHost);

        //     if (host?.AppUser?.UserName == currentUserName)
        //         context.Succeed(requirement);

        //     return Task.CompletedTask;


        // var currentUsername = _httpContextAccessor.HttpContext.User?.Claims?.SingleOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

        //     // Getting the RouteValue here is very different to that presented in the course.  The course
        //     //  uses httpCtxAcc.HttpCtx.Request.RouteValues... but this is not available in later
        //     //  versions of AspNetCore.Http.  Instead we also need AspNetCore.Routing and the approach
        //     //  found below...

        //     var routeData = _httpContextAccessor.HttpContext.GetRouteData();
        //     if (!routeData.Values.TryGetValue("id", out object idParam))
        //         throw new ApplicationException("IsActivityHost policy incorrectly applied (no activity 'id' param on this end-point)");

        //     var activityId = Guid.Parse(idParam.ToString());
        //     var activity = _context.Activities.FindAsync(activityId).Result;
        //     if (activity != null)
        //     {
        //         var host = activity.UserActivities.FirstOrDefault(x => x.IsHost);

        //         if (host?.AppUser?.UserName == currentUsername)
        //             AuthContext.Succeed(requirement);
        //     }
        //     else
        //         AuthContext.Succeed(requirement);
        //     return Task.CompletedTask;
        }
    }
}