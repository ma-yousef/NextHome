using System.Web;
using System.Web.Optimization;

namespace NextHome
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/defaultCss").Include(
                     "~/Content/custom.css",
                     "~/Content/waitMe.min.css"));

            bundles.Add(new ScriptBundle("~/Scripts/defaultJs").Include(
                     "~/Scripts/jquery.dotdotdot.min.js",
                     "~/Scripts/jquery.validate.js",
                     "~/Scripts/jquery.unobtrusive-ajax.min.js",
                     "~/Scripts/jquery.validate.unobtrusive.min.js",
                     "~/Scripts/waitMe.min.js",
                     "~/Scripts/masonry.pkgd.min.js",
                     "~/Scripts/jquery.appear.js",
                     "~/Scripts/jquery-countTo.js",
                     "~/Scripts/knockout-3.4.2.js",
                     "~/Scripts/initialize.map.js",
                     "~/Scripts/functions.js"
                     ));

            bundles.Add(new StyleBundle("~/ltr-CssBundles").Include(
                      //"~/Content-ltr/bootsnav.css",
                      //"~/Content-ltr/bootstrap.min.css",
                      //"~/Content-ltr/bootstrap-grid.min.css",
                      //"~/Content-ltr/bootstrap-reboot.min.css",
                      //"~/Content-ltr/bootstrap-select.min.css",
                      //"~/Content-ltr/owl.carousel.css",
                      //"~/Content-ltr/owl.transitions.css",
                      "~/Content-rtl/margins-paddings.css",
                      "~/Content-ltr/master.css",
                      "~/Content-ltr/color.css"));

            bundles.Add(new ScriptBundle("~/ltr-JsBundles").Include(
                "~/Scripts-ltr/jquery-3.2.1.min.js",
                "~/Scripts-ltr/bootstrap.min.js",
                "~/Scripts-ltr/bootsnav.js",
                //"~/Scripts-ltr/jquery.appear.js",
                "~/Scripts-ltr/owl.carousel.min.js",
                "~/Scripts-ltr/parallaxie.js",
                "~/Scripts-ltr/jquery.fancybox.min.js",
                "~/Scripts-ltr/cubeportfolio.min.js",
                "~/Scripts-ltr/bootstrap-select.js",
                "~/Scripts-ltr/videobox/video.js",
                "~/Scripts-ltr/datepicker.js",
                "~/Scripts-ltr/dropzone.min.js",
                "~/Scripts-ltr/wow.min.js",
                "~/Scripts-ltr/range-Slider.min.js",
                "~/Scripts-ltr/selectbox-0.2.min.js",
                "~/Scripts-ltr/scrollreveal.min.js",
                //"~/Scripts-ltr/jquery-countTo.js",
                "~/Scripts-ltr/jquery.typewriter.js",
                "~/Scripts-ltr/death.min.js",
                "~/Scripts-ltr/themepunch/jquery.themepunch.tools.min.js",
                "~/Scripts-ltr/themepunch/jquery.themepunch.revolution.min.js",
                "~/Scripts-ltr/themepunch/revolution.extension.layeranimation.min.js",
                "~/Scripts-ltr/themepunch/revolution.extension.navigation.min.js",
                "~/Scripts-ltr/themepunch/revolution.extension.parallax.min.js",
                "~/Scripts-ltr/themepunch/revolution.extension.slideanims.min.js",
                "~/Scripts-ltr/themepunch/revolution.extension.video.min.js",
                "~/Scripts-ltr/functions.js",
                "~/Scripts-ltr/languages.js",
                "~/Scripts-ltr/form.js"));

            bundles.Add(new StyleBundle("~/rtl-CssBundles").Include(
                      //"~/Content-rtl/bootsnav.css",
                      //"~/Content-rtl/bootstrap.min.css",
                      //"~/Content-rtl/bootstrap-grid.min.css",
                      //"~/Content-rtl/bootstrap-reboot.min.css",
                      //"~/Content-rtl/bootstrap-select.min.css",
                      //"~/Content-rtl/owl.carousel.css",
                      //"~/Content-rtl/owl.transitions.css",
                      "~/Content-rtl/margins-paddings.css",
                      "~/Content-rtl/master.css",
                      "~/Content-rtl/color.css"));

            bundles.Add(new ScriptBundle("~/rtl-JsBundles").Include(
                "~/Scripts-rtl/jquery-3.2.1.min.js",
                "~/Scripts-rtl/bootstrap.min.js",
                "~/Scripts-rtl/bootsnav.js",
                //"~/Scripts-rtl/jquery.appear.js",
                "~/Scripts-rtl/owl.carousel.min.js",
                "~/Scripts-rtl/parallaxie.js",
                "~/Scripts-rtl/jquery.fancybox.min.js",
                "~/Scripts-rtl/cubeportfolio.min.js",
                "~/Scripts-rtl/bootstrap-select.js",
                "~/Scripts-rtl/videobox/video.js",
                "~/Scripts-rtl/datepicker.js",
                "~/Scripts-rtl/dropzone.min.js",
                "~/Scripts-rtl/wow.min.js",
                "~/Scripts-rtl/range-Slider.min.js",
                "~/Scripts-rtl/selectbox-0.2.min.js",
                "~/Scripts-ltr/scrollreveal.min.js",
                //"~/Scripts-rtl/jquery-countTo.js",
                "~/Scripts-rtl/jquery.typewriter.js",
                "~/Scripts-rtl/death.min.js",
                "~/Scripts-rtl/themepunch/jquery.themepunch.tools.min.js",
                "~/Scripts-rtl/themepunch/jquery.themepunch.revolution.min.js",
                "~/Scripts-rtl/themepunch/revolution.extension.layeranimation.min.js",
                "~/Scripts-rtl/themepunch/revolution.extension.navigation.min.js",
                "~/Scripts-rtl/themepunch/revolution.extension.parallax.min.js",
                "~/Scripts-rtl/themepunch/revolution.extension.slideanims.min.js",
                "~/Scripts-rtl/themepunch/revolution.extension.video.min.js",
                "~/Scripts-rtl/functions.js",
                "~/Scripts-rtl/languages.js",
                "~/Scripts-rtl/form.js"));

        }
    }
}
