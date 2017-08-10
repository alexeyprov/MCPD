// © 2009 Rick Strahl. All rights reserved. 
// See http://wpflocalization.codeplex.com for related whitepaper and updates
// See http://wpfclientguidance.codeplex.com for other WPF resources

using System;
using System.Configuration;
using System.Windows;
using System.Globalization;
using System.Threading;
using System.Resources;
using WpfLocalizationLocBaml.Properties;

using System.Reflection;
using System.Windows.Markup;
using System.Drawing;

namespace WpfLocalizationLocBaml
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Create a static resource manager so you have global
        /// access to global application resources
        /// </summary>
        public static ResourceManager ResGlobal = new ResourceManager("WpfLocalizationLocBaml.Properties.Resources", typeof(App).Assembly);

        public static Resources ResourcesStrong  { get; set;}

        public static string Theme = "Luna";
        
        public App()
        {
            ResourcesStrong = new Resources();

            string HelloWorld = ResGlobal.GetString("HelloWorld");
            Bitmap img = ResGlobal.GetObject("AuthorPhoto") as Bitmap;

        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Set application startup culture based on config settings
            string culture = ConfigurationManager.AppSettings["Culture"];
            SetCulture(culture);

            Theme = ConfigurationManager.AppSettings["Theme"];
            SetTheme(Theme);

            FrameworkElement.LanguageProperty.OverrideMetadata(typeof(FrameworkElement),
            new FrameworkPropertyMetadata(XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));            

        }

        /// <summary>
        /// Sets the culture to the specified culture id
        /// </summary>
        /// <param name="culture"></param>
        public static void SetCultureSimple(string culture)
        {
            if (!string.IsNullOrEmpty(culture))
            {
                CultureInfo ci = new CultureInfo(culture);
                Thread.CurrentThread.CurrentCulture = ci;
                Thread.CurrentThread.CurrentUICulture = ci;
            }
            
        }


        /// <summary>
        /// Sets the culture to the specified culture id
        /// </summary>
        /// <param name="culture"></param>        
        public static void SetCulture(string culture)
        {
            SetCulture(culture, false);
        }

        /// <summary>
        /// Sets the culture to the specified culture id
        /// </summary>
        /// <param name="culture"></param>
        /// <param name="closeAllWindowsReloadMain">If set closes all windows and reloads the main window</param>
        public static void SetCulture(string culture, bool closeAllWindowsReloadMain)
        {
            if (culture == null)
                return;

            bool cultureChanged = culture != Thread.CurrentThread.CurrentUICulture.IetfLanguageTag;
            if (!cultureChanged)
                return;

            if (!string.IsNullOrEmpty(culture))
            {
                CultureInfo ci = new CultureInfo(culture);
                Thread.CurrentThread.CurrentCulture = ci;
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);
            }
   

            if (closeAllWindowsReloadMain)
            {
                Window oldForm = App.Current.Windows[0];
                Type mainWinType = App.Current.Windows[0].GetType();
                Window mainForm = Assembly.GetExecutingAssembly().CreateInstance(mainWinType.FullName) as Window;
                mainForm.Top = oldForm.Top;
                mainForm.Left = oldForm.Left;
                mainForm.Show();

                // close all windows and reload the startup form
                foreach (Window win in App.Current.Windows)
                {
                    if (mainForm == win) // skip our newly loaded form
                        continue;

                    win.Close();
                }
            }
        }




        /// <summary>
        /// Sets or resets the theme in the current application
        /// </summary>
        /// <param name="theme"></param>
        public static void SetTheme(string theme)
        {
            if (string.IsNullOrEmpty(theme))
                return;


            
            // Remove existing theme
            foreach (ResourceDictionary dict in App.Current.Resources.MergedDictionaries)
            {
                if (dict.Source != null && dict.Source.ToString().Contains("Themes/"))
                {
                    App.Current.Resources.MergedDictionaries.Remove(dict);
                    break;
                }
            }

            if (theme == "Luna")
            {
                Uri uri = new Uri("PresentationFramework.Luna;V3.0.0.0;31bf3856ad364e35;component\\themes/luna.normalcolor.xaml", UriKind.Relative);
                App.Current.Resources.MergedDictionaries.Add(Application.LoadComponent(uri) as ResourceDictionary);
            }
            else
            {


                ResourceDictionary dictionary = new ResourceDictionary();
                dictionary.Source = new Uri(string.Format("Themes/{0}.xaml", theme),
                                      UriKind.Relative);
                App.Current.Resources.MergedDictionaries.Add(dictionary);
            }


            App.Theme = theme;
        }

    }
}
