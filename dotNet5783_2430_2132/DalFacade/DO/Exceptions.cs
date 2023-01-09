
namespace DO;

/// <summary>
/// Excepsion class for trying to delete, update or get an object thet doesn't exist
/// </summary>
[Serializable]
public class NotExistingException : Exception
{
    // uniqe message for the specific exception
    public override string Message => "This Object Does Not Exist";

    /// <summary>
    /// convert description of exception to string
    /// </summary>
    /// <returns>string</returns>
    public override string ToString() => $" {Message}";

}


/// <summary>
/// Excepsion class for trying to add an existing object
/// </summary>
[Serializable]
public class AlreadyExistingException : Exception
{
    // uniqe message for the specific exception
    public override string Message => "This Object Already Exists";

    /// <summary>
    /// convert description of exception to string
    /// </summary>
    /// <returns>string</returns>
    public override string ToString() => $" {Message}";

}


[Serializable]
public class DalConfigException : Exception
{
    public DalConfigException(string msg) : base(msg) { }
    public DalConfigException(string msg, Exception ex) : base(msg, ex) { }
}

[Serializable]
public class XMLFileLoadException : Exception
{
    /// <summary>
    /// constructor for exception, with uninqe message and inner exception
    /// </summary>
    /// <param name="inner"></param>
    public XMLFileLoadException(string msg, Exception inner) : base(msg,inner) { }
    /// <summary>
    /// convert description of exception to string
    /// </summary>
    /// <returns>string</returns>
    public override string ToString() => $"{Message} - {this.InnerException}";
}

