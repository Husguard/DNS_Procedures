using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskSystem.Models.Services
{
    public class ServiceResponse
    {
      
        public ServiceStatus Status { get; protected set; }

        
        public string ErrorMessage { get; protected set; }

        
        public static ServiceResponse Success()
        {
            return new ServiceResponse
            {
                Status = ServiceStatus.Success,
                ErrorMessage = string.Empty
            };
        }


        public static ServiceResponse Fail(Exception ex)
        {
            return new ServiceResponse
            {
                Status = ServiceStatus.Fail,
                ErrorMessage = ex.Message
            };
        }


        public static ServiceResponse Warning(string message)
        {
            return new ServiceResponse
            {
                Status = ServiceStatus.Warning,
                ErrorMessage = message
            };
        }
    }
}
