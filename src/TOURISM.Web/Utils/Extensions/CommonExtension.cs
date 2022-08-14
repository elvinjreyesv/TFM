using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TOURISM.Web.Utils.Extensions
{
    public static class CommonExtension
    {
        public static string ReplaceAllWhiteSpaces(this string text)
        {
            return Regex.Replace(text, @"\s+", String.Empty);
        }

        public static string ReplaceBaseOntology(this string text)
        {
            return text.Replace("http://www.semanticweb.org/user/ontologies/2022/0/tourism#", string.Empty)
                .Replace("http://www.w3.org/2002/07/owl#", string.Empty);
        }

        public static string ReplaceOntology(this string text)
        {
            return text.Replace("http://www.semanticweb.org/user/ontologies/2022/0/tourism#", string.Empty)
                .Replace("http://www.w3.org/2002/07/owl#", string.Empty)
                .Replace("^^http://www.w3.org/2001/XMLSchema#int", string.Empty)
                .Replace("^^http://www.w3.org/2001/XMLSchema#double", string.Empty)
                .Replace("^^http://www.w3.org/2001/XMLSchema#boolean", string.Empty)
                .Replace("%E2%80%99", "'")
                .Replace("%C3%A1", "á")
                .Replace("_", " ");
        }
    }
}
