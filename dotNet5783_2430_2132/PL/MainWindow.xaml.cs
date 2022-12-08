﻿using PL.Products;
using System.Windows;

namespace PL;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void ShowProductListButton_Click(object sender, RoutedEventArgs e) => new ProductForListWindow().Show();

}
