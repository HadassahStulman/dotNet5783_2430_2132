using BlApi;
using System;
using System.Windows;
using System.Windows.Controls;



namespace PL.Products;

/// <summary>
/// Interaction logic for ProductForListWindow.xaml
/// </summary>
public partial class ProductForListWindow : Window
{
    private IBl bl = new BlImplementation.Bl();
    public ProductForListWindow()
    {
        InitializeComponent();
        ProductListView.ItemsSource = bl.Product.GetAll();
        CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.Category));
    }

    private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        BO.Enums.Category cat = (BO.Enums.Category)Enum.Parse(typeof(BO.Enums.Category), CategorySelector.SelectedItem.ToString() ?? throw new Exception());
        ProductListView.ItemsSource = bl.Product.GetAll(product => product?.Category == cat);
    }

    private void ProductListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        Console.WriteLine("hi");
    }

}
