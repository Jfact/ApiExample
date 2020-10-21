﻿using LoggerLibrary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace ApiApplication.Controllers.Filters
{
    public class ValidationActionFilter : IActionFilter
    {
        private readonly ILoggerManager _logger;
        public ValidationActionFilter(ILoggerManager logger)
        {
            _logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
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
