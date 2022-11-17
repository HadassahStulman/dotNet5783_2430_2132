

namespace BO;


/// <summary>
/// exception for failing to get object from data source
/// due to ilegal input or not existing object
/// </summary>
[Serializable]
public class FailedGettingObjectException : Exception
{
    /// <summary>
    /// constructor for exception, with uninqe message and inner exception
    /// </summary>
    /// <param name="inner"></param>
    public FailedGettingObjectException(Exception inner) : base("Failed To Find This Object", inner) { }

    /// <summary>
    /// convert description of exception to string
    /// </summary>
    /// <returns>string</returns>
    public override string ToString() => $"FailedGettingObjectException: {Message} - {this.InnerException}";
}


/// <summary>
/// exception for failing to add object to data source or to cart
/// due to ilegal input, already existing in data source, not existing object (for cart) or out of stock
/// </summary>
[Serializable]
public class FailedAddingObjectException : Exception
{
    /// <summary>
    /// constructor for exception, with uninqe message and inner exception
    /// </summary>
    /// <param name="inner"></param>
    public FailedAddingObjectException(Exception inner) : base("Failed To Add This Object", inner) { }

    /// <summary>
    /// convert description of exception to string
    /// </summary>
    /// <returns>string</returns>
    public override string ToString() => $"FailedToAddObjectException: {Message} - {this.InnerException}";
}



/// <summary>
/// exception for failing to delete object from data source
/// due to ilegal input, not existing object or ordered product
/// </summary>
[Serializable]
public class FailedToDeleteObjectException : Exception
{
    /// <summary>
    /// constructor for exception, with uninqe message and inner exception
    /// </summary>
    /// <param name="inner"></param>
    public FailedToDeleteObjectException(Exception inner) : base("Failed To Delete This Object", inner) { }

    /// <summary>
    /// convert description of exception to string
    /// </summary>
    /// <returns>string</returns>
    public override string ToString() => $"FailedToDeleteObjectException: {Message} - {this.InnerException}";
}



/// <summary>
/// exception for failing to update object in data source
/// due to ilegal input, not existing object, contradiction status or out of stock
/// </summary>
[Serializable]
public class FailedUpdatingObjectException : Exception
{
    /// <summary>
    /// constructor for exception, with uninqe message and inner exception
    /// </summary>
    /// <param name="inner"></param>
    public FailedUpdatingObjectException(Exception inner) : base("Failed To Update This Object", inner) { }

    /// <summary>
    /// convert description of exception to string
    /// </summary>
    /// <returns>string</returns>
    public override string ToString() => $"FailedToUpdateObjectException: {Message} - {this.InnerException}";
}





/// <summary>
/// exception for failing to update shipping or delivery status of order
/// due to ilegal input, not existing order
/// </summary>
[Serializable]
public class FailedToTrackOrderException : Exception
{
    /// <summary>
    /// constructor for exception, with uninqe message and inner exception
    /// </summary>
    /// <param name="inner"></param>
    public FailedToTrackOrderException(Exception inner) : base("Failed To Track Order Status", inner) { }

    /// <summary>
    /// convert description of exception to string
    /// </summary>
    /// <returns>string</returns>
    public override string ToString() => $"FailedToTrackOrderException: {Message} - {this.InnerException}";
}



/// <summary>
/// inner exception for getting ilegal input
/// </summary>
[Serializable]
public class IlegalDataException : Exception
{
    // uniqe message for the specific exception
    public IlegalDataException(string message) : base($"Ilegal {message}") { }

    /// <summary>
    /// convert description of exception to string
    /// </summary>
    /// <returns>string</returns>
    public override string ToString() => $"IlegalDataException: {Message}";

}



/// <summary>
/// inner exception for product is out of stock
/// </summary>
[Serializable]
public class OutOfStockException : Exception
{
    // uniqe message for the specific exception
    public override string Message => "Product Is Out Of Stock";

    /// <summary>
    /// convert description of exception to string
    /// </summary>
    /// <returns>string</returns>
    public override string ToString() => $"OutOfStockException: {Message}";

}



/// <summary>
/// inner exception for updating order after it was delivered or shipped
/// </summary>
[Serializable]
public class ConflictingStatusException : Exception
{
    // constructor with uniqe message for the specific exception
    public ConflictingStatusException(string message) : base(message) { }

    /// <summary>
    /// convert description of exception to string
    /// </summary>
    /// <returns>string</returns>
    public override string ToString() => $"ConflictingStatusException: {Message}";
}



/// <summary>
/// inner exception for deleting product that someone ordered
/// </summary>
[Serializable]
public class ProductIsOrderedException : Exception
{
    // uniqe message for the specific exception
    public override string Message => "Product Is Ordered";

    /// <summary>
    /// convert description of exception to string
    /// </summary>
    /// <returns>string</returns>
    public override string ToString() => $"ProductIsOrderedException: {Message}";
}
