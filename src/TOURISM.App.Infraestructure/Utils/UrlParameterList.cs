using System;
using System.Collections.Generic;

namespace TOURISM.App.Infrastructure.Utils
{
    public class UrlParameterList
    {
        public List<UrlParameters> ParameterList { get; set; }

        public UrlParameterList()
        {
            ParameterList = new List<UrlParameters>();
        }

        public void Add(UrlParameters urlParameter)
        {
            ParameterList.Add(urlParameter);
        }

        public override string ToString()
        {
            string _result = String.Empty;
            string _separator = String.Empty;

            foreach (var item in ParameterList)
            {
                _result += _separator + item.GetParam();
                _separator = "&";
            }

            return _result;
        }
    }
}
