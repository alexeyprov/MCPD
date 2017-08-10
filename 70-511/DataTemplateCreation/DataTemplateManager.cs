using System;
using System.Windows;
using System.Windows.Markup;

namespace DataTemplateCreation
{
    internal sealed class DataTemplateManager
    {
        public void RegisterDataTemplate<TViewModel, TView>() where TView: FrameworkElement
        {
            RegisterDataTemplate(typeof(TViewModel), typeof(TView), false);
        }

        public void RegisterObsoleteDataTemplate<TViewModel, TView>()
        {
            RegisterDataTemplate(typeof(TViewModel), typeof(TView), true);
        }

        public void RegisterDataTemplate(Type viewModelType, Type viewType, bool useObsoleteMethod)
        {
            DataTemplate template = useObsoleteMethod ? 
                CreateTemplateObsolete(viewModelType, viewType) : 
                CreateTemplate(viewModelType, viewType);

            object key = template.DataTemplateKey;
            Application.Current.Resources.Add(key, template);
        }

        private DataTemplate CreateTemplateObsolete(Type viewModelType, Type viewType)
        {
            var template = new DataTemplate()
            {
                DataType = viewModelType,
                VisualTree = new FrameworkElementFactory(viewType)
            };

            return template;
        }

        private DataTemplate CreateTemplate(Type viewModelType, Type viewType)
        {
            string xaml = $"<DataTemplate DataType=\"{{x:Type vm:{viewModelType.Name}}}\"><v:{viewType.Name} /></DataTemplate>";

            var context = new ParserContext();

            context.XamlTypeMapper = new XamlTypeMapper(new string[0]);
            context.XamlTypeMapper.AddMappingProcessingInstruction("vm", viewModelType.Namespace, viewModelType.Assembly.FullName);
            context.XamlTypeMapper.AddMappingProcessingInstruction("v", viewType.Namespace, viewType.Assembly.FullName);

            context.XmlnsDictionary.Add("", "http://schemas.microsoft.com/winfx/2006/xaml/presentation");
            context.XmlnsDictionary.Add("x", "http://schemas.microsoft.com/winfx/2006/xaml");
            context.XmlnsDictionary.Add("vm", "vm");
            context.XmlnsDictionary.Add("v", "v");

            return (DataTemplate)XamlReader.Parse(xaml, context);
        }
    }
}
