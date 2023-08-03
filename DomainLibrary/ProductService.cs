using System;
using System.Collections.Generic;
using MaryRowan;

namespace DomainLibrary;

internal class ProductService
{
    private readonly CommerceContext dbContext;

    public ProductService()
    {
        dbContext = new CommerceContext();
    }

    public IEnumerable<Product> GetFeaturedProducts(bool isCustomerPreferred)
    {
        decimal discount = isCustomerPreferred ? .95m : 1;

        var featuredProducts = 
            from product in dbContext.Products
            where product.IsFeatured
            select product;

        return
            from product in featuredProducts.AsEnumerable()
            select new Product()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                IsFeatured = product.IsFeatured,
                UnitPrice = product.UnitPrice * discount,
            };
    }

}