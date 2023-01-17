using IWantApp.Infra.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace IWantApp.EndPoints.Products;

public class ProductGetAll
{
    public static string Template => "/products";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    [Authorize(Policy = "EmployeePolicy")]
    public static async Task<IResult> Action(ApplicationDbContext context)
    {
        var products = context.Produtos.Include(p => p.Category).OrderBy(p => p.Name).ToList();
        var results = products.Select(p => new ProductResponce(p.Name, p.Category.Name, p.Description, p.HasStock, p.Active));
        return Results.Ok(results);
    }
}