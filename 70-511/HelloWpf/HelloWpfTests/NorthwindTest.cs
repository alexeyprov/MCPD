using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Threading;
using HelloWpf.Northwind;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HelloWpfTests
{
    [TestClass]
    public class NorthwindTest
    {
        [TestMethod]
        public void TestEmployees()
        {
            HelloWpf.App app = new HelloWpf.App();
            app.InitializeComponent();

            app.Startup += (s, e) =>
            {
                EmployeeListWindow window = new EmployeeListWindow();

                //It is required that a window is available on UI before calling GetChildren on it
                window.Show();

                WindowAutomationPeer windowPeer = new WindowAutomationPeer(window);

                // Find and select second list item
                ListBoxAutomationPeer listBoxPeer = FindSingleChild<ListBoxAutomationPeer>(windowPeer);
                ListBoxItemAutomationPeer listItemPeer = (ListBoxItemAutomationPeer)
                    listBoxPeer.GetChildren()[1];

                ISelectionItemProvider itemSelectionProvider = (ISelectionItemProvider)listItemPeer.GetPattern(PatternInterface.SelectionItem);
                itemSelectionProvider.Select();

                // Find last name text box
                UserControlAutomationPeer userControlPeer = FindSingleChild<UserControlAutomationPeer>(windowPeer);
                TextBoxAutomationPeer textBoxPeer = FindSingleChild<TextBoxAutomationPeer>(
                    userControlPeer,
                    p => ((FrameworkElement)p.Owner).Name == "LastNameField");

                // Check its content
                IValueProvider valueProvider = (IValueProvider)textBoxPeer.GetPattern(PatternInterface.Value);

                window.Dispatcher.BeginInvoke(
                    (ThreadStart) delegate()
                    {
                        Assert.AreEqual("Fuller", valueProvider.Value);
                        app.Shutdown();
                    },
                    DispatcherPriority.Background);
            };

            app.Run();
        }

        private static T FindSingleChild<T>(AutomationPeer parent, Func<T, bool> filter = null)
        {
            filter = filter ?? (p => true);

            return parent
                .GetChildren()
                .OfType<T>()
                .Where(filter)
                .Single();
        }
    }
}
