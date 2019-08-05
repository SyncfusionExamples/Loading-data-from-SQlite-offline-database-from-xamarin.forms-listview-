using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SampleDemo
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            listView.CollapseAll();
            listView.Loaded += ListView_Loaded;
        }

        private void ListView_Loaded(object sender, Syncfusion.ListView.XForms.ListViewLoadedEventArgs e)
        {
            if (listView.DataSource.Groups.Count > 0)
                listView.ExpandGroup(listView.DataSource.Groups[0]);
        }
    }
}
