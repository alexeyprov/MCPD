using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Xml;

namespace HelloWpf.Controls
{
    /// <summary>
    /// Interaction logic for TemplateBrowserWindow.xaml
    /// </summary>
    public partial class TemplateBrowserWindow : Window
    {
        public TemplateBrowserWindow()
        {
            InitializeComponent();

            LoadControlList();
        }

        private void LoadControlList()
        {
            foreach (Type t in Assembly.GetAssembly(typeof(Control)).GetTypes().Concat(new[] { typeof(CustomUI.ColorPicker) })
                .Where(t => typeof(Control).IsAssignableFrom(t))
                .OrderBy(t => t.FullName))
            {
                ControlsList.Items.Add(t);
            }
        }

        private void ControlsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Type t = (Type)ControlsList.SelectedItem;

            Control c = (Control)Activator.CreateInstance(t);
            c.Visibility = Visibility.Hidden;
            Window w = c as Window;

            //HACK: Add control to the tree to initialize its template
            if (w == null)
            {
                LayoutGrid.Children.Add(c);
            }
            else
            {
                w.Width = 0;
                w.Height = 0;
                w.Left = -10000;
                w.Top = -10000;

                w.Show();
            }

            using (TextWriter innerWriter = new StringWriter())
            {
                using (XmlWriter outerWriter = XmlWriter.Create(
                    innerWriter,
                    new XmlWriterSettings()
                    {
                        Indent = true
                    }))
                {
                    XamlWriter.Save(c.Template, outerWriter);
                }

                XamlTextBox.Text = innerWriter.ToString();
            }

            if (w == null)
            {
                LayoutGrid.Children.Remove(c);
            }
            else
            {
                w.Close();
            }
        }
    }
}
