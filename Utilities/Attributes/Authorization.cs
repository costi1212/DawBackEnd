﻿using Proiect.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Proiect.Utilities.Attributes
{
    public class AuthorizationAttribute: Attribute, IAuthorizationFilter
    {
        private readonly ICollection<Role> _roles;
        public AuthorizationAttribute(params Role[] roles)
        {
            _roles = roles;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var unauthorizedStatusCodeObject = new JsonResult(new { Message = "Unauthorized" })
            { StatusCode = StatusCodes.Status401Unauthorized };
   
            // might not be necessary to check the role; some endpoints just need authorization
            if( _roles == null)
            {

                context.Result = unauthorizedStatusCodeObject;
            }

            var user = (User) context.HttpContext.Items["Users"];
            Console.WriteLine(user);
            if(user == null)
            {  
                Console.WriteLine(user);
                Console.WriteLine("User is null");
                context.Result = unauthorizedStatusCodeObject;
            }

        }
    }
}
