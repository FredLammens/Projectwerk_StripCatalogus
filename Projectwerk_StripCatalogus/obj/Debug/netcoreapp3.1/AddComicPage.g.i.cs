﻿#pragma checksum "..\..\..\AddComicPage.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6E3FD8990EEE677B37975BB76CCAA7F25DFC7EC2"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Projectwerk_StripCatalogus_UI;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
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
using ViewModel;


namespace Projectwerk_StripCatalogus_UI {
    
    
    /// <summary>
    /// AddComicPage
    /// </summary>
    public partial class AddComicPage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 31 "..\..\..\AddComicPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtTitle;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\AddComicPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtSeries;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\..\AddComicPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtSeriesNumber;
        
        #line default
        #line hidden
        
        
        #line 56 "..\..\..\AddComicPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtAuthorFilter;
        
        #line default
        #line hidden
        
        
        #line 58 "..\..\..\AddComicPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid PossibleAuthorsList;
        
        #line default
        #line hidden
        
        
        #line 70 "..\..\..\AddComicPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid SelectedAuthorsList;
        
        #line default
        #line hidden
        
        
        #line 85 "..\..\..\AddComicPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cmbPublisher;
        
        #line default
        #line hidden
        
        
        #line 89 "..\..\..\AddComicPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnAddComic;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Projectwerk_StripCatalogus_UI;V1.0.0.0;component/addcomicpage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\AddComicPage.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.txtTitle = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.txtSeries = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.txtSeriesNumber = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.txtAuthorFilter = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.PossibleAuthorsList = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 6:
            this.SelectedAuthorsList = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 7:
            this.cmbPublisher = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 8:
            this.btnAddComic = ((System.Windows.Controls.Button)(target));
            
            #line 89 "..\..\..\AddComicPage.xaml"
            this.btnAddComic.Click += new System.Windows.RoutedEventHandler(this.btnAddComic_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

