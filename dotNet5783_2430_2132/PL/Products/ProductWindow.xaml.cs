
using BlApi;
using BO;
using System;
using System.Windows;
using System.Windows.Controls;


namespace PL.Products;

/// <summary>
/// Interaction logic for ProductWindow.xaml
/// </summary>
public partial class ProductWindow : Window
{
    private IBl bl = new BlImplementation.Bl();
    public ProductWindow(string source, int id)
    {
        InitializeComponent();
        CategoryCombobox.ItemsSource = Enum.GetValues(typeof(BO.Enums.Category));
        if (source == "Add")
        {
            AddProductButton.IsEnabled = true;
            AddProductButton.Visibility = Visibility.Visible;
        }
        else  // Update
        {
            BO.Product? product = bl.Product.GetByID(id);
            IDTextBox.Text = product?.ID.ToString();
            IDTextBox.IsReadOnly = true;
            NameTextBox.Text = product?.Name!.ToString();
            PriceTextBox.Text = product?.Price.ToString();
            InStockTextBox.Text = product?.InStock.ToString();
            CategoryCombobox.Text = product?.Category.ToString();
            UpdateProductButton.IsEnabled = true;
            UpdateProductButton.Visibility = Visibility.Visible;
        }
    }

    private void AddProductButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            if (!int.TryParse(IDTextBox.Text, out int id))
                id = 0;
            if (!double.TryParse(PriceTextBox.Text, out double price)) // convert string to double
                price = 0;
            if (!int.TryParse(InStockTextBox.Text, out int amount)) // convert string to int
                amount = -1;
            bl.Product.AddProduct(new BO.Product()
            {
                ID = id,
                Category = (BO.Enums.Category)Enum.Parse(typeof(BO.Enums.Category), CategoryCombobox.Text),
                Price = price,
                Name = NameTextBox.Text,
                InStock = amount
            });
            MessageBox.Show("Successfully added");
            Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.ToString(), "Exception Thrown");
        }
    }

    private void UpdateProductButton_Click(object sender, RoutedEventArgs e)
    {

        try
        {
            int.TryParse(IDTextBox.Text, out int id);
            if (!double.TryParse(PriceTextBox.Text, out double price)) // convert string to double
                price = 0;
            if (!int.TryParse(InStockTextBox.Text, out int amount)) // convert string to int
                amount = -1;
            bl.Product.UpdateProduct(new BO.Product()
            {
                ID = id,
                Category = (BO.Enums.Category)Enum.Parse(typeof(BO.Enums.Category), CategoryCombobox.Text),
                Price = price,
                Name = NameTextBox.Text,
                InStock = amount
            }); ;
            MessageBox.Show("Successfully updated");
            Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.ToString(), "Exception Thrown");
        }
    }
}
