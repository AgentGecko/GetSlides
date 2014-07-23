using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace GetSlides.API
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            /*
            bundles.Add(new ScriptBundle("~/bundles/vendor").Include("~/Scripts/jquery-{version}.js",
                                                                     "~/Scripts/bootstrap.min.js",
                                                                     "~/Scripts/jquery.validate.min.js",
                                                                     "~/Scripts/knockout-{version}.js"));
            bundles.Add(new ScriptBundle("~/bundles/source").Include("~/Scripts/TypeScript/*.js"));
            bundles.Add(new StyleBundle("~/bundles/vendorcss").Include("~/Content/bootstrap-theme.min.css",
                                                                    "~/Content/bootstrap.min.css"));
            bundles.Add(new StyleBundle("~/bundles/sourcecss").Include("~/Content/Source/*.css"));
            */
            BundleTable.EnableOptimizations = false;
        }
    }
}