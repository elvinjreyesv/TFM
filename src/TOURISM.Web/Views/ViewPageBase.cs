using Microsoft.AspNetCore.Mvc.Razor;
using TOURISM.Web.Utils.Extensions;
using TOURISM.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TOURISM.Web.Views
{
    public abstract class ViewPageBase : PublicViewPageBase<dynamic>
    { }

    public abstract class PublicViewPageBase<TModel> : RazorPage<TModel>
    {
        protected PublicViewPageBase()
        {
            LocalizationManager = new LocalizationManager(Resources.Lang.ResourceManager);
        }

        public override void BeginContext(int position, int length, bool isLiteral)
        {
            base.BeginContext(position, length, isLiteral);

            var cultureToAssign = ViewBag.CultureTwoLetterName == "ES" ? "es-us" : "en-us";
            if (cultureToAssign == Thread.CurrentThread.CurrentCulture.IetfLanguageTag)
                return;

            Thread.CurrentThread.CurrentCulture = new CultureInfo(cultureToAssign);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
        }

        public LocalizationManager LocalizationManager { get; private set; }

        protected virtual string Lang(string name)
        {
            var cultureToAssign = ViewBag.CultureTwoLetterName == "ES" ? "es-us" : "en-us";
            if (Thread.CurrentThread.CurrentCulture.Name != cultureToAssign)
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo(cultureToAssign);
                Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
            }

            var translation = LocalizationManager.GetString(name, Thread.CurrentThread.CurrentCulture);
            if (string.IsNullOrWhiteSpace(translation))
                translation = name;

            return translation;
        }

        protected virtual string D(DateTime dateTime)
        {
            return dateTime.ToString("d", Thread.CurrentThread.CurrentCulture);
        }
    }
}
