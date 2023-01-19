
using BlApi;
using BO;
using DO;

namespace BlImplementation;

internal class Product : IProduct
{
    /// <summary>
    /// private field for allowing accsess from BL to Dal
    /// </summary>
    private DalApi.IDal Dal = DalApi.Factory.Get()!;

    public void AddProduct(BO.Product Bproduct)
    {
        // checkig if all product's details are legal
        #region inputCheck
        if (Bproduct.ID < 100000)
            throw new BO.FailedAddingObjectException(new BO.IlegalDataException("Ilegal ID"));
        if (string.IsNullOrEmpty(Bproduct.Name))
            throw new BO.FailedAddingObjectException(new BO.IlegalDataException("Ilegal name"));
        if (Bproduct.Price <= 0)
            throw new BO.FailedAddingObjectException(new BO.IlegalDataException("Ilegal price"));
        if (Bproduct.InStock < 0)
            throw new BO.FailedAddingObjectException(new BO.IlegalDataException("Ilegal amount in stock"));
        if (Bproduct.Category != BO.Enums.Category.CookBooks && Bproduct.Category != BO.Enums.Category.TextBooks && Bproduct.Category != BO.Enums.Category.ReligiousBooks && Bproduct.Category != BO.Enums.Category.ReadingBooks && Bproduct.Category != BO.Enums.Category.ReligiousBooks && Bproduct.Category != BO.Enums.Category.ToddlerBooks)
            throw new BO.FailedAddingObjectException(new BO.IlegalDataException("Ilgal category"));
        #endregion
        try
        {
            DO.Product Dproduct = new DO.Product();
            Dproduct = DO.ExtentionMethods.ConvertTo(Bproduct, Dproduct);
            Dproduct.Category = (DO.Enums.Category)Bproduct.Category;
            Dal.Product.Add(Dproduct);
        }
        catch (Exception Ex)
        {
            throw new BO.FailedAddingObjectException(Ex);
        }
    }

    public void DeleteProduct(int pID)
    {
        try
        {
            if (pID < 100000) // product ID does not have at least 6 dgits or ID is negative
                throw new BO.IlegalDataException("Ilegal ID");
            IEnumerable<DO.Order?> Orderlst = Dal.Order.GetList();
            foreach (DO.Order? order in Orderlst) // check if product is ordered
            {
                var lstOi = Dal.OrderItem.GetGrouped();
                var lstOiinOrder = lstOi.Where(group => group.Key == order?.ID).FirstOrDefault();
                if (lstOiinOrder!.Where(Oitem => Oitem?.ProductId == pID).FirstOrDefault() != null && order?.ShipDate == null) // if product is ordered and order was not shiped yet
                    throw new BO.ProductIsOrderedException();
            }
            Dal.Product.Delete(pID);
        }
        catch (Exception Ex) // if dal layer threw an exception (the product doesn't exist)
        {
            throw new BO.FailedToDeleteObjectException(Ex);
        }

    }

    public IEnumerable<BO.ProductForList?> GetAll(Func<BO.ProductForList?, bool>? condition)
    {
        try
        {
            IEnumerable<DO.Product?> Plst = Dal.Product.GetList();
            var lst =
                from product in Dal.Product.GetList()
                where product != null
                let Bproduct = new BO.ProductForList()
                {
                    ID = product?.ID ?? 0,
                    Name = product?.Name ?? "",
                    Price = product?.Price ?? 0,
                    Category = (BO.Enums.Category)product?.Category!
                }
                where condition == null ? true : condition(Bproduct)
                orderby Bproduct.Name
                select Bproduct;
            return lst;
        }
        catch (Exception Ex) { throw new BO.FailedGettingObjectException(Ex); }
    }

    public BO.Product GetByID(int pID)
    {
        if (pID < 100000) // product ID does not have at least 6 dgits or ID is negative
            throw new BO.FailedGettingObjectException(new BO.IlegalDataException("Ilegal ID"));
        try
        {
            DO.Product? dproduct = Dal.Product.GetIf(item => item?.ID == pID);
            BO.Product bproduct = DO.ExtentionMethods.ConvertTo(dproduct, new BO.Product())!;
            bproduct!.Category = (BO.Enums.Category)dproduct?.Category!;
            return bproduct;
        }
        catch (Exception Ex)
        {
            throw new BO.FailedGettingObjectException(Ex);
        }
    }

    public BO.ProductItem GetByID(int pID, BO.Cart crt)
    {
        if (pID < 100000) // product ID does not have at least 6 dgits or ID is negative
            throw new BO.FailedGettingObjectException(new BO.IlegalDataException("Ilegal ID"));
        try
        {
            DO.Product? dproduct = Dal.Product.GetIf(item => item?.ID == pID);
            bool inStock = false;
            if (dproduct?.InStock != 0)
                inStock = true;
            BO.ProductItem bproduct = DO.ExtentionMethods.ConvertTo(dproduct, new BO.ProductItem())!;
            bproduct.Category = (BO.Enums.Category)dproduct?.Category!;
            bproduct.InStock = inStock;
            bproduct.Amount = crt.Items!.ToList().Find(item => item?.ProductID == pID)?.Amount ?? 0;
            return bproduct;
        }
        catch (Exception Ex)
        {
            throw new BO.FailedGettingObjectException(Ex);
        }
    }

    public void UpdateProduct(BO.Product Bproduct)
    {
        #region inputCheck
        // checkig if all product's details are legal
        if (Bproduct.ID < 100000)
            throw new BO.FailedAddingObjectException(new BO.IlegalDataException("Ilegal ID"));
        if (string.IsNullOrEmpty(Bproduct.Name))
            throw new BO.FailedAddingObjectException(new BO.IlegalDataException("Ilegal name"));
        if (Bproduct.Price <= 0)
            throw new BO.FailedAddingObjectException(new BO.IlegalDataException("Ilegal price"));
        if (Bproduct.InStock < 0)
            throw new BO.FailedAddingObjectException(new BO.IlegalDataException("Ilegal amount in stock"));
        if (Bproduct.Category != BO.Enums.Category.CookBooks && Bproduct.Category != BO.Enums.Category.TextBooks && Bproduct.Category != BO.Enums.Category.ReligiousBooks && Bproduct.Category != BO.Enums.Category.ReadingBooks && Bproduct.Category != BO.Enums.Category.ReligiousBooks && Bproduct.Category != BO.Enums.Category.ToddlerBooks)
            throw new BO.FailedAddingObjectException(new BO.IlegalDataException("Ilegal category"));
        #endregion 
        try
        {
            DO.Product Dproduct = DO.ExtentionMethods.ConvertTo(Bproduct, new DO.Product());
            Dproduct.Category = (DO.Enums.Category)Bproduct.Category;
            Dal.Product.Update(Dproduct);
        }
        catch (Exception Ex)
        {
            throw new BO.FailedUpdatingObjectException(Ex);
        }

    }
}
