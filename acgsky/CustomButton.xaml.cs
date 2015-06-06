using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// “用户控件”项模板在 http://go.microsoft.com/fwlink/?LinkId=234236 上提供

namespace acgsky
{
    public sealed partial class CustomButton : Button
    {
        // 依赖属性  
        public static readonly DependencyProperty ImageProperty;
        public CustomButton()
        {
            this.InitializeComponent();
        }
        static CustomButton()
        {
            CustomButton.ImageProperty = DependencyProperty.Register("Image", typeof(ImageSource), typeof(CustomButton), new PropertyMetadata(false, new PropertyChangedCallback(OnIsDefaultChanged)));
        }

        // .net属性包装器（可选）  
        public ImageSource Image
        {

            get { return (ImageSource)GetValue(CustomButton.ImageProperty); }
            set { SetValue(CustomButton.ImageProperty, value); }
        }

        // 属性改变的回调（可选）  
        private static void OnIsDefaultChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {

        }  
    }
}
