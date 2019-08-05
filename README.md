# Loading data from SQLite online database

The SfListView allows binding the data from online database with the help of `Azure App Service`. To perform this, follow the steps:

Step 1: Get URL to store the data online.
Step 2: Create table using AppServiceHelpers.
Step 3: Populate the data into the table using the ObservableCollection Items.
Step 4: Bind it to SfListView using SfListView.ItemSource property.
Step 5: Refer to the following link to know more about working with Azure App Service.
[https://blog.xamarin.com/add-a-backend-to-your-app-in-10-minutes/](https://blog.xamarin.com/add-a-backend-to-your-app-in-10-minutes/)

`Note` 

* Microsoft.Azure.Mobile.Client(v.2.1.1)
* Microsoft.Azure.Mobile.Client.SQLiteStore(v.2.1.1)
* AppService.Helpers (Does not support UWP platform)
* AppService.Helpers.Forms (Does not support UWP platform)

Refer to the following code which illustrates, how to initialize the library with the URL of the Azure Mobile App and registering the Model with the client to create a table.

```
public App()
{
    InitializeComponent();
    IEasyMobileServiceClient client = new EasyMobileServiceClient();
    client.Initialize("http://xamarin-todo-sample.azurewebsites.net");
    client.RegisterTable<ToDo>();
    client.FinalizeSchema();
    MainPage = new NavigationPage(new Pages.ToDoListPage(client));
}
```
Refer to the following code which illustrates, how to create a table using AppServiceHelpers and insert items in it.

```
using AppServiceHelpers.Abstractions;
using AppServiceHelpers.Models;
public class BaseAzureViewModel<T> : INotifyPropertyChanged where T : EntityData
{
    IEasyMobileServiceClient client;
    ITableDataStore<T> table;

    public BaseAzureViewModel(IEasyMobileServiceClient client)
    {
        this.client = client;
        table = client.Table<T>();
    }

    // Returns an ObservableCollection of all the items in the table
    ObservableCollection<T> items = new ObservableCollection<T>();
    public ObservableCollection<T> Items
    {
        get { return items; }
        set
        {
            items = value;
            OnPropertyChanged("items");
        }
    }

    // Adds an item to the table.
    public async Task AddItemAsync(T item)
    {
        await table.AddAsync(item);
    }

    // Deletes an item from the table.
    public async Task DeleteItemAsync(T item)
    {
        await table.DeleteAsync(item);
    }

    // Updates an item in the table.
    public async Task UpdateItemAsync(T item)
    {
        await table.UpdateAsync(item);
    }

    // Refresh the table and synchronize data with Azure.
    Command refreshCommand;
    public Command RefreshCommand
    {
        get { return refreshCommand ?? (refreshCommand = new Command(async () => await ExecuteRefreshCommand())); }
    }

    async Task ExecuteRefreshCommand()
    {
        if (IsBusy)
            return;

        IsBusy = true;

        try
        {
            var _items = await table.GetItemsAsync();
            Items.Clear();
            foreach (var item in _items)
            {
                Items.Add(item);
            }
            IsBusy = false;
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    public void OnPropertyChanged(string propertyName)
    {
        if (PropertyChanged == null)
            return;

        PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
    }
}
```
Refer to the following code which illustrates, how to bind the table contents into the SfListView.

```
<ContentPage xmlns:syncfusion="clr-namespace:Syncfusion.ListView.XForms;assembly=Syncfusion.SfListView.XForms">
    <ContentPage.Content>
        <StackLayout>
            <syncfusion:SfListView x:Name="listView" SelectedItem="{Binding SelectedToDoItem, Mode=TwoWay}" ItemSize="50">
                <syncfusion:SfListView.ItemTemplate>
                    <DataTemplate>
                        <ViewCell>
                            <StackLayout Orientation="Horizontal" HorizontalOptions="FillAndExpand" VerticalOptions="CenterAndExpand">
                                <Label Text="{Binding Text}" />
                                <Switch IsToggled="{Binding Completed}"/>
                            </StackLayout>
                        </ViewCell>
                    </DataTemplate>
                </syncfusion:SfListView.ItemTemplate>
            </syncfusion:SfListView>
            <Button Text="Add New" Command="{Binding AddNewItemCommand}"/>
            <Button Text="Fetch" Command="{Binding FetchItemCommand}" />
        </StackLayout>
    </ContentPage.Content>
</ContentPage>
```

```
public partial class ToDoListPage : ContentPage
{
  public ToDoListPage(IEasyMobileServiceClient client)
  {
    InitializeComponent();
    var viewModel = new ViewModels.ToDosViewModel(client);
    BindingContext = viewModel;
    listView.ItemsSource = viewModel.Items;
  }
  
  private void FetchButton_Clicked(object sender, EventArgs e)
  {
    var viewModel1 = (ToDosViewModel)BindingContext;
    viewModel1.RefreshCommand.Execute(null);
  }
}
```
To know more about Xamarin.Forms ListView, please refer our documentation [here](https://help.syncfusion.com/xamarin/sflistview/working-with-sflistview)

