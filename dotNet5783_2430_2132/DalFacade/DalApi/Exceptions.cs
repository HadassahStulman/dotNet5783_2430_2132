using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi;

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
    public override string ToString() =>$" {Message}";

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
