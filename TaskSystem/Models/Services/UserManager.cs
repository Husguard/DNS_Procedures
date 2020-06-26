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
        private IHttpContextAccessor _httpContext;
        public int _currentUserId { get; }
        public UserManager(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
            _currentUserId = Convert.ToInt32(httpContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
        }
    }
}
