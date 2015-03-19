﻿#pragma checksum "..\..\..\..\Gui\DisplayControl.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "CF9FD8FF91CC7637325E053F8D258AC1"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using PRAM_Machine.Gui;
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


namespace PRAM_Machine.Gui {
    
    
    /// <summary>
    /// DisplayControl
    /// </summary>
    public partial class DisplayControl : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 12 "..\..\..\..\Gui\DisplayControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Canvas paintingArea;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\..\..\Gui\DisplayControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Rectangle processorsFrame;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\..\..\Gui\DisplayControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock processorsLabel;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\..\Gui\DisplayControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid processorsGrid;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\..\Gui\DisplayControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Rectangle memoryFrame;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\..\Gui\DisplayControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock memoryLabel;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\..\Gui\DisplayControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid memoryGrid;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\..\..\Gui\DisplayControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button nextButton;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\..\..\Gui\DisplayControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button nextTickButton;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\..\..\Gui\DisplayControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button runButton;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\..\..\Gui\DisplayControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock tickCountTextBlock;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\..\..\Gui\DisplayControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock machineStateTextBlock;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\..\..\Gui\DisplayControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Canvas arrowPaintingArea;
        
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
            System.Uri resourceLocater = new System.Uri("/PRAM Machine;component/gui/displaycontrol.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Gui\DisplayControl.xaml"
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
            this.paintingArea = ((System.Windows.Controls.Canvas)(target));
            return;
            case 2:
            this.processorsFrame = ((System.Windows.Shapes.Rectangle)(target));
            return;
            case 3:
            this.processorsLabel = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 4:
            this.processorsGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 5:
            this.memoryFrame = ((System.Windows.Shapes.Rectangle)(target));
            return;
            case 6:
            this.memoryLabel = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 7:
            this.memoryGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 8:
            this.nextButton = ((System.Windows.Controls.Button)(target));
            
            #line 38 "..\..\..\..\Gui\DisplayControl.xaml"
            this.nextButton.Click += new System.Windows.RoutedEventHandler(this.nextButton_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.nextTickButton = ((System.Windows.Controls.Button)(target));
            
            #line 40 "..\..\..\..\Gui\DisplayControl.xaml"
            this.nextTickButton.Click += new System.Windows.RoutedEventHandler(this.nextTickButton_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.runButton = ((System.Windows.Controls.Button)(target));
            
            #line 42 "..\..\..\..\Gui\DisplayControl.xaml"
            this.runButton.Click += new System.Windows.RoutedEventHandler(this.runButton_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            this.tickCountTextBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 12:
            this.machineStateTextBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 13:
            this.arrowPaintingArea = ((System.Windows.Controls.Canvas)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

