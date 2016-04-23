using System.Web;
using System.Web.Optimization;

namespace fleet_tracker
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.UseCdn = true;

            bundles.Add(new StyleBundle("~/bundles/core-styles-css").Include(
                    "~/assets/css/bootstrap.min.css",
                    "~/assets/css/bootstrap-extend.min.css",
                    "~/assets/css/site.min.css",
                    "~/assets/vendor/animsition/animsition.css",
                    "~/assets/vendor/asscrollable/asScrollable.css",
                    "~/assets/vendor/switchery/switchery.css",
                    "~/assets/vendor/intro-js/introjs.css",
                    "~/assets/vendor/slidepanel/slidePanel.css",
                    "~/assets/vendor/flag-icon-css/flag-icon.css",
                    "~/assets/vendor/chartist-js/chartist.css",
                    "~/assets/vendor/jvectormap/jquery-jvectormap.css",
                    "~/assets/fonts/weather-icons/weather-icons.css",
                    "~/assets/css/dashboard/v1.css",
                    "~/assets/css/site_custom.css"
                ));

            bundles.Add(new StyleBundle("~/bundles/core-fonts-css")
                   .Include(
                    "~/assets/fonts/web-icons/web-icons.min.css", new CssRewriteUrlTransform())
                   .Include("~/assets/fonts/brand-icons/brand-icons.min.css", new CssRewriteUrlTransform())
                   );

            bundles.Add(new StyleBundle("~/bundles/roboto-font-css", "http://fonts.googleapis.com/css?family=Roboto:300,400,500,300italic").Include(
                    "~/assets/fonts/google/css.css"
                ));

            bundles.Add(new ScriptBundle("~/bundles/header-scripts").Include(
                        "~/assets/vendor/modernizr/modernizr.js",
                        "~/assets/vendor/breakpoints/breakpoints.js"));

            bundles.Add(new ScriptBundle("~/bundles/core-scripts").Include(
                        "~/assets/vendor/jquery/jquery.js",
                        "~/assets/vendor/bootstrap/bootstrap.js",
                        "~/assets/vendor/animsition/jquery.animsition.js",
                        "~/assets/vendor/asscroll/jquery-asScroll.js",
                        "~/assets/vendor/mousewheel/jquery.mousewheel.js",
                        "~/assets/vendor/asscrollable/jquery.asScrollable.all.js",
                        "~/assets/vendor/ashoverscroll/jquery-asHoverScroll.js"));

            bundles.Add(new ScriptBundle("~/bundles/plugin-scripts").Include(
                        "~/assets/vendor/switchery/switchery.min.js",
                        "~/assets/vendor/intro-js/intro.js",
                        "~/assets/vendor/screenfull/screenfull.js",
                        "~/assets/vendor/slidepanel/jquery-slidePanel.js",
                        "~/assets/vendor/skycons/skycons.js",
                        "~/assets/vendor/chartist-js/chartist.min.js",
                        "~/assets/vendor/aspieprogress/jquery-asPieProgress.min.js",
                        "~/assets/vendor/jvectormap/jquery-jvectormap.min.js",
                        "~/assets/vendor/jvectormap/maps/jquery-jvectormap-ca-lcc-en.js",
                        "~/assets/vendor/matchheight/jquery.matchHeight-min.js"));

            bundles.Add(new ScriptBundle("~/bundles/googlemapsapi-scripts", "http://maps.google.com/maps/api/js?sensor=false").Include(
                    "~/assets/vendor/google/api.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/page-scripts").Include(
                        "~/assets/vendor/gmaps/gmaps.js",
                        "~/assets/js/core.js",
                        "~/assets/js/site.js",
                        "~/assets/js/sections/menu.js",
                        "~/assets/js/sections/menubar.js",
                        "~/assets/js/sections/sidebar.js",
                        "~/assets/js/configs/config-colors.js",
                        "~/assets/js/configs/config-tour.js",
                        "~/assets/js/components/asscrollable.js",
                        "~/assets/js/components/animsition.js",
                        "~/assets/js/components/slidepanel.js",
                        "~/assets/js/components/switchery.js",
                        "~/assets/js/components/matchheight.js",
                        "~/assets/js/components/jvectormap.js",
                        "~/assets/js/components/gmaps.js"));

            
            /*

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
            */
        }
    }
}
