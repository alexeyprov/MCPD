using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Windows;
using System.Globalization;
using System.Threading;
using System.Reflection;
using Westwind.Wpf.Controls;

namespace WpfLocalizationResx
{


    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        /// <summary>
        /// Application level theme used
        /// </summary>
        public static string Theme { get; set; }


        public App()
        {
            //ResExtension.InitializeResExtension(null);
            InitializeComponent();

            // Explicitly initialize the resource manager
            LocalizationSettings.Initialize(this.GetType().Assembly, WpfLocalizationResx.Properties.Resources.ResourceManager);
            LocalizationSettings.AddAssembly("WpfLocalizationResx", this.GetType().Assembly);

            SetCulture(ConfigurationManager.AppSettings["Culture"]);
     
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
                Thread.CurrentThread.CurrentUICulture = ci;
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
