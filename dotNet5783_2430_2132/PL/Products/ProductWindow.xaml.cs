
using System;
using System.Windows;


namespace PL.Products;

/// <summary>
/// Interaction logic for ProductWindow.xaml
/// </summary>
public partial class ProductWindow : Window
{
    /// <summary>
    /// private feiled for accessing to bl methods
    /// </summary>
    private readonly BlApi.IBl bl = BlApi.Factory.Get();
    BO.Product? myProduct=null;
    /// <summary>
    /// constructor fo product window
    /// </summary>
    /// <param name="source"></param>
    /// <param name="id"></param>
    public ProductWindow(string source, int id)
    {
        InitializeComponent();
        this.DataContext = myProduct;
        CategoryCombobox.ItemsSource = Enum.GetValues(typeof(BO.Enums.Category)); // initialize categry combobox
        if (source == "Add") // if window was opened for adding
        {
            AddProductButton.IsEnabled = true;
            AddProductButton.Visibility = Visibility.Visible; // show add button
            IDTextBox.IsReadOnly = false;
        }
        else  // if window was opeaned for viewing
        {
            myProduct = bl.Product.GetByID(id);
            UpdateOptionButton.Visibility = Visibility.Visible;
            UpdateOptionButton.IsEnabled = true;

            NameTextBox.IsReadOnly = true;
            PriceTextBox.IsReadOnly = true;
            InStockTextBox.IsReadOnly = true;
            CategoryCombobox.IsEnabled = false;
        }
    }


    /// <summary>
    /// event handler for clicking on add button
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void AddProductButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            //if (!int.TryParse(IDTextBox.Text, out int id)) // convet id fron string to int
            //    id = 0;
            //if (!double.TryParse(PriceTextBox.Text, out double price)) // convert string to double
            //    price = 0;
            //if (!int.TryParse(InStockTextBox.Text, out int amount)) // convert string to int
            //    amount = -1;
            //string? cat = CategoryCombobox.Text;
            //if (string.IsNullOrEmpty(cat)) // if no category was chosen
            //    throw new BO.FailedAddingObjectException(new BO.IlegalDataException("Ilegal Category"));
            bl.Product.AddProduct(myProduct!);
            MessageBox.Show("Successfully added");
            Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.ToString(), "Exception Thrown");
        }
    }


    /// <summary>
    /// event handler for clicking on update button
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void UpdateProductButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            if (!double.TryParse(PriceTextBox.Text, out double price)) // convert string to double
                price = 0;
            if (!int.TryParse(InStockTextBox.Text, out int amount)) // convert string to int
                amount = -1;
            string? cat = CategoryCombobox.Text;
            if (string.IsNullOrEmpty(cat))
                throw new BO.FailedAddingObjectException(new BO.IlegalDataException("Ilegal Category"));
            bl.Product.UpdateProduct(myProduct!);
            MessageBox.Show("Successfully updated");
            Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.ToString(), "Exception Thrown");
        }
    }

    private void UpdateOptionButton_Click(object sender, RoutedEventArgs e)
    {
        UpdateOptionButton.Visibility = Visibility.Hidden;
        UpdateOptionButton.IsEnabled = false;
        UpdateProductButton.IsEnabled = true;
        UpdateProductButton.Visibility = Visibility.Visible;

        NameTextBox.IsReadOnly = false;
        PriceTextBox.IsReadOnly = false;
        InStockTextBox.IsReadOnly = false;
        CategoryCombobox.IsEnabled = true;
    }
}
