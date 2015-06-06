using SQlite;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Popups;
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
    public sealed partial class SCPage : Page
    {
        public SCPage()
        {
            this.InitializeComponent();
            ls.ItemsSource = Items0;
        }

        /// <summary>
        /// 在此页将要在 Frame 中显示时进行调用。
        /// </summary>
        /// <param name="e">描述如何访问此页的事件数据。
        /// 此参数通常用于配置页。</param>
        /// 
        public class Item2
        {
            public String First { get; set; }
            public String Link { get; set; }
        }

        private ObservableCollection<Item2> Items0 = new ObservableCollection<Item2>();
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            if (App.yjmsbl == true)
            {
                YJMS.Visibility = Visibility.Visible;
            }
            else
            {
                YJMS.Visibility = Visibility.Collapsed;
            }
            //上面是判断是否打开夜间模式，下面是读取数据库里收藏的内容并显示
               SQLiteAsyncConnection conn = GetConn();
                StorageFolder localFolder = ApplicationData.Current.LocalFolder;
                IReadOnlyList<StorageFile> files = await localFolder.GetFilesAsync();
                bool isExist = files.Any(file => file.Name == "note.db");
                if (!isExist)
                {

                    await conn.CreateTableAsync<Note>();
                    List<Note> query = await conn.Table<Note>().ToListAsync();
                    Items0.Clear();
                    for (int i = 0; i < query.Count; i++)
                    {
                            Item2 item = new Item2();
                            item.Link = query[i].Link;
                            item.First = query[i].Name;
                            Items0.Add(item);
                    }
                }
                else
                {
                    List<Note> query = await conn.Table<Note>().ToListAsync();
                    Items0.Clear();
                    for (int i = 0; i < query.Count; i++)
                    {
                        Item2 item = new Item2();
                        item.Link = query[i].Link;
                        item.First = query[i].Name;
                        Items0.Add(item);
                    }
                }
                Windows.Phone.UI.Input.HardwareButtons.BackPressed += HardwareButtons_BackPressed;//注册返回键事件
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            Windows.Phone.UI.Input.HardwareButtons.BackPressed -= HardwareButtons_BackPressed;//注销返回键事件
        }
        void HardwareButtons_BackPressed(object sender, Windows.Phone.UI.Input.BackPressedEventArgs e)//返回键事件
        {
            e.Handled = true;
            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }
        private SQLiteAsyncConnection GetConn()
        {

            return new SQLiteAsyncConnection(ApplicationData.Current.LocalFolder.Path + "\\note.db");

        }//建立数据库文件


        private async void AppBarButton_Click(object sender, RoutedEventArgs e)//清空收藏
        {
                SQLiteAsyncConnection conn = GetConn();
                if ((await conn.Table<Note>().CountAsync()) != 0)
                {
                    List<Note> query = await conn.Table<Note>().ToListAsync();
                    for (int i = 0; i < query.Count; i++)
                    {
                        await conn.DeleteAsync(query[i] as Object);
                    }
                    Items0.Clear();
                }
                else
                {
                    await new MessageDialog("没有收藏").ShowAsync();
                }
        }


   



        private async void ls_ItemClick_1(object sender, ItemClickEventArgs e)//点击事件
        {
            if (((Item2)(e.ClickedItem)).Link.Length > 10)
           {
               Frame.Navigate(typeof(NewsPage), ((Item2)(e.ClickedItem)).Link);
           }
           else
           {
               await new MessageDialog("目标链接错误或失效或不存在，请重试或联系作者！", "错误提示").ShowAsync();
           }
        }
    }
}
