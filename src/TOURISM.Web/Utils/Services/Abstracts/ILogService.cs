using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TOURISM.Web.Utils.Services.Abstracts
{
    public interface ILogService
    {
        void AddExceptionLog(Exception ex);
        void AddExceptionLog(string ex);
        void AddExceptionLog(string description, Exception ex);
    }
}
