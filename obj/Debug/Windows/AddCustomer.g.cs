﻿#pragma checksum "..\..\..\Windows\AddCustomer.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "0CD12AB6F8F370BCAAFA2C24FD62992CF78EF648FF7F358C59FB9BF19BDDA62A"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using Salon.Windows;
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


namespace Salon.Windows {
    
    
    /// <summary>
    /// AddCustomer
    /// </summary>
    public partial class AddCustomer : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 16 "..\..\..\Windows\AddCustomer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox FIOTextBox;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\Windows\AddCustomer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox PhoneTextBox;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\..\Windows\AddCustomer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox AddressTextBox;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\Windows\AddCustomer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox LoginTextBox;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\Windows\AddCustomer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox PasswordTextBox;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\Windows\AddCustomer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button AddButton;
        
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
            System.Uri resourceLocater = new System.Uri("/Salon;component/windows/addcustomer.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Windows\AddCustomer.xaml"
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
            this.FIOTextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 16 "..\..\..\Windows\AddCustomer.xaml"
            this.FIOTextBox.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.FIOTextBox_PreviewTextInput);
            
            #line default
            #line hidden
            return;
            case 2:
            this.PhoneTextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 18 "..\..\..\Windows\AddCustomer.xaml"
            this.PhoneTextBox.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.PhoneTextBox_PreviewTextInput);
            
            #line default
            #line hidden
            return;
            case 3:
            this.AddressTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.LoginTextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 22 "..\..\..\Windows\AddCustomer.xaml"
            this.LoginTextBox.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.LoginTextBox_PreviewTextInput);
            
            #line default
            #line hidden
            return;
            case 5:
            this.PasswordTextBox = ((System.Windows.Controls.PasswordBox)(target));
            
            #line 24 "..\..\..\Windows\AddCustomer.xaml"
            this.PasswordTextBox.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.PasswordTextBox_PreviewTextInput);
            
            #line default
            #line hidden
            return;
            case 6:
            this.AddButton = ((System.Windows.Controls.Button)(target));
            
            #line 25 "..\..\..\Windows\AddCustomer.xaml"
            this.AddButton.Click += new System.Windows.RoutedEventHandler(this.AddButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
