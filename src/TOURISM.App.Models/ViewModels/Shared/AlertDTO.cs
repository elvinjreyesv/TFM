using TOURISM.App.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace TOURISM.App.Models.ViewModels.Shared
{
    public class AlertDTO
    {
        public EAlertResponse Type { get; set; }
        public string Message { get; set; }
        public string RedirectTo { get; set; }
        public bool Closable { get; set; }

        private AlertDTO() { }

        public static AlertDTO Success(string message, string redirect = "", bool closable = true) => new AlertDTO()
        {
            Closable = closable,
            RedirectTo = redirect,
            Message = message,
            Type = EAlertResponse.Success
        };

        public static AlertDTO Error(string message, string redirect = "", bool closable = true) => new AlertDTO()
        {
            Closable = closable,
            RedirectTo = redirect,
            Message = message,
            Type = EAlertResponse.Error
        };
    }
}
