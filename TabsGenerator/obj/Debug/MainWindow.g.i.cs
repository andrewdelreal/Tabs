﻿#pragma checksum "..\..\MainWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "DD5AA4F30E1C55CF698DD9883645063053FA4E2B646FB046842E31824B6F8D65"
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
using TabsGenerator;


namespace TabsGenerator {
    
    
    /// <summary>
    /// MainWindow
    /// </summary>
    public partial class MainWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 82 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Num_Measures_Text_Box;
        
        #line default
        #line hidden
        
        
        #line 99 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Note_Spacing_Text_Box;
        
        #line default
        #line hidden
        
        
        #line 146 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Highest_Fret_Text_Box;
        
        #line default
        #line hidden
        
        
        #line 161 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Lowest_Fret_Text_Box;
        
        #line default
        #line hidden
        
        
        #line 185 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox randomCheck;
        
        #line default
        #line hidden
        
        
        #line 215 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button createButton;
        
        #line default
        #line hidden
        
        
        #line 238 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox ChordDropDown;
        
        #line default
        #line hidden
        
        
        #line 269 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Chord_Spacing_Text_Box;
        
        #line default
        #line hidden
        
        
        #line 294 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox chordCheck;
        
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
            System.Uri resourceLocater = new System.Uri("/TabsGenerator;component/mainwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\MainWindow.xaml"
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
            this.Num_Measures_Text_Box = ((System.Windows.Controls.TextBox)(target));
            
            #line 92 "..\..\MainWindow.xaml"
            this.Num_Measures_Text_Box.KeyDown += new System.Windows.Input.KeyEventHandler(this.Num_Measures_KeyDown);
            
            #line default
            #line hidden
            
            #line 93 "..\..\MainWindow.xaml"
            this.Num_Measures_Text_Box.LostFocus += new System.Windows.RoutedEventHandler(this.Num_Measures_LostFocus);
            
            #line default
            #line hidden
            return;
            case 2:
            this.Note_Spacing_Text_Box = ((System.Windows.Controls.TextBox)(target));
            
            #line 109 "..\..\MainWindow.xaml"
            this.Note_Spacing_Text_Box.KeyDown += new System.Windows.Input.KeyEventHandler(this.Note_Spacing_Text_Box_KeyDown);
            
            #line default
            #line hidden
            
            #line 110 "..\..\MainWindow.xaml"
            this.Note_Spacing_Text_Box.LostFocus += new System.Windows.RoutedEventHandler(this.Note_Spacing_Text_Box_LostFocus);
            
            #line default
            #line hidden
            return;
            case 3:
            this.Highest_Fret_Text_Box = ((System.Windows.Controls.TextBox)(target));
            
            #line 156 "..\..\MainWindow.xaml"
            this.Highest_Fret_Text_Box.LostFocus += new System.Windows.RoutedEventHandler(this.Highest_Fret_Text_Box_LostFocus);
            
            #line default
            #line hidden
            
            #line 157 "..\..\MainWindow.xaml"
            this.Highest_Fret_Text_Box.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.Highest_Fret_Text_Box_PreviewTextInput);
            
            #line default
            #line hidden
            return;
            case 4:
            this.Lowest_Fret_Text_Box = ((System.Windows.Controls.TextBox)(target));
            
            #line 171 "..\..\MainWindow.xaml"
            this.Lowest_Fret_Text_Box.LostFocus += new System.Windows.RoutedEventHandler(this.Lowest_Fret_Text_Box_LostFocus);
            
            #line default
            #line hidden
            
            #line 172 "..\..\MainWindow.xaml"
            this.Lowest_Fret_Text_Box.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.Lowest_Fret_Text_Box_PreviewTextInput);
            
            #line default
            #line hidden
            return;
            case 5:
            this.randomCheck = ((System.Windows.Controls.CheckBox)(target));
            return;
            case 6:
            this.createButton = ((System.Windows.Controls.Button)(target));
            
            #line 220 "..\..\MainWindow.xaml"
            this.createButton.Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.ChordDropDown = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 8:
            this.Chord_Spacing_Text_Box = ((System.Windows.Controls.TextBox)(target));
            
            #line 279 "..\..\MainWindow.xaml"
            this.Chord_Spacing_Text_Box.KeyDown += new System.Windows.Input.KeyEventHandler(this.Chord_Spacing_Text_Box_KeyDown);
            
            #line default
            #line hidden
            
            #line 280 "..\..\MainWindow.xaml"
            this.Chord_Spacing_Text_Box.LostFocus += new System.Windows.RoutedEventHandler(this.Chord_Spacing_Text_Box_LostFocus);
            
            #line default
            #line hidden
            return;
            case 9:
            this.chordCheck = ((System.Windows.Controls.CheckBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

