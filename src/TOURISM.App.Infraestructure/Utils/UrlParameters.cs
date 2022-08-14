using System;

namespace TOURISM.App.Infrastructure.Utils
{
    public class UrlParameters
    {
        public string ParamName { get; set; }

        public string ParamValue { get; set; }

        public string GetParam()
        {
            return String.Concat(ParamName, "=", ParamValue);
        }
    }
}
