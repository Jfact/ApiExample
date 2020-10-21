using EntitiesLibrary.Entities;
using LoggerLibrary;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace ApiApplication.Controllers.Filters
{
    public class AuthorizeActionFilter : IActionFilter
    {
        private readonly ILoggerManager _logger;
        private readonly UserManager<User> _userManager;
        public AuthorizeActionFilter(ILoggerManager logger)
        {
            _logger = logger;
        }

        public async void OnActionExecuting(ActionExecutingContext context)
        {
            User user = await _userManager.FindByEmailAsync(context.HttpContext.User.Identity.Name);

            var action = context.RouteData.Values["action"];
            var controller = context.RouteData.Values["controller"];
            var param = context.ActionArguments
                .SingleOrDefault(x => x.Value.ToString().Contains("Dto")).Value;

            if (param == null)
            {
                _logger.LogError(
                    LoggerMessage.ClientObjectIsNull(controller, action)
                    );

                context.Result = new BadRequestObjectResult($"Object is null. Controller: { controller }, action: { action}");

                return;
            }

            if (!context.ModelState.IsValid)
            {
                _logger.LogError(
                    LoggerMessage.ClientModelStateIsInvalid(controller, action)
                    );

                context.Result = new UnprocessableEntityObjectResult(context.ModelState);
            }
        }
        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}
