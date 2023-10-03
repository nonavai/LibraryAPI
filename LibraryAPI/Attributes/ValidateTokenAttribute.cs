using System.Text.RegularExpressions;
using BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc.Filters;
using Shared.Exceptions;

namespace LibraryAPI.Attributes;

public class ValidateTokenAttribute : ActionFilterAttribute
{
    public override async void OnActionExecuting(ActionExecutingContext context)
    {
        var token = context.HttpContext.Request.Cookies["Authorization"];
        var tokenService = context.HttpContext.RequestServices.GetService<ITokenService>();
        
        var userId = await tokenService.GetUserIdFromToken(token);
        
        var requestQueryString = context.HttpContext.Request.Path;
        var stringRequestUserId = Regex.Match(requestQueryString.Value,  @"/(\w+)/(\d+)");
        var num = stringRequestUserId.Groups[2].Value;
        var requestUserId = Convert.ToInt32(num);
        

        // Check if the user ids match
        if (userId != requestUserId)
        {
            // Throw an exception if the user ids do not match
            throw new NotVerifiedException("The user is not authorized to access this resource.");
        }
    }
}