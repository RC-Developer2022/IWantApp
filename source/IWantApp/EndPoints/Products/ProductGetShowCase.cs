using IWantApp.Infra.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace IWantApp.EndPoints.Products;

public class ProductGetShowCase
{
    public static string Template => "/products/showcase";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    [AllowAnonymous]
    public static async Task<IResult> Action(ApplicationDbContext context, int page = 1, int row = 10, string orderby = "name")
    {
        if (row > 10)
            return Results.Problem(title: "Row with max 10", statusCode: 400);

        var queryBase = context.Produtos.AsNoTracking().Include(p => p.Category)
            .Where(p => p.HasStock && p.Category.Active);

        if (orderby == "name")
            queryBase = queryBase.OrderBy(p => p.Name);
        else if (orderby == "price")
            queryBase = queryBase.OrderBy(p => p.Price);
        else
            return Results.Problem(title: "Order only by price ir name", statusCode: 400);

        var queryFilter = queryBase.Skip((page - 1) * row).Take(row);

        var products = queryFilter.ToList();
        var results = products.Select(p => new ProductResponce(p.Name, p.Category.Name, p.Description, p.Price, p.HasStock, p.Active));
        return Results.Ok(results);
    }
}