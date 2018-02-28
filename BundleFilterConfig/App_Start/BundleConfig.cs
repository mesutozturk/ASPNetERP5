using System.Web.Optimization;

namespace BundleFilterConfig
{
    public class BundleConfig
    {
        public static void RegisterBundle(BundleCollection bundles)
        {
            BundleTable.EnableOptimizations = true;
            bundles.Add(new StyleBundle("~/bundles/styles").Include(
                "~/Content/bootstrap.css",
                "~/Content/bootstrap-grid.css",
                "~/Content/bootstrap-reboot.css",
                "~/Content/themes/base/all.css",
                "~/Content/site.css"
                ));

            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                "~/Scripts/jquery-3.3.1.js",
                "~/Scripts/jquery-ui-1.12.1.js",
                "~/Scripts/bootstrap.js",
                "~/Scripts/jquery.validate.js",
                "~/Scripts/moment.js",
                "~/Scripts/popper.js",
                "~/Scripts/app.js"
                ));
        }
    }
}