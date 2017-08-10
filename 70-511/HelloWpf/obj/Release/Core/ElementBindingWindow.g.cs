﻿#pragma checksum "..\..\..\Core\ElementBindingWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "B46656E21CB03D7743C4A7AEA1F9159E"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace HelloWpf.Core {
    
    
    /// <summary>
    /// ElementBindingWindow
    /// </summary>
    public partial class ElementBindingWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 52 "..\..\..\Core\ElementBindingWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Slider FontSlider;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\..\Core\ElementBindingWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock SampleTextBlock;
        
        #line default
        #line hidden
        
        
        #line 55 "..\..\..\Core\ElementBindingWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SmallSizeButton;
        
        #line default
        #line hidden
        
        
        #line 56 "..\..\..\Core\ElementBindingWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button LargeSizeButton;
        
        #line default
        #line hidden
        
        
        #line 57 "..\..\..\Core\ElementBindingWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button MediumSizeButton;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/HelloWpf;component/core/elementbindingwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Core\ElementBindingWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 2:
            this.FontSlider = ((System.Windows.Controls.Slider)(target));
            return;
            case 3:
            this.SampleTextBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 4:
            this.SmallSizeButton = ((System.Windows.Controls.Button)(target));
            
            #line 55 "..\..\..\Core\ElementBindingWindow.xaml"
            this.SmallSizeButton.Click += new System.Windows.RoutedEventHandler(this.SmallSizeButton_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.LargeSizeButton = ((System.Windows.Controls.Button)(target));
            
            #line 56 "..\..\..\Core\ElementBindingWindow.xaml"
            this.LargeSizeButton.Click += new System.Windows.RoutedEventHandler(this.LargeSizeButton_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.MediumSizeButton = ((System.Windows.Controls.Button)(target));
            
            #line 57 "..\..\..\Core\ElementBindingWindow.xaml"
            this.MediumSizeButton.Click += new System.Windows.RoutedEventHandler(this.MediumSizeButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        void System.Windows.Markup.IStyleConnector.Connect(int connectionId, object target) {
            System.Windows.EventSetter eventSetter;
            switch (connectionId)
            {
            case 1:
            eventSetter = new System.Windows.EventSetter();
            eventSetter.Event = System.Windows.UIElement.MouseEnterEvent;
            
            #line 8 "..\..\..\Core\ElementBindingWindow.xaml"
            eventSetter.Handler = new System.Windows.Input.MouseEventHandler(this.Element_MouseEnter);
            
            #line default
            #line hidden
            ((System.Windows.Style)(target)).Setters.Add(eventSetter);
            eventSetter = new System.Windows.EventSetter();
            eventSetter.Event = System.Windows.UIElement.MouseLeaveEvent;
            
            #line 9 "..\..\..\Core\ElementBindingWindow.xaml"
            eventSetter.Handler = new System.Windows.Input.MouseEventHandler(this.Element_MouseLeave);
            
            #line default
            #line hidden
            ((System.Windows.Style)(target)).Setters.Add(eventSetter);
            break;
            }
        }
    }
}
