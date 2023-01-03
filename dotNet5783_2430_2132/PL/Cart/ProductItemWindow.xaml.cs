
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Windows;

namespace PL.Cart
{
    /// <summary>
    /// Interaction logic for ProductItemWindow.xaml
    /// </summary>
    public partial class ProductItemWindow : Window
    {
        private BlApi.IBl bl = BlApi.Factory.Get();
        private PL.Products.ProductItem myProduct;
        private BO.Cart cart;
        public ProductItemWindow(ref BO.Cart cart, int pID, string sender)
        {
            try
            {
                InitializeComponent();
                BO.ProductItem BProduct = bl.Product.GetByID(pID, cart);
                this.myProduct = new PL.Products.ProductItem()
                {

                    ID = pID,
                    Name = BProduct.Name!,
                    Category = (PL.Products.Enums.Category)BProduct.Category!,
                    Amount = BProduct.Amount,
                    Price = BProduct.Price,
                    InStock = BProduct.InStock
                };
                this.cart = cart;
                this.DataContext = myProduct;
                if (sender == "ADD")
                {
                    AddItemButton.Visibility = Visibility.Visible;
                    AddItemButton.IsEnabled = true;
                }
                else
                {
                    UpdateItemButton.Visibility = Visibility.Visible;
                    UpdateItemButton.IsEnabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception Thrown", ex.ToString());
            }
        }

        private void minusButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (myProduct?.Amount > 0)
                    myProduct.Amount--;
                else throw new BO.IlegalDataException("Amount is Zero");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Exception Thrown");
            }
        }

        private void plusButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (myProduct.InStock)
                    myProduct.Amount++;
                else throw new BO.OutOfStockException();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Exception Trown");
                Close();
            }
        }

        private void AddItemButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (myProduct.Amount == 0)
                    throw new BO.IlegalDataException("Please Choose Amount To Add To Cart");
                if (cart.Items?.FirstOrDefault(pItem => pItem?.ProductID == myProduct.ID) == null)
                    bl.Cart.AddToCart(cart, myProduct.ID);
                if (myProduct.Amount > 1)
                    bl.Cart.UpdateAmountInCart(cart, myProduct.ID, myProduct.Amount);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Exception Thrown");
            }
        }

        private void UpdateItemButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BO.OrderItem item = cart.Items?.Find(pItem => pItem?.ProductID == myProduct.ID)!;
                //if (myProduct.Amount == 0)
                //    cart.Items?.Remove(item);
                bl.Cart.UpdateAmountInCart(cart, myProduct.ID, myProduct.Amount);
                Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString(), "Exception Thrown"); }
        }
    }
}
