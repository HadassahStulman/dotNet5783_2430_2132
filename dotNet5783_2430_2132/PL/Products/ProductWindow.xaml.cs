
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

    /// <summary>
    /// constructor fo product window
    /// </summary>
    /// <param name="source"></param>
    /// <param name="id"></param>
    public ProductWindow(string source, int id)
    {
        InitializeComponent();
        CategoryCombobox.ItemsSource = Enum.GetValues(typeof(BO.Enums.Category)); // initialize categry combobox
        if (source == "Add") // if window was opened for adding
        {
            AddProductButton.IsEnabled = true;
            AddProductButton.Visibility = Visibility.Visible; // show add button
        }
        else  // if window was opeaned for updating
        {
            IDTextBox.IsReadOnly = true;
            BO.Product? product = bl.Product.GetByID(id); // get product to update fro data 
            // initialize all textboxes according to product to update values
            IDTextBox.Text = product?.ID.ToString();
            NameTextBox.Text = product?.Name!.ToString();
            PriceTextBox.Text = product?.Price.ToString();
            InStockTextBox.Text = product?.InStock.ToString();
            CategoryCombobox.Text = product?.Category.ToString();
            UpdateProductButton.IsEnabled = true;
            UpdateProductButton.Visibility = Visibility.Visible; // show update button
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
            if (!int.TryParse(IDTextBox.Text, out int id)) // convet id fron string to int
                id = 0;
            if (!double.TryParse(PriceTextBox.Text, out double price)) // convert string to double
                price = 0;
            if (!int.TryParse(InStockTextBox.Text, out int amount)) // convert string to int
                amount = -1;
            string? cat = CategoryCombobox.Text;
            if (string.IsNullOrEmpty(cat)) // if no category was chosen
                throw new BO.FailedAddingObjectException(new BO.IlegalDataException("Ilegal Category"));
                bl.Product.AddProduct(new BO.Product() 
                {
                    ID = id,
                    Category = (BO.Enums.Category)Enum.Parse(typeof(BO.Enums.Category), cat!),
                    Price = price,
                    Name = NameTextBox.Text,
                    InStock = amount
                }); // add new product to data
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
            if (!int.TryParse(IDTextBox.Text, out int id)) // convet id fron string to int
                id = 0;
            if (!double.TryParse(PriceTextBox.Text, out double price)) // convert string to double
                price = 0;
            if (!int.TryParse(InStockTextBox.Text, out int amount)) // convert string to int
                amount = -1;
            string? cat = CategoryCombobox.Text;
            if (string.IsNullOrEmpty(cat))
                throw new BO.FailedAddingObjectException(new BO.IlegalDataException("Ilegal Category"));
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
