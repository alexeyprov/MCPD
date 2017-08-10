using System;
using System.Windows;
using Microsoft.Practices.Unity;

using Cuboid.Modules.BasicMvvm;
using Cuboid.Modules.MediaPlayer;
using Cuboid.Shell.Views;
using Prism.Unity;
using Prism.Modularity;

namespace Cuboid.Shell
{
    internal sealed class Bootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<ShellView>();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();

            Application.Current.MainWindow = (Window)Shell;
            Application.Current.MainWindow.Show();
        }

        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();

            AddModule<BasicMvvmModule>();
            AddModule<MediaPlayerModule>();
        }

        private void AddModule<T>()
        {
            Type type = typeof(T);
            ModuleCatalog.AddModule(new ModuleInfo(type.Name, type.AssemblyQualifiedName));
        }
    }
}
