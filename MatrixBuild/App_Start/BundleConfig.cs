using System.Web;
using System.Web.Optimization;
using System.Web.Optimization.React;

namespace MatrixBuild
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/Scripts/jquery-{version}.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //            "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));

            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //          "~/Scripts/bootstrap.js"));

            //V8
            //React
            bundles.Add(new ScriptBundle("~/reactjs").Include(
                    "~/Scripts/react/react.min.js",
                    "~/Scripts/react/react-dom.min.js"));

            bundles.Add(new Bundle("~/matrixjs", new BabelTransform(), new JsMinify())
                    .Include("~/Scripts/jsx/matrixjs.jsx"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/animate.min.css",
                      "~/Content/site.css"));
        }
    }
}
