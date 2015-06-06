using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.Web.Http;
using Windows.Web.Http.Filters;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkID=390556 上有介绍

namespace acgsky
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class DMYYPage : Page
    {
        private HttpBaseProtocolFilter basefilter;
        private plugInFilter myfilter;
        private HttpClient webClient;
        private string Text0 = "";
        private bool ST0 = false;
        //private bool SST0 = false;
        private string Uri0 = @"http://acg.gamersky.com/music/";
        private Button B0;
        private ObservableCollection<Item> Items0 = new ObservableCollection<Item>();
        public DMYYPage()
        {
            this.InitializeComponent();
            ListView0.ItemsSource = Items0;
            this.NavigationCacheMode = NavigationCacheMode.Required;
            //if (App.yjmsbl == true)
            //{
            //    YJMS.Visibility = Visibility.Visible;
            //}
        }
        public class Item
        {
            public String First { get; set; }
            public String Last { get; set; }
            public ImageSource Img { get; set; }
            public String Link { get; set; }
        }
        /// <summary>
        /// 在此页将要在 Frame 中显示时进行调用。
        /// </summary>
        /// <param name="e">描述如何访问此页的事件数据。
        /// 此参数通常用于配置页。</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Windows.Phone.UI.Input.HardwareButtons.BackPressed += HardwareButtons_BackPressed;
            if (App.yjmsbl == true)
            {
                YJMS.Visibility = Visibility.Visible;
            }
            else
            {
                YJMS.Visibility = Visibility.Collapsed;
            }
        }
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            Windows.Phone.UI.Input.HardwareButtons.BackPressed -= HardwareButtons_BackPressed;
        }



        void HardwareButtons_BackPressed(object sender, Windows.Phone.UI.Input.BackPressedEventArgs e)
        {
            e.Handled = true;
                if (Frame.CanGoBack)
                {
                    Frame.GoBack();
                }
        }

        private async void Pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ST0 == false)
            {
                try
                {
                    basefilter = new HttpBaseProtocolFilter();
                    myfilter = new plugInFilter(basefilter);
                    webClient = new HttpClient(myfilter);
                    Text0 = await webClient.GetStringAsync(new Uri(@"http://acg.gamersky.com/music/"));
                    加载(Text0.Substring(Text0.IndexOf("<ul class=\"Ptxt block\">"), Text0.IndexOf("<div class=\"Page\">") - Text0.IndexOf("<ul class=\"Ptxt block\">")));
                    if (Text0.Contains("下一页</a></span>"))
                    {
                        Match M2 = new Regex("(?<=(" + "class=\"p2\" href=\"" + "))[.\\s\\S]*?(?=(" + "\">下一页" + "))", RegexOptions.Multiline | RegexOptions.Singleline).Match(Text0.Substring(Text0.LastIndexOf("\">下一页") - 80, 100));
                        Uri0 = M2.Value;
                    }
                    ST0 = true;
                    //iscw = false;
                    if (B0 != null)
                    {
                        B0.Content = "加载更多";
                    }
                }
                catch
                {
                    if (B0 != null)
                    {
                        B0.Content = "加载失败";
                    }
                    new MessageDialog("网络异常,数据加载失败，请重试或实在不行请联系作者！", "错误提示").ShowAsync();
                }
            }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if (ST0 == true )
            {
                try
                {
                    ((Button)sender).Content = "加载中";
                    basefilter = new HttpBaseProtocolFilter();
                    myfilter = new plugInFilter(basefilter);
                    webClient = new HttpClient(myfilter);

                    Text0 = await webClient.GetStringAsync(new Uri(Uri0));
                    加载(Text0.Substring(Text0.IndexOf("<ul class=\"Ptxt block\">"), Text0.IndexOf("<div class=\"Page\">") - Text0.IndexOf("<ul class=\"Ptxt block\">")));
                    if (Text0.Contains("下一页</a></span>"))
                    {
                        ((Button)sender).IsEnabled = true;
                        Match M2 = new Regex("(?<=(" + "class=\"p2\" href=\"" + "))[.\\s\\S]*?(?=(" + "\">下一页" + "))", RegexOptions.Multiline | RegexOptions.Singleline).Match(Text0.Substring(Text0.LastIndexOf("\">下一页") - 80, 100));
                        Uri0 = M2.Value;
                        B0.IsEnabled = true;
                        ((Button)sender).Content = "加载更多";
                    }
                    else
                    {
                        ((Button)sender).IsEnabled = false;
                        ((Button)sender).Content = "到底了";
                    }
                }
                catch
                {
                    ((Button)sender).Content = "加载失败";
                    new MessageDialog("网络异常,数据加载失败，请重试或实在不行请联系作者！", "错误提示").ShowAsync();
                }
            }

        }

        private async void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            Item wo = e.ClickedItem as Item;
            

            if (wo.Link.Length > 10)
            {
                Frame.Navigate(typeof(NewsPage), @"http://wap.gamersky.com/news/Content-" + wo.Link.Substring(wo.Link.LastIndexOf(@"/") + 1, 6) + ".html");
            }
            else
            {
                await new MessageDialog("目标链接错误或失效或不存在，请重试或联系作者！", "错误提示").ShowAsync();
            }
        }
        private void 加载(string html)
        {
            string s = "<img src=\"";
            string e = "\" alt=";
            MatchCollection M = new Regex("(?<=(" + s + "))[.\\s\\S]*?(?=(" + e + "))", RegexOptions.Multiline | RegexOptions.Singleline).Matches(html);
            string s2 = "alt=\"";
            string e2 = "\"  width";
            MatchCollection M2 = new Regex("(?<=(" + s2 + "))[.\\s\\S]*?(?=(" + e2 + "))", RegexOptions.Multiline | RegexOptions.Singleline).Matches(html);
            string s3 = "<div class=\"time\">";
            string e3 = "</div>";
            MatchCollection M3 = new Regex("(?<=(" + s3 + "))[.\\s\\S]*?(?=(" + e3 + "))", RegexOptions.Multiline | RegexOptions.Singleline).Matches(html);
            string s4 = "<div class=\"img\"><a href=\"";
            string e4 = "\" target";
            MatchCollection M4 = new Regex("(?<=(" + s4 + "))[.\\s\\S]*?(?=(" + e4 + "))", RegexOptions.Multiline | RegexOptions.Singleline).Matches(html);
            Task.Factory.StartNew(async () =>
            {
                await this.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    //Items0.Clear();
                    for (int i = 0; i < M.Count; i++)
                    {
                        Item item = new Item();
                        item.Link = M4[i].Value;
                        item.Img = new BitmapImage(new Uri(M[i].Value));
                        item.First = M2[i].Value;
                        item.Last = "发布日期：" + M3[i].Value;
                        Items0.Add(item); 
                    }
                    //DM1.Begin();
                });
            });
        }

        private void Button_Loaded(object sender, RoutedEventArgs e)
        {
            B0 = (Button)sender;
           // DispatcherTimer _timer = new DispatcherTimer();//定义一个定时器
           // _timer.Interval = TimeSpan.FromSeconds(0.3);
           // _timer.Tick += ((sender2, et) =>//flipview控件当前选定项的索引不断循环
           //{
           //    if (iscw == false)
           //    {
           //        if (Items0 != null && Items0.Count != 0)
           //        {
           //            if (((Button)sender).Content.ToString() != "到底了")
           //                ((Button)sender).Content = "加载更多";
           //            _timer.Stop();
           //        }
           //    }
           //    else
           //    {
           //        if (((Button)sender).Content.ToString() != "加载失败")
           //            ((Button)sender).Content = "加载失败";
           //    }
           //});
           // _timer.Start();
        }

        private async void 刷新_Click(object sender, RoutedEventArgs e)
        {
            ST0 = false;
            if (B0 != null)
            {
                B0.Content = "正在加载";
            }
            Items0.Clear();
            try
            {
                basefilter = new HttpBaseProtocolFilter();
                myfilter = new plugInFilter(basefilter);
                webClient = new HttpClient(myfilter);
                Text0 = await webClient.GetStringAsync(new Uri(@"http://acg.gamersky.com/music/"));
                加载(Text0.Substring(Text0.IndexOf("<ul class=\"Ptxt block\">"), Text0.IndexOf("<div class=\"Page\">") - Text0.IndexOf("<ul class=\"Ptxt block\">")));
                if (Text0.Contains("下一页</a></span>"))
                {
                    Match M2 = new Regex("(?<=(" + "class=\"p2\" href=\"" + "))[.\\s\\S]*?(?=(" + "\">下一页" + "))", RegexOptions.Multiline | RegexOptions.Singleline).Match(Text0.Substring(Text0.LastIndexOf("\">下一页") - 80, 100));
                    Uri0 = M2.Value;
                    if (B0 != null)
                    {
                        B0.Content = "加载更多";
                        B0.IsEnabled = true;
                    }
                }
                ST0 = true;

            }
            catch
            {
                if (B0 != null)
                {
                    B0.Content = "加载失败";
                }
                new MessageDialog("网络异常,数据加载失败，请重试或实在不行请联系作者！", "错误提示").ShowAsync();
            }
        }
    }
}
