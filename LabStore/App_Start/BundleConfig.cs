using System.Web;
using System.Web.Optimization;

namespace LabStore
{
    public class BundleConfig
    {
        // Aby uzyskać więcej informacji o grupowaniu, odwiedź stronę https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/datatables/jquery.datatables.js",
                        "~/Scripts/datatables/datatables.bootstrap.js", 
                        "~/Scripts/bootbox.js",
                        "~/Scripts/moment-with-locales.js",
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/typeahead.bundle.js",
                        "~/Scripts/bootstrap-datetimepicker.js"
                        ));


            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Użyj wersji deweloperskiej biblioteki Modernizr do nauki i opracowywania rozwiązań. Następnie, kiedy wszystko będzie
            // gotowe do produkcji, użyj narzędzia do kompilowania ze strony https://modernizr.com, aby wybrać wyłącznie potrzebne testy.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap-united.css",
                       "~/Content/datatables/css/datatables.bootstrap.css",
                       "~/Content/datatables/css/buttons.bootstrap.min.css",
                       "~/Content/datatables/css/jquery.datatables.min.css",
                       "~/Content/datatables/css/buttons.datatables.min.css",
                       "~/Content/typeahead.css",
                       "~/Content/bootstrap-datetimepicker.css",
                      "~/Content/Site.css"));
        }
    }
}
