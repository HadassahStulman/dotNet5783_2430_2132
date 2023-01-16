using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;


namespace PL;

/// <summary>
/// Interaction logic for TrackOrderDiplayWindow.xaml
/// </summary>
public partial class TrackOrderDiplayWindow : Window
{
    /// <summary>
    /// private feiled for accessing to bl methods
    /// </summary>
    private readonly BlApi.IBl bl = BlApi.Factory.Get();
    /// <summary>
    /// constructer
    /// </summary>
    public TrackOrderDiplayWindow()
    {
        InitializeComponent();
    }

    /// <summary>
    /// the event summons a new window with tracking order details
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void TrackOrder_Butten_Click(object sender, RoutedEventArgs e)
    {

        try
        {
            if (!int.TryParse(ID_TextBox.Text, out int id)) // convet id fron string to int
                id = 0;
            bl.Order.GetByID(id);
            new Orders.TrackOrderWindow(id).ShowDialog();
        }
        catch (Exception ex) // if id is illigal then throw
        {
            MessageBox.Show(ex.ToString());
        }
 
    }

    /// <summary>
    /// opening a window with details of a order
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ViewOrder_Button_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            if (!int.TryParse(ID_TextBox.Text, out int id)) // convet id fron string to int
                id = 0;
            bl.Order.GetByID(id);
            new Orders.OrderWindow(id, "customer").ShowDialog();

        }
        catch (Exception ex) // if id is illigal then throw
        {
            MessageBox.Show(ex.ToString());
        }
    }

    private void Back_button_Click(object sender, RoutedEventArgs e) => Close();

};

