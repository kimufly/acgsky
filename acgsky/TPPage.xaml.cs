using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    public sealed partial class TPPage : Page
    {
        private static HttpBaseProtocolFilter basefilter = new HttpBaseProtocolFilter();
        private static plugInFilter myfilter = new plugInFilter(basefilter);
        private HttpClient webClient = new HttpClient(myfilter);
        private ObservableCollection<Item> Items0 = new ObservableCollection<Item>();
        private ObservableCollection<Item> Items1 = new ObservableCollection<Item>();
        private ObservableCollection<Item> Items2 = new ObservableCollection<Item>();
        private ObservableCollection<Item> Items3 = new ObservableCollection<Item>();
        private ObservableCollection<Item> Items4 = new ObservableCollection<Item>();
        private ObservableCollection<Item> Items5 = new ObservableCollection<Item>();


        private string Uri0 = @"http://acg.gamersky.com/pic/wallpaper/";
        private string Uri1 = @"http://acg.gamersky.com/pic/wallpaper/pc/";
        private string Uri2 = @"http://acg.gamersky.com/pic/wallpaper/sj/";
        private string Uri3 = @"http://acg.gamersky.com/pic/mm/";
        private string Uri4 = @"http://acg.gamersky.com/pic/cosplay/";
        private string Uri5 = @"http://acg.gamersky.com/pic/xz/";

        private string Text0 = "";
        private string Text1 = "";
        private string Text2 = "";
        private string Text3 = "";
        private string Text4 = "";
        private string Text5 = "";

        private string s2 = "class=\"p2\" href=\"";
        private string e2 = "\">下一页";

        private bool ST0 = false;
        private bool ST1 = false;
        private bool ST2 = false;
        private bool ST3 = false;
        private bool ST4 = false;
        private bool ST5 = false;

        private Button B0;
        private Button B1;
        private Button B2;
        private Button B3;
        private Button B4;
        private Button B5;

        public TPPage()
        {
            this.InitializeComponent();
            GV0.ItemsSource = Items0;
            GV1.ItemsSource = Items1;
            GV2.ItemsSource = Items2;
            GV3.ItemsSource = Items3;
            GV4.ItemsSource = Items4;
            GV5.ItemsSource = Items5;
            this.NavigationCacheMode = NavigationCacheMode.Required;
        }
        public class Item
        {
            public String First { get; set; }
            public ImageSource Img { get; set; }
            public String Link { get; set; }
        }
        /// <summary>
        /// 在此页将要在 Frame 中显示时进行调用。
        /// </summary>
        /// <param name="e">描述如何访问此页的事件数据。
        /// 此参数通常用于配置页。</param>
        /// 
        string zt="0";//分页引导的值
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (App.yjmsbl == true)
            {
                YJMS.Visibility = Visibility.Visible;
            }
            else
            {
                YJMS.Visibility = Visibility.Collapsed;
            }
            if (e.Parameter!=null)
            {
                zt = e.Parameter.ToString();
                PT.SelectedIndex = int.Parse(zt);
            }
            Windows.Phone.UI.Input.HardwareButtons.BackPressed += HardwareButtons_BackPressed;
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

        private void 加载(string html)
        {
            string s = "<img src=\"";
            string e = "\" alt=";
            MatchCollection M = new Regex("(?<=(" + s + "))[.\\s\\S]*?(?=(" + e + "))", RegexOptions.Multiline | RegexOptions.Singleline).Matches(html);
            string s2 = "alt=\"";
            string e2 = "\"  width";
            MatchCollection M2 = new Regex("(?<=(" + s2 + "))[.\\s\\S]*?(?=(" + e2 + "))", RegexOptions.Multiline | RegexOptions.Singleline).Matches(html);
            string s4 = "<li><a href=\"";
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

                        switch (PT.SelectedIndex)
                        {
                            case 0: Items0.Add(item); break;
                            case 1: Items1.Add(item); break;
                            case 2: Items2.Add(item); break;
                            case 3: Items3.Add(item); break;
                            case 4: Items4.Add(item); break;
                            case 5: Items5.Add(item); break;
                        }
                    }
                    //DM1.Begin();
                });
            });
        }
        private async void Pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (((Pivot)sender).SelectedIndex)
            {
                case 0:
                    TB0.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 108, 0));
                    TB1.Foreground = new SolidColorBrush(Color.FromArgb(255, 192, 192, 192));
                    TB2.Foreground = new SolidColorBrush(Color.FromArgb(255, 192, 192, 192));
                    TB3.Foreground = new SolidColorBrush(Color.FromArgb(255, 192, 192, 192));
                    TB4.Foreground = new SolidColorBrush(Color.FromArgb(255, 192, 192, 192));
                    TB5.Foreground = new SolidColorBrush(Color.FromArgb(255, 192, 192, 192));
                    if (ST0 == false)
                    {
                        try
                        {
                            Text0 = await webClient.GetStringAsync(new Uri(@"http://acg.gamersky.com/pic/wallpaper/"));
                            加载(Text0.Substring(Text0.IndexOf("<ul class=\"Pic_txt\">"), Text0.IndexOf("<div class=\"Page\">") - Text0.IndexOf("<ul class=\"Pic_txt\">")));
                            if (Text0.Contains("下一页</a></span>"))
                            {
                                Match M2 = new Regex("(?<=(" + s2 + "))[.\\s\\S]*?(?=(" + e2 + "))", RegexOptions.Multiline | RegexOptions.Singleline).Match(Text0.Substring(Text0.LastIndexOf("\">下一页") - 80, 100));
                                Uri0 = M2.Value;
                            }
                            ST0 = true;
                            //iscw0 = false;
                            if (B0 != null)
                            {
                                B0.Content = "加载更多";
                            }
                        }
                        catch
                        {
                            //iscw0 = true;
                            if (B0 != null)
                            {
                                B0.Content = "加载失败";
                            }
                            new MessageDialog("网络异常,数据加载失败，请重试或实在不行请联系作者！", "错误提示").ShowAsync();
                        }
                    }
                    break;
                case 1:
                    TB1.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 108, 0));
                    TB0.Foreground = new SolidColorBrush(Color.FromArgb(255, 192, 192, 192));
                    TB2.Foreground = new SolidColorBrush(Color.FromArgb(255, 192, 192, 192));
                    TB3.Foreground = new SolidColorBrush(Color.FromArgb(255, 192, 192, 192));
                    TB4.Foreground = new SolidColorBrush(Color.FromArgb(255, 192, 192, 192));
                    TB5.Foreground = new SolidColorBrush(Color.FromArgb(255, 192, 192, 192));
                    if (ST1 == false)
                    {
                        try
                        {
                            Text1 = await webClient.GetStringAsync(new Uri(@"http://acg.gamersky.com/pic/wallpaper/pc/"));
                        加载(Text1.Substring(Text1.IndexOf("<ul class=\"Pic_txt\">"), Text1.IndexOf("<div class=\"Page\">") - Text1.IndexOf("<ul class=\"Pic_txt\">")));
                        if (Text1.Contains("下一页</a></span>"))
                        {
                            Match M2 = new Regex("(?<=(" + s2 + "))[.\\s\\S]*?(?=(" + e2 + "))", RegexOptions.Multiline | RegexOptions.Singleline).Match(Text1.Substring(Text1.LastIndexOf("\">下一页") - 80, 100));
                            Uri1 = M2.Value;
                        }
                        ST1 = true;
                        //iscw1 = false;
                        if (B1 != null)
                        {
                            B1.Content = "加载更多";
                        }
                        }
                        catch
                        {
                            //iscw1 = true;
                            if (B1 != null)
                            {
                                B1.Content = "加载失败";
                            }
                            new MessageDialog("网络异常,数据加载失败，请重试或实在不行请联系作者！", "错误提示").ShowAsync();
                        }
                    }
                    break;
                case 2:
                    TB2.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 108, 0));
                    TB0.Foreground = new SolidColorBrush(Color.FromArgb(255, 192, 192, 192));
                    TB1.Foreground = new SolidColorBrush(Color.FromArgb(255, 192, 192, 192));
                    TB3.Foreground = new SolidColorBrush(Color.FromArgb(255, 192, 192, 192));
                    TB4.Foreground = new SolidColorBrush(Color.FromArgb(255, 192, 192, 192));
                    TB5.Foreground = new SolidColorBrush(Color.FromArgb(255, 192, 192, 192));
                    if (ST2 == false)
                    {
                        try 
                        {
                            Text2 = await webClient.GetStringAsync(new Uri(@"http://acg.gamersky.com/pic/wallpaper/sj/"));
                        加载(Text2.Substring(Text2.IndexOf("<ul class=\"Pic_txt\">"), Text2.IndexOf("<div class=\"Page\">") - Text2.IndexOf("<ul class=\"Pic_txt\">")));
                        if (Text2.Contains("下一页</a></span>"))
                        {
                            Match M2 = new Regex("(?<=(" + s2 + "))[.\\s\\S]*?(?=(" + e2 + "))", RegexOptions.Multiline | RegexOptions.Singleline).Match(Text2.Substring(Text2.LastIndexOf("\">下一页") - 80, 100));
                            Uri2 = M2.Value;
                        }
                        ST2 = true;
                        //iscw2 = false;
                        if (B2 != null)
                        {
                            B2.Content = "加载更多";
                        }
                        }
                        catch
                        {
                            //iscw2 = true;
                            if (B2 != null)
                            {
                                B2.Content = "加载失败";
                            }
                            new MessageDialog("网络异常,数据加载失败，请重试或实在不行请联系作者！", "错误提示").ShowAsync();
                        }
                    }
                    break;
                case 3:
                    TB3.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 108, 0));
                    TB1.Foreground = new SolidColorBrush(Color.FromArgb(255, 192, 192, 192));
                    TB2.Foreground = new SolidColorBrush(Color.FromArgb(255, 192, 192, 192));
                    TB0.Foreground = new SolidColorBrush(Color.FromArgb(255, 192, 192, 192));
                    TB4.Foreground = new SolidColorBrush(Color.FromArgb(255, 192, 192, 192));
                    TB5.Foreground = new SolidColorBrush(Color.FromArgb(255, 192, 192, 192));
                    if (ST3 == false)
                    {
                        try
                        {
                            Text3 = await webClient.GetStringAsync(new Uri(@"http://acg.gamersky.com/pic/mm/"));
                        加载(Text3.Substring(Text3.IndexOf("<ul class=\"Pic_txt\">"), Text3.IndexOf("<div class=\"Page\">") - Text3.IndexOf("<ul class=\"Pic_txt\">")));
                        if (Text3.Contains("下一页</a></span>"))
                        {
                            Match M2 = new Regex("(?<=(" + s2 + "))[.\\s\\S]*?(?=(" + e2 + "))", RegexOptions.Multiline | RegexOptions.Singleline).Match(Text3.Substring(Text3.LastIndexOf("\">下一页") - 80, 100));
                            Uri3 = M2.Value;
                        }
                        ST3 = true;
                        //iscw3 = false;
                        if (B3 != null)
                        {
                            B3.Content = "加载更多";
                        }
                        }
                        catch
                        {
                            //iscw3 = true;
                            if (B3 != null)
                            {
                                B3.Content = "加载失败";
                            }
                            new MessageDialog("网络异常,数据加载失败，请重试或实在不行请联系作者！", "错误提示").ShowAsync();
                        }
                    }
                    break;
                case 4:
                    TB4.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 108, 0));
                    TB1.Foreground = new SolidColorBrush(Color.FromArgb(255, 192, 192, 192));
                    TB2.Foreground = new SolidColorBrush(Color.FromArgb(255, 192, 192, 192));
                    TB0.Foreground = new SolidColorBrush(Color.FromArgb(255, 192, 192, 192));
                    TB3.Foreground = new SolidColorBrush(Color.FromArgb(255, 192, 192, 192));
                    TB5.Foreground = new SolidColorBrush(Color.FromArgb(255, 192, 192, 192));
                    if (ST4== false)
                    {
                        try 
                        {

                            Text4 = await webClient.GetStringAsync(new Uri(@"http://acg.gamersky.com/pic/cosplay/"));
                        加载(Text4.Substring(Text4.IndexOf("<ul class=\"Pic_txt\">"), Text4.IndexOf("<div class=\"Page\">") - Text4.IndexOf("<ul class=\"Pic_txt\">")));
                        if (Text4.Contains("下一页</a></span>"))
                        {
                            Match M2 = new Regex("(?<=(" + s2 + "))[.\\s\\S]*?(?=(" + e2 + "))", RegexOptions.Multiline | RegexOptions.Singleline).Match(Text4.Substring(Text4.LastIndexOf("\">下一页") - 80, 100));
                            Uri4 = M2.Value;
                        }
                        ST4 = true;
                        //iscw4 = false;
                        if (B4 != null)
                        {
                            B4.Content = "加载更多";
                        }
                        }
                        catch
                        {
                            //iscw4 = true;
                            if (B4 != null)
                            {
                                B4.Content = "加载失败";
                            }
                            new MessageDialog("网络异常,数据加载失败，请重试或实在不行请联系作者！", "错误提示").ShowAsync();
                        }
                    }
                    break;
                case 5:
                    TB5.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 108, 0));
                    TB0.Foreground = new SolidColorBrush(Color.FromArgb(255, 192, 192, 192));
                    TB1.Foreground = new SolidColorBrush(Color.FromArgb(255, 192, 192, 192));
                    TB2.Foreground = new SolidColorBrush(Color.FromArgb(255, 192, 192, 192));
                    TB3.Foreground = new SolidColorBrush(Color.FromArgb(255, 192, 192, 192));
                    TB4.Foreground = new SolidColorBrush(Color.FromArgb(255, 192, 192, 192));
                    if (ST5 == false)
                    {
                        try
                        {
                            Text5 = await webClient.GetStringAsync(new Uri(@"http://acg.gamersky.com/pic/xz/"));
                        加载(Text5.Substring(Text5.IndexOf("<ul class=\"Pic_txt\">"), Text5.IndexOf("<div class=\"Page\">") - Text5.IndexOf("<ul class=\"Pic_txt\">")));
                        if (Text5.Contains("下一页</a></span>"))
                        {
                            Match M2 = new Regex("(?<=(" + s2 + "))[.\\s\\S]*?(?=(" + e2 + "))", RegexOptions.Multiline | RegexOptions.Singleline).Match(Text5.Substring(Text5.LastIndexOf("\">下一页") - 80, 100));
                            Uri5 = M2.Value;
                        }
                        ST5 = true;
                        //iscw5 = false;
                        if (B5 != null)
                        {
                            B5.Content = "加载更多";
                        }
                        }
                        catch
                        {
                            //iscw5 = true;
                            if (B5 != null)
                            {
                                B5.Content = "加载失败";
                            }
                            new MessageDialog("网络异常,数据加载失败，请重试或实在不行请联系作者！", "错误提示").ShowAsync();
                        }
                    }
                    break;
            }
        }//分页切换事件
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

        }//点击事件

        private async void Button_Click(object sender, RoutedEventArgs e)
        {

            //basefilter = new HttpBaseProtocolFilter();
            //myfilter = new plugInFilter(basefilter);
            //webClient = new HttpClient(myfilter);
            switch (PT.SelectedIndex)
            {
                case 0:
                    if (ST0 == true)
                    {
                        try
                        {
                            ((Button)sender).Content = "加载中";
                            Text0 = await webClient.GetStringAsync(new Uri(Uri0));
                            加载(Text0.Substring(Text0.IndexOf("<ul class=\"Pic_txt\">"), Text0.IndexOf("<div class=\"Page\">") - Text0.IndexOf("<ul class=\"Pic_txt\">")));
                            if (Text0.Contains("下一页</a></span>"))
                            {
                                ((Button)sender).IsEnabled = true;
                                Match M2 = new Regex("(?<=(" + s2 + "))[.\\s\\S]*?(?=(" + e2 + "))", RegexOptions.Multiline | RegexOptions.Singleline).Match(Text0.Substring(Text0.LastIndexOf("\">下一页") - 80, 100));
                                Uri0 = M2.Value;
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
                    break;
                case 1:
                    if (ST1 == true )
                    {
                        try
                        {
                            ((Button)sender).Content = "加载中";
                            Text1 = await webClient.GetStringAsync(new Uri(Uri1));
                            加载(Text1.Substring(Text1.IndexOf("<ul class=\"Pic_txt\">"), Text1.IndexOf("<div class=\"Page\">") - Text1.IndexOf("<ul class=\"Pic_txt\">")));
                            if (Text1.Contains("下一页</a></span>"))
                            {
                                ((Button)sender).IsEnabled = true;
                                Match M2 = new Regex("(?<=(" + s2 + "))[.\\s\\S]*?(?=(" + e2 + "))", RegexOptions.Multiline | RegexOptions.Singleline).Match(Text1.Substring(Text1.LastIndexOf("\">下一页") - 80, 100));
                                Uri1 = M2.Value;
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
                    break;
                case 2:
                    if (ST2 == true )
                    {
                        try
                        {
                            ((Button)sender).Content = "加载中";
                            Text2 = await webClient.GetStringAsync(new Uri(Uri2));
                            加载(Text2.Substring(Text2.IndexOf("<ul class=\"Pic_txt\">"), Text2.IndexOf("<div class=\"Page\">") - Text2.IndexOf("<ul class=\"Pic_txt\">")));
                            if (Text2.Contains("下一页</a></span>"))
                            {
                                ((Button)sender).IsEnabled = true;
                                Match M2 = new Regex("(?<=(" + s2 + "))[.\\s\\S]*?(?=(" + e2 + "))", RegexOptions.Multiline | RegexOptions.Singleline).Match(Text2.Substring(Text2.LastIndexOf("\">下一页") - 80, 100));
                                Uri2 = M2.Value;
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
                    break;
                case 3:
                    if (ST3 == true )
                    {
                        try
                        {
                            ((Button)sender).Content = "加载中";
                            Text3 = await webClient.GetStringAsync(new Uri(Uri3));
                            加载(Text3.Substring(Text3.IndexOf("<ul class=\"Pic_txt\">"), Text3.IndexOf("<div class=\"Page\">") - Text3.IndexOf("<ul class=\"Pic_txt\">")));
                            if (Text3.Contains("下一页</a></span>"))
                            {
                                ((Button)sender).IsEnabled = true;
                                Match M2 = new Regex("(?<=(" + s2 + "))[.\\s\\S]*?(?=(" + e2 + "))", RegexOptions.Multiline | RegexOptions.Singleline).Match(Text3.Substring(Text3.LastIndexOf("\">下一页") - 80, 100));
                                Uri3 = M2.Value;
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
                    break;
                case 4:
                    if (ST4 == true )
                    {
                        try
                        {
                            ((Button)sender).Content = "加载中";
                            Text4 = await webClient.GetStringAsync(new Uri(Uri4));
                            加载(Text4.Substring(Text4.IndexOf("<ul class=\"Pic_txt\">"), Text4.IndexOf("<div class=\"Page\">") - Text4.IndexOf("<ul class=\"Pic_txt\">")));
                            if (Text4.Contains("下一页</a></span>"))
                            {
                                ((Button)sender).IsEnabled = true;
                                Match M2 = new Regex("(?<=(" + s2 + "))[.\\s\\S]*?(?=(" + e2 + "))", RegexOptions.Multiline | RegexOptions.Singleline).Match(Text4.Substring(Text4.LastIndexOf("\">下一页") - 80, 100));
                                Uri4 = M2.Value;
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
                    break;
                case 5:
                    if (ST5 == true )
                    {
                        try
                        {
                            ((Button)sender).Content = "加载中";
                            Text5 = await webClient.GetStringAsync(new Uri(Uri5));
                            加载(Text5.Substring(Text5.IndexOf("<ul class=\"Pic_txt\">"), Text5.IndexOf("<div class=\"Page\">") - Text5.IndexOf("<ul class=\"Pic_txt\">")));
                            if (Text5.Contains("下一页</a></span>"))
                            {
                                ((Button)sender).IsEnabled = true;
                                Match M2 = new Regex("(?<=(" + s2 + "))[.\\s\\S]*?(?=(" + e2 + "))", RegexOptions.Multiline | RegexOptions.Singleline).Match(Text5.Substring(Text5.LastIndexOf("\">下一页") - 80, 100));
                                Uri5 = M2.Value;
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
                    break;
            }
        }//加载更多按钮的点击事件


        private async void 刷新_Click(object sender, RoutedEventArgs e)
        {
            //basefilter = new HttpBaseProtocolFilter();
            //myfilter = new plugInFilter(basefilter);
            //webClient = new HttpClient(myfilter);
            switch (PT.SelectedIndex)
            {
                case 0:
                    try
                    {
                        ST0 = false;
                        B0.Content = "加载中";
                        Items0.Clear();
                        Text0 = await webClient.GetStringAsync(new Uri(@"http://acg.gamersky.com/pic/wallpaper/"));
                        加载(Text0.Substring(Text0.IndexOf("<ul class=\"Pic_txt\">"), Text0.IndexOf("<div class=\"Page\">") - Text0.IndexOf("<ul class=\"Pic_txt\">")));
                        if (Text0.Contains("下一页</a></span>"))
                        {
                            Match M2 = new Regex("(?<=(" + s2 + "))[.\\s\\S]*?(?=(" + e2 + "))", RegexOptions.Multiline | RegexOptions.Singleline).Match(Text0.Substring(Text0.LastIndexOf("\">下一页") - 80, 100));
                            Uri0 = M2.Value;
                            B0.IsEnabled = true;
                            B0.Content = "加载更多";
                        }
                        ST0 = true;
                    }
                    catch
                    {
                        B0.Content = "加载失败";
                        new MessageDialog("网络异常,数据加载失败，请重试或实在不行请联系作者！", "错误提示").ShowAsync();
                    }
                    break;
                case 1:
                    try
                    {
                        ST1 = false;
                        B1.Content = "加载中";
                        Items1.Clear();
                        Text1 = await webClient.GetStringAsync(new Uri(@"http://acg.gamersky.com/pic/wallpaper/pc/"));
                        加载(Text1.Substring(Text1.IndexOf("<ul class=\"Pic_txt\">"), Text1.IndexOf("<div class=\"Page\">") - Text1.IndexOf("<ul class=\"Pic_txt\">")));
                        if (Text1.Contains("下一页</a></span>"))
                        {
                            Match M2 = new Regex("(?<=(" + s2 + "))[.\\s\\S]*?(?=(" + e2 + "))", RegexOptions.Multiline | RegexOptions.Singleline).Match(Text1.Substring(Text1.LastIndexOf("\">下一页") - 80, 100));
                            Uri1 = M2.Value;
                            B1.IsEnabled = true;
                            B1.Content = "加载更多";
                        }
                        ST1 = true;
                    }
                    catch
                    {
                        B1.Content = "加载失败";
                        new MessageDialog("网络异常,数据加载失败，请重试或实在不行请联系作者！", "错误提示").ShowAsync();
                    }
                    break;
                case 2:
                    try
                    {
                        ST2 = false;
                        B2.Content = "加载中";
                        Items2.Clear();
                        Text2 = await webClient.GetStringAsync(new Uri(@"http://acg.gamersky.com/pic/wallpaper/sj/"));
                        加载(Text2.Substring(Text2.IndexOf("<ul class=\"Pic_txt\">"), Text2.IndexOf("<div class=\"Page\">") - Text2.IndexOf("<ul class=\"Pic_txt\">")));
                        if (Text2.Contains("下一页</a></span>"))
                        {
                            Match M2 = new Regex("(?<=(" + s2 + "))[.\\s\\S]*?(?=(" + e2 + "))", RegexOptions.Multiline | RegexOptions.Singleline).Match(Text2.Substring(Text2.LastIndexOf("\">下一页") - 80, 100));
                            Uri2 = M2.Value;
                            B2.IsEnabled = true;
                            B2.Content = "加载更多";
                        }
                        ST2 = true;
                    }
                    catch
                    {
                        B2.Content = "加载失败";
                        new MessageDialog("网络异常,数据加载失败，请重试或实在不行请联系作者！", "错误提示").ShowAsync();
                    }
                    break;
                case 3:
                    try
                    {
                        ST3 = false;
                        B3.Content = "加载中";
                        Items3.Clear();
                        Text3 = await webClient.GetStringAsync(new Uri(@"http://acg.gamersky.com/pic/mm/"));
                        加载(Text3.Substring(Text3.IndexOf("<ul class=\"Pic_txt\">"), Text3.IndexOf("<div class=\"Page\">") - Text3.IndexOf("<ul class=\"Pic_txt\">")));
                        if (Text3.Contains("下一页</a></span>"))
                        {
                            Match M2 = new Regex("(?<=(" + s2 + "))[.\\s\\S]*?(?=(" + e2 + "))", RegexOptions.Multiline | RegexOptions.Singleline).Match(Text3.Substring(Text3.LastIndexOf("\">下一页") - 80, 100));
                            Uri3 = M2.Value;
                            B3.IsEnabled = true;
                            B3.Content = "加载更多";
                        }
                        ST3 = true;
                    }
                    catch
                    {
                        B3.Content = "加载失败";
                        new MessageDialog("网络异常,数据加载失败，请重试或实在不行请联系作者！", "错误提示").ShowAsync();
                    }
                    break;
                case 4:
                    try
                    {
                        ST4 = false;
                        B4.Content = "加载中";
                        Items4.Clear();
                        Text4 = await webClient.GetStringAsync(new Uri(@"http://acg.gamersky.com/pic/cosplay/"));
                        加载(Text4.Substring(Text4.IndexOf("<ul class=\"Pic_txt\">"), Text4.IndexOf("<div class=\"Page\">") - Text4.IndexOf("<ul class=\"Pic_txt\">")));
                        if (Text4.Contains("下一页</a></span>"))
                        {
                            Match M2 = new Regex("(?<=(" + s2 + "))[.\\s\\S]*?(?=(" + e2 + "))", RegexOptions.Multiline | RegexOptions.Singleline).Match(Text4.Substring(Text4.LastIndexOf("\">下一页") - 80, 100));
                            Uri4 = M2.Value;
                            B4.IsEnabled = true;
                            B4.Content = "加载更多";
                        }
                        ST4 = true;
                    }
                    catch
                    {
                        B4.Content = "加载失败";
                        new MessageDialog("网络异常,数据加载失败，请重试或实在不行请联系作者！", "错误提示").ShowAsync();
                    }
                    break;
                case 5:
                    try
                    {
                        ST5 = false;
                        B5.Content = "加载中";
                        Items5.Clear();
                        Text5 = await webClient.GetStringAsync(new Uri(@"http://acg.gamersky.com/pic/xz/"));
                        加载(Text5.Substring(Text5.IndexOf("<ul class=\"Pic_txt\">"), Text5.IndexOf("<div class=\"Page\">") - Text5.IndexOf("<ul class=\"Pic_txt\">")));
                        if (Text5.Contains("下一页</a></span>"))
                        {
                            Match M2 = new Regex("(?<=(" + s2 + "))[.\\s\\S]*?(?=(" + e2 + "))", RegexOptions.Multiline | RegexOptions.Singleline).Match(Text5.Substring(Text5.LastIndexOf("\">下一页") - 80, 100));
                            Uri5 = M2.Value;
                            B5.IsEnabled = true;
                            B5.Content = "加载更多";
                        }
                        ST5 = true;
                    }
                    catch
                    {
                        B5.Content = "加载失败";
                        new MessageDialog("网络异常,数据加载失败，请重试或实在不行请联系作者！", "错误提示").ShowAsync();
                    }
                    break;
            }
        }



        private void PT_SizeChanged(object sender, SizeChangedEventArgs e)//间接的方式让内容始终以两列显示，不懂可以不需要懂
        {
            G1.Width = (sender as Pivot).ActualWidth / 2 - 3;
        }

        private void Button_Loaded(object sender, RoutedEventArgs e)//这个也可以不需要懂
        {
            switch (PT.SelectedIndex)
            {
                case 0: B0 = (Button)sender; break;
                case 1: B1 = (Button)sender;  break;
                case 2: B2 = (Button)sender; break;
                case 3: B3 = (Button)sender; break;
                case 4: B4 = (Button)sender;  break;
                case 5: B5 = (Button)sender;  break;
            }
        }


    }
}
