﻿using System.Web;
using System.Web.Optimization;

namespace ssembassy_ankara
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-3.2.1.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrapjs").Include(
                "~/Scripts/bootstrap.min.js",
                "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/dashboardjs").Include(
                "~/Scripts/jquery.slimscroll.min.js",
                "~/Scripts/fastclick.js",
                "~/Scripts/app.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/defaultjs").Include(
                "~/Scripts/main.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/icheck").Include(
                "~/Scripts/icheck.min.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/datepicker").Include(
                "~/Scripts/bootstrap-datepicker.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/bxslider").Include(
                "~/Content/default/bxslider/jquery.bxslider.min.js"
            ));

            bundles.Add(new StyleBundle("~/css/dashboard").Include(
                      "~/Content/dashboard/bootstrap.min.css",
                      "~/Content/dashboard/font-awesome.css",
                      "~/Content/dashboard/skin-blue.css",
                      "~/Content/dashboard/AdminLTE.min.css",
                      "~/Content/dashboard/main.css"
                      ));

            bundles.Add(new StyleBundle("~/default/css").Include(
                "~/Content/default/main.css"));

            bundles.Add(new StyleBundle("~/css/icheck").Include(
                    "~/Content/dashboard/blue.css"
                ));

            bundles.Add(new StyleBundle("~/css/datepicker").Include(
                "~/Content/datepicker/datepicker3.css"
            ));

            bundles.Add(new StyleBundle("~/css/bxslider").Include(
                "~/Content/bxslider/jquery.bxlider.min.css"
            ));
        }
    }
}
