using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace TaskSystem.Models.Services
{
    public class UserManager
    {
        private readonly IHttpContextAccessor _httpContext;
        public int CurrentUserId { get; }
        public UserManager(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
            CurrentUserId = Convert.ToInt32(httpContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
        }
    }
}
