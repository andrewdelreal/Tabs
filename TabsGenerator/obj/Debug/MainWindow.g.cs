﻿#pragma checksum "..\..\MainWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "8BCE45E0DC98A4AD9D33E96CEA748DD55780EBB498C7311C573E80D7FA6F74B6"
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
        
        
        #line 83 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Num_Measures_Text_Box;
        
        #line default
        #line hidden
        
        
        #line 99 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Note_Spacing_Text_Box;
        
        #line default
        #line hidden
        
        
        #line 145 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Highest_Fret_Text_Box;
        
        #line default
        #line hidden
        
        
        #line 161 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Lowest_Fret_Text_Box;
        
        #line default
        #line hidden
        
        
        #line 197 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox StringDropDown;
        
        #line default
        #line hidden
        
        
        #line 229 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton RandomNotesCheck;
        
        #line default
        #line hidden
        
        
        #line 234 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton RandomChordsCheck;
        
        #line default
        #line hidden
        
        
        #line 241 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button createButton;
        
        #line default
        #line hidden
        
        
        #line 265 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox ChordDropDown;
        
        #line default
        #line hidden
        
        
        #line 296 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Chord_Spacing_Text_Box;
        
        #line default
        #line hidden
        
        
        #line 333 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock SelectFileNameText;
        
        #line default
        #line hidden
        
        
        #line 350 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ManualCreateButton;
        
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
            
            #line 93 "..\..\MainWindow.xaml"
            this.Num_Measures_Text_Box.KeyDown += new System.Windows.Input.KeyEventHandler(this.Num_Measures_KeyDown);
            
            #line default
            #line hidden
            
            #line 94 "..\..\MainWindow.xaml"
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
            
            #line 155 "..\..\MainWindow.xaml"
            this.Highest_Fret_Text_Box.LostFocus += new System.Windows.RoutedEventHandler(this.Highest_Fret_Text_Box_LostFocus);
            
            #line default
            #line hidden
            
            #line 156 "..\..\MainWindow.xaml"
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
            this.StringDropDown = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 6:
            this.RandomNotesCheck = ((System.Windows.Controls.RadioButton)(target));
            return;
            case 7:
            this.RandomChordsCheck = ((System.Windows.Controls.RadioButton)(target));
            return;
            case 8:
            this.createButton = ((System.Windows.Controls.Button)(target));
            
            #line 246 "..\..\MainWindow.xaml"
            this.createButton.Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.ChordDropDown = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 10:
            this.Chord_Spacing_Text_Box = ((System.Windows.Controls.TextBox)(target));
            
            #line 306 "..\..\MainWindow.xaml"
            this.Chord_Spacing_Text_Box.KeyDown += new System.Windows.Input.KeyEventHandler(this.Chord_Spacing_Text_Box_KeyDown);
            
            #line default
            #line hidden
            
            #line 307 "..\..\MainWindow.xaml"
            this.Chord_Spacing_Text_Box.LostFocus += new System.Windows.RoutedEventHandler(this.Chord_Spacing_Text_Box_LostFocus);
            
            #line default
            #line hidden
            return;
            case 11:
            this.SelectFileNameText = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 12:
            
            #line 345 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click_1);
            
            #line default
            #line hidden
            return;
            case 13:
            this.ManualCreateButton = ((System.Windows.Controls.Button)(target));
            
            #line 355 "..\..\MainWindow.xaml"
            this.ManualCreateButton.Click += new System.Windows.RoutedEventHandler(this.ManualCreateButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

