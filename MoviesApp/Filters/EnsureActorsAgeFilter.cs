using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MoviesApp.Filters;

public class EnsureActorsAge : Attribute, IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        var birthdate = DateTime.Parse(context.HttpContext.Request.Form["BirthDate"]);

        if (birthdate.Date.Year >= DateTime.Now.Date.Year ||
            DateTime.Now.Date.Year - birthdate.Date.Year < 7 ||
            DateTime.Now.Date.Year - birthdate.Date.Year > 99)
        {
            context.Result = new BadRequestResult();
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }
}