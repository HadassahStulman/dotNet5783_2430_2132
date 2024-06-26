﻿

namespace DO;
/// <summary>
/// structure for an order of books
/// </summary>
public struct Order
{
    /// <summary>
    /// uniqe ID for each order
    /// </summary>
    public int ID { get; set; }
    /// <summary>
    /// customer's name
    /// </summary>
    public string? CustomerName { get; set; }
    /// <summary>
    /// customer's Email
    /// </summary>
    public string? CustomerEmail { get; set; }
    /// <summary>
    /// customer's address for delivery
    /// </summary>
    public string? CustomerAddress { get; set; }
    /// <summary>
    /// ordering date
    /// </summary>
    public DateTime? OrderDate { get; set; }
    /// <summary>
    /// shipping date
    /// </summary>
    public DateTime? ShipDate { get; set; }
    /// <summary>
    /// date of arrival at the customer's house
    /// </summary>
    public DateTime? DeliveryDate { get; set; }

    /// <summary>
    /// returns description of order
    /// </summary>
    /// <returns>string</returns>
    public override string ToString() => this.ToStringProperty();

}
