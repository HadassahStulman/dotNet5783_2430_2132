
using BO;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Controls;


namespace PL.Products;


/// <summary>
/// Interaction logic for ProductForListWindow.xaml
/// </summary>
public partial class ProductForListWindow : Window
{
    private static BlApi.IBl bl = BlApi.Factory.Get();
    private CollectionView? view;
    public ObservableCollection<BO.ProductForList?> myProductCollection { get; set; }

    /// <summary>
    /// constructor of ProductForListWindow and setting the Sources with data from the other layers
    /// </summary>
    public ProductForListWindow()
    {
        InitializeComponent();
        myProductCollection = new ObservableCollection<BO.ProductForList?>(bl.Product.GetAll());
        this.DataContext = myProductCollection;
        CategorySelector.ItemsSource = Enum.GetValues(typeof(Enums.Category));
    }

    /// <summary>
    /// adds new product to observable collection
    /// </summary>
    /// <param name="productToAdd"></param>
    private void addProduct(BO.ProductForList productToAdd, int id = 0) => myProductCollection.Add(productToAdd);

    /// <summary>
    /// updates product in observable collection
    /// </summary>
    /// <param name="productToUpdate"></param>
    private void updateProduct(BO.ProductForList productToUpdate, int prevID)
    {

        var prevProduct = myProductCollection.FirstOrDefault(product => product?.ID == prevID);
        int index = myProductCollection.IndexOf(prevProduct);
        if (productToUpdate == null)
            myProductCollection.Remove(prevProduct);
        else
            myProductCollection[index] = productToUpdate;
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
        myProductCollection = new(/*from product in*/ bl.Product.GetAll(product => choice == Enums.Category.All.ToString() ? true : product?.Category.ToString() == choice));
        //where choice == Enums.Category.All.ToString() ? true : product?.Category.ToString() == choice
        //select product);
        this.DataContext = myProductCollection;
        this.view = null;
    }

    /// <summary>
    /// the click event moves to the ProductWindo display where he can enter the details of the new product to add 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void GoToAddProductButton_Click(object sender, RoutedEventArgs e) => new ProductWindow(addProduct).ShowDialog();

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
            new ProductWindow(updateProduct, p).ShowDialog();
        }
        catch (Exception)
        {
            MessageBox.Show("please choose a product");
        }
    }

    private void GroupByCatButton_Click(object sender, RoutedEventArgs e)
    {
        if (view == null)
        {
            view = (CollectionView)CollectionViewSource.GetDefaultView(myProductCollection);
            PropertyGroupDescription groupDescription = new PropertyGroupDescription("Category");
            view?.GroupDescriptions.Add(groupDescription);
        }
    }
}


