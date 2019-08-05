using Syncfusion.ListView.XForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SampleDemo
{
    public class CustomBehavior: Behavior<SfListView>
    {
        SfListView listView;
        ViewModel viewModel;
        protected override void OnAttachedTo(SfListView bindable)
        {
            listView = bindable;
            viewModel = new ViewModel();
            listView.BindingContext = viewModel;
            listView.ItemsSource = viewModel.OrderItemCollection;
            listView.Loaded += ListView_Loaded;
        }

        private void ListView_Loaded(object sender, ListViewLoadedEventArgs e)
        {
            if (listView.DataSource.Groups.Count > 0)
                listView.ExpandGroup(listView.DataSource.Groups[0]);
        }
    }
}
