using ElmahCore;
using Microsoft.AspNetCore.Http;
using TOURISM.Web.Utils.Services.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace TOURISM.Web.Utils.Services
{
    public class LogElmahService : ILogService
    {
        private readonly IHttpContextAccessor _context;
        public LogElmahService(IHttpContextAccessor context)
        {
            _context = context;
        }

        public void AddExceptionLog(Exception ex)
        {
            _context.HttpContext.RiseError(ex);
        }

        public void AddExceptionLog(string ex)
        {
            _context.HttpContext.RiseError(new ApplicationException(ex));
        }

        public void AddExceptionLog(string description, Exception ex)
        {
            _context.HttpContext.RiseError(new ApplicationException(description, ex));
        }
    }
}
