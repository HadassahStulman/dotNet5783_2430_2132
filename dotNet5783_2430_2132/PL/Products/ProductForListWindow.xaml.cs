
using BO;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;


namespace PL.Products;


/// <summary>
/// Interaction logic for ProductForListWindow.xaml
/// </summary>
public partial class ProductForListWindow : Window
{
    private BlApi.IBl bl = BlApi.Factory.Get();
    private ObservableCollection</*IGrouping<BO.Enums.Category?, ProductForList?>*/ProductForList?> myProductCollection;
    public enum Category
    {
        TextBooks, // school and study books
        CookBooks, // recipes
        ToddlerBooks, // children and first reading books
        ReligiousBooks,  // jewish textbooks
        ReadingBooks, // different genre of books for pleasure (novels, fantacy...)
        All // for getting all categories
    };

    /// <summary>
    /// ctor of ProductForListWindow and setting the Sources with data from the other layers
    /// </summary>
    public ProductForListWindow()
    {
        InitializeComponent();
        myProductCollection = new (bl.Product./*GetGroupedList()*/GetAll());
        this.DataContext = myProductCollection;
        CategorySelector.ItemsSource = Enum.GetValues(typeof(Category));
    }

    /// <summary>
    /// the SelectionChanged event shows on display all the products according to the selection you clicked on
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <exception cref="NullReferenceException"></exception>
    private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        string choice = CategorySelector.SelectedItem.ToString() ?? throw new NullReferenceException();
        ProductListView.ItemsSource = bl.Product.GetAll(product => choice == Category.All.ToString() ? true : product?.Category.ToString() == choice);
    }

    /// <summary>
    /// the click event moves to the ProductWindo display where he can enter the details of the new product to add 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void GoToAddProductButton_Click(object sender, RoutedEventArgs e)
    {
        new ProductWindow("Add", 0).ShowDialog();
        CategorySelector.Text = Category.All.ToString();
    }

    /// <summary>
    /// the DoubleClick (on a specific product) event moves to the ProductWindo display where he can change the details of the product to update
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void productDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        try
        {
            BO.ProductForList p = (ProductListView.SelectedItem as BO.ProductForList) ?? throw new NullReferenceException();
            new ProductWindow("View", p.ID).ShowDialog();
            CategorySelector.Text = Category.All.ToString();
        }
        catch (Exception)
        {
            MessageBox.Show("please choose a product");
        }
    }

}


