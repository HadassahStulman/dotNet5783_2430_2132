
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
        public BO.Cart cart=new BO.Cart(); // building customers cart
        public NewOrderWindow()
        {
                InitializeComponent();
                this.DataContext = cart;
        }

        /// <summary>
        /// save customers details and open catalog
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShopButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                #region input check
                if (!new EmailAddressAttribute().IsValid(cart.CustomerEmail))
                    throw new BO.IlegalDataException("Ilegal Email");
                if (string.IsNullOrEmpty(cart.CustomerName))
                    throw new BO.IlegalDataException("Ilegal Name");
                if (string.IsNullOrEmpty(cart.CustomerAddress))
                    throw new BO.IlegalDataException("Ilegal Address");
                #endregion
                new CatalogWindow(cart).Show();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Exception Thrown");
            }
        }
    }
}
