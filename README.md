# Loading data from SQLite offline database

The SfListView allows binding the data from local database by using SQLite. To perform this, follow the steps: SQLiteConnection

Step 1: Create a `SQLite database` table
Step 2: Populate the data into the table
Step 3: Store them as an `IEnumerable` collection
Step 4: Bind it to SfListView using [SfListView.ItemSource](https://help.syncfusion.com/cr/cref_files/xamarin/Syncfusion.SfListView.XForms~Syncfusion.ListView.XForms.SfListView~ItemsSource.html) property.
Step 5: Refer to the following link to know how to create SQLite connection,
http://developer.xamarin.com/guides/xamarin-forms/working-with/databases/

```
To run this sample in UWP, install sqLite.Net.Pcl, version v1.0.10 (Only this version of SQLite supports UWP platform, later versions don’t support UWP).
```
```
using SQLite;

public class OrderItem
{
    public int ID { get; set; }
    public string Name { get; set; }
}
```
```
using SQLite;
  
public class ViewModel
{
    SQLiteConnection database;
    IEnumerable<OrderItem> orderItemCollection;
    public IEnumerable<OrderItem> OrderItemCollection
    {
        get
        {
            if (orderItemCollection == null)
                orderItemCollection = GetItems();
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
        database.Query<OrderItem>("INSERT INTO OrderItem (ID,Name)values (1010,'Dhotis')");
        database.Query<OrderItem>("INSERT INTO OrderItem (ID,Name)values (1011,'Trunks')");
        database.Query<OrderItem>("INSERT INTO OrderItem (ID,Name)values (1012,'Kevin')");
        database.Query<OrderItem>("INSERT INTO OrderItem (ID,Name)values (1013,'Mathew')");
        database.Query<OrderItem>("INSERT INTO OrderItem (ID,Name)values (1014,'Watson')");
        database.Query<OrderItem>("INSERT INTO OrderItem (ID,Name)values (1015,'Chris')");
        database.Query<OrderItem>("INSERT INTO OrderItem (ID,Name)values (1016,'Phantom')");
    }
  
    public IEnumerable<OrderItem> GetItems()
    {
        // Changing the database table items as ObservableCollection
        var table = (from i in database.Table<OrderItem>() select i);
        ObservableCollection<OrderItem> OrderList = new ObservableCollection<OrderItem>();
        foreach (var order in table)
        {
            OrderList.Add(new OrderItem()
            {
                ID = order.ID,
                Name = order.Name
            });
        }
        return OrderList;
    }
}
```
Refer to the following code which illustrates, how to bind the data from the SQLite database to SfListView.

```
<ContentPage xmlns:syncfusion="clr-namespace:Syncfusion.ListView.XForms;assembly=Syncfusion.SfListView.XForms">
    <ContentPage.BindingContext>
        <local:ViewModel/>
    </ContentPage.BindingContext>
    <ContentPage.Content>
        <Grid>
            <listView:SfListView x:Name="listView" ItemSize="70" 
                                BackgroundColor="Teal"
                        ItemsSource="{Binding OrderItemCollection}">
                <listView:SfListView.ItemTemplate>
                    <DataTemplate>
                        <ViewCell>
                            <ViewCell.View>
                                <Grid>
                                    <Grid.RowDefinitions>
                                        <RowDefinition Height="*" />
                                        <RowDefinition Height="1" />
                                    </Grid.RowDefinitions>
                                    <Grid>
                                        <Grid.ColumnDefinitions>
                                            <ColumnDefinition Width="*" />
                                            <ColumnDefinition Width="*" />
                                        </Grid.ColumnDefinitions>
                                        <Label LineBreakMode="NoWrap" Text="{Binding ID}" VerticalOptions="Center"/>
                                        <Label LineBreakMode="NoWrap" Text="{Binding Name}" Grid.Column="1" VerticalOptions="Center" />
                                    </Grid>
                                    <StackLayout Grid.Row="1" BackgroundColor="Gray" HeightRequest="1"/>
                                </Grid>
                            </ViewCell.View>
                        </ViewCell>
                    </DataTemplate>
                </listView:SfListView.ItemTemplate>
            </listView:SfListView>
        </Grid>
    </ContentPage.Content>
</ContentPage>
```
To know more about working with ListView, please refer our documentation [here](https://help.syncfusion.com/xamarin/sflistview/working-with-sflistview).