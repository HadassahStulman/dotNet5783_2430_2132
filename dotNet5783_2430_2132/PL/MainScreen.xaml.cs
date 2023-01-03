
using System.Windows;
using System.Windows.Controls;
using PL.Orders;


namespace PL;

/// <summary>
/// Interaction logic for MainScreen.xaml
/// </summary>
public partial class MainScreen : Window
{
    public MainScreen()
    {
        InitializeComponent();
    }

    private void ManagerDisplayButton_Click(object sender, RoutedEventArgs e) => new ManagerDisplayWindow().ShowDialog();

    private void NewOrderButton_Click(object sender, RoutedEventArgs e) => new NewOrderWindow().ShowDialog();

    private void TrackOrderButton_Click(object sender, RoutedEventArgs e) => new TrackOrderWindow().ShowDialog();
}
