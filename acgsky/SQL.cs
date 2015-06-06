using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQlite
{
    public class Note : INotifyPropertyChanged
    {

        private int id;
        [AutoIncrement, PrimaryKey]
        public int ID
        {
            get { return id; }
            set
            {
                if (value != id)
                {
                    id = value;
                    RaisePropertyChanged("ID");

                }
            }
        }

           private string name;
            [MaxLength(100)]
           public string Name
           {
               get { return name; }
               set
               {
                   if (value != name)
                   {
                       name = value;
                       RaisePropertyChanged("Name");

                   }
               }
           }

            private string link;
            [MaxLength(100)]
            public string Link
            {
                get { return link; }
                set
                {
                    if (value != link)
                    {
                        link = value;
                        RaisePropertyChanged("Link");

                    }
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;
            protected void RaisePropertyChanged(string propertyName)
            {
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                }
            }
    }
}
