﻿#pragma checksum "..\..\..\Controls\ChartControl.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "1F0F6A4213D9B53C7916BD485F9CD232"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Demo.Controls;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
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


namespace Demo.Controls {
    
    
    /// <summary>
    /// ChartControl
    /// </summary>
    public partial class ChartControl : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 19 "..\..\..\Controls\ChartControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Line ordinata;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\..\Controls\ChartControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Line abscissa;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\Controls\ChartControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock yy;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\Controls\ChartControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock xx;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\Controls\ChartControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image topArrow;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\Controls\ChartControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image leftArrow;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\Controls\ChartControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Canvas content;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\..\Controls\ChartControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Slider colorX;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\..\Controls\ChartControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Slider colorY;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\Controls\ChartControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Slider colorZ;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\..\Controls\ChartControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Slider tchcknes;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\..\Controls\ChartControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Slider txtSize;
        
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
            System.Uri resourceLocater = new System.Uri("/Demo;component/controls/chartcontrol.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Controls\ChartControl.xaml"
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
            case 1:
            
            #line 9 "..\..\..\Controls\ChartControl.xaml"
            ((System.Windows.Controls.Grid)(target)).SizeChanged += new System.Windows.SizeChangedEventHandler(this.Grid_SizeChanged);
            
            #line default
            #line hidden
            
            #line 9 "..\..\..\Controls\ChartControl.xaml"
            ((System.Windows.Controls.Grid)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Grid_MouseDown);
            
            #line default
            #line hidden
            return;
            case 2:
            this.ordinata = ((System.Windows.Shapes.Line)(target));
            return;
            case 3:
            this.abscissa = ((System.Windows.Shapes.Line)(target));
            return;
            case 4:
            this.yy = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 5:
            this.xx = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 6:
            this.topArrow = ((System.Windows.Controls.Image)(target));
            return;
            case 7:
            this.leftArrow = ((System.Windows.Controls.Image)(target));
            return;
            case 8:
            this.content = ((System.Windows.Controls.Canvas)(target));
            return;
            case 9:
            this.colorX = ((System.Windows.Controls.Slider)(target));
            
            #line 34 "..\..\..\Controls\ChartControl.xaml"
            this.colorX.ValueChanged += new System.Windows.RoutedPropertyChangedEventHandler<double>(this.colorX_ValueChanged);
            
            #line default
            #line hidden
            return;
            case 10:
            this.colorY = ((System.Windows.Controls.Slider)(target));
            
            #line 35 "..\..\..\Controls\ChartControl.xaml"
            this.colorY.ValueChanged += new System.Windows.RoutedPropertyChangedEventHandler<double>(this.colorX_ValueChanged);
            
            #line default
            #line hidden
            return;
            case 11:
            this.colorZ = ((System.Windows.Controls.Slider)(target));
            
            #line 36 "..\..\..\Controls\ChartControl.xaml"
            this.colorZ.ValueChanged += new System.Windows.RoutedPropertyChangedEventHandler<double>(this.colorX_ValueChanged);
            
            #line default
            #line hidden
            return;
            case 12:
            this.tchcknes = ((System.Windows.Controls.Slider)(target));
            
            #line 40 "..\..\..\Controls\ChartControl.xaml"
            this.tchcknes.ValueChanged += new System.Windows.RoutedPropertyChangedEventHandler<double>(this.colorX_ValueChanged);
            
            #line default
            #line hidden
            return;
            case 13:
            this.txtSize = ((System.Windows.Controls.Slider)(target));
            
            #line 42 "..\..\..\Controls\ChartControl.xaml"
            this.txtSize.ValueChanged += new System.Windows.RoutedPropertyChangedEventHandler<double>(this.colorX_ValueChanged);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

