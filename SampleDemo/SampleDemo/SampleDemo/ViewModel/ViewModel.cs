using SQLite;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SampleDemo
{
    public class ViewModel
    {
        SQLiteConnection database;

        IEnumerable<OrderItem> orderItemCollection;

        ObservableCollection<OrderItem> OrderList;
        public IEnumerable<OrderItem> OrderItemCollection
        {
            get
            {
                if (orderItemCollection == null)
                {
                    GetItems();
                    orderItemCollection = OrderList;
                }
                return orderItemCollection;
            }
        }
        public ViewModel()
        {
            database = DependencyService.Get<ISQLite>().GetConnection();
            // Create the table
            database.CreateTable<OrderItem>();

            // Insert items into table
            database.Query<OrderItem>("INSERT INTO OrderItem (ID,Name)values (1001,'Antony')");
            database.Query<OrderItem>("INSERT INTO OrderItem (ID,Name)values (1002,'Blake')");
            database.Query<OrderItem>("INSERT INTO OrderItem (ID,Name)values (1003,'Catherine')");
            database.Query<OrderItem>("INSERT INTO OrderItem (ID,Name)values (1004,'Jude')");
            database.Query<OrderItem>("INSERT INTO OrderItem (ID,Name)values (1005,'Mark')");
            database.Query<OrderItem>("INSERT INTO OrderItem (ID,Name)values (1006,'Anderson')");
            database.Query<OrderItem>("INSERT INTO OrderItem (ID,Name)values (1007,'Wilson')");
            database.Query<OrderItem>("INSERT INTO OrderItem (ID,Name)values (1008,'Jade')");
            database.Query<OrderItem>("INSERT INTO OrderItem (ID,Name)values (1009,'Zachery')");
            database.Query<OrderItem>("INSERT INTO OrderItem (ID,Name)values (1010,'Ishant')");
            database.Query<OrderItem>("INSERT INTO OrderItem (ID,Name)values (1011,'Trunks')");
            database.Query<OrderItem>("INSERT INTO OrderItem (ID,Name)values (1012,'Itachi')");
            database.Query<OrderItem>("INSERT INTO OrderItem (ID,Name)values (1013,'Mathew')");
            database.Query<OrderItem>("INSERT INTO OrderItem (ID,Name)values (1014,'Watson')");
            database.Query<OrderItem>("INSERT INTO OrderItem (ID,Name)values (1015,'Chris Patt')");
            database.Query<OrderItem>("INSERT INTO OrderItem (ID,Name)values (1016,'Phanthom')");
        }

        public async void GetItems()
        {
            // Changing the database table items as ObservableCollection
            var table = (from i in database.Table<OrderItem>() select i);

            OrderList = new ObservableCollection<OrderItem>();

            foreach (var order in table)
            {
                OrderList.Add(new OrderItem()
                {
                    ID = order.ID,
                    Name = order.Name
                });
            }
            await Task.Delay(2000);

        }
    }
}
