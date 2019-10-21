using System.Web;
using System.Web.Optimization;

namespace WebApplication_Vy
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/jquery-ui*"));


            bundles.Add(new ScriptBundle("~/bundles/DataTables").Include(
            "~/Scripts/datatables.min.js"));

            bundles.Add(new StyleBundle("~/Content/DataTable").Include(
            "~/Content/datatables.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.validate*"));
                
            bundles.Add(new ScriptBundle("~/bundles/cleave").Include(
                "~/Scripts/cleave.min.js",
                "~/Scripts/cleave-phone.no.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.js",
                "~/Scripts/bootstrap-toggle.min.js",
                "~/Scripts/bootstrap.*"));

            bundles.Add(new ScriptBundle("~/bundles/gijgo").Include(
                "~/Scripts/gijgo.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/site.css",
                "~/Content/bootstrap-*",
                "~/Content/bootstrap-toggle.min.css",
                "~/Content/gijgo.min.css",
                "~/Content/themes/base/jquery-ui.css"));
        }
    }
}