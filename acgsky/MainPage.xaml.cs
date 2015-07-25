

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
using Windows.Graphics.Display;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
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
using Windows.Web.Http.Headers;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=391641 上有介绍

namespace acgsky
{

    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private HttpBaseProtocolFilter basefilter;
        private plugInFilter myfilter;
        private HttpClient webClient;
        private ObservableCollection<Item> Items0 = new ObservableCollection<Item>();
        private ObservableCollection<Item> Items2 = new ObservableCollection<Item>();
        private ObservableCollection<Item> Items3 = new ObservableCollection<Item>();
        private ObservableCollection<Item> Items4 = new ObservableCollection<Item>();
        private ObservableCollection<Item> Items5 = new ObservableCollection<Item>();
        private ObservableCollection<Item2> Items1 = new ObservableCollection<Item2>();
        private ObservableCollection<Item2> Items6 = new ObservableCollection<Item2>();
        private MatchCollection MM = null;
        private MatchCollection MM2 = null;
        private MatchCollection MM3 = null;
        private MatchCollection MM4 = null;
        public MainPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;

            ListView0.ItemsSource = Items0;
            ListView2.ItemsSource = Items2;
            ListView3.ItemsSource = Items3;
            ListView4.ItemsSource = Items4;
            ListView5.ItemsSource = Items5;
            //ListView1.ItemsSource = Items1;
            //ListViewyy.ItemsSource = Items6;
            获取主页文本();
            DM7.Completed += WC2;
            DM8.Completed += WC3;
            DM9.Completed += WC4;
            DM10.Completed += WC5;
            DM6.Completed += WC;
        }

        void HardwareButtons_BackPressed(object sender, Windows.Phone.UI.Input.BackPressedEventArgs e)//返回键事件
        {

            e.Handled = true;
            if (List.Visibility == Visibility.Collapsed)
            {
                if (Frame.CanGoBack)
                {
                    Frame.GoBack();
                }
                else
                {
                    Application.Current.Exit();
                }
            }
            else
            {
                DM6.Begin();
            }
        }
        /// <summary>
        /// 在此页将要在 Frame 中显示时进行调用。
        /// </summary>
        /// <param name="e">描述如何访问此页的事件数据。
        /// 此参数通常用于配置页。</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Windows.Phone.UI.Input.HardwareButtons.BackPressed += HardwareButtons_BackPressed;//注册返回键事件
            DisplayInformation.AutoRotationPreferences = DisplayOrientations.Portrait;
        }
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            Windows.Phone.UI.Input.HardwareButtons.BackPressed -= HardwareButtons_BackPressed;//注销返回键事件
        }
        public class Item
        {
            public String First { get; set; }
            public String Last { get; set; }
            public ImageSource Img { get; set; }
            public String Link { get; set; }
        }
        public class Item2
        {
            public String First { get; set; }
            public String Link { get; set; }
        }


        private async void 获取主页文本()
        {
            string Text="0";
            try
            {
                basefilter = new HttpBaseProtocolFilter();
                myfilter = new plugInFilter(basefilter);
                webClient = new HttpClient(myfilter);
                Text = await webClient.GetStringAsync(new Uri("http://acg.gamersky.com/"));//使用过滤器获取网页数据
            }
            catch
            {
                new MessageDialog("网络异常,数据加载失败，请重试或实在不行请联系作者！", "错误提示").ShowAsync();
            }
            try
            {
                if (Text.Length > 100)
                {
                    首页头条(Text.Substring(Text.IndexOf("<ul class=\"Bimg\">"), Text.IndexOf("<ul class=\"Simg\">") - Text.IndexOf("<ul class=\"Bimg\">")));
                    首页次条(Text.Substring(Text.IndexOf("<div class=\"Mid1pic\">"), Text.IndexOf("</div><!--Mid1 end-->") - Text.IndexOf("<div class=\"Mid1pic\">")));
                    今日推荐(Text.Substring(Text.IndexOf("<ul class=\"Ptxt block\">"), Text.IndexOf("<div class=\"Home-More\">") - Text.IndexOf("<ul class=\"Ptxt block\">")));
                    动漫美图(Text.Substring(Text.IndexOf("<ul class=\"DMBZ\">\r\n"), Text.IndexOf("<!--Mid6_L end-->") - Text.IndexOf("<ul class=\"DMBZ\">\r\n")));
                    Cosplay(Text.Substring(Text.IndexOf("<ul class=\"Cospaly\">"), Text.IndexOf("<!--Mid6_R end-->") - Text.IndexOf("<ul class=\"Cospaly\">")));
                    //写真(Text.Substring(Text.IndexOf("<div class=\"Mid4 R\">"), Text.IndexOf("<!--Mid_4 end-->") - Text.IndexOf("<div class=\"Mid4 R\">")));
                    //周边(Text.Substring(Text.IndexOf("<ul class=\"ZB\">"), Text.IndexOf("<ul class=\"YY\">") - Text.IndexOf("<ul class=\"ZB\">")));
                    //_动漫音乐(Text.Substring(Text.IndexOf("<ul class=\"YY\">"), Text.IndexOf("<ul class=\"RMBQ\">") - Text.IndexOf("<ul class=\"YY\">")));
                    //拆分字符串交给对应的方法解析加载
                }
            }
            catch (Exception e)
            {
                new MessageDialog("遇到致命错误,请联系作者" + e.Message, "错误提示").ShowAsync();
            }
        }
        //private void _动漫音乐(string html)
        //{
        //    string s3 = "<li class=\"li1\"><a href=\"";
        //    string e3 = "\" t";
        //    MatchCollection M3 = new Regex("(?<=(" + s3 + "))[.\\s\\S]*?(?=(" + e3 + "))", RegexOptions.Multiline | RegexOptions.Singleline).Matches(html);
        //    string s4 = "blank\">";
        //    string e4 = "</a></li>";
        //    MatchCollection M4 = new Regex("(?<=(" + s4 + "))[.\\s\\S]*?(?=(" + e4 + "))", RegexOptions.Multiline | RegexOptions.Singleline).Matches(html);
        //    Task.Factory.StartNew(async () =>
        //    {
        //        await this.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
        //        {
        //            Items6.Clear();
        //            for (int i = 0; i < M3.Count; i++)
        //            {
        //                Item2 item = new Item2();
        //                item.Link = M3[i].Value;
        //                item.First = M4[i].Value;
        //                Items6.Add(item);
        //            }
        //        });
        //    });
        //}
        private void 首页头条(string html)
        {
            string s = "<img src=\"";
            string e = "\" data-src=\"http://";
            MatchCollection M= new Regex("(?<=(" + s + "))[.\\s\\S]*?(?=(" + e + "))", RegexOptions.Multiline | RegexOptions.Singleline).Matches(html);

            //string s2 = "blank\">";
            //string e2 = "</a></li>";
            string s2 = "<div class=\"mask mk2\"><div class=\"txt\">";
            string e2 = "</div></div>\r\n\t\t\t\t\t</a>";
            MatchCollection M2 = new Regex("(?<=(" + s2 + "))[.\\s\\S]*?(?=(" + e2 + "))", RegexOptions.Multiline | RegexOptions.Singleline).Matches(html);
            //string temp;
            //Collection<Match> contents = new Collection<Match>(); 
            //for(int i = 0;i < M2.Count;i++)
            //{
            //    temp = M2[i].Value;
            //    Match tempMatch = new Regex("(?<=(\">))[.\\s\\S]*?$", RegexOptions.Multiline | RegexOptions.Singleline).Match(temp);
            //    contents.Add(tempMatch);
            //}
            string s3 = "<a href=\"";
            string e3 = "\" t";
            MatchCollection M3 = new Regex("(?<=(" + s3 + "))[.\\s\\S]*?(?=(" + e3 + "))", RegexOptions.Multiline | RegexOptions.Singleline).Matches(html);

            Task.Factory.StartNew(async () =>
            {
                await this.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    Button[] SYTTBs = { SYTTB0, SYTTB1, SYTTB2, SYTTB3, SYTTB4 };
                    for (int i = 0; i < SYTTBs.Length; i++)
                    {
                        //这一组M是提取的图片
                        SYTTBs[i].Background = new ImageBrush() { ImageSource = new BitmapImage(new Uri(M[i].Value)) };
                        //这一组content提取的是图片的标题
                        SYTTBs[i].Content = M2[i].Value;
                        //M3是图片的超链接
                        SYTTBs[i].Tag = M3[i].Value;
                    }
                   // SYTTB0.Background = new ImageBrush() { ImageSource = new BitmapImage(new Uri(M[0].Value)) };
                   // //SYTTB0.Image = new BitmapImage(new Uri(M[0].Value));
                   // //这一组M是提取的图片
                   // SYTTB1.Background = new ImageBrush() { ImageSource = new BitmapImage(new Uri(M[1].Value))};
                   // SYTTB2.Background = new ImageBrush() { ImageSource = new BitmapImage(new Uri(M[2].Value)) };
                   // SYTTB3.Background = new ImageBrush() { ImageSource = new BitmapImage(new Uri(M[3].Value))};
                   // SYTTB4.Background = new ImageBrush() { ImageSource = new BitmapImage(new Uri(M[4].Value)) };
                   ////这一组content提取的是图片的标题
                   // SYTTB0.Content = M2[0].Value;
                   // SYTTB1.Content = M2[1].Value;
                   // SYTTB2.Content = M2[2].Value;
                   // SYTTB3.Content = M2[3].Value;
                   // SYTTB4.Content = M2[4].Value;
                   // //M3是图片的超链接
                   // SYTTB0.Tag = M3[0].Value;
                   // SYTTB1.Tag = M3[1].Value;
                   // SYTTB2.Tag = M3[2].Value;
                   // SYTTB3.Tag = M3[3].Value;
                   // SYTTB4.Tag = M3[4].Value;
                });
            });
