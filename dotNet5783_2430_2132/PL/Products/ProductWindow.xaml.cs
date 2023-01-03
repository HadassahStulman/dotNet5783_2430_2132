
using System;
using System.Windows;
using System.Windows.Controls;


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
    private Action<BO.ProductForList, int> action;
    public BO.Product? myProduct = new BO.Product();

    public ProductWindow()
    {
        InitializeComponent();
        CategoryCombobox.ItemsSource = Enum.GetValues(typeof(BO.Enums.Category)); // initialize categry combobox
    }

    /// <summary>
    /// constructoe for add window
    /// </summary>
    /// <param name="function"></param>
    public ProductWindow(Action<BO.ProductForList, int> addToObservableCollection) : this()
    {
        this.DataContext = myProduct;
        action = addToObservableCollection;
        AddProductButton.IsEnabled = true;
        AddProductButton.Visibility = Visibility.Visible; // show add button
        IDTextBox.IsReadOnly = false;
    }

    /// <summary>
    /// constructor for update window
    /// </summary>
    /// <param name="function"></param>
    /// <param name="id"></param>
    public ProductWindow(Action<BO.ProductForList, int> updateObservableCollection, BO.ProductForList product) : this()
    {
        action = updateObservableCollection;
        myProduct = bl.Product.GetByID(product.ID);
        this.DataContext = myProduct;
        UpdateOptionButton.Visibility = Visibility.Visible;
        UpdateOptionButton.IsEnabled = true;
        DeleteOptionButton.Visibility = Visibility.Visible;
        DeleteOptionButton.IsEnabled = true;

        NameTextBox.IsReadOnly = true;
        PriceTextBox.IsReadOnly = true;
        InStockTextBox.IsReadOnly = true;
        CategoryCombobox.IsEnabled = false;
    }

    /// <summary>
    /// constructor fo product window
    /// </summary>
    /// <param name="source"></param>
    /// <param name="id"></param>
    //public ProductWindow(string source, Action<BO.ProductForList> function, int id = 0)
    //{
    //    if (source == "Add") // if window was opened for adding
    //    {
    //        AddProductButton.IsEnabled = true;
    //        AddProductButton.Visibility = Visibility.Visible; // show add button
    //        IDTextBox.IsReadOnly = false;
    //    }
    //    else  // if window was opeaned for viewing
    //    {
    //        myProduct = bl.Product.GetByID(id);
    //        UpdateOptionButton.Visibility = Visibility.Visible;
    //        UpdateOptionButton.IsEnabled = true;

    //        NameTextBox.IsReadOnly = true;
    //        PriceTextBox.IsReadOnly = true;
    //        InStockTextBox.IsReadOnly = true;
    //        CategoryCombobox.IsEnabled = false;
    //    }


    //}


    private void checkInput()
    {
        int checkInt;
        double checkDouble;
        if (!int.TryParse(IDTextBox.Text, out checkInt)) // convet id fron string to int
            throw new BO.IlegalDataException("ilegal ID");
        if (!double.TryParse(PriceTextBox.Text, out checkDouble)) // convert string to double
            throw new BO.IlegalDataException("ilegal price");
        if (!int.TryParse(InStockTextBox.Text, out checkInt)) // convert string to int
            throw new BO.IlegalDataException("ilegal amount");
        if (string.IsNullOrEmpty(CategoryCombobox.Text)) // if no category was chosen
            throw new BO.IlegalDataException("Ilegal Category");
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
            checkInput();
            bl.Product.AddProduct(myProduct!);
            action(new BO.ProductForList()
            {
                ID = myProduct!.ID,
                Name = myProduct.Name!,
                Category = myProduct.Category,
                Price = myProduct.Price
            },0);
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
            checkInput();
            bl.Product.UpdateProduct(myProduct!);
            action(new BO.ProductForList()
            {
                ID = myProduct!.ID,
                Name = myProduct.Name!,
                Category = myProduct.Category,
                Price = myProduct.Price
            }, myProduct.ID);
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

    private void DeleteOptionButton_Click(object sender, RoutedEventArgs e)
    {
        bl.Product.DeleteProduct(myProduct!.ID);
        MessageBox.Show("Successfully Deleted");
        action(null, myProduct.ID);
        Close();
    }
}
