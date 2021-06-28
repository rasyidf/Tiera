using System;

using Tiera.Core.Models;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Tiera.Views
{
    public sealed partial class VetsDetailControl : UserControl
    {
        public SampleOrder ListMenuItem
        {
            get { return GetValue(ListMenuItemProperty) as SampleOrder; }
            set { SetValue(ListMenuItemProperty, value); }
        }

        public static readonly DependencyProperty ListMenuItemProperty = DependencyProperty.Register("ListMenuItem", typeof(SampleOrder), typeof(VetsDetailControl), new PropertyMetadata(null, OnListMenuItemPropertyChanged));

        public VetsDetailControl()
        {
            InitializeComponent();
        }

        private static void OnListMenuItemPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as VetsDetailControl;
            control.ForegroundElement.ChangeView(0, 0, 1);
        }
    }
}
