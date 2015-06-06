using SQlite;
using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI;
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

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkID=390556 上有介绍

namespace acgsky
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class NewsPage : Page
    {
        public NewsPage()
        {
            this.InitializeComponent();
            if (App.yjmsbl==true)
            {
              Color f = new Color();
              f.A = 255;
                f.R = 64;
                f.B = 64;
                f.G = 64;
            NPG.Background= new SolidColorBrush(f);

            Color f2 = new Color();
            f2.A = 128;
            f2.R = 0;
            f2.B = 0;
            f2.G = 0;
            TPG.Background = new SolidColorBrush(f2);
            }
           //上面是夜间模式相关的
            WOF.Completed+= DM0_Completed;//注册动画的结束事件
            this.NavigationCacheMode = NavigationCacheMode.Disabled;
        }
        private void DM0_Completed(object sender, object e)//动画的结束事件
        { 
        ZT= true;
        }
        private bool ISYC = false;
        private bool ZT= false;
        private bool zhuangtai = false;
        private bool zhuangtai2 = false;
        private string URI;
        /// <summary>
        /// 在此页将要在 Frame 中显示时进行调用。
        /// </summary>
        /// <param name="e">描述如何访问此页的事件数据。
        /// 此参数通常用于配置页。</param>
        /// 
        private string link2;
        private string name2;
        protected override void OnNavigatedTo(NavigationEventArgs e)//进入页面事件
        {
            if (zhuangtai2 == false)
            {
                加载(e.Parameter.ToString());
                ljtb.Text = e.Parameter.ToString();
            }
            Windows.Phone.UI.Input.HardwareButtons.BackPressed += HardwareButtons_BackPressed;//注册返回键事件
        }
        protected override void OnNavigatedFrom(NavigationEventArgs e)//页面返回事件
        {
            we.NavigateToString("");
            TPS.Source = null;
            inStream = null;
            //上面用于情况内存
            Windows.Phone.UI.Input.HardwareButtons.BackPressed -= HardwareButtons_BackPressed;//注销返回键事件
        }
        private bool ZHUANGTAI = false;
        private void plwe_NavigationStarting(WebView sender, WebViewNavigationStartingEventArgs args)
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

        void HardwareButtons_BackPressed(object sender, Windows.Phone.UI.Input.BackPressedEventArgs e)
        {
            e.Handled = true;
            if (istpdk == true)
            {
                关闭图片();
            }
            else if (ispldk == true)
            {
                plwe.Visibility = Visibility.Collapsed;
                ZHUANGTAI = false;
                plwe.NavigateToString("");
                ispldk = false;
                CB.Visibility = Visibility.Visible;
            }
            else
            {
                Frame.GoBack();
            }
        }//返回键事件

        private bool istpdk = false;
        private bool ispldk = false;

        string bf = "";
        HttpClient webClient = new HttpClient();
        private async void 加载(string uri)
        {
            try
            {
                URI = uri;
                string lingshi = await webClient.GetStringAsync(new Uri(uri));
                lingshi = lingshi.Replace("<script type=\"text/javascript\" src=\"http://j.gamersky.com/g/playvideo.js\"></script>", "");
                lingshi = lingshi.Replace("<script type=\"text/javascript\" src=\"http://j.gamersky.com/Wap/js/wap_n.js\"></script>", "");
                lingshi = lingshi.Replace(@"http://j.gamersky.com/g/jquery-1.8.3.js", "ms-appx-web:///html/jquery-1.8.3.js");
                lingshi = lingshi.Replace(@"http://j.gamersky.com/wap/js/wapcontent.js", "ms-appx-web:///html/wapcontent.js");
                lingshi = lingshi.Replace(@"http://j.gamersky.com/wap/css/wap_n.css", "ms-appx-web:///html/wap_n.css");
                lingshi = lingshi.Replace(@"http://j.gamersky.com/wap/css/wap_content.css", "ms-appx-web:///html/wap_content.css");
                lingshi = lingshi.Replace(@"http://j.gamersky.com/wap/css/review.css", "ms-appx-web:///html/review.css");
                lingshi = lingshi.Replace(@"http://j.gamersky.com/wap/css/Comment.css", "ms-appx-web:///html/Comment.css");

                bf = lingshi;

                int lingshi2 = lingshi.IndexOf("<div class=\"Mid_3\">");
                int lingshi3 = lingshi.IndexOf("</body>");
                string lingshi4 = "</body>";
                lingshi = lingshi.Remove(lingshi2, lingshi3 - lingshi2 + lingshi4.Length);


                int lingshi22 = lingshi.IndexOf("<div class=\"Top\">");
                int lingshi33 = lingshi.IndexOf("<div class=\"Mid\">");
                lingshi = lingshi.Remove(lingshi22, lingshi33 - lingshi22 + "<div class=\"Mid\">".Length);

                int lingshi2222 = lingshi.IndexOf("<a class=\"pl");
                int lingshi3333 = lingshi.IndexOf("0</span></a>");
                lingshi = lingshi.Remove(lingshi2222, lingshi3333 - lingshi2222 + "0</span></a>".Length);


                if (lingshi.Contains("'page-num'"))
                {
                    Camera.Label = "当前是第" + new Regex("(?<=(" + "Crrt'>" + "))[.\\s\\S]*?(?=(" + @"</span>/" + "))", RegexOptions.Multiline | RegexOptions.Singleline).Match(lingshi).Value + "页，共" + new Regex("(?<=(" + "Total'>" + "))[.\\s\\S]*?(?=(" + @"</span></span>" + "))", RegexOptions.Multiline | RegexOptions.Singleline).Match(lingshi).Value + "页";
                    if (lingshi.Contains("'page-prev'"))
                    {
                        上一页.Tag = new Regex("(?<=(" + "<a href='" + "))[.\\s\\S]*?(?=(" + "' class='page-prev" + "))", RegexOptions.Multiline | RegexOptions.Singleline).Match(lingshi).Value;
                        上一页.IsEnabled = true;
                    }
                    else
                    {
                        上一页.IsEnabled = false;
                    }

                    if (lingshi.Contains("'page-next'"))
                    {
                        int LS = lingshi.IndexOf("class='page-num'");
                        下一页.Tag = new Regex("(?<=(" + "<a href='" + "))[.\\s\\S]*?(?=(" + "' class='page-next" + "))", RegexOptions.Multiline | RegexOptions.Singleline).Match(lingshi.Substring(LS, lingshi.LastIndexOf("下一页") + 3 - LS)).Value;
                        下一页.IsEnabled = true;
                        //new MessageDialog(下一页.Tag.ToString()).ShowAsync();
                    }
                    else
                    {
                        下一页.IsEnabled = false;
                    }
                }
                else
                {
                    Camera.Label = "只有一页";
                }

                
                if (lingshi.Contains("youkuplayer"))
                {
                    int ID = int.Parse(new Regex("(?<=(" + "youkuplayer" + "))[.\\s\\S]*?(?=(" + "\"" + "))", RegexOptions.Multiline | RegexOptions.Singleline).Match(lingshi).Value);
                    string GAG = "var vID = \"\"; var vMp4url = \"http://v.youku.com/v_show/id_\" + arr[" + ID.ToString() + "] + \".html\";var vWidth = \"100%\";var vHeight = \"100%\"; var vPlayer = \"\";  var vFile = \"\";  var vPic = \"\";document.write(unescape(\"%3Cscript src=\'http://api.cuplayer.net/api/?FlvID=\" + vMp4url + \"\' type='text/javascript'%3E%3C/script%3E\"));";
                    lingshi = lingshi.Replace("youkuplayer" + ID.ToString(), "CuPlayer");
                    lingshi = lingshi.Replace("<script type=\"text/javascript\" src=\"http://player.youku.com/jsapi\"></script>", "");
                    lingshi = lingshi.Replace("<script src=\"http://j.gamersky.com/g/playvideo.js\" type=\"text/javascript\"></script>", "");
                    lingshi = lingshi.Replace("play_arr(arr);", GAG);
                    lingshi = lingshi.Replace("377px","");
                    lingshi = lingshi.Replace("600px", "");
                }


                int lingshi222 = lingshi.IndexOf("<!--{HS.begin");
                int lingshi333 = lingshi.IndexOf("<!--{HS.end");
                lingshi = lingshi.Remove(lingshi222, lingshi333 - lingshi222 + "<!--{HS.end.pagination}-->".Length);
                lingshi = lingshi.Replace("target=\"_blank\"", "");
                lingshi = lingshi.Replace("0</span></a>", "</span></a>");


                if (App.yjmsbl == true)
                {
                    we.NavigateToString(lingshi.Insert(lingshi.IndexOf("</head>") - 1, "<style>body { background-color:rgb(64, 64, 64); }</style>"));
                }
                else
                {
                    we.NavigateToString(lingshi);
                }
                //夜间模式相关
                link2 = uri;
                name2=new Regex("(?<=(" + "Mid1_1\">" + "))[.\\s\\S]*?(?=(" + "</div>" + "))", RegexOptions.Multiline | RegexOptions.Singleline).Match(lingshi).Value;
                Camera2.IsEnabled = true;
                //Tbb.Text = lingshi.Insert(lingshi.IndexOf("</head>") - 1, "<style>body { background-color:rgb(64, 64, 64); }</style>");
            }
            catch (Exception E2)
            {
                ISYC = true;
                prr.Visibility = Visibility.Collapsed;
                tb2122.Visibility = Visibility.Visible;
            }

        }


        private void bi_ImageOpened(object sender, RoutedEventArgs e)
        {
            PB.Visibility = Visibility.Collapsed;
            
        }
        private void bi_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {
            PB.Visibility = Visibility.Collapsed;
            TPT.Visibility = Visibility.Visible;
            TPT.Text = "图片载入错误,请与作者联系！";
        }
        IBuffer inStream = null;

        private string lsuri = "";
        private async void we_NavigationStarting(WebView sender, WebViewNavigationStartingEventArgs args)//web控件的载入事件，点击内容都会触发这个事件
        {
            zhuangtai2 = true;
            if (zhuangtai == true)
            {
                if (args.Uri!=null)
                {
                    if (args.Uri.AbsoluteUri.Contains("img1.") == true)//判断打开的是不是图片
                    {
                        args.Cancel = true;
                        返回.Visibility = Visibility.Visible;
                        复位.Visibility = Visibility.Visible;
                        保存.Visibility = Visibility.Visible;

                        TPG.Visibility = Visibility.Visible;
                        刷新.Visibility = Visibility.Collapsed;
                        上一页.Visibility = Visibility.Collapsed;
                        下一页.Visibility = Visibility.Collapsed;
                        评论.Visibility = Visibility.Collapsed;
                        DM0.Begin();
                        try
                        {

                            if (lsuri != args.Uri.AbsoluteUri)
                            {
                                TPS.Source = null;
                                inStream = null;
                                HttpClient webClient = new HttpClient();
                                inStream = await webClient.GetBufferAsync(new Uri(args.Uri.AbsoluteUri.Substring(args.Uri.AbsoluteUri.LastIndexOf("http"))));
                            }
                            else
                            {

                                lsuri = args.Uri.AbsoluteUri;
                            }

                            if (inStream.Length < 900000)//判断图片的大小
                            {
                                CT.ScaleX = CT.ScaleY = 1;
                                CT.TranslateX = CT.TranslateY = 0;
                                zb = 0;
                                zb2 = 0;
                                zb3 = 1;
                                BitmapImage img = new BitmapImage();
                                await img.SetSourceAsync(inStream.AsStream().AsRandomAccessStream());
                                img.ImageOpened += new RoutedEventHandler(bi_ImageOpened);
                                img.ImageFailed += new ExceptionRoutedEventHandler(bi_ImageFailed);
                                img.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                                TPS.Source = img;
                                img = null;
                                PB.Visibility = Visibility.Collapsed;
                            }
                            else
                            {
                                TPT.Text = "抱歉！由于WindowsPhone内存限制，不能在一般应用里预览 超高清的大图，但你仍然可以保存到图库，去图库->保存的图片里查看！";
                                TPT.Visibility = Visibility.Visible;
                                PB.Visibility = Visibility.Collapsed;
                            }
                        }
                        catch (Exception e)
                        {
                            PB.Visibility = Visibility.Collapsed;
                            TPT.Visibility = Visibility.Visible;
                            TPT.Text = "图片载入错误：" + e.Message + " 请与作者联系！";
                        }
                        istpdk = true;
                    }
                    else if (args.Uri.AbsoluteUri.Contains("acg.gamersky") == true)//判断打开的是超链接
                    {
                        args.Cancel = true;
                        if (args.Uri.AbsoluteUri.Contains("pic/mm") || args.Uri.AbsoluteUri.Contains("pic/xz") || args.Uri.AbsoluteUri.Contains("pic/cosplay"))
                        {
                            Frame.Navigate(typeof(TPPage));
                            Frame.BackStack.RemoveAt(Frame.BackStackDepth - 1);
                        }
                        else if (args.Uri.AbsoluteUri.Contains("donghua"))
                        {
                            args.Cancel = true;
                            WB(args.Uri.AbsoluteUri);
                        }
                        else
                        {
                            Frame.Navigate(typeof(NewsPage), @"http://wap.gamersky.com/news/Content-" + args.Uri.AbsoluteUri.Substring(args.Uri.AbsoluteUri.LastIndexOf(@"/") + 1, args.Uri.AbsoluteUri.LastIndexOf(@".") - 1 - args.Uri.AbsoluteUri.LastIndexOf(@"/")) + ".html");
                        }
                    }
                    else//其他
                    {
                        args.Cancel = true;
                        WB(args.Uri.AbsoluteUri);
                    }
                }
            }
        }
        async void WB(string uri)
        {
            await Windows.System.Launcher.LaunchUriAsync(new Uri(uri));
        }


        private void 上一页_Click(object sender, RoutedEventArgs e)
        {
            if(ZT==true)
            {
                        zhuangtai = false;
           ZT = false;
            WOF2.Begin();
            加载(@"http://wap.gamersky.com/news/" + ((Button)sender).Tag.ToString());
            }

        }

        private void 下一页_Click(object sender, RoutedEventArgs e)
        {
            if (ZT == true)
            {
                WOF2.Begin();
                zhuangtai = false;
                ZT = false;
                加载(@"http://wap.gamersky.com/news/" + ((Button)sender).Tag.ToString());
            }
        }

        private void 刷新_Click(object sender, RoutedEventArgs e)
        {
            if (ZT == true)
            {
                WOF2.Begin();
                zhuangtai = false;
                ZT = false;
                加载(URI);
            }
            if(ISYC==true)
            {
                prr.Visibility = Visibility.Visible;
                tb2122.Visibility = Visibility.Collapsed;
                WOF2.Begin();
                zhuangtai = false;
                ZT = false;
                加载(URI);
                ISYC = false;
            }
        }

        private void 评论_Click(object sender, RoutedEventArgs e)
        {
            if(bf!=null||bf.Length>50)
            {

                ZHUANGTAI = false;
                plwe.Visibility = Visibility.Visible;
                ispldk = true;
                CB.Visibility = Visibility.Collapsed;
                //Frame.Navigate(typeof(PLPage), bf);
                int lingshi2 = bf.IndexOf("<div class=\"Top\">");
                int lingshi3 = bf.LastIndexOf("<div class=\"Mid_4\">");
                string zong = bf.Remove(lingshi2, lingshi3 - lingshi2);
                int lingshi22 = zong.LastIndexOf("<div class=\"Bot\">");
                int lingshi33 = zong.LastIndexOf("</body>");
                zong = zong.Remove(lingshi22, lingshi33 - lingshi22);
                plwe.NavigateToString(zong.Replace("<body>", "<body>\r\n<div class=\"Mid\">"));
                
                //plwe.NavigateToString(zong.Remove(lingshi22, lingshi33 - lingshi22));
            }
        }

        private void we_DOMContentLoaded(WebView sender, WebViewDOMContentLoadedEventArgs args)
        {
                if (zhuangtai == false)
                {
                    WOF.Begin();
                    zhuangtai = true;
                }

        }
        double zb = 0;
        double zb2 = 0;
        double zb3 = 1;
        private void TPS_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            if (CT.ScaleY > 0.6)
            {
                CT.ScaleX = CT.ScaleY = (e.Cumulative.Scale) * zb3;
            }
            else
            {
                if (CT.ScaleY < (e.Cumulative.Scale) * zb3)
                {
                    CT.ScaleX = CT.ScaleY = (e.Cumulative.Scale) * zb3;
                }
   
            }
            CT.TranslateX = e.Cumulative.Translation.X + zb;
            CT.TranslateY = e.Cumulative.Translation.Y + zb2;
            e.Handled = true;
        }//图片的放大 拖拽事件

        private void TPS_ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {
            zb = CT.TranslateX;
            zb2 = CT.TranslateY;
            zb3 = CT.ScaleY;
       
        }

        private void 返回_Click(object sender, RoutedEventArgs e)
        {
            关闭图片();
        }

        private void 关闭图片()
        {
            返回.Visibility = Visibility.Collapsed;
            复位.Visibility = Visibility.Collapsed;
            保存.Visibility = Visibility.Collapsed;

            TPG.Visibility = Visibility.Collapsed;
            刷新.Visibility = Visibility.Visible;
            上一页.Visibility = Visibility.Visible;
            下一页.Visibility = Visibility.Visible;
            评论.Visibility = Visibility.Visible;

            DM1.Begin();

            PB.Visibility = Visibility.Visible;
            TPT.Visibility = Visibility.Collapsed;

            TPS.Source = null;
            inStream = null;

            istpdk = false;
        }

        private void 复位_Click(object sender, RoutedEventArgs e)
        {
            CT.ScaleX = CT.ScaleY = 1;
            CT.TranslateX = CT.TranslateY = 0;
            zb = 0;
            zb2 = 0;
            zb3 = 1;
        }

        private async void 保存_Click(object sender, RoutedEventArgs e)
        {
            StorageFolder storageFolder = await KnownFolders.PicturesLibrary.GetFolderAsync("Saved Pictures");
            StorageFile saveFile = await storageFolder.CreateFileAsync("ACGSKY.jpg", CreationCollisionOption.GenerateUniqueName);
            var outStream = await saveFile.OpenAsync(FileAccessMode.ReadWrite);//获取要新建的图片的流  
            try
            {
           await outStream.WriteAsync(inStream);
           await outStream.FlushAsync();

           返回.Visibility = Visibility.Collapsed;
           复位.Visibility = Visibility.Collapsed;
           保存.Visibility = Visibility.Collapsed;

           TPG.Visibility = Visibility.Collapsed;
           刷新.Visibility = Visibility.Visible;
           上一页.Visibility = Visibility.Visible;
           下一页.Visibility = Visibility.Visible;
           评论.Visibility = Visibility.Visible;
           DM1.Begin();
           PB.Visibility = Visibility.Visible;
           TPT.Visibility = Visibility.Collapsed;
           TPS.Source = null;
            }
            catch (Exception err)
            {
                PB.Visibility = Visibility.Collapsed;
                TPT.Visibility = Visibility.Visible;
                MessageDialog WO = new MessageDialog(err.Message, "错误提示");
                WO.ShowAsync();
                //TPT.Text = err.Message;
                //Debug.WriteLine(err.ToString());
            }
            finally
            {
                outStream.Dispose();//释放流

            }

        }//保存图片
        private SQLiteAsyncConnection GetConn()
        {

            return new SQLiteAsyncConnection(ApplicationData.Current.LocalFolder.Path + "\\note.db");

        }//新建数据库文件

  
        private async void Camera2_Click(object sender, RoutedEventArgs e)
        {
            SQLiteAsyncConnection conn;
                StorageFolder localFolder = ApplicationData.Current.LocalFolder;
                IReadOnlyList<StorageFile> files = await localFolder.GetFilesAsync();
                bool isExist = files.Any(file => file.Name == "note.db");
                if (!isExist)
                {
                    conn = GetConn();
                    await conn.CreateTableAsync<Note>();
                }
                else
                {
                    conn = GetConn();
                }
           
   
                 if ((await conn.Table<Note>().CountAsync()) != 0)
                {
                    bool BL =false;
                    List<Note> query = await conn.Table<Note>().ToListAsync();
                    for (int i = 0; i < query.Count; i++)
                    {
                        if (query[i].Link == link2)
                        {
                            BL = true;
                            break;
                        }
                    }
                    if (BL == false)
                    {
                        await conn.InsertAsync(new Note { Name = name2, Link = link2 });
                    }
                    else
                    {
                       await new MessageDialog("你已经收藏了哦！").ShowAsync();;
                   
                    }
                }
                else
                {
                    await conn.InsertAsync(new Note { Name = name2, Link = link2 });
                }


            //StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            //StorageFile saveFile = await storageFolder.GetFileAsync("note.db");

            //List<Note> query = await conn.Table<Note>().ToListAsync();

            // await new MessageDialog(query[5].Name).ShowAsync();
        }//添加到收藏

        private async void Camera3_Click(object sender, RoutedEventArgs e)
        {
                SQLiteAsyncConnection conn ;
                StorageFolder localFolder = ApplicationData.Current.LocalFolder;
                IReadOnlyList<StorageFile> files = await localFolder.GetFilesAsync();
                bool isExist = files.Any(file => file.Name == "note.db");
                if (!isExist)
                {
                    conn = GetConn();
                    await conn.CreateTableAsync<Note>();
                }
                else
                {
                    conn = GetConn();
                }
                
   
                 if ((await conn.Table<Note>().CountAsync()) != 0)
                {
                    bool BL = false;
                    List<Note> query = await conn.Table<Note>().ToListAsync();
                    for (int i = 0; i < query.Count; i++)
                    {
                        if (query[i].Link == link2)
                        {
                            await conn.DeleteAsync(query[i] as Object);
                            BL = true;
                            break;
                        }
                    }
                    if (BL == true)
                    {
                        await new MessageDialog("已为你取消！").ShowAsync();
                    }
                    else
                    {
                       await new MessageDialog("你还没有收藏哦").ShowAsync();
                   
                    }
                }
                else
                {
                    await new MessageDialog("你还没有收藏哦").ShowAsync();
                }
        }//取消收藏

        private void Camera4_Click(object sender, RoutedEventArgs e)//查看链接
        {
            lj.Visibility = Visibility.Visible;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            lj.Visibility = Visibility.Collapsed;
        }





        //scenarioFrame.BackStack.RemoveAt(scenarioFrame.BackStackDepth - 1);
        //private void we_DOMContentLoaded(WebView sender, WebViewDOMContentLoadedEventArgs args)
        //{
        //    if (zhuangtai == false)
        //    {
        //        //prr.Visibility = Visibility.Collapsed;
        //        //we.Visibility = Visibility.Visible;
        //        WOF.Begin();
        //        zhuangtai = true;
        //    }
        //}
    }
}
