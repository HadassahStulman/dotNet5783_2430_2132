
using BlApi;


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
            DO.Product Dproduct = new DO.Product()
            {
                ID = Bproduct.ID,
                Name = Bproduct.Name,
                Price = Bproduct.Price,
                Category = (DO.Enums.Category)Bproduct.Category,
                InStock = Bproduct.InStock
            };
            //DO.Product dp = new DO.Product();
            //dp = DO.ExtentionMethods.ConvertToBO(Bproduct, dp);
            Dal.Product.Add(Dproduct);
        }
        catch (Exception Ex)
        {
            throw new BO.FailedAddingObjectException(Ex);
        }
    }

    public void DeleteProduct(int pID)
    {
        if (pID < 100000) // product ID does not have at least 6 dgits or ID is negative
            throw new BO.FailedToDeleteObjectException(new BO.IlegalDataException("Ilegal ID"));
        IEnumerable<DO.Order?> Orderlst = Dal.Order.GetList();
        foreach (DO.Order? order in Orderlst)
        {
            IEnumerable<DO.OrderItem?> lstOi = Dal.OrderItem.GetList(item => item?.OrderId == order?.ID);
            if (lstOi.Where(Oitem => Oitem?.ProductId == pID).FirstOrDefault() != null)
                throw new BO.FailedToDeleteObjectException(new BO.ProductIsOrderedException());
        }
        try
        {
            Dal.Product.Delete(pID);
        }
        catch (Exception Ex) // if dal layer threw an exception (the product doesn't exist)
        {
            throw new BO.FailedToDeleteObjectException(Ex);
        }

    }

    public IEnumerable<BO.ProductForList?> GetAll(Func<BO.ProductForList?, bool>? condition)
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
            where condition is null ? true : condition(Bproduct)
            orderby Bproduct.Name
            select Bproduct;
        //select new BO.ProductForList()
        //{
        //    ID = product?.ID ?? 0,
        //    Name = product?.Name ?? "",
        //    Price = product?.Price ?? 0,
        //    Category = (BO.Enums.Category)product?.Category!
        //};

        //List<BO.ProductForList> lst = new List<BO.ProductForList>();
        //foreach (DO.Product? p in Plst) // for each product in dal create product for list
        //{
        //    if (p != null)
        //    {
        //        lst.Add(new BO.ProductForList()
        //        {
        //            ID = p?.ID ?? 0,
        //            Name = p?.Name,
        //            Price = p?.Price ?? 0,
        //            Category = (BO.Enums.Category)p?.Category!
        //        });
        //    }
        //}
        //return lst.AsEnumerable().Where(item => condition is null ? true : condition(item));
        return lst;
    }

    public BO.Product GetByID(int pID)
    {
        if (pID < 100000) // product ID does not have at least 6 dgits or ID is negative
            throw new BO.FailedGettingObjectException(new BO.IlegalDataException("Ilegal ID"));
        try
        {
            DO.Product? dproduct = Dal.Product.GetIf(item => item?.ID == pID);
            BO.Product bproduct = new BO.Product()
            {
                ID = dproduct?.ID ?? 0,
                Name = dproduct?.Name,
                Price = dproduct?.Price ?? 0,
                Category = (BO.Enums.Category)dproduct?.Category!,
                InStock = dproduct?.InStock ?? 0
            };
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
            BO.ProductItem bproduct = new BO.ProductItem()
            {
                ID = dproduct?.ID ?? 0,
                Name = dproduct?.Name,
                Price = dproduct?.Price ?? 0,
                Category = (BO.Enums.Category)dproduct?.Category!,
                InStock = inStock,
                Amount = crt.Items!.Find(item => item?.ProductID == pID)?.Amount ?? 0
            };
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
            DO.Product Dproduct = new DO.Product()
            {
                ID = Bproduct.ID,
                Name = Bproduct.Name,
                Price = Bproduct.Price,
                Category = (DO.Enums.Category)Bproduct.Category,
                InStock = Bproduct.InStock
            };
            Dal.Product.Update(Dproduct);
        }
        catch (Exception Ex)
        {
            throw new BO.FailedUpdatingObjectException(Ex);
        }

    }
}
