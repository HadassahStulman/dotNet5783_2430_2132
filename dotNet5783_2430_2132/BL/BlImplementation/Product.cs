
using BlApi;

namespace BlImplementation;

internal class Product : IProduct
{
    /// <summary>
    /// private field for allowing accsess from BL to Dal
    /// </summary>
    private DalApi.IDal Dal = new Dal.DalList();


    public void AddProduct(BO.Product Bproduct)
    {
        // checkig if all product's details are legal
        if (Bproduct.ID < 100000)
            throw new BO.FailedAddingObjectException(new BO.IlegalDataException("Ileal ID"));
        if (string.IsNullOrEmpty(Bproduct.Name))
            throw new BO.FailedAddingObjectException(new BO.IlegalDataException("Ilegal name"));
        if (Bproduct.Price <= 0)
            throw new BO.FailedAddingObjectException(new BO.IlegalDataException("Ilegal price"));
        if (Bproduct.InStock < 0)
            throw new BO.FailedAddingObjectException(new BO.IlegalDataException("Ilegal amount in stock"));
        if (Bproduct.Category != BO.Enums.Category.CookBooks && Bproduct.Category != BO.Enums.Category.TextBooks && Bproduct.Category != BO.Enums.Category.ReligiousBooks && Bproduct.Category != BO.Enums.Category.ReadingBooks && Bproduct.Category != BO.Enums.Category.ReligiousBooks && Bproduct.Category != BO.Enums.Category.ToddlerBooks)
            throw new BO.FailedAddingObjectException(new BO.IlegalDataException("Ilgal category"));
        if (!Dal.Product.isIDUniqe(Bproduct.ID))
            throw new BO.FailedAddingObjectException(new BO.IlegalDataException("ID is not uniqe"));
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
            IEnumerable<DO.OrderItem?> lstOi = Dal.OrderItem.GetList(item => item.Value.OrderId == order.Value.ID);
            foreach (DO.OrderItem? Oitem in lstOi)
                if (Oitem.Value.ProductId == pID) // the product to delete s ordered by someone
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

    public IEnumerable<BO.ProductForList?> GetAll()
    {
        IEnumerable<DO.Product?> Plst = Dal.Product.GetList();
        List<BO.ProductForList> lst = new List<BO.ProductForList>();
        foreach (DO.Product? p in Plst) // for each product in dal create product for list
        {
            if (p != null)
            {
                BO.ProductForList pfl = new BO.ProductForList()
                {
                    ID = p.Value.ID,
                    Name = p.Value.Name,
                    Price = p.Value.Price,
                    Category = (BO.Enums.Category)p.Value.Category
                };
                lst.Add(pfl);
            }
        }
        return lst;
    }


    public BO.Product GetByID(int pID)
    {
        if (pID < 100000) // product ID does not have at least 6 dgits or ID is negative
            throw new BO.FailedGettingObjectException(new BO.IlegalDataException("Ilegal ID"));
        try
        {
            DO.Product? dproduct = Dal.Product.GetByID(pID);
            BO.Product bproduct = new BO.Product()
            {
                ID = dproduct.Value.ID,
                Name = dproduct.Value.Name,
                Price = dproduct.Value.Price,
                Category = (BO.Enums.Category)dproduct.Value.Category,
                InStock = dproduct.Value.InStock
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
            DO.Product? dproduct = Dal.Product.GetByID(pID);
            bool inStock = false;
            if (dproduct.Value.InStock == 0)
                inStock = true;
            BO.ProductItem bproduct = new BO.ProductItem()
            {
                ID = dproduct.Value.ID,
                Name = dproduct.Value.Name,
                Price = dproduct.Value.Price,
                Category = (BO.Enums.Category)dproduct.Value.Category,
                InStock = inStock,
                Amount = crt.Items.Find(item => item.ProductID == pID).Amount
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
        // checkig if all product's details are legal
        if (Bproduct.ID < 100000)
            throw new BO.FailedAddingObjectException(new BO.IlegalDataException("Ileal ID"));
        if (string.IsNullOrEmpty(Bproduct.Name))
            throw new BO.FailedAddingObjectException(new BO.IlegalDataException("Ilegal name"));
        if (Bproduct.Price <= 0)
            throw new BO.FailedAddingObjectException(new BO.IlegalDataException("Ilegal price"));
        if (Bproduct.InStock < 0)
            throw new BO.FailedAddingObjectException(new BO.IlegalDataException("Ilegal amount in stock"));
        if (Bproduct.Category != BO.Enums.Category.CookBooks && Bproduct.Category != BO.Enums.Category.TextBooks && Bproduct.Category != BO.Enums.Category.ReligiousBooks && Bproduct.Category != BO.Enums.Category.ReadingBooks && Bproduct.Category != BO.Enums.Category.ReligiousBooks && Bproduct.Category != BO.Enums.Category.ToddlerBooks)
            throw new BO.FailedAddingObjectException(new BO.IlegalDataException("Ilegal category"));
        try
        {
            DO.Product Dproduct = new DO.Product() { ID = Bproduct.ID, Name = Bproduct.Name, Price = Bproduct.Price, Category = (DO.Enums.Category)Bproduct.Category, InStock = Bproduct.InStock };
            Dal.Product.Update(Dproduct);
        }
        catch (Exception Ex)
        {
            throw new BO.FailedUpdatingObjectException(Ex);
        }

    }
}
