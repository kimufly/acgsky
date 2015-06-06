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
using Windows.Web.Http;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkID=390556 上有介绍

namespace acgsky
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class BlankPage : Page
    {
        
        public BlankPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// 在此页将要在 Frame 中显示时进行调用。
        /// </summary>
        /// <param name="e">描述如何访问此页的事件数据。
        /// 此参数通常用于配置页。</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            tbtb.Text = e.Parameter.ToString();
            //int lingshi2 = tbtb.Text.IndexOf("<div class=\"Top\">");
            //int lingshi3 = tbtb.Text.LastIndexOf("<div class=\"Mid_4\">");
            //tbtb.Text = tbtb.Text.Remove(lingshi2, lingshi3 - lingshi2);
            //int lingshi22 = tbtb.Text.LastIndexOf("<div class=\"Bot\">");
            //int lingshi33 = tbtb.Text.LastIndexOf("</body>");
            //tbtb.Text = tbtb.Text.Remove(lingshi22, lingshi33 - lingshi22);
        }
   

    }
}