//            DispatcherTimer _timer = new DispatcherTimer();//定义一个定时器
//_timer.Interval = TimeSpan.FromSeconds(3.0);
// _timer.Tick += ((sender, et) =>//flipview控件当前选定项的索引不断循环
//{
    
//if (FlipView4.SelectedIndex < FlipView4.Items.Count-1)
//    {
//     FlipView4.SelectedIndex++;
//    }
//    else
//    {
//    FlipView4.SelectedIndex = 0;
//    }
// });

        }
        private void 首页次条(string html)
        {
            string s = "<img src=\"";
            string e = "\" alt=";
            MatchCollection M = new Regex("(?<=(" + s + "))[.\\s\\S]*?(?=(" + e + "))", RegexOptions.Multiline | RegexOptions.Singleline).Matches(html);

            string s2 = "alt=\"";
            string e2 = "\"  width";
            MatchCollection M2 = new Regex("(?<=(" + s2 + "))[.\\s\\S]*?(?=(" + e2 + "))", RegexOptions.Multiline | RegexOptions.Singleline).Matches(html);

            string s3 = "<a target=\"_blank\" href=\"";
            string e3 = "\" title=\"\">\n";
            MatchCollection M3 = new Regex("(?<=(" + s3 + "))[.\\s\\S]*?(?=(" + e3 + "))", RegexOptions.Multiline | RegexOptions.Singleline).Matches(html);
            Task.Factory.StartNew(async () =>
            {
                await this.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    Button[] TTSVBs = { TTSVB0, TTSVB1, TTSVB2, TTSVB3, TTSVB4, TTSVB5 };
                    //Button[] TTSVBs = { TTSVB0, TTSVB1, TTSVB2,TTSVB3 };
                    UIElementCollection TTSVBGrids = TTSVGrid.Children;
                    for (int i = 0; i < TTSVBGrids.Count; i++)
                    {
                        TTSVBs[i].Background = new ImageBrush() { ImageSource = new BitmapImage(new Uri(M[i].Value)) };
                        TTSVBs[i].Content = M2[i].Value;
                        TTSVBs[i].Tag = M3[i].Value;
                    }
                    //TTSVB0.Background = new ImageBrush() { ImageSource = new BitmapImage(new Uri(M[0].Value)) };
                    //TTSVB1.Background = new ImageBrush() { ImageSource = new BitmapImage(new Uri(M[1].Value)) };
                    //TTSVB2.Background = new ImageBrush() { ImageSource = new BitmapImage(new Uri(M[2].Value)) };
                    //TTSVB0.Content = M2[0].Value;
                    //TTSVB1.Content = M2[1].Value;
                    //TTSVB2.Content = M2[2].Value;
                    //TTSVB0.Tag = M3[0].Value;
                    //TTSVB1.Tag = M3[1].Value;
                    //TTSVB2.Tag = M3[2].Value;
                });
            });

        }
        private void 今日推荐(string html)//要闻推荐模块
        {
            //图片地址
            string s = "<img src=\"";
            string e = "\" alt=";
            MM = new Regex("(?<=(" + s + "))[.\\s\\S]*?(?=(" + e + "))", RegexOptions.Multiline | RegexOptions.Singleline).Matches(html);
            //标题地址
            string s2 = "alt=\"";
            string e2 = "\"  width";
            MM2 = new Regex("(?<=(" + s2 + "))[.\\s\\S]*?(?=(" + e2 + "))", RegexOptions.Multiline | RegexOptions.Singleline).Matches(html);
            //时间
            string s3 = "<div class=\"time\">";
            string e3 = "</div>";
            MM3 = new Regex("(?<=(" + s3 + "))[.\\s\\S]*?(?=(" + e3 + "))", RegexOptions.Multiline | RegexOptions.Singleline).Matches(html);
            //跳转链接
            string s4 = "<div class=\"img\"><a href=\"";
            string e4 = "\" target";
            MM4 = new Regex("(?<=(" + s4 + "))[.\\s\\S]*?(?=(" + e4 + "))", RegexOptions.Multiline | RegexOptions.Singleline).Matches(html);
        }
        private void 动漫美图(string html)
        {
            //大图片地址
            string s = "<img alt=\"\" src=\"";
            string e = "\" width=\"580\"";
            MatchCollection BigImgMatch = new Regex("(?<=(" + s + "))[.\\s\\S]*?(?=(" + e + "))", RegexOptions.Multiline | RegexOptions.Singleline).Matches(html);
            //大图片标题
            string BigImgTitleStartTag = "<div class=\"mk2\">";
            string BigImgTitleEndTag = "</div>\r\n";
            MatchCollection BigImgTitle = new Regex("(?<=(" + BigImgTitleStartTag + "))[.\\s\\S]*?(?=(" + BigImgTitleEndTag + "))", RegexOptions.Multiline | RegexOptions.Singleline).Matches(html);
            //图片地址
            string imgStartTag = "img src=\"";
            string imgEndTag = "\" alt";
            MatchCollection imgMatch = new Regex("(?<=(" + imgStartTag + "))[.\\s\\S]*?(?=(" + imgEndTag + "))", RegexOptions.Multiline | RegexOptions.Singleline).Matches(html);
            //标题
            string titleStartTag = "title=\"";
            string titleEndTag = "\">\n";
            MatchCollection titleMatch = new Regex("(?<=(" + titleStartTag + "))[.\\s\\S]*?(?=(" + titleEndTag + "))", RegexOptions.Multiline | RegexOptions.Singleline).Matches(html);
            //跳转链接
            string actionStartTag = "href=\"";
            string actionEndTag = "\"";
            MatchCollection actionMatch = new Regex("(?<=(" + actionStartTag + "))[.\\s\\S]*?(?=(" + actionEndTag + "))", RegexOptions.Multiline | RegexOptions.Singleline).Matches(html);
            Task.Factory.StartNew(async () =>
            {
                await this.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    PictureB0.Background = new ImageBrush() { ImageSource = new BitmapImage(new Uri(BigImgMatch[0].Value)) };
                    PictureB1.Background = new ImageBrush() { ImageSource = new BitmapImage(new Uri(imgMatch[0].Value)) };
                    PictureB2.Background = new ImageBrush() { ImageSource = new BitmapImage(new Uri(imgMatch[1].Value)) };
                    PictureB3.Background = new ImageBrush() { ImageSource = new BitmapImage(new Uri(imgMatch[2].Value)) };
                    //PictureB4.Background = new ImageBrush() { ImageSource = new BitmapImage(new Uri(M4[3].Value)) };
                    //PictureB5.Background = new ImageBrush() { ImageSource = new BitmapImage(new Uri(M4[4].Value)) };
                    //PictureB6.Background = new ImageBrush() { ImageSource = new BitmapImage(new Uri(M4[5].Value)) };
                    PictureB0.Content = BigImgTitle[0].Value;
                    PictureB1.Content = titleMatch[0].Value;
                    PictureB2.Content = titleMatch[1].Value;
                    PictureB3.Content = titleMatch[2].Value;
                    //PictureB4.Content = M2[4].Value;
                    //PictureB5.Content = M2[5].Value;
                    //PictureB6.Content = M2[6].Value;
                    PictureB0.Tag = actionMatch[0].Value;
                    PictureB1.Tag = actionMatch[1].Value;
                    PictureB2.Tag = actionMatch[2].Value;
                    PictureB3.Tag = actionMatch[3].Value;
                    //PictureB4.Tag = M3[4].Value;
                    //PictureB5.Tag = M3[5].Value;
                    //PictureB6.Tag = M3[6].Value;
                });
            });

        }
        private void Cosplay(string html)
        {
            string imgStartTag = " src=\"";
            string imgEndTag = "\" alt=\"";
            MatchCollection imgMatch = new Regex("(?<=(" + imgStartTag + "))[.\\s\\S]*?(?=(" + imgEndTag + "))", RegexOptions.Multiline | RegexOptions.Singleline).Matches(html);

            string titleStartTag = "title=\"";
            string titleEndTag = "\"";
            MatchCollection titleMatch = new Regex("(?<=(" + titleStartTag + "))[.\\s\\S]*?(?=(" + titleEndTag + "))", RegexOptions.Multiline | RegexOptions.Singleline).Matches(html);

            string actionStartTag = " href=\"";
            string actionEndTag = "\" title=\"";
            MatchCollection actionMatch = new Regex("(?<=(" + actionStartTag + "))[.\\s\\S]*?(?=(" + actionEndTag + "))", RegexOptions.Multiline | RegexOptions.Singleline).Matches(html);

            Task.Factory.StartNew(async () =>
            {
                await this.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    Button[] CosplayBs = { CosplayB0, CosplayB1, CosplayB2, CosplayB3 };
                    for (int i = 0; i < CosplayBs.Length; i++)
                    {
                        CosplayBs[i].Background = new ImageBrush() { ImageSource = new BitmapImage(new Uri(imgMatch[i].Value)) };
                        CosplayBs[i].Content = titleMatch[i].Value;
                        CosplayBs[i].Tag = actionMatch[i].Value;
                    }
                    //CosplayB0.Background = new ImageBrush() { ImageSource = new BitmapImage(new Uri(imgMatch[0].Value)) };
                    //CosplayB1.Background = new ImageBrush() { ImageSource = new BitmapImage(new Uri(imgMatch[1].Value)) };
                    //CosplayB2.Background = new ImageBrush() { ImageSource = new BitmapImage(new Uri(imgMatch[2].Value)) };
                    //CosplayB0.Content = titleMatch[0].Value;
                    //CosplayB1.Content = titleMatch[1].Value;
                    //CosplayB2.Content = titleMatch[2].Value;
                    //CosplayB0.Tag = actionMatch[0].Value;
                    //CosplayB1.Tag = actionMatch[1].Value;
                    //CosplayB2.Tag = actionMatch[2].Value;
                });
            });
        }
        //private void 写真(string html)
        //{
        //    string s = "<img src=\"";
        //    string e = "\" ";
        //    MatchCollection M = new Regex("(?<=(" + s + "))[.\\s\\S]*?(?=(" + e + "))", RegexOptions.Multiline | RegexOptions.Singleline).Matches(html);

        //    string s2 = "<div class=\"txt\">";
        //    string e2 = "</div>";
        //    MatchCollection M2 = new Regex("(?<=(" + s2 + "))[.\\s\\S]*?(?=(" + e2 + "))", RegexOptions.Multiline | RegexOptions.Singleline).Matches(html);

        //    string s3 = "<a href=\"";
        //    string e3 = "\" t";
        //    Regex r3 = new Regex("(?<=(" + s3 + "))[.\\s\\S]*?(?=(" + e3 + "))", RegexOptions.Multiline | RegexOptions.Singleline);
        //    MatchCollection M3 = r3.Matches(html);

        //    Task.Factory.StartNew(async () =>
        //    {
        //        await this.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
        //        {
        //            PortraitB0.Background = new ImageBrush() { ImageSource = new BitmapImage(new Uri(M[0].Value)) };
        //            PortraitB1.Background = new ImageBrush() { ImageSource = new BitmapImage(new Uri(M[1].Value)) };
        //            PortraitB2.Background = new ImageBrush() { ImageSource = new BitmapImage(new Uri(M[2].Value)) };
        //            PortraitB0.Content = M2[0].Value;
        //            PortraitB1.Content = M2[1].Value;
        //            PortraitB2.Content = M2[2].Value;
        //            PortraitB0.Tag = M3[0].Value;
        //            PortraitB1.Tag = M3[1].Value;
        //            PortraitB2.Tag = M3[2].Value;
        //        });
        //    });
        //}
        //private void 周边(string html)
        //{
        //    string html2 = html.Substring(20, html.LastIndexOf("</div></a></li>"));
        //    string html3 = html.Substring(html.LastIndexOf("</div></a></li>") + 20, html.LastIndexOf("</ul>") - html.LastIndexOf("</div></a></li>") - 20);

        //    string s = "<img src=\"";
        //    string e = "\" ";
        //    MatchCollection M = new Regex("(?<=(" + s + "))[.\\s\\S]*?(?=(" + e + "))", RegexOptions.Multiline | RegexOptions.Singleline).Matches(html2);


        //    string s2 = "<div class=\"txt\">";
        //    string e2 = "</div>";
        //    MatchCollection M2 = new Regex("(?<=(" + s2 + "))[.\\s\\S]*?(?=(" + e2 + "))", RegexOptions.Multiline | RegexOptions.Singleline).Matches(html2);


        //    string s5 = "<a href=\"";
        //    string e5 = "\" t";
        //    MatchCollection M5 = new Regex("(?<=(" + s5 + "))[.\\s\\S]*?(?=(" + e5 + "))", RegexOptions.Multiline | RegexOptions.Singleline).Matches(html2);


        //    string s3 = "<li class=\"li1\"><a href=\"";
        //    string e3 = "\" t";
        //    MatchCollection M3 = new Regex("(?<=(" + s3 + "))[.\\s\\S]*?(?=(" + e3 + "))", RegexOptions.Multiline | RegexOptions.Singleline).Matches(html3);
        //    string s4 = "blank\">";
        //    string e4 = "</a></li>";
        //    MatchCollection M4 = new Regex("(?<=(" + s4 + "))[.\\s\\S]*?(?=(" + e4 + "))", RegexOptions.Multiline | RegexOptions.Singleline).Matches(html3);
        //    Task.Factory.StartNew(async () =>
        //    {
        //        await this.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
        //        {
        //            Products0.Background = new ImageBrush() { ImageSource = new BitmapImage(new Uri(M[0].Value)) };
        //            Products1.Background = new ImageBrush() { ImageSource = new BitmapImage(new Uri(M[1].Value)) };
        //            Products0.Content = M2[0].Value;
        //            Products1.Content = M2[1].Value;
        //            Products0.Tag = M5[0].Value;
        //            Products1.Tag = M5[1].Value;
        //            Items1.Clear();
        //            for (int i = 0; i < M3.Count; i++)
        //            {
        //                Item2 item = new Item2();
        //                item.Link = M3[i].Value;
        //                item.First = M4[i].Value;
        //                Items1.Add(item);
        //            }
        //        });
        //    });

        //}



        #region 今日推荐的数据分析加载
        private bool ST0 = false;
        private bool ST1 = false;
        private bool ST2 = false;
        private bool ST3 = false;
        private bool ST4 = false;
        private DispatcherTimer dispatcherTimer0 = null;
        private DispatcherTimer dispatcherTimer1 = null;
        private DispatcherTimer dispatcherTimer2 = null;
        private DispatcherTimer dispatcherTimer3 = null;
        private DispatcherTimer dispatcherTimer4 = null;



  

        void dispatcherTimer_Tick0(object sender, object e)
        {
            if (MM4 != null)
            {
                Task.Factory.StartNew(async () =>
                {
                    await this.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                    {
                        Items0.Clear();
                        for (int i = 0; i < 7; i++)
                        {
                            Item item = new Item();
                            item.Link = MM4[i].Value;
                            if (MM[i].Value!=string.Empty)
                            {
                                item.Img = new BitmapImage(new Uri(MM[i].Value));
                            }
                            item.First = MM2[i].Value;
                            item.Last = "发布日期：" + MM3[i].Value;
                            Items0.Add(item);
                        }
                        DM0.Begin();
                    });
                });
                dispatcherTimer0.Stop();
            }
        }
        void dispatcherTimer_Tick1(object sender, object e)
        {
            if (MM4 != null)
            {
                Task.Factory.StartNew(async () =>
                {
                    await this.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                    {
                        Items2.Clear();
                        for (int i = 7; i < 14; i++)
                        {
                            Item item = new Item();
                            item.Link = MM4[i].Value;
                            item.Img = new BitmapImage(new Uri(MM[i].Value));
                            item.First = MM2[i].Value;
                            item.Last = "发布日期：" + MM3[i].Value;
                            Items2.Add(item);
                        }
                        DM1.Begin();
                    });
                });
                dispatcherTimer1.Stop();
            }
        }
        void dispatcherTimer_Tick2(object sender, object e)
        {
            if (MM4 != null)
            {
                Task.Factory.StartNew(async () =>
                {
                    await this.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                    {
                        Items3.Clear();
                        for (int i = 14; i < 21; i++)
                        {
                            Item item = new Item();
                            item.Link = MM4[i].Value;
                            item.Img = new BitmapImage(new Uri(MM[i].Value));
                            item.First = MM2[i].Value;
                            item.Last = "发布日期：" + MM3[i].Value;
                            Items3.Add(item);
                        }
                        DM2.Begin();
                    });
                });
                dispatcherTimer2.Stop();
            }
        }
        void dispatcherTimer_Tick3(object sender, object e)
        {
            if (MM4 != null)
            {
                Task.Factory.StartNew(async () =>
                {
                    await this.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                    {
                        Items4.Clear();
                        for (int i = 21; i < 28; i++)
                        {
                            Item item = new Item();
                            item.Link = MM4[i].Value;
                            item.Img = new BitmapImage(new Uri(MM[i].Value));
                            item.First = MM2[i].Value;
                            item.Last = "发布日期：" + MM3[i].Value;
                            Items4.Add(item);
                        }
                        DM3.Begin();
                    });
                });
                dispatcherTimer3.Stop();
            }
        }
        void dispatcherTimer_Tick4(object sender, object e)
        {
            if (MM4 != null)
            {
                Task.Factory.StartNew(async () =>
                {
                    await this.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                    {
                        Items5.Clear();
                        for (int i = 28; i < 35; i++)
                        {
                            Item item = new Item();
                            item.Link = MM4[i].Value;
                            item.Img = new BitmapImage(new Uri(MM[i].Value));
                            item.First = MM2[i].Value;
                            item.Last = "发布日期：" + MM3[i].Value;
                            Items5.Add(item);
                        }
                        DM4.Begin();
                    });
                });
                dispatcherTimer4.Stop();
            }
        }
        private void ShouYeTuiJian_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (ShouYeTuiJian.SelectedIndex)
            {
                case 0:
                    if (ST0 == false)
                    {
                        ST0 = true;
                        dispatcherTimer0 = new DispatcherTimer();
                        dispatcherTimer0.Tick += dispatcherTimer_Tick0;
                        dispatcherTimer0.Interval = new TimeSpan(0,0,0,0,500);
                        dispatcherTimer0.Start();
                    }
                    break;
                case 1:
                    if (ST1 == false)
                    {
                        ST1 = true;
                        dispatcherTimer1 = new DispatcherTimer();
                        dispatcherTimer1.Tick += dispatcherTimer_Tick1;
                        dispatcherTimer1.Interval = new TimeSpan(0, 0, 0, 0, 500);
                        dispatcherTimer1.Start();
                    }
                    break;
                case 2:
                    if (ST2 == false)
                    {
                        ST2 = true;
                        dispatcherTimer2 = new DispatcherTimer();
                        dispatcherTimer2.Tick += dispatcherTimer_Tick2;
                        dispatcherTimer2.Interval = new TimeSpan(0, 0, 0, 0, 500);
                        dispatcherTimer2.Start();
                    }
                    break;
                case 3:
                    if (ST3 == false)
                    {
                        ST3 = true;
                        dispatcherTimer3 = new DispatcherTimer();
                        dispatcherTimer3.Tick += dispatcherTimer_Tick3;
                        dispatcherTimer3.Interval = new TimeSpan(0, 0, 0, 0, 500);
                        dispatcherTimer3.Start();
                    }
                    break;
                case 4:
                    if (ST4 == false)
                    {
                        ST4 = true;
                        dispatcherTimer4 = new DispatcherTimer();
                        dispatcherTimer4.Tick += dispatcherTimer_Tick4;
                        dispatcherTimer4.Interval = new TimeSpan(0, 0, 0, 0, 500);
                        dispatcherTimer4.Start();
                    }
                    break;
            }
        }
        #endregion
        #region 各种点击事件
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if (((Button)sender).Tag!=null)
            {
            if (((Button)sender).Tag.ToString().Length > 10)
            {
                if (((Button)sender).Tag.ToString().Contains("html")||((Button)sender).Tag.ToString().Contains("htm"))
                {
                    Frame.Navigate(typeof(NewsPage), @"http://wap.gamersky.com/news/Content-" + ((Button)sender).Tag.ToString().Substring(((Button)sender).Tag.ToString().LastIndexOf(@"/") + 1, 6) + ".html");
                }
                else 
                {
                    await Windows.System.Launcher.LaunchUriAsync(new Uri(((Button)sender).Tag.ToString()));
                }
            }
            else
            {
                await new MessageDialog("目标链接错误或失效或不存在，请重试或联系作者！", "错误提示").ShowAsync();
            }
            }
        }

        private void 刷新_Click(object sender, RoutedEventArgs e)
        {
            SYTTB0.Content = SYTTB1.Content = SYTTB2.Content = SYTTB3.Content = SYTTB4.Content  = "载入中";
            TTSVB0.Content = TTSVB1.Content = TTSVB2.Content= "载入中";
            //PictureB0.Content = PictureB1.Content = PictureB2.Content = PictureB3.Content = PictureB4.Content = PictureB5.Content = PictureB6.Content  = "载入中";
            PictureB0.Content = PictureB1.Content = PictureB2.Content = PictureB3.Content  = "载入中";
            CosplayB0.Content = CosplayB1.Content = CosplayB2.Content = "载入中";
            //PortraitB0.Content = PortraitB1.Content = PortraitB2.Content = "载入中";
            //Products0.Content = Products1.Content = "载入中";

            SYTTB0.Background = SYTTB1.Background = SYTTB2.Background = SYTTB3.Background = SYTTB4.Background = null;
            TTSVB0.Background = TTSVB1.Background = TTSVB2.Background = null;
            //PictureB0.Background = PictureB1.Background = PictureB2.Background = PictureB3.Background = PictureB4.Background = PictureB5.Background = PictureB6.Background = null;
            PictureB0.Background = PictureB1.Background = PictureB2.Background = PictureB3.Background  = null;
            CosplayB0.Background = CosplayB1.Background = CosplayB2.Background = null;
            //PortraitB0.Background = PortraitB1.Background = PortraitB2.Background = null;
            //Products0.Background = Products1.Background = null;

            switch (ShouYeTuiJian.SelectedIndex)
            {
                case 0: dispatcherTimer0.Stop(); break;
                case 1: dispatcherTimer1.Stop(); break;
                case 2: dispatcherTimer2.Stop(); break;
                case 3: dispatcherTimer3.Stop(); break;
                case 4: dispatcherTimer4.Stop(); break;
            }

            ST0 = false;
            ST1 = false;
            ST2 = false;
            ST3 = false;
            ST4 = false;
            Items0.Clear();
            Items1.Clear();
            Items2.Clear();
            Items3.Clear();
            Items4.Clear();
            Items5.Clear();
            Items6.Clear();
            获取主页文本();

            switch (ShouYeTuiJian.SelectedIndex)
            {
                case 0:
                    if (ST0 == false)
                    {
                        ST0 = true;
                        dispatcherTimer0 = new DispatcherTimer();
                        dispatcherTimer0.Tick += dispatcherTimer_Tick0;
                        dispatcherTimer0.Interval = new TimeSpan(0, 0, 0, 0, 500);
                        dispatcherTimer0.Start();
                    }
                    break;
                case 1:
                    if (ST1 == false)
                    {
                        ST1 = true;
                        dispatcherTimer1 = new DispatcherTimer();
                        dispatcherTimer1.Tick += dispatcherTimer_Tick1;
                        dispatcherTimer1.Interval = new TimeSpan(0, 0, 0, 0, 500);
                        dispatcherTimer1.Start();
                    }
                    break;
                case 2:
                    if (ST2 == false)
                    {
                        ST2 = true;
                        dispatcherTimer2 = new DispatcherTimer();
                        dispatcherTimer2.Tick += dispatcherTimer_Tick2;
                        dispatcherTimer2.Interval = new TimeSpan(0, 0, 0, 0, 500);
                        dispatcherTimer2.Start();
                    }
                    break;
                case 3:
                    if (ST3 == false)
                    {
                        ST3 = true;
                        dispatcherTimer3 = new DispatcherTimer();
                        dispatcherTimer3.Tick += dispatcherTimer_Tick3;
                        dispatcherTimer3.Interval = new TimeSpan(0, 0, 0, 0, 500);
                        dispatcherTimer3.Start();
                    }
                    break;
                case 4:
                    if (ST4 == false)
                    {
                        ST4 = true;
                        dispatcherTimer4 = new DispatcherTimer();
                        dispatcherTimer4.Tick += dispatcherTimer_Tick4;
                        dispatcherTimer4.Interval = new TimeSpan(0, 0, 0, 0, 500);
                        dispatcherTimer4.Start();
                    }
                    break;
            }
        }
        private void 全部分类_Click(object sender, RoutedEventArgs e)
        {
            List.Visibility = Visibility.Visible;
            DM5.Begin();
        }

        //private void 关于_Click(object sender, RoutedEventArgs e)
        //{
        //    Frame.Navigate(typeof(InfoPage));
        //}

        private async void ListView0_ItemClick(object sender, ItemClickEventArgs e)
        {
            Item wo = e.ClickedItem as Item;
            if (wo.Link.Length>10)
            {
                if (wo.Link.Contains("html"))
                {
                 Frame.Navigate(typeof(NewsPage), @"http://wap.gamersky.com/news/Content-" + wo.Link.Substring(wo.Link.LastIndexOf(@"/") + 1, 6) + ".html");
                }
                               else
                {
                    await Windows.System.Launcher.LaunchUriAsync(new Uri(((Button)sender).Tag.ToString()));
                }
            }
            else
            {
                await new MessageDialog("目标链接错误或失效或不存在，请重试或联系作者！", "错误提示").ShowAsync();
            }
        }

        private async void ListView1_ItemClick(object sender, ItemClickEventArgs e)
        {
            Item2 wo = e.ClickedItem as Item2;

            if (wo.Link.Length > 10)
            {
                if (wo.Link.Contains("html"))
                {
                    Frame.Navigate(typeof(NewsPage), @"http://wap.gamersky.com/news/Content-" + wo.Link.Substring(wo.Link.LastIndexOf(@"/") + 1, 6) + ".html");
                }
                else
                {
                    await Windows.System.Launcher.LaunchUriAsync(new Uri(((Button)sender).Tag.ToString()));
                }
            }
            else
            {
                await new MessageDialog("目标链接错误或失效或不存在，请重试或联系作者！", "错误提示").ShowAsync();
            }
        }
        void WC(object sender, object e)
        {
            List.Visibility = Visibility.Collapsed;
        }
        void WC2(object sender, object e)
        {
            List.Visibility = Visibility.Collapsed;
            Frame.Navigate(typeof(DMZXPage));
        }
        void WC3(object sender, object e)
        {
            List.Visibility = Visibility.Collapsed;
            Frame.Navigate(typeof(ZWHPage));
        }
        void WC4(object sender, object e)
        {
            List.Visibility = Visibility.Collapsed;
                Frame.Navigate(typeof(TPPage));
        }
        void WC5(object sender, object e)
        {
            List.Visibility = Visibility.Collapsed;
                Frame.Navigate(typeof(DMYYPage));
        }
        private void 动漫咨询_Click(object sender, RoutedEventArgs e)
        {
            DM7.Begin();//执行动画
        }

        private void 宅文化_Click(object sender, RoutedEventArgs e)
        {
            DM8.Begin();//执行动画
        }

        private void 图片_Click(object sender, RoutedEventArgs e)
        {
            DM9.Begin();//执行动画
        }
        private void 动漫音乐_Click(object sender, RoutedEventArgs e)
        {
            DM10.Begin();//执行动画
        }

        private void 收藏_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SCPage));//转跳到收藏页
        }
        private void List_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            DM6.Begin();//执行动画
        }

        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(TPPage), ((HyperlinkButton)sender).Tag);//转跳到图片列表
        }

        private void HyperlinkButton_Click_1(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ZWHPage), 2);//转跳到宅文化列表
        }
        private void HyperlinkButton_Click_2(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(DMYYPage));//转跳到动漫音乐列表
        }
        #endregion
        #region 适配1080p屏幕
        private void CosplayG_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            CosplayG.Height = e.NewSize.Width * 0.55 * 0.87;
            //写真G.Height = e.NewSize.Width * 0.55 * 0.87;
        }

        private void 周边G_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //周边G.Height = e.NewSize.Width * 0.50 * 1.31;
        }

        private void FlipView4_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            FlipView4.Height = e.NewSize.Width * 0.58;
        }

        private void ShouYeTuiJian_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.NewSize.Width > 500)
            {
                ShouYeTuiJian.Margin = new Thickness(0);
            }
            else
            {
                ShouYeTuiJian.Margin = new Thickness(0, 25, 0, 0);
            }
        }
        #endregion

        private void yjms_Click(object sender, RoutedEventArgs e)//夜间模式的按钮事件，yjms就是那个按钮，App.yjmsbl是个全局变量
        {
            if (yjms.Label == "夜间模式")
            {
                Color f = new Color();
                f.A = 180;
                f.R = 0;
                f.B = 0;
                f.G = 0;
                YJMS.Background = new SolidColorBrush(f);
                yjms.Label = "日间模式";
                App.yjmsbl = true;
            }
            else
            {
                Color f = new Color();
                f.A = 0;
                f.R = 0;
                f.B = 0;
                f.G = 0;
                YJMS.Background = new SolidColorBrush(f);
                yjms.Label = "夜间模式";
                App.yjmsbl = false;
            }

        }
    }
    public class plugInFilter : IHttpFilter//网络请求过滤器，使用了可以确保以UTF-8的编码返回网页html字符串
    {
        private IHttpFilter innerFilter;
        public plugInFilter(IHttpFilter innerFilter)
        {
            if (innerFilter == null)
            {
                throw new ArgumentException("innerFilter canner be null.");
            }
            this.innerFilter = innerFilter;
        }
        //在SendRequertAsync方法里添加自定义的HTTP头
        public IAsyncOperationWithProgress<HttpResponseMessage, HttpProgress> SendRequestAsync(HttpRequestMessage request)
        {
            return AsyncInfo.Run<HttpResponseMessage, HttpProgress>(async (cancellationToken, progress) =>
            {
                request.Headers.Add("Custom-Header", "CustomRequestValue");
                HttpResponseMessage response = await innerFilter.SendRequestAsync(request).AsTask(cancellationToken, progress);
                HttpMediaTypeHeaderValue contentType = response.Content.Headers.ContentType;
                if (string.IsNullOrEmpty(contentType.CharSet))
                {
                    contentType.CharSet = "UTF-8";
                }
                cancellationToken.ThrowIfCancellationRequested();
                response.Headers.Add("Custom-Header", "CustomResponseValue");
                return response;
            });
        }
        public void Dispose()
        {
            innerFilter.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
