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

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkID=390556 上有介绍

namespace acgsky
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class PLPage : Page
    {
        public PLPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// 在此页将要在 Frame 中显示时进行调用。
        /// </summary>
        /// <param name="e">描述如何访问此页的事件数据。
        /// 此参数通常用于配置页。</param>
        /// 
        private bool ZHUANGTAI = false;
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string TEXT = e.Parameter.ToString();
            int lingshi2 = TEXT.IndexOf("<div class=\"Top\">");
            int lingshi3 = TEXT.LastIndexOf("<div class=\"Mid_4\">");
            TEXT = TEXT.Remove(lingshi2, lingshi3 - lingshi2);
            int lingshi22 = TEXT.LastIndexOf("<div class=\"Bot\">");
            int lingshi33 = TEXT.LastIndexOf("</body>");
            TEXT = TEXT.Remove(lingshi22, lingshi33 - lingshi22);

            we.NavigateToString(TEXT.Replace("<body>", "<body>\r\n<div class=\"Mid\">"));
        }

        private void we_NavigationStarting(WebView sender, WebViewNavigationStartingEventArgs args)
        {
            if (ZHUANGTAI == false)
            {
                ZHUANGTAI = true;

            }
            else
            {
                args.Cancel = true;
            }
        }
    }
}
