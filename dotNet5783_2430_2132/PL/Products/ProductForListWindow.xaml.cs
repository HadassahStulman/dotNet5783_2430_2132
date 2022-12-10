using BlApi;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PL.Products;
 

/// <summary>
/// Interaction logic for ProductForListWindow.xaml
/// </summary>
public partial class ProductForListWindow : Window
{
    private IBl bl = new BlImplementation.Bl();
    public enum Category
    {
        TextBooks, // school and study books
        CookBooks, // recipes
        ToddlerBooks, // children and first reading books
        ReligiousBooks,  // jewish textbooks
        ReadingBooks, // different genre of books for pleasure (novels, fantacy...)
        All // for getting all categories
    };
    public ProductForListWindow()
    {
        InitializeComponent();
        ProductListView.ItemsSource = bl.Product.GetAll();
        CategorySelector.ItemsSource = Enum.GetValues(typeof(Category));

    }

    private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        string choice = CategorySelector.SelectedItem.ToString() ?? throw new NullReferenceException();
        ProductListView.ItemsSource = bl.Product.GetAll(product => choice == Category.All.ToString() ? true : product?.Category.ToString() == choice);
        List<Category>lst = ((IEnumerable<Category>)Enum.GetValues(typeof(Category))).Where(item => item.ToString() != choice).ToList();
        CategorySelector.ItemsSource = lst;
    }

    private void GoToAddProductButton_Click(object sender, RoutedEventArgs e) => new ProductWindow("Add", 0).Show();

    private void productDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        try
        {
            BO.ProductForList p = (ProductListView.SelectedItem as BO.ProductForList) ?? throw new NullReferenceException();
            new ProductWindow("Update", p.ID).Show();
        }
        catch(Exception)
        {
            MessageBox.Show("please choose a product");
        }
    }

    private void ProductListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

    }
}
