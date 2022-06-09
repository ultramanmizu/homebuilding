using System.Web;
using System.Web.Optimization;

namespace HomeBuilding
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/css/bootstrap.css",
                "~/Content/css/style.css",
                "~/Content/css/plugins.css",
                "~/Content/css/colors/sky.css"));
        }
    }
}
