using System.Web.Optimization;
using System.Web.UI.WebControls;

namespace DoctorPremium.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //-------------------------------------------------------------------------- My bundles
			bundles.Add(new StyleBundle("~/Content/assetsLayoutCSS").Include(
							 "~/Content/css/bootstrap/bootstrap.css",
							 "~/Content/css/bootstrap/bootstrap-theme.css",
							 "~/Content/css/supr-theme/jquery.ui.supr.css",
							 "~/Content/css/main.css",
							 "~/Content/css/icons.css",
							 "~/Content/css/custom.css",
							 "~/Content/plugins/forms/uniform/uniform.default.css",
							 "~/Content/js/plugins/chosen/chosen.min.css",
							 "~/Content/css/digital_clock.css",
							 "~/Content/plugins/misc/qtip/jquery.qtip.css"
							 , "~/Content/plugins/datepicker/css/datepicker.css"
							 , "~/Content/plugins/timepicker/css/bootstrap-timepicker.css"
							 , "~/Content/plugins/datepicker/css/bootstrap-datepicker.min.css"
							 , "~/Content/plugins/datepicker/css/datepicker.css"
                             , "~/Content/plugins/forms/switch/static/stylesheets/bootstrap-switch.css",
								"~/Content/bootstrap/bootstrap.min.css"
                             

                   ));

			bundles.Add(new ScriptBundle("~/bundles/assetsLayoutJS").Include(
                   "~/Scripts/jquery-2.0.3.min.js",
				   "~/Content/js/libs/modernizr.js",
				   "~/Scripts/respond.min.js",
				   "~/Content/js/bootstrap/bootstrap.min.js",
				   "~/Content/js/supr-theme/jquery-ui-1.8.22.custom.min.js",
				   "~/Content/js/digitam_clock.js",
				   "~/Content/js/moment.js",
				   "~/Content/js/main.js",
				   "~/Scripts/common.js",
				   "~/Content/js/plugins/chosen/chosen.jquery.min.js",
				   "~/Content/plugins/misc/qtip/jquery.qtip.min.js",
				   "~/Content/plugins/datepicker/js/bootstrap-datepicker.min.js",
				   "~/Content/plugins/datepicker/locales/bootstrap-datepicker.ru.min.js",
				   "~/Content/plugins/timepicker/js/bootstrap-timepicker.js",
					"~/Content/plugins/forms/uniform/jquery.uniform.min.js",
					"~/Content/js/libs/jRespond.min.js",
                    "~/Content/plugins/forms/select/select2.min.js",
                   "~/Content/plugins/forms/tiny_mce/tinymce.min.js",
				   //, "~/Content/plugins/forms/switch/static/js/bootstrap-switch.min.js",
                   "~/Content/HelpPage/assets/jquery.js",
                   "~/Content/plugins/forms/switch/static/js/bootstrap-switch.js" 
                ));

            bundles.Add(new StyleBundle("~/Content/assetsScheduleCSS").Include(
						"~/Content/plugins/fullcalendar-1.6.2/fullcalendar/fullcalendar.print.css",
						"~/Content/plugins/fullcalendar-1.6.2/fullcalendar/fullcalendar.css",
						"~/Content/css/calendar.css"
                    ));



            bundles.Add(new ScriptBundle("~/bundles/assetsScheduleJS").Include(
			   "~/Content/js/calendarInit.js",
			   "~/Content/plugins/fullcalendar-1.6.2/fullcalendar/fullcalendar.js",
				"~/Content/plugins/fullcalendar-1.6.2/fullcalendar/gcal.js",
				"~/Scripts/jquery-migrate-1.0.0.js"
               ));

            bundles.Add(new ScriptBundle("~/bundles/assetsPatientJS").Include(
               "~/Scripts/jquery.unobtrusive-ajax.min.js",
			   "~/Content/plugins/jasny/js/bootstrap-inputmask.js",
				"~/Content/plugins/forms/switch/static/js/bootstrap-switch.js",
				//"~/Content/plugins/forms/switch/static/js/bootstrap-switch.min.js",
				"~/Scripts/Countries.js"
               ));
			bundles.Add(new StyleBundle("~/Content/assetsPatientCSS").Include(
						"~/Content/plugins/forms/switch/static/stylesheets/bootstrap-switch.css"
						));

            bundles.Add(new StyleBundle("~/Content/assetsLoginCSS").Include(
						"~/Content/css/bootstrap/bootstrap.css",
						"~/Content/css/bootstrap/bootstrap-responsive.css",
						"~/Content/css/supr-theme/jquery.ui.supr.css",
						"~/Content/plugins/forms/uniform/uniform.default.css",
						"~/Content/css/icons.css",
						"~/Content/css/main.css"
                        ));


			bundles.Add(new ScriptBundle("~/Content/assetsLoginJS").Include(
			   "~/Scripts/jquery-2.0.3.min.js",
               "~/Content/js/bootstrap/bootstrap.min.js",
			   "~/Content/plugins/forms/uniform/jquery.uniform.min.js",
			   "~/Content/js/libs/modernizr.js",
               "~/Scripts/jquery.validate.min.js",
			   "~/Scripts/common.js",
			   "~/Scripts/Countries.js"
               ));

			bundles.Add(new StyleBundle("~/bundles/ErrorCSS").Include(
			"~/Content/css/bootstrap/bootstrap.css",
			"~/Content/css/bootstrap/bootstrap-responsive.css",
			"~/Content/css/icons.css",
			"~/Content/css/main.css"
               ));

            bundles.Add(new StyleBundle("~/bundles/UploadCSS").Include(
			 "~/Content/css/bootstrap-fileupload.min.css"
             ));

            bundles.Add(new ScriptBundle("~/bundles/UploadJS").Include(
			 "~/Content/plugins/jasny/js/bootstrap-fileupload.js" 
			 ));

			bundles.Add(new StyleBundle("~/bundles/TablesCSS").Include(
             "~/Content/plugins/tables/dataTables/jquery.dataTables.css",
             "~/Content/plugins/forms/uniform/uniform.default.css"
             ));

			bundles.Add(new ScriptBundle("~/bundles/TablesJS").Include(
			"~/Scripts/Tables.js",
			"~/Content/plugins/dataTables/jquery.dataTables.js",
				"~/Content/plugins/dataTables/dataTables.bootstrap.js"
			 ));

			bundles.Add(new ScriptBundle("~/bundles/UnobtrusiveJS").Include(
                      "~/Scripts/jquery.validate.min.js",
					  "~/Scripts/jquery.validate.unobtrusive.js",
                "~/Scripts/jquery.unobtrusive-ajax.min.js"
            ));

            bundles.Add(new StyleBundle("~/bundles/TextEditor").Include(
            "~/Content/plugins/Font-Awesome/css/font-awesome.css",
            "~/Content/css/Markdown.Editor.hack.css",
            "~/Content/plugins/CLEditor1_4_3/jquery.cleditor.css",
            "~/Content/css/jquery.cleditor-hack.css",
            "~/Content/css/bootstrap-wysihtml5-hack.css"
            ));

            bundles.Add(new ScriptBundle("~/bundles/TextEditorJS").Include(
                "~/Content/plugins/wysihtml5/lib/js/wysihtml5-0.3.0.js",
             "~/Content/plugins/bootstrap-wysihtml5-hack.js",
            "~/Content/plugins/CLEditor1_4_3/jquery.cleditor.min.js",
            "~/Content/plugins/pagedown/Markdown.Converter.js",
            "~/Content/plugins/pagedown/Markdown.Sanitizer.js",
            "~/Content/plugins/Markdown.Editor-hack.js",
			"~/Content/plugins/sceditor/jquery.sceditor.js",
            "~/Content/js/editorInit.js"
             ));


            bundles.Add(new StyleBundle("~/Content/LandingCSS").Include(
		   "~/Content/LandigPage/css/bootstrap.min.css",
           "~/Content/LandigPage/font-awesome/css/font-awesome.min.css",
           "~/Content/LandigPage/plugins/cubeportfolio/css/cubeportfolio.min.css",
           "~/Content/LandigPage/css/nivo-lightbox.css",
           "~/Content/LandigPage/css/nivo-lightbox-theme/default/default.css",
           "~/Content/LandigPage/css/owl.carousel.css",
           "~/Content/LandigPage/css/owl.theme.css",
           "~/Content/LandigPage/css/animate.css",
           "~/Content/LandigPage/css/style.css"
           ));

            bundles.Add(new ScriptBundle("~/bundles/LandingJS").Include(
                "~/Content/LandigPage/js/jquery.min.js",
              "~/Content/LandigPage/js/bootstrap.min.js",
                 "~/Content/LandigPage/js/jquery.easing.min.js",
                    "~/Content/LandigPage/js/wow.min.js",
                       "~/Content/LandigPage/js/jquery.scrollTo.js",
                          "~/Content/LandigPage/js/jquery.appear.js",
                             "~/Content/LandigPage/js/stellar.js",
                             "~/Content/LandigPage/plugins/cubeportfolio/js/jquery.cubeportfolio.min.js",
                             "~/Content/LandigPage/js/owl.carousel.min.js",
                             "~/Content/LandigPage/js/nivo-lightbox.min.js",
                   "~/Content/LandigPage/js/custom.js"

             ));

            bundles.Add(new StyleBundle("~/Content/PublicCSS").Include(
           "~/Content/PublicPage/css/font-awesome.min.css",
           "~/Content/PublicPage/css/bootstrap.min.css",
           "~/Content/PublicPage/css/style.css"
           ));

            bundles.Add(new ScriptBundle("~/bundles/PublicJS").Include(
                 "~/Content/PublicPage/js/jquery-2.1.1.js",
                "~/Content/PublicPage/js/modernizr.js",  
                "~/Content/PublicPage/js/smoothscroll.js",
                "~/Content/PublicPage/js/bootstrap.min.js",
                "~/Content/PublicPage/js/custom.js"
             ));


			bundles.Add(new ScriptBundle("~/bundles/ChosenJS").Include(
				"~/Content/plugins/chosen/chosen.jquery.js"
                 
			   ));
			//bundles.Add(new ScriptBundle("~/bundles/ChosenJS.1.9.1").Include(
			//	"~/Content/plugins/chosen/chosen.jquery.js",
			//	"~/Scripts/jquery-1.9.1.min.js" 
			//   ));
         
			bundles.Add(new StyleBundle("~/Content/ChosenCSS").Include(
						"~/Content/plugins/chosen/chosen.css"
						));
           


            bundles.Add(new StyleBundle("~/Content/HelpCSS").Include(
                        "~/Content/HelpPage/assets/bootstrap/css/bootstrap.min.css",
                        "~/Content/HelpPage/assets/animate/animate.css",
                        "~/Content/HelpPage/assets/animate/set.css",
                        "~/Content/HelpPage/assets/gallery/blueimp-gallery.min.css"

                        ));

            bundles.Add(new ScriptBundle("~/Bundles/HelpJS").Include(
              "~/Content/HelpPage/assets/jquery.js",
              "~/Content/HelpPage/assets/wow/wow.min.js",
                "~/Content/HelpPage/assets/bootstrap/js/bootstrap.js",
              "~/Content/HelpPage/assets/mobile/touchSwipe.min.js",
              "~/Content/HelpPage/assets/respond/respond.js",
              "~/Content/HelpPage/assets/gallery/jquery.blueimp-gallery.min.js",
              "~/Content/HelpPage/assets/script.js"
             ));

			//for fix http://stackoverflow.com/questions/11355935/mvc4-stylebundle-not-resolving-images'
			//http://stackoverflow.com/questions/19765238/cssrewriteurltransform-with-or-without-virtual-directory
			BundleTable.EnableOptimizations = false;
        }
    }
}
