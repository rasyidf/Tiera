using System;

using Tiera.Core.Models;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Tiera.Views
{
    public sealed partial class ServicesDetailControl : UserControl
    {
        public SampleOrder ListMenuItem
        {
            get { return GetValue(ListMenuItemProperty) as SampleOrder; }
            set { SetValue(ListMenuItemProperty, value); }
        }

        public static readonly DependencyProperty ListMenuItemProperty = DependencyProperty.Register("ListMenuItem", typeof(SampleOrder), typeof(ServicesDetailControl), new PropertyMetadata(null, OnListMenuItemPropertyChanged));

        public ServicesDetailControl()
        {
            InitializeComponent();
        }

        private static void OnListMenuItemPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as ServicesDetailControl;
            control.ForegroundElement.ChangeView(0, 0, 1);
        }
    }
}
