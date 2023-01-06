
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

        /// <summary>
        /// single product item display
        /// </summary>
        /// <param name="cart"></param>
        /// <param name="pID"></param>
        /// <param name="sender"></param>
        public ProductItemWindow( BO.Cart cart, int pID, string sender)
        {
            try
            {
                InitializeComponent();
                BO.ProductItem BProduct = bl.Product.GetByID(pID, cart);
                // create pl object for display
                this.myProduct = new PL.Products.ProductItem 
                {
                    ID = pID,
                    Name = BProduct.Name!,
                    Category = (PL.Products.Enums.Category)BProduct.Category!,
                    Amount = BProduct.Amount,
                    Price = BProduct.Price,
                    InStock = BProduct.InStock
                };
                this.cart = cart;
                this.DataContext = myProduct; // binding
                
                // customer open window from catalog in order to add item to cart  
                if (sender == "ADD")
                {
                    AddItemButton.Visibility = Visibility.Visible;
                    AddItemButton.IsEnabled = true;
                }
                else  // customer open window from cart in order to update item in cart
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


        /// <summary>
        /// reduce amount of product
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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


        /// <summary>
        /// add to amount
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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


        /// <summary>
        /// add the product to cart
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddItemButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (myProduct.Amount == 0)
                    throw new BO.IlegalDataException("Please Choose Amount To Add To Cart");
                if (cart.Items?.FirstOrDefault(pItem => pItem?.ProductID == myProduct.ID) == null) // if item does not exist i cart
                    bl.Cart.AddToCart(cart, myProduct.ID); // add one product to cart
                if (myProduct.Amount > 1) // if amount is bigger than 1
                    bl.Cart.UpdateAmountInCart(cart, myProduct.ID, myProduct.Amount); //update amount n cart
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Exception Thrown");
            }
        }

        /// <summary>
        /// update amount of product in cart
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateItemButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BO.OrderItem item = cart.Items?.ToList().Find(pItem => pItem?.ProductID == myProduct.ID)!;
                bl.Cart.UpdateAmountInCart(cart, myProduct.ID, myProduct.Amount);
                Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString(), "Exception Thrown"); }
        }
    }
}
