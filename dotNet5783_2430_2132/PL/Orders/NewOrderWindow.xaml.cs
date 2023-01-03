
using PL.Cart;
using System;
using System.Windows;
using System.ComponentModel.DataAnnotations;


namespace PL.Orders
{
    /// <summary>
    /// Interaction logic for NewOrderWindow.xaml
    /// </summary>
    public partial class NewOrderWindow : Window
    {
        public BO.Cart cart=new BO.Cart();
        public NewOrderWindow()
        {
                InitializeComponent();
                this.DataContext = cart;
        }

        private void ShopButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!new EmailAddressAttribute().IsValid(cart.CustomerEmail))
                    throw new BO.IlegalDataException("Ilegal Email");
                if (string.IsNullOrEmpty(cart.CustomerName))
                    throw new BO.IlegalDataException("Ilegal Name");
                if (string.IsNullOrEmpty(cart.CustomerAddress))
                    throw new BO.IlegalDataException("Ilegal Address");
                new CatalogWindow(ref cart).Show();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Exception Thrown");
            }
        }
    }
}
