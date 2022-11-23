
namespace BO;

public struct Enums
{
    public enum Category
    {
        TextBooks, // school and study books
        CookBooks, // recipes
        ToddlerBooks, // children and first reading books
        ReligiousBooks,  // jewish textbooks
        ReadingBooks // different genre of books for pleasure (novels, fantacy...)
    }; 
    public enum OrderStatus
    {
        OrderConfirmed, // order is confirmed or payed
        OrderShipped,  // order was shiped
        OrderDelivered // order was delivered
    }
}
